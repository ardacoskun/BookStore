using FluentValidation;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailValidator:AbstractValidator<GetGenreDetailQuery>
{
    public GetGenreDetailValidator()
    {
        RuleFor(query => query.GenreId).GreaterThan(0);
    }
}
