using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using AccessToDB;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookRepository bookRepository, ILogger<BookController> logger)
        {
            this._bookRepository = bookRepository;
            this._logger = logger;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add(BookDto bookDto)
        {
            _logger.LogInformation($"Adding book begin process with book dto - {bookDto}");
            var book = new Book()
            {
                Name = bookDto.Name,
                PublishKey = bookDto.PublishKey,
                PublishingHousesType = bookDto.PublishingHousesType,
                Year = bookDto.Year,
                Country = bookDto.Country,
                City = bookDto.City,
                BookingTime = bookDto.BookingTime,
            };

            await _bookRepository.AddBook(book);
            return Created("api/Book" + book.Id, book);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBook(int id)
        {
            _logger.LogInformation($"Get book by id - {id}");

            var book = await _bookRepository.GetBook(id);
            if (book != null)
                return Ok(book);

            return NotFound("Book not found!");
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> GetBooks()
        {
            _logger.LogInformation($"Get list of books");

            var books = await _bookRepository.GetBooks();
            if (books != null)
                return Ok(books);

            return NotFound("Books not found!");
        }

        [Authorize]
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Update(int id, BookDto bookDto)
        {
            _logger.LogInformation($"Update book begin process with book dto - {bookDto}");
            var book = await _bookRepository.GetBook(id);
            if (book != null)
            {
                book.Name = bookDto.Name;
                book.PublishKey = bookDto.PublishKey;
                book.PublishingHousesType = bookDto.PublishingHousesType;
                book.Year = bookDto.Year;
                book.Country = bookDto.Country;
                book.City = bookDto.City;
                book.BookingTime = bookDto.BookingTime;
            }

            var updatedBook = await _bookRepository.UpdateBook(book);
            return Created("api/Book/" + updatedBook.Id, updatedBook);
        }
    }
}
