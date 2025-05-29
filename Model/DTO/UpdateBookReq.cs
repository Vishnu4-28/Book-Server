namespace E_commerce.Server.Model.DTO
{
    public class UpdateBookReq
    {
        public  string?  Title { get; set; }

        public  string?  Author { get; set; }

        public int? ISBN { get; set; }

        public int? Quantity { get; set; }
    }
}
