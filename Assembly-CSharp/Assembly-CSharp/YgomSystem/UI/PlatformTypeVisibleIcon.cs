using System;

namespace YgomSystem.UI
{
	// Token: 0x020005B0 RID: 1456
	public class PlatformTypeVisibleIcon : PlatformVisibleIconBase
	{
		// Token: 0x06002DFF RID: 11775 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool IsDispPlatform()
		{
			return false;
		}

		// Token: 0x04002BB4 RID: 11188
		[EnumFlags]
		public PlatformTypeVisibleIcon.PlatformFlags platformFlags;

		// Token: 0x020005B1 RID: 1457
		public enum PlatformFlags
		{
			// Token: 0x04002BB6 RID: 11190
			Console_Pad = 1,
			// Token: 0x04002BB7 RID: 11191
			Console_Point,
			// Token: 0x04002BB8 RID: 11192
			Mobile = 4,
			// Token: 0x04002BB9 RID: 11193
			PC_Pad = 8,
			// Token: 0x04002BBA RID: 11194
			PC_Point = 16
		}
	}
}
