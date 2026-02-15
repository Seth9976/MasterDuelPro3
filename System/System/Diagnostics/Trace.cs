using System;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that help you trace the execution of your code. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000160 RID: 352
	public sealed class Trace
	{
		/// <summary>Gets the collection of listeners that is monitoring the trace output.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> that represents a collection of type <see cref="T:System.Diagnostics.TraceListener" /> monitoring the trace output.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0002C8F4 File Offset: 0x0002AAF4
		public static TraceListenerCollection Listeners
		{
			get
			{
				return TraceInternal.Listeners;
			}
		}

		/// <summary>Gets or sets whether <see cref="M:System.Diagnostics.Trace.Flush" /> should be called on the <see cref="P:System.Diagnostics.Trace.Listeners" /> after every write.</summary>
		/// <returns>true if <see cref="M:System.Diagnostics.Trace.Flush" /> is called on the <see cref="P:System.Diagnostics.Trace.Listeners" /> after every write; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0002C8FB File Offset: 0x0002AAFB
		public static bool AutoFlush
		{
			get
			{
				return TraceInternal.AutoFlush;
			}
		}

		/// <summary>Gets the correlation manager for the thread for this trace.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.CorrelationManager" /> object associated with the thread for this trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0002C902 File Offset: 0x0002AB02
		public static CorrelationManager CorrelationManager
		{
			get
			{
				if (Trace.correlationManager == null)
				{
					Trace.correlationManager = new CorrelationManager();
				}
				return Trace.correlationManager;
			}
		}

		// Token: 0x04000643 RID: 1603
		private static volatile CorrelationManager correlationManager;
	}
}
