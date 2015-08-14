using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SmartCalendar.Models;

namespace SmartCalendar.Hubs
{
    public class AlarmHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            {
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }
    }
}