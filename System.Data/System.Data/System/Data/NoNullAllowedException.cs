using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to insert a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is set to false.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class NoNullAllowedException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060000B8 RID: 184 RVA: 0x000044F9 File Offset: 0x000026F9
		protected NoNullAllowedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class.</summary>
		// Token: 0x060000B9 RID: 185 RVA: 0x000045DF File Offset: 0x000027DF
		public NoNullAllowedException()
			: base("Null not allowed.")
		{
			base.HResult = -2146232026;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060000BA RID: 186 RVA: 0x000045F7 File Offset: 0x000027F7
		public NoNullAllowedException(string s)
			: base(s)
		{
			base.HResult = -2146232026;
		}
	}
}
