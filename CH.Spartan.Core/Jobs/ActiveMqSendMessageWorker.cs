using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Notifications;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Castle.Core.Logging;
using CH.Spartan.Infrastructure;
using CH.Spartan.Messages;

namespace CH.Spartan.Jobs
{
    public class ActiveMqSendMessageWorker : ISingletonDependency
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
        public ActiveMqSendMessageWorker(ILogger logger, ISettingManager settingManager)
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
            else
            {
#if DEBUG
              Send(new GetwayMessage()
                {
                    Title = "震动报警",
                    UserId = 4,
                    DeviceId = 2171,
                    AlarmType = EnumAlarmType.Shake,
                    Content = "震动报警",
                    Latitude = 45.77378,
                    Longitude = 126.746296666667,
                    Severity = NotificationSeverity.Warn
                });
#endif
            }
        }

        private void TryConnect()
        {
            try
            {
                _name = _settingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Message_Name);
                _uri = _settingManager.GetSettingValueForApplication(SpartanSettingKeys.General_ActiveMq_Message_Uri);
                _logger.Info("开始连接消息队列服务器![{0}-{1}]".GetFormat(_name, _uri));
                _factory = new ConnectionFactory(_uri);
                _connection = _factory.CreateConnection();
                _connection.ExceptionListener += (p) =>
                {
                    _logger.Error("与消息队列服务器断开连接![{0}-{1}]".GetFormat(_name, _uri));
                    _isConnected = false;
                };
                _connection.ClientId = $"{_clientId}ActiveMqSendMessageWorker";
                _connection.Start();
                _session = _connection.CreateSession();
                _producer = _session.CreateProducer(new ActiveMQTopic(_name));
                _isConnected = true;
                _logger.Info("连接消息队列服务器成功![{0}-{1}]".GetFormat(_name, _uri));
            }
            catch (Exception ex)
            {
                _logger.Error("连接消息队列服务器失败![{0}-{1}]".GetFormat(_name, _uri), ex);
                _isConnected = false;
            }
        }

        public bool Send(GetwayMessage message)
        {
            try
            {
                if (_isConnected)
                {
                    _producer.Send(new ActiveMQObjectMessage() { Body = message }, MsgDeliveryMode.NonPersistent,
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
