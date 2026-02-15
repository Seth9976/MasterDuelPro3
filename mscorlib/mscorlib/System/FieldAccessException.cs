using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an invalid attempt to access a private or protected field inside a class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E1 RID: 225
	[Serializable]
	public class FieldAccessException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class.</summary>
		// Token: 0x06000789 RID: 1929 RVA: 0x0001E74E File Offset: 0x0001C94E
		public FieldAccessException()
			: base("Attempted to access a field that is not accessible by the caller.")
		{
			base.HResult = -2146233081;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600078A RID: 1930 RVA: 0x0001E766 File Offset: 0x0001C966
		public FieldAccessException(string message)
			: base(message)
		{
			base.HResult = -2146233081;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600078B RID: 1931 RVA: 0x0001E77A File Offset: 0x0001C97A
		protected FieldAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
