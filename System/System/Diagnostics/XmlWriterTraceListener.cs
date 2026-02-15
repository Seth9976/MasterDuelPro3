using System;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output as XML-encoded data to a <see cref="T:System.IO.TextWriter" /> or to a <see cref="T:System.IO.Stream" />, such as a <see cref="T:System.IO.FileStream" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200016D RID: 365
	public class XmlWriterTraceListener : TextWriterTraceListener
	{
		// Token: 0x04000687 RID: 1671
		internal bool shouldRespectFilterOnTraceTransfer;
	}
}
