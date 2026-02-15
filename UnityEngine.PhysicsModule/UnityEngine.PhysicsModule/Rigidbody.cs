using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[NativeHeader("Modules/Physics/Rigidbody.h")]
	[RequireComponent(typeof(Transform))]
	public class Rigidbody : Component
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003494 File Offset: 0x00001694
		public Vector3 linearVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Rigidbody.get_linearVelocity_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000034BC File Offset: 0x000016BC
		public float mass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody.get_mass_Injected(intPtr);
			}
		}

		// Token: 0x17000028 RID: 40
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000034E0 File Offset: 0x000016E0
		public bool isKinematic
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_isKinematic_Injected(intPtr, value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003504 File Offset: 0x00001704
		public bool detectCollisions
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_detectCollisions_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003528 File Offset: 0x00001728
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003550 File Offset: 0x00001750
		public Vector3 position
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Rigidbody.get_position_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_position_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003574 File Offset: 0x00001774
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000359C File Offset: 0x0000179C
		public Quaternion rotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Rigidbody.get_rotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody.set_rotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000035C0 File Offset: 0x000017C0
		public void MovePosition(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.MovePosition_Injected(intPtr, ref position);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000035E4 File Offset: 0x000017E4
		public void MoveRotation(Quaternion rot)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody.MoveRotation_Injected(intPtr, ref rot);
		}

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_linearVelocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_mass_Injected(IntPtr _unity_self);

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isKinematic_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_detectCollisions_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_position_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_position_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x060000A9 RID: 169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MovePosition_Injected(IntPtr _unity_self, [In] ref Vector3 position);

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveRotation_Injected(IntPtr _unity_self, [In] ref Quaternion rot);
	}
}
