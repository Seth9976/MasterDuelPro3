using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000D9 RID: 217
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public struct RenderBuffer
	{
		// Token: 0x0400028B RID: 651
		internal int m_RenderTextureInstanceID;

		// Token: 0x0400028C RID: 652
		internal IntPtr m_BufferPtr;
	}
}
