using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.Entity
{
    public class Exercise:BaseEntity
    {
        public Guid? ExerciseID { get; set; }
        public string ExerciseName { get; set; }
        public Guid? ExerciseImage { get; set; } = Guid.Empty;
        public int ExerciseState { get; set; }
        public Guid SubjectID { get; set; }
        public Guid GradeID { get; set; }
        public string? SubjectName { get; set; }
        public Guid? SubjectImage { get; set; } = Guid.Empty;
        public string? GradeName { get; set; }
        public Exercise()
        {
            
        }
    }
}
