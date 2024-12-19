namespace ElleChristine.API.Dtos
{
    public class ShowDto : LinkedResourcesDto
    {
        public ShowDto()
        {
            Title = string.Empty;
            Location = string.Empty;
            Time = string.Empty;
            Description = string.Empty;
            Image = string.Empty;
            Active = true;
        }

        /// <summary>
        /// Id of show
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of show
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Location of Show
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Date of Show
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Time description of show
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Description of show
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Image of venue
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Promotion url
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Google maps url
        /// </summary>
        public string? MapUrl { get; set; }

        /// <summary>
        /// Active bool
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Date record was added
        /// </summary>
        public DateTime Added { get; set; }
    }
}