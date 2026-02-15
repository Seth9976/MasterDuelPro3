using System;

namespace DG.Tweening.Core
{
	// Token: 0x020000B1 RID: 177
	internal struct SafeModeReport
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000113B2 File Offset: 0x0000F5B2
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x000113BA File Offset: 0x0000F5BA
		public int totMissingTargetOrFieldErrors { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000113C3 File Offset: 0x0000F5C3
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x000113CB File Offset: 0x0000F5CB
		public int totCallbackErrors { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x000113D4 File Offset: 0x0000F5D4
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x000113DC File Offset: 0x0000F5DC
		public int totStartupErrors { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x000113E5 File Offset: 0x0000F5E5
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x000113ED File Offset: 0x0000F5ED
		public int totUnsetErrors { get; private set; }

		// Token: 0x06000420 RID: 1056 RVA: 0x000113F8 File Offset: 0x0000F5F8
		public void Add(SafeModeReport.SafeModeReportType type)
		{
			switch (type)
			{
			case SafeModeReport.SafeModeReportType.TargetOrFieldMissing:
			{
				int num = this.totMissingTargetOrFieldErrors;
				this.totMissingTargetOrFieldErrors = num + 1;
				return;
			}
			case SafeModeReport.SafeModeReportType.Callback:
			{
				int num = this.totCallbackErrors;
				this.totCallbackErrors = num + 1;
				return;
			}
			case SafeModeReport.SafeModeReportType.StartupFailure:
			{
				int num = this.totStartupErrors;
				this.totStartupErrors = num + 1;
				return;
			}
			default:
			{
				int num = this.totUnsetErrors;
				this.totUnsetErrors = num + 1;
				return;
			}
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001145E File Offset: 0x0000F65E
		public int GetTotErrors()
		{
			return this.totMissingTargetOrFieldErrors + this.totCallbackErrors + this.totStartupErrors + this.totUnsetErrors;
		}

		// Token: 0x020000B2 RID: 178
		internal enum SafeModeReportType
		{
			// Token: 0x04000219 RID: 537
			Unset,
			// Token: 0x0400021A RID: 538
			TargetOrFieldMissing,
			// Token: 0x0400021B RID: 539
			Callback,
			// Token: 0x0400021C RID: 540
			StartupFailure
		}
	}
}
