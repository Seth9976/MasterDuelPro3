using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.Scripting;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x02000084 RID: 132
	[Preserve]
	internal class ProviderOperation<TObject> : AsyncOperationBase<TObject>, IGenericProviderOperation, ICachable
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000C230 File Offset: 0x0000A430
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000C238 File Offset: 0x0000A438
		IOperationCacheKey ICachable.Key { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000C241 File Offset: 0x0000A441
		public int ProvideHandleVersion
		{
			get
			{
				return this.m_ProvideHandleVersion;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000C249 File Offset: 0x0000A449
		public IResourceLocation Location
		{
			get
			{
				return this.m_Location;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000C251 File Offset: 0x0000A451
		public void SetDownloadProgressCallback(Func<DownloadStatus> callback)
		{
			this.m_GetDownloadProgressCallback = callback;
			if (this.m_GetDownloadProgressCallback != null)
			{
				this.m_DownloadStatus = this.m_GetDownloadProgressCallback();
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000C273 File Offset: 0x0000A473
		public void SetWaitForCompletionCallback(Func<bool> callback)
		{
			this.m_WaitForCompletionCallback = callback;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000C27C File Offset: 0x0000A47C
		protected override bool InvokeWaitForCompletion()
		{
			if (base.IsDone || this.m_ProviderCompletedCalled)
			{
				return true;
			}
			if (this.m_DepOp.IsValid() && !this.m_DepOp.IsDone)
			{
				this.m_DepOp.WaitForCompletion();
			}
			ResourceManager rm = this.m_RM;
			if (rm != null)
			{
				rm.Update(Time.unscaledDeltaTime);
			}
			if (!this.HasExecuted)
			{
				base.InvokeExecute();
			}
			return this.m_WaitForCompletionCallback != null && this.m_WaitForCompletionCallback();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000C2FC File Offset: 0x0000A4FC
		internal override DownloadStatus GetDownloadStatus(HashSet<object> visited)
		{
			DownloadStatus depDLS = (this.m_DepOp.IsValid() ? this.m_DepOp.InternalGetDownloadStatus(visited) : default(DownloadStatus));
			if (this.m_GetDownloadProgressCallback != null)
			{
				this.m_DownloadStatus = this.m_GetDownloadProgressCallback();
			}
			if (base.Status == AsyncOperationStatus.Succeeded)
			{
				this.m_DownloadStatus.DownloadedBytes = this.m_DownloadStatus.TotalBytes;
			}
			return new DownloadStatus
			{
				DownloadedBytes = this.m_DownloadStatus.DownloadedBytes + depDLS.DownloadedBytes,
				TotalBytes = this.m_DownloadStatus.TotalBytes + depDLS.TotalBytes,
				IsDone = base.IsDone
			};
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000C3BD File Offset: 0x0000A5BD
		public override void GetDependencies(List<AsyncOperationHandle> deps)
		{
			if (this.m_DepOp.IsValid())
			{
				deps.Add(this.m_DepOp);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000C3DD File Offset: 0x0000A5DD
		internal override void ReleaseDependencies()
		{
			if (this.m_DepOp.IsValid())
			{
				this.m_DepOp.Release();
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000C3F7 File Offset: 0x0000A5F7
		protected override string DebugName
		{
			get
			{
				return string.Format("Resource<{0}>({1})", typeof(TObject).Name, (this.m_Location == null) ? "Invalid" : AsyncOperationBase<TObject>.ShortenPath(this.m_Location.InternalId, true));
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000C434 File Offset: 0x0000A634
		public void GetDependencies(IList<object> dstList)
		{
			dstList.Clear();
			if (!this.m_DepOp.IsValid())
			{
				return;
			}
			if (this.m_DepOp.Result == null)
			{
				return;
			}
			for (int i = 0; i < this.m_DepOp.Result.Count; i++)
			{
				dstList.Add(this.m_DepOp.Result[i].Result);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000B578 File Offset: 0x00009778
		public Type RequestedType
		{
			get
			{
				return typeof(TObject);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000C49D File Offset: 0x0000A69D
		public int DependencyCount
		{
			get
			{
				if (this.m_DepOp.IsValid() && this.m_DepOp.Result != null)
				{
					return this.m_DepOp.Result.Count;
				}
				return 0;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		public TDepObject GetDependency<TDepObject>(int index)
		{
			if (!this.m_DepOp.IsValid() || this.m_DepOp.Result == null)
			{
				throw new Exception("Cannot get dependency because no dependencies were available");
			}
			return (TDepObject)((object)this.m_DepOp.Result[index].Result);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000C51C File Offset: 0x0000A71C
		public void SetProgressCallback(Func<float> callback)
		{
			this.m_GetProgressCallback = callback;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000C528 File Offset: 0x0000A728
		public void ProviderCompleted<T>(T result, bool status, Exception e)
		{
			this.m_ProvideHandleVersion++;
			this.m_GetProgressCallback = null;
			this.m_GetDownloadProgressCallback = null;
			this.m_WaitForCompletionCallback = null;
			this.m_NeedsRelease = status;
			this.m_ProviderCompletedCalled = true;
			ProviderOperation<T> top = this as ProviderOperation<T>;
			if (top != null)
			{
				top.Result = result;
			}
			else if (result == null && !typeof(TObject).IsValueType)
			{
				base.Result = (TObject)((object)null);
			}
			else
			{
				if (result == null || !typeof(TObject).IsAssignableFrom(result.GetType()))
				{
					string errorMsg = string.Format("Provider of type {0} with id {1} has provided a result of type {2} which cannot be converted to requested type {3}. The operation will be marked as failed.", new object[]
					{
						this.m_Provider.GetType().ToString(),
						this.m_Provider.ProviderId,
						typeof(T),
						typeof(TObject)
					});
					base.Complete(base.Result, false, errorMsg);
					throw new Exception(errorMsg);
				}
				base.Result = (TObject)((object)result);
			}
			base.Complete(base.Result, status, e, this.m_ReleaseDependenciesOnFailure);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000C654 File Offset: 0x0000A854
		protected override float Progress
		{
			get
			{
				float num;
				try
				{
					float numberOfOps = 1f;
					float total = 0f;
					if (this.m_GetProgressCallback != null)
					{
						total += this.m_GetProgressCallback();
					}
					if (!this.m_DepOp.IsValid() || this.m_DepOp.Result == null || this.m_DepOp.Result.Count == 0)
					{
						total += 1f;
						numberOfOps += 1f;
					}
					else
					{
						foreach (AsyncOperationHandle handle in this.m_DepOp.Result)
						{
							total += handle.PercentComplete;
							numberOfOps += 1f;
						}
					}
					num = Mathf.Min(total / numberOfOps, 0.99f);
				}
				catch
				{
					num = 0f;
				}
				return num;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000C73C File Offset: 0x0000A93C
		protected override void Execute()
		{
			if (this.m_DepOp.IsValid() && this.m_DepOp.Status == AsyncOperationStatus.Failed && (this.m_Provider.BehaviourFlags & ProviderBehaviourFlags.CanProvideWithFailedDependencies) == ProviderBehaviourFlags.None)
			{
				this.ProviderCompleted<TObject>(default(TObject), false, new Exception("Dependency Exception", this.m_DepOp.OperationException));
				return;
			}
			try
			{
				this.m_Provider.Provide(new ProvideHandle(this.m_ResourceManager, this));
			}
			catch (Exception e)
			{
				this.ProviderCompleted<TObject>(default(TObject), false, e);
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		public void Init(ResourceManager rm, IResourceProvider provider, IResourceLocation location, AsyncOperationHandle<IList<AsyncOperationHandle>> depOp)
		{
			this.m_DownloadStatus = default(DownloadStatus);
			this.m_ResourceManager = rm;
			this.m_DepOp = depOp;
			if (this.m_DepOp.IsValid())
			{
				this.m_DepOp.Acquire();
			}
			this.m_Provider = provider;
			this.m_Location = location;
			this.m_ReleaseDependenciesOnFailure = true;
			this.m_ProviderCompletedCalled = false;
			this.SetWaitForCompletionCallback(new Func<bool>(this.WaitForCompletionHandler));
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000C848 File Offset: 0x0000AA48
		public void Init(ResourceManager rm, IResourceProvider provider, IResourceLocation location, AsyncOperationHandle<IList<AsyncOperationHandle>> depOp, bool releaseDependenciesOnFailure)
		{
			this.m_DownloadStatus = default(DownloadStatus);
			this.m_ResourceManager = rm;
			this.m_DepOp = depOp;
			if (this.m_DepOp.IsValid())
			{
				this.m_DepOp.Acquire();
			}
			this.m_Provider = provider;
			this.m_Location = location;
			this.m_ReleaseDependenciesOnFailure = releaseDependenciesOnFailure;
			this.m_ProviderCompletedCalled = false;
			this.SetWaitForCompletionCallback(new Func<bool>(this.WaitForCompletionHandler));
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		private bool WaitForCompletionHandler()
		{
			if (base.IsDone)
			{
				return true;
			}
			if (!this.m_DepOp.IsDone)
			{
				this.m_DepOp.WaitForCompletion();
			}
			if (!this.HasExecuted)
			{
				base.InvokeExecute();
			}
			return base.IsDone;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		protected override void Destroy()
		{
			if (this.m_NeedsRelease)
			{
				this.m_Provider.Release(this.m_Location, base.Result);
			}
			if (this.m_DepOp.IsValid())
			{
				this.m_DepOp.Release();
			}
			base.Result = default(TObject);
			this.m_Location = null;
		}

		// Token: 0x04000179 RID: 377
		private bool m_ReleaseDependenciesOnFailure = true;

		// Token: 0x0400017A RID: 378
		private Func<float> m_GetProgressCallback;

		// Token: 0x0400017B RID: 379
		private Func<DownloadStatus> m_GetDownloadProgressCallback;

		// Token: 0x0400017C RID: 380
		private Func<bool> m_WaitForCompletionCallback;

		// Token: 0x0400017D RID: 381
		private bool m_ProviderCompletedCalled;

		// Token: 0x0400017E RID: 382
		private DownloadStatus m_DownloadStatus;

		// Token: 0x0400017F RID: 383
		private IResourceProvider m_Provider;

		// Token: 0x04000180 RID: 384
		internal AsyncOperationHandle<IList<AsyncOperationHandle>> m_DepOp;

		// Token: 0x04000181 RID: 385
		private IResourceLocation m_Location;

		// Token: 0x04000182 RID: 386
		private int m_ProvideHandleVersion;

		// Token: 0x04000183 RID: 387
		private bool m_NeedsRelease;

		// Token: 0x04000185 RID: 389
		private ResourceManager m_ResourceManager;

		// Token: 0x04000186 RID: 390
		private const float k_OperationWaitingToCompletePercentComplete = 0.99f;

		// Token: 0x04000187 RID: 391
		internal const string kInvalidHandleMsg = "The ProvideHandle is invalid. After the handle has been completed, it can no longer be used";
	}
}
