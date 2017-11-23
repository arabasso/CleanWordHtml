using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CleanWordHtml
{
    public static class Program
    {
        private static readonly string[] Attributes =
        {
            "valign", "align", "class", "width", "height", "border", "lang", "link", "vlink"
        };

        private static readonly string[] Styles =
        {
            //"height", "width",
            "border", "border-top", "border-bottom", "border-left", "border-right", "border-collapse",
            //"padding", "padding-top", "padding-bottom", "padding-left", "padding-right",
            //"margin", "margin-top", "margin-bottom", "margin-left", "margin-right",
            "font-size", "font-family", "text-indent",
            "cellspacing", "cellpadding", 
        };

        public static void Main(
            string[] args)
        {
            var lineCount = 0;
            using (var reader = File.OpenText(args[0]))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }

            var i = 1;

            using (var sr = new StreamReader(args[0], Encoding.Default))
            using(var sw = new StreamWriter(File.Create(args[1]), Encoding.Default))
            {
                string s;

                var c = new TextBlock('<', '>');

                while ((s = sr.ReadLine()) != null)
                {
                    c.AppendContent(s);

                    if (c.IsBalanced())
                    {
                        foreach (var block in c.Split())
                        {
                            if (Tag.IsValid(block))
                            {
                                var tag = new Tag(block);

                                foreach (var styleAttribute in tag.Attributes.OfType<StyleAttribute>())
                                {
                                    styleAttribute.Styles.RemoveAll(r => Styles.Any(a => a == r.Property));
                                    styleAttribute.Styles.RemoveAll(r => r.Property == "text-align" && r.Value == "justify");
                                }

                                tag.Attributes.RemoveAll(r => Attributes.Any(a => a == r.Name));

                                sw.Write(tag.ToString());
                            }

                            else
                            {
                                sw.Write(block);
                            }
                        }

                        sw.WriteLine();
                        c.Clear();
                    }

                    else
                    {
                        c.AppendContent(" ");
                    }

                    Console.Write($"\rProgress({lineCount}): {(i++ * 100.0f / lineCount):F}%");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
        }
    }
}
