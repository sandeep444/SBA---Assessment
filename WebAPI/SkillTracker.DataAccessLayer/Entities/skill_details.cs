using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.DataAccessLayer.Entities
{
    public class skill_details
    {
        [Key]
        public int Skill_Id { get; set; }
        public string Skill_Name { get; set; }

    }
}
