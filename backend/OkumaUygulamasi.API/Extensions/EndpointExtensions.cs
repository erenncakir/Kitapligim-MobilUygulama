using Asp.Versioning;
using Asp.Versioning.Builder;
namespace OkumaUygulamasi.API.Extensions
{
    public static class EndpointExtensions
    {
        private static ApiVersionSet? _apiVersionSet;

        public static RouteGroupBuilder MapVersionedGroup(this IEndpointRouteBuilder app, string prefix, int version = 1)
        {
            if (_apiVersionSet == null)
            {
                _apiVersionSet = app.NewApiVersionSet()
                    .HasApiVersion(new ApiVersion(1, 0))
                    .ReportApiVersions()
                    .Build();
            }
            return app.MapGroup($"/api/v{{version:ApiVersion}}/{prefix}")
                .WithApiVersionSet(_apiVersionSet)
                .MapToApiVersion(version);
        }
    }
}
