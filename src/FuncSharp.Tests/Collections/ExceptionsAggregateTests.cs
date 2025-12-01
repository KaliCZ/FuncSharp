using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FuncSharp.Tests.Collections;

public class CustomException : Exception
{
    public CustomException(string message) : base(message) { }
    public bool Equals(CustomException? other) => Message.Equals(other?.Message);
    public override bool Equals(object? obj) => Equals(obj as CustomException);
    public override int GetHashCode() => Message.GetHashCode();
}

public class ExceptionsAggregateTests
{
    [Fact]
    public void Aggregate_Empty()
    {
        IEnumerable<Exception> enumerable = Enumerable.Empty<Exception>();
        Exception[] array = new Exception[]{};

        Assert.Null(enumerable.Aggregate());
        Assert.Null(array.Aggregate());
    }

    [Fact]
    public void Aggregate_Single()
    {
        var singleException = new CustomException("A potato here.");

        IEnumerable<Exception> enumerable = Enumerable.Repeat(singleException, 1);
        CustomException[] array = new []{singleException};
        var nonEmpty = array.AsNonEmpty()!;

        Assert.Equal(singleException, enumerable.Aggregate());
        Assert.Equal(singleException, array.Aggregate());
        Assert.IsType<CustomException>(nonEmpty.Aggregate());
    }

    [Fact]
    public void Aggregate_Multiple()
    {
        IEnumerable<Exception> enumerable = Enumerable.Range(0, 10).Select(i => new CustomException($"{i} potatoes")).ToArray();
        CustomException[] array = Enumerable.Range(0, 10).Select(i => new CustomException($"{i} potatoes")).ToArray();
        INonEmptyEnumerable<Exception> nonEmpty = array.AsNonEmpty()!;

        Assert.NotNull(enumerable.Aggregate());
        Assert.Equal(array, ((AggregateException)enumerable.Aggregate()!).InnerExceptions);

        Assert.NotNull(array.Aggregate());
        Assert.Equal(array, ((AggregateException)array.Aggregate()!).InnerExceptions);

        Assert.NotNull(nonEmpty.Aggregate());
        Assert.Equal(array, ((AggregateException)nonEmpty.Aggregate()).InnerExceptions);
    }
}