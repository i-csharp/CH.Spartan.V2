using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Spartan.Devices;
using CH.Spartan.DeviceStocks;
using CH.Spartan.DeviceTypes;
using CH.Spartan.EntityFramework;
using CH.Spartan.Infrastructure;
using CH.Spartan.Nodes;
using CH.Spartan.Users;

namespace CH.Spartan.Migrations.SeedData
{
   public class DefaultDataBuilder
    {

        private readonly SpartanDbContext _context;

        public DefaultDataBuilder(SpartanDbContext context)
        {
            _context = context;
        }

       public void Build()
       {

           //添加节点
           CreateNode();
           //添加设备类型
           CreateDeviceType();

            //添加设备
           CreateDevice("粤B56325", "3562698756337", "18566369898", "隐藏安装", DeviceType.D10, User.DemoUserName);
           CreateDevice("粤B36598", "3563659856988", "18566365555", "隐藏安装", DeviceType.Gt02, User.DemoUserName);
           CreateDevice("粤B36598", "3563695986989", "18566369777", "安装车头", DeviceType.Gt06, User.DemoUserName);
           CreateDevice("桂A33333", "8563698789365", "13896569856", "隐藏安装", DeviceType.D10, User.DemoUserName);
           CreateDevice("桂A56778", "8569832366698", "13569878598", "安装车尾", DeviceType.Gt02, User.DemoUserName);
           CreateDevice("桂A07569", "8536987856399", "13698789669", "隐藏安装", DeviceType.Gt06, User.DemoUserName);
        }


       private void CreateDevice(string name, string no, string simNo, string description, string deviceTypeName,
           string userName)
       {

           var device = _context.Devices.FirstOrDefault(p => p.BNo == no);
           if (device == null)
           {
                var deviceType = _context.DeviceTypes.FirstOrDefault(p => p.Name == deviceTypeName);
                if (deviceType == null) return;

                var user = _context.Users.FirstOrDefault(p => p.UserName == userName);
                if (user?.TenantId == null) return;

                device = new Device
                {
                    BName = name,
                    BNo = no,
                    BSimNo = simNo,
                    BDescription = description,
                    BDeviceTypeId = deviceType.Id,
                    BIconType = "PlaneCar",
                    BNodeId = 1,
                    SLimitSpeed = SpartanConsts.DefaultLimitSpeed
                };
                device.BCode = DeviceTypeHelper.CreateCode(device, deviceType);
                device.UserId = user.Id;
                device.TenantId = user.TenantId.Value;
                device.CreatorUserId = user.Id;
                device.GLatitude = 23.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0;
                device.GLongitude = 113.00 + new Random(Guid.NewGuid().GetHashCode()).Next(10000, 90000) / 10000.0;
                device.GReportTime = DateTime.Now.AddSeconds(20);
                device.GReceiveTime = DateTime.Now;
                device.BExpireTime = DateTime.Now.AddYears(5);
                device.GIsLocated = true;
                device.GDirection = new Random(Guid.NewGuid().GetHashCode()).Next(0, 350);
                _context.Devices.Add(device);
                var deviceStocks = _context.DeviceStocks.FirstOrDefault(p => p.No == no && p.TenantId == user.TenantId.Value);
                if (deviceStocks == null)
                {
                    _context.DeviceStocks.Add(new DeviceStock()
                    {
                        No = no,
                        TenantId = user.TenantId.Value
                    });
                }
                _context.SaveChanges();
            }
       }

       private void CreateNode()
       {
           for (var i = 1; i <= 10; i++)
           {
               var nodeName = "T" + i;
               var node = _context.Nodes.FirstOrDefault(p => p.Name == nodeName);
               if (node == null)
               {
                   node = new Node
                   {
                       Name = nodeName,
                       HistoryTableName = "historydatas_" + i,
                       HistoryConnectionStringRead = "Server=localhost;Database=spartan_historydata;Uid=root;Pwd=123456",
                       HistoryConnectionStringWrite =
                           "Server=localhost;Database=spartan_historydata;Uid=root;Pwd=123456"
                   };
                   _context.Nodes.Add(node);
               }
           }
           _context.SaveChanges();
       }

