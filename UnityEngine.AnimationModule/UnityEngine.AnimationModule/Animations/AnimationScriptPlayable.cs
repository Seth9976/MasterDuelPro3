using System;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000038 RID: 56
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationScriptPlayable.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AnimationScriptPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	public struct AnimationScriptPlayable : IPlayable, IEquatable<AnimationScriptPlayable>
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00006450 File Offset: 0x00004650
		internal AnimationScriptPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationScriptPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationScriptPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000648C File Offset: 0x0000468C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000064A4 File Offset: 0x000046A4
		public bool Equals(AnimationScriptPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x040000DA RID: 218
		private PlayableHandle m_Handle;

		// Token: 0x040000DB RID: 219
		private static readonly AnimationScriptPlayable m_NullPlayable = new AnimationScriptPlayable(PlayableHandle.Null);
	}
}
