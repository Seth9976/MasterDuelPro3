using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000F0 RID: 240
	public class Asn1Identifier : ICloneable
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00018A37 File Offset: 0x00016C37
		public virtual int Asn1Class
		{
			get
			{
				return this.tagClass;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00018A3F File Offset: 0x00016C3F
		public virtual bool Constructed
		{
			get
			{
				return this.constructed;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00018A47 File Offset: 0x00016C47
		public virtual int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00018A4F File Offset: 0x00016C4F
		public virtual int EncodedLength
		{
			get
			{
				return this.encodedLength;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00018A57 File Offset: 0x00016C57
		[CLSCompliant(false)]
		public virtual bool Universal
		{
			get
			{
				return this.tagClass == 0;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00018A62 File Offset: 0x00016C62
		[CLSCompliant(false)]
		public virtual bool Application
		{
			get
			{
				return this.tagClass == 1;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00018A6D File Offset: 0x00016C6D
		[CLSCompliant(false)]
		public virtual bool Context
		{
			get
			{
				return this.tagClass == 2;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00018A78 File Offset: 0x00016C78
		[CLSCompliant(false)]
		public virtual bool Private
		{
			get
			{
				return this.tagClass == 3;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00018A83 File Offset: 0x00016C83
		public Asn1Identifier(int tagClass, bool constructed, int tag)
		{
			this.tagClass = tagClass;
			this.constructed = constructed;
			this.tag = tag;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00018AA0 File Offset: 0x00016CA0
		public Asn1Identifier(Stream in_Renamed)
		{
			int num = in_Renamed.ReadByte();
			this.encodedLength++;
			if (num < 0)
			{
				throw new EndOfStreamException("BERDecoder: decode: EOF in Identifier");
			}
			this.tagClass = num >> 6;
			this.constructed = (num & 32) != 0;
			this.tag = num & 31;
			if (this.tag == 31)
			{
				this.tag = this.decodeTagNumber(in_Renamed);
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00002499 File Offset: 0x00000699
		public Asn1Identifier()
		{
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00018B10 File Offset: 0x00016D10
		public void reset(Stream in_Renamed)
		{
			this.encodedLength = 0;
			int num = in_Renamed.ReadByte();
			this.encodedLength++;
			if (num < 0)
			{
				throw new EndOfStreamException("BERDecoder: decode: EOF in Identifier");
			}
			this.tagClass = num >> 6;
			this.constructed = (num & 32) != 0;
			this.tag = num & 31;
			if (this.tag == 31)
			{
				this.tag = this.decodeTagNumber(in_Renamed);
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00018B80 File Offset: 0x00016D80
		private int decodeTagNumber(Stream in_Renamed)
		{
			int num = 0;
			for (;;)
			{
				int num2 = in_Renamed.ReadByte();
				this.encodedLength++;
				if (num2 < 0)
				{
					break;
				}
				num = (num << 7) + (num2 & 127);
				if ((num2 & 128) == 0)
				{
					return num;
				}
			}
			throw new EndOfStreamException("BERDecoder: decode: EOF in tag number");
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00018BC8 File Offset: 0x00016DC8
		public object Clone()
		{
			object obj;
			try
			{
				obj = base.MemberwiseClone();
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj;
		}

		// Token: 0x040004DE RID: 1246
		public const int UNIVERSAL = 0;

		// Token: 0x040004DF RID: 1247
		public const int APPLICATION = 1;

		// Token: 0x040004E0 RID: 1248
		public const int CONTEXT = 2;

		// Token: 0x040004E1 RID: 1249
		public const int PRIVATE = 3;

		// Token: 0x040004E2 RID: 1250
		private int tagClass;

		// Token: 0x040004E3 RID: 1251
		private bool constructed;

		// Token: 0x040004E4 RID: 1252
		private int tag;

		// Token: 0x040004E5 RID: 1253
		private int encodedLength;
	}
}
