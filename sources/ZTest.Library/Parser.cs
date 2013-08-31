namespace ZTest.Library
{
    using System;

    /// <summary>
    /// Parser.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parse string to long value.
        /// </summary>
        /// <param name="s">Input value.</param>
        /// <returns>Parsed long value.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="s"/> is null.</exception>
        /// <exception cref="FormatException">If input string <paramref name="s"/> is invalid.</exception>
        public static long StringToLong(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (s.Length == 0)
            {
                throw new ArgumentException(Strings.ErrMsg_EmptyString, "s");
            }

            long result = 0;
            bool isNegative = false;

            // Input string can be a negative number
            int index = 0;
            if (s[index] == '-')
            {
                index++;
                isNegative = true;

                // In case if input string is just one symbol '-'
                if (s.Length == 1)
                {
                    throw new FormatException(Strings.ErrMsg_InvalidInputString);
                }
            }

            // By default CLR does not check if overflow can happen 
            // http://msdn.microsoft.com/en-us/library/74b4xzyw.aspx
            checked
            {
                while (index < s.Length)
                {
                    // 0x30 = '0', ... , 0x39 = '9'
                    long d = (s[index] - 0x30);
                    if (d < 0 || d > 9)
                    {
                        throw new FormatException(Strings.ErrMsg_InvalidInputString);
                    }

                    result *= 10;
                    if (isNegative)
                    {
                        result -= d;
                    }
                    else
                    {
                        result += d;
                    }

                    index++;
                }
            }

            return result;
        }
    }
}
