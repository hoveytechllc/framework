using System;

namespace HoveyTech.Core
{
    public class Response
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public Response()
        {

        }

        public Response(bool success)
        {
            Success = success;
        }

        public Response(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Success = false;
        }

        private static Response _successResponse;
        public static Response SuccessResponse
        {
            get
            {
                _successResponse = _successResponse ?? new Response(success: true);
                return _successResponse;
            }
        }

        public static Response Error(string errorMessage)
        {
            return new Response(errorMessage: errorMessage);
        }

        public static Response<TResult> Create<TResult>(Action<TResult> action)
            where TResult : new()
        {
            var result = new TResult();
            action(result);
            return new Response<TResult>(result);
        }
    }

    public class Response<T> : Response
    {
        public T Result { get; set; }

        public Response()
        {
            
        }

        public Response(T result)
        {
            Result = result;
            Success = true;
        }

        public Response(string errorMessage)
            :base(errorMessage) { }

        public new static Response<T> Error(string errorMessage)
        {
            return new Response<T>(errorMessage: errorMessage);
        }

        public static Response<T> Create(T result)
        {
            return new Response<T>(result);
        }
    }

}
