using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to change the value of a read-only column.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class ReadOnlyException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060000BB RID: 187 RVA: 0x000044F9 File Offset: 0x000026F9
		protected ReadOnlyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class.</summary>
		// Token: 0x060000BC RID: 188 RVA: 0x0000460B File Offset: 0x0000280B
		public ReadOnlyException()
			: base("Column is marked read only.")
		{
			base.HResult = -2146232025;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060000BD RID: 189 RVA: 0x00004623 File Offset: 0x00002823
		public ReadOnlyException(string s)
			: base(s)
		{
			base.HResult = -2146232025;
		}
	}
}
