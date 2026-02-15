using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the event type of an event log entry.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000186 RID: 390
	public enum EventLogEntryType
	{
		/// <summary>An error event. This indicates a significant problem the user should know about; usually a loss of functionality or data.</summary>
		// Token: 0x0400070E RID: 1806
		Error = 1,
		/// <summary>A warning event. This indicates a problem that is not immediately significant, but that may signify conditions that could cause future problems.</summary>
		// Token: 0x0400070F RID: 1807
		Warning,
		/// <summary>An information event. This indicates a significant, successful operation.</summary>
		// Token: 0x04000710 RID: 1808
		Information = 4,
		/// <summary>A success audit event. This indicates a security event that occurs when an audited access attempt is successful; for example, logging on successfully.</summary>
		// Token: 0x04000711 RID: 1809
		SuccessAudit = 8,
		/// <summary>A failure audit event. This indicates a security event that occurs when an audited access attempt fails; for example, a failed attempt to open a file.</summary>
		// Token: 0x04000712 RID: 1810
		FailureAudit = 16
	}
}
