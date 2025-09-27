using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class SymptomAndAttackRelation:BaseEntity
    {
        public SymptomAndAttackRelation()
        {

        }
        [Required]
        public int Arrange { get; set; }
        [InverseProperty("SymptomAndAttackRelations1")]
        public virtual SymptomAndAttack SymptomAndAttack1 { get; set; }
        [InverseProperty("SymptomAndAttackRelations2")]
        public virtual SymptomAndAttack SymptomAndAttack2 { get; set; }
    }
}
