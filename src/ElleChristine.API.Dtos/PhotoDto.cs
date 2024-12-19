namespace ElleChristine.API.Dtos
{
    public class PhotoDto : LinkedResourcesDto
    {
        public PhotoDto()
        {
            FileName = string.Empty;
            Heading = string.Empty;
            Description = string.Empty;
            Active = true;
        }

        /// <summary>
        /// Id of photo
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Img file name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Img Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Img Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Heading for photo
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Description of photo
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date photo was taken
        /// </summary>
        public DateTime PhotoDate { get; set; }

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