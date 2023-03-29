using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiMasterDetaileCRUD.Model;
using System.Text.Json;

namespace NetCoreWebApiMasterDetaileCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDetailsController : ControllerBase
    {
        ApplicationDbContext db;
        public MasterDetailsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [Route("one")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            IEnumerable<string> classes = new List<string>() { "one", "two", "three" };
            return classes;
        }
        [Route("two")]
        [HttpGet]
        public IEnumerable<class1> Get2() 
        {
            IEnumerable<class1> a = new List<class1>() { new class1 { id = "S-01", name = "Karim", Class = "Six" }, new class1 { id = "S-02", name = "Rahim", Class = "Six" }, new class1 { id = "S-03", name = "Jamal", Class = "Seven" } };

            return a;

        }
        [HttpPost]
        [Route("AddMasterDetailsVm")]
        public string Add(MasterDetailsVm masterDetails)
        {
            student student = new student()
            {
                ID = masterDetails.student.ID,
                studentName = masterDetails.student.studentName
            };
            db.students.Add(student);
            db.SaveChanges();
            foreach(var c in masterDetails.details)
            {
                Details details = new Details()
                {
                    DetailsID = c.DetailsID,
                    ID = c.ID,
                    Class = c.Class,
                    phone = c.phone
                    
                };
                db.details.Add(details);
               
                
            }
            db.SaveChanges();
            return "success";
        }
        [HttpPost]
        [Route("RemoveMasterDetailsVm")]
        public string RemoveMasterDetailsVm(string id)
        {
            List<Details> details = db.details.Where(z=>z.DetailsID == id).ToList();
            db.details.RemoveRange(details);
            db.SaveChanges();
            student student = db.students.Find(id);
            if(student == null)
            {
                return "Not Found";
            }
            else
            {
                db.students.Remove(student);
                
            }
            db.SaveChanges();
            return "success";
        }
        [HttpPost]
        [Route("UpdateMasterDetailsVm")]
        public string UpdateMasterDetailsVm(MasterDetailsVm md)
        {
            RemoveMasterDetailsVm(md.student.ID);
            Add(md);
            return "success";
        }
        [HttpGet]
        [Route("GetTwoTables")]
        public string GetTwoTables()
        {


            var a = (from d in db.students select new { d.ID, d.studentName, d.details });
            //var jo = JsonSerializer.Deserialize<Master>(a);
            return JsonSerializer.Serialize(a);
        }
        [HttpGet]
        [Route("GetTwoTables2")]
        public string GetTwoTables2(string id)
        {
            var a = (from d in db.students where d.ID == id select new { d.ID, d.studentName, d.details }).FirstOrDefault();
            return JsonSerializer.Serialize(a);

        }
        [HttpGet]
        [Route("GenerateCode")]
        public string GenerateCode()
        {
            string a1 = "";
            string b1 = "";

            try
            {
                var a = (from det in db.students select det.ID.Substring(3)).Max();
                if (a == null)
                    a = "0";
                int b = int.Parse(a.ToString()) + 1;
                if (b < 10)
                {
                    b1 = "000" + b.ToString();
                }
                else if (b < 100)
                {
                    b1 = "00" + b.ToString();
                }
                else if (b < 1000)
                {
                    b1 = "0" + b.ToString();
                }
                else
                {
                    b1 = b.ToString();
                }
                a1 = "S-" + b1.ToString();
            }
            catch (Exception ex)
            {
                a1 = "S-0001";
            }
            return JsonSerializer.Serialize(a1);
        }
        [HttpGet]
        [Route("Child_Exists")]
        public string Child_Exists(string id)
        {
            var a = (from p in db.details where p.DetailsID == id select new { p.DetailsID, }).FirstOrDefault();
            if (a != null)
                return JsonSerializer.Serialize("1");
            else
                return JsonSerializer.Serialize("0");
        }



    }




}

