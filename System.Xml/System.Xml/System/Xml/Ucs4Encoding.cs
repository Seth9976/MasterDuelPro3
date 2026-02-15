using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200011E RID: 286
	internal class Ucs4Encoding : Encoding
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0004B532 File Offset: 0x00049732
		public override string WebName
		{
			get
			{
				return this.EncodingName;
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0004B53A File Offset: 0x0004973A
		public override Decoder GetDecoder()
		{
			return this.ucs4Decoder;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0004B542 File Offset: 0x00049742
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return checked(count * 4);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00014C6C File Offset: 0x00012E6C
		public override byte[] GetBytes(string s)
		{
			return null;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return 0;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override int GetMaxByteCount(int charCount)
		{
			return 0;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0004B547 File Offset: 0x00049747
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.ucs4Decoder.GetCharCount(bytes, index, count);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0004B557 File Offset: 0x00049757
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.ucs4Decoder.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0004B56B File Offset: 0x0004976B
		public override int GetMaxCharCount(int byteCount)
		{
			return (byteCount + 3) / 4;
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override int CodePage
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00014C6C File Offset: 0x00012E6C
		public override Encoder GetEncoder()
		{
			return null;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0004B572 File Offset: 0x00049772
		internal static Encoding UCS4_Littleendian
		{
			get
			{
				return new Ucs4Encoding4321();
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0004B579 File Offset: 0x00049779
		internal static Encoding UCS4_Bigendian
		{
			get
			{
				return new Ucs4Encoding1234();
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0004B580 File Offset: 0x00049780
		internal static Encoding UCS4_2143
		{
			get
			{
				return new Ucs4Encoding2143();
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0004B587 File Offset: 0x00049787
		internal static Encoding UCS4_3412
		{
			get
			{
				return new Ucs4Encoding3412();
			}
		}

		// Token: 0x04000746 RID: 1862
		internal Ucs4Decoder ucs4Decoder;
	}
}
