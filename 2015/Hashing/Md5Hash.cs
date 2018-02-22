namespace Hashing
{
	using System;
	using System.Linq;
	using System.Text;
	using System.Collections.Generic;

	using Rounds;

	public class Md5Hash
	{
		private const int BytesInInt = 4;

		private const int BytesInChunk = 64;

		private const int IntsInChunk = Md5Hash.BytesInChunk / Md5Hash.BytesInInt;

		private static readonly IRound[] Rounds = { new FirstRound(), new SecondRound(), new ThirdRound(), new FourthRound() };

		private static readonly int[] Constants =
		{
			-680876936,
			-389564586,
			606105819,
			-1044525330,
			-176418897,
			1200080426,
			-1473231341,
			-45705983,
			1770035416,
			-1958414417,
			-42063,
			-1990404162,
			1804603682,
			-40341101,
			-1502002290,
			1236535329,
			-165796510,
			-1069501632,
			643717713,
			-373897302,
			-701558691,
			38016083,
			-660478335,
			-405537848,
			568446438,
			-1019803690,
			-187363961,
			1163531501,
			-1444681467,
			-51403784,
			1735328473,
			-1926607734,
			-378558,
			-2022574463,
			1839030562,
			-35309556,
			-1530992060,
			1272893353,
			-155497632,
			-1094730640,
			681279174,
			-358537222,
			-722521979,
			76029189,
			-640364487,
			-421815835,
			530742520,
			-995338651,
			-198630844,
			1126891415,
			-1416354905,
			-57434055,
			1700485571,
			-1894986606,
			-1051523,
			-2054922799,
			1873313359,
			-30611744,
			-1560198380,
			1309151649,
			-145523070,
			-1120210379,
			718787259,
			-343485551
		};

		private Encoding encoding;

		private Md5Hash(Encoding encoding)
		{
			this.HashBytes = null;
			this.Hash = new[] { 1732584193, -271733879, -1732584194, 271733878 };
			this.Encoding = encoding;
		}

		private Md5Hash()
			: this(Encoding.Unicode)
		{
			;
		}

		public Md5Hash(byte[] data)
			: this()
		{
			this.GenerateHash(data);
		}

		public Md5Hash(string message)
			: this()
		{
			this.GenerateHash(message);
		}

		public Md5Hash(string message, Encoding encoding)
			: this(encoding)
		{
			this.GenerateHash(message);
		}

		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}

			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Encoding can't be null.");
				}

				this.encoding = value;
			}
		}

		private int[] Hash { get; set; }

		private byte[] HashBytes { get; set; }

		private int[] Words { get; set; }

		public byte[] GetBytes()
		{
			if (this.HashBytes == null)
			{
				this.SetHashBytes();
			}

			return this.HashBytes;
		}

		public override string ToString()
		{
			if (this.HashBytes == null)
			{
				this.SetHashBytes();
			}

			StringBuilder result = new StringBuilder();

			foreach (var byt in this.HashBytes)
			{
				result.Append(byt.ToString("x").PadLeft(2, '0'));
			}

			return result.ToString();
		}

		private void GenerateHash(string message)
		{
			this.GenerateHash(this.Encoding.GetBytes(message));
		}

		private void GenerateHash(byte[] data)
		{
			data = this.AddPadding(data);
			this.Words = this.GetWords(data);

			for (int chunk = 0; chunk < this.Words.Length; chunk += Md5Hash.IntsInChunk)
			{
				int[] chunkHash = this.GenerateChunkHash(chunk);

				for (int i = 0; i < this.Hash.Length; i++)
				{
					this.Hash[i] += chunkHash[i];
				}
			}
		}

		private int[] GenerateChunkHash(int chunkStart)
		{
			int[] chunkHash = (int[])this.Hash.Clone();
			for (int step = 0; step < 64; step++)
			{
				int round = step / 16;

				RoundResult result = Md5Hash.Rounds[round].Execute(chunkHash, step);

				result.WordIndex += chunkStart;

				int holder = chunkHash[3];
				chunkHash[3] = chunkHash[2];
				chunkHash[2] = chunkHash[1];
				chunkHash[1] += (chunkHash[0] + result.Value + Md5Hash.Constants[step] + this.Words[result.WordIndex]).LeftRotate(result.Rotation);
				chunkHash[0] = holder;
			}

			return chunkHash;
		}

		private byte[] AddPadding(byte[] data)
		{
			const byte LeftestBit = 128;

			var result = new List<byte>(data);

			result.Add(LeftestBit);

			int zeroPaddingLength = (Md5Hash.BytesInChunk - (result.Count % Md5Hash.BytesInChunk)) - 8;
			result.AddRange(new byte[zeroPaddingLength]);

			long messageBits = (long)data.Length * 8;

			IEnumerable<byte> lastBytes = BitConverter.GetBytes(messageBits);
			if (!BitConverter.IsLittleEndian)
			{
				lastBytes = lastBytes.Reverse();
			}

			result.AddRange(lastBytes);

			return result.ToArray();
		}

		private int[] GetWords(params byte[] bytes)
		{
			int[] result = new int[bytes.Length / Md5Hash.BytesInInt];

			for (int i = 0; i < result.Length; i++)
			{
				int start = i * Md5Hash.BytesInInt;
				if (!BitConverter.IsLittleEndian)
				{
					Array.Reverse(bytes, start, Md5Hash.BytesInInt);
				}

				result[i] = BitConverter.ToInt32(bytes, i * Md5Hash.BytesInInt);
			}

			return result;
		}

		private void SetHashBytes()
		{
			this.HashBytes = new byte[this.Hash.Length * Md5Hash.BytesInInt];

			for (int i = 0; i < this.Hash.Length; i++)
			{
				byte[] bytes = BitConverter.GetBytes(this.Hash[i]);
				if (!BitConverter.IsLittleEndian)
				{
					Array.Reverse(bytes);
				}

				Buffer.BlockCopy
				(
					src: bytes, 
					srcOffset: 0, 
					dst: this.HashBytes, 
					dstOffset: i * Md5Hash.BytesInInt, 
					count: bytes.Length
				);
			}
		}
	}
}