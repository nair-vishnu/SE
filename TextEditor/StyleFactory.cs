using System.Collections.Generic;

namespace FlyweightDesignPattern
{
    /// <summary>
    /// Provides a way to manage and retrieve character styles using the Flyweight pattern.
    /// </summary>
    public class StyleFactory
    {
        /// <summary>
        /// A dictionary to store and retrieve character styles based on a unique key.
        /// </summary>
        private readonly Dictionary<string, CharacterStyle> _style = new();

        /// <summary>
        /// Retrieves or creates a character style based on the specified attributes.
        /// </summary>
        /// <param name="font">The font of the character style.</param>
        /// <param name="size">The size of the character style.</param>
        /// <param name="isBold">Indicates whether the character style is bold.</param>
        /// <param name="isItalic">Indicates whether the character style is italic.</param>
        /// <param name="color">The color of the character style.</param>
        /// <param name="isUnderline">Indicates whether the character style is underlined.</param>
        /// <param name="textAlignment">The text alignment of the character style.</param>
        /// <returns>The character style that matches the specified attributes. If no matching style exists, a new style is created and added to the dictionary.</returns>
        public CharacterStyle GetStyle(
            string font,
            int size,
            bool isBold,
            bool isItalic,
            ConsoleColor color,
            bool isUnderline,
            TextAlignment textAlignment)
        {
            string key = $"{font}_{size}_{isBold}_{isItalic}_{color}_{isUnderline}_{textAlignment}";

            if (!_style.ContainsKey(key))
            {
                _style[key] = new CharacterStyle(font, size, isBold, isItalic, color, isUnderline, textAlignment);
            }

            return _style[key];
        }
    }
}
