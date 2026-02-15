using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000EB RID: 235
	[Conditional("UNITY_EDITOR")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = false)]
	public class CurrentPipelineHelpURLAttribute : HelpURLAttribute
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001280C File Offset: 0x00010A0C
		private string pageName { get; }

		// Token: 0x060007A9 RID: 1961 RVA: 0x00012814 File Offset: 0x00010A14
		public CurrentPipelineHelpURLAttribute(string pageName)
			: base(null)
		{
			this.pageName = pageName;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public override string URL
		{
			get
			{
				return string.Empty;
			}
		}
	}
}
