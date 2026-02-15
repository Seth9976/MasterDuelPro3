using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Identifies the level of an event.</summary>
	// Token: 0x020006E6 RID: 1766
	public enum EventLevel
	{
		/// <summary>No level filtering is done on the event.</summary>
		// Token: 0x04001E18 RID: 7704
		LogAlways,
		/// <summary>This level corresponds to a critical error, which is a serious error that has caused a major failure.</summary>
		// Token: 0x04001E19 RID: 7705
		Critical,
		/// <summary>This level adds standard errors that signify a problem.</summary>
		// Token: 0x04001E1A RID: 7706
		Error,
		/// <summary>This level adds warning events (for example, events that are published because a disk is nearing full capacity).</summary>
		// Token: 0x04001E1B RID: 7707
		Warning,
		/// <summary>This level adds informational events or messages that are not errors. These events can help trace the progress or state of an application.</summary>
		// Token: 0x04001E1C RID: 7708
		Informational,
		/// <summary>This level adds lengthy events or messages. It causes all events to be logged.</summary>
		// Token: 0x04001E1D RID: 7709
		Verbose
	}
}
