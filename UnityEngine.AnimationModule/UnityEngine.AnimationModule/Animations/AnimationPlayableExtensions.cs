using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x02000033 RID: 51
	[NativeHeader("Modules/Animation/AnimationClip.h")]
	[NativeHeader("Modules/Animation/Director/AnimationPlayableExtensions.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	public static class AnimationPlayableExtensions
	{
		// Token: 0x0600027F RID: 639 RVA: 0x000060AC File Offset: 0x000042AC
		public static void SetAnimatedProperties<U>(this U playable, AnimationClip clip) where U : struct, IPlayable
		{
			PlayableHandle handle = playable.GetHandle();
			AnimationPlayableExtensions.SetAnimatedPropertiesInternal(ref handle, clip);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000060D4 File Offset: 0x000042D4
		[NativeThrows]
		internal static void SetAnimatedPropertiesInternal(ref PlayableHandle playable, AnimationClip animatedProperties)
		{
			AnimationPlayableExtensions.SetAnimatedPropertiesInternal_Injected(ref playable, Object.MarshalledUnityObject.Marshal<AnimationClip>(animatedProperties));
		}

		// Token: 0x06000281 RID: 641
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAnimatedPropertiesInternal_Injected(ref PlayableHandle playable, IntPtr animatedProperties);
	}
}
