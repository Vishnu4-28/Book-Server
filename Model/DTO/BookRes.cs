namespace E_commerce.Server.Model.DTO
{
    public class BookRes
    {
        public int Book_Id { get; set; }

        public required string Title { get; set; }

        public required string Author { get; set; }

        public int ISBN { get; set; }

        public int Quantity { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string? FilePath { get; set; }
    }
}
