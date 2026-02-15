using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004BE RID: 1214
	[Serializable]
	internal class FixupHolder
	{
		// Token: 0x060026D1 RID: 9937 RVA: 0x0009CDFE File Offset: 0x0009AFFE
		internal FixupHolder(long id, object fixupInfo, int fixupType)
		{
			this.m_id = id;
			this.m_fixupInfo = fixupInfo;
			this.m_fixupType = fixupType;
		}

		// Token: 0x0400127E RID: 4734
		internal long m_id;

		// Token: 0x0400127F RID: 4735
		internal object m_fixupInfo;

		// Token: 0x04001280 RID: 4736
		internal int m_fixupType;
	}
}
