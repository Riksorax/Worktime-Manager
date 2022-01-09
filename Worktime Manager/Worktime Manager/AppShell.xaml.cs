using Xamarin.Forms;
using Worktime_Manager.Views;

namespace Worktime_Manager
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DateTimeCalculate), typeof(DateTimeCalculate)); 
        }

    }
}
