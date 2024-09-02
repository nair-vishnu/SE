using System;
using System.Collections.Generic;

namespace FlyweightDesignPattern
{
    /// <summary>
    /// Represents a text editor that manages a collection of characters.
    /// </summary>
    public class TextEditor
    {
        /// <summary>
        /// The list of characters in the document.
        /// </summary>
        private readonly List<Character> _document = new();

        /// <summary>
        /// The factory used to create and retrieve character styles.
        /// </summary>
        private readonly StyleFactory _styleFactory = new();

        /// <summary>
        /// Inserts a character into the document with specified properties.
        /// </summary>
        /// <param name="symbol">The symbol of the character to insert.</param>
        /// <param name="position">The position where the character will be inserted.</param>
        /// <param name="font">The font of the character.</param>
        /// <param name="size">The size of the character.</param>
        /// <param name="isBold">Indicates whether the character is bold.</param>
        /// <param name="isItalic">Indicates whether the character is italic.</param>
        public void InsertCharacter(char symbol, int position, string font, int size, bool isBold, bool isItalic)
        {
            CharacterStyle style = _styleFactory.GetStyle(
                font, size, isBold, isItalic, ConsoleColor.White, false, TextAlignment.Left);

            _document.Add(new Character(symbol, position, style));
        }

        /// <summary>
        /// Inserts a character into the document with specified properties including color, underline, and text alignment.
        /// </summary>
        /// <param name="symbol">The symbol of the character to insert.</param>
        /// <param name="position">The position where the character will be inserted.</param>
        /// <param name="font">The font of the character.</param>
        /// <param name="size">The size of the character.</param>
        /// <param name="isBold">Indicates whether the character is bold.</param>
        /// <param name="isItalic">Indicates whether the character is italic.</param>
        /// <param name="color">The color of the character.</param>
        /// <param name="isUnderline">Indicates whether the character is underlined.</param>
        /// <param name="textAlignment">The text alignment of the character.</param>
        public void InsertCharacter(
            char symbol, int position, string font, int size, bool isBold, bool isItalic,
            ConsoleColor color, bool isUnderline, TextAlignment textAlignment)
        {
            CharacterStyle style = _styleFactory.GetStyle(
                font, size, isBold, isItalic, color, isUnderline, textAlignment);

            _document.Add(new Character(symbol, position, style));
        }

        /// <summary>
        /// Displays the document by printing each character and its style properties to the console.
        /// </summary>
        public void DisplayDocument()
        {
            foreach (Character character in _document)
            {
                character.Display();
                Console.WriteLine("******");
            }
        }
    }
}
