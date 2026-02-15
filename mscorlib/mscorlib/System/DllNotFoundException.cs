using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a DLL specified in a DLL import cannot be found.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D9 RID: 217
	[Serializable]
	public class DllNotFoundException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with default properties.</summary>
		// Token: 0x0600074F RID: 1871 RVA: 0x0001E26D File Offset: 0x0001C46D
		public DllNotFoundException()
			: base("Dll was not found.")
		{
			base.HResult = -2146233052;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000750 RID: 1872 RVA: 0x0001E285 File Offset: 0x0001C485
		public DllNotFoundException(string message)
			: base(message)
		{
			base.HResult = -2146233052;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000751 RID: 1873 RVA: 0x0001E299 File Offset: 0x0001C499
		public DllNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233052;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DllNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06000752 RID: 1874 RVA: 0x0001E2AE File Offset: 0x0001C4AE
		protected DllNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
