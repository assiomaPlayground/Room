using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models.Types
{
    public class DeltaTime
    {
        //StarTime
        public string StartTime { get; set; }

        //EndTime
        public string EndTime { get; set; }

        //Equals
        public override bool Equals(object obj)
            => ((obj as DeltaTime).StartTime == this.StartTime && (obj as DeltaTime).EndTime == this.EndTime);

        //GetHashCode
        public override int GetHashCode()
            => base.GetHashCode();
    }
}
