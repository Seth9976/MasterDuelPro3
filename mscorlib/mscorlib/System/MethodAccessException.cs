using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an invalid attempt to access a method, such as accessing a private method from partially trusted code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000120 RID: 288
	[Serializable]
	public class MethodAccessException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "Attempt to access the method failed." This message takes into account the current system culture.</summary>
		// Token: 0x060009A5 RID: 2469 RVA: 0x00029705 File Offset: 0x00027905
		public MethodAccessException()
			: base("Attempt to access the method failed.")
		{
			base.HResult = -2146233072;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x060009A6 RID: 2470 RVA: 0x0002971D File Offset: 0x0002791D
		public MethodAccessException(string message)
			: base(message)
		{
			base.HResult = -2146233072;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060009A7 RID: 2471 RVA: 0x0001E77A File Offset: 0x0001C97A
		protected MethodAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
