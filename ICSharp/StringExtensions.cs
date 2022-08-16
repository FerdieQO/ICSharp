using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FSX
{
    public static class StringExtensions
    {
        private static readonly string _COMMA = ",";
        private static readonly string _DOT = ".";
        private static readonly string _UNDERSCORE = "_";
        private static readonly string _SPACE = " ";
        private static readonly string _LPARENTHESIS = "(";
        private static readonly string _RPARENTHESIS = ")";

        /// <summary>
        /// Replaces the comma with dot.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceCommaWithDot(this String str)
        {
            return str.Replace(_COMMA, _DOT);
        }

        /// <summary>
        /// Replaces the underscore with space.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceUnderscoreWithSpace(this String str)
        {
            return str.Replace(_UNDERSCORE, _SPACE);
        }

        /// <summary>
        /// Replaces the spaces with underscores.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceSpaceWithUnderscore(this String str)
        {
            return str.Replace(_SPACE, _UNDERSCORE);
        }

        /// <summary>
        /// Removes the spaces from the string (replaces them with
        /// string.empty).
        /// </summary>
        /// <param name="str"></param>
        /// <example>
        /// string exampleString = "This Is A   Test";
        /// exampleString = exampleString.RemoveSpaces();
        /// Debug.Log(exampleString); // Result: "ThisIsATest"
        /// </example>
        /// <returns></returns>
        public static string RemoveSpaces(this String str)
        {
            return str.Replace(_SPACE, string.Empty);
        }

        /// <summary>
        /// Removes the dots and commas from the string (replaces them with
        /// string.Empty).
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveDotsAndCommas(this String str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return str.Replace(_COMMA, string.Empty).Replace(_DOT, string.Empty);
        }

        /// <summary>
        /// Removes the parenthesis from the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string RemoveParenthesis(this String str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return str.Replace(_LPARENTHESIS, string.Empty).Replace(_RPARENTHESIS, string.Empty);
        }

        /// <summary>
        /// Removes all characters after the first occurrence of the provides character argument.
        /// See example.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        /// <example>
        /// <c>
        /// string fullNameOfItem = "Item 2(priced at $300)";
        /// 
        /// // Remove everything after the first opening parenthesis
        /// string regularName = fullNameOfItem.RemoveEverythingAfterFirstInstanceOfCharacter('(');
        /// 
        /// Debug.Log(regularName); // "Item 2"
        /// </c>
        /// </example>
        public static string RemoveEverythingAfterFirstInstanceOfCharacter(this String str, char character)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            string result = str;
            if (result.Contains(character.ToString()))
            {
                int characterIndex = result.IndexOf(character);
                result = result.Remove(characterIndex);
            }
            return result;
        }

        /// <summary>
        /// Removes all characters after the last occurrence of the provides character argument.
        /// See example.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        /// <example>
        /// <c>
        /// string fullNameOfItem = "Item 2(2.75KG)(priced at $300)";
        /// 
        /// // Remove everything after the first opening parenthesis
        /// string regularName = fullNameOfItem.RemoveEverythingAfterLastInstanceOfCharacter('(');
        /// 
        /// Debug.Log(regularName); // "Item 2(2.75KG)"
        /// </c>
        /// </example>
        public static string RemoveEverythingAfterLastInstanceOfCharacter(this String str, char character)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            string result = str;
            if (result.Contains(character.ToString()))
            {
                int characterIndex = result.LastIndexOf(character);
                result = result.Remove(characterIndex);
            }
            return result;
        }

        /// <summary>
        /// Cleans the string of non digits.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string CleanStringOfNonDigits(this String str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return new string(str.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Counts the nr of occurrences of the parameter in the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="substringToCheck">The substring to check.</param>
        /// <returns>System.Int32.</returns>
        public static int CountNrOfOccurrences(this String str, string substringToCheck)
        {
            int result = 0;
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(substringToCheck)) return result;
            substringToCheck = substringToCheck.Replace(@"[", @"\[");
            result = Regex.Matches(str, @substringToCheck)?.Count ?? 0;
            return result;
        }

        /// <summary>
        /// Takes a CamelCase string and adds spaces before capitalised letters.
        /// </summary>
        /// <example>
        /// <code>
        /// string camelCaseString = "ThisIsAString";
        /// string spacedString = camelCaseString.AddSpacesBeforeCapitalLetters();
        /// 
        /// DebugHelpers.Log(spacedString); // result: This Is A String
        /// </code>
        /// </example>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddSpacesBeforeCapitalLetters(this String str)
        {
            string text = str;
            if (string.IsNullOrEmpty(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        public static string LowerAllExceptFirstCapital(this String str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string result = str.ToLower();
                result = result.Remove(0, 1).Insert(0, Char.ToUpper(result[0]).ToString());
                return result;
            }
            return str;
        }

        public static string ToUpperFirstLetter(this String str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return str.Remove(0, 1).Insert(0, Char.ToUpper(str[0]).ToString());
        }

        public static bool Contains(this String str, string stringToCheck, StringComparison comparison)
        {
            if (str == null) return false;
            return str.IndexOf(stringToCheck, comparison) >= 0;
        }

        public static bool ContainsDigit(this String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool OnlyContainsDigit(this String str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsDigit(str[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool OnlyContainsLetters(this String str,
            bool spacesAllowed = false)
        {
            if (string.IsNullOrEmpty(str)) return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetter(str[i]))
                {
                    if (char.IsWhiteSpace(str[i]) && !spacesAllowed)
                    {
                        return false;
                    }
                    else if (!char.IsWhiteSpace(str[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool ContainsSpecialCharacter(this String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsLetterOrDigit(str[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsUppercase(this String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsLowerCase(this String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsLower(str[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
