using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace CleanWordHtml
{
    public class Tag
    {
        public string Name { get; private set; }
        public List<Attribute> Attributes { get; private set; }
        public List<StyleAttribute> Styles { get; private set; }

        public Tag(
            string content)
        {
            var match = MatchTag(content);

            if (!match.Success) throw new TagException("Unsupported tag");

            Name = match.Groups["tag"].Value;

            Attributes = Attribute.Parse(match.Groups["attr"].Value);
        }

        private static Match MatchTag(string content)
        {
            return Regex.Match(content, @"<(?<tag>\w+)\s+(?<attr>.+?)>");
        }

        public static bool IsValid(
            string content)
        {
            return MatchTag(content).Success;
        }

        public override string ToString()
        {
            var attr = string.Join(" ", Attributes.Where(w => !string.IsNullOrEmpty(w.Value)));

            return !string.IsNullOrEmpty(attr)
                ? $"<{Name} {attr}>"
                : $"<{Name}{attr}>";
        }
    }
}
