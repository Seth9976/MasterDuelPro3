using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A2 RID: 674
	internal struct ColorPage
	{
		// Token: 0x0600122F RID: 4655 RVA: 0x0004C184 File Offset: 0x0004A384
		public static ColorPage Init(RenderChain renderChain, BMPAlloc alloc)
		{
			bool isValid = alloc.IsValid();
			return new ColorPage
			{
				isValid = isValid,
				pageAndID = (isValid ? renderChain.shaderInfoAllocator.ColorAllocToVertexData(alloc) : default(Color32))
			};
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0004C1D0 File Offset: 0x0004A3D0
		public MeshBuilderNative.NativeColorPage ToNativeColorPage()
		{
			return new MeshBuilderNative.NativeColorPage
			{
				isValid = (this.isValid ? 1 : 0),
				pageAndID = this.pageAndID
			};
		}

		// Token: 0x04000A8E RID: 2702
		public bool isValid;

		// Token: 0x04000A8F RID: 2703
		public Color32 pageAndID;
	}
}
