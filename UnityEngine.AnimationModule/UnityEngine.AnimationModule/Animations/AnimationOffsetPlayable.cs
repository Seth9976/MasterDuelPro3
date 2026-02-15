using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000032 RID: 50
	[RequiredByNativeCode]
	[StaticAccessor("AnimationOffsetPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationOffsetPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationOffsetPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	internal struct AnimationOffsetPlayable : IPlayable, IEquatable<AnimationOffsetPlayable>
	{
		// Token: 0x06000276 RID: 630 RVA: 0x00005F80 File Offset: 0x00004180
		public static AnimationOffsetPlayable Create(PlayableGraph graph, Vector3 position, Quaternion rotation, int inputCount)
		{
			PlayableHandle handle = AnimationOffsetPlayable.CreateHandle(graph, position, rotation, inputCount);
			return new AnimationOffsetPlayable(handle);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00005FA4 File Offset: 0x000041A4
		private static PlayableHandle CreateHandle(PlayableGraph graph, Vector3 position, Quaternion rotation, int inputCount)
		{
			PlayableHandle handle = PlayableHandle.Null;
			bool flag = !AnimationOffsetPlayable.CreateHandleInternal(graph, position, rotation, ref handle);
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

		// Token: 0x06000278 RID: 632 RVA: 0x00005FE0 File Offset: 0x000041E0
		internal AnimationOffsetPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationOffsetPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationOffsetPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000601C File Offset: 0x0000421C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00006034 File Offset: 0x00004234
		public static implicit operator Playable(AnimationOffsetPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006054 File Offset: 0x00004254
		public bool Equals(AnimationOffsetPlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00006080 File Offset: 0x00004280
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, Vector3 position, Quaternion rotation, ref PlayableHandle handle)
		{
			return AnimationOffsetPlayable.CreateHandleInternal_Injected(ref graph, ref position, ref rotation, ref handle);
		}

		// Token: 0x0600027E RID: 638
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, [In] ref Vector3 position, [In] ref Quaternion rotation, ref PlayableHandle handle);

		// Token: 0x040000D3 RID: 211
		private PlayableHandle m_Handle;

		// Token: 0x040000D4 RID: 212
		private static readonly AnimationOffsetPlayable m_NullPlayable = new AnimationOffsetPlayable(PlayableHandle.Null);
	}
}
