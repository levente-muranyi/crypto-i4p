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
