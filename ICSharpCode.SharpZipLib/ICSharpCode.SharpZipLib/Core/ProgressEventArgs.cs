using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000AC RID: 172
	public class ProgressEventArgs : EventArgs
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x0001A312 File Offset: 0x00018512
		public ProgressEventArgs(string name, long processed, long target)
		{
			this.name_ = name;
			this.processed_ = processed;
			this.target_ = target;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001A336 File Offset: 0x00018536
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0001A33E File Offset: 0x0001853E
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0001A346 File Offset: 0x00018546
		public bool ContinueRunning
		{
			get
			{
				return this.continueRunning_;
			}
			set
			{
				this.continueRunning_ = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0001A350 File Offset: 0x00018550
		public float PercentComplete
		{
			get
			{
				float num;
				if (this.target_ <= 0L)
				{
					num = 0f;
				}
				else
				{
					num = (float)this.processed_ / (float)this.target_ * 100f;
				}
				return num;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001A386 File Offset: 0x00018586
		public long Processed
		{
			get
			{
				return this.processed_;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001A38E File Offset: 0x0001858E
		public long Target
		{
			get
			{
				return this.target_;
			}
		}

		// Token: 0x04000431 RID: 1073
		private string name_;

		// Token: 0x04000432 RID: 1074
		private long processed_;

		// Token: 0x04000433 RID: 1075
		private long target_;

		// Token: 0x04000434 RID: 1076
		private bool continueRunning_ = true;
	}
}
