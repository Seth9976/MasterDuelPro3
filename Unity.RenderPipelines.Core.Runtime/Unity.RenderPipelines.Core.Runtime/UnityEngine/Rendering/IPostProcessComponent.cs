using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200014B RID: 331
	public interface IPostProcessComponent
	{
		// Token: 0x06000A26 RID: 2598
		bool IsActive();

		// Token: 0x06000A27 RID: 2599 RVA: 0x000090C6 File Offset: 0x000072C6
		[Obsolete("Unused #from(2023.1)", false)]
		bool IsTileCompatible()
		{
			return false;
		}
	}
}
