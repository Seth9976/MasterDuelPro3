using System;
using UnityEngine.Networking;

namespace UnityEngine.ResourceManagement
{
	// Token: 0x02000012 RID: 18
	public class WebRequestQueueOperation
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000040B0 File Offset: 0x000022B0
		public bool IsDone
		{
			get
			{
				return this.m_Completed || this.Result != null;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000040C5 File Offset: 0x000022C5
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000040CD File Offset: 0x000022CD
		public UnityWebRequest WebRequest
		{
			get
			{
				return this.m_WebRequest;
			}
			internal set
			{
				this.m_WebRequest = value;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000040D6 File Offset: 0x000022D6
		public WebRequestQueueOperation(UnityWebRequest request)
		{
			this.m_WebRequest = request;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000040E5 File Offset: 0x000022E5
		internal void Complete(UnityWebRequestAsyncOperation asyncOp)
		{
			this.m_Completed = true;
			this.Result = asyncOp;
			Action<UnityWebRequestAsyncOperation> onComplete = this.OnComplete;
			if (onComplete == null)
			{
				return;
			}
			onComplete(this.Result);
		}

		// Token: 0x0400004A RID: 74
		private bool m_Completed;

		// Token: 0x0400004B RID: 75
		public UnityWebRequestAsyncOperation Result;

		// Token: 0x0400004C RID: 76
		public Action<UnityWebRequestAsyncOperation> OnComplete;

		// Token: 0x0400004D RID: 77
		internal UnityWebRequest m_WebRequest;
	}
}
