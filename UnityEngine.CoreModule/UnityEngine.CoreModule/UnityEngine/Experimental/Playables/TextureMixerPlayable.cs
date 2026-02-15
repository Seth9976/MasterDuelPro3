using System;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x020003F2 RID: 1010
	[StaticAccessor("TextureMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Graphics/Director/TextureMixerPlayable.h")]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Director/TextureMixerPlayable.bindings.h")]
	public struct TextureMixerPlayable : IPlayable, IEquatable<TextureMixerPlayable>
	{
		// Token: 0x06001B44 RID: 6980 RVA: 0x0003C7E4 File Offset: 0x0003A9E4
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0003C7FC File Offset: 0x0003A9FC
		public bool Equals(TextureMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000D82 RID: 3458
		private PlayableHandle m_Handle;
	}
}
