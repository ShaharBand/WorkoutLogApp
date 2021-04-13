using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WorkoutLogger.Model;
using WorkoutLogger.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WorkoutLogger.DataService
{
    /// <summary>
    /// Data service to load the data from json file.
    /// </summary>
    [Preserve(AllMembers = true)]
    class UserDataService
    {
        #region fields
        private static UserDataService instance;
        private MainViewModel mainViewModel;
        #endregion

        #region Constructor
        private UserDataService()
        {
        }
        #endregion

        #region Properties
        public static UserDataService Instance => instance ?? (instance = new UserDataService());

        /// <summary>
        /// Gets or sets the data in the main page view model for later use.
        /// </summary>
        public MainViewModel MainViewModel =>
            this.mainViewModel ?? (this.mainViewModel = PopulateData<MainViewModel>("ExercisesData.json"));

        #endregion

        #region Methods
        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            T obj;
            //Preferences.Clear();
            try
            {
                var data = Preferences.Get("UserData", null);
                if (String.IsNullOrEmpty(data)) throw new Exception();
                obj = JsonConvert.DeserializeObject<T>(data);
            }
            catch
            {
                var file = "WorkoutLogger.Data." + fileName;
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(T)).Assembly;
                Stream stream = assembly.GetManifestResourceStream(file);
                string text;
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                    obj = JsonConvert.DeserializeObject<T>(text);
                    reader.Dispose();
                }
                stream.Dispose();
            }
            return obj;
        }
        public static void SaveData()
        {
            JObject userDataArray = new JObject(
                new JProperty("Chest", JSONChestData()),
                new JProperty("Back", JSONBackData()),
                new JProperty("Legs", JSONLegsData()),
                new JProperty("Shoulders", JSONShouldersData()),
                new JProperty("Arms", JSONArmsData()),
                new JProperty("Abs", JSONAbsData()));

            Preferences.Set("UserData", JsonConvert.SerializeObject(userDataArray));
        }
        public static JArray JSONChestData()
        {
            JArray workoutArray = new JArray();
            foreach(ExerciseModel em in MainViewModel.ChestList)
            {
                JArray dataArray = new JArray();

                foreach (LogModel j in em.Data)
                {
                    JObject log = new JObject(new JProperty("Date", j.Date),
                                              new JProperty("Reps", j.Reps),
                                              new JProperty("Sets", j.Sets),
                                              new JProperty("Weight", j.Weights),
                                              new JProperty("Trend", j.Trend));
                    dataArray.Add(log);
                }
                JObject exercise = new JObject(new JProperty("Name", em.Name), new JProperty("Data", dataArray));
                workoutArray.Add(exercise);
            }
            return workoutArray;
        }
        public static JArray JSONBackData()
        {
            JArray workoutArray = new JArray();
            foreach (ExerciseModel i in MainViewModel.BackList)
            {
                JArray dataArray = new JArray();
                foreach (LogModel j in i.Data)
                {
                    JObject log = new JObject(new JProperty("Date", j.Date),
                                              new JProperty("Reps", j.Reps),
                                              new JProperty("Sets", j.Sets),
                                              new JProperty("Weight", j.Weights),
                                              new JProperty("Trend", j.Trend));
                    dataArray.Add(log);
                }
                JObject exercise = new JObject(new JProperty("Name", i.Name), new JProperty("Data", dataArray));
                workoutArray.Add(exercise);
            }
            return workoutArray;
        }
        public static JArray JSONLegsData()
        {
            JArray workoutArray = new JArray();
            foreach (ExerciseModel i in MainViewModel.LegsList)
            {
                JArray dataArray = new JArray();
                foreach (LogModel j in i.Data)
                {
                    JObject log = new JObject(new JProperty("Date", j.Date),
                                              new JProperty("Reps", j.Reps),
                                              new JProperty("Sets", j.Sets),
                                              new JProperty("Weight", j.Weights),
                                              new JProperty("Trend", j.Trend));
                    dataArray.Add(log);
                }
                JObject exercise = new JObject(new JProperty("Name", i.Name), new JProperty("Data", dataArray));
                workoutArray.Add(exercise);
            }
            return workoutArray;
        }
        public static JArray JSONShouldersData()
        {
            JArray workoutArray = new JArray();
            foreach (ExerciseModel i in MainViewModel.ShouldersList)
            {
                JArray dataArray = new JArray();
                foreach (LogModel j in i.Data)
                {
                    JObject log = new JObject(new JProperty("Date", j.Date),
                                              new JProperty("Reps", j.Reps),
                                              new JProperty("Sets", j.Sets),
                                              new JProperty("Weight", j.Weights),
                                              new JProperty("Trend", j.Trend));
                    dataArray.Add(log);
                }
                JObject exercise = new JObject(new JProperty("Name", i.Name), new JProperty("Data", dataArray));
                workoutArray.Add(exercise);
            }
            return workoutArray;
        }
        public static JArray JSONArmsData()
        {
            JArray workoutArray = new JArray();
            foreach (ExerciseModel i in MainViewModel.ArmsList)
            {
                JArray dataArray = new JArray();
                foreach (LogModel j in i.Data)
                {
                    JObject log = new JObject(new JProperty("Date", j.Date),
                                              new JProperty("Reps", j.Reps),
                                              new JProperty("Sets", j.Sets),
                                              new JProperty("Weight", j.Weights),
                                              new JProperty("Trend", j.Trend));
                    dataArray.Add(log);
                }
                JObject exercise = new JObject(new JProperty("Name", i.Name), new JProperty("Data", dataArray));
                workoutArray.Add(exercise);
            }
            return workoutArray;
        }
        public static JArray JSONAbsData()
        {
            JArray workoutArray = new JArray();
            foreach (ExerciseModel i in MainViewModel.AbsList)
            {
                JArray dataArray = new JArray();
                foreach (LogModel j in i.Data)
                {
                    JObject log = new JObject(new JProperty("Date", j.Date),
                                              new JProperty("Reps", j.Reps),
                                              new JProperty("Sets", j.Sets),
                                              new JProperty("Weight", j.Weights),
                                              new JProperty("Trend", j.Trend));
                    dataArray.Add(log);
                }
                JObject exercise = new JObject(new JProperty("Name", i.Name), new JProperty("Data", dataArray));
                workoutArray.Add(exercise);
            }
            return workoutArray;
        }

        #endregion
    }
}
