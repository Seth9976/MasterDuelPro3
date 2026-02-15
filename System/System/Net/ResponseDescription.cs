using System;
using System.Text;

namespace System.Net
{
	// Token: 0x0200038C RID: 908
	internal class ResponseDescription
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x00060432 File Offset: 0x0005E632
		internal bool PositiveIntermediate
		{
			get
			{
				return this.Status >= 100 && this.Status <= 199;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00060450 File Offset: 0x0005E650
		internal bool PositiveCompletion
		{
			get
			{
				return this.Status >= 200 && this.Status <= 299;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00060471 File Offset: 0x0005E671
		internal bool TransientFailure
		{
			get
			{
				return this.Status >= 400 && this.Status <= 499;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00060492 File Offset: 0x0005E692
		internal bool PermanentFailure
		{
			get
			{
				return this.Status >= 500 && this.Status <= 599;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x000604B3 File Offset: 0x0005E6B3
		internal bool InvalidStatusCode
		{
			get
			{
				return this.Status < 100 || this.Status > 599;
			}
		}

		// Token: 0x04000DC9 RID: 3529
		internal bool Multiline;

		// Token: 0x04000DCA RID: 3530
		internal int Status = -1;

		// Token: 0x04000DCB RID: 3531
		internal string StatusDescription;

		// Token: 0x04000DCC RID: 3532
		internal StringBuilder StatusBuffer = new StringBuilder();

		// Token: 0x04000DCD RID: 3533
		internal string StatusCodeString;
	}
}
