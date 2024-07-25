using FluentValidation;

namespace BookStore.BooksOperations.GetBookDetail;

public class GetBookDetailQueryValidator:AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        RuleFor(query => query.BookId).GreaterThan(0);
    }
}
