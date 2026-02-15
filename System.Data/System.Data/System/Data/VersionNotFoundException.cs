using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to return a version of a <see cref="T:System.Data.DataRow" /> that has been deleted.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class VersionNotFoundException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x060000C1 RID: 193 RVA: 0x000044F9 File Offset: 0x000026F9
		protected VersionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class.</summary>
		// Token: 0x060000C2 RID: 194 RVA: 0x00004663 File Offset: 0x00002863
		public VersionNotFoundException()
			: base("Version not found.")
		{
			base.HResult = -2146232023;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x060000C3 RID: 195 RVA: 0x0000467B File Offset: 0x0000287B
		public VersionNotFoundException(string s)
			: base(s)
		{
			base.HResult = -2146232023;
		}
	}
}
