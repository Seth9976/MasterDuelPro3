using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000020 RID: 32
	public readonly struct BindingResult
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000033D4 File Offset: 0x000015D4
		public BindingStatus status { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000033DC File Offset: 0x000015DC
		public string message { get; }

		// Token: 0x0600008E RID: 142 RVA: 0x000033E4 File Offset: 0x000015E4
		public BindingResult(BindingStatus status, string message = null)
		{
			this.status = status;
			this.message = message;
		}
	}
}
