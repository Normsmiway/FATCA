using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fatca.Core.Extensions
{
    public static class StringExtension
    {
        public static string ToTimedId(this string value, string dateTimeString = "")
        {
            if (string.IsNullOrWhiteSpace(dateTimeString))
            {
                dateTimeString = string.Concat(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
                    .Where(c => Char.IsLetterOrDigit(c)).ToArray());
            }
            else
            {
                dateTimeString = string.Concat(dateTimeString.Where(c => Char.IsLetterOrDigit(c)).ToArray());
            }
            return $"{string.Concat($"{value}-", dateTimeString)}";
        }
    }
}
