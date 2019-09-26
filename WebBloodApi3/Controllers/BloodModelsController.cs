using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebBloodApi3.Helpers;
using WebBloodApi3.Models;

namespace WebBloodApi3.Controllers
{
    [Authorize]
    public class BloodModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IHttpActionResult Post([FromBody] BloodModel bloodmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var stream = new MemoryStream(bloodmodel.ImageArray);
                    var guid = Guid.NewGuid().ToString();
                    var file = string.Format("{0}.jpg", guid);
                    var folder = "~\\Content\\Users";
                    var fullpath = string.Format("{0}\\{1}", folder, file);
                    var response = FilesHelper.UploadPhoto(stream, folder, file);
                    //if(response)
                    //{
                    //    bloodmodel.ImagePath = fullpath;
                    //}
                    var user = new BloodModel()
                    {
                        Username = bloodmodel.Username,
                        Email = bloodmodel.Email,
                        Phone = bloodmodel.Phone,
                        Country = bloodmodel.Country,
                        Location = bloodmodel.Location,
                        Date = bloodmodel.Date,
                        BloodGroup = bloodmodel.BloodGroup,
                        ImagePath = fullpath,
                        userId = bloodmodel.userId
                    };
                    db.bloodmodels.Add(user);
                    db.SaveChanges();
                    return Ok();

                }
                else
                {
                    return BadRequest("Incorrect request sent");
                }
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
            
        }
        public IEnumerable<BloodModel> Get(string bloodgroup,string country)
        {
          var response =  db.bloodmodels.Where(Blooduser => Blooduser.BloodGroup == bloodgroup && Blooduser.Country == country);
            if (response!=null)
            {
                return response;
            }
            return null;
        }
     public IEnumerable<BloodModel> Get()
        {
            return db.bloodmodels.OrderByDescending(c => c.Id);
        }
        public IHttpActionResult Delete (int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid UserId");

            var delete = db.bloodmodels.Where(s => s.Id == id).FirstOrDefault();
            db.Entry(delete).State = EntityState.Deleted;
            db.SaveChanges();
            

                return Ok();
        }
      

    }
}