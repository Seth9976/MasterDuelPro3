using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200001B RID: 27
	[Obsolete("This class is not used anymore. See AnimatorOverrideController.GetOverrides() and AnimatorOverrideController.ApplyOverrides()")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class AnimationClipPair
	{
		// Token: 0x04000064 RID: 100
		public AnimationClip originalClip;

		// Token: 0x04000065 RID: 101
		public AnimationClip overrideClip;
	}
}
