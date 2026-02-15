using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x02000003 RID: 3
	[CLSCompliant(false)]
	public class MiniParser
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		public MiniParser()
		{
			this.twoCharBuff = new int[2];
			this.splitCData = false;
			this.Reset();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000207D File Offset: 0x0000027D
		public void Reset()
		{
			this.line = 0;
			this.col = 0;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
		protected static bool StrEquals(string str, StringBuilder sb, int sbStart, int len)
		{
			if (len != str.Length)
			{
				return false;
			}
			for (int i = 0; i < len; i++)
			{
				if (str[i] != sb[sbStart + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020C9 File Offset: 0x000002C9
		protected void FatalErr(string descr)
		{
			throw new MiniParser.XMLError(descr, this.line, this.col);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		protected static int Xlat(int charCode, int state)
		{
			int num = state * MiniParser.INPUT_RANGE;
			int num2 = Math.Min(MiniParser.tbl.Length - num, MiniParser.INPUT_RANGE);
			while (--num2 >= 0)
			{
				ushort num3 = MiniParser.tbl[num];
				if (charCode == num3 >> 12)
				{
					return (int)(num3 & 4095);
				}
				num++;
			}
			return 4095;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public void Parse(MiniParser.IReader reader, MiniParser.IHandler handler)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (handler == null)
			{
				handler = new MiniParser.HandlerAdapter();
			}
			MiniParser.AttrListImpl attrListImpl = new MiniParser.AttrListImpl();
			string text = null;
			Stack stack = new Stack();
			string text2 = null;
			this.line = 1;
			this.col = 0;
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num3 = 0;
			handler.OnStartParsing(this);
			for (;;)
			{
				this.col++;
				num = reader.Read();
				if (num == -1)
				{
					break;
				}
				int num4 = "<>/?=&'\"![ ]\t\r\n".IndexOf((char)num) & 15;
				if (num4 != 13)
				{
					if (num4 == 12)
					{
						num4 = 10;
					}
					if (num4 == 14)
					{
						this.col = 0;
						this.line++;
						num4 = 10;
					}
					int num5 = MiniParser.Xlat(num4, num2);
					num2 = num5 & 255;
					if (num != 10 || (num2 != 14 && num2 != 15))
					{
						num5 >>= 8;
						if (num2 >= 128)
						{
							if (num2 == 255)
							{
								this.FatalErr("State dispatch error.");
							}
							else
							{
								this.FatalErr(MiniParser.errors[num2 ^ 128]);
							}
						}
						switch (num5)
						{
						case 0:
							break;
						case 1:
						{
							text2 = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							string text3 = null;
							if (stack.Count == 0 || text2 != (text3 = stack.Pop() as string))
							{
								if (text3 == null)
								{
									this.FatalErr("Tag stack underflow");
								}
								else
								{
									this.FatalErr(string.Format("Expected end tag '{0}' but found '{1}'", text2, text3));
								}
							}
							handler.OnEndElement(text2);
							continue;
						}
						case 2:
							text2 = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							if (num != 47 && num != 62)
							{
								continue;
							}
							break;
						case 3:
							text = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							continue;
						case 4:
							if (text == null)
							{
								this.FatalErr("Internal error.");
							}
							attrListImpl.Add(text, stringBuilder.ToString());
							stringBuilder = new StringBuilder();
							text = null;
							continue;
						case 5:
							handler.OnChars(stringBuilder.ToString());
							stringBuilder = new StringBuilder();
							continue;
						case 6:
						{
							string text4 = "CDATA[";
							flag2 = false;
							flag3 = false;
							if (num == 45)
							{
								num = reader.Read();
								if (num != 45)
								{
									this.FatalErr("Invalid comment");
								}
								this.col++;
								flag2 = true;
								this.twoCharBuff[0] = -1;
								this.twoCharBuff[1] = -1;
								continue;
							}
							if (num != 91)
							{
								flag3 = true;
								num3 = 0;
								continue;
							}
							for (int i = 0; i < text4.Length; i++)
							{
								if (reader.Read() != (int)text4[i])
								{
									this.col += i + 1;
									break;
								}
							}
							this.col += text4.Length;
							flag = true;
							continue;
						}
						case 7:
						{
							int num6 = 0;
							num = 93;
							while (num == 93)
							{
								num = reader.Read();
								num6++;
							}
							if (num != 62)
							{
								for (int j = 0; j < num6; j++)
								{
									stringBuilder.Append(']');
								}
								stringBuilder.Append((char)num);
								num2 = 18;
							}
							else
							{
								for (int k = 0; k < num6 - 2; k++)
								{
									stringBuilder.Append(']');
								}
								flag = false;
							}
							this.col += num6;
							continue;
						}
						case 8:
							this.FatalErr(string.Format("Error {0}", num2));
							continue;
						case 9:
							continue;
						case 10:
							stringBuilder = new StringBuilder();
							if (num != 60)
							{
								goto IL_03E3;
							}
							continue;
						case 11:
							goto IL_03E3;
						case 12:
							if (flag2)
							{
								if (num == 62 && this.twoCharBuff[0] == 45 && this.twoCharBuff[1] == 45)
								{
									flag2 = false;
									num2 = 0;
									continue;
								}
								this.twoCharBuff[0] = this.twoCharBuff[1];
								this.twoCharBuff[1] = num;
								continue;
							}
							else
							{
								if (!flag3)
								{
									if (this.splitCData && stringBuilder.Length > 0 && flag)
									{
										handler.OnChars(stringBuilder.ToString());
										stringBuilder = new StringBuilder();
									}
									flag = false;
									stringBuilder.Append((char)num);
									continue;
								}
								if (num == 60 || num == 62)
								{
									num3 ^= 1;
								}
								if (num == 62 && num3 != 0)
								{
									flag3 = false;
									num2 = 0;
									continue;
								}
								continue;
							}
							break;
						case 13:
						{
							num = reader.Read();
							int num7 = this.col + 1;
							if (num == 35)
							{
								int num8 = 10;
								int num9 = 0;
								int num10 = 0;
								num = reader.Read();
								num7++;
								if (num == 120)
								{
									num = reader.Read();
									num7++;
									num8 = 16;
								}
								NumberStyles numberStyles = ((num8 == 16) ? NumberStyles.HexNumber : NumberStyles.Integer);
								for (;;)
								{
									int num11 = -1;
									if (char.IsNumber((char)num) || "abcdef".IndexOf(char.ToLower((char)num)) != -1)
									{
										try
										{
											num11 = int.Parse(new string((char)num, 1), numberStyles);
										}
										catch (FormatException)
										{
											num11 = -1;
										}
									}
									if (num11 == -1)
									{
										break;
									}
									num9 *= num8;
									num9 += num11;
									num10++;
									num = reader.Read();
									num7++;
								}
								if (num == 59 && num10 > 0)
								{
									stringBuilder.Append((char)num9);
								}
								else
								{
									this.FatalErr("Bad char ref");
								}
							}
							else
							{
								string text5 = "aglmopqstu";
								string text6 = "&'\"><";
								int num12 = 0;
								int num13 = 15;
								int num14 = 0;
								int length = stringBuilder.Length;
								for (;;)
								{
									if (num12 != 15)
									{
										num12 = text5.IndexOf((char)num) & 15;
									}
									if (num12 == 15)
									{
										this.FatalErr(MiniParser.errors[7]);
									}
									stringBuilder.Append((char)num);
									int num15 = (int)"Ｕ㾏侏ཟｸ\ue1f4⊙\ueeff\ueeffｏ"[num12];
									int num16 = (num15 >> 4) & 15;
									int num17 = num15 & 15;
									int num18 = num15 >> 12;
									int num19 = (num15 >> 8) & 15;
									num = reader.Read();
									num7++;
									num12 = 15;
									if (num16 != 15 && num == (int)text5[num16])
									{
										if (num18 < 14)
										{
											num13 = num18;
										}
										num14 = 12;
									}
									else if (num17 != 15 && num == (int)text5[num17])
									{
										if (num19 < 14)
										{
											num13 = num19;
										}
										num14 = 8;
									}
									else if (num == 59)
									{
										if (num13 != 15 && num14 != 0 && ((num15 >> num14) & 15) == 14)
										{
											break;
										}
										continue;
									}
									num12 = 0;
								}
								int num20 = num7 - this.col - 1;
								if (num20 > 0 && num20 < 5 && (MiniParser.StrEquals("amp", stringBuilder, length, num20) || MiniParser.StrEquals("apos", stringBuilder, length, num20) || MiniParser.StrEquals("quot", stringBuilder, length, num20) || MiniParser.StrEquals("lt", stringBuilder, length, num20) || MiniParser.StrEquals("gt", stringBuilder, length, num20)))
								{
									stringBuilder.Length = length;
									stringBuilder.Append(text6[num13]);
								}
								else
								{
									this.FatalErr(MiniParser.errors[7]);
								}
							}
							this.col = num7;
							continue;
						}
						default:
							this.FatalErr(string.Format("Unexpected action code - {0}.", num5));
							continue;
						}
						handler.OnStartElement(text2, attrListImpl);
						if (num != 47)
						{
							stack.Push(text2);
						}
						else
						{
							handler.OnEndElement(text2);
						}
						attrListImpl.Clear();
						continue;
						IL_03E3:
						stringBuilder.Append((char)num);
					}
				}
			}
			if (num2 != 0)
			{
				this.FatalErr("Unexpected EOF");
			}
			handler.OnEndParsing(this);
		}

		// Token: 0x04000001 RID: 1
		private static readonly int INPUT_RANGE = 13;

		// Token: 0x04000002 RID: 2
		private static readonly ushort[] tbl = new ushort[]
		{
			2305, 43264, 63616, 10368, 6272, 14464, 18560, 22656, 26752, 34944,
			39040, 47232, 30848, 2177, 10498, 6277, 14595, 18561, 22657, 26753,
			35088, 39041, 43137, 47233, 30849, 64004, 4352, 43266, 64258, 2177,
			10369, 14465, 18561, 22657, 26753, 34945, 39041, 47233, 30849, 14597,
			2307, 10499, 6403, 18691, 22787, 26883, 35075, 39171, 43267, 47363,
			30979, 63747, 64260, 8710, 4615, 41480, 2177, 14465, 18561, 22657,
			26753, 34945, 39041, 47233, 30849, 6400, 2307, 10499, 14595, 18691,
			22787, 26883, 35075, 39171, 43267, 47363, 30979, 63747, 6400, 2177,
			10369, 14465, 18561, 22657, 26753, 34945, 39041, 43137, 47233, 30849,
			63617, 2561, 23818, 11274, 7178, 15370, 19466, 27658, 35850, 39946,
			43783, 48138, 31754, 64522, 64265, 8198, 4103, 43272, 2177, 14465,
			18561, 22657, 26753, 34945, 39041, 47233, 30849, 64265, 17163, 43276,
			2178, 10370, 6274, 14466, 22658, 26754, 34946, 39042, 47234, 30850,
			2317, 23818, 11274, 7178, 15370, 19466, 27658, 35850, 39946, 44042,
			48138, 31754, 64522, 26894, 30991, 43275, 2180, 10372, 6276, 14468,
			18564, 22660, 34948, 39044, 47236, 63620, 17163, 43276, 2178, 10370,
			6274, 14466, 22658, 26754, 34946, 39042, 47234, 30850, 63618, 9474,
			35088, 2182, 6278, 14470, 18566, 22662, 26758, 39046, 43142, 47238,
			30854, 63622, 25617, 23822, 2830, 11022, 6926, 15118, 19214, 35598,
			39694, 43790, 47886, 31502, 64270, 29713, 23823, 2831, 11023, 6927,
			15119, 19215, 27407, 35599, 39695, 43791, 47887, 64271, 38418, 6400,
			1555, 9747, 13843, 17939, 22035, 26131, 34323, 42515, 46611, 30227,
			62995, 8198, 4103, 43281, 64265, 2177, 14465, 18561, 22657, 26753,
			34945, 39041, 47233, 30849, 46858, 3090, 11282, 7186, 15378, 19474,
			23570, 27666, 35858, 39954, 44050, 31762, 64530, 3091, 11283, 7187,
			15379, 19475, 23571, 27667, 35859, 39955, 44051, 48147, 31763, 64531,
			ushort.MaxValue, ushort.MaxValue
		};

		// Token: 0x04000003 RID: 3
		protected static string[] errors = new string[] { "Expected element", "Invalid character in tag", "No '='", "Invalid character entity", "Invalid attr value", "Empty tag", "No end tag", "Bad entity ref" };

		// Token: 0x04000004 RID: 4
		protected int line;

		// Token: 0x04000005 RID: 5
		protected int col;

		// Token: 0x04000006 RID: 6
		protected int[] twoCharBuff;

		// Token: 0x04000007 RID: 7
		protected bool splitCData;

		// Token: 0x02000004 RID: 4
		public interface IReader
		{
			// Token: 0x0600000A RID: 10
			int Read();
		}

		// Token: 0x02000005 RID: 5
		public interface IAttrList
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600000B RID: 11
			int Length { get; }

			// Token: 0x0600000C RID: 12
			string GetName(int i);

			// Token: 0x0600000D RID: 13
			string GetValue(int i);
		}

		// Token: 0x02000006 RID: 6
		public interface IHandler
		{
			// Token: 0x0600000E RID: 14
			void OnStartParsing(MiniParser parser);

			// Token: 0x0600000F RID: 15
			void OnStartElement(string name, MiniParser.IAttrList attrs);

			// Token: 0x06000010 RID: 16
			void OnEndElement(string name);

			// Token: 0x06000011 RID: 17
			void OnChars(string ch);

			// Token: 0x06000012 RID: 18
			void OnEndParsing(MiniParser parser);
		}

		// Token: 0x02000007 RID: 7
		public class HandlerAdapter : MiniParser.IHandler
		{
			// Token: 0x06000014 RID: 20 RVA: 0x00002945 File Offset: 0x00000B45
			public void OnStartParsing(MiniParser parser)
			{
			}

			// Token: 0x06000015 RID: 21 RVA: 0x00002945 File Offset: 0x00000B45
			public void OnStartElement(string name, MiniParser.IAttrList attrs)
			{
			}

			// Token: 0x06000016 RID: 22 RVA: 0x00002945 File Offset: 0x00000B45
			public void OnEndElement(string name)
			{
			}

			// Token: 0x06000017 RID: 23 RVA: 0x00002945 File Offset: 0x00000B45
			public void OnChars(string ch)
			{
			}

			// Token: 0x06000018 RID: 24 RVA: 0x00002945 File Offset: 0x00000B45
			public void OnEndParsing(MiniParser parser)
			{
			}
		}

		// Token: 0x02000008 RID: 8
		public class AttrListImpl : MiniParser.IAttrList
		{
			// Token: 0x06000019 RID: 25 RVA: 0x00002947 File Offset: 0x00000B47
			public AttrListImpl()
				: this(0)
			{
			}

			// Token: 0x0600001A RID: 26 RVA: 0x00002950 File Offset: 0x00000B50
			public AttrListImpl(int initialCapacity)
			{
				if (initialCapacity <= 0)
				{
					this.names = new ArrayList();
					this.values = new ArrayList();
					return;
				}
				this.names = new ArrayList(initialCapacity);
				this.values = new ArrayList(initialCapacity);
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600001B RID: 27 RVA: 0x0000298B File Offset: 0x00000B8B
			public int Length
			{
				get
				{
					return this.names.Count;
				}
			}

			// Token: 0x0600001C RID: 28 RVA: 0x00002998 File Offset: 0x00000B98
			public string GetName(int i)
			{
				string text = null;
				if (i >= 0 && i < this.Length)
				{
					text = this.names[i] as string;
				}
				return text;
			}

			// Token: 0x0600001D RID: 29 RVA: 0x000029C8 File Offset: 0x00000BC8
			public string GetValue(int i)
			{
				string text = null;
				if (i >= 0 && i < this.Length)
				{
					text = this.values[i] as string;
				}
				return text;
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000029F7 File Offset: 0x00000BF7
			public void Clear()
			{
				this.names.Clear();
				this.values.Clear();
			}

			// Token: 0x0600001F RID: 31 RVA: 0x00002A0F File Offset: 0x00000C0F
			public void Add(string name, string value)
			{
				this.names.Add(name);
				this.values.Add(value);
			}

			// Token: 0x04000008 RID: 8
			protected ArrayList names;

			// Token: 0x04000009 RID: 9
			protected ArrayList values;
		}

		// Token: 0x02000009 RID: 9
		public class XMLError : Exception
		{
			// Token: 0x06000020 RID: 32 RVA: 0x00002A2B File Offset: 0x00000C2B
			public XMLError(string descr, int line, int column)
				: base(descr)
			{
				this.descr = descr;
				this.line = line;
				this.column = column;
			}

			// Token: 0x06000021 RID: 33 RVA: 0x00002A49 File Offset: 0x00000C49
			public override string ToString()
			{
				return string.Format("{0} @ (line = {1}, col = {2})", this.descr, this.line, this.column);
			}

			// Token: 0x0400000A RID: 10
			protected string descr;

			// Token: 0x0400000B RID: 11
			protected int line;

			// Token: 0x0400000C RID: 12
			protected int column;
		}
	}
}
