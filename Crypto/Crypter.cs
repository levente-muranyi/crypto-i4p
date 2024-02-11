using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    public class Crypter
    {
        private static Dictionary<int, char> charSetKV = new Dictionary<int, char>();
        private static Dictionary<char, int> charSetVK = new Dictionary<char, int>();
        private static List<string> words = new List<string>();

        public Crypter()
        {
            InitDictionaries(charSetKV, charSetVK);
            InitWordlist(words);
        }

        /// <summary>
        /// Initializes dictionaries for later use.
        /// </summary>
        /// <param name="keyValue">Dictionary that holds KeyValue pairs. Used for obtaining a char given an integer value.</param>
        /// <param name="valueKey">Dictionary that holds ValueKey pairs. Used for giving each char an integer value.</param>
        private void InitDictionaries(Dictionary<int, char> keyValue, Dictionary<char, int> valueKey)
        {
            for (int i = 0; i <= 25; i++)
            {
                keyValue.Add(i, (char)('a' + i));
                valueKey.Add((char)('a' + i), i);
            }
            keyValue.Add(26, ' ');
            valueKey.Add(' ', 26);
        }

        /// <summary>
        /// Initializes the wordlist for later use.
        /// </summary>
        /// <param name="wordlist">A list which holds English words only.</param>
        private void InitWordlist(List<string> wordlist)
        {
            using (StreamReader reader = new StreamReader("../../../data/words.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    wordlist.Add(line);
                }
            }
        }

        /// <summary>
        /// Encrypts the message using the given key.
        /// </summary>
        /// <param name="message">Message to be encrypted.</param>
        /// <param name="key">Key used for encryption.</param>
        /// <returns>The encrypted message which cannot be longer than the length of the <paramref name="key"/>.</returns>
        public string EncryptMessage(string message, string key)
        {
            string encryptedMessage = "";
            if (key.Length < message.Length) message = message.Substring(0, key.Length);

            for (int i = 0; i < message.Length; i++)
            {
                int newCharKey = (charSetVK[message[i]] + charSetVK[key[i % key.Length]]) % 27;
                encryptedMessage += charSetKV[newCharKey];
            }

            return encryptedMessage;
        }

        /// <summary>
        /// Decrypts the encrypted message using the given key.
        /// </summary>
        /// <param name="encryptedMessage">Message to be decrypted.</param>
        /// <param name="key">Key used for decryption.</param>
        /// <returns>The decrypted message which cannot be longer than the length of the <paramref name="key"/>.</returns>
        public string DecryptMessage(string encryptedMessage, string key)
        {
            string decryptedMessage = "";
            if (key.Length < encryptedMessage.Length) encryptedMessage = encryptedMessage.Substring(0, key.Length);
            
            for (int i = 0; i < encryptedMessage.Length; i++)
            {
                int oldCharKey = (charSetVK[encryptedMessage[i]] - charSetVK[key[i % key.Length]]) % 27;
                if (oldCharKey < 0) oldCharKey = 27 + oldCharKey;
                decryptedMessage += charSetKV[oldCharKey];
            }

            return decryptedMessage;
        }
    }
}
