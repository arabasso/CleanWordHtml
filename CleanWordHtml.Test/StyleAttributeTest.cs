using NUnit.Framework;

namespace CleanWordHtml.Test
{
    [TestFixture]
    public class StyleAttributeTest
    {
        [Test]
        public void Is_valid()
        {
            var style = new StyleAttribute("style", "text-align : center");

            Assert.That(style.Value, Is.EqualTo("text-align:center;"));
        }

        [Test]
        public void No_value()
        {
            var style = new StyleAttribute("style", "");

            Assert.That(style.Value, Is.EqualTo(""));
        }
    }
}
