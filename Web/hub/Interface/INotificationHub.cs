using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.hub.Models;

namespace Web.hub
{
    public interface INotificationHub
    {
        void login(string username);
        void logout();
        void updateUserList(List<Client> clients);
        void addNewMsgByUser(string from_user, string to_user, string msg);
        void addNewMsg(string from_user, string msg);
    }
}
