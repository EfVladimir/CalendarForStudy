using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeProjectTimeTable.Models
{
    public class Lesson : BaseModel
    {
        public int SubjectId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public int TeacherId { get; set; }
        public int GroupId { get; set; }
        public int ClassroomId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public virtual Subject Subject { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public virtual Teacher Teacher { get; set; }
        [ForeignKey(nameof(GroupId))]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(ClassroomId))]
        public virtual Classroom Classroom { get; set; }
        
    }
}
