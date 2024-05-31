﻿using Newtonsoft.Json;


namespace ElleChristine.API.Dtos
{
    public class LinkedResourcesDto
    {
        /// <summary>
        /// links to be added to resource dtos
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public List<LinkDto> Links { get; set; }

        /// <summary>
        ///  constructor
        /// </summary>
        public LinkedResourcesDto()
        {
            Links = new List<LinkDto>();
        }
    }
}
