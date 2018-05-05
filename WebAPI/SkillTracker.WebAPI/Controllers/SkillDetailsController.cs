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
    public class SkillDetailsController : ApiController
    {
        SkillDetailsBusiness business = new SkillDetailsBusiness();

        [HttpGet]
        [Route("api/SkillDetails/GetAllSkillDetails")]
        public List<SkillDetailsModel> GetAllSkillDetails()
        {
            return business.GetAllSkillDetails();
        }

        [HttpPost]
        [Route("api/SkillDetails/AddSkill")]
        public string AddSkill([FromBody] SkillDetailsModel skill)
        {
            try
            {
                var rr = business.AddSkill(skill);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }


        [HttpPost]
        [Route("api/SkillDetails/UpdateSkill")]
        public string UpdateSkill([FromBody] SkillDetailsModel skill)
        {
            try
            {
                var rr = business.UpdateSkill(skill);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }

        [HttpPost]
        [Route("api/SkillDetails/DeleteSkill")]
        public string DeleteSkill([FromBody] SkillDetailsModel skill)
        {
            try
            {
                var rr = business.DeleteSkill(skill);
                return rr;
            }
            catch (Exception e)
            {
                //this.log.Error(e.Message);
                return null;
            }
        }
    }
}
