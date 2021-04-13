using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WorkoutLogger.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseListPage : ContentPage
    {
        private string type;
        public ExerciseListPage(string pageType = "Chest")
        {
            InitializeComponent();
            BindingContext = new ViewModel.ExerciseListViewModel(pageType);
            type = pageType;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            ((ListView)sender).SelectedItem = null; //Deselect Item

            var content = e.Item as ExerciseModel;
            await Application.Current.MainPage.Navigation.PushAsync(new Views.ExercisePage(content, type)); //pass content if you want to pass the clicked item object to another page
        }
    }
}
