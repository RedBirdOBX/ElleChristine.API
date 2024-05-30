namespace ElleChristine.API.Dtos
{
    public class ShowDto
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
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? MapUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime Added { get; set; }

    }
}