using SingWing.PgSql.Protocol.Messages.DataTypes;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace SingWing.PgSql;

public abstract partial class Parameter
{
    #region BooleanArray

    /// <summary>
    /// Implicitly converts an array of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(bool?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(bool[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(List<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(List<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(Queue<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(Stack<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(Collection<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="bool"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<bool?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<bool>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a BitArray of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The BitArray of <see langword="bool"/> values to convert.</param>
    public static implicit operator Parameter(BitArray? values)
    {
        return ArrayParameter<bool, BooleanProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<bool>? ToEnumerable(BitArray values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                yield return values[i];
            }
        }
    }

    #endregion

    #region ByteaArray

    /// <summary>
    /// Implicitly converts an array of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(byte[]?[]? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(HashSet<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(LinkedList<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(List<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(Queue<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(SortedSet<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(Stack<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(Collection<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="byte"/>[]? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<byte[]?>? values)
    {
        return (values as IEnumerable<byte[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The array of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyMemory<byte>?[]? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The array of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ReadOnlyMemory<byte>[]? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(HashSet<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(HashSet<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(LinkedList<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(LinkedList<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The List of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(List<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The List of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(List<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Queue of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(Queue<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Queue of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(Queue<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(SortedSet<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(SortedSet<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Stack of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(Stack<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Stack of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(Stack<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Collection of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(Collection<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Collection of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(Collection<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of ReadOnlyMemory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of ReadOnlyMemory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ReadOnlyMemory<byte>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of ReadOnlyMemory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of ReadOnlyMemory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ReadOnlyMemory<byte>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The array of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(Memory<byte>?[]? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The array of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(Memory<byte>[]? values)
    {
        return (values as IEnumerable<Memory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ArraySegment<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(HashSet<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(HashSet<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(LinkedList<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(LinkedList<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The List of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(List<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The List of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(List<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Queue of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(Queue<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Queue of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(Queue<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(SortedSet<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(SortedSet<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Stack of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(Stack<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Stack of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(Stack<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableList<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Collection of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(Collection<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Collection of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(Collection<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of Memory&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of Memory&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<Memory<byte>?>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of Memory&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of Memory&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<Memory<byte>>? values)
    {
        return (values as IEnumerable<Memory<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The array of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<byte>?[]? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The array of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ArraySegment<byte>[]? values)
    {
        return (values as IEnumerable<ArraySegment<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(HashSet<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(HashSet<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(LinkedList<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(LinkedList<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The List of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(List<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The List of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(List<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Queue of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(Queue<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Queue of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(Queue<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(SortedSet<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(SortedSet<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Stack of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(Stack<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Stack of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(Stack<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Collection of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(Collection<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The Collection of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(Collection<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of ArraySegment&lt;byte&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of ArraySegment&lt;byte&gt;? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ArraySegment<byte>?>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of ArraySegment&lt;byte&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of ArraySegment&lt;byte&gt; values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ArraySegment<byte>>? values)
    {
        return (values as IEnumerable<ArraySegment<byte>?>).ToArrayParameter();
    }

    #endregion

    #region DateArray

    /// <summary>
    /// Implicitly converts an array of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The array of DateOnly? values to convert.</param>
    public static implicit operator Parameter(DateOnly?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The array of DateOnly values to convert.</param>
    public static implicit operator Parameter(DateOnly[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of DateOnly values to convert.</param>
    public static implicit operator Parameter(ArraySegment<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of DateOnly values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of DateOnly values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of DateOnly values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of DateOnly? values to convert.</param>
    public static implicit operator Parameter(HashSet<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of DateOnly values to convert.</param>
    public static implicit operator Parameter(HashSet<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of DateOnly? values to convert.</param>
    public static implicit operator Parameter(LinkedList<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of DateOnly values to convert.</param>
    public static implicit operator Parameter(LinkedList<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The List of DateOnly? values to convert.</param>
    public static implicit operator Parameter(List<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The List of DateOnly values to convert.</param>
    public static implicit operator Parameter(List<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The Queue of DateOnly? values to convert.</param>
    public static implicit operator Parameter(Queue<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The Queue of DateOnly values to convert.</param>
    public static implicit operator Parameter(Queue<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of DateOnly? values to convert.</param>
    public static implicit operator Parameter(SortedSet<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of DateOnly values to convert.</param>
    public static implicit operator Parameter(SortedSet<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The Stack of DateOnly? values to convert.</param>
    public static implicit operator Parameter(Stack<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The Stack of DateOnly values to convert.</param>
    public static implicit operator Parameter(Stack<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of DateOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of DateOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of DateOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableList<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of DateOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of DateOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of DateOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The Collection of DateOnly? values to convert.</param>
    public static implicit operator Parameter(Collection<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The Collection of DateOnly values to convert.</param>
    public static implicit operator Parameter(Collection<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of DateOnly? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<DateOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of DateOnly values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<DateOnly>? values)
    {
        return values.ToArrayParameter();
    }

    #endregion

    #region FloatArray

    /// <summary>
    /// Implicitly converts an array of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(float?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(float[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(List<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(List<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(Queue<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(Stack<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(Collection<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="float"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<float?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="float"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<float>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(double?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(double[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(List<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(List<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(Queue<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(Stack<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(Collection<double>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="double"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<double?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="double"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<double>? values)
    {
        return values.ToArrayParameter();
    }

    #endregion

    #region Int2Array

    /// <summary>
    /// Implicitly converts an array of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(short?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(short[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(List<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(List<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(Queue<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(Stack<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(Collection<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="short"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<short?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="short"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<short>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ushort?[]? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ushort[]? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(List<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(List<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(Queue<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(Stack<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(Collection<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="ushort"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ushort?>? values)
    {
        return (values as IEnumerable<ushort?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="ushort"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ushort>? values)
    {
        return (values as IEnumerable<ushort>).ToArrayParameter();
    }

    #endregion

    #region Int4Array

    /// <summary>
    /// Implicitly converts an array of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(int?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(int[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(List<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(List<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(Queue<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(Stack<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(Collection<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="int"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<int?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="int"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<int>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(uint?[]? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(uint[]? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(List<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(List<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(Queue<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(Stack<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(Collection<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="uint"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<uint?>? values)
    {
        return (values as IEnumerable<uint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="uint"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<uint>? values)
    {
        return (values as IEnumerable<uint>).ToArrayParameter();
    }

    #endregion

    #region Int8Array

    /// <summary>
    /// Implicitly converts an array of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(long?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(long[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(List<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(List<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(Queue<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(Stack<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(Collection<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="long"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<long?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="long"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<long>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ulong?[]? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ulong[]? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(List<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(List<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(Queue<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(Stack<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(Collection<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="ulong"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ulong?>? values)
    {
        return (values as IEnumerable<ulong?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="ulong"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ulong>? values)
    {
        return (values as IEnumerable<ulong>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(nint?[]? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(nint[]? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(List<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(List<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(Queue<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(Stack<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(Collection<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="nint"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<nint?>? values)
    {
        return (values as IEnumerable<nint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="nint"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<nint>? values)
    {
        return (values as IEnumerable<nint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(nuint?[]? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(nuint[]? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(List<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(List<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(Queue<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(Stack<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(Collection<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="nuint"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<nuint?>? values)
    {
        return (values as IEnumerable<nuint?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="nuint"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<nuint>? values)
    {
        return (values as IEnumerable<nuint>).ToArrayParameter();
    }

    #endregion

    #region IntervalArray

    /// <summary>
    /// Implicitly converts an array of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The array of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(TimeSpan?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The array of TimeSpan values to convert.</param>
    public static implicit operator Parameter(TimeSpan[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ArraySegment<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(HashSet<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of TimeSpan values to convert.</param>
    public static implicit operator Parameter(HashSet<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(LinkedList<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of TimeSpan values to convert.</param>
    public static implicit operator Parameter(LinkedList<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The List of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(List<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The List of TimeSpan values to convert.</param>
    public static implicit operator Parameter(List<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The Queue of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(Queue<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The Queue of TimeSpan values to convert.</param>
    public static implicit operator Parameter(Queue<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(SortedSet<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of TimeSpan values to convert.</param>
    public static implicit operator Parameter(SortedSet<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The Stack of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(Stack<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The Stack of TimeSpan values to convert.</param>
    public static implicit operator Parameter(Stack<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ImmutableList<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The Collection of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(Collection<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The Collection of TimeSpan values to convert.</param>
    public static implicit operator Parameter(Collection<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of TimeSpan? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<TimeSpan?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of TimeSpan values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<TimeSpan>? values)
    {
        return values.ToArrayParameter();
    }

    #endregion

    #region NumericArray

    /// <summary>
    /// Implicitly converts an array of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(decimal?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(decimal[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ArraySegment<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(HashSet<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(LinkedList<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(List<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(List<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(Queue<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(SortedSet<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(Stack<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableList<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(Collection<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="decimal"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<decimal?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="decimal"/> values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<decimal>? values)
    {
        return values.ToArrayParameter();
    }

    #endregion

    #region TimeArray

    /// <summary>
    /// Implicitly converts an array of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The array of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(TimeOnly?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The array of TimeOnly values to convert.</param>
    public static implicit operator Parameter(TimeOnly[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ArraySegment<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(HashSet<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of TimeOnly values to convert.</param>
    public static implicit operator Parameter(HashSet<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(LinkedList<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of TimeOnly values to convert.</param>
    public static implicit operator Parameter(LinkedList<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The List of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(List<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The List of TimeOnly values to convert.</param>
    public static implicit operator Parameter(List<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The Queue of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(Queue<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The Queue of TimeOnly values to convert.</param>
    public static implicit operator Parameter(Queue<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(SortedSet<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of TimeOnly values to convert.</param>
    public static implicit operator Parameter(SortedSet<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The Stack of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(Stack<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The Stack of TimeOnly values to convert.</param>
    public static implicit operator Parameter(Stack<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableList<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The Collection of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(Collection<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The Collection of TimeOnly values to convert.</param>
    public static implicit operator Parameter(Collection<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of TimeOnly? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<TimeOnly?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of TimeOnly values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<TimeOnly>? values)
    {
        return values.ToArrayParameter();
    }

    #endregion

    #region TimestampTzArray

    /// <summary>
    /// Implicitly converts an array of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The array of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(DateTime?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The array of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(DateTime[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ArraySegment<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ArraySegment<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(HashSet<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(HashSet<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(LinkedList<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(LinkedList<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The List of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(List<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The List of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(List<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Queue of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(Queue<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Queue of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(Queue<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(SortedSet<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(SortedSet<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Stack of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(Stack<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Stack of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(Stack<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableArray<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableArray<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableList<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableList<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableStack<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableStack<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Collection of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(Collection<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Collection of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(Collection<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of DateTime? values (UTC) to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<DateTime?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of DateTime values (UTC) to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<DateTime>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The array of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(DateTimeOffset?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The array of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(DateTimeOffset[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ArraySegment<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ArraySegment<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(HashSet<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(HashSet<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(LinkedList<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(LinkedList<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The List of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(List<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The List of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(List<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Queue of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(Queue<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Queue of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(Queue<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(SortedSet<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(SortedSet<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Stack of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(Stack<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Stack of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(Stack<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableArray<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableArray<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableList<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableList<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableStack<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ImmutableStack<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Collection of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(Collection<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The Collection of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(Collection<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of DateTimeOffset? values (UTC) to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<DateTimeOffset?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of DateTimeOffset values (UTC) to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<DateTimeOffset>? values)
    {
        return values.ToArrayParameter();
    }

    #endregion

    #region UuidArray

    /// <summary>
    /// Implicitly converts an array of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The array of Guid? values to convert.</param>
    public static implicit operator Parameter(Guid?[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The array of Guid values to convert.</param>
    public static implicit operator Parameter(Guid[]? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of Guid? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of Guid values to convert.</param>
    public static implicit operator Parameter(ArraySegment<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of Guid? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of Guid values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of Guid? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of Guid values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of Guid? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of Guid values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of Guid? values to convert.</param>
    public static implicit operator Parameter(HashSet<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of Guid values to convert.</param>
    public static implicit operator Parameter(HashSet<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of Guid? values to convert.</param>
    public static implicit operator Parameter(LinkedList<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of Guid values to convert.</param>
    public static implicit operator Parameter(LinkedList<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The List of Guid? values to convert.</param>
    public static implicit operator Parameter(List<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The List of Guid values to convert.</param>
    public static implicit operator Parameter(List<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The Queue of Guid? values to convert.</param>
    public static implicit operator Parameter(Queue<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The Queue of Guid values to convert.</param>
    public static implicit operator Parameter(Queue<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of Guid? values to convert.</param>
    public static implicit operator Parameter(SortedSet<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of Guid values to convert.</param>
    public static implicit operator Parameter(SortedSet<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The Stack of Guid? values to convert.</param>
    public static implicit operator Parameter(Stack<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The Stack of Guid values to convert.</param>
    public static implicit operator Parameter(Stack<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of Guid? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of Guid values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of Guid? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of Guid values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of Guid? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of Guid values to convert.</param>
    public static implicit operator Parameter(ImmutableList<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of Guid? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of Guid values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of Guid? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of Guid values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of Guid? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of Guid values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The Collection of Guid? values to convert.</param>
    public static implicit operator Parameter(Collection<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The Collection of Guid values to convert.</param>
    public static implicit operator Parameter(Collection<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of Guid? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<Guid?>? values)
    {
        return values.ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of Guid values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<Guid>? values)
    {
        return values.ToArrayParameter();
    }

    #endregion

    #region VarcharArray

    /// <summary>
    /// Implicitly converts an array of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(string?[]? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(HashSet<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(LinkedList<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(List<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(Queue<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(SortedSet<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(Stack<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(Collection<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="string"/>? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<string?>? values)
    {
        return (values as IEnumerable<string?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The array of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(char[]?[]? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(HashSet<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(LinkedList<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The List of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(List<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Queue of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(Queue<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(SortedSet<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Stack of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(Stack<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Collection of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(Collection<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of <see langword="char"/>[]? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<char[]?>? values)
    {
        return (values as IEnumerable<char[]?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The array of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyMemory<char>?[]? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The array of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ReadOnlyMemory<char>[]? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(HashSet<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(HashSet<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(LinkedList<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(LinkedList<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The List of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(List<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The List of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(List<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Queue of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(Queue<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Queue of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(Queue<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(SortedSet<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(SortedSet<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Stack of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(Stack<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Stack of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(Stack<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Collection of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(Collection<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Collection of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(Collection<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of ReadOnlyMemory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of ReadOnlyMemory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ReadOnlyMemory<char>?>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of ReadOnlyMemory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of ReadOnlyMemory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ReadOnlyMemory<char>>? values)
    {
        return (values as IEnumerable<ReadOnlyMemory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The array of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(Memory<char>?[]? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The array of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(Memory<char>[]? values)
    {
        return (values as IEnumerable<Memory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ArraySegment<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(HashSet<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(HashSet<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(LinkedList<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(LinkedList<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The List of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(List<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The List of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(List<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Queue of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(Queue<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Queue of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(Queue<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(SortedSet<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(SortedSet<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Stack of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(Stack<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Stack of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(Stack<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableList<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Collection of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(Collection<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Collection of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(Collection<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of Memory&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of Memory&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<Memory<char>?>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of Memory&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of Memory&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<Memory<char>>? values)
    {
        return (values as IEnumerable<Memory<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The array of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<char>?[]? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an array of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The array of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ArraySegment<char>[]? values)
    {
        return (values as IEnumerable<ArraySegment<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ArraySegment of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ArraySegment of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ArraySegment<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentBag<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentBag of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentBag of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentQueue of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentQueue of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentQueue<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ConcurrentStack of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ConcurrentStack of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ConcurrentStack<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(HashSet<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a HashSet of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The HashSet of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(HashSet<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(LinkedList<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a LinkedList of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The LinkedList of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(LinkedList<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The List of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(List<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a List of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The List of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(List<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Queue of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(Queue<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Queue of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Queue of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(Queue<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(SortedSet<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a SortedSet of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The SortedSet of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(SortedSet<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Stack of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(Stack<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Stack of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Stack of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(Stack<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableArray of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableArray of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableArray<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableHashSet of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableHashSet of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableHashSet<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableList of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableList of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableList<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableQueue of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableQueue of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableQueue<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableSortedSet of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableSortedSet of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableSortedSet<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts an ImmutableStack of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ImmutableStack of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ImmutableStack<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Collection of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(Collection<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a Collection of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The Collection of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(Collection<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of ArraySegment&lt;char&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of ArraySegment&lt;char&gt;? values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ArraySegment<char>?>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyCollection of ArraySegment&lt;char&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The ReadOnlyCollection of ArraySegment&lt;char&gt; values to convert.</param>
    public static implicit operator Parameter(ReadOnlyCollection<ArraySegment<char>>? values)
    {
        return (values as IEnumerable<ArraySegment<char>?>).ToArrayParameter();
    }

    #endregion
}
