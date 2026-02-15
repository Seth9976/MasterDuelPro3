using System;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200003A RID: 58
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h")]
	[StaticAccessor("AnimatorControllerPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/Animator.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimatorControllerPlayable.h")]
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	public struct AnimatorControllerPlayable : IPlayable, IEquatable<AnimatorControllerPlayable>
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x000064D9 File Offset: 0x000046D9
		internal AnimatorControllerPlayable(PlayableHandle handle)
		{
			this.m_Handle = PlayableHandle.Null;
			this.SetHandle(handle);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000064F0 File Offset: 0x000046F0
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00006508 File Offset: 0x00004708
		public void SetHandle(PlayableHandle handle)
		{
			bool flag = this.m_Handle.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("Cannot call IPlayable.SetHandle on an instance that already contains a valid handle.");
			}
			bool flag2 = handle.IsValid();
			if (flag2)
			{
				bool flag3 = !handle.IsPlayableOfType<AnimatorControllerPlayable>();
				if (flag3)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimatorControllerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00006560 File Offset: 0x00004760
		public bool Equals(AnimatorControllerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x040000E3 RID: 227
		private PlayableHandle m_Handle;

		// Token: 0x040000E4 RID: 228
		private static readonly AnimatorControllerPlayable m_NullPlayable = new AnimatorControllerPlayable(PlayableHandle.Null);
	}
}
