namespace StreamProcessing
{
    using System;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            var result = Solve(new StreamReader("input.txt"));
            //var result = Solve(new StringReader(Console.ReadLine()));

            Console.WriteLine($"Group Score: {result.GroupScore}");
            Console.WriteLine($"Garbage Count: {result.GarbageCount}");
        }

        public static Result Solve(TextReader reader)
        {
            Result result = new Result();

            bool ignore = false;
            bool inGarbage = false;

            int group = 0;

            while (true)
            {
                int data = reader.Read();
                if (data < 0)
                {
                    return result;
                }
                char @char = (char)data;

                if (ignore)
                {
                    ignore = false;
                    continue;
                }
                else if (@char == Symbols.Ignore)
                {
                    ignore = true;
                }
                else if (inGarbage && @char != Symbols.CloseGarbage)
                {
                    result.GarbageCount++;
                    continue;
                }
                else if (@char == Symbols.OpenGarbage)
                {
                    inGarbage = true;
                }
                else if (inGarbage && @char == Symbols.CloseGarbage)
                {
                    inGarbage = false;
                }
                else if (@char == Symbols.OpenGroup)
                {
                    group++;

                }
                else if (@char == Symbols.CloseGroup)
                {
                    result.GroupScore += group;
                    group--;
                }

            }

            return result;
        }
    }
}