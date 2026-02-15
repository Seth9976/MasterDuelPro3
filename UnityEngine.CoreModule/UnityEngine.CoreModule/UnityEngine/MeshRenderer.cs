using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000124 RID: 292
	[NativeHeader("Runtime/Graphics/Mesh/MeshRenderer.h")]
	public class MeshRenderer : Renderer
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x00003D56 File Offset: 0x00001F56
		[RequiredByNativeCode]
		private void DontStripMeshRenderer()
		{
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00012D1C File Offset: 0x00010F1C
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x00012D44 File Offset: 0x00010F44
		public Mesh additionalVertexStreams
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(MeshRenderer.get_additionalVertexStreams_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshRenderer.set_additionalVertexStreams_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00012D6C File Offset: 0x00010F6C
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00012D94 File Offset: 0x00010F94
		public Mesh enlightenVertexStream
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(MeshRenderer.get_enlightenVertexStream_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshRenderer.set_enlightenVertexStream_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00012DBC File Offset: 0x00010FBC
		public int subMeshStartIndex
		{
			[NativeName("GetSubMeshStartIndex")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MeshRenderer.get_subMeshStartIndex_Injected(intPtr);
			}
		}

		// Token: 0x06000A1C RID: 2588
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_additionalVertexStreams_Injected(IntPtr _unity_self);

		// Token: 0x06000A1D RID: 2589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_additionalVertexStreams_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000A1E RID: 2590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_enlightenVertexStream_Injected(IntPtr _unity_self);

		// Token: 0x06000A1F RID: 2591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enlightenVertexStream_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000A20 RID: 2592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_subMeshStartIndex_Injected(IntPtr _unity_self);
	}
}
