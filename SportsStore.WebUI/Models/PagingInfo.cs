using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Models
{
    //ref: View Model (not Domain model)
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

        public HtmlString Tst()
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tag = new TagBuilder("label");
            tag.InnerHtml = "use Html helper, please (not this way) thanks!";
            result.Append(tag.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}