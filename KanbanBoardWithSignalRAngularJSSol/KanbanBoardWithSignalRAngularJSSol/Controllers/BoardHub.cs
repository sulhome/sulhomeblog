using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace KanbanBoardWithSignalRAngularJSSol.Controllers
{
    [HubName("KanbanBoard")]
    public class BoardHub : Hub
    {
        public void BoardUpdated()
        {
            Clients.All.BoardUpdated();
        }
    }
}