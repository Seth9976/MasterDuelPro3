using System;

namespace Mono
{
	// Token: 0x02000011 RID: 17
	internal static class X509Pal
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002796 File Offset: 0x00000996
		public static X509PalImpl Instance
		{
			get
			{
				return SystemDependencyProvider.Instance.X509Pal;
			}
		}
	}
}
