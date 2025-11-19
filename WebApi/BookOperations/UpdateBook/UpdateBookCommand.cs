using Microsoft.AspNetCore;
using WebApi.BookOperations;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("Book to be updated not found.");
            
            book.Title = Model.Title == default ? book.Title : Model.Title;
            book.GenreId = Model.GenreId == default ? book.GenreId : Model.GenreId; 
            book.PageCount = Model.PageCount == default ? book.PageCount : Model.PageCount;
            book.PublishDate = Model.PublishDate == default ? book.PublishDate : Model.PublishDate;

            _dbContext.SaveChanges();
        }
        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}