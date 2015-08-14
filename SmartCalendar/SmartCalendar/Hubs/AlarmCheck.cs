using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using SmartCalendar.Hubs;
using SmartCalendar.Models;
using SmartCalendar.Models.Abstracts;
using SmartCalendar.Models.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace SmartCalendar.Hubs
{
    public class AlarmCheck : ApiController, IRegisteredObject
    {
        private readonly IHubContext _uptimeHub;
        private Timer _timer;
        public DateTime StartDate = DateTime.Now;
        int x = 0;

        private IRepository repository;

        ApplicationDbContext db = new ApplicationDbContext();

        public AlarmCheck(IRepository repos)
        {
            repository = repos;
        }

        public AlarmCheck()

        {
            _uptimeHub = GlobalHost.ConnectionManager.GetHubContext<AlarmHub>();

            StartTimer();
        }


        private void StartTimer()
        {
            var delayStartby = 1000;
            var repeatEvery = 60000;

            _timer = new Timer(BroadcastUptimeToClients, null, delayStartby, repeatEvery);
        }
        private void BroadcastUptimeToClients(object state)
        {
            var userId = User.Identity.GetUserId();
            string str = null;
            var res = db.Events.Where(x => x.UserId == userId && (x.DateStart >= StartDate)).ToList().Where(x => x.DateStart <= DateTime.Now.AddDays(2)).OrderBy(x => x.DateStart);

            foreach (var e in res)
            {
                if (e != null)
                {
                    str = e.DateStart.ToString("g");


                    if (DateTime.Now.AddMinutes(5).ToString("g").Equals(str))
                        _uptimeHub.Clients.All.addNewMessageToPage(e.Title, e.DateStart.ToString("g"));
                    break;
                }
            }

        }

        public void Stop(bool immediate)
        {
            _timer.Dispose();

            HostingEnvironment.UnregisterObject(this);
        }
    }
}