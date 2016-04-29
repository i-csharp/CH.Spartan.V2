using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using CH.Spartan.Domain;
using CH.Spartan.MultiTenancy;

namespace CH.Spartan.HistoryDatas
{
    public class HistoryDataManager : ManagerBase
    {
        public HistoryDataManager(
            ISettingManager settingManager,
            ICacheManager cacheManager,
            IIocResolver iocResolver,
            IUnitOfWorkManager unitOfWorkManager)
            : base(settingManager, cacheManager, iocResolver, unitOfWorkManager)
        {

        }
    }
}
