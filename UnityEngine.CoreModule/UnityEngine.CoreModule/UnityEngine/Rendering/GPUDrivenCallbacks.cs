using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200038F RID: 911
	[RequiredByNativeCode]
	internal static class GPUDrivenCallbacks
	{
		// Token: 0x060018FB RID: 6395 RVA: 0x0003566D File Offset: 0x0003386D
		[RequiredByNativeCode(GenerateProxy = true)]
		public static void InvokeGPUDrivenLODGroupDataNativeCallback(GPUDrivenLODGroupDataNativeCallback callback, in GPUDrivenLODGroupDataNative lodGroupDataNative, GPUDrivenLODGroupDataCallback target)
		{
			callback(in lodGroupDataNative, target);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x00035679 File Offset: 0x00033879
		[RequiredByNativeCode(GenerateProxy = true)]
		public static void InvokeGPUDrivenRendererDataNativeCallback(GPUDrivenRendererDataNativeCallback callback, in GPUDrivenRendererGroupDataNative rendererDataNative, List<Mesh> meshes, List<Material> materials, GPUDrivenRendererDataCallback target)
		{
			callback(in rendererDataNative, meshes, materials, target);
		}
	}
}
