using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worktime_Manager.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Windows.Input;
using Worktime_Manager.Services;

namespace Worktime_Manager.ViewModels
{
    public partial class DateTimeCalculatePageViewModel : ViewModelBase
    {
        public ICommand WorkTimeComplete { get; }
        public DateTime DateToday { get; set; }
        public TimeSpan startTimePicker { get; set; }
        public TimeSpan endTimePicker { get; set; }
        public TimeSpan breakTimePicker { get; set; }
        public TimeSpan overHoursPTimePicker { get; set; }
        public TimeSpan overTimeMTimePicker { get; set; }
        public string dateToday_Label { get; set; }
        public string hoursTotal_Label { get; set; }
        public string overTimeToday_Label { get; set; }
        public string overTimeTotal_Label { get; set; }
        public DateTime dateToday { get; set; }
        public TimeSpan hoursWBreak { get; set; }
        public TimeSpan overTiToday { get; set; }
        public TimeSpan overTimeTotal { get; set; }
        public AsyncCommand AddCommand { get; }


        public DateTimeCalculatePageViewModel()
        {
            WorkTimeComplete = new Command(WorkTimeComplete_Click);
            AddCommand = new AsyncCommand(Add);

        }

        async Task Add()
        {
            DateTime dateToday = DateTime.Now;

            //Hier werden die gesamten Stunden mit der Pause berechnet und ausgegeben
            TimeSpan totalhours = endTimePicker - startTimePicker;
            TimeSpan hoursWBreak = totalhours - breakTimePicker;
            //string totalHoursString = hoursWBreak.ToString();
            //hoursTotal_Label = totalHoursString;

            //Hier werde die tatsächlichen Stunden mit den eigentlichen stunden verrechnet
            double workHours = 7.7;
            TimeSpan hours = TimeSpan.FromHours(workHours);
            TimeSpan overTiToday = hoursWBreak - hours;


            if (overTiToday < hours)
            {
                TimeSpan overTiTodayString = overTiToday;
                //overTimeToday_Label = overTiTodayString;
            }
            else if (overTiToday > hours)
            {
                double overTimeTyZero = 0.0;
                TimeSpan zero = TimeSpan.FromMinutes(overTimeTyZero);
                //overTimeToday_Label = zero.ToString();
            }


            //Hier sind die gesamten Überstunden standtmäißg  auf  null
            int overTimeZero = 0;
            TimeSpan overTimeTotal = TimeSpan.FromHours(overTimeZero);
            // overTimeString = overTimeTotal.TotalHours.ToString();
            //overTimeTotal_Label = overTimeString;

            //Hie werden die Kompletten überstunden ausgerechnet

            if (hoursWBreak < hours)
            {

                TimeSpan overTimeTotal1 = overTimeTotal + overTiToday;
                string overTimeString1 = overTimeTotal1.TotalHours.ToString();
                // overTimeTotal_Label = overTimeString;

            }
            else if (hoursWBreak > hours)
            {

                TimeSpan overTimeTotal1 = overTimeTotal - overTiToday;
                string overTimeString1 = overTimeTotal1.TotalHours.ToString();
                // overTimeTotal_Label = overTimeString;
            }

            await DateTimePickService.AddDateTimePick(dateToday, hoursWBreak, overTiToday, overTimeTotal);
        }



        public void TimeCalculate()
        {

            dateToday_Label = DateToday.ToString("dd/MM/yyyy");

            //Hier werden die gesamten Stunden mit der Pause berechnet und ausgegeben
            TimeSpan totalhours = endTimePicker - startTimePicker;
            TimeSpan hoursWBreak = totalhours - breakTimePicker;
            //string totalHoursString = hoursWBreak.ToString();
            //hoursTotal_Label = totalHoursString;

            //Hier werde die tatsächlichen Stunden mit den eigentlichen stunden verrechnet
            double workHours = 7.7;
            TimeSpan hours = TimeSpan.FromHours(workHours);
            TimeSpan overTiToday = hoursWBreak - hours;


            if (overTiToday < hours)
            {
                TimeSpan overTiTodayString = overTiToday;
                //overTimeToday_Label = overTiTodayString;
            }
            else if (overTiToday > hours)
            {
                double overTimeTyZero = 0.0;
                TimeSpan zero = TimeSpan.FromMinutes(overTimeTyZero);
                //overTimeToday_Label = zero.ToString();
            }


            //Hier sind die gesamten Überstunden standtmäißg  auf  null
            int overTimeZero = 0;
            TimeSpan overTimeTotal = TimeSpan.FromHours(overTimeZero);
            // overTimeString = overTimeTotal.TotalHours.ToString();
            //overTimeTotal_Label = overTimeString;

            //Hie werden die Kompletten überstunden ausgerechnet

            if (hoursWBreak < hours)
            {

                TimeSpan overTimeTotal1 = overTimeTotal + overTiToday;
                string overTimeString1 = overTimeTotal1.TotalHours.ToString();
               // overTimeTotal_Label = overTimeString;

            }
            else if (hoursWBreak > hours)
            {

                TimeSpan overTimeTotal1 = overTimeTotal - overTiToday;
                string overTimeString1 = overTimeTotal1.TotalHours.ToString();
               // overTimeTotal_Label = overTimeString;
            }
        }

        public void WorkTimeComplete_Click()
        {
            TimeCalculate();
        }
    }
}
