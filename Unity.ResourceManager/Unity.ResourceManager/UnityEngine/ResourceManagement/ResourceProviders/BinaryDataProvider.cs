using System;
using System.ComponentModel;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.Exceptions;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x0200004E RID: 78
	[DisplayName("Binary Data Provider")]
	internal class BinaryDataProvider : ResourceProviderBase
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000082A9 File Offset: 0x000064A9
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x000082B1 File Offset: 0x000064B1
		public bool IgnoreFailures { get; set; }

		// Token: 0x060001C8 RID: 456 RVA: 0x000082BA File Offset: 0x000064BA
		public virtual object Convert(Type type, byte[] data)
		{
			return data;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000082BD File Offset: 0x000064BD
		public override void Provide(ProvideHandle provideHandle)
		{
			new BinaryDataProvider.InternalOp().Start(provideHandle, this);
		}

		// Token: 0x0200004F RID: 79
		internal class InternalOp
		{
			// Token: 0x060001CB RID: 459 RVA: 0x000082CB File Offset: 0x000064CB
			private float GetPercentComplete()
			{
				if (this.m_RequestOperation == null)
				{
					return 0f;
				}
				return this.m_RequestOperation.progress;
			}

			// Token: 0x060001CC RID: 460 RVA: 0x000082E8 File Offset: 0x000064E8
			public void Start(ProvideHandle provideHandle, BinaryDataProvider rawProvider)
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
					if (path.EndsWith(".json"))
					{
						throw new Exception("Trying to read non binary data at path '" + path + "'.");
					}
					byte[] data = File.ReadAllBytes(path);
					object result = this.ConvertBytes(data);
					this.m_PI.Complete<object>(result, result != null, (result == null) ? new Exception(string.Format("Unable to load asset of type {0} from location {1}.", this.m_PI.Type, this.m_PI.Location)) : null);
					this.m_Complete = true;
					return;
				}
				else
				{
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
					return;
				}
			}

			// Token: 0x060001CD RID: 461 RVA: 0x00008484 File Offset: 0x00006684
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

			// Token: 0x060001CE RID: 462 RVA: 0x000084DC File Offset: 0x000066DC
			private void RequestOperation_completed(AsyncOperation op)
			{
				if (this.m_Complete)
				{
					return;
				}
				UnityWebRequestAsyncOperation webOp = op as UnityWebRequestAsyncOperation;
				byte[] binaryResult = null;
				Exception exception = null;
				if (webOp != null)
				{
					UnityWebRequest webReq = webOp.webRequest;
					UnityWebRequestResult uwrResult;
					if (!UnityWebRequestUtilities.RequestHasErrors(webReq, out uwrResult))
					{
						binaryResult = webReq.downloadHandler.data;
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
				this.CompleteOperation(binaryResult, exception);
			}

			// Token: 0x060001CF RID: 463 RVA: 0x0000856C File Offset: 0x0000676C
			protected void CompleteOperation(byte[] data, Exception exception)
			{
				object result = null;
				if (data != null && data.Length != 0)
				{
					result = this.ConvertBytes(data);
				}
				this.m_PI.Complete<object>(result, result != null || this.m_IgnoreFailures, exception);
				this.m_Complete = true;
			}

			// Token: 0x060001D0 RID: 464 RVA: 0x000085AC File Offset: 0x000067AC
			private object ConvertBytes(byte[] data)
			{
				object obj;
				try
				{
					obj = this.m_Provider.Convert(this.m_PI.Type, data);
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

			// Token: 0x060001D1 RID: 465 RVA: 0x000085F8 File Offset: 0x000067F8
			protected virtual void SendWebRequest(string path)
			{
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
				if (!this.m_RequestQueueOperation.IsDone)
				{
					WebRequestQueueOperation requestQueueOperation = this.m_RequestQueueOperation;
					requestQueueOperation.OnComplete = (Action<UnityWebRequestAsyncOperation>)Delegate.Combine(requestQueueOperation.OnComplete, new Action<UnityWebRequestAsyncOperation>(delegate(UnityWebRequestAsyncOperation asyncOperation)
					{
						this.m_RequestOperation = asyncOperation;
						this.m_RequestOperation.completed += this.RequestOperation_completed;
					}));
					return;
				}
				this.m_RequestOperation = this.m_RequestQueueOperation.Result;
				if (this.m_RequestOperation.isDone)
				{
					this.RequestOperation_completed(this.m_RequestOperation);
					return;
				}
				this.m_RequestOperation.completed += this.RequestOperation_completed;
			}

			// Token: 0x040000D2 RID: 210
			private BinaryDataProvider m_Provider;

			// Token: 0x040000D3 RID: 211
			private UnityWebRequestAsyncOperation m_RequestOperation;

			// Token: 0x040000D4 RID: 212
			private WebRequestQueueOperation m_RequestQueueOperation;

			// Token: 0x040000D5 RID: 213
			private ProvideHandle m_PI;

			// Token: 0x040000D6 RID: 214
			private bool m_IgnoreFailures;

			// Token: 0x040000D7 RID: 215
			private bool m_Complete;

			// Token: 0x040000D8 RID: 216
			private int m_Timeout;
		}
	}
}
