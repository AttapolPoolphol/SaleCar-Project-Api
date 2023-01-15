using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICar.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICar.Controllers
{
    public class BookController : Controller
    {
        FirestoreDb database;

        [HttpGet]
        [Route("book/getall")]
        public async Task<IActionResult> getall()
        {
            Book book = null;
            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            Query query = database.Collection("Book");
            QuerySnapshot snap = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot document in snap)
            {
                book = document.ConvertTo<Book>();

            }
            return Ok(book);
        }
        [HttpGet]
        [Route("book/geid")]
        public async Task<IActionResult> getid(Book c)
        {
            Book book = null;
            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            DocumentReference docref = database.Collection("Book").Document(c.Id);
            DocumentSnapshot snap = await docref.GetSnapshotAsync();
            if (snap.Exists)
            {
                book = snap.ConvertTo<Book>();

            }
            return Ok(book);
        }
        [HttpPost]
        [Route("book/post")]
        public async Task<IActionResult> post(Book c)
        {
            Book book = null;

            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            CollectionReference coll = database.Collection("Book");
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"Name",c.Name },
                {"Email",c.Email},
                {"Phone",c.Phone },
                {"Idcar",c.Idcar },
                {"Date",DateTime.Now }
            };
            coll.AddAsync(data1);
            return Ok(data1);
        }
        [HttpPut]
        [Route("book/put")]
        public async Task<IActionResult> put(Book c)
        {
            Book book = null;

            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            DocumentReference docref = database.Collection("Book").Document(c.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
               {"Name",c.Name },
                {"Email",c.Email},
                {"Phone",c.Phone },
                {"Idcar",c.Idcar },
                {"Date",DateTime.Now }
            };
            await docref.UpdateAsync(data);
            return Ok(data); ;
        }
    }
}
