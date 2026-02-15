using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000361 RID: 865
	[RequiredByNativeCode]
	public class OnDemandRendering
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0002F6D0 File Offset: 0x0002D8D0
		public static int renderFrameInterval
		{
			get
			{
				return OnDemandRendering.m_RenderFrameInterval;
			}
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0002F6E7 File Offset: 0x0002D8E7
		[RequiredByNativeCode]
		internal static void GetRenderFrameInterval(out int frameInterval)
		{
			frameInterval = OnDemandRendering.renderFrameInterval;
		}

		// Token: 0x04000A2D RID: 2605
		private static int m_RenderFrameInterval = 1;
	}
}
