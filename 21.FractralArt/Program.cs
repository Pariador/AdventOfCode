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
            var rules = ReadRules("test.txt");

            PrintImage(image);

            Transformations.FlipHorizontal(image);
            PrintImage(image);

            Transformations.FlipVertical(image);
            PrintImage(image);
        }

        public static Dictionary<char[,], char[,]> ReadRules(string file)
        {
            var rules = new Dictionary<char[,], char[,]>();

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