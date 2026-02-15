using System;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x020003F1 RID: 1009
	[NativeHeader("Runtime/Shaders/Director/MaterialEffectPlayable.h")]
	[NativeHeader("Runtime/Export/Director/MaterialEffectPlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[RequiredByNativeCode]
	[StaticAccessor("MaterialEffectPlayableBindings", StaticAccessorType.DoubleColon)]
	public struct MaterialEffectPlayable : IPlayable, IEquatable<MaterialEffectPlayable>
	{
		// Token: 0x06001B42 RID: 6978 RVA: 0x0003C7A8 File Offset: 0x0003A9A8
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0003C7C0 File Offset: 0x0003A9C0
		public bool Equals(MaterialEffectPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000D81 RID: 3457
		private PlayableHandle m_Handle;
	}
}
