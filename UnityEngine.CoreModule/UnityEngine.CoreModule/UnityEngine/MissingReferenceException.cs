using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x020001B8 RID: 440
	[Serializable]
	public class MissingReferenceException : SystemException
	{
		// Token: 0x0600111C RID: 4380 RVA: 0x000249D3 File Offset: 0x00022BD3
		public MissingReferenceException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000249EE File Offset: 0x00022BEE
		public MissingReferenceException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00024A05 File Offset: 0x00022C05
		protected MissingReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
