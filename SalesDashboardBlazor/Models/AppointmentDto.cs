using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboardBlazor.Models
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? ManagerId { get; set; }

        public int? RoomId { get; set; }

        public bool IsAllDay { get; set; }

        public string RecurrenceRule { get; set; }

        public int? RecurrenceId { get; set; }

        public string RecurrenceExceptions { get; set; }

        public string StartTimezone { get; set; }

        public string EndTimezone { get; set; }
    }
}
