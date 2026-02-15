using System;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004A RID: 74
	// (Invoke) Token: 0x0600012F RID: 303
	internal delegate void OnCullingCompleteCallback(JobHandle jobHandle, in BatchCullingContext cullingContext, in BatchCullingOutput cullingOutput);
}
