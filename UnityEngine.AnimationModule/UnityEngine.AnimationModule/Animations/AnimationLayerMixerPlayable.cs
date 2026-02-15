using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200002F RID: 47
	[StaticAccessor("AnimationLayerMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationLayerMixerPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationLayerMixerPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[RequiredByNativeCode]
	public struct AnimationLayerMixerPlayable : IPlayable, IEquatable<AnimationLayerMixerPlayable>
	{
		// Token: 0x06000255 RID: 597 RVA: 0x00005B7C File Offset: 0x00003D7C
		public static AnimationLayerMixerPlayable Create(PlayableGraph graph, int inputCount, bool singleLayerOptimization)
		{
			PlayableHandle handle = AnimationLayerMixerPlayable.CreateHandle(graph, inputCount);
			AnimationLayerMixerPlayable mixer = new AnimationLayerMixerPlayable(handle, singleLayerOptimization);
			return mixer;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00005BA0 File Offset: 0x00003DA0
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle handle = PlayableHandle.Null;
			bool flag = !AnimationLayerMixerPlayable.CreateHandleInternal(graph, ref handle);
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

		// Token: 0x06000257 RID: 599 RVA: 0x00005BDC File Offset: 0x00003DDC
		internal AnimationLayerMixerPlayable(PlayableHandle handle, bool singleLayerOptimization = true)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationLayerMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationLayerMixerPlayable.");
				}
				AnimationLayerMixerPlayable.SetSingleLayerOptimizationInternal(ref handle, singleLayerOptimization);
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00005C20 File Offset: 0x00003E20
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00005C38 File Offset: 0x00003E38
		public static implicit operator Playable(AnimationLayerMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00005C58 File Offset: 0x00003E58
		public bool Equals(AnimationLayerMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00005C7C File Offset: 0x00003E7C
		public void SetLayerMaskFromAvatarMask(uint layerIndex, AvatarMask mask)
		{
			bool flag = (ulong)layerIndex >= (ulong)((long)this.m_Handle.GetInputCount());
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layerIndex", string.Format("layerIndex {0} must be in the range of 0 to {1}.", layerIndex, this.m_Handle.GetInputCount() - 1));
			}
			bool flag2 = mask == null;
			if (flag2)
			{
				throw new ArgumentNullException("mask");
			}
			AnimationLayerMixerPlayable.SetLayerMaskFromAvatarMaskInternal(ref this.m_Handle, layerIndex, mask);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00005CF4 File Offset: 0x00003EF4
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationLayerMixerPlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x0600025D RID: 605
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSingleLayerOptimizationInternal(ref PlayableHandle handle, bool value);

		// Token: 0x0600025E RID: 606 RVA: 0x00005D0C File Offset: 0x00003F0C
		[NativeThrows]
		private static void SetLayerMaskFromAvatarMaskInternal(ref PlayableHandle handle, uint layerIndex, AvatarMask mask)
		{
			AnimationLayerMixerPlayable.SetLayerMaskFromAvatarMaskInternal_Injected(ref handle, layerIndex, Object.MarshalledUnityObject.Marshal<AvatarMask>(mask));
		}

		// Token: 0x06000260 RID: 608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x06000261 RID: 609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerMaskFromAvatarMaskInternal_Injected(ref PlayableHandle handle, uint layerIndex, IntPtr mask);

		// Token: 0x040000CD RID: 205
		private PlayableHandle m_Handle;

		// Token: 0x040000CE RID: 206
		private static readonly AnimationLayerMixerPlayable m_NullPlayable = new AnimationLayerMixerPlayable(PlayableHandle.Null, true);
	}
}
