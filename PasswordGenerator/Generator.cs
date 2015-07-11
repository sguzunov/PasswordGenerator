namespace PasswordRandomGenerator
{

    /// <summary>
    /// Provides a random password generator.Each password contains low case, up case, numbers and special characters.
    /// </summary>
    public static class Generator
    {
        private const int DEFAULT_PASSWORD_LENGTH = 10;
        private const string LOWCASE_PASS_CHARACTERS = "abcdefgijkmnopqrstwxyz";
        private const string UPCASE_PASS_CHARACTERS = "ABCDEFGHJKLMNPQRSTWXYZ";
        private const string NUMERIC_PASS_CHARACTERS = "23456789";
        private const string SPECIAL_PASS_CHARACTERS = "*$-+?_&=!%{}/";

        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <returns>A string randomly generated password.</returns>
        public static string Generate()
        {
            return Generate(DEFAULT_PASSWORD_LENGTH);
        }

        /// <summary>
        /// Generates a random password with a default length.
        /// </summary>
        /// <param name="length">The exact length of password.</param>
        /// <returns>Randomly generated password.</returns>
        private static string Generate(int length)
        {
            // A local array containing the supported password characters grouped by types.
            char[][] charGroups = new char[][]
            {
                LOWCASE_PASS_CHARACTERS.ToCharArray(),
                UPCASE_PASS_CHARACTERS.ToCharArray(),
                NUMERIC_PASS_CHARACTERS.ToCharArray(),
                SPECIAL_PASS_CHARACTERS.ToCharArray()
            };

            // An array to track the number of unused characters in each char group.
            int[] charsLeftInGroup = new int[charGroups.Length];

            // Firstly all the character in each group are available
            for (int i = 0; i < charsLeftInGroup.Length; i++)
            {
                charsLeftInGroup[i] = charGroups[i].Length;
            }

            // An array to track over unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Firstly all character groups are not used
            for (int i = 0; i < leftGroupsOrder.Length; i++)
            {
                leftGroupsOrder[i] = i;
            }

            char[] password = new char[length];
            int nextCharIndex;
            int nextGroupIndex;
            int nextLeftGroupsOrderIndex;
            int lastCharIndex;

            // Index of the last non-processed group.
            int lastLeftGroupsOrderIndex = leftGroupsOrder.Length - 1;

            // Generates the password character one at a time.
            for (int i = 0; i < password.Length; i++)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list. To allow a special character to appear in the
                // first position, increment the second parameter of the Next
                // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                if (lastLeftGroupsOrderIndex == 0)
                {
                    nextLeftGroupsOrderIndex = 0;
                }
                else
                {
                    nextLeftGroupsOrderIndex = RandomNumberGenerator.GetRandomNumber(0, lastLeftGroupsOrderIndex);
                }

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIndex = leftGroupsOrder[nextLeftGroupsOrderIndex];

                // Get the index of the last unprocessed character in this group.
                lastCharIndex = charsLeftInGroup[nextGroupIndex] - 1;

                if (lastCharIndex == 0)
                {
                    nextCharIndex = 0;
                }
                else
                {
                    nextCharIndex = RandomNumberGenerator.GetRandomNumber(0, lastCharIndex + 1);
                }

                password[i] = charGroups[nextGroupIndex][nextCharIndex];

                if (lastCharIndex == 0)
                {
                    charsLeftInGroup[nextGroupIndex] = charGroups[nextGroupIndex].Length;
                }
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIndex != nextGroupIndex)
                    {
                        char oldValue = charGroups[nextGroupIndex][lastCharIndex];
                        charGroups[nextGroupIndex][lastCharIndex] = charGroups[nextGroupIndex][nextCharIndex];
                        charGroups[nextGroupIndex][nextCharIndex] = oldValue;
                    }

                    charsLeftInGroup[nextGroupIndex]--;
                }

                // If the last group is processed, start all over.
                if (lastLeftGroupsOrderIndex == 0)
                {
                    lastLeftGroupsOrderIndex = leftGroupsOrder.Length - 1;
                }
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIndex != nextGroupIndex)
                    {
                        int oldValue = leftGroupsOrder[lastLeftGroupsOrderIndex];
                        leftGroupsOrder[lastLeftGroupsOrderIndex] = leftGroupsOrder[nextLeftGroupsOrderIndex];
                        leftGroupsOrder[nextLeftGroupsOrderIndex] = oldValue;
                    }

                    lastLeftGroupsOrderIndex--;
                }
            }

            return new string(password);
        }
    }
}