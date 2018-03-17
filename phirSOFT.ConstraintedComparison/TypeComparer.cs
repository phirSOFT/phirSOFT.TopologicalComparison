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
        // Todo: provide a larger, customazible cache.
        private (Type, Type)? _lastKey;
        private int _result = 0;
        private readonly object _locker = new object();

        public int Compare(Type x, Type y)
        {
            var result = CompareInternal(x, y);
            if(result == null)
                throw new InvalidOperationException();

            return result.Value;
        }

        public bool CanCompare(Type x, Type y)
        {
            return CompareInternal(x,y) != null;
        }

        private int? CompareInternal(Type x, Type y)
        {
            lock (_locker)
            {
                if (_lastKey.HasValue && _lastKey.Value.Item1 == x && _lastKey.Value.Item2 == y)
                    return _result;
            }

            var infoX = x.GetTypeInfo();
            var infoY = y.GetTypeInfo();


            var ySuperX = infoY.IsAssignableFrom(infoX);
            var xSuperY = infoX.IsAssignableFrom(infoY);

            if (!ySuperX && !xSuperY)
            {
                return null;
            }

            lock (_locker)
            {
                _lastKey = (x, y);
                _result = (ySuperX ? 1 : 0) - (xSuperY ? 1 : 0);
                return _result;
            }
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
