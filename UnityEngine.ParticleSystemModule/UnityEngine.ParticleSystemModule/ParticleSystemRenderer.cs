using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemRendererScriptBindings.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemRenderer.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	public sealed class ParticleSystemRenderer : Renderer
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002374 File Offset: 0x00000574
		[RequiredByNativeCode]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMeshes", HasExplicitThis = true)]
		public int GetMeshes([NotNull] [Out] Mesh[] meshes)
		{
			if (meshes == null)
			{
				ThrowHelper.ThrowArgumentNullException(meshes, "meshes");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ParticleSystemRenderer.GetMeshes_Injected(intPtr, meshes);
		}

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMeshes_Injected(IntPtr _unity_self, [Out] Mesh[] meshes);
	}
}
