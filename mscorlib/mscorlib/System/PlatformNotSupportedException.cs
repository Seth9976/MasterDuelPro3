using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a feature does not run on a particular platform.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000135 RID: 309
	[Serializable]
	public class PlatformNotSupportedException : NotSupportedException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with default properties.</summary>
		// Token: 0x06000A5D RID: 2653 RVA: 0x0002F23A File Offset: 0x0002D43A
		public PlatformNotSupportedException()
			: base("Operation is not supported on this platform.")
		{
			base.HResult = -2146233031;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with a specified error message.</summary>
		/// <param name="message">The text message that explains the reason for the exception. </param>
		// Token: 0x06000A5E RID: 2654 RVA: 0x0002F252 File Offset: 0x0002D452
		public PlatformNotSupportedException(string message)
			: base(message)
		{
			base.HResult = -2146233031;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.PlatformNotSupportedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06000A5F RID: 2655 RVA: 0x0002F266 File Offset: 0x0002D466
		protected PlatformNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
