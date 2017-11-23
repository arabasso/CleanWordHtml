using NUnit.Framework;

namespace CleanWordHtml.Test
{
    [TestFixture]
    public class StyleTest
    {
        private readonly string[] _tags =
        {
            "text-align:center",
            "font-size:10.0pt;font-family:\"Arial\",sans-serif",
        };

        [Test]
        public void Parse_styles()
        {
            Assert.That(Style.Parse(_tags[0]), Has.Count.EqualTo(1));
        }

        [Test]
        public void Is_valid()
        {
            var style = new Style("text-align : center");

            Assert.That(style.Property, Is.EqualTo("text-align"));
            Assert.That(style.Value, Is.EqualTo("center"));
        }
    }
}
