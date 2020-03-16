using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace RoomService.Utils
{
    public class ServerTaskUtils
    {
        public void CreateTimeBasedServerTask(DateTime time, Func<bool> op)
        {
            var now      = DateTime.Now;
            var tickTime = (time - now).TotalMilliseconds;
            if (tickTime < 1)
                return;
            var timer = new Timer(tickTime)
            {
                AutoReset = false
            };
            timer.Elapsed += (sender, e) => RaiseEvent(sender, e, op);
            timer.Start();
        }

        private void RaiseEvent(object o, ElapsedEventArgs e, Func<bool> op)
        {
            op.Invoke();
        }
    }

    public enum TaskTypes
    {
        RESSTATUSUPDATE,
        RESNOTIFIER
    }
    public struct ServerTimeTaskData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskTypes TaskType { get; set; }
        public int Hour { get; set; }
    };
}
