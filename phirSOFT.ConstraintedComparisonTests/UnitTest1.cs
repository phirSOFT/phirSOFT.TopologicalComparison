using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using phirSOFT.TopologicalComparison;
using static System.String;

namespace phirSOFT.ConstraintedComparisonTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var s = new List<AbsoluteTopological<int>>();

            var rnd = new Random();

            var a = new List<int>();
            var b = new List<int>();

            for (int i = 0; i < 10; i++)
            {

                a.Add(rnd.Next());
                b.Add(rnd.Next());

                foreach (var i1 in b)
                {
                    if (i1 < a.Last())
                    {
                        ((AbsoluteTopological<int>)i1).Before(a.Last());
                    }
                    else
                    {
                        ((AbsoluteTopological<int>)i1).After(a.Last());
                    }
                }

                foreach (var i1 in a)
                {
                    if (i1 < b.Last())
                    {
                        ((AbsoluteTopological<int>)i1).Before(b.Last());
                    }
                    else
                    {
                        ((AbsoluteTopological<int>)i1).After(b.Last());
                    }
                }
            }

            s.AddRange(a.Select(aa => (AbsoluteTopological<int>)aa));
            s.AddRange(b.Select(bb => (AbsoluteTopological<int>)bb));

            var c = TopologicalComparer<AbsoluteTopological<int>>.Default;
            var cint = Comparer<int>.Default;

            for (int i = 0; i < s.Count; i++)
            {
                for (int j = i; j < s.Count; j++)
                {
                    var aa = s[i];
                    var bb = s[j];

                    Assert.AreEqual((a.Contains(aa) && b.Contains(bb)) || (a.Contains(bb) && b.Contains(aa)), c.CanCompare(aa, bb));
                    if (c.CanCompare(aa, bb))
                        Assert.AreEqual(cint.Compare(aa, bb), c.Compare(aa, bb));

                }
            }

            var sorted = new List<AbsoluteTopological<int>>();
            foreach (var z in s)
            {
                sorted.Insert(z);
            }
        }

        [TestMethod]
        public void TestInsertion()
        {
            var list = new List<AbsoluteTopological<int>>
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9
            };

            ((AbsoluteTopological<int>)1).Before(4);
            ((AbsoluteTopological<int>)1).Before(9);

            ((AbsoluteTopological<int>)2).Before(5);
            ((AbsoluteTopological<int>)2).Before(6);

            ((AbsoluteTopological<int>)3).Before(6);
            ((AbsoluteTopological<int>)3).Before(7);

            ((AbsoluteTopological<int>)5).Before(6);
            ((AbsoluteTopological<int>)5).Before(7);

            ((AbsoluteTopological<int>)6).Before(8);
            ((AbsoluteTopological<int>)6).Before(9);

            ((AbsoluteTopological<int>)8).Before(9);

            var rnd = new Random();

            var sorted = new List<AbsoluteTopological<int>>();
            var insertOrder = new List<int>();

            var report = new StringBuilder();

            while (list.Count > 0)
            {
                var index = rnd.Next(list.Count);
                var item = list[index];
                list.RemoveAt(index);

                sorted.Insert(item);
                insertOrder.Add(item);


                report.AppendFormat(" -- >> {{{0}}}\n", Join(", ", sorted));
            }

            Assert.IsTrue(sorted.IndexOf(1) < sorted.IndexOf(4), "p(1) < p(4)\n -- p(1) = {0}, p(4) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(1), sorted.IndexOf(4), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);
            Assert.IsTrue(sorted.IndexOf(1) < sorted.IndexOf(9), "p(1) < p(9)\n -- p(1) = {0}, p(9) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(1), sorted.IndexOf(9), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);

            Assert.IsTrue(sorted.IndexOf(2) < sorted.IndexOf(5), "p(2) < p(5)\n -- p(2) = {0}, p(5) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(2), sorted.IndexOf(5), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);
            Assert.IsTrue(sorted.IndexOf(2) < sorted.IndexOf(6), "p(2) < p(6)\n -- p(2) = {0}, p(6) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(2), sorted.IndexOf(6), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);

            Assert.IsTrue(sorted.IndexOf(3) < sorted.IndexOf(6), "p(3) < p(6)\n -- p(3) = {0}, p(6) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(3), sorted.IndexOf(6), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);
            Assert.IsTrue(sorted.IndexOf(3) < sorted.IndexOf(7), "p(3) < p(7)\n -- p(3) = {0}, p(7) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(3), sorted.IndexOf(7), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);

            Assert.IsTrue(sorted.IndexOf(5) < sorted.IndexOf(6), "p(5) < p(6)\n -- p(5) = {0}, p(6) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(5), sorted.IndexOf(6), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);
            Assert.IsTrue(sorted.IndexOf(5) < sorted.IndexOf(7), "p(5) < p(7)\n -- p(5) = {0}, p(7) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(5), sorted.IndexOf(7), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);

            Assert.IsTrue(sorted.IndexOf(6) < sorted.IndexOf(8), "p(6) < p(8)\n -- p(6) = {0}, p(8) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(6), sorted.IndexOf(8), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);
            Assert.IsTrue(sorted.IndexOf(6) < sorted.IndexOf(9), "p(6) < p(9)\n -- p(6) = {0}, p(9) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(6), sorted.IndexOf(9), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);

            Assert.IsTrue(sorted.IndexOf(8) < sorted.IndexOf(9), "p(8) < p(9)\n -- p(8) = {0}, p(9) = {1}\n -- List Dump = {2}\n -- Insert Order = {3}\n --- Insertion Log ---\n{4}", sorted.IndexOf(8), sorted.IndexOf(9), "{" + Join(", ", sorted) + "}", "{" + Join(", ", insertOrder) + "}", report);
        }

        [TestMethod]
        [Ignore]
        public void CircleTest()
        {
            CircleTest(5);
            CircleTest(11);
        }
        public void CircleTest(int m)
        {
            var list = new List<Circle>();

            for (var i = 0; i < m; i++)
            {
                list.Insert(new Circle((3 * i) % m, m));
            }

            CollectionAssert.AreEqual(Enumerable.Range(0, m).ToList(), list.Select(i => i.m).ToList());

        }

        [TestMethod]
        public void TestInsertion01()
        {
            ((AbsoluteTopological<int>)1).Before(4);
            ((AbsoluteTopological<int>)1).Before(9);

            ((AbsoluteTopological<int>)2).Before(5);
            ((AbsoluteTopological<int>)2).Before(6);

            ((AbsoluteTopological<int>)3).Before(6);
            ((AbsoluteTopological<int>)3).Before(7);

            ((AbsoluteTopological<int>)5).Before(6);
            ((AbsoluteTopological<int>)5).Before(7);

            ((AbsoluteTopological<int>)6).Before(8);
            ((AbsoluteTopological<int>)6).Before(9);

            ((AbsoluteTopological<int>)8).Before(9);

            var l = new List<AbsoluteTopological<int>>()
            {
                2,
                9,
                5
            };
            l.Insert(6);

            Assert.AreEqual(0, l.IndexOf(2));
            Assert.AreEqual(1, l.IndexOf(5));
            Assert.AreEqual(2, l.IndexOf(6));
            Assert.AreEqual(3, l.IndexOf(9));
        }

        [TestMethod]
        public void TestInsertion02()
        {
            ((AbsoluteTopological<int>)1).Before(4);
            ((AbsoluteTopological<int>)1).Before(9);

            ((AbsoluteTopological<int>)2).Before(5);
            ((AbsoluteTopological<int>)2).Before(6);

            ((AbsoluteTopological<int>)3).Before(6);
            ((AbsoluteTopological<int>)3).Before(7);

            ((AbsoluteTopological<int>)5).Before(6);
            ((AbsoluteTopological<int>)5).Before(7);

            ((AbsoluteTopological<int>)6).Before(8);
            ((AbsoluteTopological<int>)6).Before(9);

            ((AbsoluteTopological<int>)8).Before(9);

            var l = new List<AbsoluteTopological<int>>()
            {
               1, 8, 3,2 , 5
            };
            l.Insert(6);

            Assert.AreEqual(0, l.IndexOf(1));
            
        }

        private class Circle : ITopologicalComparable<Circle>
        {
            public readonly int m;
            private readonly int mod;

            public Circle(int m, int mod)
            {
                this.m = m;
                this.mod = mod;
            }

            public int CompareTo(Circle other)
            {
                var tmp = m - other.m;
                if (tmp < 0)
                    tmp += mod;

                if (tmp > mod / 2)
                    tmp = -(mod - tmp);

                return tmp;
            }

            public bool CanCompareTo(Circle other)
            {
                return mod == other.mod;
            }

            public override string ToString()
            {
                return $"{m} mod {mod}";
            }
        }

    }

    internal class AbsoluteTopological<T> : ITopologicalComparable<AbsoluteTopological<T>>
    {
        public T Value { get; }

        private static readonly Dictionary<T, AbsoluteTopological<T>> Constraints = new Dictionary<T, AbsoluteTopological<T>>();

        private readonly List<AbsoluteTopological<T>> _before = new List<AbsoluteTopological<T>>();

        private readonly List<AbsoluteTopological<T>> _after = new List<AbsoluteTopological<T>>();


        public AbsoluteTopological(T value)
        {
            Value = value;
            Constraints.Add(value, this);
        }


        public void Before(AbsoluteTopological<T> other)
        {
            other._after.Add(this);
            _before.Add(other);
        }

        public void After(AbsoluteTopological<T> other)
        {
            other._before.Add(this);
            _after.Add(other);
        }

        public int CompareTo(AbsoluteTopological<T> other)
        {
            if (_after.Contains(other))
                return 1;
            if (_before.Contains(other))
                return -1;
            return 0;
        }

        public bool CanCompareTo(AbsoluteTopological<T> other)
        {
            return _after.Contains(other) || _before.Contains(other);
        }

        public static implicit operator T(AbsoluteTopological<T> val)
        {
            return val.Value;
        }

        public static implicit operator AbsoluteTopological<T>(T val)
        {
            if (Constraints.ContainsKey(val))
                return Constraints[val];
            var c = new AbsoluteTopological<T>(val);
            return c;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
