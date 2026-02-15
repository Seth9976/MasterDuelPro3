using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when attempting an action that violates a constraint.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class ConstraintException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class using the specified serialization and stream context.</summary>
		/// <param name="info">The data necessary to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060000A9 RID: 169 RVA: 0x000044F9 File Offset: 0x000026F9
		protected ConstraintException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class. This is the default constructor.</summary>
		// Token: 0x060000AA RID: 170 RVA: 0x00004503 File Offset: 0x00002703
		public ConstraintException()
			: base("Constraint Exception.")
		{
			base.HResult = -2146232022;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060000AB RID: 171 RVA: 0x0000451B File Offset: 0x0000271B
		public ConstraintException(string s)
			: base(s)
		{
			base.HResult = -2146232022;
		}
	}
}
