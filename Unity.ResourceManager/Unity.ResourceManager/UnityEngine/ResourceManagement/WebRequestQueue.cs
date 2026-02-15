using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement
{
	// Token: 0x02000013 RID: 19
	public static class WebRequestQueue
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x0000410B File Offset: 0x0000230B
		public static void SetMaxConcurrentRequests(int maxRequests)
		{
			if (maxRequests < 1)
			{
				throw new ArgumentException("MaxRequests must be 1 or greater.", "maxRequests");
			}
			WebRequestQueue.s_MaxRequest = maxRequests;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004128 File Offset: 0x00002328
		public static WebRequestQueueOperation QueueRequest(UnityWebRequest request)
		{
			WebRequestQueueOperation queueOperation = new WebRequestQueueOperation(request);
			if (WebRequestQueue.s_ActiveRequests.Count < WebRequestQueue.s_MaxRequest)
			{
				WebRequestQueue.BeginWebRequest(queueOperation);
			}
			else
			{
				WebRequestQueue.s_QueuedOperations.Enqueue(queueOperation);
			}
			return queueOperation;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004164 File Offset: 0x00002364
		public static void WaitForRequestToBeActive(WebRequestQueueOperation request, int millisecondsTimeout)
		{
			List<UnityWebRequestAsyncOperation> completedRequests = new List<UnityWebRequestAsyncOperation>();
			while (WebRequestQueue.s_QueuedOperations.Contains(request))
			{
				completedRequests.Clear();
				foreach (UnityWebRequestAsyncOperation webRequestAsyncOp in WebRequestQueue.s_ActiveRequests)
				{
					if (UnityWebRequestUtilities.IsAssetBundleDownloaded(webRequestAsyncOp))
					{
						completedRequests.Add(webRequestAsyncOp);
					}
				}
				foreach (UnityWebRequestAsyncOperation webRequestAsyncOp2 in completedRequests)
				{
					bool flag = WebRequestQueue.s_QueuedOperations.Peek() == request;
					webRequestAsyncOp2.completed -= WebRequestQueue.OnWebAsyncOpComplete;
					WebRequestQueue.OnWebAsyncOpComplete(webRequestAsyncOp2);
					if (flag)
					{
						return;
					}
				}
				Thread.Sleep(millisecondsTimeout);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004244 File Offset: 0x00002444
		internal static void DequeueRequest(UnityWebRequestAsyncOperation operation)
		{
			operation.completed -= WebRequestQueue.OnWebAsyncOpComplete;
			WebRequestQueue.OnWebAsyncOpComplete(operation);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000425E File Offset: 0x0000245E
		private static void OnWebAsyncOpComplete(AsyncOperation operation)
		{
			WebRequestQueue.OnWebAsyncOpComplete(operation as UnityWebRequestAsyncOperation);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000426B File Offset: 0x0000246B
		private static void OnWebAsyncOpComplete(UnityWebRequestAsyncOperation operation)
		{
			if (WebRequestQueue.s_ActiveRequests.Remove(operation) && WebRequestQueue.s_QueuedOperations.Count > 0)
			{
				WebRequestQueue.BeginWebRequest(WebRequestQueue.s_QueuedOperations.Dequeue());
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004298 File Offset: 0x00002498
		private static void BeginWebRequest(WebRequestQueueOperation queueOperation)
		{
			UnityWebRequest request = queueOperation.m_WebRequest;
			UnityWebRequestAsyncOperation webRequestAsyncOp = null;
			try
			{
				webRequestAsyncOp = request.SendWebRequest();
				if (webRequestAsyncOp != null)
				{
					WebRequestQueue.s_ActiveRequests.Add(webRequestAsyncOp);
					if (webRequestAsyncOp.isDone)
					{
						WebRequestQueue.OnWebAsyncOpComplete(webRequestAsyncOp);
					}
					else
					{
						webRequestAsyncOp.completed += WebRequestQueue.OnWebAsyncOpComplete;
					}
				}
				else
				{
					WebRequestQueue.OnWebAsyncOpComplete(null);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
			}
			queueOperation.Complete(webRequestAsyncOp);
		}

		// Token: 0x0400004E RID: 78
		internal static int s_MaxRequest = 3;

		// Token: 0x0400004F RID: 79
		internal static Queue<WebRequestQueueOperation> s_QueuedOperations = new Queue<WebRequestQueueOperation>();

		// Token: 0x04000050 RID: 80
		internal static List<UnityWebRequestAsyncOperation> s_ActiveRequests = new List<UnityWebRequestAsyncOperation>();
	}
}
