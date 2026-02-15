using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000035 RID: 53
	[StaticAccessor("AnimationPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableOutput.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationPlayableOutput.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	public struct AnimationPlayableOutput : IPlayableOutput
	{
		// Token: 0x06000284 RID: 644 RVA: 0x00006148 File Offset: 0x00004348
		public static AnimationPlayableOutput Create(PlayableGraph graph, string name, Animator target)
		{
			PlayableOutputHandle handle;
			bool flag = !AnimationPlayableGraphExtensions.InternalCreateAnimationOutput(ref graph, name, out handle);
			AnimationPlayableOutput animationPlayableOutput;
			if (flag)
			{
				animationPlayableOutput = AnimationPlayableOutput.Null;
			}
			else
			{
				AnimationPlayableOutput output = new AnimationPlayableOutput(handle);
				output.SetTarget(target);
				animationPlayableOutput = output;
			}
			return animationPlayableOutput;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00006188 File Offset: 0x00004388
		internal AnimationPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<AnimationPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000061C4 File Offset: 0x000043C4
		public static AnimationPlayableOutput Null
		{
			get
			{
				return new AnimationPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000061E0 File Offset: 0x000043E0
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000061F8 File Offset: 0x000043F8
		public static implicit operator PlayableOutput(AnimationPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00006218 File Offset: 0x00004418
		public static explicit operator AnimationPlayableOutput(PlayableOutput output)
		{
			return new AnimationPlayableOutput(output.GetHandle());
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00006238 File Offset: 0x00004438
		public Animator GetTarget()
		{
			return AnimationPlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00006255 File Offset: 0x00004455
		public void SetTarget(Animator value)
		{
			AnimationPlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00006268 File Offset: 0x00004468
		[NativeThrows]
		private static Animator InternalGetTarget(ref PlayableOutputHandle handle)
		{
			return Unmarshal.UnmarshalUnityObject<Animator>(AnimationPlayableOutput.InternalGetTarget_Injected(ref handle));
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00006280 File Offset: 0x00004480
		[NativeThrows]
		private static void InternalSetTarget(ref PlayableOutputHandle handle, Animator target)
		{
			AnimationPlayableOutput.InternalSetTarget_Injected(ref handle, Object.MarshalledUnityObject.Marshal<Animator>(target));
		}

		// Token: 0x0600028E RID: 654
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetTarget_Injected(ref PlayableOutputHandle handle);

		// Token: 0x0600028F RID: 655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetTarget_Injected(ref PlayableOutputHandle handle, IntPtr target);

		// Token: 0x040000D5 RID: 213
		private PlayableOutputHandle m_Handle;
	}
}
