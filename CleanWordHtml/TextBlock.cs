using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public List<string> Split()
        {
            string s;

            var count = 0;

            var list = new List<string>();

            var sb = new StringBuilder();

            foreach (var c in Content)
            {
                if (_leftBrackets.Contains(c))
                {
                    count++;

                    if (count == 1)
                    {
                        s = sb.ToString();

                        sb.Clear();

                        if (!string.IsNullOrEmpty(s))
                        {
                            list.Add(s);
                        }
                    }

                    sb.Append(c);
                }

                else if (_rightBrackets.Contains(c))
                {
                    sb.Append(c);

                    count--;

                    if (count == 0)
                    {
                        s = sb.ToString();

                        sb.Clear();

                        if (!string.IsNullOrEmpty(s))
                        {
                            list.Add(s);
                        }
                    }
                }

                else
                {
                    sb.Append(c);
                }
            }

            s = sb.ToString();

            if (!string.IsNullOrEmpty(s))
            {
                list.Add(s);
            }

            return list;
        }
    }
}
