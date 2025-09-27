using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CPT:BaseEntity
    {
        public CPT()
        {

        }        
        [Required]
        public int State { get; set; }
        public double Prob { get; set; }
        public virtual SymptomAndAttack SymptomAndAttack { get; set; }
    }
}
