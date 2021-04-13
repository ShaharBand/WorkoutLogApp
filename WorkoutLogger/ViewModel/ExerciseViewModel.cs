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
using WorkoutLogger.Views.Popups;
using Rg.Plugins.Popup.Services;

namespace WorkoutLogger.ViewModel
{
    /// <summary>
    /// ViewModel for the exercise list page
    /// </summary> 
    [Preserve(AllMembers = true)]
    class ExerciseViewModel : BaseViewModel
    {
        #region Fields
        private Chart chart;
        private string title;

        private LogPopup _logPage;
        #endregion

        #region Constructor
        public ExerciseViewModel(ExerciseModel content = null, string pageType = "Chest")
        {
            Title = content.Name;
            // order it to be up to date using comprator then update
            ExerciseModel pageContent = content;
            pageContent.Data.Sort(SortByDate);

            ObservableCollection<LogModel> loglist = new ObservableCollection<LogModel>();
            foreach (LogModel i in pageContent.Data)
                loglist.Add(i);

            LogList = loglist;

            List<Entry> entries = LoadGraph();
            Chart = new LineChart()
            {
                Entries = entries,
                MinValue = 0,
                LineSize = 6,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                LineMode = LineMode.Straight,
                PointMode = PointMode.Square,
                //LineMode = LineMode.Spline,
                LabelTextSize = 36,
            };

            _popup = PopupNavigation.Instance;
            _logPage = new LogPopup(pageType, content.Name);
            this.BackButtonCommand = new Command(this.BackButtonClicked);
            this.AddLogCommand = new Command(this.AddLogClicked);
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
        /// Gets or sets the property that bounds with the page title.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; this.NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets a collection of values to be displayed in the exercise list page.
        /// </summary>
        public ObservableCollection<LogModel> LogList { get; set; }
        #endregion

        #region Commands
        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public Command BackButtonCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the add log button is clicked.
        /// </summary>
        public Command AddLogCommand { get; set; }
        #endregion

        #region Methods
        int SortByDate(LogModel a, LogModel b)
        {
            DateTime a1 = DateTime.ParseExact(a.Date, "dd/MM/yy", CultureInfo.InvariantCulture);
            DateTime b1 = DateTime.ParseExact(b.Date, "dd/MM/yy", CultureInfo.InvariantCulture);
            return a1.CompareTo(b1)*-1;
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
            foreach (LogModel i in LogList)
            {
                int volume = (int)(i.Reps * i.Sets * i.Weights);
                Entry e = new Entry(volume)
                {
                    ValueLabel = (volume).ToString(),
                    Label = i.Date,
                    Color = colorList[index],
                };
                index++;
                entries.Add(e);
                if (index > 10) break;
            }
            
            return entries;
        }
        /// <summary>
        /// Invoked when the back button clicked
        /// </summary>
        private void BackButtonClicked(object obj) => Application.Current.MainPage.Navigation.PopAsync();

        /// <summary>
        /// Invoked when the add log button clicked
        /// </summary>
        private async void AddLogClicked(object obj) => await _popup.PushAsync(_logPage);

        #endregion
    }
}
