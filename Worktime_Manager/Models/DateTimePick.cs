using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Worktime_Manager.Models
{
    public class DateTimePick
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Hours_Today { get; set; }

        public TimeSpan OverTime_Today { get; set; }

        public TimeSpan OverTime_Total { get; set; }
    }
}
