namespace Crypto
{
    public class Tests
    {

        Crypter crypt = new Crypter();

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        [TestCase("helloworld", "abcdefgijkl", "hfnosauzun")]
        [TestCase("stays the same", "aaaaaaaaaaaaaa", "stays the same")]
        [TestCase("key is shorter", "bb", "lf")]
        [TestCase("not stronger than rsa", "qoamplyoaoanmyhoqrbnxgmrois", "cbtlgdobnuedlqoocqsex")]
        [TestCase("muranyilevente", "anoebnyniuenasg", "mgeeokfymoi tw")]
        public void TestEncryptMessage(string message, string key, string encryptedMessage)
        {
            Assert.That(encryptedMessage, Is.EqualTo(crypt.EncryptMessage(message, key)));
        }

        [Test]
        [TestCase("hfnosauzun", "abcdefgijkl", "helloworld")]
        [TestCase("stays the same", "aaaaaaaaaaaaaa", "stays the same")]
        [TestCase("lf", "bb", "ke")]
        [TestCase("cbtlgdobnuedlqoocqsex", "qoamplyoaoanmyhoqrbnxgmrois", "not stronger than rsa")]
        [TestCase("mgeeokfymoi tw", "anoebnyniuenasg", "muranyilevente")]
        public void TestDecryptMessage(string encryptedMessage, string key, string decryptedMessage)
        {
            Assert.That(decryptedMessage, Is.EqualTo(crypt.DecryptMessage(encryptedMessage, key)));
        }
    }
}