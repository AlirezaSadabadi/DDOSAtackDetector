using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class SymptomAndAttack:BaseEntity
    {
        public SymptomAndAttack()
        {

        }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }        
        public double? PriorProb { get; set; }
        [Required]
        [MaxLength(1)]
        public string SorA { get; set; }
        //برای اینکه متوجه شویم آیا این علامت از طریق سنسورها و یا کاربر
        //هم قابل دستکاری است (صحیح) ویا فقط از طریق سیستم محاسبه می شود (غلط
        //در قسمت لرنینگ از این ستون بهره می بریم 
        [Required]        
        public bool UI { get; set; }
        //برای رعایت ترتیب در استخراج کد باینری علایم
        [Required]
        public int Sorter { get; set; }
        public virtual ICollection<AttackLog> AttacksLogs { get; set; }        
        public virtual ICollection<CPT> CPT { get; set; }
        [InverseProperty("SymptomAndAttack1")]
        public virtual ICollection<SymptomAndAttackRelation> SymptomAndAttackRelations1 { get; set; }
        [InverseProperty("SymptomAndAttack2")]
        public virtual ICollection<SymptomAndAttackRelation> SymptomAndAttackRelations2 { get; set; }
    }
}
