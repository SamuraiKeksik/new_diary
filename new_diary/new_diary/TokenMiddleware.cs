using System.Runtime.CompilerServices;

namespace new_diary
{
    public static class TokenExtension
    {
        public static IApplicationBuilder UseTokenMiddleware(this IApplicationBuilder builder, string pattern) 
        {
            return builder.UseMiddleware<TokenMiddleware>(pattern);
        }
    }

    public class TokenMiddleware
    {
        private readonly RequestDelegate next;
        private string pattern;
        public TokenMiddleware(RequestDelegate next, string pattern)
        {
            this.next = next;
            this.pattern = pattern;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["Token"];
            if(token != "1234")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Error!");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
