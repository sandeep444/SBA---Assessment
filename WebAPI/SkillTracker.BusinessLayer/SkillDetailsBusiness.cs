using SkillTracker.BusinessEntities;
using SkillTracker.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.BusinessLayer
{
    public class SkillDetailsBusiness
    {
        SkillRepository repo = new SkillRepository();

        public List<SkillDetailsModel> GetAllSkillDetails()
        {
            return repo.GetAllSkillDetails();
        }

        public string AddSkill(SkillDetailsModel skill)
        {
            return repo.AddSkill(skill);
        }

        public string UpdateSkill(SkillDetailsModel skill)
        {
            return repo.UpdateSkill(skill);
        }

        public string DeleteSkill(SkillDetailsModel skill)
        {
            return repo.DeleteSkill(skill);
        }
    }
}
