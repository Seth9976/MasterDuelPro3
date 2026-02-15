using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000195 RID: 405
	public struct RTHandleStaticHelpers
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x00028195 File Offset: 0x00026395
		public static void SetRTHandleStaticWrapper(RenderTargetIdentifier rtId)
		{
			if (RTHandleStaticHelpers.s_RTHandleWrapper == null)
			{
				RTHandleStaticHelpers.s_RTHandleWrapper = RTHandles.Alloc(rtId);
				return;
			}
			RTHandleStaticHelpers.s_RTHandleWrapper.SetTexture(rtId);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000281B5 File Offset: 0x000263B5
		public static void SetRTHandleUserManagedWrapper(ref RTHandle rtWrapper, RenderTargetIdentifier rtId)
		{
			if (rtWrapper == null)
			{
				return;
			}
			rtWrapper.SetTexture(rtId);
		}

		// Token: 0x040007B6 RID: 1974
		public static RTHandle s_RTHandleWrapper;
	}
}
