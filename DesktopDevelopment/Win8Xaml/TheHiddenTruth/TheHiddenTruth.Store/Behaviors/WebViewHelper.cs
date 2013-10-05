using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TheHiddenTruth.Store.Behaviors
{
    public static class WebViewHelper
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
            "Html", typeof(string), typeof(WebViewHelper), new PropertyMetadata(null, OnHtmlChanged));

        public static string GetHtml(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browser = d as WebView;

            if (browser == null)
                return;

            if (e.NewValue != null)
            {
                var html = e.NewValue.ToString();

                browser.NavigateToString(html);
            }
        }
    }
}
