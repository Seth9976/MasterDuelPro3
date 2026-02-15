using System;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that help debug your code. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200014D RID: 333
	public static class Debug
	{
		/// <summary>Checks for a condition; if the condition is false, outputs a specified message and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is true, the specified message is not sent and the message box is not displayed.  </param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060007BD RID: 1981 RVA: 0x0002B3BA File Offset: 0x000295BA
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message)
		{
			TraceInternal.Assert(condition, message);
		}
	}
}
