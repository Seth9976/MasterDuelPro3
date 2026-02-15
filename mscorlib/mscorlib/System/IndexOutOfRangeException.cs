using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to access an element of an array with an index that is outside the bounds of the array. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200010C RID: 268
	[Serializable]
	public sealed class IndexOutOfRangeException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class.</summary>
		// Token: 0x060008A2 RID: 2210 RVA: 0x000277E2 File Offset: 0x000259E2
		public IndexOutOfRangeException()
			: base("Index was outside the bounds of the array.")
		{
			base.HResult = -2146233080;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060008A3 RID: 2211 RVA: 0x000277FA File Offset: 0x000259FA
		public IndexOutOfRangeException(string message)
			: base(message)
		{
			base.HResult = -2146233080;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00018324 File Offset: 0x00016524
		internal IndexOutOfRangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
