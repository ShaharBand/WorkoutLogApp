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
    public class LogModel
    {
        /// <summary>
        /// Gets or sets the activity date.
        /// </summary>
        [DataMember(Name = "Date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the activity rep count.
        /// </summary>
        [DataMember(Name = "Reps")]
        public float Reps { get; set; }

        /// <summary>
        /// Gets or sets the activity sets count.
        /// </summary>
        [DataMember(Name = "Sets")]
        public float Sets { get; set; }

        /// <summary>
        /// Gets or sets the sets the activity weight.
        /// </summary>
        [DataMember(Name = "Weight")]
        public float Weights { get; set; }

        /// <summary>
        /// Gets or sets the sets the activity trend precentage by volume.
        /// </summary>
        [DataMember(Name = "Trend")]
        public float Trend { get; set; }
        public string TrendFormat => String.Format("{0:0.00}% {1}", Trend, (Trend >= 0) ? "↑" : "↓");
    }
}
