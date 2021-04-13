using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
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
    class LogPopupViewModel : BaseViewModel
    {
        #region Fields
        private double entryReps1 = 0;
        private double entryReps2 = 0;
        private double entryReps3 = 0;
        private double entryReps4 = 0;
        private double entryReps5 = 0;
        private double entryWeight1 = 0;
        private double entryWeight2 = 0;
        private double entryWeight3 = 0;
        private double entryWeight4 = 0;
        private double entryWeight5 = 0;

        private string errorLabel = "";
        private bool errorBool = false;

        private string type;
        private string exerciseName;
        #endregion
        #region Constructor
        public LogPopupViewModel(string pageType = "Chest", string eName = "")
        {
            type = pageType;
            exerciseName = eName;
            LogDate = DateTime.Now.ToString("dd/MM/yy");
            ErrorBool = false;
            this.BackButtonCommand = new Command(this.BackButtonClicked);
            this.ConfirmCommand = new Command(this.ConfirmClicked);
        }
        #endregion
        #region Propreties
        public double EntryReps1 {
            get { return entryReps1; }
            set { entryReps1 = value; this.NotifyPropertyChanged(); }
        }
        public double EntryReps2
        {
            get { return entryReps2; }
            set { entryReps2 = value; this.NotifyPropertyChanged(); }
        }
        public double EntryReps3
        {
            get { return entryReps3; }
            set { entryReps3 = value; this.NotifyPropertyChanged(); }
        }
        public double EntryReps4
        {
            get { return entryReps4; }
            set { entryReps4 = value; this.NotifyPropertyChanged(); }
        }
        public double EntryReps5
        {
            get { return entryReps5; }
            set { entryReps5 = value; this.NotifyPropertyChanged(); }
        }

        public double EntryWeight1 
        {
            get { return entryWeight1; }
            set { entryWeight1 = value; this.NotifyPropertyChanged(); }
        }
        public double EntryWeight2
        {
            get { return entryWeight2; }
            set { entryWeight2 = value; this.NotifyPropertyChanged(); }
        }
        public double EntryWeight3
        {
            get { return entryWeight3; }
            set { entryWeight3 = value; this.NotifyPropertyChanged(); }
        }
        public double EntryWeight4
        {
            get { return entryWeight4; }
            set { entryWeight4 = value; this.NotifyPropertyChanged(); }
        }
        public double EntryWeight5
        {
            get { return entryWeight5; }
            set { entryWeight5 = value; this.NotifyPropertyChanged(); }
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
        
        public string LogDate { get; set; }
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
                double amount_of_sets = 0;
                double sum_of_reps = EntryReps1 + EntryReps2 + EntryReps3 + EntryReps4 + EntryReps5;
                double sum_of_weights = EntryWeight1 + EntryWeight2 + EntryWeight3 + EntryWeight4 + EntryWeight5;

                if (EntryReps1 != 0 && EntryWeight1 != 0) amount_of_sets++;
                if (EntryReps2 != 0 && EntryWeight1 != 0) amount_of_sets++;
                if (EntryReps3 != 0 && EntryWeight1 != 0) amount_of_sets++;
                if (EntryReps4 != 0 && EntryWeight1 != 0) amount_of_sets++;
                if (EntryReps5 != 0 && EntryWeight1 != 0) amount_of_sets++;
                if (amount_of_sets == 0)
                {
                    ErrorBool = true;
                    ErrorLabel = "Error: no information found!";
                    return;
                }
                if (EntryReps1 > 99 || EntryReps2 > 99 || EntryReps3 > 99 || EntryReps4 > 99 || EntryReps5 > 99)
                {
                    ErrorBool = true;
                    ErrorLabel = "Error: you cannot put more than 99 reps per set!";
                    return;
                }
                if (EntryReps1 < 0 || EntryReps2 < 0 || EntryReps3 < 0 || EntryReps4 < 0 || EntryReps5 < 0)
                {
                    ErrorBool = true;
                    ErrorLabel = "Error: you cannot put negative amount of reps!";
                    return;
                }
                if (EntryWeight1 > 999 || EntryWeight2 > 999 || EntryWeight3 > 999 || EntryWeight4 > 999 || EntryWeight5 > 999)
                {
                    ErrorBool = true;
                    ErrorLabel = "Error: you cannot put more than 999kg per set!";
                    return;
                }
                if (EntryWeight1 < 0 || EntryWeight2 < 0 || EntryWeight3 < 0 || EntryWeight4 < 0 || EntryWeight5 < 0)
                {
                    ErrorBool = true;
                    ErrorLabel = "Error: you cannot put negative weights!";
                    return;
                }
                double average_reps = sum_of_reps / amount_of_sets;
                double average_weight = sum_of_weights / amount_of_sets;

                LogModel newLog = new LogModel();
                newLog.Date = DateTime.Now.ToString("dd/MM/yy");
                newLog.Weights = (float)average_weight;
                newLog.Reps = (float)average_reps;
                newLog.Sets = (float)amount_of_sets;

                MainViewModel.AddLog(type,newLog, exerciseName);
                await PopupNavigation.Instance.PopAsync(true);
                ResetEntries();
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
            ResetEntries();
            await PopupNavigation.Instance.PopAsync(true);
        }
        private void ResetEntries()
        {
            EntryReps1 = 0;
            EntryReps2 = 0;
            EntryReps3 = 0;
            EntryReps4 = 0;
            EntryReps5 = 0;
            EntryWeight1 = 0;
            EntryWeight2 = 0;
            EntryWeight3 = 0;
            EntryWeight4 = 0;
            EntryWeight5 = 0;
            ErrorBool = false;
        }
        #endregion
    }
}
