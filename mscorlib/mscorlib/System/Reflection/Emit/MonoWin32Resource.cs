using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000655 RID: 1621
	internal struct MonoWin32Resource
	{
		// Token: 0x060030DA RID: 12506 RVA: 0x000B816A File Offset: 0x000B636A
		public MonoWin32Resource(int res_type, int res_id, int lang_id, byte[] data)
		{
			this.res_type = res_type;
			this.res_id = res_id;
			this.lang_id = lang_id;
			this.data = data;
		}

		// Token: 0x040018E8 RID: 6376
		public int res_type;

		// Token: 0x040018E9 RID: 6377
		public int res_id;

		// Token: 0x040018EA RID: 6378
		public int lang_id;

		// Token: 0x040018EB RID: 6379
		public byte[] data;
	}
}
