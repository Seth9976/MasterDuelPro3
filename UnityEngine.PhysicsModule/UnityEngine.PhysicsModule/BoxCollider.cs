using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/Physics/BoxCollider.h")]
	[RequireComponent(typeof(Transform))]
	public class BoxCollider : Collider
	{
		// Token: 0x17000006 RID: 6
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000021BC File Offset: 0x000003BC
		public Vector3 center
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BoxCollider.set_center_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000021E0 File Offset: 0x000003E0
		public Vector3 size
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BoxCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BoxCollider.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_center_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600000C RID: 12
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector3 value);
	}
}
