// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Caching;
/// <summary>
/// Static class for managing cache keys and expiration for Vehicle-related data.
/// </summary>
public static class VehicleCacheKey
{
    // Defines the refresh interval for the cache expiration token
    private static readonly TimeSpan RefreshInterval = TimeSpan.FromMinutes(30);
    // Object used for locking to ensure thread safety
    private static readonly object TokenLock = new();
    // CancellationTokenSource used for managing cache expiration
    private static CancellationTokenSource _tokenSource = new (RefreshInterval);
    /// <summary>
    /// Gets the memory cache entry options with an expiration token.
    /// </summary>
    public static MemoryCacheEntryOptions MemoryCacheEntryOptions =>
        new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(GetOrCreateTokenSource().Token));

    public const string GetAllCacheKey = "all-Vehicles";
    public static string GetPaginationCacheKey(string parameters) {
        return $"VehicleCacheKey:VehiclesWithPaginationQuery,{parameters}";
    }
    public static string GetByNameCacheKey(string parameters) {
        return $"VehicleCacheKey:GetByNameCacheKey,{parameters}";
    }
    public static string GetByIdCacheKey(string parameters) {
        return $"VehicleCacheKey:GetByIdCacheKey,{parameters}";
    }

    
    /// <summary>
    /// Gets or creates a new <see cref="CancellationTokenSource"/> with the specified refresh interval.
    /// </summary>
    /// <returns>The current or new <see cref="CancellationTokenSource"/>.</returns>
    public static CancellationTokenSource GetOrCreateTokenSource()
    {
        lock (TokenLock)
        {
            if (_tokenSource.IsCancellationRequested)
            {
                _tokenSource.Dispose();
                _tokenSource = new CancellationTokenSource(RefreshInterval);
            }
            return _tokenSource;
        }
    }
    /// <summary>
    /// Refreshes the cache expiration token by cancelling and recreating the <see cref="CancellationTokenSource"/>.
    /// </summary>
    public static void Refresh()
    {
        lock (TokenLock)
        {
            if (!_tokenSource.IsCancellationRequested)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
                _tokenSource = new CancellationTokenSource(RefreshInterval);
            }
        }
    }
}

