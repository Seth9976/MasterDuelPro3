using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/Physics2D/Public/Rigidbody2D.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class Rigidbody2D : Component
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002910 File Offset: 0x00000B10
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002938 File Offset: 0x00000B38
		public Vector2 position
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Rigidbody2D.get_position_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_position_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000295C File Offset: 0x00000B5C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002980 File Offset: 0x00000B80
		public float rotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_rotation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_rotation_Injected(intPtr, value);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void MovePosition(Vector2 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.MovePosition_Injected(intPtr, ref position);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029C8 File Offset: 0x00000BC8
		public void MoveRotation(float angle)
		{
			this.MoveRotation_Angle(angle);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029D4 File Offset: 0x00000BD4
		[NativeMethod("MoveRotation")]
		private void MoveRotation_Angle(float angle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rigidbody2D.MoveRotation_Angle_Injected(intPtr, angle);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000029F8 File Offset: 0x00000BF8
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002A20 File Offset: 0x00000C20
		public Vector2 linearVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Rigidbody2D.get_linearVelocity_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_linearVelocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002A44 File Offset: 0x00000C44
		public float mass
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_mass_Injected(intPtr);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002A68 File Offset: 0x00000C68
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002A8C File Offset: 0x00000C8C
		public float gravityScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Rigidbody2D.get_gravityScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_gravityScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public RigidbodyType2D bodyType
		{
			[NativeMethod("SetBodyType_Binding")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rigidbody2D.set_bodyType_Injected(intPtr, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002AD3 File Offset: 0x00000CD3
		[Obsolete("Please use Rigidbody2D.bodyType instead.", false)]
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool isKinematic
		{
			set
			{
				this.bodyType = (value ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic);
			}
		}

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_position_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_position_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_rotation_Injected(IntPtr _unity_self);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotation_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MovePosition_Injected(IntPtr _unity_self, [In] ref Vector2 position);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveRotation_Angle_Injected(IntPtr _unity_self, float angle);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_linearVelocity_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearVelocity_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_mass_Injected(IntPtr _unity_self);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_gravityScale_Injected(IntPtr _unity_self);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gravityScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bodyType_Injected(IntPtr _unity_self, RigidbodyType2D value);
	}
}
