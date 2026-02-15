using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000BB RID: 187
	[Obsolete("Use ExtendedPathFilter instead")]
	public class NameAndSizeFilter : PathFilter
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x0001ACF6 File Offset: 0x00018EF6
		public NameAndSizeFilter(string filter, long minSize, long maxSize)
			: base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001AD1C File Offset: 0x00018F1C
		public override bool IsMatch(string name)
		{
			bool flag = base.IsMatch(name);
			if (flag)
			{
				long length = new FileInfo(name).Length;
				flag = this.MinSize <= length && this.MaxSize >= length;
			}
			return flag;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001AD5A File Offset: 0x00018F5A
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0001AD62 File Offset: 0x00018F62
		public long MinSize
		{
			get
			{
				return this.minSize_;
			}
			set
			{
				if (value < 0L || this.maxSize_ < value)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.minSize_ = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001AD84 File Offset: 0x00018F84
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0001AD8C File Offset: 0x00018F8C
		public long MaxSize
		{
			get
			{
				return this.maxSize_;
			}
			set
			{
				if (value < 0L || this.minSize_ > value)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxSize_ = value;
			}
		}

		// Token: 0x04000449 RID: 1097
		private long minSize_;

		// Token: 0x0400044A RID: 1098
		private long maxSize_ = long.MaxValue;
	}
}
