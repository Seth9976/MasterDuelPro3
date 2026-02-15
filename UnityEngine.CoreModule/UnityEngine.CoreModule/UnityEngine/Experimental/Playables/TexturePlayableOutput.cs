using System;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x020003F3 RID: 1011
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Director/TexturePlayableOutput.bindings.h")]
	[NativeHeader("Runtime/Graphics/Director/TexturePlayableOutput.h")]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[StaticAccessor("TexturePlayableOutputBindings", StaticAccessorType.DoubleColon)]
	public struct TexturePlayableOutput : IPlayableOutput
	{
		// Token: 0x06001B46 RID: 6982 RVA: 0x0003C820 File Offset: 0x0003AA20
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x04000D83 RID: 3459
		private PlayableOutputHandle m_Handle;
	}
}
