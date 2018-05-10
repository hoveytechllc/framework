using HoveyTech.Core.Runtime;
using Xunit;

namespace HoveyTech.Core.Tests.Model
{
    public class ResponseTests
    {
        [Fact]
        public void ctorResult_does_create_success_response()
        {
            var result = new ResponseTestInstance();
            var sut = new Response<ResponseTestInstance>(result);
            Assert.True(sut.Success);
            Assert.Null(sut.ErrorMessage);
            Assert.True(result == sut.Result);
        }

        [Fact]
        public void ErrorResult_does_create_Error()
        {
            var sut = Response<ResponseTestInstance>.Error("*err*");
            Assert.False(sut.Success);
            Assert.Null(sut.Result);
            Assert.Equal("*err*", sut.ErrorMessage);
        }

        [Fact]
        public void ctorResult_does_create_Error()
        {
            var sut = new Response<ResponseTestInstance>("*err*");
            Assert.False(sut.Success);
            Assert.Null(sut.Result);
            Assert.Equal("*err*", sut.ErrorMessage);
        }

        [Fact]
        public void Create_does_create_instance()
        {
            var result = new ResponseTestInstance();
            var sut = Response<ResponseTestInstance>.Create(result);
            Assert.True(sut.Success);
            Assert.NotNull(sut.Result);
            Assert.True(result == sut.Result);
            Assert.Null(sut.ErrorMessage);
        }

        [Fact]
        public void ctor_does_have_empty_errorMessage_and_success_is_false()
        {
            var sut = new Response();
            Assert.False(sut.Success);
            Assert.Null(sut.ErrorMessage);
        }

        [Fact]
        public void Error_does_create_instance_with_errorMessage()
        {
            var sut = Response.Error("*err*");
            Assert.False(sut.Success);
            Assert.Equal("*err*", sut.ErrorMessage);
        }

        [Fact]
        public void SuccessResponse_is_success()
        {
            Assert.True(Response.SuccessResponse.Success);
            Assert.Null(Response.SuccessResponse.ErrorMessage);
        }

        [Fact]
        public void SuccessResponse_is_same_instance()
        {
            var success1 = Response.SuccessResponse;
            var success2 = Response.SuccessResponse;
            Assert.True(success1 == success2);
        }

        public class ResponseTestInstance
        {
            public string Value { get; set; }

            public ResponseTestInstance()
            {
                
            }

            public ResponseTestInstance(string value)
            {
                Value = value;
            }
        }

        [Fact]
        public void Create_does_create_Success_with_result()
        {
            var sut = Response.Create<ResponseTestInstance>(instance =>
            {
                Assert.NotNull(instance);
                Assert.Null(instance.Value);

                instance.Value = "called";
            });
            Assert.True(sut.Success);
            Assert.Null(sut.ErrorMessage);
            Assert.Equal("called", sut.Result.Value);
        }
    }
}
