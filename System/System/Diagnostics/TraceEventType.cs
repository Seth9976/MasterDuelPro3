using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Identifies the type of event that has caused the trace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000162 RID: 354
	public enum TraceEventType
	{
		/// <summary>Fatal error or application crash.</summary>
		// Token: 0x0400064A RID: 1610
		Critical = 1,
		/// <summary>Recoverable error.</summary>
		// Token: 0x0400064B RID: 1611
		Error,
		/// <summary>Noncritical problem.</summary>
		// Token: 0x0400064C RID: 1612
		Warning = 4,
		/// <summary>Informational message.</summary>
		// Token: 0x0400064D RID: 1613
		Information = 8,
		/// <summary>Debugging trace.</summary>
		// Token: 0x0400064E RID: 1614
		Verbose = 16,
		/// <summary>Starting of a logical operation.</summary>
		// Token: 0x0400064F RID: 1615
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Start = 256,
		/// <summary>Stopping of a logical operation.</summary>
		// Token: 0x04000650 RID: 1616
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Stop = 512,
		/// <summary>Suspension of a logical operation.</summary>
		// Token: 0x04000651 RID: 1617
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Suspend = 1024,
		/// <summary>Resumption of a logical operation.</summary>
		// Token: 0x04000652 RID: 1618
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Resume = 2048,
		/// <summary>Changing of correlation identity.</summary>
		// Token: 0x04000653 RID: 1619
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Transfer = 4096
	}
}
