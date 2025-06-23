using E_commerce.Server.Hubs;
using E_commerce.Server.Model.DTO;
using E_commerce.Server.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;

namespace E_commerce.Server.Controllers
{
    //[Authorize(Roles = "Admin")]
    [EnableCors("AllowLocalhost3000")]
    [ApiController]
    [Route("[controller]/[Action]")]



    public class BooksController : ControllerBase
    {

        private readonly IBookService _service;

        private readonly IHubContext<NotificationHub> _notificationHubContext;  

        public BooksController(IBookService service, IHubContext<NotificationHub> hub )
        {
            _service = service;
            _notificationHubContext = hub;

        }
        //[HttpPost(Name = "AddBook")]
        [HttpPost(Name = "sendNotification")]
        public async Task<IActionResult> SendNotification(string msg)
        {
            var notification = new
            {
                imageUrl = "https://placekitten.com/200/200",
                msg = msg ,
                connection = "Some connection info"
            };

            await Task.Delay(5000);
            await _notificationHubContext.Clients.All.SendAsync("receiveNotification", notification);
            return Ok(notification);
        }



        [HttpPost(Name = "upload-excel")]
        public async Task<IActionResult> UploadExcelFile(IFormFile file)
        {
            try
            {
                var result = await _service.PostExcelFile(file);
                if (result == null || !result.Any())
                    return BadRequest("No data found in the file");

                return Ok(new
                {
                    statusCode = 200,
                    message = "File processed successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = "An error occurred",
                    error = ex.Message
                });
            }

        }



        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "AddBook")]
        public async Task<IActionResult> AddBook([FromBody] BookReq book)
        {

            var errors = BookReqValidator.Validate(book);
            if (errors.Any())
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Validation failed",
                    errors
                });

            }

            var data = await _service.AddBooks(book);
            if (!data.success)
            {
                return StatusCode(data.statusCode, new
                {
                    statusCode = data.statusCode,
                    message = "Failed to add book"
                });
            }
            return Ok(new
            {
                statusCode = data.statusCode,
                message = "Book added successfully"
            });

        }




        //[HttpPost]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> ImportExcelFile([FromForm] IFormFile file)
        //{
        //    try
        //    {
        //        if (file == null || file.Length == 0)
        //            return BadRequest("No file uploaded");

        //        var result = await _service.PostExcelFile(file);

        //        if (result == null || !result.Any())
        //            return BadRequest("No data found in the file");

        //        return Ok(new
        //        {
        //            statusCode = 200,
        //            message = "File processed successfully",
        //            data = result
        //        });
        //    }
        //    catch
        //    {
        //        return StatusCode(500, "Something went wrong: ");
        //    }
        //}





        [Authorize(Roles = "Admin")]
        [HttpDelete(Name = "DeleteBook")]
        public async Task<IActionResult> deleteBook(int book_id)
        {
            if (book_id <= 0)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Invalid book ID"
                });
            }
            var data = await _service.deleteBook(book_id);
            if (!data.success)
            {
                return StatusCode(data.statusCode, new
                {
                    statusCode = data.statusCode,
                    message = "Failed to delete book"
                });
            }
            return Ok(new
            {
                statusCode = data.statusCode,
                message = "Book deleted successfully"
            });
        }


        [Authorize(Roles = "Admin")]
        [HttpPut(Name = "RestoreBooks")]
        public async Task<IActionResult> restoreBooks(int book_id)
        {
            if (book_id <= 0)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Invalid book ID"
                });
            }

            var result = await _service.RestoreBook(book_id);

            if (!result.success)
            {
                return StatusCode(result.StatusCode, new
                {
                    statusCode = result.StatusCode,
                    message = "Something went wrong"
                });
            }
            return Ok(new
            {
                statusCode = result.StatusCode,
                message = "Book restored successfully"
            });
        }



        [Authorize(Roles = "Admin")]
        [HttpPut(Name = "softDelete")]
        public async Task<IActionResult> SoftDelete(int book_id)
        {
            if (book_id <= 0)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Invalid book ID"
                });
            }

            var result = await _service.SoftDeleteBook(book_id);

            if (!result.success)
            {
                return StatusCode(result.statusCode, new
                {
                    statusCode = result.statusCode,
                    message = result.message
                });
            }
            return Ok(new
            {
                statusCode = result.statusCode,
                message = "Book soft deleted successfully"
            });

        }



        [HttpGet(Name = "GetById")]
        public async Task<IActionResult> getById(int book_id)
        {
            if (book_id <= 0)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Invalid book ID"
                });
            }

            var data = await _service.GetById(book_id);

            return Ok(new
            {
                statusCode = data.StatusCode,
                data = data.Books
            });
        }


        [Authorize(Roles = "Admin")]
        [HttpPut(Name = "UpdateBook")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookReq req, int book_id)
        {
            if (req == null || book_id <= 0)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Invalid book data or ID"
                });
            }
            var data = await _service.UpdateById(req, book_id);
            if (!data.success)
            {
                return StatusCode(data.StatusCode, new
                {
                    statusCode = data.StatusCode,
                    message = "Failed to update book"
                });
            }
            return Ok(new
            {
                statusCode = data.StatusCode,
                message = "Book updated successfully"
            });

        }



        [HttpGet(Name = "GetDeletedBooks")]
        public async Task<IActionResult> getdeletedBooks()
        {

            var data = await _service.getDeleteBooks();

            return Ok(new
            {
                statusCode = data.statusCode,
                Data = data.Books
            });

        }


        //[Authorize(Roles = "Admin")]
        [HttpGet(Name = "GetAllBooks")]
        public async Task<IActionResult> getAlllBooks()
        {

            var data = await _service.GetBooks();


            //return Ok(data.Books);
            if(data.statusCode == 200)
            {
                string msg = "all books are shown here";
                SendNotification(msg);
            }

            return Ok(new Xmlres
            {
                statusCode = data.statusCode,
                Data = data.Books?.ToArray() ?? Array.Empty<BookRes>()

            });


            //return View(new
            //{
            //    statusCode = data.statusCode,
            //    Data = data.Books
            //});

        }




        [HttpGet]
        public async Task<IActionResult> getAllBooksInProtoBuff()
        {
            var data = await _service.GetBooks();

            var bookProtobufList = data.Books.Select(book => new bookProtobuf
            {
                Book_Id = book.Book_Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Quantity = book.Quantity,   
                IsDeleted = book.IsDeleted,
                FilePath = book.FilePath
            }).ToList();

            byte[] protoBytes = ProtoSerializer.ProtoSerialize(bookProtobufList);
            return File(protoBytes, "application/x-protobuf", "Book.bin");
        }


    }
}

