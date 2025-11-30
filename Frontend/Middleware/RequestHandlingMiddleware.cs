using Frontend.Services;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Frontend.Handlers;

public class RequestHandlingMiddleware : DelegatingHandler
{
    private readonly IUserContextService _userService;
    private readonly ILogger<RequestHandlingMiddleware> _logger;

    public RequestHandlingMiddleware(IUserContextService userService, ILogger<RequestHandlingMiddleware> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            if (request != null && !string.IsNullOrEmpty(_userService.User.BearerToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _userService.User.BearerToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, ex.Message);

            return new HttpResponseMessage(ex.StatusCode ?? HttpStatusCode.InternalServerError);
        }
    }
}
