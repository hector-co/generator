using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;

namespace Generator.Templates
{
    public static class Extensions
    {
        public static string GetVariablePluralName(this string name) => (name[..1].ToLower() + name[1..]).Pluralize();

        public static string GetVariableName(this string name) => name[..1].ToLower() + name[1..];

        public static string GetExtension(this string value)
        {
            return !value.Contains('.') ? value : value[(value.LastIndexOf(".", StringComparison.Ordinal) + 1)..];
        }

        public static IEnumerable<(T item, int index, int count)> WithIndex<T>(this IEnumerable<T> source)
        {
            var enumerable = source as T[] ?? source.ToArray();
            return enumerable.Select((item, index) => (item, index, enumerable.Length));
        }
    }
}
