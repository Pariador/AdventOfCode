namespace FractralArt
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            char[,] image = InitImage();

            var rules = ReadRules("input.txt");
            rules = ExpandRules(rules);

            Transformer transformer = new Transformer(image, rules);

            transformer.Enhance(5);
            int onesCount = CountOnes(transformer.Image);
            Console.WriteLine($"Ones Count for 5 itereations: {onesCount}");

            transformer.Enhance(13);
            onesCount = CountOnes(transformer.Image);
            Console.WriteLine($"Ones Count for 18 itereations: {onesCount}");
        }

        public static Dictionary<char[,], char[,]> ReadRules(string file)
        {
            var rules = new Dictionary<char[,], char[,]>(new MatrixComparer<char>());

            using (var reader = new StreamReader(file))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    string[] data = line.Split(new[] { " => " }, StringSplitOptions.RemoveEmptyEntries);

                    rules[ToImage(data[0])] = ToImage(data[1]);
                }
            }

            return rules;
        }

        public static Dictionary<char[,], char[,]> ExpandRules(Dictionary<char[,], char[,]> @base)
        {
            var expanded = new Dictionary<char[,], char[,]>(@base, new MatrixComparer<char>());

            foreach (var pair in @base)
            {
                var variants = GetVariants(pair.Key);

                foreach (var variant in variants)
                {
                    expanded[variant] = pair.Value;
                }
            }

            return expanded;
        }

        public static List<char[,]> GetVariants(char[,] image)
        {
            var variants = new List<char[,]>();

            variants.Add(Transformer.FlipHorizontal(image));
            variants.Add(Transformer.FlipVertical(image));

            var last = image;
            for (int i = 0; i < 3; i++)
            {
                var @new = Transformer.Rotate(last);

                variants.Add(@new);
                variants.Add(Transformer.FlipHorizontal(@new));
                variants.Add(Transformer.FlipVertical(@new));

                last = @new;
            }

            return variants;
        }

        public static char[,] ToImage(string str)
        {
            string[] lines = str.Split('/');

            char[,] image = new char[lines[0].Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int c = 0; c < lines[i].Length; c++)
                {
                    image[i, c] = lines[i][c];
                }
            }

            return image;
        }

        public static char[,] InitImage()
        {
            return new char[,]
            {
                { '.', '#', '.' },
                { '.', '.', '#' },
                { '#', '#', '#' }
            };
        }

        public static int CountOnes(char[,] image)
        {
            int count = 0;

            for (int row = 0; row < image.GetLength(0); row++)
            {
                for (int col = 0; col < image.GetLength(1); col++)
                {
                    if (image[row, col] == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public static void PrintImage(char[,] image)
        {
            for (int row = 0; row < image.GetLength(0); row++)
            {
                for (int col = 0; col < image.GetLength(1); col++)
                {
                    Console.Write(image[row, col]);
                }

                Console.WriteLine();
            }

            Console.WriteLine("--------------------");
        }
    }
}