using System.ComponentModel.DataAnnotations;

namespace E_commerce.Server.Model.Entities
{
    public class Books
    {
       [Key]
       public int Book_Id { get; set; }

       public required string Title { get; set; }

       public required string Author { get; set; }

       public int ISBN { get; set; }

       public int Quantity { get; set; }

       public  bool IsDeleted { get; set; } = false;

    }
}
