using System.Web.Mvc;

namespace HelperMethods.Infrastructure {
    public static class CustomHelpers {

        public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] list) {
            TagBuilder tag = new TagBuilder("ul");
            foreach(string li in list) {
                TagBuilder itemTag = new TagBuilder("li");
                itemTag.SetInnerText(li);
                tag.InnerHtml += itemTag.ToString();
            }

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString DisplayMessage(this HtmlHelper html, string msg) =>
            new MvcHtmlString("This is the message: <p>" + html.Encode(msg) + "</p>");
    }
}