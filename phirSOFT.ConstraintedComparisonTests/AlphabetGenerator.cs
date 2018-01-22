using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phirSOFT.ConstraintedComparisonTests
{
    internal class AlphabetGenerator<T>
    {
        private readonly IEnumerable<T> _alphabet;
        private readonly T _empty;
        private readonly Func<T, T, T> _concatFunc;

        public AlphabetGenerator(IEnumerable<T> alphabet, T empty, Func<T, T, T> concatFunc)
        {
            _alphabet = alphabet;
            _empty = empty;
            _concatFunc = concatFunc;
        }

        public IEnumerable<T> EnumerateWords(int maxLengh)
        {
            var q = new Queue<(T, int)>();
            q.Enqueue((_empty, 0));

            while (q.Count > 0)
            {
                var word = q.Dequeue();
                yield return word.Item1;

                if (word.Item2 >= maxLengh) continue;

                foreach (var letter in _alphabet)
                {
                    q.Enqueue((_concatFunc(word.Item1, letter), word.Item2 + 1));
                }
            }
        }
    }
}
