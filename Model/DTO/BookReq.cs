using System.ComponentModel.DataAnnotations;

namespace E_commerce.Server.Model.DTO
{
    public class BookReq
    {
        public  string? Title { get; set; }

        
        public  string? Author { get; set; }

        public int ISBN { get; set; }

        public int Quantity { get; set; }

        //public int BookId { get; set; }
    }
}
