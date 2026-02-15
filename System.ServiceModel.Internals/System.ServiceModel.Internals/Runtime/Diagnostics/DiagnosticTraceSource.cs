using System;
using System.Diagnostics;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000032 RID: 50
	internal class DiagnosticTraceSource : TraceSource
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00004F06 File Offset: 0x00003106
		internal DiagnosticTraceSource(string name)
			: base(name)
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004F0F File Offset: 0x0000310F
		protected override string[] GetSupportedAttributes()
		{
			return new string[] { "propagateActivity" };
		}
	}
}
