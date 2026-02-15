using System;

namespace System.Diagnostics
{
	// Token: 0x02000151 RID: 337
	internal class FilterElement : TypedElement
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x0002B5E8 File Offset: 0x000297E8
		public FilterElement()
			: base(typeof(TraceFilter))
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002B5FA File Offset: 0x000297FA
		public TraceFilter GetRuntimeObject()
		{
			TraceFilter traceFilter = (TraceFilter)base.BaseGetRuntimeObject();
			traceFilter.initializeData = base.InitData;
			return traceFilter;
		}
	}
}
