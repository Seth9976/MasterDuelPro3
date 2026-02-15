using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.Win32;

namespace System.Security.Cryptography
{
	/// <summary>The exception that is thrown when an error occurs during a cryptographic operation.</summary>
	// Token: 0x02000385 RID: 901
	[ComVisible(true)]
	[Serializable]
	public class CryptographicException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with default properties.</summary>
		// Token: 0x06001F43 RID: 8003 RVA: 0x0007C1CF File Offset: 0x0007A3CF
		public CryptographicException()
			: base(Environment.GetResourceString("Error occurred during a cryptographic operation."))
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06001F44 RID: 8004 RVA: 0x0007C1EC File Offset: 0x0007A3EC
		public CryptographicException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with a specified error message in the specified format.</summary>
		/// <param name="format">The format used to output the error message. </param>
		/// <param name="insert">The error message that explains the reason for the exception. </param>
		// Token: 0x06001F45 RID: 8005 RVA: 0x0007C200 File Offset: 0x0007A400
		public CryptographicException(string format, string insert)
			: base(string.Format(CultureInfo.CurrentCulture, format, insert))
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001F46 RID: 8006 RVA: 0x0007C21F File Offset: 0x0007A41F
		public CryptographicException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with the specified HRESULT error code.</summary>
		/// <param name="hr">The HRESULT error code. </param>
		// Token: 0x06001F47 RID: 8007 RVA: 0x0007C234 File Offset: 0x0007A434
		public CryptographicException(int hr)
			: this(Win32Native.GetMessage(hr))
		{
			if (((long)hr & (long)((ulong)(-2147483648))) != (long)((ulong)(-2147483648)))
			{
				hr = (hr & 65535) | -2147024896;
			}
			base.SetErrorCode(hr);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001F48 RID: 8008 RVA: 0x00018324 File Offset: 0x00016524
		protected CryptographicException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
