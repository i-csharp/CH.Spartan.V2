using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Events.Bus;
using Abp.Json;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Castle.Core.Logging;
using CH.Spartan.Infrastructure;
using CH.Spartan.Notifications;

namespace CH.Spartan.Jobs
{
    public abstract class ActiveMqReceiveWorker
    {
        protected readonly ILogger Logger;
        protected readonly ISettingManager SettingManager;
        protected readonly IEventBus EventBus;
        private IConnectionFactory _factory;
        private IConnection _connection;
        private ISession _session;
        private IMessageConsumer _consumer;
       
        protected string ClientId { get; set; }
        protected string Name { get; set; }
        protected string Uri { get; set; }
        protected bool IsConnected { get; set; }

        protected ActiveMqReceiveWorker(ILogger logger, ISettingManager settingManager, IEventBus eventBus)
        {
            Logger = logger;
            SettingManager = settingManager;
            EventBus = eventBus;
        }

        protected virtual void TryConnect()
        {
            try
            {
                Logger.Info($"开始连接队列服务器![Receive-{Name}-{Uri}]");
                _factory = new ConnectionFactory(Uri);
                _connection = _factory.CreateConnection();
                _connection.ExceptionListener += (p) =>
                {
                    Logger.Error($"与队列服务器断开连接![Receive-{Name}-{Uri}]");
                    IsConnected = false;
                };
                _connection.ClientId = $"{ClientId}ActiveMqReceive{Name}Worker";
                _connection.Start();
                _session = _connection.CreateSession();
                _consumer = _session.CreateDurableConsumer(new ActiveMQTopic(Name), _connection.ClientId, null, false);
                _consumer.Listener += OnReceived;
                IsConnected = true;
                Logger.Info($"连接队列服务器成功![Receive-{Name}-{Uri}]");
            }
            catch (Exception ex)
            {
                Logger.Error($"连接对列服务器失败![Receive-{Name}-{Uri}]", ex);
                IsConnected = false;
            }
        }
        protected virtual void OnReceived(IMessage message)
        {
            try
            {
                Received(message as IObjectMessage);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }

        public abstract void DoWork(string clientId);

        protected abstract void Received(IObjectMessage message);
    }
}
