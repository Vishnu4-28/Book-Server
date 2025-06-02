using E_commerce.Server.Model.DTO;
using E_commerce.Server.Model.Entities;
using E_commerce.Server.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Server.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]/[Action]")]
    public class BooksController : ControllerBase
    {

        private readonly IService _service;

        public BooksController(IService service)
        {
            _service = service;

        }


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




        [HttpPut(Name ="softDelete")]
        public async Task<IActionResult> SoftDelete(int book_id)
        {
            if(book_id <= 0)
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



        [HttpGet(Name = "GetBooks")]
        public async Task<IActionResult> getdeletedBooks()
        {

            var data = await _service.getDeleteBooks();

            return Ok(new
            {
                statusCode = data.statusCode,
                Data = data.Books
            });

        }

      

        [HttpGet(Name = "GetDeletedBooks")]
        public async Task<IActionResult> getAlllBooks()
        {

            var data = await _service.GetBooks();



            return Ok(new
            {
                statusCode = data.statusCode,
                Data = data.Books
            });

            //return View(new
            //{
            //    statusCode = data.statusCode,
            //    Data = data.Books
            //});
        }






    }
}

