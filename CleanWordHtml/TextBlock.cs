using System.Linq;

namespace CleanWordHtml
{
    public class TextBlock
    {
        private readonly char[] _leftBrackets;
        private readonly char[] _rightBrackets;

        public string Content { get; private set; }

        public TextBlock(
            char left,
            char right,
            params string[] content)
        {
            _leftBrackets = new[] {left};
            _rightBrackets = new[] { right };

            Content = string.Concat(content);
        }

        public TextBlock(
            params string [] content)
        {
            _leftBrackets = new[] { '<' };
            _rightBrackets = new[] { '>' };

            Content = string.Concat(content);
        }

        public bool IsBalanced()
        {
            var count = 0;

            foreach (var character in Content)
            {
                if (_leftBrackets.Contains(character)) count++;
                if (_rightBrackets.Contains(character)) count--;
            }

            return count == 0;
        }

        public void AppendContent(
            params string[] content)
        {
            Content += string.Concat(content);
        }

        public void Clear()
        {
            Content = null;
        }

        public override string ToString()
        {
            return Content;
        }

        //public override string ToString()
        //{
        //    var c = Content;

        //    Content = null;

        //    return c;
        //}
    }
}
