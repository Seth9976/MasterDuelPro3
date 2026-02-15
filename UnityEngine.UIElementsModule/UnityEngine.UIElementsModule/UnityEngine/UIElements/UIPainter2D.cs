using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000276 RID: 630
	[NativeHeader("Modules/UIElements/Core/Native/Renderer/UIPainter2D.bindings.h")]
	internal static class UIPainter2D
	{
		// Token: 0x060010ED RID: 4333
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Create(bool computeBBox = false);

		// Token: 0x060010EE RID: 4334
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Destroy(IntPtr handle);

		// Token: 0x060010EF RID: 4335
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Reset(IntPtr handle);

		// Token: 0x060010F0 RID: 4336
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearSnapshots(IntPtr handle);

		// Token: 0x060010F1 RID: 4337 RVA: 0x00048D40 File Offset: 0x00046F40
		[ThreadSafe]
		public static MeshWriteDataInterface ExecuteSnapshotFromJob(IntPtr painterHandle, int i)
		{
			MeshWriteDataInterface meshWriteDataInterface;
			UIPainter2D.ExecuteSnapshotFromJob_Injected(painterHandle, i, out meshWriteDataInterface);
			return meshWriteDataInterface;
		}

		// Token: 0x060010F2 RID: 4338
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExecuteSnapshotFromJob_Injected(IntPtr painterHandle, int i, out MeshWriteDataInterface ret);
	}
}
