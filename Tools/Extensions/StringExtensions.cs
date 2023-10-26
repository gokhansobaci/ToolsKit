using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools.Extensions
{
    //Gelen string veriyi düzgün bir url yapısına çevirmeye işe yarar
    public static class StringExtensions
    {
        
        public static string ToURL(this string input)
        {
            string cleanedInput = input.ToLower()
                                       .Replace("ı", "i")
                                       .Replace("ş", "s")
                                       .Replace("ö", "o")
                                       .Replace("ü", "u")
                                       .Replace("ğ", "g");

            // Turn special characters to hyphen ("-")
            cleanedInput = Regex.Replace(cleanedInput, @"[^a-zA-Z0-9\s-]", "-").Trim();

            // Replace spaces with hyphens
            cleanedInput = Regex.Replace(cleanedInput, @"\s+", "-");

            // Remove consecutive hyphens
            cleanedInput = Regex.Replace(cleanedInput, @"-+", "-");

            // Convert to lowercase
            cleanedInput = cleanedInput.ToLower();

            return cleanedInput;
        }
    }
}
