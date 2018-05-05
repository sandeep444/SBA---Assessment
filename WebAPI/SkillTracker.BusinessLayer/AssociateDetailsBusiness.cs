using SkillTracker.BusinessEntities;
using SkillTracker.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.BusinessLayer
{
    public class AssociateDetailsBusiness
    {
        AssociateRepository repo = new AssociateRepository();

        public DashBoardModel GetDashBoardData()
        {
            return repo.GetDashBoardData();
        }

        public AssociateSkillsModel GetAssociateSkillDetails(int associatedetailId)
        {
            return repo.GetAssociateSkillDetails(associatedetailId);
        }
        public string AddAssociateSkill(AssociateSkillsModel askills)
        {
            return repo.AddAssociateSkill(askills);
        }
        public string UpdateAssociateSkill(AssociateSkillsModel askills)
        {
            return repo.UpdateAssociateSkill(askills);
        }
        public string DeleteAssociate(int associatedetailId)
        {
            return repo.DeleteAssociate(associatedetailId);
        }
    }
}
