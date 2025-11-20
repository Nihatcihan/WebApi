

using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookId;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using AutoMapper;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using FluentValidation;
using FluentValidation.Results;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
         
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
               GetBookIdQuery query = new GetBookIdQuery(_context, _mapper);
               query.BookId = id; 
                GetBookIdQueryValidator validator = new GetBookIdQueryValidator();
                validator.ValidateAndThrow(query);
               result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           return Ok(result);
         }

         [HttpPost]
            public IActionResult AddBook([FromBody] CreateBookModel newBook)
            {
            //     var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
               
            //    if (book is not null)
            //        return BadRequest("Book with the same title already exists.");
            
            //     _context.Books.Add(newBook);
            //     _context.SaveChanges();
            //     return Ok(); // view model öncesi böyle
               CreateBookCommand command = new CreateBookCommand(_context, _mapper);
               try
               {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);     

                // if(!result.IsValid)
                // {
                //     foreach(var error in result.Errors)
                //     {
                //         Console.WriteLine("Özellik: " + error.PropertyName + " - Error Message: " + error.ErrorMessage);
                //     }
                // }
                // else
                command.Handle();

               }
               catch (Exception ex)
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
            var command = new UpdateBookCommand(_context, _mapper)
            {
                BookId = id,
                Model = updatedBook
            };
            try
            {
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
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
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
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