using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	[ExcludeFromObjectFactory]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h")]
	public class RuntimeAnimatorController : Object
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00005990 File Offset: 0x00003B90
		protected RuntimeAnimatorController()
		{
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000059C0 File Offset: 0x00003BC0
		public AnimationClip[] animationClips
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RuntimeAnimatorController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RuntimeAnimatorController.get_animationClips_Injected(intPtr);
			}
		}

		// Token: 0x06000241 RID: 577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationClip[] get_animationClips_Injected(IntPtr _unity_self);
	}
}
