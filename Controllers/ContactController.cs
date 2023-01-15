using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICar.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace APICar.Controllers
{
    public class ContactController : Controller
    {
        FirestoreDb database;

        [HttpGet]
        [Route("contact/getall")]
        public async Task<IActionResult> getall()
        {
            Contact contact = null;
            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            Query query = database.Collection("Contact");
            QuerySnapshot snap = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot document in snap)
            {
                 contact = document.ConvertTo<Contact>();
               
            }
            return Ok(contact);
        }
        [HttpGet]
        [Route("contact/geid")]
        public async Task<IActionResult> getid(Contact c)
        {
            Contact contact = null;
            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            DocumentReference docref = database.Collection("Contact").Document(c.Id);
            DocumentSnapshot snap = await docref.GetSnapshotAsync();
            if (snap.Exists)
            {
                 contact = snap.ConvertTo<Contact>();
                
            }
            return Ok(contact);
        }
        [HttpPost]
        [Route("contact/post")]
        public async Task<IActionResult> post(Contact c)
        {
            Contact contact = null;
            
            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            CollectionReference coll = database.Collection("Contact");
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"Facebook",c.Facebook },
                {"Email",c.Phone },
                {"Line",c.Facebook },
                {"Phone",c.Phone },
                {"Phone",c.Facebook },
                {"Address",c.Phone }
            };
            coll.AddAsync(data1);
            return Ok(data1);
        }
        [HttpPut]
        [Route("contact/put")]
        public async Task<IActionResult> put(Contact c)
        {
            Contact contact = null;

            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            DocumentReference docref = database.Collection("Contect").Document(c.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
               {"Facebook",c.Facebook },
                {"Email",c.Phone },
                {"Line",c.Facebook },
                {"Phone",c.Phone },
                {"Phone",c.Facebook },
                {"Address",c.Phone }
            };
            await docref.UpdateAsync(data);
            return Ok(data); ;
        }
    }
}
