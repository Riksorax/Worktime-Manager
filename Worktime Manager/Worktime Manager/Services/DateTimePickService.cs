using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Worktime_Manager.Models;
using Xamarin.Essentials;

namespace Worktime_Manager.Services
{
    public static class DateTimePickService
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;
            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<DateTimePick>();
        }

        public static async Task AddDateTimePick(DateTime date, TimeSpan hours_today, TimeSpan overTime_today, TimeSpan overTime_total)
        {
            await Init();
            var dateTimePick = new DateTimePick
            {
                Date = date,
                Hours_Today = hours_today,
                OverTime_Today = overTime_today,
                OverTime_Total = overTime_total
            };
            if (dateTimePick.Id != 0)
            {
                await db.UpdateAsync(dateTimePick);
            }
            else
            {
                await db.InsertAsync(dateTimePick);
            }
           //var id = await db.InsertAsync(dateTimePick);
        }

        public static async Task RemoveDateTimePick(int id)
        {
            await Init();

            await db.DeleteAsync<DateTimePick>(id);
        }
        public static async Task<IEnumerable<DateTimePick>> GetDateTimePick()
        {
            await Init();

            var dateTimePick = await db.Table<DateTimePick>().ToListAsync();
            return dateTimePick;
        }
    }

        
    
}
