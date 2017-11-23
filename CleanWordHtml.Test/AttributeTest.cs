using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CleanWordHtml.Test
{
    [TestFixture]
    public class AttributeTest
    {
        private readonly string[] _tags =
        {
            "<p class=MsoNormal align=center style=\'text-align:center\'>",
            "<span style=\'font-size:10.0pt;font-family:\"Arial\",sans-serif\'>",
        };

        [Test]
        public void To_string()
        {
            var attr = new Attribute("class", "mso");

            Assert.That(attr.ToString(), Is.EqualTo("class=\"mso\""));
        }

        [Test]
        public void Parse_attributes()
        {
            Assert.That(Attribute.Parse(_tags[0]), Has.Count.EqualTo(3));
        }
    }
}
