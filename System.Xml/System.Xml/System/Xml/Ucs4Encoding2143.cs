using System;

namespace System.Xml
{
	// Token: 0x02000121 RID: 289
	internal class Ucs4Encoding2143 : Ucs4Encoding
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x0004B5FA File Offset: 0x000497FA
		public Ucs4Encoding2143()
		{
			this.ucs4Decoder = new Ucs4Decoder2143();
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0004B60D File Offset: 0x0004980D
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (order 2143)";
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0004B614 File Offset: 0x00049814
		public override byte[] GetPreamble()
		{
			return new byte[] { 0, 0, byte.MaxValue, 254 };
		}
	}
}
