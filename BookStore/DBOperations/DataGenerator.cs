using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations;

public class DataGenerator
{

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
        {

            if (context.Books.Any())
            {
                return;
            }

            context.Books.AddRange(
                new Book { Title = "Lean Startup", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12) },
        new Book { Title = "Herland", GenreId = 2, PageCount = 250, PublishDate = new DateTime(2010, 06, 12) },
        new Book { Title = "Dune2", GenreId = 2, PageCount = 540, PublishDate = new DateTime(2001, 12, 21) }
            );

            context.SaveChanges();




        }
    }


}