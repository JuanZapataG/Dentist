namespace Pratice1_2018_II.API.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Domain.Models;
    using Helpers;

    public class BabiesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Babies
        public IQueryable<Baby> GetBabies()
        {
            return db.Babies;
        }

        // GET: api/Babies/5
        [ResponseType(typeof(Baby))]
        public async Task<IHttpActionResult> GetBaby(int id)
        {
            Baby baby = await db.Babies.FindAsync(id);
            if (baby == null)
            {
                return NotFound();
            }

            return Ok(baby);
        }

        // PUT: api/Babies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBaby(int id, Baby baby)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != baby.BabyId)
            {
                return BadRequest();
            }

            if (baby.ImageArray != null && baby.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(baby.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Babies";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    baby.ImagePath = fullPath;
                }
            }

            db.Entry(baby).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BabyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Babies
        [ResponseType(typeof(Baby))]
        public async Task<IHttpActionResult> PostBaby(Baby baby)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (baby.ImageArray != null && baby.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(baby.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Babies";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    baby.ImagePath = fullPath;
                }
            }

            db.Babies.Add(baby);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = baby.BabyId }, baby);
        }

        // DELETE: api/Babies/5
        [ResponseType(typeof(Baby))]
        public async Task<IHttpActionResult> DeleteBaby(int id)
        {
            Baby baby = await db.Babies.FindAsync(id);
            if (baby == null)
            {
                return NotFound();
            }

            db.Babies.Remove(baby);
            await db.SaveChangesAsync();

            return Ok(baby);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BabyExists(int id)
        {
            return db.Babies.Count(e => e.BabyId == id) > 0;
        }
    }
}