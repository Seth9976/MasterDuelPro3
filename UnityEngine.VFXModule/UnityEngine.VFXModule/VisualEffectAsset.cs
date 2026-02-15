using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("VFXScriptingClasses.h")]
	[NativeHeader("Modules/VFX/Public/VisualEffectAsset.h")]
	[UsedByNativeCode]
	public class VisualEffectAsset : VisualEffectObject
	{
		// Token: 0x0400001C RID: 28
		public static readonly int PlayEventID = Shader.PropertyToID("OnPlay");

		// Token: 0x0400001D RID: 29
		public static readonly int StopEventID = Shader.PropertyToID("OnStop");
	}
}
