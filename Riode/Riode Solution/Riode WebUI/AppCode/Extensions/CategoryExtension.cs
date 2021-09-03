﻿using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Extentions
{
    static public partial class Extension
    {
        public static string GetCategoriesRow(this List<Category> categories)
        {
            if (categories == null || !categories.Any())
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("<ul class='widget-body filter-items search-ul'>");

            foreach (var category in categories)
            {
                GetChildrenRow(category);
            }
            sb.Append("</ul>");

            return sb.ToString();

            void GetChildrenRow(Category category)
            {
                sb.Append("<li>");
                sb.Append($"<a href='#'> {category.Name} </a>");

                if (category.Children != null && category.Children.Any())
                {
                    sb.Append("<ul>");

                    foreach (var item in category.Children)
                    {
                        GetChildrenRow(item);

                    }

                    sb.Append("</ul>");
                }
                sb.Append("</li>");
            }
        }
    }


}