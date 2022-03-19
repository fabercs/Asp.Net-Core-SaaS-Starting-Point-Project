using System.Text;

namespace EMSApp.Shared
{
    /// <summary>
    /// Options used by <see cref="IEncryptionService"/>.
    /// </summary>
    public class EmsEncryptionOptions
    {
        /// <summary>
        /// This constant is used to determine the keysize of the encryption algorithm.
        /// Default value: 256.
        /// </summary>
        public int Keysize { get; set; }

        /// <summary>
        /// Default password to encrypt/decrypt texts.
        /// It's recommended to set to another value for security.
        /// Default value: "fbKzBrkhnK2Nr789"
        /// </summary>
        public string DefaultPassPhrase { get; set; }

        /// <summary>
        /// This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        /// This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        /// 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        /// Default value: Encoding.ASCII.GetBytes("hg5T42we38Ytb6v1")
        /// </summary>
        public byte[] InitVectorBytes { get; set; }

        /// <summary>
        /// Default value: Encoding.ASCII.GetBytes("f12Bd34!")
        /// </summary>
        public byte[] DefaultSalt { get; set; }

        public EmsEncryptionOptions()
        {
            Keysize = 256;
            DefaultPassPhrase = "fbKzBrkhnK2Nr789";
            InitVectorBytes = Encoding.ASCII.GetBytes("hg5T42we38Ytb6v1");
            DefaultSalt = Encoding.ASCII.GetBytes("f12Bd34!");
        }
    }
}
