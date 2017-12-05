// partial port of https://github.com/kelektiv/node-uuid/blob/master/lib/v35.js

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekLearning.Primitives
{
    public class GuidV5
    {

        private const byte Version = 0x50;

        // Pre-defined namespaces, per Appendix C
        public const string DNSString = "6ba7b810-9dad-11d1-80b4-00c04fd430c8";
        public static readonly Guid DNS = Guid.Parse(DNSString);
        //public static readonly byte[] DNSBytes = DNS.ToByteArray(); 
        public const string URLString = "6ba7b811-9dad-11d1-80b4-00c04fd430c8";
        public static readonly Guid URL = Guid.Parse(URLString);
        //public static readonly byte[] URLBytes = URL.ToByteArray();


        private static byte[] FastStringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        private static byte[] GuidToBytes(Guid uuid)
        {
            return FastStringToByteArray(uuid.ToString("N"));
        }

        private static byte[] StringToBytes(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        public static string ByteArrayToString(IEnumerable<byte> bytes)
        {
            StringBuilder hex = new StringBuilder(32);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static Guid CreateGuid(string value, Guid @namespace)
        {
            return CreateGuid(StringToBytes(value), GuidToBytes(@namespace));
        }

        public static Guid CreateGuid(byte[] value, byte[] @namespace)
        {
            if (@namespace.Length != 16)
            {
                throw new ArgumentException("Invalid namespace length", nameof(@namespace));
            }

            var hash = System.Security.Cryptography.SHA1.Create();

            var bytes = hash.ComputeHash(@namespace.Concat(value).ToArray());

            bytes[6] = (byte)((bytes[6] & 0x0f) | Version);
            bytes[8] = (byte)((bytes[8] & 0x3f) | 0x80);

            return Guid.Parse(ByteArrayToString(bytes.Take(16)));
        }
    }
}
