using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using WorkoutLogger.Model;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WorkoutLogger.ViewModel
{
    /// <summary>
    /// ViewModel for the main navigation page 'MainPage'.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    class MainViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance for the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.ChestCommand = new Command(ChestClicked);
            this.BackCommand = new Command(BackClicked);
            this.LegCommand = new Command(LegClicked);
            this.ShoulderCommand = new Command(ShoulderClicked);
            this.ArmsCommand = new Command(ArmsClicked);
            this.AbsCommand = new Command(AbsClicked);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a list of values related to chest data List(Exercise Name, List(Log Data)).
        /// </summary>
        [DataMember(Name = "Chest")]
        public static List<ExerciseModel> ChestList { get; set; }

        /// <summary>
        /// Gets or sets a list of values related to back data List(Exercise Name, List(Log Data)).
        /// </summary>
        [DataMember(Name = "Back")]
        public static List<ExerciseModel> BackList { get; set; }

        /// <summary>
        /// Gets or sets a list of values related to legs data List(Exercise Name, List(Log Data)).
        /// </summary>
        [DataMember(Name = "Legs")]
        public static List<ExerciseModel> LegsList { get; set; }

        /// <summary>
        /// Gets or sets a list of values related to shoulders data List(Exercise Name, List(Log Data)).
        /// </summary>
        [DataMember(Name = "Shoulders")]
        public static List<ExerciseModel> ShouldersList { get; set; }

        /// <summary>
        /// Gets or sets a list of values related to arms data List(Exercise Name, List(Log Data)).
        /// </summary>
        [DataMember(Name = "Arms")]
        public static List<ExerciseModel> ArmsList { get; set; }

        /// <summary>
        /// Gets or sets a list of values related to abs data List(Exercise Name, List(Log Data)).
        /// </summary>
        [DataMember(Name = "Abs")]
        public static List<ExerciseModel> AbsList { get; set; }
        #endregion

        #region Command
        public Command ChestCommand
        {
            get; set;
        }
        public Command BackCommand
        {
            get; set;
        }
        public Command LegCommand
        {
            get; set;
        }
        public Command ShoulderCommand
        {
            get; set;
        }
        public Command ArmsCommand
        {
            get; set;
        }
        public Command AbsCommand
        {
            get; set;
        }
        #endregion

        #region Methods
        public void ChestClicked() => Application.Current.MainPage.Navigation.PushAsync(new WorkoutLogger.Views.ExerciseListPage("Chest"));
        public void BackClicked() => Application.Current.MainPage.Navigation.PushAsync(new WorkoutLogger.Views.ExerciseListPage("Back"));
        public void LegClicked() => Application.Current.MainPage.Navigation.PushAsync(new WorkoutLogger.Views.ExerciseListPage("Legs"));
        public void ShoulderClicked() => Application.Current.MainPage.Navigation.PushAsync(new WorkoutLogger.Views.ExerciseListPage("Shoulders"));
        public void ArmsClicked() => Application.Current.MainPage.Navigation.PushAsync(new WorkoutLogger.Views.ExerciseListPage("Arms"));
        public void AbsClicked() => Application.Current.MainPage.Navigation.PushAsync(new WorkoutLogger.Views.ExerciseListPage("Abs"));

        public static void AddLog(string type, LogModel log, string exerciseName)
        {
            ExerciseModel exercise = null;
            if (type == "Chest")
            {
                foreach (ExerciseModel i in ChestList)
                {
                    if (i.Name.Equals(exerciseName))
                    {
                        exercise = i;
                        i.Data.Sort(SortByDate);
                        if (i.Data[0].Date.Equals(log.Date)) i.Data.RemoveAt(0);

                        log.Trend = 0;
                        if (i.Data.Count > 1)
                            log.Trend = (log.Weights * log.Reps * log.Sets - i.Data[0].Weights * i.Data[0].Reps * i.Data[0].Sets) /
                            (log.Weights * log.Reps * log.Sets);

                        i.Data.Add(log);
                        if (i.Data.Count >= 30)
                        {
                            i.Data.RemoveAt(30);
                        }
                    }
                }
            }
            else if (type == "Back")
            {
                foreach (ExerciseModel i in BackList)
                {
                    if (i.Name.Equals(exerciseName))
                    {
                        exercise = i;
                        i.Data.Sort(SortByDate);
                        if (i.Data[0].Date.Equals(log.Date)) i.Data.RemoveAt(0);

                        log.Trend = 0;
                        if (i.Data.Count > 1)
                            log.Trend = (log.Weights * log.Reps * log.Sets - i.Data[0].Weights * i.Data[0].Reps * i.Data[0].Sets) /
                            (log.Weights * log.Reps * log.Sets);

                        i.Data.Add(log);
                        if (i.Data.Count >= 30)
                        {
                            i.Data.RemoveAt(30);
                        }
                    }
                }
            }
            else if (type == "Legs")
            {
                foreach (ExerciseModel i in LegsList)
                {
                    if (i.Name.Equals(exerciseName))
                    {
                        exercise = i;
                        i.Data.Sort(SortByDate);
                        if (i.Data[0].Date.Equals(log.Date)) i.Data.RemoveAt(0);

                        log.Trend = 0;
                        if (i.Data.Count > 1)
                            log.Trend = (log.Weights * log.Reps * log.Sets - i.Data[0].Weights * i.Data[0].Reps * i.Data[0].Sets) /
                            (log.Weights * log.Reps * log.Sets);

                        i.Data.Add(log);
                        if (i.Data.Count >= 30)
                        {
                            i.Data.RemoveAt(30);
                        }
                    }
                }
            }
            else if (type == "Shoulders")
            {
                foreach (ExerciseModel i in ShouldersList)
                {
                    if (i.Name.Equals(exerciseName))
                    {
                        exercise = i;
                        i.Data.Sort(SortByDate);
                        if (i.Data[0].Date.Equals(log.Date)) i.Data.RemoveAt(0);

                        log.Trend = 0;
                        if (i.Data.Count > 1)
                            log.Trend = (log.Weights * log.Reps * log.Sets - i.Data[0].Weights * i.Data[0].Reps * i.Data[0].Sets) /
                            (log.Weights * log.Reps * log.Sets);

                        i.Data.Add(log);
                        if (i.Data.Count >= 30)
                        {
                            i.Data.RemoveAt(30);
                        }
                    }
                }
            }
            else if (type == "Arms")
            {
                foreach (ExerciseModel i in ArmsList)
                {
                    if (i.Name.Equals(exerciseName))
                    {
                        exercise = i;
                        i.Data.Sort(SortByDate);
                        if (i.Data[0].Date.Equals(log.Date)) i.Data.RemoveAt(0);

                        log.Trend = 0;
                        if (i.Data.Count > 1)
                            log.Trend = (log.Weights * log.Reps * log.Sets - i.Data[0].Weights * i.Data[0].Reps * i.Data[0].Sets) /
                            (log.Weights * log.Reps * log.Sets);

                        i.Data.Add(log);
                        if (i.Data.Count >= 30)
                        {
                            i.Data.RemoveAt(30);
                        }
                    }
                }
            }
            else if (type == "Abs")
            {
                foreach (ExerciseModel i in AbsList)
                {
                    if (i.Name.Equals(exerciseName))
                    {
                        exercise = i;
                        i.Data.Sort(SortByDate);
                        if (i.Data[0].Date.Equals(log.Date)) i.Data.RemoveAt(0);

                        log.Trend = 0;
                        if (i.Data.Count > 1)
                            log.Trend = (log.Weights * log.Reps * log.Sets - i.Data[0].Weights * i.Data[0].Reps * i.Data[0].Sets) /
                            (log.Weights * log.Reps * log.Sets);

                        i.Data.Add(log);
                        if (i.Data.Count >= 30)
                        {
                            i.Data.RemoveAt(30);
                        }
                    }
                }
            }

            App.Current.MainPage.Navigation.PopToRootAsync();
            Application.Current.MainPage.Navigation.PushAsync(new WorkoutLogger.Views.ExerciseListPage(type));
            Application.Current.MainPage.Navigation.PushAsync(new WorkoutLogger.Views.ExercisePage(exercise, type));
            DataService.UserDataService.SaveData();
        }

        public static void AddExercise(string type, string entry)
        {
            ExerciseModel newExercise = new ExerciseModel();
            newExercise.Name = entry;

            List<LogModel> newData = new List<LogModel>();
            LogModel data1 = new LogModel();
            data1.Reps = 0;
            data1.Sets = 0;
            data1.Weights = 0;
            data1.Trend = 0;
            data1.Date = DateTime.Now.ToString("dd/MM/yy");
            newData.Add(data1);

            newExercise.Data = newData;

            if (type == "Chest") ChestList.Add(newExercise);
            else if (type == "Back") BackList.Add(newExercise);
            else if (type == "Legs") LegsList.Add(newExercise);
            else if (type == "Shoulders") ShouldersList.Add(newExercise);
            else if (type == "Arms") ArmsList.Add(newExercise);
            else if (type == "Abs") AbsList.Add(newExercise);

            App.Current.MainPage.Navigation.PopToRootAsync();
            Application.Current.MainPage.Navigation.PushAsync(new WorkoutLogger.Views.ExerciseListPage(type));
            DataService.UserDataService.SaveData();
        }

        public static int SortByDate(LogModel a, LogModel b)
        {
            DateTime a1 = DateTime.ParseExact(a.Date, "dd/MM/yy", CultureInfo.InvariantCulture);
            DateTime b1 = DateTime.ParseExact(b.Date, "dd/MM/yy", CultureInfo.InvariantCulture);
            return a1.CompareTo(b1) * -1;
        }

        public static void RemoveExercise(string type, ExerciseModel exercise)
        {
            if (type == "Chest") ChestList.Remove(exercise);
            else if (type == "Back") BackList.Remove(exercise);
            else if (type == "Legs") LegsList.Remove(exercise);
            else if (type == "Shoulders") ShouldersList.Remove(exercise);
            else if (type == "Arms") ArmsList.Remove(exercise);
            else if (type == "Abs") AbsList.Remove(exercise);

            App.Current.MainPage.Navigation.PopToRootAsync();
            Application.Current.MainPage.Navigation.PushAsync(new WorkoutLogger.Views.ExerciseListPage(type));
            DataService.UserDataService.SaveData();
        }

        #endregion
    }
}
