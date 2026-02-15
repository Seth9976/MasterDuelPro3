using System;
using System.IO;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000065 RID: 101
	public class SchemaTokenCreator
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x00010CFC File Offset: 0x0000EEFC
		private void Initialise()
		{
			this.ctype = new sbyte[256];
			this.buf = new char[20];
			this.peekchar = int.MaxValue;
			this.WordCharacters(97, 122);
			this.WordCharacters(65, 90);
			this.WordCharacters(160, 255);
			this.WhitespaceCharacters(0, 32);
			this.CommentCharacter(47);
			this.QuoteCharacter(34);
			this.QuoteCharacter(39);
			this.parseNumbers();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00010D7C File Offset: 0x0000EF7C
		public SchemaTokenCreator(Stream instream)
		{
			this.Initialise();
			if (instream == null)
			{
				throw new NullReferenceException();
			}
			this.input = instream;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		public SchemaTokenCreator(StreamReader r)
		{
			this.Initialise();
			if (r == null)
			{
				throw new NullReferenceException();
			}
			this.reader = r;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00010DD4 File Offset: 0x0000EFD4
		public SchemaTokenCreator(StringReader r)
		{
			this.Initialise();
			if (r == null)
			{
				throw new NullReferenceException();
			}
			this.sreader = r;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00010E00 File Offset: 0x0000F000
		public void pushBack()
		{
			this.pushedback = true;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00010E09 File Offset: 0x0000F009
		public int CurrentLine
		{
			get
			{
				return this.linenumber;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00010E14 File Offset: 0x0000F014
		public string ToStringValue()
		{
			int num = this.lastttype;
			string text;
			switch (num)
			{
			case -5:
				text = this.StringValue;
				break;
			case -4:
			case -2:
				text = "n=" + this.NumberValue.ToString();
				break;
			case -3:
				text = this.StringValue;
				break;
			case -1:
				text = "EOF";
				break;
			default:
				if (num != 10)
				{
					if (this.lastttype < 256 && (this.ctype[this.lastttype] & 8) != 0)
					{
						text = this.StringValue;
					}
					else
					{
						char[] array = new char[3];
						array[0] = (array[2] = '\'');
						array[1] = (char)this.lastttype;
						text = new string(array);
					}
				}
				else
				{
					text = "EOL";
				}
				break;
			}
			return text;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00010ED1 File Offset: 0x0000F0D1
		public void WordCharacters(int min, int max)
		{
			if (min < 0)
			{
				min = 0;
			}
			if (max >= this.ctype.Length)
			{
				max = this.ctype.Length - 1;
			}
			while (min <= max)
			{
				sbyte[] array = this.ctype;
				int num = min++;
				array[num] |= 4;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00010F0E File Offset: 0x0000F10E
		public void WhitespaceCharacters(int min, int max)
		{
			if (min < 0)
			{
				min = 0;
			}
			if (max >= this.ctype.Length)
			{
				max = this.ctype.Length - 1;
			}
			while (min <= max)
			{
				this.ctype[min++] = 1;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00010F42 File Offset: 0x0000F142
		public void OrdinaryCharacters(int min, int max)
		{
			if (min < 0)
			{
				min = 0;
			}
			if (max >= this.ctype.Length)
			{
				max = this.ctype.Length - 1;
			}
			while (min <= max)
			{
				this.ctype[min++] = 0;
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00010F76 File Offset: 0x0000F176
		public void OrdinaryCharacter(int ch)
		{
			if (ch >= 0 && ch < this.ctype.Length)
			{
				this.ctype[ch] = 0;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00010F90 File Offset: 0x0000F190
		public void CommentCharacter(int ch)
		{
			if (ch >= 0 && ch < this.ctype.Length)
			{
				this.ctype[ch] = 16;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00010FAC File Offset: 0x0000F1AC
		public void InitTable()
		{
			int num = this.ctype.Length;
			while (--num >= 0)
			{
				this.ctype[num] = 0;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00010FD5 File Offset: 0x0000F1D5
		public void QuoteCharacter(int ch)
		{
			if (ch >= 0 && ch < this.ctype.Length)
			{
				this.ctype[ch] = 8;
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		public void parseNumbers()
		{
			for (int i = 48; i <= 57; i++)
			{
				sbyte[] array = this.ctype;
				int num = i;
				array[num] |= 2;
			}
			sbyte[] array2 = this.ctype;
			int num2 = 46;
			array2[num2] |= 2;
			sbyte[] array3 = this.ctype;
			int num3 = 45;
			array3[num3] |= 2;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00011044 File Offset: 0x0000F244
		private int read()
		{
			if (this.sreader != null)
			{
				return this.sreader.Read();
			}
			if (this.reader != null)
			{
				return this.reader.Read();
			}
			if (this.input != null)
			{
				return this.input.ReadByte();
			}
			throw new SystemException();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00011094 File Offset: 0x0000F294
		public int nextToken()
		{
			if (this.pushedback)
			{
				this.pushedback = false;
				return this.lastttype;
			}
			this.StringValue = null;
			int num = this.peekchar;
			if (num < 0)
			{
				num = int.MaxValue;
			}
			if (num == 2147483646)
			{
				num = this.read();
				if (num < 0)
				{
					return this.lastttype = -1;
				}
				if (num == 10)
				{
					num = int.MaxValue;
				}
			}
			if (num == 2147483647)
			{
				num = this.read();
				if (num < 0)
				{
					return this.lastttype = -1;
				}
			}
			this.lastttype = num;
			this.peekchar = int.MaxValue;
			int num2 = (int)((num < 256) ? this.ctype[num] : 4);
			while ((num2 & 1) != 0)
			{
				if (num == 13)
				{
					this.linenumber++;
					if (this.iseolsig)
					{
						this.peekchar = 2147483646;
						return this.lastttype = 10;
					}
					num = this.read();
					if (num == 10)
					{
						num = this.read();
					}
				}
				else
				{
					if (num == 10)
					{
						this.linenumber++;
						if (this.iseolsig)
						{
							return this.lastttype = 10;
						}
					}
					num = this.read();
				}
				if (num < 0)
				{
					return this.lastttype = -1;
				}
				num2 = (int)((num < 256) ? this.ctype[num] : 4);
			}
			if ((num2 & 2) != 0)
			{
				bool flag = false;
				if (num == 45)
				{
					num = this.read();
					if (num != 46 && (num < 48 || num > 57))
					{
						this.peekchar = num;
						return this.lastttype = 45;
					}
					flag = true;
				}
				double num3 = 0.0;
				int i = 0;
				int num4 = 0;
				for (;;)
				{
					if (num == 46 && num4 == 0)
					{
						num4 = 1;
					}
					else
					{
						if (48 > num || num > 57)
						{
							break;
						}
						num3 = num3 * 10.0 + (double)(num - 48);
						i += num4;
					}
					num = this.read();
				}
				this.peekchar = num;
				if (i != 0)
				{
					double num5 = 10.0;
					for (i--; i > 0; i--)
					{
						num5 *= 10.0;
					}
					num3 /= num5;
				}
				this.NumberValue = (flag ? (-num3) : num3);
				return this.lastttype = -2;
			}
			if ((num2 & 4) != 0)
			{
				int num6 = 0;
				do
				{
					if (num6 >= this.buf.Length)
					{
						char[] array = new char[this.buf.Length * 2];
						Array.Copy(this.buf, 0, array, 0, this.buf.Length);
						this.buf = array;
					}
					this.buf[num6++] = (char)num;
					num = this.read();
					num2 = (int)((num < 0) ? 1 : ((num < 256) ? this.ctype[num] : 4));
				}
				while ((num2 & 6) != 0);
				this.peekchar = num;
				this.StringValue = new string(this.buf, 0, num6);
				if (this.cidtolower)
				{
					this.StringValue = this.StringValue.ToLower();
				}
				return this.lastttype = -3;
			}
			if ((num2 & 8) != 0)
			{
				this.lastttype = num;
				int num7 = 0;
				int num8 = this.read();
				while (num8 >= 0 && num8 != this.lastttype && num8 != 10 && num8 != 13)
				{
					if (num8 == 92)
					{
						num = this.read();
						int num9 = num;
						if (num >= 48 && num <= 55)
						{
							num -= 48;
							int num10 = this.read();
							if (48 <= num10 && num10 <= 55)
							{
								num = (num << 3) + (num10 - 48);
								num10 = this.read();
								if (48 <= num10 && num10 <= 55 && num9 <= 51)
								{
									num = (num << 3) + (num10 - 48);
									num8 = this.read();
								}
								else
								{
									num8 = num10;
								}
							}
							else
							{
								num8 = num10;
							}
						}
						else
						{
							if (num <= 98)
							{
								if (num != 97)
								{
									if (num == 98)
									{
										num = 8;
									}
								}
								else
								{
									num = 7;
								}
							}
							else if (num != 102)
							{
								if (num != 110)
								{
									switch (num)
									{
									case 114:
										num = 13;
										break;
									case 116:
										num = 9;
										break;
									case 118:
										num = 11;
										break;
									}
								}
								else
								{
									num = 10;
								}
							}
							else
							{
								num = 12;
							}
							num8 = this.read();
						}
					}
					else
					{
						num = num8;
						num8 = this.read();
					}
					if (num7 >= this.buf.Length)
					{
						char[] array2 = new char[this.buf.Length * 2];
						Array.Copy(this.buf, 0, array2, 0, this.buf.Length);
						this.buf = array2;
					}
					this.buf[num7++] = (char)num;
				}
				this.peekchar = ((num8 == this.lastttype) ? int.MaxValue : num8);
				this.StringValue = new string(this.buf, 0, num7);
				return this.lastttype;
			}
			if (num == 47 && (this.cppcomments || this.ccomments))
			{
				num = this.read();
				if (num == 42 && this.ccomments)
				{
					int num11 = 0;
					while ((num = this.read()) != 47 || num11 != 42)
					{
						if (num == 13)
						{
							this.linenumber++;
							num = this.read();
							if (num == 10)
							{
								num = this.read();
							}
						}
						else if (num == 10)
						{
							this.linenumber++;
							num = this.read();
						}
						if (num < 0)
						{
							return this.lastttype = -1;
						}
						num11 = num;
					}
					return this.nextToken();
				}
				if (num == 47 && this.cppcomments)
				{
					while ((num = this.read()) != 10 && num != 13 && num >= 0)
					{
					}
					this.peekchar = num;
					return this.nextToken();
				}
				if ((this.ctype[47] & 16) != 0)
				{
					while ((num = this.read()) != 10 && num != 13 && num >= 0)
					{
					}
					this.peekchar = num;
					return this.nextToken();
				}
				this.peekchar = num;
				return this.lastttype = 47;
			}
			else
			{
				if ((num2 & 16) != 0)
				{
					while ((num = this.read()) != 10 && num != 13 && num >= 0)
					{
					}
					this.peekchar = num;
					return this.nextToken();
				}
				return this.lastttype = num;
			}
		}

		// Token: 0x0400023D RID: 573
		private string basestring;

		// Token: 0x0400023E RID: 574
		private bool cppcomments;

		// Token: 0x0400023F RID: 575
		private bool ccomments;

		// Token: 0x04000240 RID: 576
		private bool iseolsig;

		// Token: 0x04000241 RID: 577
		private bool cidtolower;

		// Token: 0x04000242 RID: 578
		private bool pushedback;

		// Token: 0x04000243 RID: 579
		private int peekchar;

		// Token: 0x04000244 RID: 580
		private sbyte[] ctype;

		// Token: 0x04000245 RID: 581
		private int linenumber = 1;

		// Token: 0x04000246 RID: 582
		private int ichar = 1;

		// Token: 0x04000247 RID: 583
		private char[] buf;

		// Token: 0x04000248 RID: 584
		private StreamReader reader;

		// Token: 0x04000249 RID: 585
		private StringReader sreader;

		// Token: 0x0400024A RID: 586
		private Stream input;

		// Token: 0x0400024B RID: 587
		public string StringValue;

		// Token: 0x0400024C RID: 588
		public double NumberValue;

		// Token: 0x0400024D RID: 589
		public int lastttype;
	}
}
