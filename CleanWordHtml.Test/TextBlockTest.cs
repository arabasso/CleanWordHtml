using System.Linq;
using NUnit.Framework;

namespace CleanWordHtml.Test
{
    [TestFixture]
    public class TextBlockTest
    {
        private readonly string[] _content1 = {
            "<td width=\"26%\" nowrap colspan=9 valign=top style=\'width:26.54%;border-top:",
            "none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;",
            "padding:0cm 5.4pt 0cm 5.4pt;height:11.25pt\'>"
        };

        private readonly string[] _content2 =
        {
            "<p class=MsoNormal align=center style=\'text-align:center\'><span",
            "style=\'font-size:10.0pt;font-family:\"Arial\",sans-serif\'>44,00</span></p>"
        };

        [Test]
        public void Append_content()
        {
            var tag = new TextBlock('<', '>', _content1[0]);

            tag.AppendContent(_content1[1]);

            Assert.That(tag.Content, Is.EqualTo(string.Concat(_content1[0], _content1[1])));
        }

        [Test]
        public void Append_content_array()
        {
            var tag = new TextBlock('<', '>');

            tag.AppendContent(_content1);

            Assert.That(tag.Content, Is.EqualTo(string.Concat(_content1)));
        }

        [Test]
        public void Not_balanced()
        {
            var tag = new TextBlock('<', '>', _content1[0]);

            Assert.That(tag.IsBalanced(), Is.False);
        }

        [Test]
        public void Is_balanced_content()
        {
            var tag = new TextBlock('<', '>', _content2[0]);

            Assert.That(tag.IsBalanced(), Is.False);
        }

        [Test]
        public void Is_balanced_appended_content()
        {
            var tag = new TextBlock('<', '>');

            foreach (var content in _content1)
            {
                tag.AppendContent(content);
            }

            Assert.That(tag.IsBalanced(), Is.True);
        }

        [Test]
        public void Split_has_one()
        {
            var tag = new TextBlock('<', '>', _content1);

            Assert.That(tag.Split(), Has.Count.EqualTo(1));
        }

        [Test]
        public void Split_has_two()
        {
            var tag = new TextBlock('<', '>', _content2);

            Assert.That(tag.Split(), Has.Count.EqualTo(5));
        }
    }
}
