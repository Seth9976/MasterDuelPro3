using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	[NativeHeader("Modules/Physics2D/AnchoredJoint2D.h")]
	public class AnchoredJoint2D : Joint2D
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002F9C File Offset: 0x0000119C
		public Vector2 connectedAnchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AnchoredJoint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				AnchoredJoint2D.get_connectedAnchor_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x06000085 RID: 133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_connectedAnchor_Injected(IntPtr _unity_self, out Vector2 ret);
	}
}
