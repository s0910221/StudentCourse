using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Entity.Entity
{
    public class StudentCourse
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        [Range(0, 100)]
        public int? Score { get; set; }

        [JsonIgnore]
        public virtual Student Student { get; set; }

        [JsonIgnore]
        public virtual Course Course { get; set; }
    }
}
