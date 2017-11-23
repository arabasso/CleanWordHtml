using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CleanWordHtml.Test
{
    [TestFixture]
    public class TagTest
    {
        private List<string> _textBlock;

        private readonly string[] _tags =
        {
            "<p class=MsoNormal align=center style=\'text-align:center\'>",
            "<span style=\'font-size:10.0pt;font-family:\"Arial\",sans-serif\'>",
            "44,00",
            "</span>",
            "</p>"
        };

        [SetUp]
        public void Initialization()
        {
            _textBlock = new TextBlock('<', '>', _tags).Split();
        }

        [TestCase("<html>")]
        public void Is_case_not_valid(
            string tag)
        {
            Assert.That(Tag.IsValid(tag), Is.False);
        }

        [Test]
        public void Is_valid()
        {
            var tag = _textBlock.First();

            Assert.That(Tag.IsValid(tag), Is.True);
        }

        [Test]
        public void Not_is_valid()
        {
            var tag = _textBlock.Last();

            Assert.That(Tag.IsValid(tag), Is.False);
        }

        [Test]
        [ExpectedException(typeof(TagException))]
        public void Not_valid_throws_exception()
        {
            var tag = _textBlock.Last();

            // ReSharper disable once ObjectCreationAsStatement
            new Tag(tag);
        }

        [TestCase("<p class=MsoNormal align=center style=\'text-align:center\'>",
            ExpectedResult = "<p class=\"MsoNormal\" align=\"center\" style=\"text-align:center;\">")]
        [TestCase("<span style=\'font-size:10.0pt;font-family:\"Arial\",sans-serif\'>",
            ExpectedResult = "<span style=\"font-size:10.0pt;font-family:\'Arial\',sans-serif;\">")]
        [TestCase("<p style=\'\'>",
            ExpectedResult = "<p>")]
        public string To_string(
            string content)
        {
            var tag = new Tag(content);

            return tag.ToString();
        }

        [TestCase("<p class=MsoNormal>",
            ExpectedResult = "<p>")]
        public string To_string_remove(
            string content)
        {
            var tag = new Tag(content);

            tag.Attributes.RemoveAll(r => r.Name == "class");

            return tag.ToString();
        }
    }
}
