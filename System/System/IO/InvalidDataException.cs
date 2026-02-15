using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when a data stream is in an invalid format.</summary>
	// Token: 0x0200035C RID: 860
	[Serializable]
	public sealed class InvalidDataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class.</summary>
		// Token: 0x0600154C RID: 5452 RVA: 0x0005B3D2 File Offset: 0x000595D2
		public InvalidDataException()
			: base(global::Locale.GetText("Invalid data format."))
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600154D RID: 5453 RVA: 0x0005B3EF File Offset: 0x000595EF
		public InvalidDataException(string message)
			: base(message)
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x0600154E RID: 5454 RVA: 0x0005B403 File Offset: 0x00059603
		public InvalidDataException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00033B0D File Offset: 0x00031D0D
		private InvalidDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
