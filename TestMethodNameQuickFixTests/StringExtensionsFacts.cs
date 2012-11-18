using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TestMethodNameQuickFix;

namespace TestMethodNameQuickFixTests
{
    public class StringExtensionsFacts
    {
        public class ConvertToUnderscoreCaseMethod
        {
            [Fact]
            public void converts_simple_name()
            {
                // Arrange
                string camelCase = "MethodName";

                // Act
                string result = camelCase.ToUnderscoreCase();

                // Assert
                Assert.Equal("method_name", result);
            }

            [Fact]
            public void converts_3_parts_name()
            {
                // Arrange
                string camelCase = "MethodNameTest";

                // Act
                string result = camelCase.ToUnderscoreCase();

                // Assert
                Assert.Equal("method_name_test", result);
            }

            [Fact]
            public void converts_4_parts_name_with_numbers()
            {
                // Arrange
                string camelCase = "MethodName2TestNumber34";

                // Act
                string result = camelCase.ToUnderscoreCase();

                // Assert
                Assert.Equal("method_name_2_test_number_34", result);
            }
        }
    }
}
