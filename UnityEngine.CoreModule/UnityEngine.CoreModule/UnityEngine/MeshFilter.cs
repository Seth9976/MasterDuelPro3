using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000FC RID: 252
	[NativeHeader("Runtime/Graphics/Mesh/MeshFilter.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class MeshFilter : Component
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x00003D56 File Offset: 0x00001F56
		[RequiredByNativeCode]
		private void DontStripMeshFilter()
		{
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00012C58 File Offset: 0x00010E58
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x00012C80 File Offset: 0x00010E80
		public Mesh sharedMesh
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(MeshFilter.get_sharedMesh_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshFilter.set_sharedMesh_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00012CA8 File Offset: 0x00010EA8
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00012CD0 File Offset: 0x00010ED0
		public Mesh mesh
		{
			[NativeName("GetInstantiatedMeshFromScript")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(MeshFilter.get_mesh_Injected(intPtr));
			}
			[NativeName("SetInstantiatedMesh")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshFilter.set_mesh_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x06000A0F RID: 2575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sharedMesh_Injected(IntPtr _unity_self);

		// Token: 0x06000A10 RID: 2576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sharedMesh_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000A11 RID: 2577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_mesh_Injected(IntPtr _unity_self);

		// Token: 0x06000A12 RID: 2578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mesh_Injected(IntPtr _unity_self, IntPtr value);
	}
}
