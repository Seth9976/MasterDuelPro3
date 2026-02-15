using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Physics/MeshCollider.h")]
	[NativeHeader("Runtime/Graphics/Mesh/Mesh.h")]
	public class MeshCollider : Collider
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002D80 File Offset: 0x00000F80
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public Mesh sharedMesh
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(MeshCollider.get_sharedMesh_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshCollider.set_sharedMesh_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002DD0 File Offset: 0x00000FD0
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public bool convex
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MeshCollider.get_convex_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshCollider.set_convex_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002E18 File Offset: 0x00001018
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002E3C File Offset: 0x0000103C
		public MeshColliderCookingOptions cookingOptions
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MeshCollider.get_cookingOptions_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshCollider.set_cookingOptions_Injected(intPtr, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002E60 File Offset: 0x00001060
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002E73 File Offset: 0x00001073
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Configuring smooth sphere collisions is no longer needed.", true)]
		public bool smoothSphereCollisions
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002E78 File Offset: 0x00001078
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002E73 File Offset: 0x00001073
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("MeshCollider.skinWidth is no longer used.")]
		public float skinWidth
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002E90 File Offset: 0x00001090
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002E73 File Offset: 0x00001073
		[Obsolete("MeshCollider.inflateMesh is no longer supported. The new cooking algorithm doesn't need inflation to be used.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool inflateMesh
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06000065 RID: 101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_sharedMesh_Injected(IntPtr _unity_self);

		// Token: 0x06000066 RID: 102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sharedMesh_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_convex_Injected(IntPtr _unity_self);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_convex_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000069 RID: 105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MeshColliderCookingOptions get_cookingOptions_Injected(IntPtr _unity_self);

		// Token: 0x0600006A RID: 106
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cookingOptions_Injected(IntPtr _unity_self, MeshColliderCookingOptions value);
	}
}
