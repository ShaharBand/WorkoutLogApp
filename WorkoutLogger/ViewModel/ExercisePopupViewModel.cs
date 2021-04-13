using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
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
    class ExercisePopupViewModel : BaseViewModel
    {
        #region Fields
        private string entry = "";

        private string errorLabel = "";
        private bool errorBool = false;

        private string type;
        #endregion

        #region Constructor
        public ExercisePopupViewModel(string pageType = "Chest")
        {
            type = pageType;
            ErrorBool = false;
            this.BackButtonCommand = new Command(this.BackButtonClicked);
            this.ConfirmCommand = new Command(this.ConfirmClicked);
        }
        #endregion

        #region Propreties
        public string Entry {
            get { return entry; }
            set { entry = value; this.NotifyPropertyChanged(); }
        }
     
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
        public Command ConfirmCommand { get; set; }
        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public Command BackButtonCommand { get; set; }
        #endregion

        #region Methods
        public async void ConfirmClicked()
        {
            try
            {
                if (String.IsNullOrEmpty(Entry))
                {
                    ErrorBool = true;
                    ErrorLabel = "Error: no information found!";
                    return;
                }
                if (!Regex.IsMatch(Entry, @"^[א-ת0-9a-zA-Z\s]+$"))
                {
                    ErrorBool = true;
                    ErrorLabel = "Error: the entry text isn't valid!";
                    return;
                }
                if (Entry.Length > 24)
                {
                    ErrorBool = true;
                    ErrorLabel = "Error: the entry text cannot be longer than 24 characters!";
                    return;
                }
                if (type == "Chest")
                {
                    if (MainViewModel.ChestList.Count >= 20)
                    {
                        ErrorBool = true;
                        ErrorLabel = "Error: each category cannot have more than 20 exercises!";
                        return;
                    }
                    foreach (ExerciseModel i in MainViewModel.ChestList)
                    {
                        if (i.Name.Equals(Entry))
                        {
                            ErrorBool = true;
                            ErrorLabel = "Error: the requested name already exists!";
                            return;
                        }
                    }
                }
                else if (type == "Back")
                {
                    if (MainViewModel.BackList.Count >= 20)
                    {
                        ErrorBool = true;
                        ErrorLabel = "Error: each category cannot have more than 20 exercises!";
                        return;
                    }
                    foreach (ExerciseModel i in MainViewModel.BackList)
                    {
                        if (i.Name.Equals(Entry))
                        {
                            ErrorBool = true;
                            ErrorLabel = "Error: the requested name already exists!";
                            return;
                        }
                    }
                }
                else if (type == "Legs")
                {
                    if (MainViewModel.LegsList.Count >= 20)
                    {
                        ErrorBool = true;
                        ErrorLabel = "Error: each category cannot have more than 20 exercises!";
                        return;
                    }
                    foreach (ExerciseModel i in MainViewModel.LegsList)
                    {
                        if (i.Name.Equals(Entry))
                        {
                            ErrorBool = true;
                            ErrorLabel = "Error: the requested name already exists!";
                            return;
                        }
                    }
                }
                else if (type == "Shoulders")
                {
                    if (MainViewModel.ShouldersList.Count >= 20)
                    {
                        ErrorBool = true;
                        ErrorLabel = "Error: each category cannot have more than 20 exercises!";
                        return;
                    }
                    foreach (ExerciseModel i in MainViewModel.ShouldersList)
                    {
                        if (i.Name.Equals(Entry))
                        {
                            ErrorBool = true;
                            ErrorLabel = "Error: the requested name already exists!";
                            return;
                        }
                    }
                }
                else if (type == "Arms")
                {
                    if (MainViewModel.ArmsList.Count >= 20)
                    {
                        ErrorBool = true;
                        ErrorLabel = "Error: each category cannot have more than 20 exercises!";
                        return;
                    }
                    foreach (ExerciseModel i in MainViewModel.ArmsList)
                    {
                        if (i.Name.Equals(Entry))
                        {
                            ErrorBool = true;
                            ErrorLabel = "Error: the requested name already exists!";
                            return;
                        }
                    }
                }
                else if (type == "Abs")
                {
                    if (MainViewModel.AbsList.Count >= 20)
                    {
                        ErrorBool = true;
                        ErrorLabel = "Error: each category cannot have more than 20 exercises!";
                        return;
                    }
                    foreach (ExerciseModel i in MainViewModel.AbsList)
                    {
                        if (i.Name.Equals(Entry))
                        {
                            ErrorBool = true;
                            ErrorLabel = "Error: the requested name already exists!";
                            return;
                        }
                    }
                }

                MainViewModel.AddExercise(type, Entry);
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
        #endregion
    }
}
