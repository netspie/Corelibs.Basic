using System;

namespace Common.Basic.Collections
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string WhitespaceIfNullOrEmpty(this string str)
        {
            if (str.IsNullOrEmpty())
                return " ";

            return str;
        }

        public static bool Substring(this string str, char firstCharacter, char secondCharacter, out string result) =>
            str.Substring(firstCharacter, secondCharacter, out result, out int firstIndex, out int secondIndex);

        public static bool Substring(
            this string str, char firstCharacter, char secondCharacter, out string result, out int firstIndex, out int secondIndex)
        {
            result = null;
            firstIndex = -1;
            secondIndex = -1;

            if (str.IsNullOrEmpty())
                return false;

            firstIndex = str.IndexOf(firstCharacter);
            secondIndex = str.IndexOf(secondCharacter);
            if (firstIndex < 0 || secondIndex < 0)
                return false;

            if (firstIndex >= secondIndex)
                return false;

            result = str.Substring(firstIndex, secondIndex - firstIndex);
            if (result.IsNullOrEmpty())
                return false;

            return true;
        }

        public static bool SubstringAfter(this string str, char character, out string result)
        {
            result = null;
            int i = str.IndexOf(character);
            if (i < 0)
                return false;

            result = str.Substring(i + 1);
            if (str.IsNullOrEmpty())
                return false;

            return true;
        }

        public static bool SubstringBefore(this string str, char character, out string result)
        {
            result = null;
            int i = str.IndexOf(character);
            if (i < 0)
                return false;

            result = str.Substring(0, i);
            if (str.IsNullOrEmpty())
                return false;

            return true;
        }

        public static string SubstringBeforeOrThis(this string str, char character)
        {
            if (str.SubstringBefore(character, out var result))
                return result;

            return str;
        }
    }
}
