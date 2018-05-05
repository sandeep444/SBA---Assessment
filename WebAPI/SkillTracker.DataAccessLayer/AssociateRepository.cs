using SkillTracker.BusinessEntities;
using SkillTracker.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTracker.DataAccessLayer
{
    public class AssociateRepository
    {
        public DashBoardModel GetDashBoardData()
        {
            var dModel = new DashBoardModel();
            var gModel = new List<GraphModel>();
            var aModel = new List<AssociateModel>();
            using (var dbContext = new SkillTrackerDBContext())
            {
                 
                //Skill Details
                var smodel = dbContext.SkillDetails
                    .Select(x => new SkillDetailsModel() { SkillId = x.Skill_Id, SkillName = x.Skill_Name })
                    .ToList();
                //Associate Skill Mapped
                var totSkill = dbContext.AssociateSkill.Where(i => i.Skill_Rate != 0).ToList();
                //Associate Details
                var associates = dbContext.AssociateDetails.ToList();

                //Graph model population
                foreach (var sk in smodel)
                {
                    var cnt = totSkill.Where(i => i.Skill_Id == sk.SkillId).Count();
                    if (cnt > 0)
                    {
                        var s = new GraphModel();
                        s.Color = "Red";
                        s.SkillName = sk.SkillName;
                        s.Percent = ((double)cnt / (double)totSkill.Count()) * 100;
                        gModel.Add(s);
                    }
                }
                dModel.GraphDetails = gModel;

                //Other details population
                dModel.RegistredCandidate = associates.Count();
                dModel.FemaleCandidate = ((double)associates.Where(i => i.Sex.ToLower() == "female").Count() / (double)dModel.RegistredCandidate) * 100;
                dModel.MaleCandidate = ((double)associates.Where(i => i.Sex.ToLower() == "male").Count() / (double)dModel.RegistredCandidate) * 100; 
                dModel.FresherCandidate = ((double)associates.Where(i => i.Level_1 ==true).Count() / (double)dModel.RegistredCandidate) * 100;

                //Rated
                var rated = totSkill.Select(k => k.Associate_Details_ID).Distinct().ToList();
                dModel.RatedCandidate = rated.Count();
                dModel.FemaleRatedCandidate = associates.Where(i => i.Level_1 == true).Count();
                dModel.MaleRatedCandidate = associates.Where(i => i.Level_1 == true).Count();

                //Level
                dModel.Level1Candidate = ((double)associates.Where(i => i.Level_1 == true).Count() / (double)dModel.RegistredCandidate) * 100;
                dModel.Level2Candidate = ((double)associates.Where(i => i.Level_2 == true).Count() / (double)dModel.RegistredCandidate) * 100;
                dModel.Level3Candidate = ((double)associates.Where(i => i.Level_3 == true).Count() / (double)dModel.RegistredCandidate) * 100;
 
                //Associate model population
                foreach(var itm in associates)
                {
                    var model = new AssociateModel();
                    model.AssociateDetailsID = itm.Associate_Details_ID;
                    model.Pic = itm.Pic;
                    model.Name = itm.Name;
                    model.Mobile = itm.Mobile;
                    model.Email = itm.Email;
                    model.StatusColor = "Red";
                    var sk= totSkill.Where(j => j.Associate_Details_ID == itm.Associate_Details_ID)
                                         .Join(smodel, k => k.Skill_Id, i => i.SkillId, (k, i) =>
                                          new AssociateSkillMappingModel()
                                          {
                                              AssociateSkillID = k.Associate_SkillID,
                                              AssociateDetailsID = k.Associate_Details_ID,
                                              SkillId = k.Skill_Id,
                                              SkillName = i.SkillName,
                                              SkillRate = k.Skill_Rate
                                          }).ToList();
                    model.SkillSummary= string.Join(",", sk.Select(i => i.SkillName));
                    aModel.Add(model);
                }
                dModel.AssociateDetails = aModel;
            }
            return dModel;
        }
        public AssociateSkillsModel GetAssociateSkillDetails(int associatedetailId)
        {
            var model = new AssociateSkillsModel();
            using (var dbContext = new SkillTrackerDBContext())
            {
                if (associatedetailId == 0)
                {
                    model.AssociateDetails = SetDefaultAssociateModel();
                    model.Skills = dbContext.SkillDetails
                         .Select(x => new AssociateSkillMappingModel()
                         {
                             AssociateSkillID = 0,
                             AssociateDetailsID = associatedetailId,
                             SkillId = x.Skill_Id,
                             SkillName = x.Skill_Name,
                             SkillRate = 0
                         })
                    .ToList();
                }
                else
                {
                    model.AssociateDetails = dbContext.AssociateDetails.Where(j => j.Associate_Details_ID == associatedetailId)
                    .Select(x => new AssociateModel()
                    {
                        AssociateDetailsID = x.Associate_Details_ID,
                        AssociateID = x.Associate_ID,
                        Email = x.Email,
                        Level1 = x.Level_1,
                        Level2 = x.Level_2,
                        Level3 = x.Level_3,
                        Mobile = x.Mobile,
                        Name = x.Name,
                        Other = x.Other,
                        Pic = x.Pic,
                        Remark = x.Remark,
                        Sex = x.Sex,
                        StatusBlue = x.Status_Blue,
                        StatusGreen = x.Status_Green,
                        StatusRed = x.Status_Red,
                        Strength = x.Strength,
                        Weakness = x.Weakness
                    })
                    .FirstOrDefault();

                    model.Skills = dbContext.AssociateSkill.Where(j => j.Associate_Details_ID == associatedetailId)
                         .Join(dbContext.SkillDetails, k => k.Skill_Id, i => i.Skill_Id, (k, i) =>
                          new AssociateSkillMappingModel()
                          {
                              AssociateSkillID = k.Associate_SkillID,
                              AssociateDetailsID = k.Associate_Details_ID,
                              SkillId = k.Skill_Id,
                              SkillName = i.Skill_Name,
                              SkillRate = k.Skill_Rate
                          }).ToList();

                    //Add excluded items                    

                    var res = dbContext.SkillDetails.Where(c => !dbContext.AssociateSkill.Where(l => l.Associate_Details_ID == associatedetailId)
                      .Select(b => b.Skill_Id).Contains(c.Skill_Id)).ToList();
                    foreach (var item in res)
                    {
                        var map = new AssociateSkillMappingModel();
                        map.AssociateDetailsID = associatedetailId;
                        map.SkillId = item.Skill_Id;
                        map.SkillName = item.Skill_Name;
                        map.SkillRate = 0;
                        model.Skills.Add(map);
                    }

                }
            }
            return model;
        }

        public string AddAssociateSkill(AssociateSkillsModel askills)
        {
            using (var dbContext = new SkillTrackerDBContext())
            {
                var associate = new assocoate_details();
                associate.Associate_ID = askills.AssociateDetails.AssociateID;
                associate.Email = askills.AssociateDetails.Email;
                associate.Level_1 = askills.AssociateDetails.Level1;
                associate.Level_2 = askills.AssociateDetails.Level2;
                associate.Level_3 = askills.AssociateDetails.Level3;
                associate.Mobile = askills.AssociateDetails.Mobile;
                associate.Name = askills.AssociateDetails.Name;
                associate.Other = askills.AssociateDetails.Other;
                associate.Pic = askills.AssociateDetails.Pic;
                associate.Remark = askills.AssociateDetails.Remark;
                associate.Sex = askills.AssociateDetails.Sex;
                associate.Status_Blue = askills.AssociateDetails.StatusBlue;
                associate.Status_Green = askills.AssociateDetails.StatusGreen;
                associate.Status_Red = askills.AssociateDetails.StatusRed;
                associate.Strength = askills.AssociateDetails.Strength;
                associate.Weakness = askills.AssociateDetails.Weakness;
                dbContext.AssociateDetails.Add(associate);
                dbContext.SaveChanges();
                int detailsId = associate.Associate_Details_ID;
                var skillDet = new List<assocoate_skill>();
                foreach (var items in askills.Skills)
                {
                    var skill = new assocoate_skill();
                    skill.Associate_Details_ID = detailsId;
                    skill.Skill_Id = items.SkillId;
                    skill.Skill_Rate = items.SkillRate;
                    dbContext.AssociateSkill.Add(skill);

                }
                dbContext.SaveChanges();
                return "Associate Details Successfully Inserted";
            }
        }

        public string UpdateAssociateSkill(AssociateSkillsModel askills)
        {
            using (var dbContext = new SkillTrackerDBContext())
            {
                var associate = dbContext.AssociateDetails.Where(j => j.Associate_Details_ID == askills.AssociateDetails.AssociateDetailsID).FirstOrDefault();
                associate.Associate_ID = askills.AssociateDetails.AssociateID;
                associate.Email = askills.AssociateDetails.Email;
                associate.Level_1 = askills.AssociateDetails.Level1;
                associate.Level_2 = askills.AssociateDetails.Level2;
                associate.Level_3 = askills.AssociateDetails.Level3;
                associate.Mobile = askills.AssociateDetails.Mobile;
                associate.Name = askills.AssociateDetails.Name;
                associate.Other = askills.AssociateDetails.Other;
                associate.Pic = askills.AssociateDetails.Pic;
                associate.Remark = askills.AssociateDetails.Remark;
                associate.Sex = askills.AssociateDetails.Sex;
                associate.Status_Blue = askills.AssociateDetails.StatusBlue;
                associate.Status_Green = askills.AssociateDetails.StatusGreen;
                associate.Status_Red = askills.AssociateDetails.StatusRed;
                associate.Strength = askills.AssociateDetails.Strength;
                associate.Weakness = askills.AssociateDetails.Weakness;


                //Remove all associate skills
                dbContext.AssociateSkill.RemoveRange(dbContext.AssociateSkill.Where(k => k.Associate_Details_ID == associate.Associate_Details_ID));
                dbContext.SaveChanges();

                var skillDet = new List<assocoate_skill>();

                foreach (var items in askills.Skills)
                {
                    var skill = new assocoate_skill();
                    skill.Associate_Details_ID = associate.Associate_Details_ID;
                    skill.Skill_Id = items.SkillId;
                    skill.Skill_Rate = items.SkillRate;
                    dbContext.AssociateSkill.Add(skill);
                }
                dbContext.SaveChanges();
                return "Associate Details Successfully Updated";
            }
        }

        public string DeleteAssociate(int associatedetailId)
        {
            using (var dbContext = new SkillTrackerDBContext())
            {
                var associate = dbContext.AssociateDetails.Where(j => j.Associate_Details_ID == associatedetailId).FirstOrDefault();
                var associateskill = dbContext.AssociateSkill.Where(k => k.Associate_Details_ID == associate.Associate_Details_ID);
                foreach (var it in associateskill)
                {
                    dbContext.AssociateSkill.Remove(it);
                }
                dbContext.SaveChanges();
                dbContext.AssociateDetails.Remove(associate);
                dbContext.SaveChanges();
            }
            return "Associate Details Successfully Deleted";
        }

        private AssociateModel SetDefaultAssociateModel()
        {
            var model = new AssociateModel();
            model.AssociateDetailsID = 0;
            model.AssociateID = "";
            model.Name = "";
            model.Email = "";
            model.Mobile = 0;
            model.Sex = "Male";
            model.Pic = "";
            model.StatusGreen = false;
            model.StatusBlue = false;
            model.StatusRed = false;
            model.Level1 = false;
            model.Level2 = false;
            model.Level3 = false;
            model.Remark = "";
            model.Other = "";
            model.Strength = "";
            model.Weakness = "";
            return model;
        }
    }
}
