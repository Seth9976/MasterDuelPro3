using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200000F RID: 15
	public enum TypeAffinity
	{
		// Token: 0x04000041 RID: 65
		Uninitialized,
		// Token: 0x04000042 RID: 66
		Int64,
		// Token: 0x04000043 RID: 67
		Double,
		// Token: 0x04000044 RID: 68
		Text,
		// Token: 0x04000045 RID: 69
		Blob,
		// Token: 0x04000046 RID: 70
		Null,
		// Token: 0x04000047 RID: 71
		DateTime = 10,
		// Token: 0x04000048 RID: 72
		None
	}
}
