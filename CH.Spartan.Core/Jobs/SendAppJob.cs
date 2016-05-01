using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using CH.Spartan.Users;

namespace CH.Spartan.Jobs
{
    [Serializable]
    public class SendAppJobArgs
    {
        public long SenderUserId { get; set; }

        public int? TargetTenantId { get; set; }

        public long TargetUserId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }

    public class SendAppJob : BackgroundJob<SendAppJobArgs>, ITransientDependency
    {
        private readonly IRepository<User, long> _userRepository;

        public SendAppJob(IRepository<User, long> userRepository)
        {
            _userRepository = userRepository;
        }

        [UnitOfWork]
        public override void Execute(SendAppJobArgs args)
        {
            using (CurrentUnitOfWork.SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, args.TargetTenantId))
            {
                var user = _userRepository.FirstOrDefault(args.TargetUserId);
                if (user == null)
                {
                    Logger.WarnFormat("Unknown userId: {0}. Can not execute job!", args.TargetUserId);
                    return;
                }

                //Here, we should actually send the App! We can inject and use IAppSender for example.
                Logger.Info("Sending App to " + user.Name + " in background job -> " + args.Subject);
                Logger.Info(args.Body);
            }
        }
    }
}
