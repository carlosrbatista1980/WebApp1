using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Models
{
    public class VideoModel
    {
        public string Link { get; set; }
        public string VideoSource { get; set; } = "empty";

        /// <summary>
        /// <param name="visible">Default value. The element is visible</param>
        /// <param name="hidden">The element is hidden (but still takes up space, affects the style of page)</param>
        /// <param name="collapse">Only for table rows (<tr>), row groups (<tbody>), columns (<col>), column groups (<colgroup>).
        /// This value removes a row or column, but it does not affect the table layout.
        /// The space taken up by the row or column will be available for other content.
        /// If collapse is used on other elements, it renders as "hidden"</param>
        /// <param name="initial">Sets this property to its default value.</param>
        /// <param name="inherit">Inherits this property from its parent element.</param>
        /// </summary>
        public List<string> VideoVisibility { get; set; }

        public VideoModel()
        {
            VideoVisibility = new List<string>()
            {
                "visible",
                "hidden",
                "collapse",
                "initial",
                "inherit"
            };
        }
    }
}
