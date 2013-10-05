using System;
using System.Linq;

namespace DocumentSystem
{
    abstract class EncryptableDocument: BinaryDocument, IEncryptable
    {
        private bool isEncrypted = false;
        public bool IsEncrypted
        {
            get
            {
                return isEncrypted;
            }
        }

        public EncryptableDocument(string[] attributes)
            : base(attributes)
        {
        }

        public void Encrypt()
        {
            this.isEncrypted = true;
        }

        public void Decrypt()
        {
            this.isEncrypted = false;
        }
    }
}
