using FluentValidation;

namespace BookStore.BooksOperations.DeleteBook;

public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
    }
}
