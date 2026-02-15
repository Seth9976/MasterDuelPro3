using System;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000036 RID: 54
	[RequiredByNativeCode]
	[StaticAccessor("AnimationPosePlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/Director/AnimationPosePlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPosePlayable.bindings.h")]
	internal struct AnimationPosePlayable : IPlayable, IEquatable<AnimationPosePlayable>
	{
		// Token: 0x06000290 RID: 656 RVA: 0x0000629C File Offset: 0x0000449C
		internal AnimationPosePlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationPosePlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationPosePlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000062D8 File Offset: 0x000044D8
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000062F0 File Offset: 0x000044F0
		public bool Equals(AnimationPosePlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x040000D6 RID: 214
		private PlayableHandle m_Handle;

		// Token: 0x040000D7 RID: 215
		private static readonly AnimationPosePlayable m_NullPlayable = new AnimationPosePlayable(PlayableHandle.Null);
	}
}
