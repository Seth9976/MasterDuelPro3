using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.U2D
{
	// Token: 0x020003FE RID: 1022
	[NativeHeader("Runtime/2D/Renderer/SpriteRendererGroup.h")]
	[RequiredByNativeCode]
	internal struct SpriteIntermediateRendererInfo
	{
		// Token: 0x04000E49 RID: 3657
		public int SpriteID;

		// Token: 0x04000E4A RID: 3658
		public int TextureID;

		// Token: 0x04000E4B RID: 3659
		public int MaterialID;

		// Token: 0x04000E4C RID: 3660
		public Color Color;

		// Token: 0x04000E4D RID: 3661
		public Matrix4x4 Transform;

		// Token: 0x04000E4E RID: 3662
		public Bounds Bounds;

		// Token: 0x04000E4F RID: 3663
		public int Layer;

		// Token: 0x04000E50 RID: 3664
		public int SortingLayer;

		// Token: 0x04000E51 RID: 3665
		public int SortingOrder;

		// Token: 0x04000E52 RID: 3666
		public ulong SceneCullingMask;

		// Token: 0x04000E53 RID: 3667
		public IntPtr IndexData;

		// Token: 0x04000E54 RID: 3668
		public IntPtr VertexData;

		// Token: 0x04000E55 RID: 3669
		public int IndexCount;

		// Token: 0x04000E56 RID: 3670
		public int VertexCount;

		// Token: 0x04000E57 RID: 3671
		public int ShaderChannelMask;
	}
}
