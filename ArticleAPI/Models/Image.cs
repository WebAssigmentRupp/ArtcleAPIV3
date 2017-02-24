using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleAPI.Models
{
    public class Image
    {
        public short Id { get; set; }
        public string url { get; set; }
        public DateTime uploaded { get; set; }
    }
}