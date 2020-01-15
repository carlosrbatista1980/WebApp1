using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Models
{
    public class VideoViewModel
    {
        public string Link { get; set; }
        public string VideoSource { get; set; }
        public string VideoVisibilityy { get; set; } = "visiblevm";

        public VideoViewModel()
        {
        }
    }
}
