using Microsoft.AspNetCore.Http;

namespace AspCoreDataTable.Core.Extensions
{
    public static class HttpContextWrapper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext Current => _httpContextAccessor.HttpContext;
    }
}
