using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogger.DataService;
using Xamarin.Forms;

namespace WorkoutLogger
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = UserDataService.Instance.MainViewModel;
        }
    }
}
