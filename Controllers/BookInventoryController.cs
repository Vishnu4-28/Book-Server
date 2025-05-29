//using E_commerce.Server.Service;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace E_commerce.Server.Controllers
//{

//    [ApiController]
//    [Route("[controller]")]
//    public class BookInventoryController : Controller
//    {

//        private readonly IService _service;

//        public BookInventoryController(IService service)
//        {
//            _service = service;
//        }



//        [HttpGet(Name = "GetBooks")]
//        public  async Task<IActionResult> getAlllBooks()
//        {

//            var data = _service.GetBooks();

//            return View();
//        }

//        // GET: BookInventoryController/Details/5
//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: BookInventoryController/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: BookInventoryController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: BookInventoryController/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: BookInventoryController/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: BookInventoryController/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: BookInventoryController/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}
