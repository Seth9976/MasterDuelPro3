using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("Modules/Animation/Director/AnimationClipPlayable.h")]
	[StaticAccessor("AnimationClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClipPlayable.bindings.h")]
	[RequiredByNativeCode]
	public struct AnimationClipPlayable : IPlayable, IEquatable<AnimationClipPlayable>
	{
		// Token: 0x06000245 RID: 581 RVA: 0x00005A34 File Offset: 0x00003C34
		public static AnimationClipPlayable Create(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle handle = AnimationClipPlayable.CreateHandle(graph, clip);
			return new AnimationClipPlayable(handle);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00005A54 File Offset: 0x00003C54
		private static PlayableHandle CreateHandle(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle handle = PlayableHandle.Null;
			bool flag = !AnimationClipPlayable.CreateHandleInternal(graph, clip, ref handle);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = handle;
			}
			return playableHandle;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00005A88 File Offset: 0x00003C88
		internal AnimationClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00005ADC File Offset: 0x00003CDC
		public static implicit operator Playable(AnimationClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00005AFC File Offset: 0x00003CFC
		public bool Equals(AnimationClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00005B20 File Offset: 0x00003D20
		public void SetApplyFootIK(bool value)
		{
			AnimationClipPlayable.SetApplyFootIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00005B30 File Offset: 0x00003D30
		internal void SetRemoveStartOffset(bool value)
		{
			AnimationClipPlayable.SetRemoveStartOffsetInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00005B40 File Offset: 0x00003D40
		internal void SetOverrideLoopTime(bool value)
		{
			AnimationClipPlayable.SetOverrideLoopTimeInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00005B50 File Offset: 0x00003D50
		internal void SetLoopTime(bool value)
		{
			AnimationClipPlayable.SetLoopTimeInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00005B60 File Offset: 0x00003D60
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle)
		{
			return AnimationClipPlayable.CreateHandleInternal_Injected(ref graph, Object.MarshalledUnityObject.Marshal<AnimationClip>(clip), ref handle);
		}

		// Token: 0x06000250 RID: 592
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000251 RID: 593
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRemoveStartOffsetInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000252 RID: 594
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetOverrideLoopTimeInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000253 RID: 595
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoopTimeInternal(ref PlayableHandle handle, bool value);

		// Token: 0x06000254 RID: 596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, IntPtr clip, ref PlayableHandle handle);

		// Token: 0x040000CB RID: 203
		private PlayableHandle m_Handle;
	}
}
