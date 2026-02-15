using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[NativeHeader("Modules/Physics/SphereCollider.h")]
	[RequireComponent(typeof(Transform))]
	public class SphereCollider : Collider
	{
		// Token: 0x1700002C RID: 44
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003608 File Offset: 0x00001808
		public float radius
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SphereCollider>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SphereCollider.set_radius_Injected(intPtr, value);
			}
		}

		// Token: 0x060000AC RID: 172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_radius_Injected(IntPtr _unity_self, float value);
	}
}
