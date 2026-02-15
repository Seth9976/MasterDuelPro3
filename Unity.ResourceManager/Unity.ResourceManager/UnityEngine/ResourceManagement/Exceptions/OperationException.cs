using System;

namespace UnityEngine.ResourceManagement.Exceptions
{
	// Token: 0x02000016 RID: 22
	public class OperationException : Exception
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004341 File Offset: 0x00002541
		public OperationException(string message, Exception innerException = null)
			: base(message, innerException)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004355 File Offset: 0x00002555
		public override string ToString()
		{
			return string.Format("{0} : {1}\n{2}", base.GetType().Name, base.Message, base.InnerException);
		}
	}
}
