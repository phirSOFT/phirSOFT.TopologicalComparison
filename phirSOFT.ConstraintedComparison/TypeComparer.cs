using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace phirSOFT.TopologicalComparison
{
    /// <summary>
    /// Compares types based on ther assignability. A type A is considerd less than type B, if the A is an ancestor of B in the type hirachy. 
    /// </summary>
    public class TypeComparer : ITopologicalComparer<Type>, ITopologicalComparer
    {
        public int Compare(Type x, Type y)
        {
            // Todo: cache results
            var infoX = x.GetTypeInfo();
            var infoY = y.GetTypeInfo();

            return (infoY.IsAssignableFrom(infoX) ? 1 : 0) - (infoX.IsAssignableFrom(infoY) ? 1 : 0);
        }

        public bool CanCompare(Type x, Type y)
        {
            // Todo: cache results
            var infoX = x.GetTypeInfo();
            var infoY = y.GetTypeInfo();

            return infoY.IsAssignableFrom(infoX) || infoX.IsAssignableFrom(infoY);
        }

        public int Compare(object x, object y)
        {
            return Compare((Type)x, (Type)y);
        }

        public bool CanCompare(object x, object y)
        {
            return x is Type typeX && y is Type typeY && CanCompare(typeX, typeY);
        }
    }
}
