using System.ComponentModel.DataAnnotations.Schema;

namespace ElleChristine.API.Data.Entities
{

    [Table("Photos")]
    public class Photo
    {
        public Photo()
        {
            FileName = string.Empty;
            Heading = string.Empty;
            Description = string.Empty;
            Active = true;
        }

        public int Id { get; set; }

        public string FileName { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public string Heading { get; set; }

        public string Description { get; set; }

        public DateTime PhotoDate { get; set; }

        public DateTime Added { get; set; }

        public bool Active { get; set; }
    }
}