using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Helpers;

public static class ApiHelper
{
    public static Task<T?> SafeApiCallAsync<T>(Func<Task<T>> apiCall, Action<string>? onError = null)
    => SafeApiCallCoreAsync(apiCall, onError);

    public static Task SafeApiCallAsync(Func<Task> apiCall, Action<string>? onError = null)
        => SafeApiCallCoreAsync<object?>(async () => { await apiCall(); return null; }, onError);

    private static async Task<TResult?> SafeApiCallCoreAsync<TResult>(
        Func<Task<TResult>> apiCall,
        Action<string>? onError)
    {
        try
        {
            return await apiCall();
        }
        catch (HttpRequestException ex) when (ex.StatusCode is HttpStatusCode statusCode)
        {
            string message = statusCode switch
            {
                HttpStatusCode.BadRequest => $"Bad request. Please check your input. Exception: {ex}",
                HttpStatusCode.NotFound => $"The requested resource was not found. . Exception: {ex}",
                HttpStatusCode.InternalServerError => $"A server error occurred. Please try again later. . Exception: {ex}",
                _ => $"Request failed with status code {(int)statusCode}."
            };

            onError?.Invoke(message);
        }
        catch (TaskCanceledException)
        {
            onError?.Invoke("The request timed out. Please try again later.");
        }
        catch (Exception)
        {
            onError?.Invoke("An unexpected error occurred.");
        }

        return default;
    }
}
