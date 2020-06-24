using System;
using System.Threading;
using System.Threading.Tasks;
using Qmos.Helper;
using Qmos.Manager;
using Microsoft.Extensions.Logging;

namespace Qmos.UI.Services
{
    public class MyCronJob1 : CronJobService
    {
        private readonly ILogger<MyCronJob1> _logger;
        IEmailSender Mailer;
        //IUpdater Updater;

        public MyCronJob1(IScheduleConfig<MyCronJob1> config, ILogger<MyCronJob1> logger, IEmailSender mailer)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            Mailer = mailer;
            //Updater = updater;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //Mailer.SendEmailAsync("info@key-core.com", "CronJob 1 starts", "CronJob 1 starts", null);
            //Updater.GetCustomerGroups();
            //Updater.GetItems();
            //Updater.GetWarehouses();
            //Updater.GetCustomers();
            //Updater.GetStock();
            //Updater.GetDiscountGroups();
            //Updater.GetSaleEmployees();
            //Updater.GetPriceLists();
            //Updater.GetManufacters();
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            //Updater.GetCustomerGroups();
            //Updater.GetItems();
            //Updater.GetWarehouses();
            //Updater.GetCustomers();
            //Updater.GetStock();
            //Updater.GetDiscountGroups();
            //Updater.GetSaleEmployees();
            //Updater.GetPriceLists();
            //Updater.GetManufacters();
            //_logger.LogInformation($"{DateTime.Now:hh:mm:ss} CronJob 1 is working");
            //Mailer.SendEmailAsync("info@key-core.com", "CronJob 1 is working", "CronJob 1 is working", null);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("CronJob 1 is stopping.");
            //Mailer.SendEmailAsync("info@key-core.com", "CronJob 1 is stopping", "CronJob 1 is stopping", null);
            return base.StopAsync(cancellationToken);
        } 
    }
}
