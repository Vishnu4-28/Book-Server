using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Server.Model.Entities
{
    public class BookImg
    {
        [Key]
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? FileType { get; set; }
        public long FileSize { get; set; }
        public string? ImageCaption { get; set; }
        public string? ImageDescription { get; set; }
        public DateTime UploadDate { get; set; }
        public int Book_id { get; set; }

        [ForeignKey("Book_id")]
        public virtual Books Books { get; set; }

    }
}
