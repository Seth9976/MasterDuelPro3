using System;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	internal class TraceRecord
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00002ACF File Offset: 0x00000CCF
		internal virtual void WriteTo(XmlWriter writer)
		{
		}
	}
}
