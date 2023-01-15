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
    public class CarController : Controller
    {
        FirestoreDb database;

        [HttpGet]
        [Route("contact/getall")]
        public async Task<IActionResult> getall()
        {
            Car car = null;
            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            Query query = database.Collection("Car");
            QuerySnapshot snap = await query.GetSnapshotAsync();
            foreach (DocumentSnapshot document in snap)
            {
                car = document.ConvertTo<Car>();

            }
            return Ok(car);
        }
        [HttpGet]
        [Route("contact/geid")]
        public async Task<IActionResult> getid(Car c)
        {
            Car car = null;
            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            DocumentReference docref = database.Collection("Car").Document(c.Id);
            DocumentSnapshot snap = await docref.GetSnapshotAsync();
            if (snap.Exists)
            {
                car = snap.ConvertTo<Car>();

            }
            return Ok(car);
        }
        [HttpPost]
        [Route("contact/post")]
        public async Task<IActionResult> post(Car c)
        {
            Car car = null;

            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            CollectionReference coll = database.Collection("Car");
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"brand",c.brand },
                {"generation",c.generation },
                {"datail",c.datail },
                {"year",c.year },
                {"Engine_size",c.Engine_size },
                {"system",c.system },
                {"condition",c.condition },
                {"img",c.img }
            };
            coll.AddAsync(data1);
            return Ok(data1);
        }
        [HttpPut]
        [Route("contact/put")]
        public async Task<IActionResult> put(Car c)
        {
            Car car = null;

            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            database = FirestoreDb.Create("projectcar-f8a97");

            DocumentReference docref = database.Collection("Car").Document(c.Id);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
               {"brand",c.brand },
                {"generation",c.generation },
                {"datail",c.datail },
                {"year",c.year },
                {"Engine_size",c.Engine_size },
                {"system",c.system },
                {"condition",c.condition },
                {"img",c.img }
            };
            await docref.UpdateAsync(data);
            return Ok(data); ;
        }
    }
}
