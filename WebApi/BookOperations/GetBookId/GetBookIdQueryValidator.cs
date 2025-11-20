using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookId
{
    public class GetBookIdQueryValidator : AbstractValidator<GetBookIdQuery>
    {
        public GetBookIdQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}