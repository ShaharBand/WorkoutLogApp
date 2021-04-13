using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkoutLogger.Model;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WorkoutLogger.ViewModel
{
    /// <summary>
    /// ViewModel for the add log popup.
    /// </summary>
    [Preserve(AllMembers = true)]
    class RemoveExercisePopupViewModel : BaseViewModel
    {
        #region Fields
        private string errorLabel = "";
        private bool errorBool = false;

        private string type;

        #endregion

        #region Constructor
        public RemoveExercisePopupViewModel(string pageType = "Chest")
        {
            ObservableCollection<ExerciseModel> dataToLoad = LoadList(pageType);
            ExerciseList = dataToLoad;

            type = pageType;
            ErrorBool = false;
            this.BackButtonCommand = new Command(this.BackButtonClicked);
            this.RemoveCommand = new Command(this.RemoveClicked);
        }
        #endregion

        #region Propreties
        /// <summary>
        /// Gets or sets a collection of values to be displayed in the exercise list page.
        /// </summary>
        public ObservableCollection<ExerciseModel> ExerciseList { get; set; }

        public ExerciseModel selectedItem { get; set; }
        public string ErrorLabel
        {
            get { return errorLabel; }
            set { errorLabel = value; this.NotifyPropertyChanged(); }
        }
        public bool ErrorBool
        {
            get { return errorBool; }
            set { errorBool = value; this.NotifyPropertyChanged(); }
        }
       
        #endregion

        #region Commands
        /// <summary>
        /// Gets or sets the command is executed when the confirm button is clicked.
        /// </summary>
        public Command RemoveCommand { get; set; }
        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods
        public async void RemoveClicked()
        {
            try
            {
                if(selectedItem == null)
                {
                    ErrorBool = true;
                    ErrorLabel = "Error: you need to select something!";
                    return;
                }

                MainViewModel.RemoveExercise(type, selectedItem);
                await PopupNavigation.Instance.PopAsync(true);
                ErrorBool = false;
            }
            catch
            {
                ErrorBool = true;
                ErrorLabel = "Error: action cannot be completed, something went wrong!";
                return;
            }
        }
        public async void BackButtonClicked()
        {
            ErrorBool = false;
            await PopupNavigation.Instance.PopAsync(true);
        }

        private ObservableCollection<ExerciseModel> LoadList(string pageType)
        {
            ObservableCollection<ExerciseModel> dataToLoad = new ObservableCollection<ExerciseModel>();
            if (pageType == "Chest")
                foreach (ExerciseModel i in MainViewModel.ChestList)
                    dataToLoad.Add(i);
            else if (pageType == "Back")
                foreach (ExerciseModel i in MainViewModel.BackList)
                    dataToLoad.Add(i);
            else if (pageType == "Legs")
                foreach (ExerciseModel i in MainViewModel.LegsList)
                    dataToLoad.Add(i);
            else if (pageType == "Shoulders")
                foreach (ExerciseModel i in MainViewModel.ShouldersList)
                    dataToLoad.Add(i);
            else if (pageType == "Arms")
                foreach (ExerciseModel i in MainViewModel.ArmsList)
                    dataToLoad.Add(i);
            else if (pageType == "Abs")
                foreach (ExerciseModel i in MainViewModel.AbsList)
                    dataToLoad.Add(i);

            /*foreach (ExerciseModel i in dataToLoad)
                i.Data.Sort(SortByDate);*/

            Sort(dataToLoad, SortByName);
            return dataToLoad;
        }
        /*int SortByDate(LogModel a, LogModel b)
        {
            DateTime a1 = DateTime.ParseExact(a.Date, "dd/MM/yy", CultureInfo.InvariantCulture);
            DateTime b1 = DateTime.ParseExact(b.Date, "dd/MM/yy", CultureInfo.InvariantCulture);
            return a1.CompareTo(b1) * -1;
        }*/
        int SortByName(ExerciseModel a, ExerciseModel b)
        {
            string a1 = a.Name;
            string b1 = b.Name;
            return a1.CompareTo(b1);
        }
        private void Sort<T>(ObservableCollection<T> collection, Comparison<T> comparison)
        {
            var sortableList = new List<T>(collection);
            sortableList.Sort(comparison);

            for (int i = 0; i < sortableList.Count; i++)
                collection.Move(collection.IndexOf(sortableList[i]), i);
        }
        #endregion
    }
}
