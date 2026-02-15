using System;

namespace Willow.InGameField
{
	// Token: 0x0200156D RID: 5485
	[Serializable]
	public class IntParameterRef
	{
		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06009F18 RID: 40728 RVA: 0x000029CC File Offset: 0x00000BCC
		public int value
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06009F19 RID: 40729 RVA: 0x000029CC File Offset: 0x00000BCC
		public static implicit operator int(IntParameterRef reference)
		{
			return 0;
		}

		// Token: 0x0400DE48 RID: 56904
		public IntParameter param;
	}
}
