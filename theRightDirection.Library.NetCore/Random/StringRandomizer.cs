﻿using System;
using System.Collections.Generic;
using System.Text;

namespace theRightDirection.Random
{
    /// <summary>
    /// Randomizer.
    /// </summary>
    public class StringRandomizer
    {
        int _length;
        IRandomizerStore _store;
        IRandomizerOptions _options;

        string[] numArr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        string[] upperAlphaArr = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string[] lowerAlphaArr = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        string[] specialCharArr = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "+" };

        List<string[]> _bags = new List<string[]>();

        System.Random random = new System.Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="StringRandomizer.Randomizer"/> class.
        /// </summary>
        /// <param name="length">Length.</param>
        /// <param name="options">Options.</param>
        /// <param name="store">Store.</param>
        public StringRandomizer(int length = 10, IRandomizerOptions options = null, IRandomizerStore store = null)
        {
            _length = length;
            _options = options ?? new DefaultRandomizerOptions();
            _store = store;

            if (_options.HasNumbers)
                _bags.Add(numArr);

            if (_options.HasLowerAlphabets)
                _bags.Add(lowerAlphaArr);

            if (_options.HasUpperAlphabets)
                _bags.Add(upperAlphaArr);

            if (_options.HasSpecialChars)
                _bags.Add(specialCharArr);
        }

        /// <summary>
        /// Next random string.
        /// </summary>
        /// <returns>The next.</returns>
        public string Next()
        {
            return Generate();
        }

        private string Generate(int retry = 0)
        {
            if (retry > 20)
                throw new Exception("Cant generate string any more");

            StringBuilder randomStr = new StringBuilder(_length);

            var cnt = 0;

            while (cnt < _length)
            {
                var bagIndex = random.Next(0, _bags.Count);

                var bag = _bags[bagIndex];

                randomStr.Append(bag[random.Next(0, bag.Length)]);

                cnt++;
            }

            if (_store == null || _store.TryAdd(randomStr.ToString()))
                return randomStr.ToString();

            return Generate(++retry);
        }
    }
}
