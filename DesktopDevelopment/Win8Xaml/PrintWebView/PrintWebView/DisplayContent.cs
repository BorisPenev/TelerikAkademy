using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintWebView
{
    internal enum DisplayContent : int
    {
        /// <summary>
        /// Show only text
        /// </summary>
        Text = 1,

        /// <summary>
        /// Show only images
        /// </summary>
        Images = 2,

        /// <summary>
        /// Show a combination of images and text
        /// </summary>
        TextAndImages = 3
    }
}
