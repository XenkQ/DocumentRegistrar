using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Helpers;

public static class ApiHelper
{
    public static async Task<T?> SafeApiCallAsync<T>(Func<Task<T>> apiCall, Action<string>? onError = null)
    {
        try
        {
            return await apiCall();
        }
        catch (HttpRequestException ex) when (ex.StatusCode is HttpStatusCode statusCode)
        {
            string message = statusCode switch
            {
                HttpStatusCode.BadRequest => "Bad request. Please check your input.",
                HttpStatusCode.NotFound => "The requested resource was not found.",
                HttpStatusCode.InternalServerError => "A server error occurred. Please try again later.",
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
