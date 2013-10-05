using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheHiddenTruth.Library.Helpers
{
    public static class WebContentHelper
    {
        public static string HtmlHeader(double viewportWidth, double height) //adapt parametres
        {
            var head = new StringBuilder();
            head.Append("<head>");
            //head.Append("<meta name=\"viewport\" content=\"initial-scale=1, maximum-scale=1, user-scalable=0\"/>");
            //head.Append("<script type=\"text/javascript\">" +
            //    "document.documentElement.style.msScrollTranslation = 'vertical-to-horizontal';" +
            //    "</script>"); //horizontal scrolling
            //head.Append("<meta name=\"viewport\" content=\"width=720px\">");
            head.Append("<style type=\"text/css\">");
            //head.Append("html { -ms-text-size-adjust:150%;}");
            head.Append(string.Format("body {{ line-height: 1.4; text-align: left; font-family:'Segoe UI'; font-size:26px; padding: 15px; }}" +
                                      "img {{ text-align: center; padding: 15px; }}"));
            head.Append("</style>");

            // head.Append(NotifyScript);
            head.Append("</head>");
            return head.ToString();
        }
        public static string WrapHtml(string htmlSubString, double viewportWidth, double height)
        {
            var html = new StringBuilder();
            html.Append("<html>");
            html.Append(HtmlHeader(viewportWidth, height));
            html.Append("<body><article class=\"content\">");
            html.Append(htmlSubString);
            html.Append("</article></body>");
            html.Append("</html>");
            return html.ToString();
        }
    }
}
