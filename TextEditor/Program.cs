using System;

namespace FlyweightDesignPattern
{
    public class Program
    {
        public static void Main()
        {
            var editor = new TextEditor();

            editor.InsertCharacter('A', 0, "Arial", 12, true, false, ConsoleColor.Red, true, TextAlignment.Left);
            editor.InsertCharacter('B', 1, "Roboto", 12, true, false, ConsoleColor.Green, false, TextAlignment.Center);
            editor.InsertCharacter('C', 2, "Times New Roman", 14, false, true, ConsoleColor.Blue, true, TextAlignment.Right);

            editor.DisplayDocument();

            Console.ReadKey();
        }
    }
}
