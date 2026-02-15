using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000584 RID: 1412
	internal struct LayoutConfigData
	{
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x0009A530 File Offset: 0x00098730
		public static LayoutConfigData Default
		{
			get
			{
				return new LayoutConfigData
				{
					PointScaleFactor = 1f,
					ShouldLog = false
				};
			}
		}

		// Token: 0x040013A2 RID: 5026
		public float PointScaleFactor;

		// Token: 0x040013A3 RID: 5027
		[MarshalAs(UnmanagedType.U1)]
		public bool ShouldLog;
	}
}
