using System;
using System.Runtime.Serialization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B7 RID: 439
	[RequiredByNativeCode]
	[Serializable]
	public class UnityException : SystemException
	{
		// Token: 0x06001119 RID: 4377 RVA: 0x000249D3 File Offset: 0x00022BD3
		public UnityException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000249EE File Offset: 0x00022BEE
		public UnityException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00024A05 File Offset: 0x00022C05
		protected UnityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
