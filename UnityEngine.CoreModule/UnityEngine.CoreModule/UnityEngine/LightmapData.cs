using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E0 RID: 224
	[NativeHeader("Runtime/Graphics/LightmapData.h")]
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LightmapData
	{
		// Token: 0x040002A1 RID: 673
		internal Texture2D m_Light;

		// Token: 0x040002A2 RID: 674
		internal Texture2D m_Dir;

		// Token: 0x040002A3 RID: 675
		internal Texture2D m_ShadowMask;
	}
}
