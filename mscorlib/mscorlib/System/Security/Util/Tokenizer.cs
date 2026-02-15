using System;
using System.IO;
using System.Text;

namespace System.Security.Util
{
	// Token: 0x0200032B RID: 811
	internal sealed class Tokenizer
	{
		// Token: 0x06001CF3 RID: 7411 RVA: 0x00071948 File Offset: 0x0006FB48
		internal void BasicInitialization()
		{
			this.LineNo = 1;
			this._inProcessingTag = 0;
			this._inSavedCharacter = -1;
			this._inIndex = 0;
			this._inSize = 0;
			this._inNestedSize = 0;
			this._inNestedIndex = 0;
			this._inTokenSource = Tokenizer.TokenSource.Other;
			this._maker = SharedStatics.GetSharedStringMaker();
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00071998 File Offset: 0x0006FB98
		public void Recycle()
		{
			SharedStatics.ReleaseSharedStringMaker(ref this._maker);
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000719A5 File Offset: 0x0006FBA5
		internal Tokenizer(string input)
		{
			this.BasicInitialization();
			this._inString = input;
			this._inSize = input.Length;
			this._inTokenSource = Tokenizer.TokenSource.String;
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x000719D0 File Offset: 0x0006FBD0
		internal void ChangeFormat(Encoding encoding)
		{
			if (encoding == null)
			{
				return;
			}
			Tokenizer.TokenSource tokenSource = this._inTokenSource;
			if (tokenSource > Tokenizer.TokenSource.ASCIIByteArray)
			{
				if (tokenSource - Tokenizer.TokenSource.CharArray <= 2)
				{
					return;
				}
			}
			else
			{
				if (encoding == Encoding.Unicode)
				{
					this._inTokenSource = Tokenizer.TokenSource.UnicodeByteArray;
					return;
				}
				if (encoding == Encoding.UTF8)
				{
					this._inTokenSource = Tokenizer.TokenSource.UTF8ByteArray;
					return;
				}
				if (encoding == Encoding.ASCII)
				{
					this._inTokenSource = Tokenizer.TokenSource.ASCIIByteArray;
					return;
				}
			}
			tokenSource = this._inTokenSource;
			Stream stream;
			if (tokenSource > Tokenizer.TokenSource.ASCIIByteArray)
			{
				if (tokenSource - Tokenizer.TokenSource.CharArray <= 2)
				{
					return;
				}
				Tokenizer.StreamTokenReader streamTokenReader = this._inTokenReader as Tokenizer.StreamTokenReader;
				if (streamTokenReader == null)
				{
					return;
				}
				stream = streamTokenReader._in.BaseStream;
				string text = new string(' ', streamTokenReader.NumCharEncountered);
				stream.Position = (long)streamTokenReader._in.CurrentEncoding.GetByteCount(text);
			}
			else
			{
				stream = new MemoryStream(this._inBytes, this._inIndex, this._inSize - this._inIndex);
			}
			this._inTokenReader = new Tokenizer.StreamTokenReader(new StreamReader(stream, encoding));
			this._inTokenSource = Tokenizer.TokenSource.Other;
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x00071AB8 File Offset: 0x0006FCB8
		internal void GetTokens(TokenizerStream stream, int maxNum, bool endAfterKet)
		{
			while (maxNum == -1 || stream.GetTokenCount() < maxNum)
			{
				int num = 0;
				bool flag = false;
				bool flag2 = false;
				Tokenizer.StringMaker maker = this._maker;
				maker._outStringBuilder = null;
				maker._outIndex = 0;
				int num2;
				for (;;)
				{
					if (this._inSavedCharacter != -1)
					{
						num2 = this._inSavedCharacter;
						this._inSavedCharacter = -1;
					}
					else
					{
						switch (this._inTokenSource)
						{
						case Tokenizer.TokenSource.UnicodeByteArray:
							if (this._inIndex + 1 >= this._inSize)
							{
								goto Block_3;
							}
							num2 = ((int)this._inBytes[this._inIndex + 1] << 8) + (int)this._inBytes[this._inIndex];
							this._inIndex += 2;
							break;
						case Tokenizer.TokenSource.UTF8ByteArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_4;
							}
							byte[] inBytes = this._inBytes;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inBytes[num3];
							if ((num2 & 128) != 0)
							{
								switch ((num2 & 240) >> 4)
								{
								case 8:
								case 9:
								case 10:
								case 11:
									goto IL_012D;
								case 12:
								case 13:
									num2 &= 31;
									num = 2;
									break;
								case 14:
									num2 &= 15;
									num = 3;
									break;
								case 15:
									goto IL_014B;
								}
								if (this._inIndex >= this._inSize)
								{
									goto Block_7;
								}
								byte[] inBytes2 = this._inBytes;
								num3 = this._inIndex;
								this._inIndex = num3 + 1;
								byte b = inBytes2[num3];
								if ((b & 192) != 128)
								{
									goto Block_8;
								}
								num2 = (num2 << 6) | (int)(b & 63);
								if (num != 2)
								{
									if (this._inIndex >= this._inSize)
									{
										goto Block_10;
									}
									byte[] inBytes3 = this._inBytes;
									num3 = this._inIndex;
									this._inIndex = num3 + 1;
									b = inBytes3[num3];
									if ((b & 192) != 128)
									{
										goto Block_11;
									}
									num2 = (num2 << 6) | (int)(b & 63);
								}
							}
							break;
						}
						case Tokenizer.TokenSource.ASCIIByteArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_12;
							}
							byte[] inBytes4 = this._inBytes;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inBytes4[num3];
							break;
						}
						case Tokenizer.TokenSource.CharArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_13;
							}
							char[] inChars = this._inChars;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inChars[num3];
							break;
						}
						case Tokenizer.TokenSource.String:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_14;
							}
							string inString = this._inString;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = (int)inString[num3];
							break;
						}
						case Tokenizer.TokenSource.NestedStrings:
						{
							int num3;
							if (this._inNestedSize != 0)
							{
								if (this._inNestedIndex < this._inNestedSize)
								{
									string inNestedString = this._inNestedString;
									num3 = this._inNestedIndex;
									this._inNestedIndex = num3 + 1;
									num2 = (int)inNestedString[num3];
									break;
								}
								this._inNestedSize = 0;
							}
							if (this._inIndex >= this._inSize)
							{
								goto Block_17;
							}
							string inString2 = this._inString;
							num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = (int)inString2[num3];
							if (num2 == 123)
							{
								for (int i = 0; i < this._searchStrings.Length; i++)
								{
									if (string.Compare(this._searchStrings[i], 0, this._inString, this._inIndex - 1, this._searchStrings[i].Length, StringComparison.Ordinal) == 0)
									{
										this._inNestedString = this._replaceStrings[i];
										this._inNestedSize = this._inNestedString.Length;
										this._inNestedIndex = 1;
										num2 = (int)this._inNestedString[0];
										this._inIndex += this._searchStrings[i].Length - 1;
										break;
									}
								}
							}
							break;
						}
						default:
							num2 = this._inTokenReader.Read();
							if (num2 == -1)
							{
								goto Block_21;
							}
							break;
						}
					}
					if (!flag)
					{
						if (num2 <= 34)
						{
							switch (num2)
							{
							case 9:
							case 13:
								continue;
							case 10:
								this.LineNo++;
								continue;
							case 11:
							case 12:
								break;
							default:
								switch (num2)
								{
								case 32:
									continue;
								case 33:
									if (this._inProcessingTag != 0)
									{
										goto Block_32;
									}
									break;
								case 34:
									flag = true;
									flag2 = true;
									continue;
								}
								break;
							}
						}
						else if (num2 != 45)
						{
							if (num2 != 47)
							{
								switch (num2)
								{
								case 60:
									goto IL_048A;
								case 61:
									goto IL_04C0;
								case 62:
									goto IL_04A4;
								case 63:
									if (this._inProcessingTag != 0)
									{
										goto Block_31;
									}
									break;
								}
							}
							else if (this._inProcessingTag != 0)
							{
								goto Block_30;
							}
						}
						else if (this._inProcessingTag != 0)
						{
							goto Block_33;
						}
					}
					else if (num2 <= 34)
					{
						switch (num2)
						{
						case 9:
						case 13:
							break;
						case 10:
							this.LineNo++;
							if (!flag2)
							{
								goto Block_46;
							}
							goto IL_062F;
						case 11:
						case 12:
							goto IL_062F;
						default:
							if (num2 != 32)
							{
								if (num2 != 34)
								{
									goto IL_062F;
								}
								if (flag2)
								{
									goto Block_44;
								}
								goto IL_062F;
							}
							break;
						}
						if (!flag2)
						{
							goto Block_45;
						}
					}
					else
					{
						if (num2 != 47)
						{
							if (num2 != 60)
							{
								if (num2 - 61 > 1)
								{
									goto IL_062F;
								}
							}
							else
							{
								if (!flag2)
								{
									goto Block_41;
								}
								goto IL_062F;
							}
						}
						if (!flag2 && this._inProcessingTag != 0)
						{
							goto Block_43;
						}
					}
					IL_062F:
					flag = true;
					if (maker._outIndex < 512)
					{
						char[] outChars = maker._outChars;
						Tokenizer.StringMaker stringMaker = maker;
						int num3 = stringMaker._outIndex;
						stringMaker._outIndex = num3 + 1;
						outChars[num3] = (ushort)num2;
					}
					else
					{
						if (maker._outStringBuilder == null)
						{
							maker._outStringBuilder = new StringBuilder();
						}
						maker._outStringBuilder.Append(maker._outChars, 0, 512);
						maker._outChars[0] = (char)num2;
						maker._outIndex = 1;
					}
				}
				IL_048A:
				this._inProcessingTag++;
				stream.AddToken(0);
				continue;
				Block_3:
				stream.AddToken(-1);
				return;
				IL_04A4:
				this._inProcessingTag--;
				stream.AddToken(1);
				if (endAfterKet)
				{
					return;
				}
				continue;
				IL_04C0:
				stream.AddToken(4);
				continue;
				Block_30:
				stream.AddToken(2);
				continue;
				Block_31:
				stream.AddToken(5);
				continue;
				Block_32:
				stream.AddToken(6);
				continue;
				Block_33:
				stream.AddToken(7);
				continue;
				Block_41:
				this._inSavedCharacter = num2;
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_43:
				this._inSavedCharacter = num2;
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_44:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_45:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_46:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_4:
				stream.AddToken(-1);
				return;
				IL_012D:
				throw new XmlSyntaxException(this.LineNo);
				IL_014B:
				throw new XmlSyntaxException(this.LineNo);
				Block_7:
				throw new XmlSyntaxException(this.LineNo, Environment.GetResourceString("Unexpected end of file."));
				Block_8:
				throw new XmlSyntaxException(this.LineNo);
				Block_10:
				throw new XmlSyntaxException(this.LineNo, Environment.GetResourceString("Unexpected end of file."));
				Block_11:
				throw new XmlSyntaxException(this.LineNo);
				Block_12:
				stream.AddToken(-1);
				return;
				Block_13:
				stream.AddToken(-1);
				return;
				Block_14:
				stream.AddToken(-1);
				return;
				Block_17:
				stream.AddToken(-1);
				return;
				Block_21:
				stream.AddToken(-1);
				return;
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x00072182 File Offset: 0x00070382
		private string GetStringToken()
		{
			return this._maker.MakeString();
		}

		// Token: 0x04000D48 RID: 3400
		public int LineNo;

		// Token: 0x04000D49 RID: 3401
		private int _inProcessingTag;

		// Token: 0x04000D4A RID: 3402
		private byte[] _inBytes;

		// Token: 0x04000D4B RID: 3403
		private char[] _inChars;

		// Token: 0x04000D4C RID: 3404
		private string _inString;

		// Token: 0x04000D4D RID: 3405
		private int _inIndex;

		// Token: 0x04000D4E RID: 3406
		private int _inSize;

		// Token: 0x04000D4F RID: 3407
		private int _inSavedCharacter;

		// Token: 0x04000D50 RID: 3408
		private Tokenizer.TokenSource _inTokenSource;

		// Token: 0x04000D51 RID: 3409
		private Tokenizer.ITokenReader _inTokenReader;

		// Token: 0x04000D52 RID: 3410
		private Tokenizer.StringMaker _maker;

		// Token: 0x04000D53 RID: 3411
		private string[] _searchStrings;

		// Token: 0x04000D54 RID: 3412
		private string[] _replaceStrings;

		// Token: 0x04000D55 RID: 3413
		private int _inNestedIndex;

		// Token: 0x04000D56 RID: 3414
		private int _inNestedSize;

		// Token: 0x04000D57 RID: 3415
		private string _inNestedString;

		// Token: 0x0200032C RID: 812
		private enum TokenSource
		{
			// Token: 0x04000D59 RID: 3417
			UnicodeByteArray,
			// Token: 0x04000D5A RID: 3418
			UTF8ByteArray,
			// Token: 0x04000D5B RID: 3419
			ASCIIByteArray,
			// Token: 0x04000D5C RID: 3420
			CharArray,
			// Token: 0x04000D5D RID: 3421
			String,
			// Token: 0x04000D5E RID: 3422
			NestedStrings,
			// Token: 0x04000D5F RID: 3423
			Other
		}

		// Token: 0x0200032D RID: 813
		[Serializable]
		internal sealed class StringMaker
		{
			// Token: 0x06001CF9 RID: 7417 RVA: 0x00072190 File Offset: 0x00070390
			private static uint HashString(string str)
			{
				uint num = 0U;
				int length = str.Length;
				for (int i = 0; i < length; i++)
				{
					num = (num << 3) ^ (uint)str[i] ^ (num >> 29);
				}
				return num;
			}

			// Token: 0x06001CFA RID: 7418 RVA: 0x000721C4 File Offset: 0x000703C4
			private static uint HashCharArray(char[] a, int l)
			{
				uint num = 0U;
				for (int i = 0; i < l; i++)
				{
					num = (num << 3) ^ (uint)a[i] ^ (num >> 29);
				}
				return num;
			}

			// Token: 0x06001CFB RID: 7419 RVA: 0x000721ED File Offset: 0x000703ED
			public StringMaker()
			{
				this.cStringsMax = 2048U;
				this.cStringsUsed = 0U;
				this.aStrings = new string[this.cStringsMax];
				this._outChars = new char[512];
			}

			// Token: 0x06001CFC RID: 7420 RVA: 0x00072228 File Offset: 0x00070428
			private bool CompareStringAndChars(string str, char[] a, int l)
			{
				if (str.Length != l)
				{
					return false;
				}
				for (int i = 0; i < l; i++)
				{
					if (a[i] != str[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06001CFD RID: 7421 RVA: 0x0007225C File Offset: 0x0007045C
			public string MakeString()
			{
				char[] outChars = this._outChars;
				int outIndex = this._outIndex;
				if (this._outStringBuilder != null)
				{
					this._outStringBuilder.Append(this._outChars, 0, this._outIndex);
					return this._outStringBuilder.ToString();
				}
				uint num3;
				if (this.cStringsUsed > this.cStringsMax / 4U * 3U)
				{
					uint num = this.cStringsMax * 2U;
					string[] array = new string[num];
					int num2 = 0;
					while ((long)num2 < (long)((ulong)this.cStringsMax))
					{
						if (this.aStrings[num2] != null)
						{
							num3 = Tokenizer.StringMaker.HashString(this.aStrings[num2]) % num;
							while (array[(int)num3] != null)
							{
								if ((num3 += 1U) >= num)
								{
									num3 = 0U;
								}
							}
							array[(int)num3] = this.aStrings[num2];
						}
						num2++;
					}
					this.cStringsMax = num;
					this.aStrings = array;
				}
				num3 = Tokenizer.StringMaker.HashCharArray(outChars, outIndex) % this.cStringsMax;
				string text;
				while ((text = this.aStrings[(int)num3]) != null)
				{
					if (this.CompareStringAndChars(text, outChars, outIndex))
					{
						return text;
					}
					if ((num3 += 1U) >= this.cStringsMax)
					{
						num3 = 0U;
					}
				}
				text = new string(outChars, 0, outIndex);
				this.aStrings[(int)num3] = text;
				this.cStringsUsed += 1U;
				return text;
			}

			// Token: 0x04000D60 RID: 3424
			private string[] aStrings;

			// Token: 0x04000D61 RID: 3425
			private uint cStringsMax;

			// Token: 0x04000D62 RID: 3426
			private uint cStringsUsed;

			// Token: 0x04000D63 RID: 3427
			public StringBuilder _outStringBuilder;

			// Token: 0x04000D64 RID: 3428
			public char[] _outChars;

			// Token: 0x04000D65 RID: 3429
			public int _outIndex;
		}

		// Token: 0x0200032E RID: 814
		internal interface ITokenReader
		{
			// Token: 0x06001CFE RID: 7422
			int Read();
		}

		// Token: 0x0200032F RID: 815
		internal class StreamTokenReader : Tokenizer.ITokenReader
		{
			// Token: 0x06001CFF RID: 7423 RVA: 0x00072387 File Offset: 0x00070587
			internal StreamTokenReader(StreamReader input)
			{
				this._in = input;
				this._numCharRead = 0;
			}

			// Token: 0x06001D00 RID: 7424 RVA: 0x0007239D File Offset: 0x0007059D
			public virtual int Read()
			{
				int num = this._in.Read();
				if (num != -1)
				{
					this._numCharRead++;
				}
				return num;
			}

			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06001D01 RID: 7425 RVA: 0x000723BC File Offset: 0x000705BC
			internal int NumCharEncountered
			{
				get
				{
					return this._numCharRead;
				}
			}

			// Token: 0x04000D66 RID: 3430
			internal StreamReader _in;

			// Token: 0x04000D67 RID: 3431
			internal int _numCharRead;
		}
	}
}
