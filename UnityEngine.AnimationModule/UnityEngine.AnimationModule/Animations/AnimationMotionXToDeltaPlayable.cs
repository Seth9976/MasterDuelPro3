using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000031 RID: 49
	[RequiredByNativeCode]
	[StaticAccessor("AnimationMotionXToDeltaPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationMotionXToDeltaPlayable.bindings.h")]
	internal struct AnimationMotionXToDeltaPlayable : IPlayable, IEquatable<AnimationMotionXToDeltaPlayable>
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00005E54 File Offset: 0x00004054
		public static AnimationMotionXToDeltaPlayable Create(PlayableGraph graph)
		{
			PlayableHandle handle = AnimationMotionXToDeltaPlayable.CreateHandle(graph);
			return new AnimationMotionXToDeltaPlayable(handle);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00005E74 File Offset: 0x00004074
		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle handle = PlayableHandle.Null;
			bool flag = !AnimationMotionXToDeltaPlayable.CreateHandleInternal(graph, ref handle);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				handle.SetInputCount(1);
				playableHandle = handle;
			}
			return playableHandle;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00005EB0 File Offset: 0x000040B0
		private AnimationMotionXToDeltaPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationMotionXToDeltaPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationMotionXToDeltaPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00005EEC File Offset: 0x000040EC
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00005F04 File Offset: 0x00004104
		public static implicit operator Playable(AnimationMotionXToDeltaPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00005F24 File Offset: 0x00004124
		public bool Equals(AnimationMotionXToDeltaPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00005F48 File Offset: 0x00004148
		public void SetAbsoluteMotion(bool value)
		{
			AnimationMotionXToDeltaPlayable.SetAbsoluteMotionInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00005F58 File Offset: 0x00004158
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationMotionXToDeltaPlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x06000273 RID: 627
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAbsoluteMotionInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000275 RID: 629
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x040000D1 RID: 209
		private PlayableHandle m_Handle;

		// Token: 0x040000D2 RID: 210
		private static readonly AnimationMotionXToDeltaPlayable m_NullPlayable = new AnimationMotionXToDeltaPlayable(PlayableHandle.Null);
	}
}
