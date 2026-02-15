using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to access an unloaded application domain. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000179 RID: 377
	[Serializable]
	public class AppDomainUnloadedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class.</summary>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x00039EEB File Offset: 0x000380EB
		public AppDomainUnloadedException()
			: base("Attempted to access an unloaded AppDomain.")
		{
			base.HResult = -2146234348;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000DA1 RID: 3489 RVA: 0x00039F03 File Offset: 0x00038103
		public AppDomainUnloadedException(string message)
			: base(message)
		{
			base.HResult = -2146234348;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the error. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x00039F17 File Offset: 0x00038117
		public AppDomainUnloadedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146234348;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00018324 File Offset: 0x00016524
		protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000599 RID: 1433
		internal const int COR_E_APPDOMAINUNLOADED = -2146234348;
	}
}
