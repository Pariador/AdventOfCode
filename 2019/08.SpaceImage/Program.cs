namespace SpaceImage
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using static System.Linq.Enumerable;
    using System.Text;

    static class Program
    {
        const int Width = 25;
        const int Height = 6;

        const char WhiteChar = '#';
        const char BlackChar = ' ';

        static void Main()
        {
            int[] pixels = File.ReadAllText("input.txt")
                .ToCharArray()
                .Select(c => c - 48)
                .ToArray();

            var layers = Layer(pixels);

            int checksum = Checksum(layers);
            Console.WriteLine($"Checksum: {checksum}");

            var image = Flatten(layers);
            PrintImage(image);
        }

        static int Checksum(int[][,] layers)
        {
            var minZeroes = CountDigits(layers[0]);

            for (int i = 1; i < layers.Length; i++)
            {
                var counts = CountDigits(layers[i]);

                if (counts[0] < minZeroes[0])
                    minZeroes = counts;
            }

            return minZeroes[1] * minZeroes[2];
        }

        static int[][,] Layer(int[] pixels)
        {
            int layerLength = Width * Height;
            int layersCount = pixels.Length / layerLength;

            var layers = new int[layersCount][,];

            for (int l = 0; l < layersCount; l++)
            {
                layers[l] = new int[Height, Width];

                for (int r = 0; r < Height; r++)
                {
                    for (int c = 0; c < Width; c++)
                    {
                        int p = layerLength * l + r * Width + c;

                        layers[l][r, c] = pixels[p];
                    }
                }
            }

            return layers;
        }

        static int[,] Flatten(int[][,] layers)
        {
            var image = new int[layers[0].GetLength(0), layers[0].GetLength(1)];

            for (int l = layers.Length - 1; l >= 0; l--)
            {
                for (int r = 0; r < image.GetLength(0); r++)
                {
                    for (int c = 0; c < image.GetLength(1); c++)
                    {
                        int color = layers[l][r, c];

                        if (color != 2) image[r, c] = color;
                    }
                }
            }

            return image;
        }

        static void PrintImage(int[,] image)
        {
            var builder = new StringBuilder();

            for (int r = 0; r < image.GetLength(0); r++)
            {
                for (int c = 0; c < image.GetLength(1); c++)
                {
                    char symbol = image[r, c] == 1 ? WhiteChar : BlackChar;

                    builder.Append(symbol);
                }

                builder.AppendLine();
            }

            Console.Write(builder.ToString());
        }

        static Dictionary<int, int> CountDigits(int[,] layer)
        {
            var counts = new Dictionary<int, int>();
            for (int i = 0; i < 3; i++)
                counts[i] = 0;

            for (int r = 0; r < layer.GetLength(0); r++)
            {
                for (int c = 0; c < layer.GetLength(1); c++)
                {
                    int digit = layer[r, c];

                    counts[digit]++;
                }
            }

            return counts;
        }
    }
}