using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001E8 RID: 488
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class VolumeComponentMenu : Attribute
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00033961 File Offset: 0x00031B61
		public VolumeComponentMenu(string menu)
		{
			this.menu = menu;
		}

		// Token: 0x0400093F RID: 2367
		public readonly string menu;
	}
}
