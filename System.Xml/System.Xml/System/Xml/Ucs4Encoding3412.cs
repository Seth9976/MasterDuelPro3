using System;

namespace System.Xml
{
	// Token: 0x02000122 RID: 290
	internal class Ucs4Encoding3412 : Ucs4Encoding
	{
		// Token: 0x06000EF2 RID: 3826 RVA: 0x0004B62C File Offset: 0x0004982C
		public Ucs4Encoding3412()
		{
			this.ucs4Decoder = new Ucs4Decoder3412();
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0004B63F File Offset: 0x0004983F
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (order 3412)";
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0004B646 File Offset: 0x00049846
		public override byte[] GetPreamble()
		{
			byte[] array = new byte[4];
			array[0] = 254;
			array[1] = byte.MaxValue;
			return array;
		}
	}
}
