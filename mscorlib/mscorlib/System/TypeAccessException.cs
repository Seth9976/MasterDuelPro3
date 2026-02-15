using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a method attempts to use a type that it does not have access to.</summary>
	// Token: 0x02000159 RID: 345
	[Serializable]
	public class TypeAccessException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TypeAccessException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000C4A RID: 3146 RVA: 0x000340CB File Offset: 0x000322CB
		public TypeAccessException()
			: base("Attempt to access the type failed.")
		{
			base.HResult = -2146233021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000C4B RID: 3147 RVA: 0x0001E2AE File Offset: 0x0001C4AE
		protected TypeAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
