using Frontend.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Frontend.Handlers;

public class AuthTokenHandler : DelegatingHandler
{
    private readonly IUserService _userService;

    public AuthTokenHandler(IUserService userService)
    {
        _userService = userService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request != null && !string.IsNullOrEmpty(_userService.User.BearerToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _userService.User.BearerToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
