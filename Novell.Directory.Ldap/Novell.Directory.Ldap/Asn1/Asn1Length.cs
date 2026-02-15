using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000F2 RID: 242
	public class Asn1Length
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00018C6E File Offset: 0x00016E6E
		public virtual int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00018C76 File Offset: 0x00016E76
		public virtual int EncodedLength
		{
			get
			{
				return this.encodedLength;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00002499 File Offset: 0x00000699
		public Asn1Length()
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00018C7E File Offset: 0x00016E7E
		public Asn1Length(int length)
		{
			this.length = length;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00018C90 File Offset: 0x00016E90
		public Asn1Length(Stream in_Renamed)
		{
			int i = in_Renamed.ReadByte();
			this.encodedLength++;
			if (i == 128)
			{
				this.length = -1;
				return;
			}
			if (i < 128)
			{
				this.length = i;
				return;
			}
			this.length = 0;
			for (i &= 127; i > 0; i--)
			{
				int num = in_Renamed.ReadByte();
				this.encodedLength++;
				if (num < 0)
				{
					throw new EndOfStreamException("BERDecoder: decode: EOF in Asn1Length");
				}
				this.length = (this.length << 8) + num;
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00018D24 File Offset: 0x00016F24
		public void reset(Stream in_Renamed)
		{
			this.encodedLength = 0;
			int i = in_Renamed.ReadByte();
			this.encodedLength++;
			if (i == 128)
			{
				this.length = -1;
				return;
			}
			if (i < 128)
			{
				this.length = i;
				return;
			}
			this.length = 0;
			for (i &= 127; i > 0; i--)
			{
				int num = in_Renamed.ReadByte();
				this.encodedLength++;
				if (num < 0)
				{
					throw new EndOfStreamException("BERDecoder: decode: EOF in Asn1Length");
				}
				this.length = (this.length << 8) + num;
			}
		}

		// Token: 0x040004E8 RID: 1256
		private int length;

		// Token: 0x040004E9 RID: 1257
		private int encodedLength;
	}
}
