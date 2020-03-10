using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    public class RSAProvider
    {
        private ASCIIEncoding ASCIIEncoder      { get; set; }
        private RSACryptoServiceProvider RSAalg { get; set; }
        public RSAProvider()
        {
            this.ASCIIEncoder = new ASCIIEncoding();
            this.RSAalg       = new RSACryptoServiceProvider();
        }
        //@TODO generate a mongo compatible string
        public string Encrypt(string data)
            => data;
        public string Decrypt(string data)
            => data;
    }
}
