using System;

namespace FlyweightDesignPattern
{
    /// <summary>
    /// Represents the style of a character, including its font, size, boldness, italicization, color, underline status, and text alignment.
    /// </summary>
    public class CharacterStyle
    {
        /// <summary>
        /// Gets the font of the character style.
        /// </summary>
        public string Font { get; }

        /// <summary>
        /// Gets the size of the character style.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets a value indicating whether the character style is bold.
        /// </summary>
        public bool IsBold { get; }

        /// <summary>
        /// Gets a value indicating whether the character style is italic.
        /// </summary>
        public bool IsItalic { get; }

        /// <summary>
        /// Gets the color of the character style.
        /// </summary>
        public ConsoleColor Color { get; }

        /// <summary>
        /// Gets a value indicating whether the character style is underlined.
        /// </summary>
        public bool IsUnderline { get; }

        /// <summary>
        /// Gets the text alignment of the character style.
        /// </summary>
        public TextAlignment TextAlignment { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterStyle"/> class with specified style attributes.
        /// </summary>
        /// <param name="font">The font of the character style.</param>
        /// <param name="size">The size of the character style.</param>
        /// <param name="isBold">Indicates whether the character style is bold.</param>
        /// <param name="isItalic">Indicates whether the character style is italic.</param>
        /// <param name="color">The color of the character style.</param>
        /// <param name="isUnderline">Indicates whether the character style is underlined.</param>
        /// <param name="textAlignment">The text alignment of the character style.</param>
        public CharacterStyle(
            string font,
            int size,
            bool isBold,
            bool isItalic,
            ConsoleColor color,
            bool isUnderline,
            TextAlignment textAlignment)
        {
            Font = font;
            Size = size;
            IsBold = isBold;
            IsItalic = isItalic;
            Color = color;
            IsUnderline = isUnderline;
            TextAlignment = textAlignment;
        }

        /// <summary>
        /// Displays the style attributes of the character to the console.
        /// </summary>
        public void DisplayStyle()
        {
            Console.WriteLine($"Font: {Font}, Size: {Size}, Bold: {IsBold}, Italic: {IsItalic}, Color: {Color}, Underline: {IsUnderline}, Text Alignment: {TextAlignment}");
        }
    }

    /// <summary>
    /// Defines the alignment of text within a document or display.
    /// </summary>
    public enum TextAlignment
    {
        /// <summary>
        /// Aligns text to the left.
        /// </summary>
        Left,

        /// <summary>
        /// Centers text.
        /// </summary>
        Center,

        /// <summary>
        /// Aligns text to the right.
        /// </summary>
        Right
    }
}
