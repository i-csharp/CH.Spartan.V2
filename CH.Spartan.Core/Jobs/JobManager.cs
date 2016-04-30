using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using CH.Spartan.Domain;

namespace CH.Spartan.Jobs
{
    public class JobManager : ManagerBase
    {
        public JobManager(
            ISettingManager settingManager,
            ICacheManager cacheManager,
            IIocResolver iocResolver,
            IUnitOfWorkManager unitOfWorkManager) : base(settingManager, cacheManager, iocResolver, unitOfWorkManager)
        {
        }

        public void GetGetwayNotification()
        {

        }
    }
}
