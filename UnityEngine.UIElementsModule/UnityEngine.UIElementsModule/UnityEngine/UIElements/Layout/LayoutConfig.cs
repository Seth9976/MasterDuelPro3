using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200056C RID: 1388
	internal readonly struct LayoutConfig
	{
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x00098760 File Offset: 0x00096960
		public static LayoutConfig Undefined
		{
			get
			{
				return new LayoutConfig(default(LayoutDataAccess), LayoutHandle.Undefined);
			}
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00098780 File Offset: 0x00096980
		internal LayoutConfig(LayoutDataAccess access, LayoutHandle handle)
		{
			this.m_Access = access;
			this.m_Handle = handle;
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x00098791 File Offset: 0x00096991
		public LayoutHandle Handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x00098799 File Offset: 0x00096999
		public ref float PointScaleFactor
		{
			get
			{
				return ref this.m_Access.GetConfigData(this.m_Handle).PointScaleFactor;
			}
		}

		// Token: 0x0400135A RID: 4954
		private readonly LayoutDataAccess m_Access;

		// Token: 0x0400135B RID: 4955
		private readonly LayoutHandle m_Handle;
	}
}
