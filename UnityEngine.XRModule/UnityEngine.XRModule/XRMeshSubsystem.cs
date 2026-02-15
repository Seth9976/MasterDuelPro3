using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000020 RID: 32
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshingSubsystem.h")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeConditional("ENABLE_XR")]
	public class XRMeshSubsystem : IntegratedSubsystem<XRMeshSubsystemDescriptor>
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002D34 File Offset: 0x00000F34
		[RequiredByNativeCode]
		private void InvokeMeshReadyDelegate(MeshGenerationResult result, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			bool flag = onMeshGenerationComplete != null;
			if (flag)
			{
				onMeshGenerationComplete(result);
			}
		}

		// Token: 0x02000021 RID: 33
		[NativeConditional("ENABLE_XR")]
		private readonly struct MeshTransformList : IDisposable
		{
			// Token: 0x0600006B RID: 107 RVA: 0x00002D5B File Offset: 0x00000F5B
			public void Dispose()
			{
				XRMeshSubsystem.MeshTransformList.Dispose(this.m_Self);
			}

			// Token: 0x0600006C RID: 108
			[FreeFunction("UnityXRMeshTransformList_Dispose")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void Dispose(IntPtr self);

			// Token: 0x04000096 RID: 150
			private readonly IntPtr m_Self;
		}
	}
}
