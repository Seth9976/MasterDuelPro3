using System;
using System.Runtime.Serialization;

namespace System.Resources
{
	/// <summary>The exception that is thrown if the main assembly does not contain the resources for the neutral culture, and an appropriate satellite assembly is missing.</summary>
	// Token: 0x020005C2 RID: 1474
	[Serializable]
	public class MissingManifestResourceException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with default properties.</summary>
		// Token: 0x06002BB4 RID: 11188 RVA: 0x000AC796 File Offset: 0x000AA996
		public MissingManifestResourceException()
			: base("Unable to find manifest resource.")
		{
			base.HResult = -2146233038;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06002BB5 RID: 11189 RVA: 0x000AC7AE File Offset: 0x000AA9AE
		public MissingManifestResourceException(string message)
			: base(message)
		{
			base.HResult = -2146233038;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination of the exception. </param>
		// Token: 0x06002BB6 RID: 11190 RVA: 0x00018324 File Offset: 0x00016524
		protected MissingManifestResourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
