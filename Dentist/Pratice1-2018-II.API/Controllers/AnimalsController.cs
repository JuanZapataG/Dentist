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

    public class AnimalsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Animals
        public IQueryable<Animal> GetAnimals()
        {
            return db.Animals;
        }

        // GET: api/Animals/5
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> GetAnimal(int id)
        {
            Animal animal = await db.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        // PUT: api/Animals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAnimal(int id, Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animal.AnimalId)
            {
                return BadRequest();
            }

            if (animal.ImageArray != null && animal.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(animal.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Animals";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    animal.ImagePath = fullPath;
                }
            }


            db.Entry(animal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        // POST: api/Animals
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> PostAnimal(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (animal.ImageArray != null && animal.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(animal.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Animals";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    animal.ImagePath = fullPath;
                }
            }

            db.Animals.Add(animal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = animal.AnimalId }, animal);
        }

        // DELETE: api/Animals/5
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> DeleteAnimal(int id)
        {
            Animal animal = await db.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            db.Animals.Remove(animal);
            await db.SaveChangesAsync();

            return Ok(animal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnimalExists(int id)
        {
            return db.Animals.Count(e => e.AnimalId == id) > 0;
        }
    }
}