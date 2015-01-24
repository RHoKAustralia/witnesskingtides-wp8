using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KingTides.Core.Api.Models
{
    public class Photo
    {
        [JsonProperty(PropertyName = "user")]
        public PhotoUser User { get; set; }

        [JsonProperty(PropertyName = "flickrUrl")]
        public string FlickrUrl { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public decimal Longitude { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty(PropertyName = "flickrId")]
        public string FlickrId { get; set; }

        [JsonProperty(PropertyName = "submitted")]
        public DateTime Submitted { get; set; }

        [JsonProperty(PropertyName = "uploadStatus")]
        public string UploadStatus { get; set; }
    }

    public class PhotoUser
    {
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
    }

    
}
