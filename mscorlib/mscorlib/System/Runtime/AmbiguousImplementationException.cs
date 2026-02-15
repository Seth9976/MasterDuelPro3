using System;
using System.Runtime.Serialization;

namespace System.Runtime
{
	// Token: 0x0200040B RID: 1035
	[Serializable]
	public sealed class AmbiguousImplementationException : Exception
	{
		// Token: 0x060022B2 RID: 8882 RVA: 0x0008F3E1 File Offset: 0x0008D5E1
		public AmbiguousImplementationException()
			: base("Ambiguous implementation found.")
		{
			base.HResult = -2146234262;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0008F3F9 File Offset: 0x0008D5F9
		public AmbiguousImplementationException(string message)
			: base(message)
		{
			base.HResult = -2146234262;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0001876A File Offset: 0x0001696A
		private AmbiguousImplementationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
