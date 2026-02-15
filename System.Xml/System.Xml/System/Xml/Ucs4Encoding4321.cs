using System;

namespace System.Xml
{
	// Token: 0x02000120 RID: 288
	internal class Ucs4Encoding4321 : Ucs4Encoding
	{
		// Token: 0x06000EEC RID: 3820 RVA: 0x0004B5C8 File Offset: 0x000497C8
		public Ucs4Encoding4321()
		{
			this.ucs4Decoder = new Ucs4Decoder4321();
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0004B5DB File Offset: 0x000497DB
		public override string EncodingName
		{
			get
			{
				return "ucs-4";
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0004B5E2 File Offset: 0x000497E2
		public override byte[] GetPreamble()
		{
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = 254;
			return array;
		}
	}
}
