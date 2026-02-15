using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to unload an application domain fails.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017A RID: 378
	[Serializable]
	public class CannotUnloadAppDomainException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class.</summary>
		// Token: 0x06000DA4 RID: 3492 RVA: 0x00039F2C File Offset: 0x0003812C
		public CannotUnloadAppDomainException()
			: base("Attempt to unload the AppDomain failed.")
		{
			base.HResult = -2146234347;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06000DA5 RID: 3493 RVA: 0x00039F44 File Offset: 0x00038144
		public CannotUnloadAppDomainException(string message)
			: base(message)
		{
			base.HResult = -2146234347;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000DA6 RID: 3494 RVA: 0x00018324 File Offset: 0x00016524
		protected CannotUnloadAppDomainException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
