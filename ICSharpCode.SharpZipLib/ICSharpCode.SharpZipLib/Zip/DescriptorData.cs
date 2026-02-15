using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200003B RID: 59
	public class DescriptorData
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008986 File Offset: 0x00006B86
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000898E File Offset: 0x00006B8E
		public long CompressedSize { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00008997 File Offset: 0x00006B97
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000899F File Offset: 0x00006B9F
		public long Size { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000089A8 File Offset: 0x00006BA8
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x000089B0 File Offset: 0x00006BB0
		public long Crc
		{
			get
			{
				return this._crc;
			}
			set
			{
				this._crc = value & (long)((ulong)(-1));
			}
		}

		// Token: 0x04000121 RID: 289
		private long _crc;
	}
}
