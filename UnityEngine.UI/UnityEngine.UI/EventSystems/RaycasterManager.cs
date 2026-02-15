using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000C4 RID: 196
	public static class RaycasterManager
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x0001C33F File Offset: 0x0001A53F
		internal static void AddRaycaster(BaseRaycaster baseRaycaster)
		{
			if (RaycasterManager.s_Raycasters.Contains(baseRaycaster))
			{
				return;
			}
			RaycasterManager.s_Raycasters.Add(baseRaycaster);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001C35A File Offset: 0x0001A55A
		public static List<BaseRaycaster> GetRaycasters()
		{
			return RaycasterManager.s_Raycasters;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001C361 File Offset: 0x0001A561
		internal static void RemoveRaycasters(BaseRaycaster baseRaycaster)
		{
			if (!RaycasterManager.s_Raycasters.Contains(baseRaycaster))
			{
				return;
			}
			RaycasterManager.s_Raycasters.Remove(baseRaycaster);
		}

		// Token: 0x04000351 RID: 849
		private static readonly List<BaseRaycaster> s_Raycasters = new List<BaseRaycaster>();
	}
}
