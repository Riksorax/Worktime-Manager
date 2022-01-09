using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worktime_Manager.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Windows.Input;
using Worktime_Manager.Services;
using Xamarin.Forms;
using Worktime_Manager.Models;

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
        public DateTime dateToday { get; set; }
        public TimeSpan hoursWBreak { get; set; }
        public TimeSpan overTiToday { get; set; }
        public TimeSpan overTimeTotal { get; set; }
        public AsyncCommand AddCommand { get; }


        public DateTimeCalculatePageViewModel()
        {
            AddCommand = new AsyncCommand(Add);

        }

        async Task Add()
        {
            WorkTimeCalculate();
            OverTimeCalculate();
            await Shell.Current.GoToAsync("..");
        }

        public async void WorkTimeCalculate()
        {
            DateTime dateToday = DateTime.Now;

            //Hier werden die gesamten Stunden mit der Pause berechnet und ausgegeben
            TimeSpan totalhours = endTimePicker - startTimePicker;
            TimeSpan hoursWBreak = totalhours - breakTimePicker;

            //Hier werde die tatsächlichen Stunden mit den eigentlichen stunden verrechnet
            double workHours = 7.7;
            TimeSpan hours = TimeSpan.FromHours(workHours);
            TimeSpan overTiToday = hoursWBreak - hours;

            //Hier sind die gesamten Überstunden standtmäißg  auf  null und alle daten werden bis dahin gespeichert

            TimeSpan overTimeTotalZero = TimeSpan.Zero;

            TimeSpan overTimeTotal = overTimeTotalZero + overTiToday;

            await DateTimePickService.AddDateTimePick(dateToday, hoursWBreak, overTiToday, overTimeTotal);                      

        }

        public async void OverTimeCalculate()
        {
            //Hie werden die Kompletten überstunden ausgerechnet

            if (overTimeTotal < TimeSpan.Zero)
            {
                TimeSpan newOverTiTotal = overTimeTotal + overTiToday;
                overTimeTotal = newOverTiTotal;
                await DateTimePickService.AddDateTimePick(dateToday, hoursWBreak, overTiToday, overTimeTotal);

            }
            else if (overTimeTotal > overTiToday)
            {
                TimeSpan newOverTiTotal = overTimeTotal + overTiToday;
                overTimeTotal = newOverTiTotal;
                await DateTimePickService.AddDateTimePick(dateToday, hoursWBreak, overTiToday, overTimeTotal);
            }
        }


    }
}
