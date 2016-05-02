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
    public class SendEmailJobArgs
    {
        public int? TargetTenantId { get; set; }

        public long TargetUserId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }

    public class SendEmailJob : BackgroundJob<SendEmailJobArgs>, ITransientDependency
    {
        private readonly IRepository<User, long> _userRepository;

        public SendEmailJob(IRepository<User, long> userRepository)
        {
            _userRepository = userRepository;
        }

        [UnitOfWork]
        public override void Execute(SendEmailJobArgs args)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var user = _userRepository.FirstOrDefault(args.TargetUserId);
                if (user == null)
                {
                    Logger.WarnFormat("Unknown userId: {0}. Can not execute job!", args.TargetUserId);
                    return;
                }

                //Here, we should actually send the email! We can inject and use IEmailSender for example.
                Logger.Info("Sending email to " + user.EmailAddress + " in background job -> " + args.Subject);
                Logger.Info(args.Body);
            }
        }
    }
}
