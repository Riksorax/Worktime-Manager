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

        public String Date { get; set; }

        public String Hours_Today { get; set; }

        public String OverTime_Today { get; set; }

        public String OverTime_Total { get; set; }
    }
}
