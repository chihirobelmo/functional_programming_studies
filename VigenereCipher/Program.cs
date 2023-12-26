using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VigenereCipher
{
	static internal class Program
	{
		static void Main(string[] args)
		{
			const string plainText = "I have come to bring fire on the earth, and how I wish it were already kindled!";

			const string key1 = "luke";
			const string key2 = "oshii";
			const string key3 = "mamoru";

			string cipher = plainText.Crypt(key1).Crypt(key2).Crypt(key3);
			string decipted = cipher.DeCrypt(key3).DeCrypt(key2).DeCrypt(key1);

			Console.WriteLine(cipher);
			Console.WriteLine(decipted);
			Console.ReadLine();
		}

		private static string DeCrypt(this string plainText, string key) => plainText.Crypt(key, true);
		private static string Crypt(this string plainText, string key, bool decipherMode = false) => plainText.CleanUp().Byte().Aggregate(
			string.Empty,
			(c, b) => c += b.CryptByte(key.OffsetValue(c.ToCharArray().Length) * (decipherMode ? -1 : 1)).FreqAlphabet().Ascii(),
			c => decipherMode ? c.ToLower() : c.ToUpper());

		const int ALPHA = 97;
		const int ZULU = 122;

		private static string CleanUp(this string text) => Regex.Replace(text, @"[^a-zA-Z]", "");
		private static byte[] Byte(this string plainText) => Encoding.ASCII.GetBytes(plainText.ToLower());
		private static int CryptByte(this byte b, int offset) => b + offset;
		private static int OffsetValue(this string key, int count) => Encoding.ASCII.GetBytes(key).ElementAt(count % key.Length) - ALPHA;
		private static int FreqAlphabet(this int b) => b - (b > ZULU ? ZULU - ALPHA : b < ALPHA ? -ZULU + ALPHA : 0);
		private static string Ascii(this int b) => ((char)(byte)b).ToString();
	}
}
