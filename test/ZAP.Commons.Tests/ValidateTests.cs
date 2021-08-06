using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ZAP.Commons.Tests
{
    public class ValidateTests
    {
        public static readonly IEnumerable<object[]> NotNullObjects = new List<object[]>()
        {
            new object[] { "This is a test" },
            new object[] { true },
            new object[] { new object() },
        };

        public static readonly IEnumerable<object[]> ThrowMessages = new List<object[]>()
        {
            new object[] { "This is a test" },
            new object[] { "" },
            new object[] { null },
        };

        public static IEnumerable<object[]> DataHasSameTypes = new List<object[]>
        {
            new object[] { typeof(string), "hello" },
            new object[] { typeof(double), 12d },
            new object[] { typeof(IEnumerable<string>), new string[0]}
        };

        public static IEnumerable<object[]> DataHasMismatchTypes = new List<object[]>
        {
            new object[] { typeof(DateTime), "hello", "Error" },
            new object[] { typeof(double?), null, "Error" },
            new object[] { typeof(IEnumerable<string>), "bro", "Error" }
        };


        [Fact]
        public void IsTrue_TrueStatement_ShouldReturns()
        {
            // Arrange
            var testValue = true;

            // Act 
            Validate.IsTrue(testValue, "");

            // Assert - Method returns void
        }

        [Theory]
        [MemberData(nameof(ThrowMessages))]
        public void IsTrue_FalseStatement_ShouldThrows(string message)
        {
            // Arrage
            var testValue = false;

            // Act, Assert
            var exception = Assert.Throws<ValidationException>(() => Validate.IsTrue(testValue, message));
            // Make sure the correct message being thrown
            if(message != null)
                Assert.Equal(message, exception.Message);
        }


        [Fact]
        public void IsFalse_FalseStatement_ShouldReturns()
        {
            // Arrange
            var testValue = false;

            // Act 
            Validate.IsFalse(testValue, "");

            // Assert - Method returns void
        }

        [Theory]
        [MemberData(nameof(ThrowMessages))]
        public void IsFalse_TrueStatement_ShouldThrows(string message)
        {
            // Arrage
            var testValue = true;

            // Act, Assert
            var exception = Assert.Throws<ValidationException>(() => Validate.IsFalse(testValue, message));
            // Make sure the correct message being thrown
            if(message != null)
                Assert.Equal(message, exception.Message);
        }

        [Theory]
        [MemberData(nameof(NotNullObjects))]
        public void NotNull_PassedObject_ShouldReturns(object objectToTest)
        {   
            // Act 
            Validate.NotNull(objectToTest, "");

            // Assert - Method returns void
        }

        [Theory]
        [MemberData(nameof(ThrowMessages))]
        public void NotNull_PassedNull_ShouldThrows(string message)
        {
            // Arrange
            object nullVariable = null;

            // Act 
            var exception = Assert.Throws<ValidationException>(() => Validate.NotNull(nullVariable, message));
            // Make sure the correct message being thrown
            if (message != null)
                Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void NotNullOrEmpty_StringHasContent_ShouldReturns()
        {
            // Arrange
            var testString = "Hello xUnit!";
            
            // Act
            Validate.NotNullOrEmpty(testString, "");

            // Assert - Method returns void
        }

        [Theory]
        [InlineData("", "Throws")]
        [InlineData(null, "Throws")]
        public void NotNullOrEmpty_StringEmptyOrNull_ShouldThrows(string target, string message)
        {
            var exception = Assert.Throws<ValidationException>(() => Validate.NotNullOrEmpty(target, message));

            Assert.Equal(message, exception.Message);
        }

        [Theory]
        [MemberData(nameof(DataHasSameTypes))]
        public void IsType_SameOrBaseType_ShouldCast(Type type, object obj)
        {
            // Arrange 
            var reflectedMethod = typeof(Validate)
                .GetMethod(nameof(Validate.IsType))
                .MakeGenericMethod(type);

            // Act
            var actual = reflectedMethod.Invoke(null, new object[] { obj, "" });

            // Assert
            Assert.Equal(obj, actual);
        }

        [Theory]
        [MemberData(nameof(DataHasMismatchTypes))]
        public void IsType_TypeMisMatch_ShouldThrows(Type type, object obj, string message)
        {
            // Arrange 
            var reflectedMethod = typeof(Validate)
                .GetMethod(nameof(Validate.IsType))
                .MakeGenericMethod(type);

            // Act
            var exception = Assert.Throws<TargetInvocationException>(() => reflectedMethod.Invoke(null, new object[] { obj, message }));
            Assert.IsType<ValidationException>(exception.InnerException);
            // Make sure the correct message being thrown
            if (message != null)
                Assert.Equal(message, exception.InnerException.Message);
        }
    }
}
