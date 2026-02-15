using System;
using System.Diagnostics;
using UnityEngine.Networking;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000041 RID: 65
	public class UnityWebRequestUtilities
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public static bool RequestHasErrors(UnityWebRequest webReq, out UnityWebRequestResult result)
		{
			result = null;
			if (webReq == null || !webReq.isDone)
			{
				return false;
			}
			UnityWebRequest.Result result2 = webReq.result;
			if (result2 <= UnityWebRequest.Result.Success)
			{
				return false;
			}
			if (result2 - UnityWebRequest.Result.ConnectionError > 2)
			{
				throw new NotImplementedException(string.Format("Cannot determine whether UnityWebRequest succeeded or not from result : {0}", webReq.result));
			}
			result = new UnityWebRequestResult(webReq);
			return true;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006D08 File Offset: 0x00004F08
		public static bool IsAssetBundleDownloaded(UnityWebRequestAsyncOperation op)
		{
			DownloadHandlerAssetBundle handler = (DownloadHandlerAssetBundle)op.webRequest.downloadHandler;
			if (handler != null && handler.autoLoadAssetBundle)
			{
				return handler.isDownloadComplete;
			}
			return op.isDone;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006D40 File Offset: 0x00004F40
		internal static void LogOperationResult(AsyncOperation op)
		{
			UnityWebRequestAsyncOperation webRequestOperation = op as UnityWebRequestAsyncOperation;
			if (webRequestOperation != null)
			{
				UnityWebRequestResult result = new UnityWebRequestResult(webRequestOperation.webRequest);
				if (result.Result == UnityWebRequest.Result.Success)
				{
					return;
				}
				UnityWebRequestUtilities.LogError(result.ToString());
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006D78 File Offset: 0x00004F78
		[Conditional("ADDRESSABLES_LOG_ALL")]
		internal static void Log(string msg)
		{
			Debug.Log(msg);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006D80 File Offset: 0x00004F80
		internal static void LogError(string msg)
		{
			Debug.LogError(msg);
		}

		// Token: 0x0400009D RID: 157
		private const string k_AddressablesLogConditional = "ADDRESSABLES_LOG_ALL";
	}
}
