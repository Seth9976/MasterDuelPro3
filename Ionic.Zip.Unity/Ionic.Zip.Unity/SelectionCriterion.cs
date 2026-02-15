using System;
using System.Diagnostics;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000019 RID: 25
	internal abstract class SelectionCriterion
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002496 File Offset: 0x00000696
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000249E File Offset: 0x0000069E
		internal virtual bool Verbose { get; set; }

		// Token: 0x0600005C RID: 92
		internal abstract bool Evaluate(string filename);

		// Token: 0x0600005D RID: 93 RVA: 0x000024A7 File Offset: 0x000006A7
		[Conditional("SelectorTrace")]
		protected static void CriterionTrace(string format, params object[] args)
		{
		}

		// Token: 0x0600005E RID: 94
		internal abstract bool Evaluate(ZipEntry entry);
	}
}
