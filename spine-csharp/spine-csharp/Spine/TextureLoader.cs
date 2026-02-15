using System;

namespace Spine
{
	// Token: 0x02000048 RID: 72
	public interface TextureLoader
	{
		// Token: 0x060001B5 RID: 437
		void Load(AtlasPage page, string path);

		// Token: 0x060001B6 RID: 438
		void Unload(object texture);
	}
}
