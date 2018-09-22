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

    public class VehiclesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Vehicles
        public IQueryable<Vehicle> GetVehicles()
        {
            return db.Vehicles;
        }

        // GET: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public async Task<IHttpActionResult> GetVehicle(int id)
        {
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // PUT: api/Vehicles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.VehicleId)
            {
                return BadRequest();
            }

            if (vehicle.ImageArray != null && vehicle.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(vehicle.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Vehicles";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    vehicle.ImagePath = fullPath;
                }
            }

            db.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        [ResponseType(typeof(Vehicle))]
        public async Task<IHttpActionResult> PostVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (vehicle.ImageArray != null && vehicle.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(vehicle.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Vehicles";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    vehicle.ImagePath = fullPath;
                }
            }

            db.Vehicles.Add(vehicle);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = vehicle.VehicleId }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public async Task<IHttpActionResult> DeleteVehicle(int id)
        {
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            db.Vehicles.Remove(vehicle);
            await db.SaveChangesAsync();

            return Ok(vehicle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehicleExists(int id)
        {
            return db.Vehicles.Count(e => e.VehicleId == id) > 0;
        }
    }
}