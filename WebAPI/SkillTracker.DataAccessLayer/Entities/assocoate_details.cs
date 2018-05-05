using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.DataAccessLayer.Entities
{
    public class assocoate_details
    {
        [Key]
        public int Associate_Details_ID { get; set; }
        public string Associate_ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string Sex { get; set; }
        public string Pic { get; set; }
        public bool Status_Green { get; set; }
        public bool Status_Blue { get; set; }
        public bool Status_Red { get; set; }
        public bool Level_1 { get; set; }
        public bool Level_2 { get; set; }
        public bool Level_3 { get; set; }
        public string Remark { get; set; }
        public string Other { get; set; }
        public string Strength { get; set; }
        public string Weakness { get; set; }

    }
}
