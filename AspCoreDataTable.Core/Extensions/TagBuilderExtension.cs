using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;

namespace AspCoreDataTable.Core.Extensions
{
    public static class TagBuilderExtension
    {
        public static string ConvertHtmlString(this TagBuilder tagBuilder)
        {
            using (var writer = new System.IO.StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);

                return System.Web.HttpUtility.HtmlDecode(writer.ToString());
            }
        }
    }
}
