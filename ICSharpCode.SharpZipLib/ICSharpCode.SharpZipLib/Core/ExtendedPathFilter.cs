using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000BA RID: 186
	public class ExtendedPathFilter : PathFilter
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x0001AB0F File Offset: 0x00018D0F
		public ExtendedPathFilter(string filter, long minSize, long maxSize)
			: base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001AB4B File Offset: 0x00018D4B
		public ExtendedPathFilter(string filter, DateTime minDate, DateTime maxDate)
			: base(filter)
		{
			this.MinDate = minDate;
			this.MaxDate = maxDate;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001AB88 File Offset: 0x00018D88
		public ExtendedPathFilter(string filter, long minSize, long maxSize, DateTime minDate, DateTime maxDate)
			: base(filter)
		{
			this.MinSize = minSize;
			this.MaxSize = maxSize;
			this.MinDate = minDate;
			this.MaxDate = maxDate;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001ABE0 File Offset: 0x00018DE0
		public override bool IsMatch(string name)
		{
			bool flag = base.IsMatch(name);
			if (flag)
			{
				FileInfo fileInfo = new FileInfo(name);
				flag = this.MinSize <= fileInfo.Length && this.MaxSize >= fileInfo.Length && this.MinDate <= fileInfo.LastWriteTime && this.MaxDate >= fileInfo.LastWriteTime;
			}
			return flag;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001AC44 File Offset: 0x00018E44
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0001AC4C File Offset: 0x00018E4C
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

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001AC6E File Offset: 0x00018E6E
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0001AC76 File Offset: 0x00018E76
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001AC98 File Offset: 0x00018E98
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		public DateTime MinDate
		{
			get
			{
				return this.minDate_;
			}
			set
			{
				if (value > this.maxDate_)
				{
					throw new ArgumentOutOfRangeException("value", "Exceeds MaxDate");
				}
				this.minDate_ = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001ACC7 File Offset: 0x00018EC7
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001ACCF File Offset: 0x00018ECF
		public DateTime MaxDate
		{
			get
			{
				return this.maxDate_;
			}
			set
			{
				if (this.minDate_ > value)
				{
					throw new ArgumentOutOfRangeException("value", "Exceeds MinDate");
				}
				this.maxDate_ = value;
			}
		}

		// Token: 0x04000445 RID: 1093
		private long minSize_;

		// Token: 0x04000446 RID: 1094
		private long maxSize_ = long.MaxValue;

		// Token: 0x04000447 RID: 1095
		private DateTime minDate_ = DateTime.MinValue;

		// Token: 0x04000448 RID: 1096
		private DateTime maxDate_ = DateTime.MaxValue;
	}
}
