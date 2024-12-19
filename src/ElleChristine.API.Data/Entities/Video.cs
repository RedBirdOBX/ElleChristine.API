using System.ComponentModel.DataAnnotations.Schema;

namespace ElleChristine.API.Data.Entities
{

    [Table("Videos")]
    public class Video
    {
        public Video()
        {
            Html = string.Empty;
            Description = string.Empty;
            Active = true;
        }

        public int Id { get; set; }

        public string Html { get; set; }

        public string Description { get; set; }

        public DateTime Added { get; set; }

        public bool Active { get; set; }
    }
}