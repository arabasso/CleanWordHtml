using System;

namespace CleanWordHtml
{
    public class TagException
        : Exception
    {
        public TagException(
            string message)
            : base(message)
        {
        }
    }
}