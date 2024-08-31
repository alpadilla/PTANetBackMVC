using System;

namespace Alicunde.System.Exam.Domain
{
    public class BaseTrackableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}