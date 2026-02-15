using System;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.Exceptions
{
	// Token: 0x02000018 RID: 24
	public class RemoteProviderException : ProviderException
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004435 File Offset: 0x00002635
		public RemoteProviderException(string message, IResourceLocation location = null, UnityWebRequestResult uwrResult = null, Exception innerException = null)
			: base(message, location, innerException)
		{
			this.WebRequestResult = uwrResult;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004448 File Offset: 0x00002648
		public override string Message
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004450 File Offset: 0x00002650
		public UnityWebRequestResult WebRequestResult { get; }

		// Token: 0x060000BD RID: 189 RVA: 0x00004458 File Offset: 0x00002658
		public override string ToString()
		{
			if (this.WebRequestResult != null)
			{
				return string.Format("{0} : {1}\nUnityWebRequest result : {2}\n{3}", new object[]
				{
					base.GetType().Name,
					base.Message,
					this.WebRequestResult,
					base.InnerException
				});
			}
			return base.ToString();
		}
	}
}
