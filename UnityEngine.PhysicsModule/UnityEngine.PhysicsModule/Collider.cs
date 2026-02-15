using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Modules/Physics/Collider.h")]
	public class Collider : Component
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002204 File Offset: 0x00000404
		public bool enabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Collider.get_enabled_Injected(intPtr);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002228 File Offset: 0x00000428
		public Rigidbody attachedRigidbody
		{
			[NativeMethod("GetRigidbody")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Rigidbody>(Collider.get_attachedRigidbody_Injected(intPtr));
			}
		}

		// Token: 0x1700000A RID: 10
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002250 File Offset: 0x00000450
		public bool isTrigger
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Collider.set_isTrigger_Injected(intPtr, value);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002274 File Offset: 0x00000474
		public Vector3 ClosestPoint(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Collider.ClosestPoint_Injected(intPtr, ref position, out vector);
			return vector;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000229C File Offset: 0x0000049C
		public Bounds bounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Collider.get_bounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022C4 File Offset: 0x000004C4
		private RaycastHit Raycast(Ray ray, float maxDistance, ref bool hasHit)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RaycastHit raycastHit;
			Collider.Raycast_Injected(intPtr, ref ray, maxDistance, ref hasHit, out raycastHit);
			return raycastHit;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022F0 File Offset: 0x000004F0
		public bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			bool hasHit = false;
			hitInfo = this.Raycast(ray, maxDistance, ref hasHit);
			return hasHit;
		}

		// Token: 0x06000015 RID: 21
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enabled_Injected(IntPtr _unity_self);

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_attachedRigidbody_Injected(IntPtr _unity_self);

		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isTrigger_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClosestPoint_Injected(IntPtr _unity_self, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x06000019 RID: 25
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x0600001A RID: 26
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Raycast_Injected(IntPtr _unity_self, [In] ref Ray ray, float maxDistance, ref bool hasHit, out RaycastHit ret);
	}
}
