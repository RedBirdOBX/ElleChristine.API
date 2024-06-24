using System.ComponentModel.DataAnnotations.Schema;

namespace ElleChristine.API.Data.Entities
{

    [Table("photos")]
    public class Photo
    {
        public Photo()
        {
            FileName = string.Empty;
            Heading = string.Empty;
            Description = string.Empty;
            Active = true;
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("file_name")]
        public string FileName { get; set; }

        [Column("heading")]
        public string Heading { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("photo_date")]
        public DateTime Date { get; set; }

        [Column("added")]
        public DateTime Added { get; set; }

        [Column("active")]
        public bool Active { get; set; }
    }
}