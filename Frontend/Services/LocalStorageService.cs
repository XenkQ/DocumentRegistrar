using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Frontend.Services;

public interface ILocalStorageService
{
    Task ClearAsync();

    Task<T?> ReadAsync<T>(string key);

    Task<bool> RemoveAsync(string key);

    Task SaveAsync<T>(string key, T value);
}

public class LocalStorageService : ILocalStorageService
{
    public static class Keys
    {
        public const string User = "User";
    }

    private readonly string _filePath;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public LocalStorageService(string? storageFilePath = null)
    {
        if (!string.IsNullOrWhiteSpace(storageFilePath))
        {
            _filePath = storageFilePath;
        }
        else
        {
            var localApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(localApp, "DocumentRegistrar");
            Directory.CreateDirectory(dir);
            _filePath = Path.Combine(dir, "localstorage.json");
        }
    }

    public async Task SaveAsync<T>(string key, T value)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key must be provided.", nameof(key));
        }

        var store = await LoadStoreAsync().ConfigureAwait(false);
        store[key] = JsonSerializer.SerializeToNode(value, _jsonOptions);

        await WriteStoreAtomicallyAsync(store).ConfigureAwait(false);
    }

    public async Task<T?> ReadAsync<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key must be provided.", nameof(key));
        }

        var store = await LoadStoreAsync().ConfigureAwait(false);
        if (!store.ContainsKey(key)) return default;

        var node = store[key];
        if (node is null) return default;

        try
        {
            return node.Deserialize<T>(_jsonOptions);
        }
        catch
        {
            return default;
        }
    }

    public async Task<bool> RemoveAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key must be provided.", nameof(key));
        }

        var store = await LoadStoreAsync().ConfigureAwait(false);
        if (!store.Remove(key))
        {
            return false;
        }

        await WriteStoreAtomicallyAsync(store).ConfigureAwait(false);

        return true;
    }

    public Task ClearAsync()
    {
        var empty = new JsonObject();

        return WriteStoreAtomicallyAsync(empty);
    }

    private async Task<JsonObject> LoadStoreAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return new JsonObject();
            }

            var text = await File.ReadAllTextAsync(_filePath).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(text))
            {
                return new JsonObject();
            }

            var node = JsonNode.Parse(text);

            return node as JsonObject ?? new JsonObject();
        }
        catch
        {
            return new JsonObject();
        }
    }

    private async Task WriteStoreAtomicallyAsync(JsonObject store)
    {
        var dir = Path.GetDirectoryName(_filePath) ?? throw new InvalidOperationException("Invalid storage path.");
        Directory.CreateDirectory(dir);

        var tempFile = Path.Combine(dir, $"{Guid.NewGuid():N}.tmp");
        var json = store.ToJsonString(_jsonOptions);
        await File.WriteAllTextAsync(tempFile, json).ConfigureAwait(false);

        try
        {
            if (File.Exists(_filePath))
            {
                File.Replace(tempFile, _filePath, null);
            }
            else
            {
                File.Move(tempFile, _filePath);
            }
        }
        catch
        {
            try
            {
                File.Copy(tempFile, _filePath, overwrite: true);
                File.Delete(tempFile);
            }
            catch
            {
                if (File.Exists(tempFile)) File.Delete(tempFile);
                throw;
            }
        }
    }
}
