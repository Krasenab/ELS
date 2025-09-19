using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ELS.Infrastrucure
{
    public static class FormatFixer
    {
        public static string RemoveTagsFromText(string text)
        {
            string removeTags = Regex.Replace(text, @"<[^>]+>", string.Empty);
            return removeTags;
        }
    }
}
