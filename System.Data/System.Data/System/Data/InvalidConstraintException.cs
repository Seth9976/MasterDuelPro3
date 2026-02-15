using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when incorrectly trying to create or access a relation.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000013 RID: 19
	[Serializable]
	public class InvalidConstraintException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060000B5 RID: 181 RVA: 0x000044F9 File Offset: 0x000026F9
		protected InvalidConstraintException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class.</summary>
		// Token: 0x060000B6 RID: 182 RVA: 0x000045B3 File Offset: 0x000027B3
		public InvalidConstraintException()
			: base("Invalid constraint.")
		{
			base.HResult = -2146232028;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060000B7 RID: 183 RVA: 0x000045CB File Offset: 0x000027CB
		public InvalidConstraintException(string s)
			: base(s)
		{
			base.HResult = -2146232028;
		}
	}
}
