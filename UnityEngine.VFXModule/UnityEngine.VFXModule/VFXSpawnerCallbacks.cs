using System;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200000A RID: 10
	[RequiredByNativeCode]
	[Serializable]
	public abstract class VFXSpawnerCallbacks : ScriptableObject
	{
		// Token: 0x06000017 RID: 23
		public abstract void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		// Token: 0x06000018 RID: 24
		public abstract void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		// Token: 0x06000019 RID: 25
		public abstract void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);
	}
}
