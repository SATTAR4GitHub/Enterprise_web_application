using Fashionplex.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fashionplex.Hubs
{
    /// <summary>
    /// This class uses method to send chat message and inherits fetures from the base class of the SignalR
    /// </summary>
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message) =>
           await Clients.All.SendAsync("receiveMessage", message);
    }
}
