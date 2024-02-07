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
        public Crypter()
        {
            InitDictKV(charSetKV);
            InitDictVK(charSetVK);
        }

        private void InitDictKV(Dictionary<int, char> dict)
        {
            for (int i = 0; i <= 25; i++)
            {
                dict.Add(i, (char)('a' + i));
            }
            dict.Add(26, ' ');
        }

        private void InitDictVK(Dictionary<char, int> dict)
        {
            for (int i = 0; i <= 25; i++)
            {
                dict.Add((char)('a' + i), i);
            }
            dict.Add(' ', 26);
        }

        public string EncryptMessage(string message, string key)
        {
            string encryptedMessage = "";
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
