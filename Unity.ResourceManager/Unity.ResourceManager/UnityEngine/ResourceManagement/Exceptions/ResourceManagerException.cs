using System;
using System.Runtime.Serialization;

namespace UnityEngine.ResourceManagement.Exceptions
{
	// Token: 0x02000014 RID: 20
	public class ResourceManagerException : Exception
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00004330 File Offset: 0x00002530
		public ResourceManagerException()
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004338 File Offset: 0x00002538
		public ResourceManagerException(string message)
			: base(message)
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004341 File Offset: 0x00002541
		public ResourceManagerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000434B File Offset: 0x0000254B
		protected ResourceManagerException(SerializationInfo message, StreamingContext context)
			: base(message, context)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004355 File Offset: 0x00002555
		public override string ToString()
		{
			return string.Format("{0} : {1}\n{2}", base.GetType().Name, base.Message, base.InnerException);
		}
	}
}
