using System;
using UnityEngine.Networking;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x02000085 RID: 133
	internal class UnityWebRequestOperation : AsyncOperationBase<UnityWebRequest>
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000C953 File Offset: 0x0000AB53
		public UnityWebRequestOperation(UnityWebRequest webRequest)
		{
			this.m_UWR = webRequest;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000C962 File Offset: 0x0000AB62
		protected override void Execute()
		{
			this.m_UWR.SendWebRequest().completed += delegate(AsyncOperation request)
			{
				base.Complete(this.m_UWR, string.IsNullOrEmpty(this.m_UWR.error), this.m_UWR.error);
			};
		}

		// Token: 0x04000188 RID: 392
		private UnityWebRequest m_UWR;
	}
}
