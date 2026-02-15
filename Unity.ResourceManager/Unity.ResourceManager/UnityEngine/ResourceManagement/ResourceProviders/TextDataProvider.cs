using System;
using System.ComponentModel;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.Exceptions;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000065 RID: 101
	[DisplayName("Text Data Provider")]
	public class TextDataProvider : ResourceProviderBase
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000987F File Offset: 0x00007A7F
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00009887 File Offset: 0x00007A87
		public bool IgnoreFailures { get; set; }

		// Token: 0x06000244 RID: 580 RVA: 0x000082BA File Offset: 0x000064BA
		public virtual object Convert(Type type, string text)
		{
			return text;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009890 File Offset: 0x00007A90
		public override void Provide(ProvideHandle provideHandle)
		{
			new TextDataProvider.InternalOp().Start(provideHandle, this);
		}

		// Token: 0x02000066 RID: 102
		internal class InternalOp
		{
			// Token: 0x06000247 RID: 583 RVA: 0x0000989E File Offset: 0x00007A9E
			private float GetPercentComplete()
			{
				if (this.m_RequestOperation == null)
				{
					return 0f;
				}
				return this.m_RequestOperation.progress;
			}

			// Token: 0x06000248 RID: 584 RVA: 0x000098BC File Offset: 0x00007ABC
			public void Start(ProvideHandle provideHandle, TextDataProvider rawProvider)
			{
				this.m_PI = provideHandle;
				this.m_PI.SetWaitForCompletionCallback(new Func<bool>(this.WaitForCompletionHandler));
				provideHandle.SetProgressCallback(new Func<float>(this.GetPercentComplete));
				this.m_Provider = rawProvider;
				ProviderLoadRequestOptions providerData = this.m_PI.Location.Data as ProviderLoadRequestOptions;
				if (providerData != null)
				{
					this.m_IgnoreFailures = providerData.IgnoreFailures;
					this.m_Timeout = providerData.WebRequestTimeout;
				}
				else
				{
					this.m_IgnoreFailures = rawProvider.IgnoreFailures;
					this.m_Timeout = 0;
				}
				string path = this.m_PI.ResourceManager.TransformInternalId(this.m_PI.Location);
				if (ResourceManagerConfig.ShouldPathUseWebRequest(path))
				{
					this.SendWebRequest(path);
					return;
				}
				if (File.Exists(path))
				{
					if (path.Length >= 260)
					{
						path = "\\\\?\\" + path;
					}
					string text = File.ReadAllText(path);
					object result = this.ConvertText(text);
					this.m_PI.Complete<object>(result, result != null, (result == null) ? new Exception(string.Format("Unable to load asset of type {0} from location {1}.", this.m_PI.Type, this.m_PI.Location)) : null);
					this.m_Complete = true;
					return;
				}
				Exception exception = null;
				if (this.m_IgnoreFailures)
				{
					this.m_PI.Complete<object>(null, true, exception);
					this.m_Complete = true;
					return;
				}
				exception = new Exception(string.Format("Invalid path in TextDataProvider : '{0}'.", path));
				this.m_PI.Complete<object>(null, false, exception);
				this.m_Complete = true;
			}

			// Token: 0x06000249 RID: 585 RVA: 0x00009A30 File Offset: 0x00007C30
			private bool WaitForCompletionHandler()
			{
				if (this.m_Complete)
				{
					return true;
				}
				if (this.m_RequestOperation != null)
				{
					if (this.m_RequestOperation.isDone && !this.m_Complete)
					{
						this.RequestOperation_completed(this.m_RequestOperation);
					}
					else if (!this.m_RequestOperation.isDone)
					{
						return false;
					}
				}
				return this.m_Complete;
			}

			// Token: 0x0600024A RID: 586 RVA: 0x00009A88 File Offset: 0x00007C88
			private void RequestOperation_completed(AsyncOperation op)
			{
				if (this.m_Complete)
				{
					return;
				}
				UnityWebRequestAsyncOperation webOp = op as UnityWebRequestAsyncOperation;
				string textResult = null;
				Exception exception = null;
				if (webOp != null)
				{
					UnityWebRequest webReq = webOp.webRequest;
					UnityWebRequestResult uwrResult;
					if (!UnityWebRequestUtilities.RequestHasErrors(webReq, out uwrResult))
					{
						textResult = webReq.downloadHandler.text;
					}
					else
					{
						exception = new RemoteProviderException("TextDataProvider : unable to load from url : " + webReq.url, this.m_PI.Location, uwrResult, null);
					}
					webReq.Dispose();
				}
				else
				{
					exception = new RemoteProviderException("TextDataProvider unable to load from unknown url", this.m_PI.Location, null, null);
				}
				this.CompleteOperation(textResult, exception);
			}

			// Token: 0x0600024B RID: 587 RVA: 0x00009B18 File Offset: 0x00007D18
			protected void CompleteOperation(string text, Exception exception)
			{
				object result = null;
				if (!string.IsNullOrEmpty(text))
				{
					result = this.ConvertText(text);
				}
				this.m_PI.Complete<object>(result, result != null || this.m_IgnoreFailures, exception);
				this.m_Complete = true;
			}

			// Token: 0x0600024C RID: 588 RVA: 0x00009B58 File Offset: 0x00007D58
			private object ConvertText(string text)
			{
				object obj;
				try
				{
					obj = this.m_Provider.Convert(this.m_PI.Type, text);
				}
				catch (Exception e)
				{
					if (!this.m_IgnoreFailures)
					{
						Debug.LogException(e);
					}
					obj = null;
				}
				return obj;
			}

			// Token: 0x0600024D RID: 589 RVA: 0x00009BA4 File Offset: 0x00007DA4
			protected virtual void SendWebRequest(string path)
			{
				try
				{
					path = path.Replace('\\', '/');
					UnityWebRequest request = new UnityWebRequest(path, "GET", new DownloadHandlerBuffer(), null);
					if (this.m_Timeout > 0)
					{
						request.timeout = this.m_Timeout;
					}
					Action<UnityWebRequest> webRequestOverride = this.m_PI.ResourceManager.WebRequestOverride;
					if (webRequestOverride != null)
					{
						webRequestOverride(request);
					}
					this.m_RequestQueueOperation = WebRequestQueue.QueueRequest(request);
					if (this.m_RequestQueueOperation.IsDone)
					{
						this.m_RequestOperation = this.m_RequestQueueOperation.Result;
						if (this.m_RequestOperation.isDone)
						{
							this.RequestOperation_completed(this.m_RequestOperation);
						}
						else
						{
							this.m_RequestOperation.completed += this.RequestOperation_completed;
						}
					}
					else
					{
						WebRequestQueueOperation requestQueueOperation = this.m_RequestQueueOperation;
						requestQueueOperation.OnComplete = (Action<UnityWebRequestAsyncOperation>)Delegate.Combine(requestQueueOperation.OnComplete, new Action<UnityWebRequestAsyncOperation>(delegate(UnityWebRequestAsyncOperation asyncOperation)
						{
							this.m_RequestOperation = asyncOperation;
							this.m_RequestOperation.completed += this.RequestOperation_completed;
						}));
					}
				}
				catch (UriFormatException e)
				{
					throw new UriFormatException("Invalid path '" + path + "'", e);
				}
			}

			// Token: 0x04000106 RID: 262
			private TextDataProvider m_Provider;

			// Token: 0x04000107 RID: 263
			private UnityWebRequestAsyncOperation m_RequestOperation;

			// Token: 0x04000108 RID: 264
			private WebRequestQueueOperation m_RequestQueueOperation;

			// Token: 0x04000109 RID: 265
			private ProvideHandle m_PI;

			// Token: 0x0400010A RID: 266
			private bool m_IgnoreFailures;

			// Token: 0x0400010B RID: 267
			private bool m_Complete;

			// Token: 0x0400010C RID: 268
			private int m_Timeout;
		}
	}
}
