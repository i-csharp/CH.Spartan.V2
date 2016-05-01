using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Json;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Castle.Core.Logging;
using CH.Spartan.Infrastructure;
using CH.Spartan.Messages;

namespace CH.Spartan.Jobs
{
    public class ActiveMqReceiveInstructionWorker : ISingletonDependency
    {
        private readonly ILogger _logger;
        private readonly ISettingManager _settingManager;
        private readonly MessageManager _messageManager;
        private IConnectionFactory _factory;
        private IConnection _connection;
        private ISession _session;
        private IMessageConsumer _consumer;
        private string _name;
        private string _uri;
        private bool _isConnected;
        private string _clientId = "ActiveMqReceiveInstructionWorker";

        public ActiveMqReceiveInstructionWorker(ILogger logger, ISettingManager settingManager, MessageManager messageManager)
        {
            _logger = logger;
            _settingManager = settingManager;
            _messageManager = messageManager;
        }

        public void DoWork(string clientId)
        {
            _clientId = $"{clientId}{_clientId}";
            if (!_isConnected)
            {
                TryConnect();
            }
        }

        private void TryConnect()
        {
            _logger.Info("开始连接指令队列服务器![{0}-{1}]".GetFormat(_name, _uri));
            try
            {
                _name = _settingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Instruction_Name);
                _uri = _settingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Instruction_Uri);
                _factory = new ConnectionFactory(_uri);
                _connection = _factory.CreateConnection();
                _connection.ConnectionInterruptedListener += () =>
                {
                    _logger.Error("与指令队列服务器断开连接![{0}-{1}]".GetFormat(_name, _uri));
                    _isConnected = false;
                };
                _connection.ClientId = _clientId;
                _connection.Start();
                _session = _connection.CreateSession();
                _consumer = _session.CreateDurableConsumer(new ActiveMQTopic(_name), _connection.ClientId, null, false);
                _consumer.Listener += OnReceived;
                _isConnected = true;
                _logger.Info("连接指令队列服务器成功![{0}-{1}]".GetFormat(_name, _uri));
            }
            catch (Exception ex)
            {
                _logger.Error("连接指令列服务器失败![{0}-{1}]".GetFormat(_name, _uri), ex);
                _isConnected = false;
            }
        }
        public void OnReceived(IMessage message)
        {
            try
            {
                var msg = (IObjectMessage)message;
                _logger.Info(msg.ToJsonString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

    }
}
