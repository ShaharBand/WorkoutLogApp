using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;
using Xamarin.Forms.Internals;
using System.Collections.ObjectModel;
using WorkoutLogger.Model;
using System.Globalization;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using WorkoutLogger.Views.Popups;

namespace WorkoutLogger.ViewModel
{
    /// <summary>
    /// ViewModel for the exercise list page
    /// </summary> 
    [Preserve(AllMembers = true)]
    class ExerciseListViewModel : BaseViewModel
    {
        #region Fields
        private Chart chart;
        private string type;


        private ExercisePopup _exercisePage;
        private RemoveExercisePopup _removeExercisePage;

        #endregion

        #region Constructor
        public ExerciseListViewModel(string pageType = "Chest")
        {
            Type = pageType;
            // order it to be up to date using comprator then update
            ObservableCollection<ExerciseModel> dataToLoad = LoadList(pageType);

            ExerciseList = dataToLoad;

            List<Entry> entries = LoadGraph();
            Chart = new RadarChart()
            {
                Entries = entries,
                MinValue = 0,
                LineSize = 6,
                LabelTextSize = 36,
            };

            _popup = PopupNavigation.Instance;
            _exercisePage = new ExercisePopup(type);
            _removeExercisePage = new RemoveExercisePopup(type);

            this.AddExerciseCommand = new Command(this.AddExerciseClicked);
            this.RemoveExerciseCommand = new Command(this.RemoveExerciseClicked);
            this.BackButtonCommand = new Command(this.BackButtonClicked);
        }
        #endregion

        #region Property
        private IPopupNavigation _popup { get; set; }

        /// <summary>
        /// Gets or sets the property that bounds with the page chart.
        /// </summary>
        public Chart Chart
        {
            get { return chart; }
            set { chart = value; this.NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the property that bounds with the page type.
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; this.NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets a collection of values to be displayed in the exercise list page.
        /// </summary>
        public ObservableCollection<ExerciseModel> ExerciseList { get; set; }
        #endregion

        #region Commands
        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public Command BackButtonCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the add exercise button is clicked.
        /// </summary>
        public Command AddExerciseCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the remove exercise button is clicked.
        /// </summary>
        public Command RemoveExerciseCommand { get; set; }
        #endregion

        #region Methods
        int SortByDate(LogModel a, LogModel b)
        {
            DateTime a1 = DateTime.ParseExact(a.Date, "dd/MM/yy", CultureInfo.InvariantCulture);
            DateTime b1 = DateTime.ParseExact(b.Date, "dd/MM/yy", CultureInfo.InvariantCulture);
            return a1.CompareTo(b1)*-1;
        }
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
        private List<Entry> LoadGraph()
        {
            List<SKColor> colorList = new List<SKColor>();
            colorList.Add(SKColor.Parse("#FF1943"));
            colorList.Add(SKColor.Parse("#00BFFF"));
            colorList.Add(SKColor.Parse("#00CED1"));
            colorList.Add(SKColor.Parse("#FF5733"));
            colorList.Add(SKColor.Parse("#FFC300"));
            colorList.Add(SKColor.Parse("#75FA33"));
            colorList.Add(SKColor.Parse("#9033FF"));
            colorList.Add(SKColor.Parse("#3399FF"));
            colorList.Add(SKColor.Parse("#FF33E1")); 
            colorList.Add(SKColor.Parse("#33FF8D"));

            List<Entry> entries = new List<Entry>();

            int index = 0;
            foreach (ExerciseModel i in ExerciseList)
            {
                Entry e = new Entry(i.Data.Count())
                {
                    ValueLabel = (i.Data.Count()).ToString(),
                    Label = i.Name,
                    Color = colorList[index],
                };
                index++;
                entries.Add(e);
                if (index > 10) break;
            }
            
            return entries;
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

            foreach (ExerciseModel i in dataToLoad)
                i.Data.Sort(SortByDate);

            Sort(dataToLoad, SortByName);
            return dataToLoad;
        }
        /// <summary>
        /// Invoked when the back button clicked
        /// </summary>
        private void BackButtonClicked(object obj) => Application.Current.MainPage.Navigation.PopAsync();

        /// <summary>
        /// Invoked when the add log button clicked
        /// </summary>
        private async void AddExerciseClicked(object obj) => await _popup.PushAsync(_exercisePage);

        private async void RemoveExerciseClicked(object obj) => await _popup.PushAsync(_removeExercisePage);
        #endregion
    }
}
