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

    public class CensusesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Censuses
        public IQueryable<Census> GetCensus()
        {
            return db.Census;
        }

        // GET: api/Censuses/5
        [ResponseType(typeof(Census))]
        public async Task<IHttpActionResult> GetCensus(int id)
        {
            Census census = await db.Census.FindAsync(id);
            if (census == null)
            {
                return NotFound();
            }

            return Ok(census);
        }

        // PUT: api/Censuses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCensus(int id, Census census)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != census.CensusId)
            {
                return BadRequest();
            }

            if (census.ImageArray != null && census.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(census.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Censuses";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    census.ImagePath = fullPath;
                }
            }

            db.Entry(census).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CensusExists(id))
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

        // POST: api/Censuses
        [ResponseType(typeof(Census))]
        public async Task<IHttpActionResult> PostCensus(Census census)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (census.ImageArray != null && census.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(census.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Censuses";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    census.ImagePath = fullPath;
                }
            }

            db.Census.Add(census);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = census.CensusId }, census);
        }

        // DELETE: api/Censuses/5
        [ResponseType(typeof(Census))]
        public async Task<IHttpActionResult> DeleteCensus(int id)
        {
            Census census = await db.Census.FindAsync(id);
            if (census == null)
            {
                return NotFound();
            }

            db.Census.Remove(census);
            await db.SaveChangesAsync();

            return Ok(census);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CensusExists(int id)
        {
            return db.Census.Count(e => e.CensusId == id) > 0;
        }
    }
}