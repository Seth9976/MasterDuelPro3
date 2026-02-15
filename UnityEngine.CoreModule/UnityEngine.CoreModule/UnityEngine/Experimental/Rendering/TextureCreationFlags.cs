using System;
using UnityEngine.Internal;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003F8 RID: 1016
	[Flags]
	public enum TextureCreationFlags
	{
		// Token: 0x04000D87 RID: 3463
		None = 0,
		// Token: 0x04000D88 RID: 3464
		MipChain = 1,
		// Token: 0x04000D89 RID: 3465
		DontInitializePixels = 4,
		// Token: 0x04000D8A RID: 3466
		Crunch = 64,
		// Token: 0x04000D8B RID: 3467
		DontUploadUponCreate = 1024,
		// Token: 0x04000D8C RID: 3468
		[Obsolete("IgnoreMipmapLimit flag is no longer used since this is now the default behavior for all Texture shapes. Please provide mipmap limit information using a MipmapLimitDescriptor argument.", false)]
		[ExcludeFromDocs]
		IgnoreMipmapLimit = 2048
	}
}
