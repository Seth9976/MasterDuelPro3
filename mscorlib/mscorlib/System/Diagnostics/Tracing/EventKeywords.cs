using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Defines the standard keywords that apply to events.</summary>
	// Token: 0x020006E7 RID: 1767
	[Flags]
	public enum EventKeywords : long
	{
		/// <summary>No filtering on keywords is performed when the event is published.</summary>
		// Token: 0x04001E1F RID: 7711
		None = 0L,
		// Token: 0x04001E20 RID: 7712
		All = -1L,
		// Token: 0x04001E21 RID: 7713
		MicrosoftTelemetry = 562949953421312L,
		/// <summary>Attached to all Windows Diagnostics Infrastructure (WDI) context events.</summary>
		// Token: 0x04001E22 RID: 7714
		WdiContext = 562949953421312L,
		/// <summary>Attached to all Windows Diagnostics Infrastructure (WDI) diagnostic events.</summary>
		// Token: 0x04001E23 RID: 7715
		WdiDiagnostic = 1125899906842624L,
		/// <summary>Attached to all Service Quality Mechanism (SQM) events.</summary>
		// Token: 0x04001E24 RID: 7716
		Sqm = 2251799813685248L,
		/// <summary>Attached to all failed security audit events. Use this keyword only  for events in the security log.</summary>
		// Token: 0x04001E25 RID: 7717
		AuditFailure = 4503599627370496L,
		/// <summary>Attached to all successful security audit events. Use this keyword only for events in the security log.</summary>
		// Token: 0x04001E26 RID: 7718
		AuditSuccess = 9007199254740992L,
		/// <summary>Attached to transfer events where the related activity ID (correlation ID) is a computed value and is not guaranteed to be unique (that is, it is not a real GUID).</summary>
		// Token: 0x04001E27 RID: 7719
		CorrelationHint = 4503599627370496L,
		/// <summary>Attached to events that are raised by using the RaiseEvent function.</summary>
		// Token: 0x04001E28 RID: 7720
		EventLogClassic = 36028797018963968L
	}
}
