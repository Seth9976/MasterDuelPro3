using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an object appears more than once in an array of synchronization objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DB RID: 219
	[Serializable]
	public class DuplicateWaitObjectException : ArgumentException
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0001E69F File Offset: 0x0001C89F
		private static string DuplicateWaitObjectMessage
		{
			get
			{
				if (DuplicateWaitObjectException.s_duplicateWaitObjectMessage == null)
				{
					DuplicateWaitObjectException.s_duplicateWaitObjectMessage = "Duplicate objects in argument.";
				}
				return DuplicateWaitObjectException.s_duplicateWaitObjectMessage;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class.</summary>
		// Token: 0x0600077A RID: 1914 RVA: 0x0001E6BD File Offset: 0x0001C8BD
		public DuplicateWaitObjectException()
			: base(DuplicateWaitObjectException.DuplicateWaitObjectMessage)
		{
			base.HResult = -2146233047;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600077B RID: 1915 RVA: 0x000188B2 File Offset: 0x00016AB2
		protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000334 RID: 820
		private static volatile string s_duplicateWaitObjectMessage;
	}
}
