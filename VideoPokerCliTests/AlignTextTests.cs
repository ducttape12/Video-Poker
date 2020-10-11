using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoPokerCli;

namespace VideoPokerCliTests
{
    [TestClass]
    public class AlignTextTests
    {
        [TestMethod]
        public void GivenSmallText_WhenCalledWithLeft_ThenReturnsTextWithLeftPadding()
        {
            // Arrange
            const string text = "Test";
            const int columns = 6;

            // Act
            var aligned = AlignText.AlignAndFit(text, Alignment.Left, columns);

            // Assert
            Assert.AreEqual("Test  ", aligned);
        }

        [TestMethod]
        public void GivenBigText_WhenCalledWithLeft_ThenReturnsSubstringOfText()
        {
            // Arrange
            const string text = "Test";
            const int columns = 2;

            // Act
            var aligned = AlignText.AlignAndFit(text, Alignment.Left, columns);

            // Assert
            Assert.AreEqual("Te", aligned);
        }

        [TestMethod]
        public void GivenWhiteSpaceInText_WhenCalledWithLeft_ThenReturnsTrimmedTextWithLeftPadding()
        {
            // Arrange
            const string text = "Test  ";
            const int columns = 10;

            // Act
            var aligned = AlignText.AlignAndFit(text, Alignment.Left, columns);

            // Assert
            Assert.AreEqual("Test      ", aligned);
        }

        [TestMethod]
        public void GivenTextAndColumnsThatEvenlySplit_WhenCalledWithCenter_ThenReturnsCenteredText()
        {
            // Arrange
            const string text = "Test";
            const int columns = 8;

            // Act
            var aligned = AlignText.AlignAndFit(text, Alignment.Center, columns);

            // Assert
            Assert.AreEqual("  Test  ", aligned);
        }

        [TestMethod]
        public void GivenTextAndColumnsThatDoNotSplit_WhenCalledWithCenter_ThenReturnsCenteredTextWithLeftPreference()
        {
            // Arrange
            const string text = "Test";
            const int columns = 7;

            // Act
            var aligned = AlignText.AlignAndFit(text, Alignment.Center, columns);

            // Assert
            Assert.AreEqual(" Test  ", aligned);
        }
        [TestMethod]
        public void GivenSmallText_WhenCalledWithRight_ThenReturnsTextWithRightPadding()
        {
            // Arrange
            const string text = "Test";
            const int columns = 6;

            // Act
            var aligned = AlignText.AlignAndFit(text, Alignment.Right, columns);

            // Assert
            Assert.AreEqual("  Test", aligned);
        }
    }
}
