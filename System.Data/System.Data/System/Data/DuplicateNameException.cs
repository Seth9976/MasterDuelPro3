using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when a duplicate database object name is encountered during an add operation in a <see cref="T:System.Data.DataSet" /> -related object.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class DuplicateNameException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060000AF RID: 175 RVA: 0x000044F9 File Offset: 0x000026F9
		protected DuplicateNameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class.</summary>
		// Token: 0x060000B0 RID: 176 RVA: 0x0000455B File Offset: 0x0000275B
		public DuplicateNameException()
			: base("Duplicate name not allowed.")
		{
			base.HResult = -2146232030;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060000B1 RID: 177 RVA: 0x00004573 File Offset: 0x00002773
		public DuplicateNameException(string s)
			: base(s)
		{
			base.HResult = -2146232030;
		}
	}
}
