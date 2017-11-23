using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanWordHtml
{
    public class Style
    {
        public string Property { get; set; }
        public string Value { get; set; }

        public Style(
            string content)
        {
            var s = content.Split(':');

            Property = s[0].Trim();
            Value = s[1].Trim();
        }

        public override string ToString()
        {
            return $"{Property}:{Value}";
        }

        public static List<Style> Parse(
            string value)
        {
            return value.Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => new Style(s))
                .ToList();
        }
    }
}