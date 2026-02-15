using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>The exception that is thrown when something has gone wrong during remoting.</summary>
	// Token: 0x02000420 RID: 1056
	[ComVisible(true)]
	[Serializable]
	public class RemotingException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingException" /> class with default properties.</summary>
		// Token: 0x0600232C RID: 9004 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		public RemotingException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingException" /> class with a specified message.</summary>
		/// <param name="message">The error message that explains why the exception occurred. </param>
		// Token: 0x0600232D RID: 9005 RVA: 0x000536B7 File Offset: 0x000518B7
		public RemotingException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination of the exception. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		// Token: 0x0600232E RID: 9006 RVA: 0x00018324 File Offset: 0x00016524
		protected RemotingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains why the exception occurred. </param>
		/// <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="InnerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600232F RID: 9007 RVA: 0x000536C0 File Offset: 0x000518C0
		public RemotingException(string message, Exception InnerException)
			: base(message, InnerException)
		{
		}
	}
}
