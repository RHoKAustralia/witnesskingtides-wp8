using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingTides.Core.Api.Models
{
    public class Photo
    {
        public PhotoUser user { get; set; }
        public string flickrUrl { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string flickrId { get; set; }
        public DateTime submitted { get; set; }
        public string uploadStatus { get; set; }
    }

    public class PhotoUser
    {
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
    }
}
