﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Castle.Core.Logging;
using CH.Spartan.Infrastructure;
using CH.Spartan.Instructions;

namespace CH.Spartan.Jobs
{
    public abstract class ActiveMqSendWorker: PeriodicBackgroundWorkerBase
    {
        private IConnectionFactory _factory;
        private IConnection _connection;
        private ISession _session;
        private IMessageProducer _producer;
        protected string Name;
        protected string Uri;
        protected bool IsConnected;
        protected string ClientId = "";

        protected ActiveMqSendWorker(AbpTimer timer):base(timer)
        {
            timer.Period = 15*1000;
        }

        protected virtual void TryConnect()
        {
            try
            {
                Logger.Info($"开始连接队列服务器![Send-{Name}-{Uri}]");
                _factory = new ConnectionFactory(Uri);
                _connection = _factory.CreateConnection();
                _connection.ExceptionListener += (p) =>
                {
                    Logger.Error($"与队列服务器断开连接![Send-{Name}-{Uri}]");
                    IsConnected = false;
                };
                _connection.ClientId = $"{ClientId}ActiveMqSend{Name}Worker";
                _connection.Start();
                _session = _connection.CreateSession();
                _producer = _session.CreateProducer(new ActiveMQTopic(Name));
                IsConnected = true;
                Logger.Info($"连接队列服务器成功![Send-{Name}-{Uri}]");
            }
            catch (Exception ex)
            {
                Logger.Error($"连接队列服务器失败![Send-{Name}-{Uri}]", ex);
                IsConnected = false;
            }
        }

        public void SendAsync(object message)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (IsConnected)
                    {
                        _producer.Send(new ActiveMQObjectMessage() {Body = message}, MsgDeliveryMode.NonPersistent,
                            MsgPriority.Normal, TimeSpan.MinValue);
                    }
                    else
                    {
                        Logger.Error($"队列服务器没有建立连接![Send-{Name}-{Uri}]");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString());
                    IsConnected = false;
                }
            });
        }
    }
}
