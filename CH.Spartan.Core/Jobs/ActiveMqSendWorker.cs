using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Castle.Core.Logging;
using CH.Spartan.Infrastructure;
using CH.Spartan.Instructions;

namespace CH.Spartan.Jobs
{
    public abstract class ActiveMqSendWorker : ISingletonDependency
    {
        protected readonly ILogger Logger;
        protected readonly ISettingManager SettingManager;
        private IConnectionFactory _factory;
        private IConnection _connection;
        private ISession _session;
        private IMessageProducer _producer;
        protected string Name;
        protected string Uri;
        protected bool IsConnected;
        protected string ClientId = "";
        protected ActiveMqSendWorker(ILogger logger, ISettingManager settingManager)
        {
            Logger = logger;
            SettingManager = settingManager;
        }

        public abstract void DoWork(string clientId);

        protected virtual void TryConnect()
        {
            try
            {
                Logger.Info($"开始连接队列服务器![{Name}-{Uri}]");
                _factory = new ConnectionFactory(Uri);
                _connection = _factory.CreateConnection();
                _connection.ExceptionListener += (p) =>
                {
                    Logger.Error($"与Web事件队列服务器断开连接![{Name}-{Uri}]");
                    IsConnected = false;
                };
                _connection.ClientId = $"{ClientId}ActiveMqSend{Name}Worker";
                _connection.Start();
                _session = _connection.CreateSession();
                _producer = _session.CreateProducer(new ActiveMQTopic(Name));
                IsConnected = true;
                Logger.Info($"连接队列服务器成功![{Name}-{Uri}]");
            }
            catch (Exception ex)
            {
                Logger.Error($"连接队列服务器失败![{Name}-{Uri}]", ex);
                IsConnected = false;
            }
        }

        public Task<bool> SendAsync(object message)
        {
            try
            {
                if (IsConnected)
                {
                    Task.Run(()=>
                    {
                        _producer.Send(new ActiveMQObjectMessage() { Body = message }, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);
                    });
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                IsConnected = false;
            }
            return Task.FromResult(false);
        }

    }
}
