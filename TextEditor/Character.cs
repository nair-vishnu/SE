using System;

namespace FlyweightDesignPattern
{
    /// <summary>
    /// Represents a character with specific style attributes in the text editor.
    /// </summary>
    public class Character
    {
        /// <summary>
        /// The style of the character.
        /// </summary>
        private readonly CharacterStyle _style;

        /// <summary>
        /// Gets the symbol of the character.
        /// </summary>
        public char Symbol { get; }

        /// <summary>
        /// Gets the position of the character in the document.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class with the specified symbol, position, and style.
        /// </summary>
        /// <param name="symbol">The symbol of the character.</param>
        /// <param name="position">The position where the character will be inserted.</param>
        /// <param name="style">The style of the character.</param>
        public Character(char symbol, int position, CharacterStyle style)
        {
            Symbol = symbol;
            Position = position;
            _style = style;
        }

        /// <summary>
        /// Displays the character to the console with its style attributes.
        /// </summary>
        /// <remarks>
        /// The method sets the console foreground color according to the character's style.
        /// It simulates text alignment by padding the output based on the character's alignment style.
        /// If an alignment requires padding, the padding is calculated based on the current console width.
        /// The method also handles cases where the console width might not be available by catching an <see cref="IOException"/>.
        /// Finally, it resets the console color to the default and outputs the character along with any additional styling like underlining.
        /// </remarks>
        public void Display()
        {
            Console.ForegroundColor = _style.Color;

            try
            {
                if (_style.TextAlignment == TextAlignment.Center)
                {
                    int padding = Math.Max(0, (Console.WindowWidth / 2 - 1));
                    Console.Write(new string(' ', padding));
                }
                else if (_style.TextAlignment == TextAlignment.Right)
                {
                    int padding = Math.Max(0, (Console.WindowWidth - 2));
                    Console.Write(new string(' ', padding));
                }
            }
            catch (IOException)
            {
                Console.Write(" ");  // Fallback to a single space
            }

            Console.Write(Symbol);

            if (_style.IsUnderline)
            {
                Console.Write("_");
            }

            Console.ResetColor();

            Console.WriteLine();
            _style.DisplayStyle();
        }
    }
}
