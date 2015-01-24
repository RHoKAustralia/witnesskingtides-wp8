using Newtonsoft.Json;

namespace KingTides.Core.Api.Models
{
    public class UploadPhoto
    {
        [JsonProperty(PropertyName = "fileId")]
        public string FileId { get; set; }

        [JsonProperty(PropertyName = "Photo")]
        public string Photo { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty(PropertyName = "Longitude")]
        public decimal Longitude { get; set; }
    }

    public class UploadPhotoResponse
    {
        [JsonProperty(PropertyName = "photoId")]
        public string PhotoId { get; set; }
    }
}