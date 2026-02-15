using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	/// <summary>The exception that is thrown when the key specified for accessing an element in a collection does not match any key in the collection.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000752 RID: 1874
	[Serializable]
	public class KeyNotFoundException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class using default property values.</summary>
		// Token: 0x06003BA1 RID: 15265 RVA: 0x000E69D7 File Offset: 0x000E4BD7
		public KeyNotFoundException()
			: base("The given key was not present in the dictionary.")
		{
			base.HResult = -2146232969;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06003BA2 RID: 15266 RVA: 0x000E69EF File Offset: 0x000E4BEF
		public KeyNotFoundException(string message)
			: base(message)
		{
			base.HResult = -2146232969;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" />  that contains contextual information about the source or destination.</param>
		// Token: 0x06003BA3 RID: 15267 RVA: 0x00018324 File Offset: 0x00016524
		protected KeyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
