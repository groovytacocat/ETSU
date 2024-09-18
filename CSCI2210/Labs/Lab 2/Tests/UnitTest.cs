using Moq;
using IOperationsNS;

namespace Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(int.MaxValue)]
        public void IsEven_InputOdd_ReturnFalse(int value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.IsEven(value)).Returns(false);

            bool result = IOPMock.Object.IsEven(value);

            Assert.IsFalse(result, $"{value} is odd.");

        }

        [TestMethod]
        [DataRow(-5)]
        [DataRow(-11)]
        [DataRow(-3)]
        public void IsEven_InputOdd_Negative_ReturnFalse(int value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.IsEven(value)).Returns(false);

            bool result = IOPMock.Object.IsEven(value);

            Assert.IsFalse(result, $"{value} is odd and negative");
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(2)]
        [DataRow(4)]
        public void IsEven_InputEven_ReturnTrue(int value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.IsEven(value)).Returns(true);

            bool result = IOPMock.Object.IsEven(value);

            Assert.IsTrue(result, $"{value} is even");
        }


        [TestMethod]
        [DataRow(int.MinValue)]
        [DataRow(-2)]
        [DataRow(-4)]
        [DataRow(-100)]
        public void IsEven_InputEven_Negative_ReturnTrue(int value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.IsEven(value)).Returns(true);

            bool result = IOPMock.Object.IsEven(value);

            Assert.IsTrue(result, $"{value} is even and negative");

        }

        [TestMethod]
        [DataRow(null)]
        public void IsEven_InputNull_ThrowException(int value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.IsEven(value)).Throws<ArgumentNullException>();

            Assert.ThrowsException<ArgumentNullException>(() => IOPMock.Object.IsEven(value));
        }

        [TestMethod]
        [DataRow(-0.1)]
        [DataRow(100.1)]
        [DataRow(Double.MinValue)]
        [DataRow(Double.MaxValue)]
        [DataRow(Double.PositiveInfinity)]
        [DataRow(Double.NegativeInfinity)]
        public void IsValid_InputOutOfRange_ReturnFalse(double value)
        {
            var IOPMock = new Mock<IOperations>();


            IOPMock.Setup(x => x.IsValid(value)).Returns(false);

            bool result = IOPMock.Object.IsValid(value);

            Assert.IsFalse(result, $"{value} is not between 0 and 100");
        }

        [TestMethod]
        [DataRow(0.0)]
        [DataRow(0.00000000000000000000000001)]
        [DataRow(0.1)]
        [DataRow(50.0)]
        [DataRow(99.999999)]
        [DataRow(100.0)]
        public void IsValid_InputInRange_ReturnTrue(double value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.IsValid(It.IsInRange<double>(0.0, 100.0, Moq.Range.Inclusive))).Returns(true);

            bool result = IOPMock.Object.IsValid(value);

            Assert.IsTrue(result, $"{value} is between 0 and 100");
        }

        [TestMethod]
        [DataRow(null)]
        public void IsValid_InputNull_ThrowsException(double value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.IsValid(value)).Throws<ArgumentNullException>();

            Assert.ThrowsException<ArgumentNullException>(() => IOPMock.Object.IsValid(value));
        }

        [TestMethod]
        [DataRow(0.0, 0.0)]
        [DataRow(-1000, -3000)]
        [DataRow(500.0, 1500.0)]
        [DataRow(.0000000001, .0000000003)]
        [DataRow(1.333333333, 3.999999999)]
        public void Triple_ValidInput_ReturnsExpectedValue(double value, double expected)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.Triple(value)).Returns(3.0 * value);

            double actual = IOPMock.Object.Triple(value);

            Assert.AreEqual(expected, actual, $"{expected} is 3 * {value}");
        }

        [TestMethod]
        [DataRow(null)]
        public void Triple_InvalidInput_ThrowsException(double value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.Triple(value)).Throws<ArgumentNullException>();

            Assert.ThrowsException<ArgumentNullException>(() => IOPMock.Object.Triple(value));
        }

        [TestMethod]
        [DataRow(-1, 98)]
        [DataRow(5, 110)]
        [DataRow(-50, 0)]
        public void Operate_InputLessThanZero_ReturnsExpected(int value, int expected)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.Operate(value)).Returns(2 * value + 100);

            int actual = IOPMock.Object.Operate(value);

            Assert.AreEqual(expected, actual, $"2 * {value} + 100 = {expected} not {actual}");
        }

        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(300, 1500)]
        [DataRow(999, 4995)]
        public void Operate_InputBetweenZeroAndThousand(int value, int expected)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.Operate(It.IsInRange<int>(0, 999, Moq.Range.Inclusive))).Returns(5 * value);

            int actual = IOPMock.Object.Operate(value);

            Assert.AreEqual(expected, actual, $"5 * {value} is {expected} not {actual}");
        }

        [TestMethod]
        [DataRow(1000, 1500)]
        [DataRow(23523526, 23524026)]
        [DataRow(2147483147, 2147483647)]
        public void Operate_InputGreaterEqualThousand_ReturnsExpected(int value, int expected)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.Operate(It.IsInRange<int>(1000, int.MaxValue, Moq.Range.Inclusive))).Returns(value + 500);

            int actual = IOPMock.Object.Operate(value);

            Assert.AreEqual(expected, actual, $"500 + {value} = {expected} not {actual}");
        }

        [TestMethod]
        [DataRow(int.MinValue)]
        [DataRow(-1073741875)]
        [DataRow(-2000000000)]
        public void Operate_InputMinValue_ThrowsException(int value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.Operate(It.IsInRange<int>(int.MinValue, -1073741874, Moq.Range.Inclusive))).Throws<ArgumentOutOfRangeException>();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => IOPMock.Object.Operate(value));
        }

        [TestMethod]
        [DataRow(2147483149)]
        [DataRow(2147483300)]
        [DataRow(int.MaxValue)]
        public void Operate_InputMaxValue_ThrowsException(int value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.Operate(It.IsInRange<int>(2147483148, int.MaxValue, Moq.Range.Inclusive))).Throws<ArgumentOutOfRangeException>();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => IOPMock.Object.Operate(value));
        }

        [TestMethod]
        [DataRow(null)]
        public void Operate_InputInvalid_ThrowsException(int value)
        {
            var IOPMock = new Mock<IOperations>();

            IOPMock.Setup(x => x.Operate(value)).Throws<ArgumentNullException>();

            Assert.ThrowsException<ArgumentNullException>(() => IOPMock.Object.Operate(value));
        }
    }
}

