using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E5 RID: 229
	[RequiredByNativeCode]
	public struct Resolution
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		public int width
		{
			get
			{
				return this.m_Width;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000CC84 File Offset: 0x0000AE84
		public int height
		{
			get
			{
				return this.m_Height;
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000CC9C File Offset: 0x0000AE9C
		public override string ToString()
		{
			return UnityString.Format("{0} x {1} @ {2}Hz", new object[] { this.m_Width, this.m_Height, this.m_RefreshRate });
		}

		// Token: 0x040002AA RID: 682
		private int m_Width;

		// Token: 0x040002AB RID: 683
		private int m_Height;

		// Token: 0x040002AC RID: 684
		private RefreshRate m_RefreshRate;
	}
}
