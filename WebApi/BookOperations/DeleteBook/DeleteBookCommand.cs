using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("The book you are trying to delete was not found.");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}