using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomService.Models;
using RoomService.Models.Types;

namespace RoomService.Utils
{
    /// <summary>
    /// Exstensin methods for manage DeltaTime Class
    /// </summary>
    public static class DeltaTimeExtensions
    {
        //@TODO Use settings
        //Hours of the day to hook
        private readonly static int _morningStartHour   = 9;
        private readonly static int _morningEndHour     = 13;
        private readonly static int _afternoonStartHour = 14;
        private readonly static int _afternoonEndHour   = 18;
        /// <summary>
        /// Validator for DeltaTime class
        /// <para>
        ///     Delta time valid definition is a delta time that has EndTime attribute 
        ///     Bigger than StartTime attribute, the two values should be string "o" (ISO 8601) formatted DateTime classes
        /// </para>
        /// </summary>
        /// <param name="deltaTime">Target to validate</param>
        /// <returns>The bool that indicates the valid status</returns>
        public static bool IsValid(this DeltaTime deltaTime)
            => (string.Compare(deltaTime.StartTime, deltaTime.EndTime) < 0);
        /// <summary>
        /// Clamp DeltaTime attributes inside nearests hours of the day to hook
        /// </summary>
        /// <param name="delta">The target deltatime</param>
        /// <returns>A new DeltaTime with clamped attributes</returns>
        public static DeltaTime Clamp(this DeltaTime delta)
        {
            //Parse to date for manage
            DateTime start = DateTime.Parse(delta.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            //Get candidate hooks
            var StartMorning   = new DateTime(start.Year, start.Month, start.Day, _morningStartHour,   0, 0, start.Kind);
            var StartAfternoon = new DateTime(start.Year, start.Month, start.Day, _afternoonStartHour, 0, 0, start.Kind);
            //Set the nearest candidate
            start = HourDistance(start, StartMorning) < HourDistance(start, StartAfternoon) ? StartMorning : StartAfternoon;
            //Parse to date for manage
            DateTime end = DateTime.Parse(delta.EndTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            //Get candidate hooks
            var EndMorning   = new DateTime(end.Year, end.Month, end.Day, _morningEndHour,   0, 0, end.Kind);
            var EndAfternoon = new DateTime(end.Year, end.Month, end.Day, _afternoonEndHour, 0, 0, end.Kind);
            //Set the nearest candidate
            end = HourDistance(end, EndMorning) < HourDistance(end, EndAfternoon) ? EndMorning : EndAfternoon;
            //Return a new clamped class
            return new DeltaTime { StartTime = start.ToString("o"), EndTime = end.ToString("o") };
        }
        /// <summary>
        /// Get the first DeltaTime Interval inside a DeltaTime to split into defined units
        /// </summary>
        /// <param name="delta">The DeltaTime to iterate</param>
        /// <returns>The first DeltaTime day interval</returns>
        public static DeltaTime First(this DeltaTime delta)
        {
            //Parse the interval to date for manage
            DateTime start = DateTime.Parse(delta.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            //Same for end NOTE: Could be optimized using a clone of start
            DateTime end   = DateTime.Parse(delta.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            //Use add for set the end DateTime unit
            if (start.Hour == _morningStartHour)
                end = end.AddHours(_morningEndHour - _morningStartHour);
            if (start.Hour == _afternoonStartHour)
                end = end.AddHours(_afternoonEndHour - _afternoonStartHour);
            //Else Not clamped DeltaTime ERROR
            //Return the new DeltaTime having "o" (ISO 8601) DateTime string formatted
            return new DeltaTime { StartTime = start.ToString("o"), EndTime = end.ToString("o") };
        }
        /// <summary>
        /// Get the next DeltaTime Interval inside a Deltatime to split into defined units
        /// </summary>
        /// <param name="delta">The DeltaTime to iterate</param>
        /// <returns>The next DeltaTime day interval</returns>
        public static DeltaTime Next(this DeltaTime delta) 
        {
            //Parse the interval to date for manage
            DateTime start = DateTime.Parse(delta.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            //Same for end NOTE: Could be optimized using a clone of start
            DateTime end   = DateTime.Parse(delta.StartTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            //Get the last item of sequence
            DateTime last = DateTime.Parse(delta.EndTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
            //Sets the start and the end to next unit interval
            if (start.Hour == _morningStartHour)
            {
                start = start.AddHours(_afternoonStartHour - _morningStartHour);
                end   = end  .AddHours(_afternoonEndHour   - _morningStartHour);
            }
            //Add a Day if the next unit is the last of the day and restart from first time hook
            else if (start.Hour == _afternoonStartHour)
            {
                start = start.AddDays(1).AddHours(_morningStartHour - _afternoonStartHour);
                end   = end  .AddDays(1).AddHours(_morningEndHour   - _afternoonStartHour);
            }
            //Else Not clamped DeltaTime ERROR
            //Return null if last item of the sequence has been reached
            if (end > last)
                return null;
            //Return the new DeltaTime having "o" (ISO 8601) DateTime string formatted 
            return new DeltaTime { StartTime = start.ToString("o"), EndTime = end.ToString("o") };
        }
        /// <summary>
        /// Return the linear distance between two Dates expressed in hours
        /// </summary>
        /// <param name="a">First date</param>
        /// <param name="b">Second date</param>
        /// <returns></returns>
        private static double HourDistance(DateTime a, DateTime b)
            => Math.Abs((a - b).TotalHours);      
    }
}
