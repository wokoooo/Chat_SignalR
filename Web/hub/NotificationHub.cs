using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Web.hub.Models;

namespace Web.hub
{
    public class NotificationHub : Hub<INotificationHub>
    {
        private static List<Client> clients = new List<Client>();
        public static string GetConnectionId(string username)
        {
            string connectionId = "";
            var user = clients.FirstOrDefault(x => x.Name == username);
            if (user != null)
            {
                connectionId = user.ConnectionId;
            }
            return connectionId;
        }
        public override Task OnConnected()
        {
            var user = clients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user == null)
            {
                clients.Add(new Client() { ConnectionId = Context.ConnectionId });
            }
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            if (!clients.Any(x => x.ConnectionId == Context.ConnectionId))
            {
                clients.Add(new Client() { ConnectionId = Context.ConnectionId });
            }
            return base.OnReconnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = clients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user != null)
            {
                clients.Remove(user);
            }
            Clients.All.updateUserList(clients.Where(x => x.Name != "").ToList());
            return base.OnDisconnected(stopCalled);
        }

        public void Login(string username, string email)
        {
            var user = clients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if(user == null)
            {
                user = new Client();
                user.ConnectionId = Context.ConnectionId;
                clients.Add(user);
            }
            user.Name = username;
            user.Email = email;
            Clients.All.updateUserList(clients.Where(x => x.Name != "").ToList());
        }
        public void Logout()
        {
            var user = clients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user != null)
            {
                clients.Remove(user);
            }
        }
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            var from_user = clients.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            var to_user = clients.FirstOrDefault(x => x.Name == name);
            if (to_user != null && from_user != null)
            {
                //Clients.User(to_user.ConnectionId).addNewMsgByUser(from_user.Name, message);
                Clients.Client(to_user.ConnectionId).addNewMsgByUser(from_user.Name, to_user.Name, message);
                Clients.Caller.addNewMsgByUser(from_user.Name, to_user.Name, message);
            }
        }


    }
}