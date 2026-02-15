using System;

namespace System.Xml
{
	// Token: 0x0200011F RID: 287
	internal class Ucs4Encoding1234 : Ucs4Encoding
	{
		// Token: 0x06000EE9 RID: 3817 RVA: 0x0004B596 File Offset: 0x00049796
		public Ucs4Encoding1234()
		{
			this.ucs4Decoder = new Ucs4Decoder1234();
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0004B5A9 File Offset: 0x000497A9
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (Bigendian)";
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0004B5B0 File Offset: 0x000497B0
		public override byte[] GetPreamble()
		{
			return new byte[] { 0, 0, 254, byte.MaxValue };
		}
	}
}
