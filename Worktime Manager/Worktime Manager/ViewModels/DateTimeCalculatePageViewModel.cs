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
using System.Linq;

namespace Worktime_Manager.ViewModels
{
    public partial class DateTimeCalculatePageViewModel : ViewModelBase
    {
        public ICommand WorkTimeComplete { get; }
        public DateTime dateTodayPicker { get; set; }
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

        public ObservableRangeCollection<DateTimePick> DateTimePick { get; set; }


        public DateTimeCalculatePageViewModel()
        {
            AddCommand = new AsyncCommand(Add);

            DateTimePick = new ObservableRangeCollection<DateTimePick>();
        }

        async Task Add()
        {
            WorkTimeCalculate();
            OverTimeCalculate();
            await Shell.Current.GoToAsync("..");
        }

        public async void WorkTimeCalculate()
        {
            //Hier greife ich das eingestellte Datum vom DatePicker ab und lasse es weiter unten speichern
            DateTime dateToday = dateTodayPicker; ;

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

            /*
            //Hie werden die Kompletten überstunden ausgerechnet. Es sollen ansich die wertev ich brauhche geladen werden und dann wieder gespeichert werden
            IList<DateTimePick> newOverTime = (IList<DateTimePick>)await DateTimePickService.GetDateTimePick();
            foreach(var item in newOverTime)
            {
                if (item.OverTime_Today < item.Hours_Today)
                {
                    //this.overTimeTotal.Add(item.OverTime_Total);
                    overTimeTotal = item.OverTime_Total + item.OverTime_Today;;
                }
                else if (item.OverTime_Today > item.Hours_Today)
                {
                    overTimeTotal = item.OverTime_Total - item.OverTime_Today;
                }
                await DateTimePickService.AddDateTimePick(dateToday, hoursWBreak, overTiToday, overTimeTotal);
            }*/
        }

        //Das ist ansich das gleiche wie oben nur als eine einzelne Funktion
        public async void OverTimeCalculate()
        {
            var newHours = DateTimePick.;
            TimeSpan hoursToday = newHours.Hours_Today;
            var newOverTimeToday = DateTimePick.ElementAt(2);
            TimeSpan overTiToday  = newOverTimeToday.OverTime_Today;
            var newOverTimeTotal = DateTimePick.ElementAt(3);
            TimeSpan overTiTotal = newOverTimeTotal.OverTime_Total;

            //Hie werden die Kompletten überstunden ausgerechnet
            if (overTiToday < hoursToday)
            {
                //this.overTimeTotal.Add(item.OverTime_Total);
                TimeSpan overTimeTotal = overTiToday + overTiTotal;
                await DateTimePickService.AddDateTimePick(dateToday, hoursWBreak, overTiToday, overTimeTotal);
            }
            else if(overTiToday > hoursToday)
            {
                TimeSpan overTimeTotal = overTiTotal - overTiToday;
                await DateTimePickService.AddDateTimePick(dateToday, hoursWBreak, overTiToday, overTimeTotal);
            }

            
        }


    }
}
