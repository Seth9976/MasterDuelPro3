using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to read or write protected memory.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000A4 RID: 164
	[Serializable]
	public class AccessViolationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000459 RID: 1113 RVA: 0x000182F8 File Offset: 0x000164F8
		public AccessViolationException()
			: base("Attempted to read or write protected memory. This is often an indication that other memory is corrupt.")
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x0600045A RID: 1114 RVA: 0x00018310 File Offset: 0x00016510
		public AccessViolationException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AccessViolationException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x0600045B RID: 1115 RVA: 0x00018324 File Offset: 0x00016524
		protected AccessViolationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
