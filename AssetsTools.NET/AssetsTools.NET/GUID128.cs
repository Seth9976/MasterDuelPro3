using System;
using System.Text;

namespace AssetsTools.NET
{
	// Token: 0x02000045 RID: 69
	public struct GUID128
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00010C40 File Offset: 0x0000EE40
		public bool IsEmpty
		{
			get
			{
				return this.data0 == 0U && this.data1 == 0U && this.data2 == 0U && this.data3 == 0U;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00010C66 File Offset: 0x0000EE66
		public GUID128(AssetsFileReader reader)
		{
			this = default(GUID128);
			this.Read(reader);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00010C78 File Offset: 0x0000EE78
		public void Read(AssetsFileReader reader)
		{
			this.data0 = reader.ReadUInt32();
			this.data1 = reader.ReadUInt32();
			this.data2 = reader.ReadUInt32();
			this.data3 = reader.ReadUInt32();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00010CAB File Offset: 0x0000EEAB
		public void Write(AssetsFileWriter writer)
		{
			writer.Write(this.data0);
			writer.Write(this.data1);
			writer.Write(this.data2);
			writer.Write(this.data3);
		}

		// Token: 0x1700003C RID: 60
		public uint this[int i]
		{
			get
			{
				if (!true)
				{
				}
				uint num;
				switch (i)
				{
				case 0:
					num = this.data0;
					break;
				case 1:
					num = this.data1;
					break;
				case 2:
					num = this.data2;
					break;
				case 3:
					num = this.data3;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
				if (!true)
				{
				}
				return num;
			}
			set
			{
				switch (i)
				{
				case 0:
					this.data0 = value;
					break;
				case 1:
					this.data1 = value;
					break;
				case 2:
					this.data2 = value;
					break;
				case 3:
					this.data3 = value;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00010D98 File Offset: 0x0000EF98
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			for (int i = 3; i >= 0; i--)
			{
				for (int j = 7; j >= 0; j--)
				{
					uint num = this[i];
					num >>= j * 4;
					num &= 15U;
					stringBuilder.Insert(0, "0123456789abcdef"[(int)num]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00010E14 File Offset: 0x0000F014
		public static bool TryParse(string str, out GUID128 guid)
		{
			guid = default(GUID128);
			bool flag = str.Length != 32;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					uint num = 0U;
					for (int j = 7; j >= 0; j--)
					{
						uint num2 = GUID128.LiteralToHex(str[i * 8 + j]);
						bool flag3 = num2 == uint.MaxValue;
						if (flag3)
						{
							return false;
						}
						num |= num2 << j * 4;
					}
					guid[i] = num;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00010EB0 File Offset: 0x0000F0B0
		private static uint LiteralToHex(char c)
		{
			if (!true)
			{
			}
			uint num;
			switch (c)
			{
			case '0':
				num = 0U;
				break;
			case '1':
				num = 1U;
				break;
			case '2':
				num = 2U;
				break;
			case '3':
				num = 3U;
				break;
			case '4':
				num = 4U;
				break;
			case '5':
				num = 5U;
				break;
			case '6':
				num = 6U;
				break;
			case '7':
				num = 7U;
				break;
			case '8':
				num = 8U;
				break;
			case '9':
				num = 9U;
				break;
			default:
				switch (c)
				{
				case 'a':
					num = 10U;
					break;
				case 'b':
					num = 11U;
					break;
				case 'c':
					num = 12U;
					break;
				case 'd':
					num = 13U;
					break;
				case 'e':
					num = 14U;
					break;
				case 'f':
					num = 15U;
					break;
				default:
					num = uint.MaxValue;
					break;
				}
				break;
			}
			if (!true)
			{
			}
			return num;
		}

		// Token: 0x04000195 RID: 405
		private const string HexToLiteral = "0123456789abcdef";

		// Token: 0x04000196 RID: 406
		public uint data0;

		// Token: 0x04000197 RID: 407
		public uint data1;

		// Token: 0x04000198 RID: 408
		public uint data2;

		// Token: 0x04000199 RID: 409
		public uint data3;
	}
}
