using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace WorkoutLogger.Model
{
    /// <summary>
    /// Model for the Exercise Data.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    class ExerciseModel
    {
        /// <summary>
        /// Gets or sets the Exercise name.
        /// </summary>
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Exercise data.
        /// </summary>
        [DataMember(Name = "Data")]
        public List<LogModel> Data { get; set; }
    }
}
