using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PerfectPasswordGenerator
{
	internal sealed class PasswordGenerator
	{
		#region Fields

		private static char[] _symbols =
		{
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
			'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
			'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'.', ',', '!', '?', '+', '-', '*', '/', '\\',
			'\'', '`', '"', ':', ';',
			'.', ',', '+', '-', '*', '/', '?', '!', '@', '#', '$', '%', '^', '&', '(', ')'
		};

		private static char[] _vowel = { 'a', 'e', 'i', 'o', 'u', 'y' };

		private IDictionary<char, IList<char>> _pairs = new Dictionary<char, IList<char>>();
		private RNGCryptoServiceProvider _random = new RNGCryptoServiceProvider();

		#endregion

		#region Methods

		private bool IsVowel(char symbol)
		{
			return Array.IndexOf<char>(_vowel, char.ToLower(symbol)) != -1;
		}

		private int GetMobileKey(char symbol)
		{
			switch (char.ToLower(symbol))
			{
				case '1':
					return 1;
				case '2':
				case 'a':
				case 'b':
				case 'c':
					return 2;
				case '3':
				case 'd':
				case 'e':
				case 'f':
					return 3;
				case '4':
				case 'g':
				case 'h':
				case 'i':
					return 4;
				case '5':
				case 'j':
				case 'k':
				case 'l':
					return 5;
				case '6':
				case 'm':
				case 'n':
				case 'o':
					return 6;
				case '7':
				case 'p':
				case 'q':
				case 'r':
				case 's':
					return 7;
				case '8':
				case 't':
				case 'u':
				case 'v':
					return 8;
				case '9':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
					return 9;
				case '0':
					return 0;
				default:
					return -1;
			}
		}

		private bool IsAllowedSymbol(
			char prevSymbol,
			char nextSymbol,
			bool useLowerCaseLetters,
			bool useUpperCaseLetters,
			bool useDigits,
			bool useSpecialSymbols,
			bool generatePronounceable,
			bool generateUnambiguous,
			bool generateMobileFriendly)
		{
			if ((prevSymbol >= 'a') && (prevSymbol <= 'z'))
			{
				if (!useLowerCaseLetters)
				{
					return false;
				}
			}
			else if ((prevSymbol >= 'A') && (prevSymbol <= 'Z'))
			{
				if (!useUpperCaseLetters)
				{
					return false;
				}
			}
			else if ((prevSymbol >= '0') && (prevSymbol <= '9'))
			{
				if (!useDigits)
				{
					return false;
				}
			}
			else
			{
				if (!useSpecialSymbols)
				{
					return false;
				}
			}

			if ((nextSymbol >= 'a') && (nextSymbol <= 'z'))
			{
				if (!useLowerCaseLetters)
				{
					return false;
				}
			}
			else if ((nextSymbol >= 'A') && (nextSymbol <= 'Z'))
			{
				if (!useUpperCaseLetters)
				{
					return false;
				}
			}
			else if ((nextSymbol >= '0') && (nextSymbol <= '9'))
			{
				if (!useDigits)
				{
					return false;
				}
			}
			else
			{
				if (!useSpecialSymbols)
				{
					return false;
				}
			}

			if (generatePronounceable)
			{
				if (char.IsLetter(prevSymbol) && char.IsLetter(nextSymbol))
				{
					if (!(IsVowel(prevSymbol) ^ IsVowel(nextSymbol)))
					{
						return false;
					}
				}
			}

			if (generateUnambiguous)
			{
				switch (char.ToLower(prevSymbol))
				{
					case '1':
					case '0':
					case 'i':
					case 'j':
					case 'l':
					case 'o':
					case 'q':
						return false;
				}

				switch (char.ToLower(nextSymbol))
				{
					case '1':
					case '0':
					case 'i':
					case 'j':
					case 'l':
					case 'o':
					case 'q':
						return false;
				}
			}

			if (generateMobileFriendly)
			{
				if (GetMobileKey(prevSymbol) == GetMobileKey(nextSymbol))
				{
					return false;
				}
			}

			return true;
		}

		public PasswordGenerator(
			bool useLowerCaseLetters,
			bool useUpperCaseLetters,
			bool useDigits,
			bool useSpecialSymbols,
			bool generatePronounceable,
			bool generateUnambiguous,
			bool generateMobileFriendly)
		{
			foreach (char prevSymbol in _symbols)
			{
				List<char> list = new List<char>();

				foreach (char nextSymbol in _symbols)
				{
					if (IsAllowedSymbol(
						prevSymbol,
						nextSymbol,
						useLowerCaseLetters,
						useUpperCaseLetters,
						useDigits,
						useSpecialSymbols,
						generatePronounceable,
						generateUnambiguous,
						generateMobileFriendly))
					{
						list.Add(nextSymbol);
					}
				}

				if (list.Count > 0)
				{
					this._pairs[prevSymbol] = list;
				}
			}
		}

		public string Generate(int passwordLength)
		{
			if (passwordLength < 1)
			{
				throw new ArgumentException("passwordLength is too small.");
			}

			if (passwordLength > 100)
			{
				throw new ArgumentException("passwordLength is too big.");
			}

			byte[] rawRandomDataValue = new byte[1024];
			int rawRandomDataOffset = 1;
			long randomData = 0;

			this._random.GetBytes(rawRandomDataValue);

			char[] result = new char[passwordLength];

			while (randomData < (long)256 * 256 * 256 * 256)
			{
				randomData = (randomData << 8) | rawRandomDataValue[rawRandomDataOffset];
				++rawRandomDataOffset;
			}

			if (this._pairs.Keys.Count == 0)
			{
				return null;
			}

			int symbolIndex = (int)(randomData % this._pairs.Keys.Count);

			randomData /= this._pairs.Keys.Count;

			foreach (char symbol in this._pairs.Keys)
			{
				if (symbolIndex == 0)
				{
					result[0] = symbol;
					break;
				}

				--symbolIndex;
			}

			for (int index = 1; index < result.Length; ++index)
			{
				while (randomData < (long)256 * 256 * 256 * 256)
				{
					randomData = (randomData << 8) | rawRandomDataValue[rawRandomDataOffset];
					++rawRandomDataOffset;
				}

				IList<char> symbols = _pairs[result[index - 1]];

				if (symbols.Count == 0)
				{
					return null;
				}

				result[index] = symbols[(int)(randomData % symbols.Count)];
				randomData /= symbols.Count;
			}

			return new string(result);
		}

		public double GetStrengthRating(int passwordLength)
		{
			if (this._pairs.Values.Count == 0)
			{
				return 0;
			}
			else
			{
				double symbolPermutations = 1;

				foreach (IList<char> paths in this._pairs.Values)
				{
					symbolPermutations *= paths.Count;
				}

				double symbolsPerPosition = Math.Exp(Math.Log(symbolPermutations) / this._pairs.Values.Count);

				return Math.Log(Math.Pow(symbolsPerPosition, passwordLength));
			}
		}

		#endregion
	}
}