       private void CreateDeviceType()
       {
           var d10 = _context.DeviceTypes.FirstOrDefault(p => p.Name == DeviceType.D10);
           if (d10 == null)
           {
               d10 = new DeviceType
               {
                   Name = DeviceType.D10,
                   CodeCreateRule = EnumCodeCreateRule.No,
                   SwitchInGateway = "120.24.100.60:8102",
                   ServiceCharge = 50,
                   Supplier = "",
                   Manufacturer = ""
               };
               _context.DeviceTypes.Add(d10);
           }

           var gt02 = _context.DeviceTypes.FirstOrDefault(p => p.Name == DeviceType.Gt02);
           if (gt02 == null)
           {
               gt02 = new DeviceType
               {
                   Name = DeviceType.Gt02,
                   CodeCreateRule = EnumCodeCreateRule.No,
                   SwitchInGateway = "120.24.100.60:8104",
                   ServiceCharge = 50,
                   Supplier = "",
                   Manufacturer = ""
               };
               _context.DeviceTypes.Add(gt02);
           }

           var gt06 = _context.DeviceTypes.FirstOrDefault(p => p.Name == DeviceType.Gt06);
           if (gt06 == null)
           {
               gt06 = new DeviceType
               {
                   Name = DeviceType.Gt06,
                   CodeCreateRule = EnumCodeCreateRule.No,
                   SwitchInGateway = "120.24.100.60:8103",
                   ServiceCharge = 50,
                   Supplier = "",
                   Manufacturer = ""
               };
               _context.DeviceTypes.Add(gt06);
           }


           var gt07 = _context.DeviceTypes.FirstOrDefault(p => p.Name == DeviceType.Gt07);
           if (gt07 == null)
           {
               gt07 = new DeviceType
               {
                   Name = DeviceType.Gt07,
                   CodeCreateRule = EnumCodeCreateRule.No,
                   SwitchInGateway = "120.24.100.60:8105",
                   ServiceCharge = 50,
                   Supplier = "",
                   Manufacturer = ""
               };
               _context.DeviceTypes.Add(gt07);
           }

           var h1 = _context.DeviceTypes.FirstOrDefault(p => p.Name == DeviceType.H1);
           if (h1 == null)
           {
               h1 = new DeviceType
               {
                   Name = DeviceType.H1,
                   CodeCreateRule = EnumCodeCreateRule.No,
                   SwitchInGateway = "120.24.100.60:8101",
                   ServiceCharge = 50,
                   Supplier = "",
                   Manufacturer = ""
               };
               _context.DeviceTypes.Add(h1);
           }

           var h5 = _context.DeviceTypes.FirstOrDefault(p => p.Name == DeviceType.H5);
           if (h5 == null)
           {
               h5 = new DeviceType
               {
                   Name = DeviceType.H5,
                   CodeCreateRule = EnumCodeCreateRule.No,
                   SwitchInGateway = "120.24.100.60:8101",
                   ServiceCharge = 50,
                   Supplier = "",
                   Manufacturer = ""
               };
               _context.DeviceTypes.Add(h5);
           }

           var t1 = _context.DeviceTypes.FirstOrDefault(p => p.Name == DeviceType.T1);
           if (t1 == null)
           {
               t1 = new DeviceType
               {
                   Name = DeviceType.T1,
                   CodeCreateRule = EnumCodeCreateRule.No,
                   SwitchInGateway = "120.24.100.60:8106",
                   ServiceCharge = 50,
                   Supplier = "",
                   Manufacturer = ""
               };
               _context.DeviceTypes.Add(t1);
           }

           var t2 = _context.DeviceTypes.FirstOrDefault(p => p.Name == DeviceType.T2);
           if (t2 == null)
           {
               t2 = new DeviceType
               {
                   Name = DeviceType.T2,
                   CodeCreateRule = EnumCodeCreateRule.No,
                   SwitchInGateway = "120.24.100.60:8106",
                   ServiceCharge = 50,
                   Supplier = "",
                   Manufacturer = ""
               };
               _context.DeviceTypes.Add(t2);
           }
           _context.SaveChanges();
       }
    }
}
