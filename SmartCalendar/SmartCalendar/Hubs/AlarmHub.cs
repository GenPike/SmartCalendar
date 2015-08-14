using Microsoft.AspNet.SignalR;

namespace SmartCalendar.Hubs
{
    public class AlarmHub : Hub
    {

        //    static List<User> Users = new List<User>();
        // Отправка сообщений
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            {
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }
    }
}