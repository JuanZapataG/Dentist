namespace Pratice1_2018_II.Backend.Controllers
{
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Domain.Models;
    using Models;

    public class BabiesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Babies
        public async Task<ActionResult> Index()
        {
            return View(await db.Babies.ToListAsync());
        }

        // GET: Babies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Baby baby = await db.Babies.FindAsync(id);
            if (baby == null)
            {
                return HttpNotFound();
            }
            return View(baby);
        }

        // GET: Babies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Babies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Baby baby)
        {
            if (ModelState.IsValid)
            {
                db.Babies.Add(baby);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(baby);
        }

        // GET: Babies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Baby baby = await db.Babies.FindAsync(id);
            if (baby == null)
            {
                return HttpNotFound();
            }
            return View(baby);
        }

        // POST: Babies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Baby baby)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baby).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(baby);
        }

        // GET: Babies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Baby baby = await db.Babies.FindAsync(id);
            if (baby == null)
            {
                return HttpNotFound();
            }
            return View(baby);
        }

        // POST: Babies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Baby baby = await db.Babies.FindAsync(id);
            db.Babies.Remove(baby);
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