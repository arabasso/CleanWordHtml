using System.Collections.Generic;
using System.Linq;

namespace CleanWordHtml
{
    public class StyleAttribute
        : Attribute
    {
        public List<Style> Styles { get; private set; }

        public override string Value
        {
            get => Styles.Any() ? string.Join(";", Styles) + ";" : "";
            set => Styles = Style.Parse(value);
        }

        public StyleAttribute(
            string name,
            string value)
            : base(name, value)
        {
        }
    }
}
