using Worktime_Manager.Models;
using Worktime_Manager.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Worktime_Manager.Services;

namespace Worktime_Manager.ViewModels
{
    public partial class DateTCPViewModel : ViewModelBase
    {      

        public ObservableRangeCollection<DateTimePick> DateTimePick { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<DateTimePick> RemoveCommand { get; }


        public DateTCPViewModel()
        {
            Title = "Überstunden berechnen";

            
            DateTimePick = new ObservableRangeCollection<DateTimePick>();

            
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<DateTimePick>(Remove);
        }     
       

        async Task Add()
        {
            var route = $"{nameof(DateTimeCalculate)}";
            await Shell.Current.GoToAsync(route);
            await Refresh();
        }

        async Task Remove(DateTimePick dateTimePick)
        {
            await DateTimePickService.RemoveDateTimePick(dateTimePick.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            DateTimePick.Clear();

            var dateTimePick = await DateTimePickService.GetDateTimePick();

            DateTimePick.AddRange(dateTimePick);

            IsBusy = false;
        }
    }

}
