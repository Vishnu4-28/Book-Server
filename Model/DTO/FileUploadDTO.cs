using System.ComponentModel.DataAnnotations;

namespace E_commerce.Server.Model.DTO
{
    public class FileUploadDTO
    {
        [Required]
        public IFormFile MyImage { get; set; }
        public int Book_id { get; set; }
        public string ImageCaption { get; set; }
        public string ImageDescription { get; set; }
    }
}
