using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using SmartCalendar.Models;
using SmartCalendar.Models.Abstracts;
using System;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using System.Web.Http;

namespace SmartCalendar.Hubs
{
    public class AlarmCheck : ApiController, IRegisteredObject
    {
        private readonly IHubContext _uptimeHub;
        private Timer _timer;

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

            var res = db.Events.Where(e => /*e.UserId == userId &&*/ (e.DateStart >= DateTime.Now))
                .ToList()
                .Where(e => e.DateStart <= DateTime.Now.AddDays(2))
                .OrderBy(e => e.DateStart);
            
            foreach (var e in res)
            {
                if (e != null )
                {
                    string str = e.DateStart.ToString("g");

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