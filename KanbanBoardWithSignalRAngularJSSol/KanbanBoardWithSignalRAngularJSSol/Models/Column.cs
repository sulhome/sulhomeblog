using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KanbanBoardWithSignalRAngularJSSol.Models
{
    public partial class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public virtual List<Task> Tasks { get; set; }
    }
}