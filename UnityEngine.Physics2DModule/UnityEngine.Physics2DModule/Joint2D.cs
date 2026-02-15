using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[RequireComponent(typeof(Transform), typeof(Rigidbody2D))]
	[NativeHeader("Modules/Physics2D/Joint2D.h")]
	public class Joint2D : Behaviour
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002F74 File Offset: 0x00001174
		public Rigidbody2D connectedBody
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Joint2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Rigidbody2D>(Joint2D.get_connectedBody_Injected(intPtr));
			}
		}

		// Token: 0x06000083 RID: 131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_connectedBody_Injected(IntPtr _unity_self);
	}
}
