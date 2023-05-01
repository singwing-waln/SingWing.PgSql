namespace SingWing.PgSql;

/// <summary>
/// Provides extension methods for executing query commands.
/// </summary>
public static partial class ExecutorExtensions
{
    /// <summary>
    /// Executes the specified query command and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameters">The list of parameters used by the command. The number of parameters cannot exceed 65535.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        in ReadOnlyMemory<Parameter> parameters,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameters,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with no parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            ReadOnlyMemory<Parameter>.Empty,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 1 parameter and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter">The command parameter.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholder in <paramref name="commandText"/> must be $1.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 2 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 3 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 4 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 5 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 6 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 7 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 8 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 9 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 10 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9,
            parameter10,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 11 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9,
            parameter10,
            parameter11,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 12 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="parameter12">The 12th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        Parameter parameter12,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9,
            parameter10,
            parameter11,
            parameter12,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 13 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="parameter12">The 12th parameter of the command.</param>
    /// <param name="parameter13">The 13th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        Parameter parameter12,
        Parameter parameter13,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9,
            parameter10,
            parameter11,
            parameter12,
            parameter13,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 14 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="parameter12">The 12th parameter of the command.</param>
    /// <param name="parameter13">The 13th parameter of the command.</param>
    /// <param name="parameter14">The 14th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        Parameter parameter12,
        Parameter parameter13,
        Parameter parameter14,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9,
            parameter10,
            parameter11,
            parameter12,
            parameter13,
            parameter14,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 15 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="parameter12">The 12th parameter of the command.</param>
    /// <param name="parameter13">The 13th parameter of the command.</param>
    /// <param name="parameter14">The 14th parameter of the command.</param>
    /// <param name="parameter15">The 15th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        Parameter parameter12,
        Parameter parameter13,
        Parameter parameter14,
        Parameter parameter15,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9,
            parameter10,
            parameter11,
            parameter12,
            parameter13,
            parameter14,
            parameter15,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 16 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="parameter12">The 12th parameter of the command.</param>
    /// <param name="parameter13">The 13th parameter of the command.</param>
    /// <param name="parameter14">The 14th parameter of the command.</param>
    /// <param name="parameter15">The 15th parameter of the command.</param>
    /// <param name="parameter16">The 16th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        string commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        Parameter parameter12,
        Parameter parameter13,
        Parameter parameter14,
        Parameter parameter15,
        Parameter parameter16,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText.AsMemory(),
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9,
            parameter10,
            parameter11,
            parameter12,
            parameter13,
            parameter14,
            parameter15,
            parameter16,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with no parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    public static ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        CancellationToken cancellationToken = default) =>
        executor.QueryAsync(
            commandText,
            ReadOnlyMemory<Parameter>.Empty,
            cancellationToken);

    /// <summary>
    /// Executes the specified query command with 1 parameter and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter">The command parameter.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholder in <paramref name="commandText"/> must be $1.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..1],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 2 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..2],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 3 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..3],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 4 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..4],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 5 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..5],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 6 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..6],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 7 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            parameters[6] = parameter7;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..7],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 8 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            parameters[6] = parameter7;
            parameters[7] = parameter8;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..8],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 9 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            parameters[6] = parameter7;
            parameters[7] = parameter8;
            parameters[8] = parameter9;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..9],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 10 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            parameters[6] = parameter7;
            parameters[7] = parameter8;
            parameters[8] = parameter9;
            parameters[9] = parameter10;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..10],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 11 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            parameters[6] = parameter7;
            parameters[7] = parameter8;
            parameters[8] = parameter9;
            parameters[9] = parameter10;
            parameters[10] = parameter11;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..11],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 12 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="parameter12">The 12th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        Parameter parameter12,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            parameters[6] = parameter7;
            parameters[7] = parameter8;
            parameters[8] = parameter9;
            parameters[9] = parameter10;
            parameters[10] = parameter11;
            parameters[11] = parameter12;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..12],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 13 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="parameter12">The 12th parameter of the command.</param>
    /// <param name="parameter13">The 13th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        Parameter parameter12,
        Parameter parameter13,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            parameters[6] = parameter7;
            parameters[7] = parameter8;
            parameters[8] = parameter9;
            parameters[9] = parameter10;
            parameters[10] = parameter11;
            parameters[11] = parameter12;
            parameters[12] = parameter13;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..13],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 14 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="parameter12">The 12th parameter of the command.</param>
    /// <param name="parameter13">The 13th parameter of the command.</param>
    /// <param name="parameter14">The 14th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        Parameter parameter12,
        Parameter parameter13,
        Parameter parameter14,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            parameters[6] = parameter7;
            parameters[7] = parameter8;
            parameters[8] = parameter9;
            parameters[9] = parameter10;
            parameters[10] = parameter11;
            parameters[11] = parameter12;
            parameters[12] = parameter13;
            parameters[13] = parameter14;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..14],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 15 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="parameter12">The 12th parameter of the command.</param>
    /// <param name="parameter13">The 13th parameter of the command.</param>
    /// <param name="parameter14">The 14th parameter of the command.</param>
    /// <param name="parameter15">The 15th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        Parameter parameter12,
        Parameter parameter13,
        Parameter parameter14,
        Parameter parameter15,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            parameters[6] = parameter7;
            parameters[7] = parameter8;
            parameters[8] = parameter9;
            parameters[9] = parameter10;
            parameters[10] = parameter11;
            parameters[11] = parameter12;
            parameters[12] = parameter13;
            parameters[13] = parameter14;
            parameters[14] = parameter15;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..15],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }

    /// <summary>
    /// Executes the specified query command with 16 parameters and returns a single result set.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="commandText">The command text, such as "SELECT ...", "FETCH ...", etc.</param>
    /// <param name="parameter1">The 1st parameter of the command.</param>
    /// <param name="parameter2">The 2nd parameter of the command.</param>
    /// <param name="parameter3">The 3rd parameter of the command.</param>
    /// <param name="parameter4">The 4th parameter of the command.</param>
    /// <param name="parameter5">The 5th parameter of the command.</param>
    /// <param name="parameter6">The 6th parameter of the command.</param>
    /// <param name="parameter7">The 7th parameter of the command.</param>
    /// <param name="parameter8">The 8th parameter of the command.</param>
    /// <param name="parameter9">The 9th parameter of the command.</param>
    /// <param name="parameter10">The 10th parameter of the command.</param>
    /// <param name="parameter11">The 11th parameter of the command.</param>
    /// <param name="parameter12">The 12th parameter of the command.</param>
    /// <param name="parameter13">The 13th parameter of the command.</param>
    /// <param name="parameter14">The 14th parameter of the command.</param>
    /// <param name="parameter15">The 15th parameter of the command.</param>
    /// <param name="parameter16">The 16th parameter of the command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        this IExecutor executor,
        ReadOnlyMemory<char> commandText,
        Parameter parameter1,
        Parameter parameter2,
        Parameter parameter3,
        Parameter parameter4,
        Parameter parameter5,
        Parameter parameter6,
        Parameter parameter7,
        Parameter parameter8,
        Parameter parameter9,
        Parameter parameter10,
        Parameter parameter11,
        Parameter parameter12,
        Parameter parameter13,
        Parameter parameter14,
        Parameter parameter15,
        Parameter parameter16,
        CancellationToken cancellationToken = default)
    {
        var parameters = ParameterListPool.Rent();

        try
        {
            parameters[0] = parameter1;
            parameters[1] = parameter2;
            parameters[2] = parameter3;
            parameters[3] = parameter4;
            parameters[4] = parameter5;
            parameters[5] = parameter6;
            parameters[6] = parameter7;
            parameters[7] = parameter8;
            parameters[8] = parameter9;
            parameters[9] = parameter10;
            parameters[10] = parameter11;
            parameters[11] = parameter12;
            parameters[12] = parameter13;
            parameters[13] = parameter14;
            parameters[14] = parameter15;
            parameters[15] = parameter16;
            return await executor.QueryAsync(
                commandText,
                parameters.AsMemory()[..16],
                cancellationToken);
        }
        finally
        {
            ParameterListPool.Return(parameters);
        }
    }
}
