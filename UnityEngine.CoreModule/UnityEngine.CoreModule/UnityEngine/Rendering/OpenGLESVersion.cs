using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000354 RID: 852
	public enum OpenGLESVersion
	{
		// Token: 0x04000A00 RID: 2560
		None,
		// Token: 0x04000A01 RID: 2561
		[Obsolete("OpenGL ES 2.0 is no longer supported in Unity 2023.1")]
		OpenGLES20,
		// Token: 0x04000A02 RID: 2562
		OpenGLES30,
		// Token: 0x04000A03 RID: 2563
		OpenGLES31,
		// Token: 0x04000A04 RID: 2564
		OpenGLES31AEP,
		// Token: 0x04000A05 RID: 2565
		OpenGLES32
	}
}
