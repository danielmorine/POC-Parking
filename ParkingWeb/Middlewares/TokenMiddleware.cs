//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using System.Threading.Tasks;

//namespace ParkingWeb.Middlewares
//{
//    public class TokenMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public TokenMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task<bool> Invoke(HttpContext context)
//        {
//            await _next(context);
//            return true;
//        }
//    }

//    public static class TokenMiddlewareExtension
//    {
//        public static IApplicationBuilder UseTokenMiddleware(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<TokenMiddleware>();
//        }
//    }
//}
