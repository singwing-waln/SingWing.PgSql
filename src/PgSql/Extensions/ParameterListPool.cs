using System.Buffers;

namespace SingWing.PgSql;

/// <summary>
/// Provides a pool of parameter lists based on ArrayPool.
/// </summary>
internal static class ParameterListPool
{
    /// <summary>
    /// The maximum number of parameters supported by this parameter list pool.
    /// </summary>
    private const int MaxParameterCount = 16;

    /// <summary>
    /// The underlying ArrayPool.
    /// </summary>
    private static readonly ArrayPool<Parameter> ListPool =
        ArrayPool<Parameter>.Create(MaxParameterCount, maxArraysPerBucket: 32);

    /// <summary>
    /// Retrieves a parameter list.
    /// </summary>
    /// <returns>An array of <see cref="Parameter"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Parameter[] Rent() => ListPool.Rent(MaxParameterCount);

    /// <summary>
    /// Returns an array to the pool that was previously obtained using the Rent method.
    /// </summary>
    /// <param name="parameters">A buffer to return to the pool that was previously obtained using the Rent method.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Return(Parameter[] parameters) => ListPool.Return(parameters, clearArray: false);
}
