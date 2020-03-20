using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace RoomService.Utils
{
    /// <summary>
    /// Utility Service for server Timer Based Tasks 
    /// </summary>
    public class ServerTaskUtils
    {
        /// <summary>
        /// Create a task that runs at time param 
        /// </summary>
        /// <param name="time">When should execute the operation</param>
        /// <param name="op">The delegate operation to run</param>
        public void CreateTimeBasedServerTask(DateTime time, Func<bool> op)
        {
            //Create a timer from now to time (target time)
            var now      = DateTime.Now;
            var tickTime = (time - now).TotalMilliseconds;
            //Validate the timer
            if (tickTime < 1)
                return;
            //Instantiate the timer
            var timer = new Timer(tickTime)
            {
                AutoReset = false
            };
            //Setup and run the delegate func on timer elapsed
            timer.Elapsed += (sender, e) => RaiseEvent(sender, e, op);
            //Start the timer
            timer.Start();
        }
        /// <summary>
        /// Event raiser wrapper
        /// </summary>
        /// <param name="o">Timer object</param>
        /// <param name="e">Timer args</param>
        /// <param name="op">The operation to run</param>
        #pragma warning disable IDE0060 // Remove unused param it's needed by timer callback even if not used
        private void RaiseEvent(object o, ElapsedEventArgs e, Func<bool> op)
        #pragma warning restore IDE0060
        {
            op.Invoke();
        }
    }
    /// <summary>
    /// Task types enum definition
    /// @TODO: Should be moved in a new file
    /// </summary>
    public enum TaskTypes
    {
        /// <summary>
        /// Status for update task selection
        /// </summary>
        RESSTATUSUPDATE,
        /// <summary>
        /// Status for Notification tasks selection
        /// </summary>
        RESNOTIFIER
    }
    /// <summary>
    /// Server Time Task Data for settings propose
    /// @TODO: Should be moved in a new file
    /// </summary>
    public struct ServerTimeTaskData
    {
        /// <summary>
        /// Task name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Task description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Task type
        /// </summary>
        public TaskTypes TaskType { get; set; }
        /// <summary>
        /// Hour of the day when task will run
        /// </summary>
        public int Hour { get; set; }
    };
}
