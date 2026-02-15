using System;
using System.Text;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000283 RID: 643
	internal struct Name
	{
		// Token: 0x06001165 RID: 4453 RVA: 0x0003F0DD File Offset: 0x0003D2DD
		public Name(string name, bool computeUTF8ByteCount = false)
		{
			this.name = name;
			this.utf8ByteCount = ((name != null && name.Length > 0 && computeUTF8ByteCount) ? Encoding.UTF8.GetByteCount(name) : 0);
		}

		// Token: 0x04000B3D RID: 2877
		public readonly string name;

		// Token: 0x04000B3E RID: 2878
		public readonly int utf8ByteCount;
	}
}
