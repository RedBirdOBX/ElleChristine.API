namespace ElleChristine.API.Dtos
{
    public class VideoDto : LinkedResourcesDto
    {
        public VideoDto()
        {
            Html = string.Empty;
            Description = string.Empty;
            Active = true;
        }

        /// <summary>
        /// Id of photo
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// html to embed
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Description of video
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date record was added
        /// </summary>
        public DateTime Added { get; set; }

        /// <summary>
        /// Active bool
        /// </summary>
        public bool Active { get; set; }
    }
}