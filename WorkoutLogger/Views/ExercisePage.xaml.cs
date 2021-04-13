using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogger.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExercisePage : ContentPage
    {
        public ExercisePage(object content, string pageType)
        {
            InitializeComponent();
            BindingContext = new ViewModel.ExerciseViewModel(content as ExerciseModel, pageType);
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            ((ListView)sender).SelectedItem = null; //Deselect Item
        }
    }
}