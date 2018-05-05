using SkillTracker.BusinessEntities;
using SkillTracker.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.DataAccessLayer
{
    public class SkillRepository
    {
        public List<SkillDetailsModel> GetAllSkillDetails()
        {
            var model = new List<SkillDetailsModel>();
            using (var dbContext = new SkillTrackerDBContext())
            {
                model = dbContext.SkillDetails
                    .Select(x => new SkillDetailsModel() { SkillId = x.Skill_Id, SkillName = x.Skill_Name })
                    .ToList();
            }
            return model;
        }


        public string AddSkill(SkillDetailsModel skill)
        {
            using (var dbContext = new SkillTrackerDBContext())
            {
                var addskill = new skill_details() { Skill_Id = skill.SkillId, Skill_Name = skill.SkillName };
                dbContext.SkillDetails.Add(addskill);
                dbContext.SaveChanges();
                return "Skill Successfully Inserted";
            }
        }

        public string UpdateSkill(SkillDetailsModel skill)
        {
            using (var dbContext = new SkillTrackerDBContext())
            {
                var sk = dbContext.SkillDetails.Where(a => a.Skill_Id.Equals(skill.SkillId)).SingleOrDefault();
                sk.Skill_Name = skill.SkillName;
                dbContext.SaveChanges();
                return "Skill Successfully Updated";
            }
        }

        public string DeleteSkill(SkillDetailsModel skill)
        {
            using (var dbContext = new SkillTrackerDBContext())
            {
                var skilldet = dbContext.SkillDetails.Where(a => a.Skill_Id.Equals(skill.SkillId))
                 .SingleOrDefault();
                var associateskills = dbContext.AssociateSkill.Where(i => i.Skill_Id == skill.SkillId).ToList();
                foreach (var it in associateskills)
                {
                    var associate = dbContext.AssociateSkill.Where(i => i.Associate_SkillID == it.Associate_SkillID);                    
                    dbContext.AssociateSkill.Remove(it);
                }
                dbContext.SaveChanges();
                dbContext.SkillDetails.Remove(skilldet);
                dbContext.SaveChanges();
                return "Skill Details Successfully deleted";
            }
        }


    }
}
