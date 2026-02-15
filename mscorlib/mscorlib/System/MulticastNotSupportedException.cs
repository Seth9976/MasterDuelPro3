using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to combine two delegates based on the <see cref="T:System.Delegate" /> type instead of the <see cref="T:System.MulticastDelegate" /> type. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000123 RID: 291
	[Serializable]
	public sealed class MulticastNotSupportedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class.</summary>
		// Token: 0x060009AD RID: 2477 RVA: 0x000297E0 File Offset: 0x000279E0
		public MulticastNotSupportedException()
			: base("Attempted to add multiple callbacks to a delegate that does not support multicast.")
		{
			base.HResult = -2146233068;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060009AE RID: 2478 RVA: 0x000297F8 File Offset: 0x000279F8
		public MulticastNotSupportedException(string message)
			: base(message)
		{
			base.HResult = -2146233068;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00018324 File Offset: 0x00016524
		internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
