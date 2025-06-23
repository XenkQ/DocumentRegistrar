using System;
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
        catch (HttpRequestException)
        {
            onError?.Invoke("Unable to connect to the server. Please check your internet connection.");
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
