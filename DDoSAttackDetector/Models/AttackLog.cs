using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class AttackLog:BaseEntity
    {
        public AttackLog()
        {
            DateTimeOccurred = DateTime.Now;
        }
        [Required]        
        public string SymptomState { get; set; }
        [Required]
        public double PosteriorProb { get; set; }
        [Required]
        public bool ApprovalFlag { get; set; }
        [Required]
        public bool LearningFlag { get; set; }
        public DateTime DateTimeOccurred { get; set; }
        public virtual SymptomAndAttack SymptomAndAttack { get; set; }
    }
}
