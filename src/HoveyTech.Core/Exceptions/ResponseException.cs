using System;
using HoveyTech.Core.Runtime;

namespace HoveyTech.Core.Exceptions
{
    public class ResponseException : Exception
    {
        public Response Response { get; private set; }

        public ResponseException(Response response)
        {
            Response = response;
        }

        public ResponseException(string errorMessage)
        {
            Response = new Response(errorMessage: errorMessage);
        }

        public override string ToString()
        {
            if (Response.Success)
                return "ResponseException: Success: True";
            return $"ResponseException: ErrorMessage: {Response.ErrorMessage}";
        }
    }
}
