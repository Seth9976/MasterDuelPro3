using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/Avatar.h")]
	public class Avatar : Object
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00005854 File Offset: 0x00003A54
		public bool isValid
		{
			[NativeMethod("IsValid")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Avatar.get_isValid_Injected(intPtr);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00005878 File Offset: 0x00003A78
		public bool isHuman
		{
			[NativeMethod("IsHuman")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Avatar.get_isHuman_Injected(intPtr);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000589C File Offset: 0x00003A9C
		public HumanDescription humanDescription
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HumanDescription humanDescription;
				Avatar.get_humanDescription_Injected(intPtr, out humanDescription);
				return humanDescription;
			}
		}

		// Token: 0x0600022F RID: 559
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isValid_Injected(IntPtr _unity_self);

		// Token: 0x06000230 RID: 560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isHuman_Injected(IntPtr _unity_self);

		// Token: 0x06000231 RID: 561
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_humanDescription_Injected(IntPtr _unity_self, out HumanDescription ret);
	}
}
