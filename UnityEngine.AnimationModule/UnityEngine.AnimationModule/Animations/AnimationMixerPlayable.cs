using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000030 RID: 48
	[NativeHeader("Modules/Animation/Director/AnimationMixerPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationMixerPlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[RequiredByNativeCode]
	[StaticAccessor("AnimationMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	public struct AnimationMixerPlayable : IPlayable, IEquatable<AnimationMixerPlayable>
	{
		// Token: 0x06000262 RID: 610 RVA: 0x00005D38 File Offset: 0x00003F38
		public static AnimationMixerPlayable Create(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle handle = AnimationMixerPlayable.CreateHandle(graph, inputCount);
			return new AnimationMixerPlayable(handle);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00005D58 File Offset: 0x00003F58
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle handle = PlayableHandle.Null;
			bool flag = !AnimationMixerPlayable.CreateHandleInternal(graph, ref handle);
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

		// Token: 0x06000264 RID: 612 RVA: 0x00005D94 File Offset: 0x00003F94
		internal AnimationMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00005DD0 File Offset: 0x00003FD0
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public static implicit operator Playable(AnimationMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00005E08 File Offset: 0x00004008
		public bool Equals(AnimationMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00005E2C File Offset: 0x0000402C
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationMixerPlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x0600026A RID: 618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x040000CF RID: 207
		private PlayableHandle m_Handle;

		// Token: 0x040000D0 RID: 208
		private static readonly AnimationMixerPlayable m_NullPlayable = new AnimationMixerPlayable(PlayableHandle.Null);
	}
}
