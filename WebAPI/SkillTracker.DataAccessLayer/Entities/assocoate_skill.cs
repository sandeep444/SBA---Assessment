using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.DataAccessLayer.Entities
{
    public class assocoate_skill
    {
        [Key]
        public int Associate_SkillID { get; set; }
        public int Associate_Details_ID { get; set; }
        public int Skill_Id { get; set; }
        public int Skill_Rate { get; set; }

        //[Key, ForeignKey("Associate_Details_ID")]
        //public virtual ICollection<assocoate_details> AssocoateDetails { get; set; }
        //[Key, ForeignKey("Skill_Id")]
        //public virtual ICollection<assocoate_skill> AssocoateSkill { get; set; }
    }
}
