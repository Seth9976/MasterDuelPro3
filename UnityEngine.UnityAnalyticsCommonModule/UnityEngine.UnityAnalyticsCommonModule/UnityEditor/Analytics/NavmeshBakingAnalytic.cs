using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000011 RID: 17
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class NavmeshBakingAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000227B File Offset: 0x0000047B
		public NavmeshBakingAnalytic()
			: base("navigation_navmesh_baking", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002294 File Offset: 0x00000494
		[RequiredByNativeCode]
		internal static NavmeshBakingAnalytic CreateNavmeshBakingAnalytic()
		{
			return new NavmeshBakingAnalytic();
		}

		// Token: 0x04000032 RID: 50
		private bool new_nav_api;

		// Token: 0x04000033 RID: 51
		private bool bake_at_runtime;

		// Token: 0x04000034 RID: 52
		private int height_meshes_count;

		// Token: 0x04000035 RID: 53
		private int offmesh_links_count;
	}
}
