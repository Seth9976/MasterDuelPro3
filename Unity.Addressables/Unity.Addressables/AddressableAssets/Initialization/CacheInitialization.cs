using System;
using System.IO;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.AddressableAssets.Initialization
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	public class CacheInitialization : IInitializableObject
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00009A2C File Offset: 0x00007C2C
		public bool Initialize(string id, string dataStr)
		{
			CacheInitializationData data = JsonUtility.FromJson<CacheInitializationData>(dataStr);
			if (data != null)
			{
				Caching.compressionEnabled = data.CompressionEnabled;
				Cache activeCache = Caching.currentCacheForWriting;
				if (!string.IsNullOrEmpty(data.CacheDirectoryOverride))
				{
					string dir = Addressables.ResolveInternalId(data.CacheDirectoryOverride);
					if (!Directory.Exists(dir))
					{
						Directory.CreateDirectory(dir);
					}
					activeCache = Caching.GetCacheByPath(dir);
					if (!activeCache.valid)
					{
						activeCache = Caching.AddCache(dir);
					}
					Caching.currentCacheForWriting = activeCache;
				}
				if (data.LimitCacheSize)
				{
					activeCache.maximumAvailableStorageSpace = data.MaximumCacheSize;
				}
				else
				{
					activeCache.maximumAvailableStorageSpace = long.MaxValue;
				}
				activeCache.expirationDelay = 12960000;
			}
			return true;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009AD0 File Offset: 0x00007CD0
		public virtual AsyncOperationHandle<bool> InitializeAsync(ResourceManager rm, string id, string data)
		{
			CacheInitialization.CacheInitOp op = new CacheInitialization.CacheInitOp();
			op.Init(() => this.Initialize(id, data));
			return rm.StartOperation<bool>(op, default(AsyncOperationHandle));
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00009B20 File Offset: 0x00007D20
		public static string RootPath
		{
			get
			{
				return Path.GetDirectoryName(Caching.defaultCache.path);
			}
		}

		// Token: 0x0200005E RID: 94
		private class CacheInitOp : AsyncOperationBase<bool>, IUpdateReceiver
		{
			// Token: 0x06000250 RID: 592 RVA: 0x00009B3F File Offset: 0x00007D3F
			public void Init(Func<bool> callback)
			{
				this.m_Callback = callback;
			}

			// Token: 0x06000251 RID: 593 RVA: 0x00009B48 File Offset: 0x00007D48
			protected override bool InvokeWaitForCompletion()
			{
				ResourceManager rm = this.m_RM;
				if (rm != null)
				{
					rm.Update(Time.unscaledDeltaTime);
				}
				if (!base.IsDone)
				{
					base.InvokeExecute();
				}
				return base.IsDone;
			}

			// Token: 0x06000252 RID: 594 RVA: 0x00009B74 File Offset: 0x00007D74
			public void Update(float unscaledDeltaTime)
			{
				if (Caching.ready && this.m_UpdateRequired)
				{
					this.m_UpdateRequired = false;
					if (this.m_Callback != null)
					{
						base.Complete(this.m_Callback(), true, "");
						return;
					}
					base.Complete(true, true, "");
				}
			}

			// Token: 0x06000253 RID: 595 RVA: 0x00009BC4 File Offset: 0x00007DC4
			protected override void Execute()
			{
				((IUpdateReceiver)this).Update(0f);
			}

			// Token: 0x0400014D RID: 333
			private Func<bool> m_Callback;

			// Token: 0x0400014E RID: 334
			private bool m_UpdateRequired = true;
		}
	}
}
