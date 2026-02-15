using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception that is thrown when an unrecognized HRESULT is returned from a COM method call.</summary>
	// Token: 0x0200051A RID: 1306
	[Serializable]
	public class COMException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with default values.</summary>
		// Token: 0x060028FB RID: 10491 RVA: 0x000A7D7E File Offset: 0x000A5F7E
		public COMException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x060028FC RID: 10492 RVA: 0x000A7D86 File Offset: 0x000A5F86
		public COMException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with a specified message and error code.</summary>
		/// <param name="message">The message that indicates the reason the exception occurred. </param>
		/// <param name="errorCode">The error code (HRESULT) value associated with this exception. </param>
		// Token: 0x060028FD RID: 10493 RVA: 0x000A7D8F File Offset: 0x000A5F8F
		public COMException(string message, int errorCode)
			: base(message)
		{
			base.HResult = errorCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class from serialization data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that supplies the contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x060028FE RID: 10494 RVA: 0x000A7D9F File Offset: 0x000A5F9F
		protected COMException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Converts the contents of the exception to a string.</summary>
		/// <returns>A string containing the <see cref="P:System.Exception.HResult" />, <see cref="P:System.Exception.Message" />, <see cref="P:System.Exception.InnerException" />, and <see cref="P:System.Exception.StackTrace" /> properties of the exception.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060028FF RID: 10495 RVA: 0x000A7DAC File Offset: 0x000A5FAC
		public override string ToString()
		{
			string message = this.Message;
			string text = base.GetType().ToString() + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (message != null && message.Length > 0)
			{
				text = text + ": " + message;
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				text = text + " ---> " + innerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			return text;
		}
	}
}
