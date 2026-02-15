using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000110 RID: 272
	[NullableContext(1)]
	public interface ITraceWriter
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060007C8 RID: 1992
		TraceLevel LevelFilter { get; }

		// Token: 0x060007C9 RID: 1993
		void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex);
	}
}
