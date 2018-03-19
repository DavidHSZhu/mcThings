using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustProject.Models;

namespace CustProject.Controllers
{
    public class CustomersController : Controller



    {
        private CustomerDbContext db = new CustomerDbContext();
        private static readonly int PAGE_SIZE = 3;//maxmum records displayed on each page

        private int GetPageCount(int recordCount)//get total pages bases on total records
        {
            int pageCount = recordCount / PAGE_SIZE;
            if (recordCount % PAGE_SIZE != 0)
            {
                pageCount += 1;//if there is mod we need add one page to display
            }
            return pageCount;
        }


        private List<Customer> GetPagedDataSource(IQueryable<Customer> customers,
        int pageIndex, int recordCount)
        {
            var pageCount = GetPageCount(recordCount);//get total page numbers
            if (pageIndex >= pageCount && pageCount >= 1)
            {
                pageIndex = pageCount - 1;//pageIndex couldn't be greater than pagecount
            }
            //paging 
            return customers.OrderBy(m => m.CustomerID)
                .Skip(pageIndex * PAGE_SIZE)//skip pageIndex*pagesize records
                .Take(PAGE_SIZE).ToList();
        }



        // GET: Customers
        //first time to load index page
        public ActionResult Index()
        {
            var customers = db.Customers as IQueryable<Customer>;
            var recordCount = customers.Count();
            var pageCount = GetPageCount(recordCount);

            ViewBag.PageIndex = 0;//initialize pageindex and communicate with index.cshtml
            ViewBag.PageCount = pageCount;

           
            return View(GetPagedDataSource(customers, 0, recordCount));
        }
        //get response when click on search button 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Name, int PageIndex)//get requested variable customername and pageindex
        {
            var customers = db.Customers as IQueryable<Customer>;
            if (!String.IsNullOrEmpty(Name))
            {
                customers = customers.Where(m => m.CustomerName.Contains(Name));
            }
            var recordCount = customers.Count();//get records count after executinging  inquiry statement 
            var pageCount = GetPageCount(recordCount);
            if (PageIndex >= pageCount && pageCount >= 1)
            {
                PageIndex = pageCount - 1;
            }

            customers = customers.OrderBy(m => m.CustomerID)
                 .Skip(PageIndex * PAGE_SIZE).Take(PAGE_SIZE);

            ViewBag.PageIndex = PageIndex;
            ViewBag.PageCount = pageCount;
            return View(customers.ToList());
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CustomerName,CustomerAddress,Remarks")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CustomerName,CustomerAddress,Remarks")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
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
