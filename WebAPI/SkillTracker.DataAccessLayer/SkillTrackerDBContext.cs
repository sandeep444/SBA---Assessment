using SkillTracker.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.DataAccessLayer
{
   public class SkillTrackerDBContext : DbContext
    {
        public SkillTrackerDBContext() : base("name = SkillTrackerConnection") { }

        public virtual DbSet<skill_details> SkillDetails { get; set; }
        public virtual DbSet<assocoate_details> AssociateDetails { get; set; }
        public virtual DbSet<assocoate_skill> AssociateSkill { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<assocoate_details>()
            //.HasMany<assocoate_skill>(c => c.)
            //.WithOptional(x => x.Parent)
            //.WillCascadeOnDelete(true);
        }
    }
}
