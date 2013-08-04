using System;
using System.Text;
using ChARTServices.Models;
using System.Web.Mvc;

namespace ChARTServices.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl) {
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder liTag = new TagBuilder("li");
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                }
                liTag.InnerHtml = tag.ToString();
                builder.Append(liTag.ToString());
            }
            return MvcHtmlString.Create(builder.ToString());
        }
    }
}