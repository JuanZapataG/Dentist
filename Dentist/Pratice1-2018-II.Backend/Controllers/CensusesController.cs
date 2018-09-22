namespace Pratice1_2018_II.Backend.Controllers
{
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Domain.Models;
    using Models;

    public class CensusesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Censuses
        public async Task<ActionResult> Index()
        {
            return View(await db.Census.ToListAsync());
        }

        // GET: Censuses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Census census = await db.Census.FindAsync(id);
            if (census == null)
            {
                return HttpNotFound();
            }
            return View(census);
        }

        // GET: Censuses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Censuses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Census census)
        {
            if (ModelState.IsValid)
            {
                db.Census.Add(census);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(census);
        }

        // GET: Censuses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Census census = await db.Census.FindAsync(id);
            if (census == null)
            {
                return HttpNotFound();
            }
            return View(census);
        }

        // POST: Censuses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Census census)
        {
            if (ModelState.IsValid)
            {
                db.Entry(census).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(census);
        }

        // GET: Censuses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Census census = await db.Census.FindAsync(id);
            if (census == null)
            {
                return HttpNotFound();
            }
            return View(census);
        }

        // POST: Censuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Census census = await db.Census.FindAsync(id);
            db.Census.Remove(census);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}