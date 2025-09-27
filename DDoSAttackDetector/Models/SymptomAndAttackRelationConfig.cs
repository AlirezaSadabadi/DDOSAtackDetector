using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace Models
{
    public class SymptomAndAttackRelationConfig//:EntityTypeConfiguration<SymptomAndAttackRelation>
    {
        //public SymptomAndAttackRelationConfig()
        //{
        //    this.HasRequired(z => z.SymptomAndAttack1)
        //        .WithMany(z => z.SymptomAndAttackRelations1)
        //        .HasForeignKey(z => z.ParentId)
        //        .WillCascadeOnDelete(false);

        //    this.HasRequired(z => z.SymptomAndAttack2)
        //       .WithMany(z => z.SymptomAndAttackRelations2)
        //       .HasForeignKey(z => z.ChildId)
        //       .WillCascadeOnDelete(false);
        //}
    }
}
