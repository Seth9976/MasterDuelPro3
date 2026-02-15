using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when the binary format of a custom attribute is invalid.</summary>
	// Token: 0x020005F5 RID: 1525
	[Serializable]
	public class CustomAttributeFormatException : FormatException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with the default properties.</summary>
		// Token: 0x06002CA7 RID: 11431 RVA: 0x000B189D File Offset: 0x000AFA9D
		public CustomAttributeFormatException()
			: this("Binary format of the specified custom attribute was invalid.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with the specified message.</summary>
		/// <param name="message">The message that indicates the reason this exception was thrown. </param>
		// Token: 0x06002CA8 RID: 11432 RVA: 0x000B18AA File Offset: 0x000AFAAA
		public CustomAttributeFormatException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002CA9 RID: 11433 RVA: 0x000B18B4 File Offset: 0x000AFAB4
		public CustomAttributeFormatException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232827;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The data for serializing or deserializing the custom attribute. </param>
		/// <param name="context">The source and destination for the custom attribute. </param>
		// Token: 0x06002CAA RID: 11434 RVA: 0x000B18C9 File Offset: 0x000AFAC9
		protected CustomAttributeFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
