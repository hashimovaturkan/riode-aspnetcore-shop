using Riode_WebUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Riode_WebUI.AppCode.Extensions
{
    static public partial class Extension
    {

        public static string PlainText(this string text)
        {
            return Regex.Replace(text, @"<[^>]*>", "");
        }
    }


}