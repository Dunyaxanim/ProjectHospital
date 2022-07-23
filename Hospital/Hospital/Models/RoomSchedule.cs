using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class RoomSchedule
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
    }
}
