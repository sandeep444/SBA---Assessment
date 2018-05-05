using SkillTracker.BusinessEntities;
using SkillTracker.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SkillTracker.WebAPI.Controllers
{
    public class AssociateDetailsController : ApiController
    {
        AssociateDetailsBusiness business = new AssociateDetailsBusiness();

        [HttpGet]
        [Route("api/AssociateDetails/GetDashBoardData")]
        public DashBoardModel GetDashBoardData()
        {
            return business.GetDashBoardData();
        }

        [HttpGet]
        [Route("api/AssociateDetails/GetAssociateSkillDetails")]
        public AssociateSkillsModel GetAssociateSkillDetails(int id)
        {
            return business.GetAssociateSkillDetails(id);
        }

        [HttpPost]
        [Route("api/AssociateDetails/AddAssociateSkillDetails")]
        public string AddAssociateSkill([FromBody] AssociateSkillsModel askills)
        {
            return business.AddAssociateSkill(askills);
        }

        [HttpPost]
        [Route("api/AssociateDetails/UpdateAssociateSkill")]
        public string UpdateAssociateSkill([FromBody] AssociateSkillsModel askills)
        {
            return business.UpdateAssociateSkill(askills);
        }

        [HttpPost]
        [Route("api/AssociateDetails/DeleteAssociate")]
        public string DeleteAssociate([FromBody]int id)
        {
            return business.DeleteAssociate(id);
        }
    }
}
