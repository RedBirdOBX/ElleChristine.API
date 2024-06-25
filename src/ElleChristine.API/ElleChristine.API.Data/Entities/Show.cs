using System.ComponentModel.DataAnnotations.Schema;

namespace ElleChristine.API.Data.Entities
{

    [Table("shows")]
    public class Show
    {
        public Show()
        {
            Title = string.Empty;
            Location = string.Empty;
            Time = string.Empty;
            Description = string.Empty;
            Image = string.Empty;
            Active = true;
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("time")]
        public string Time { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("url")]
        public string? Url { get; set; }

        [Column("mapurl")]
        public string? MapUrl { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        [Column("added")]
        public DateTime Added { get; set; }
    }
}