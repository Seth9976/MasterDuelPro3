using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an array with the wrong number of dimensions is passed to a method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000139 RID: 313
	[Serializable]
	public class RankException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class.</summary>
		// Token: 0x06000A72 RID: 2674 RVA: 0x0002F65C File Offset: 0x0002D85C
		public RankException()
			: base("Attempted to operate on an array with the incorrect number of dimensions.")
		{
			base.HResult = -2146233065;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06000A73 RID: 2675 RVA: 0x0002F674 File Offset: 0x0002D874
		public RankException(string message)
			: base(message)
		{
			base.HResult = -2146233065;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000A74 RID: 2676 RVA: 0x00018324 File Offset: 0x00016524
		protected RankException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
