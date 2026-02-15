using System;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x0200000C RID: 12
	public class ClipperRegistry
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000029E2 File Offset: 0x00000BE2
		protected ClipperRegistry()
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000029F5 File Offset: 0x00000BF5
		public static ClipperRegistry instance
		{
			get
			{
				if (ClipperRegistry.s_Instance == null)
				{
					ClipperRegistry.s_Instance = new ClipperRegistry();
				}
				return ClipperRegistry.s_Instance;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A10 File Offset: 0x00000C10
		public void Cull()
		{
			int clippersCount = this.m_Clippers.Count;
			for (int i = 0; i < clippersCount; i++)
			{
				this.m_Clippers[i].PerformClipping();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A46 File Offset: 0x00000C46
		public static void Register(IClipper c)
		{
			if (c == null)
			{
				return;
			}
			ClipperRegistry.instance.m_Clippers.AddUnique(c, true);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A5E File Offset: 0x00000C5E
		public static void Unregister(IClipper c)
		{
			ClipperRegistry.instance.m_Clippers.Remove(c);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A71 File Offset: 0x00000C71
		public static void Disable(IClipper c)
		{
			ClipperRegistry.instance.m_Clippers.DisableItem(c);
		}

		// Token: 0x0400002D RID: 45
		private static ClipperRegistry s_Instance;

		// Token: 0x0400002E RID: 46
		private readonly IndexedSet<IClipper> m_Clippers = new IndexedSet<IClipper>();
	}
}
