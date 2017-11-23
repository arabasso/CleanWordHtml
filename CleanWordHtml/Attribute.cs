using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CleanWordHtml
{
    public class Attribute
    {
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }

        public Attribute(
            string name,
            string value)
        {
            Name = name;
            Value = value;
        }

        public static List<Attribute> Parse(
            string attributes)
        {
            var matches = Regex.Matches(attributes, @"((?:(?!\s|=).)*)\s*?=\s*?[""']?((?:(?<="")(?:(?<=\\)""|[^""])*|(?<=')(?:(?<=\\)'|[^'])*)|(?:(?!""|')(?:(?!\/>|>|\s).)+))");

            return matches.Cast<Match>()
                .Select(m =>
                {
                    var name = m.Groups[1].Value;
                    var value = m.Groups[2].Value;

                    switch (name)
                    {
                        case "style":
                            return new StyleAttribute(name, value);
                        default:
                            return new Attribute(name, value);
                    }
                })
                .ToList();
        }

        public override string ToString()
        {
            return $"{Name}=\"{Value.Replace('\"', '\'')}\"";
        }
    }
}
