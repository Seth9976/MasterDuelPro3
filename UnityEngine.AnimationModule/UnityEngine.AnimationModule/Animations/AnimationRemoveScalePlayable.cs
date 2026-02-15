using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000037 RID: 55
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[RequiredByNativeCode]
	[StaticAccessor("AnimationRemoveScalePlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationRemoveScalePlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationRemoveScalePlayable.h")]
	internal struct AnimationRemoveScalePlayable : IPlayable, IEquatable<AnimationRemoveScalePlayable>
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000632C File Offset: 0x0000452C
		public static AnimationRemoveScalePlayable Create(PlayableGraph graph, int inputCount)
		{
			PlayableHandle handle = AnimationRemoveScalePlayable.CreateHandle(graph, inputCount);
			return new AnimationRemoveScalePlayable(handle);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000634C File Offset: 0x0000454C
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount)
		{
			PlayableHandle handle = PlayableHandle.Null;
			bool flag = !AnimationRemoveScalePlayable.CreateHandleInternal(graph, ref handle);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				handle.SetInputCount(inputCount);
				playableHandle = handle;
			}
			return playableHandle;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00006388 File Offset: 0x00004588
		internal AnimationRemoveScalePlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationRemoveScalePlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationRemoveScalePlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000063C4 File Offset: 0x000045C4
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000063DC File Offset: 0x000045DC
		public static implicit operator Playable(AnimationRemoveScalePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000063FC File Offset: 0x000045FC
		public bool Equals(AnimationRemoveScalePlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00006428 File Offset: 0x00004628
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationRemoveScalePlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x0600029C RID: 668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x040000D8 RID: 216
		private PlayableHandle m_Handle;

		// Token: 0x040000D9 RID: 217
		private static readonly AnimationRemoveScalePlayable m_NullPlayable = new AnimationRemoveScalePlayable(PlayableHandle.Null);
	}
}
