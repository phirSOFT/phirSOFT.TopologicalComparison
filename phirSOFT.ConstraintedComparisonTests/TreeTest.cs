using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ConstraintedComparisonTests
{
    [TestClass]
    public class TreeTest
    {
        private static readonly Random _rnd = new Random(100);

        [TestMethod]
        public void TestReverseTreeSort_ArrayList()
        {
            string[] input =
            {
                "A-1-1",
                "A-1-2",
                "A-2-1",
                "A-2-2",
                "A-2",
                "B-1-1",
                "B-1-2",
                "B-2-1",
                "B-2-2",
                "A-1",
                "B-1",
                "B-2",
                "B",
                "A"
            };

            var output = new List<string>();
            var comparer = new SubstringComparer<string, char>();

            foreach (var value in input) output.Insert(value, comparer);

            Assert.IsTrue(IsSorted(output, comparer));
        }

        [TestMethod]
        public void TestReverseTreeSort_LinkedList()
        {
            string[] input =
            {
                "A-1-1",
                "A-1-2",
                "A-2-1",
                "A-2-2",
                "A-2",
                "B-1-1",
                "B-1-2",
                "B-2-1",
                "B-2-2",
                "A-1",
                "B-1",
                "B-2",
                "B",
                "A"
            };

            var output = new LinkedList<string>();
            var comparer = new SubstringComparer<string, char>();

            foreach (var value in input) output.Insert(value, comparer);

            Assert.IsTrue(IsSorted(output, comparer));
        }

        private bool IsSorted<T>(IEnumerable<T> items, ITopologicalComparer<T> comparison)
        {
            var itemArray = items.ToArray();
            for (var i = 0; i < itemArray.Length; i++)
            for (var j = 0; j < i; j++)
            {
                if (!comparison.TryCompare(itemArray[i], itemArray[j], out var result)) continue;

                if (result < 0) return false;
            }

            return true;
        }

        [TestMethod]
        public void TestForwardTree()
        {
            var tree = new ForwardLinkedTree<string>("");

            var words = new AlphabetGenerator<string>(new[] { "0", "1" }, "", (s, s1) => s + s1).EnumerateWords(4)
                .ToList();

            var processed = words.Count;
            while (words.Count > 0)
            {
                var word = GetRandom(words);
                words.Remove(word);
                tree.Insert(word, new TreeComparer());
            }

            var q = new Queue<ITreeNode<string>>();
            q.Enqueue(tree.Root);


            while (q.Count > 0)
            {
                processed--;
                var word = q.Dequeue();

                if (word.Value.Length < 4)
                {
                    Assert.IsTrue(word.Children.Any(c => c.Value == word.Value + "0"));
                    Assert.IsTrue(word.Children.Any(c => c.Value == word.Value + "1"));
                }


                foreach (var child in word.Children) q.Enqueue(child);
            }

            Assert.AreEqual(0, processed);
        }

        private static T GetRandom<T>(IList<T> list)
        {
            var max = list.Count;

            var random = list[_rnd.Next(max)];
            Debug.WriteLine($"Requested radom tree item. Returned: '{random}'");
            return random;
        }

        [TestMethod]
        public void TestAlphabet()
        {
            var alphabet = new AlphabetGenerator<string>(new[] { "0", "1" }, "", (s, s1) => s + s1);

            var result = alphabet.EnumerateWords(3);
            var expected = new[]
                { "", "0", "1", "00", "01", "10", "11", "000", "001", "010", "011", "100", "101", "110", "111" };
            CollectionAssert.AreEquivalent(expected, result.ToList());
        }
    }
}