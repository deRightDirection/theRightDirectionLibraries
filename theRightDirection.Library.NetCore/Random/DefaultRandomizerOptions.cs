﻿namespace theRightDirection.Random
{
    /// <summary>
    /// Default randomizer options.
    /// </summary>
    public class DefaultRandomizerOptions : IRandomizerOptions
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="DefaultRandomizerOptions"/> has numbers.
        /// </summary>
        /// <value><c>true</c> if has numbers; otherwise, <c>false</c>.</value>
        public bool HasNumbers { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="DefaultRandomizerOptions"/> has
        /// lower alphabets.
        /// </summary>
        /// <value><c>true</c> if has lower alphabets; otherwise, <c>false</c>.</value>
        public bool HasLowerAlphabets { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="DefaultRandomizerOptions"/> has
        /// upper alphabets.
        /// </summary>
        /// <value><c>true</c> if has upper alphabets; otherwise, <c>false</c>.</value>
        public bool HasUpperAlphabets { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="DefaultRandomizerOptions"/> has
        /// special chars.
        /// </summary>
        /// <value><c>true</c> if has special chars; otherwise, <c>false</c>.</value>
        public bool HasSpecialChars { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRandomizerOptions"/> class.
        /// </summary>
        /// <param name="hasNumbers">If set to <c>true</c> has numbers.</param>
        /// <param name="hasUpperAlphabets">If set to <c>true</c> has upper alphabets.</param>
        /// <param name="hasLowerAlphabets">If set to <c>true</c> has lower alphabets.</param>
        /// <param name="hasSpecialChars">If set to <c>true</c> has special chars.</param>
        public DefaultRandomizerOptions(bool hasNumbers = true, bool hasUpperAlphabets = true, bool hasLowerAlphabets = false, bool hasSpecialChars = false)
        {
            HasNumbers = hasNumbers;
            HasUpperAlphabets = hasUpperAlphabets;
            HasLowerAlphabets = hasLowerAlphabets;
            HasSpecialChars = hasSpecialChars;
        }
    }
}
