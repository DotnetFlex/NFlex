using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Demo.Web.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {

        }
        public void Send(string name,string message)
        {
            
        }
    }
}