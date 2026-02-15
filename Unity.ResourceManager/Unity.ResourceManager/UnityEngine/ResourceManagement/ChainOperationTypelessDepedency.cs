using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Exceptions;

namespace UnityEngine.ResourceManagement
{
	// Token: 0x0200000A RID: 10
	internal class ChainOperationTypelessDepedency<TObject> : AsyncOperationBase<TObject>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002971 File Offset: 0x00000B71
		internal AsyncOperationHandle<TObject> WrappedOp
		{
			get
			{
				return this.m_WrappedOp;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002979 File Offset: 0x00000B79
		public ChainOperationTypelessDepedency()
		{
			this.m_CachedOnWrappedCompleted = new Action<AsyncOperationHandle<TObject>>(this.OnWrappedCompleted);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000299A File Offset: 0x00000B9A
		protected override string DebugName
		{
			get
			{
				return "ChainOperation<" + typeof(TObject).Name + "> - " + this.m_DepOp.DebugName;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override void GetDependencies(List<AsyncOperationHandle> deps)
		{
			if (this.m_DepOp.IsValid())
			{
				deps.Add(this.m_DepOp);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029E0 File Offset: 0x00000BE0
		public void Init(AsyncOperationHandle dependentOp, Func<AsyncOperationHandle, AsyncOperationHandle<TObject>> callback, bool releaseDependenciesOnFailure)
		{
			this.m_DepOp = dependentOp;
			this.m_DepOp.Acquire();
			this.m_Callback = callback;
			this.m_ReleaseDependenciesOnFailure = releaseDependenciesOnFailure;
			this.RefreshDownloadStatus(null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A0C File Offset: 0x00000C0C
		protected override bool InvokeWaitForCompletion()
		{
			if (base.IsDone)
			{
				return true;
			}
			if (!this.m_DepOp.IsDone)
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
			if (!this.m_WrappedOp.IsValid())
			{
				return this.m_WrappedOp.IsDone;
			}
			base.Result = this.m_WrappedOp.WaitForCompletion();
			return true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A8B File Offset: 0x00000C8B
		protected override void Execute()
		{
			this.m_WrappedOp = this.m_Callback(this.m_DepOp);
			this.m_WrappedOp.Completed += this.m_CachedOnWrappedCompleted;
			this.m_Callback = null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002ABC File Offset: 0x00000CBC
		private void OnWrappedCompleted(AsyncOperationHandle<TObject> x)
		{
			OperationException ex = null;
			if (x.Status == AsyncOperationStatus.Failed)
			{
				ex = new OperationException("ChainOperation failed because dependent operation failed", x.OperationException);
			}
			base.Complete(this.m_WrappedOp.Result, x.Status == AsyncOperationStatus.Succeeded, ex, this.m_ReleaseDependenciesOnFailure);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B09 File Offset: 0x00000D09
		protected override void Destroy()
		{
			if (this.m_WrappedOp.IsValid())
			{
				this.m_WrappedOp.Release();
			}
			if (this.m_DepOp.IsValid())
			{
				this.m_DepOp.Release();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B3B File Offset: 0x00000D3B
		internal override void ReleaseDependencies()
		{
			if (this.m_DepOp.IsValid())
			{
				this.m_DepOp.Release();
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B58 File Offset: 0x00000D58
		internal override DownloadStatus GetDownloadStatus(HashSet<object> visited)
		{
			this.RefreshDownloadStatus(visited);
			return new DownloadStatus
			{
				DownloadedBytes = this.m_depStatus.DownloadedBytes + this.m_wrapStatus.DownloadedBytes,
				TotalBytes = this.m_depStatus.TotalBytes + this.m_wrapStatus.TotalBytes,
				IsDone = base.IsDone
			};
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BC0 File Offset: 0x00000DC0
		private void RefreshDownloadStatus(HashSet<object> visited = null)
		{
			this.m_depStatus = (this.m_DepOp.IsValid() ? this.m_DepOp.InternalGetDownloadStatus(visited) : this.m_depStatus);
			this.m_wrapStatus = (this.m_WrappedOp.IsValid() ? this.m_WrappedOp.InternalGetDownloadStatus(visited) : this.m_wrapStatus);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002C1C File Offset: 0x00000E1C
		protected override float Progress
		{
			get
			{
				DownloadStatus downloadStatus = this.GetDownloadStatus(new HashSet<object>());
				if (!downloadStatus.IsDone && downloadStatus.DownloadedBytes == 0L)
				{
					return 0f;
				}
				float total = 0f;
				int numberOfOps = 2;
				if (this.m_DepOp.IsValid())
				{
					total += this.m_DepOp.PercentComplete;
				}
				else
				{
					total += 1f;
				}
				if (this.m_WrappedOp.IsValid())
				{
					total += this.m_WrappedOp.PercentComplete;
				}
				else
				{
					total += 1f;
				}
				return total / (float)numberOfOps;
			}
		}

		// Token: 0x04000016 RID: 22
		private AsyncOperationHandle m_DepOp;

		// Token: 0x04000017 RID: 23
		private AsyncOperationHandle<TObject> m_WrappedOp;

		// Token: 0x04000018 RID: 24
		private DownloadStatus m_depStatus;

		// Token: 0x04000019 RID: 25
		private DownloadStatus m_wrapStatus;

		// Token: 0x0400001A RID: 26
		private Func<AsyncOperationHandle, AsyncOperationHandle<TObject>> m_Callback;

		// Token: 0x0400001B RID: 27
		private Action<AsyncOperationHandle<TObject>> m_CachedOnWrappedCompleted;

		// Token: 0x0400001C RID: 28
		private bool m_ReleaseDependenciesOnFailure = true;
	}
}
