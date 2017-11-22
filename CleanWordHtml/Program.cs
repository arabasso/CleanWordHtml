using System;
using System.IO;
using System.Text;

namespace CleanWordHtml
{
    public static class Program
    {
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

            using (var sr = new StreamReader(File.OpenRead(args[0]), Encoding.Default))
            using(var sw = new StreamWriter(File.OpenWrite(args[1]), Encoding.Default))
            {
                string s;

                var c = new TextBlock();

                while ((s = sr.ReadLine()) != null)
                {
                    c.AppendContent(s);

                    if (c.IsBalanced())
                    {
                        sw.WriteLine(c.Content);

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
