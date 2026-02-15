using System;

namespace System.Diagnostics
{
	/// <summary>Provides a simple listener that directs tracing or debugging output to an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018B RID: 395
	public sealed class EventLogTraceListener : TraceListener
	{
		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000964 RID: 2404 RVA: 0x00031860 File Offset: 0x0002FA60
		public override void Write(string message)
		{
			this.TraceData(new TraceEventCache(), this.event_log.Source, TraceEventType.Information, 0, message);
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000965 RID: 2405 RVA: 0x0003187B File Offset: 0x0002FA7B
		public override void WriteLine(string message)
		{
			this.Write(message);
		}

		// Token: 0x0400071B RID: 1819
		private EventLog event_log;
	}
}
