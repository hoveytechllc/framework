using HoveyTech.Core.Runtime;

namespace HoveyTech.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static Response<T> ToResponse<T>(this T obj)
        {
            return new Response<T>(result: obj);
        }
    }
}
