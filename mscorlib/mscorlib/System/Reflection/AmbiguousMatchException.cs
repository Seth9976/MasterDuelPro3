using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when binding to a member results in more than one member matching the binding criteria. This class cannot be inherited.</summary>
	// Token: 0x020005E0 RID: 1504
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with an empty message string and the root cause exception set to null.</summary>
		// Token: 0x06002C83 RID: 11395 RVA: 0x000B1727 File Offset: 0x000AF927
		public AmbiguousMatchException()
			: base("Ambiguous match found.")
		{
			base.HResult = -2147475171;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with its message string set to the given message and the root cause exception set to null.</summary>
		/// <param name="message">A string indicating the reason this exception was thrown. </param>
		// Token: 0x06002C84 RID: 11396 RVA: 0x000B173F File Offset: 0x000AF93F
		public AmbiguousMatchException(string message)
			: base(message)
		{
			base.HResult = -2147475171;
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x00018324 File Offset: 0x00016524
		internal AmbiguousMatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
