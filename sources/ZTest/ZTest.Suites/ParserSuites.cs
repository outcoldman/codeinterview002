namespace ZTest.Suites
{
    using System;

    using NUnit.Framework;

    using ZTest.Library;

    public class ParserSuites
    {
        [Test]
        public void StringToLong_Null_ThrowsArgumentNullException()
        {
            // Arrange
            string input = null;

            // Act
            TestDelegate act = () => Parser.StringToLong(input);

            // Assert
            Assert.Throws<ArgumentNullException>(act, "Null value should not be parsed. ArgumentNullException is expected.");
        }

        [Test]
        public void StringToLong_EmptyString_ThrowsArgumentException()
        {
            // Arrange
            string input = string.Empty;

            // Act
            TestDelegate act = () => Parser.StringToLong(input);

            // Assert
            Assert.Throws<ArgumentException>(act, "Empty String '-' should not be parsed. ArgumentException is expected.");
        }

        [Test]
        public void StringToLong_JustMinus_ThrowsFormatException()
        {
            // Arrange
            string input = "-";

            // Act
            TestDelegate act = () => Parser.StringToLong(input);

            // Assert
            Assert.Throws<FormatException>(act, "String '-' should not be parsed. FormatException is expected.");
        }

        [Test]
        public void StringToLong_CorrectInput_CorrectResult()
        {
            // Arrange
            string input = "123";

            // Act
            long result = Parser.StringToLong(input);

            // Assert
            Assert.AreEqual(123, result, "String '123' should be parsed to value 123.");
        }

        [Test]
        public void StringToLong_AllZeros_ZeroAsResult()
        {
            // Arrange
            string input = "00";

            // Act
            long result = Parser.StringToLong(input);

            // Assert
            Assert.AreEqual(0, result, "String '00' should be parsed to 0.");
        }

        [Test]
        public void StringToLong_ZeroAsFistDigit_CorrectResult()
        {
            // Arrange
            string input = "0123";

            // Act
            long result = Parser.StringToLong(input);

            // Assert
            Assert.AreEqual(123, result, "String '0123' should be parsed as simple '123' string.");
        }

        [Test]
        public void StringToLong_MaximumLongValue_ShouldBeParsedAsLong()
        {
            // Arrange
            string input = "9223372036854775807";

            // Act
            long result = Parser.StringToLong(input);

            // Assert
            Assert.AreEqual(9223372036854775807L, result, "Maximim long value '9223372036854775807' should be parsed.");
        }

        [Test]
        public void StringToLong_MinimumLongValue_ShouldBeParsedAsLong()
        {
            // Arrange
            string input = "-9223372036854775808";

            // Act
            long result = Parser.StringToLong(input);

            // Assert
            Assert.AreEqual(-9223372036854775808L, result, "Maximim long value '-9223372036854775808' should be parsed.");
        }

        [Test]
        public void StringToLong_OverMaximumLongValue_OverflowException()
        {
            // Arrange
            string input = "9223372036854775808";

            // Act
            TestDelegate act = () => Parser.StringToLong(input);

            // Assert
            Assert.Throws<OverflowException>(act, "If input string contains bigger value than Long can store method should throw OverflowException.");
        }

        [Test]
        public void StringToLong_OverMinimumLongValue_OverflowException()
        {
            // Arrange
            string input = "-9223372036854775809";

            // Act
            TestDelegate act = () => Parser.StringToLong(input);

            // Assert
            Assert.Throws<OverflowException>(act, "If input string contains smaller value than Long can store method should throw OverflowException.");
        }

        [Test]
        public void StringToLong_OverflowInputAndCodeMarkedAsUnchecked_OverflowException()
        {
            // Arrange
            string input = "9223372036854775808";

            // Act
            TestDelegate act = () =>
                {
                    unchecked
                    {
                        Parser.StringToLong(input);
                    }
                };

            // Assert
            Assert.Throws<OverflowException>(act, "If input string contains bigger value than Long can store and code marked as unchecked method should continue to throw OverflowException.");
        }

        [Test]
        public void StringToLong_HexValue_ThrowsFormatException()
        {
            // Arrange
            string input = "0x0123";

            // Act
            TestDelegate act = () => Parser.StringToLong(input);

            // Assert
            Assert.Throws<FormatException>(act, "HEX input strins are not supported.");
        }

        [Test]
        public void StringToLong_CultureSpecificInputValueWithGroupSeparators_ThrowsFormatException()
        {
            // Arrange
            string input = "123,123,000";

            // Act
            TestDelegate act = () => Parser.StringToLong(input);

            // Assert
            Assert.Throws<FormatException>(act, "Culure specific values with group separators are not supported.");
        }
    }
}
