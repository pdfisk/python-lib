using System.Text;

namespace PythonLib.util
{
    public class UnicodeUtil
    {
        public static string ConvertUnicodeToAscii(string unicodeString)
        {
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;
            byte[] unicodeBytes = PackUnicode(unicode.GetBytes(unicodeString));
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);
            return ascii.GetString(asciiBytes);
        }

        static byte[] PackUnicode(byte[] unicodeBytes)
        {
            byte[] asciiBytes = new byte[unicodeBytes.Length / 2];
            int dstOffset = 0;
            for (int srcOffset = 0; srcOffset < unicodeBytes.Length;)
            {
                asciiBytes[dstOffset++] = (byte)(unicodeBytes[srcOffset++] & 0x7F);
                asciiBytes[dstOffset++] = (byte)(unicodeBytes[srcOffset++] & 0x7F);
                srcOffset += 2;
            }
            return asciiBytes;
        }

        public static string ConvertAsciiToUnicode(string asciiString)
        {
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;
            byte[] asciiBytes = ascii.GetBytes(asciiString);
            byte[] unicodeBytes = Encoding.Convert(ascii, unicode, asciiBytes);
            return unicode.GetString(unicodeBytes);
        }
    }
}
