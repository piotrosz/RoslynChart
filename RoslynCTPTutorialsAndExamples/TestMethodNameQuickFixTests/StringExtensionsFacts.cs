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

            [Fact]
            public void converts_partly_underscore_case()
            {
                // Arrange
                string camelCase = "MethodName_partly_underscore_case";

                // Act
                string result = camelCase.ToUnderscoreCase();

                // Assert
                Assert.Equal("method_name_partly_underscore_case", result);
            }

            [Fact]
            public void converts_with_spaces()
            {
                // Arrange
                string camelCase = "Method Name la la uiouoiu";

                // Act
                string result = camelCase.ToUnderscoreCase();

                // Assert
                Assert.Equal("method_name_la_la_uiouoiu", result);
            }
        }

        public class IsUnderscoreCaseMethod
        {
            [Fact]
            public void correctly_validates_2_parts_method()
            {
                // Arrange
                string methodName = "method_name";

                // Act
                bool result = methodName.IsUnderscoreCase();

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void validates_with_numbers()
            {
                // Arrange
                string methodName = "baba_2_ma_koty_3";

                // Act
                bool result = methodName.IsUnderscoreCase();

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void should_not_validate_method_with_upper_case()
            {
                // Arrange
                string methodName = "Ala_ma_kota";

                // Act
                bool result = methodName.IsUnderscoreCase();

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void should_not_validate_method_wihout_underscore()
            {
                // Arrange
                string methodName = "DoEverything";

                // Act
                bool result = methodName.IsUnderscoreCase();

                // Assert
                Assert.False(result);
            }
        }
    }
}
