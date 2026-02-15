using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[NativeHeader("Modules/Animation/Motion.h")]
	public class Motion : Object
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00005990 File Offset: 0x00003B90
		protected Motion()
		{
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000599C File Offset: 0x00003B9C
		public bool isLooping
		{
			[NativeMethod("IsLooping")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Motion>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Motion.get_isLooping_Injected(intPtr);
			}
		}

		// Token: 0x0600023E RID: 574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isLooping_Injected(IntPtr _unity_self);
	}
}
