using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when reading is attempted past the end of a stream.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000794 RID: 1940
	[Serializable]
	public class EndOfStreamException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with its message string set to a system-supplied message and its HRESULT set to COR_E_ENDOFSTREAM.</summary>
		// Token: 0x06003D4C RID: 15692 RVA: 0x000EC2A5 File Offset: 0x000EA4A5
		public EndOfStreamException()
			: base("Attempted to read past the end of the stream.")
		{
			base.HResult = -2147024858;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with its message string set to <paramref name="message" /> and its HRESULT set to COR_E_ENDOFSTREAM.</summary>
		/// <param name="message">A string that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06003D4D RID: 15693 RVA: 0x000EC2BD File Offset: 0x000EA4BD
		public EndOfStreamException(string message)
			: base(message)
		{
			base.HResult = -2147024858;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06003D4E RID: 15694 RVA: 0x000EC29B File Offset: 0x000EA49B
		protected EndOfStreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
