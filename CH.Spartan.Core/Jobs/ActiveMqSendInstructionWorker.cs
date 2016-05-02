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
using CH.Spartan.Messages;

namespace CH.Spartan.Jobs
{
    public class ActiveMqSendInstructionWorker : ISingletonDependency
    {
        private readonly ILogger _logger;
        private readonly ISettingManager _settingManager;
        private IConnectionFactory _factory;
        private IConnection _connection;
        private ISession _session;
        private IMessageProducer _producer;
        private string _name;
        private string _uri;
        private bool _isConnected;
        private string _clientId = "";
        public ActiveMqSendInstructionWorker(ILogger logger, ISettingManager settingManager)
        {
            _logger = logger;
            _settingManager = settingManager;
        }

        public void DoWork(string clientId)
        {
            if (!_isConnected)
            {
                _clientId = clientId;
                TryConnect();
            }
        }

        private void TryConnect()
        {
            try
            {
                _name = _settingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Instruction_Name);
                _uri = _settingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Instruction_Uri);
                _logger.Info("开始连接指令队列服务器![{0}-{1}]".GetFormat(_name, _uri));
                _factory = new ConnectionFactory(_uri);
                _connection = _factory.CreateConnection();
                _connection.ExceptionListener += (p) =>
                {
                    _logger.Error("与指令队列服务器断开连接![{0}-{1}]".GetFormat(_name, _uri));
                    _isConnected = false;
                };
                _connection.ClientId = $"{_clientId}ActiveMqSendInstructionWorker";
                _connection.Start();
                _session = _connection.CreateSession();
                _producer = _session.CreateProducer(new ActiveMQTopic(_name));
                _isConnected = true;
                _logger.Info("连接指令队列服务器成功![{0}-{1}]".GetFormat(_name, _uri));
            }
            catch (Exception ex)
            {
                _logger.Error("连接指令队列服务器失败![{0}-{1}]".GetFormat(_name, _uri), ex);
                _isConnected = false;
            }
        }

        public bool Send(InstructionMessage message)
        {
            try
            {
                if (_isConnected)
                {
                    _producer.Send(new ActiveMQObjectMessage() {Body = message }, MsgDeliveryMode.NonPersistent,
                        MsgPriority.Normal, TimeSpan.MinValue);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                _isConnected = false;
            }
            return false;
        }

    }
}
