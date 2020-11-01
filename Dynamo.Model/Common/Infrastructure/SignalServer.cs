using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.Model.Common.Infrastructure
{
    public class SignalServer : Hub
    {

        public async Task Send()
        {
          
            await Clients.All.SendAsync("Receive");
        }
        public async Task SendMessage()
        {

            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}
