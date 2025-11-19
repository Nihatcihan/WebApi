

using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookId;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }


    //    private static List<Book> BookList = new List<Book>
    //    {
    //        new Book { Id = 1, Title = "Book One", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2020, 1, 1) },
    //        new Book { Id = 2, Title = "Book Two", GenreId = 2, PageCount = 300, PublishDate = new DateTime(2021, 2, 2) },
    //        new Book { Id = 3, Title = "Book Three", GenreId = 1, PageCount = 150, PublishDate = new DateTime(2019, 3, 3) }
    //    };

       [HttpGet]
         public IActionResult GetBooks()
         {
            //   var booklist = _context.Books.OrderBy(x => x.Id).ToList();
            //   return booklist; // view model kullandığımız için artık bu şekilde kullanmıyoruz
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
         }
         

         
         [HttpGet("{id}")]
         public IActionResult GetById(int id)
         {
            // var book = _context.Books.Where (book => book.Id == id).SingleOrDefault();
            // return book;
            BookViewModel result;
            try
            {
               GetBookIdQuery query = new GetBookIdQuery(_context);
               query.BookId = id; 
               result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           return Ok(result);
         }

         [HttpPost]
            public IActionResult AddBook([FromBody] CreateBookCommand.CreateBookModel newBook)
            {
            //     var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
               
            //    if (book is not null)
            //        return BadRequest("Book with the same title already exists.");
            
            //     _context.Books.Add(newBook);
            //     _context.SaveChanges();
            //     return Ok(); // view model öncesi böyle
                CreateBookCommand command = new CreateBookCommand(_context);
                try
                {
                    command.Model = newBook;
                    command.Handle();
                }
                catch (Exception ex )
                {
                    return BadRequest(ex.Message);
                }
                return Ok();


            }
        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] 
       UpdateBookModel updatedBook)
        {
            // var book = _context.Books.SingleOrDefault(x => x.Id == id);
            // if (book is null)
            //     return BadRequest();

            // book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            // book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            // book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            // book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            // _context.SaveChanges();
            // return Ok();
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                
                command.Model = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            // var book = _context.Books.SingleOrDefault(x => x.Id == id);
            // if (book is null)
            //     return BadRequest();

            // _context.Books.Remove(book);
            // _context.SaveChanges();
            // return Ok();
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
    
   
}