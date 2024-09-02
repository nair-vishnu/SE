using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyweightDesignPattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace UnitTests
{
    [TestClass]
    public class FlyweightTests
    {
        /// <summary>
        /// Verifies that a CharacterStyle object is created with the correct properties.
        /// </summary>
        [TestMethod]
        public void CharacterStyle_Creation_Success()
        {
            string font = "Arial";
            int size = 12;
            bool isBold = true;
            bool isItalic = false;
            ConsoleColor color = ConsoleColor.Red;
            bool isUnderline = true;
            TextAlignment textAlignment = TextAlignment.Center;

            CharacterStyle style = new( font , size , isBold , isItalic , color , isUnderline , textAlignment );

            Assert.AreEqual( font , style.Font );
            Assert.AreEqual( size , style.Size );
            Assert.AreEqual( isBold , style.IsBold );
            Assert.AreEqual( isItalic , style.IsItalic );
            Assert.AreEqual( color , style.Color );
            Assert.AreEqual( isUnderline , style.IsUnderline );
            Assert.AreEqual( textAlignment , style.TextAlignment );
        }

        /// <summary>
        /// Verifies that the StyleFactory returns the same CharacterStyle instance for the same parameters.
        /// </summary>
        [TestMethod]
        public void StyleFactory_ReturnsSameInstanceForSameParameters()
        {
            StyleFactory factory = new();
            string font = "Arial";
            int size = 12;
            bool isBold = true;
            bool isItalic = false;
            ConsoleColor color = ConsoleColor.Red;
            bool isUnderline = true;
            TextAlignment textAlignment = TextAlignment.Center;

            CharacterStyle style1 = factory.GetStyle( font , size , isBold , isItalic , color , isUnderline , textAlignment );
            CharacterStyle style2 = factory.GetStyle( font , size , isBold , isItalic , color , isUnderline , textAlignment );

            Assert.AreSame( style1 , style2 );
        }

        /// <summary>
        /// Verifies that the StyleFactory returns different CharacterStyle instances for different parameters.
        /// </summary>
        [TestMethod]
        public void StyleFactory_ReturnsDifferentInstancesForDifferentParameters()
        {
            StyleFactory factory = new();
            string font = "Arial";
            int size = 12;
            bool isBold = true;
            bool isItalic = false;
            ConsoleColor color = ConsoleColor.Red;
            bool isUnderline = true;
            TextAlignment textAlignment = TextAlignment.Center;

            CharacterStyle style1 = factory.GetStyle( font , size , isBold , isItalic , color , isUnderline , textAlignment );
            CharacterStyle style2 = factory.GetStyle( font , size + 2 , isBold , isItalic , color , isUnderline , textAlignment );

            Assert.AreNotSame( style1 , style2 );
        }

        /// <summary>
        /// Verifies that a Character object is created with the correct properties and style.
        /// </summary>
        [TestMethod]
        public void Character_Creation_Success()
        {
            char symbol = 'A';
            int position = 0;
            CharacterStyle style = new( "Arial" , 12 , true , false , ConsoleColor.Red , true , TextAlignment.Left );

            Character character = new( symbol , position , style );

            Assert.AreEqual( symbol , character.Symbol );
            Assert.AreEqual( position , character.Position );

            FieldInfo styleField = typeof( Character ).GetField( "_style" , BindingFlags.NonPublic | BindingFlags.Instance );
            CharacterStyle retrievedStyle = styleField.GetValue( character ) as CharacterStyle;

            Assert.IsNotNull( retrievedStyle );
            Assert.AreSame( style , retrievedStyle );
        }

        /// <summary>
        /// Retrieves the list of characters from the document field of the TextEditor using reflection.
        /// </summary>
        /// <param name="editor">The TextEditor instance to retrieve the document from.</param>
        /// <returns>A list of Character objects from the TextEditor document.</returns>
        private List<Character> GetDocument( TextEditor editor )
        {
            FieldInfo documentField = typeof( TextEditor ).GetField( "_document" , BindingFlags.NonPublic | BindingFlags.Instance );
            return documentField.GetValue( editor ) as List<Character>;
        }

        /// <summary>
        /// Verifies that inserting a character into the TextEditor adds the character to the document.
        /// </summary>
        [TestMethod]
        public void TextEditor_InsertCharacter_AddsCharacterToDocument()
        {
            TextEditor editor = new();
            string font = "Arial";
            int size = 12;
            bool isBold = true;
            bool isItalic = false;

            editor.InsertCharacter( 'A' , 0 , font , size , isBold , isItalic );

            List<Character> document = GetDocument( editor );
            Assert.AreEqual( 1 , document.Count );
            Character insertedCharacter = document[0];
            Assert.AreEqual( 'A' , insertedCharacter.Symbol );
            Assert.AreEqual( 0 , insertedCharacter.Position );
        }

        /// <summary>
        /// Verifies that inserting a character into the TextEditor updates the document correctly and outputs the expected formatted text.
        /// </summary>
        [TestMethod]
        public void TextEditor_InsertCharacter_UpdatesDocument()
        {
            TextEditor editor = new();
            string font = "Arial";
            int size = 12;
            bool isBold = true;
            bool isItalic = false;
            ConsoleColor color = ConsoleColor.Red;
            bool isUnderline = true;
            TextAlignment textAlignment = TextAlignment.Left;

            editor.InsertCharacter( 'A' , 0 , font , size , isBold , isItalic , color , isUnderline , textAlignment );

            using StringWriter sw = new();
            TextWriter originalOut = Console.Out;

            try
            {
                Console.SetOut( sw );
                editor.DisplayDocument();

                string result = sw.ToString().Trim();

                string expectedSymbol = "A";
                string expectedFont = $"Font: {font}";
                string expectedSize = $"Size: {size}";
                string expectedBold = $"Bold: {isBold}";
                string expectedItalic = $"Italic: {isItalic}";
                string expectedColor = $"Color: {color}";
                string expectedUnderline = $"Underline: {isUnderline}";
                string expectedAlignment = $"Text Alignment: {textAlignment}";
                string expectedSeparator = "******";

                Assert.IsTrue( result.Contains( expectedSymbol ) );
                Assert.IsTrue( result.Contains( expectedFont ) );
                Assert.IsTrue( result.Contains( expectedSize ) );
                Assert.IsTrue( result.Contains( expectedBold ) );
                Assert.IsTrue( result.Contains( expectedItalic ) );
                Assert.IsTrue( result.Contains( expectedColor ) );
                Assert.IsTrue( result.Contains( expectedUnderline ) );
                Assert.IsTrue( result.Contains( expectedAlignment ) );
                Assert.IsTrue( result.Contains( expectedSeparator ) );
            }
            finally
            {
                Console.SetOut( originalOut );
            }
        }

        /// <summary>
        /// Verifies that inserting characters with different styles into the TextEditor outputs the correct formatted text for each character.
        /// </summary>
        [TestMethod]
        public void TextEditor_InsertCharacterWithDifferentStyles()
        {
            TextEditor editor = new();

            editor.InsertCharacter( 'A' , 0 , "Arial" , 12 , true , false , ConsoleColor.Red , false , TextAlignment.Left );
            editor.InsertCharacter( 'B' , 1 , "Times New Roman" , 14 , false , true , ConsoleColor.Blue , true , TextAlignment.Right );

            using StringWriter sw = new();
            TextWriter originalOut = Console.Out;

            try
            {
                Console.SetOut( sw );
                editor.DisplayDocument();

                string result = sw.ToString().Trim();

                StringAssert.Contains( result , "A" );
                StringAssert.Contains( result , "Arial" );
                StringAssert.Contains( result , "12" );

                StringAssert.Contains( result , "B" );
                StringAssert.Contains( result , "Times New Roman" );
                StringAssert.Contains( result , "14" );
            }
            finally
            {
                Console.SetOut( originalOut );
            }
        }

        /// <summary>
        /// Verifies that inserting multiple characters into the TextEditor updates the document correctly and outputs the expected formatted text for each character.
        /// </summary>
        [TestMethod]
        public void TextEditor_InsertMultipleCharacters()
        {
            TextEditor editor = new();

            editor.InsertCharacter( 'A' , 0 , "Arial" , 12 , true , false , ConsoleColor.Red , false , TextAlignment.Left );
            editor.InsertCharacter( 'B' , 1 , "Arial" , 12 , true , false , ConsoleColor.Red , false , TextAlignment.Center );
            editor.InsertCharacter( 'C' , 2 , "Times New Roman" , 14 , false , true , ConsoleColor.Blue , true , TextAlignment.Right );

            using StringWriter sw = new();
            TextWriter originalOut = Console.Out;

            try
            {
                Console.SetOut( sw );
                editor.DisplayDocument();

                string result = sw.ToString().Trim();

                Assert.IsTrue( result.Contains( "A" ) );
                Assert.IsTrue( result.Contains( "B" ) );
                Assert.IsTrue( result.Contains( "C" ) );
            }
            finally
            {
                Console.SetOut( originalOut );
            }
        }

        /// <summary>
        /// Verifies that the TextEditor document is empty when initialized.
        /// </summary>
        [TestMethod]
        public void TextEditor_InitialDocumentIsEmpty()
        {
            TextEditor editor = new();

            using StringWriter sw = new();
            TextWriter originalOut = Console.Out;

            try
            {
                Console.SetOut( sw );
                editor.DisplayDocument();

                string result = sw.ToString().Trim();

                Assert.AreEqual( string.Empty , result );
            }
            finally
            {
                Console.SetOut( originalOut );
            }
        }
    }
}
