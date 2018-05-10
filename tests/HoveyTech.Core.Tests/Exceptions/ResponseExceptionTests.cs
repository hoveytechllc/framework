using HoveyTech.Core.Exceptions;
using HoveyTech.Core.Runtime;
using Xunit;

namespace HoveyTech.Core.Tests.Exceptions
{
    public class ResponseExceptionTests
    {
        [Fact]
        public void ctor_String_does_create_error_response()
        {
            var ex = new ResponseException("*Error*");

            Assert.False(ex.Response.Success);
            Assert.Equal("*Error*", ex.Response.ErrorMessage);
        }

        [Fact]
        public void ToString_does_indicate_success_string_if_response_success()
        {
            var response = new Response(success: true);
            var ex = new ResponseException(response);

            Assert.Equal("ResponseException: Success: True", ex.ToString());
        }

        [Fact]
        public void ToString_does_include_response_message()
        {
            var response = new Response("Something bad happened.");
            var ex = new ResponseException(response);

            Assert.Equal("ResponseException: ErrorMessage: Something bad happened.", ex.ToString());
        }

        [Fact]
        public void Response_does_equal_ctor_parameter()
        {
            var response = new Response("Something bad happened.");
            var ex = new ResponseException(response);

            Assert.True(ex.Response == response);
        }
    }
}
