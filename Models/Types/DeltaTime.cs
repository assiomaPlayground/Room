using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.Models.Types
{
    public class DeltaTime
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public override bool Equals(object obj)
            => ((obj as DeltaTime).StartTime == this.StartTime && (obj as DeltaTime).EndTime == this.EndTime);
        public override int GetHashCode()
            => base.GetHashCode();
    }
}
