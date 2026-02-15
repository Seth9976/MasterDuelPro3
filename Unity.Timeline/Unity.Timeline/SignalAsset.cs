using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000048 RID: 72
	[AssetFileNameExtension("signal", new string[] { })]
	public class SignalAsset : ScriptableObject
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000293 RID: 659 RVA: 0x00008F9C File Offset: 0x0000719C
		// (remove) Token: 0x06000294 RID: 660 RVA: 0x00008FD0 File Offset: 0x000071D0
		internal static event Action<SignalAsset> OnEnableCallback;

		// Token: 0x06000295 RID: 661 RVA: 0x00009003 File Offset: 0x00007203
		private void OnEnable()
		{
			if (SignalAsset.OnEnableCallback != null)
			{
				SignalAsset.OnEnableCallback(this);
			}
		}
	}
}
