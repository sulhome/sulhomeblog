using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KanbanBoardWithSignalRAngularJSSol.Models
{
    public class BoardRepository
    {
        public Column GetColumn(int colId)
        {
            return (from c in this.GetColumns()
                    where c.Id == colId
                    select c).FirstOrDefault();
        }

        public Task GetTask(int taskId)
        {
            var columns = this.GetColumns();
            foreach (var c in columns)
            {
                foreach (var task in c.Tasks)
                {
                    if (task.Id == taskId)
                        return task;
                }
            }

            return null;
        }

        public List<Column> GetColumns()
        {
            if (HttpContext.Current.Cache["columns"] == null)
            {
                var columns = new List<Column>();
                var tasks = new List<Task>();
                for (int i = 1; i < 6; i++)
                {
                    tasks.Add(new Task { ColumnId = 1, Id = i, Name = "Task " + i, Description = "Task " + i + "Description" });
                }
                columns.Add(new Column { Description = "to do column", Id = 1, Limit = 8, Name = "to do", Tasks = tasks });
                columns.Add(new Column { Description = "in progress column", Id = 2, Limit = 3, Name = "in progress", Tasks = new List<Task>() });
                columns.Add(new Column { Description = "test column", Id = 3, Limit = 3, Name = "test", Tasks = new List<Task>() });
                columns.Add(new Column { Description = "done column", Id = 4, Limit = 8, Name = "done", Tasks = new List<Task>() });
                HttpContext.Current.Cache["columns"] = columns;
            }
            return (List<Column>)HttpContext.Current.Cache["columns"];
        }

        public void MoveTask(int taskId, int targetColId)
        {
            var columns = this.GetColumns();
            var targetColumn = this.GetColumn(targetColId);
            var task = this.GetTask(taskId);
            var sourceColId = task.ColumnId;
            task.ColumnId = targetColId;
            targetColumn.Tasks.Add(task);

            var sourceCol = this.GetColumn(sourceColId);
            sourceCol.Tasks.RemoveAll(t => t.Id == taskId);

            columns.RemoveAll(c => c.Id == sourceColId || c.Id == targetColId);
            columns.Add(targetColumn);
            columns.Add(sourceCol);

            this.UpdateColumns(columns.OrderBy(c => c.Id).ToList());
        }

        private void UpdateColumns(List<Column> columns)
        {
            HttpContext.Current.Cache["columns"] = columns;
        }
    }
}