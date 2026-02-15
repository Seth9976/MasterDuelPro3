using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Exceptions;

namespace UnityEngine.ResourceManagement
{
	// Token: 0x02000009 RID: 9
	internal class ChainOperation<TObject, TObjectDependency> : AsyncOperationBase<TObject>
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002604 File Offset: 0x00000804
		public ChainOperation()
		{
			this.m_CachedOnWrappedCompleted = new Action<AsyncOperationHandle<TObject>>(this.OnWrappedCompleted);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002628 File Offset: 0x00000828
		protected override string DebugName
		{
			get
			{
				return string.Concat(new string[]
				{
					"ChainOperation<",
					typeof(TObject).Name,
					",",
					typeof(TObjectDependency).Name,
					"> - ",
					this.m_DepOp.DebugName
				});
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000268A File Offset: 0x0000088A
		public override void GetDependencies(List<AsyncOperationHandle> deps)
		{
			if (this.m_DepOp.IsValid())
			{
				deps.Add(this.m_DepOp);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026AA File Offset: 0x000008AA
		public void Init(AsyncOperationHandle<TObjectDependency> dependentOp, Func<AsyncOperationHandle<TObjectDependency>, AsyncOperationHandle<TObject>> callback, bool releaseDependenciesOnFailure)
		{
			this.m_DepOp = dependentOp;
			this.m_DepOp.Acquire();
			this.m_Callback = callback;
			this.m_ReleaseDependenciesOnFailure = releaseDependenciesOnFailure;
			this.RefreshDownloadStatus(null);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026D4 File Offset: 0x000008D4
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
			this.m_WrappedOp.WaitForCompletion();
			return this.m_WrappedOp.IsDone;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002758 File Offset: 0x00000958
		protected override void Execute()
		{
			this.m_WrappedOp = this.m_Callback(this.m_DepOp);
			this.m_WrappedOp.Completed += this.m_CachedOnWrappedCompleted;
			this.m_Callback = null;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000278C File Offset: 0x0000098C
		private void OnWrappedCompleted(AsyncOperationHandle<TObject> x)
		{
			OperationException ex = null;
			if (x.Status == AsyncOperationStatus.Failed)
			{
				ex = new OperationException("ChainOperation failed because dependent operation failed", x.OperationException);
			}
			base.Complete(this.m_WrappedOp.Result, x.Status == AsyncOperationStatus.Succeeded, ex, this.m_ReleaseDependenciesOnFailure);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027D9 File Offset: 0x000009D9
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

		// Token: 0x06000031 RID: 49 RVA: 0x0000280B File Offset: 0x00000A0B
		internal override void ReleaseDependencies()
		{
			if (this.m_DepOp.IsValid())
			{
				this.m_DepOp.Release();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002828 File Offset: 0x00000A28
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

		// Token: 0x06000033 RID: 51 RVA: 0x00002890 File Offset: 0x00000A90
		private void RefreshDownloadStatus(HashSet<object> visited = null)
		{
			this.m_depStatus = (this.m_DepOp.IsValid() ? this.m_DepOp.InternalGetDownloadStatus(visited) : this.m_depStatus);
			this.m_wrapStatus = (this.m_WrappedOp.IsValid() ? this.m_WrappedOp.InternalGetDownloadStatus(visited) : this.m_wrapStatus);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000028EC File Offset: 0x00000AEC
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

		// Token: 0x0400000F RID: 15
		private AsyncOperationHandle<TObjectDependency> m_DepOp;

		// Token: 0x04000010 RID: 16
		private AsyncOperationHandle<TObject> m_WrappedOp;

		// Token: 0x04000011 RID: 17
		private DownloadStatus m_depStatus;

		// Token: 0x04000012 RID: 18
		private DownloadStatus m_wrapStatus;

		// Token: 0x04000013 RID: 19
		private Func<AsyncOperationHandle<TObjectDependency>, AsyncOperationHandle<TObject>> m_Callback;

		// Token: 0x04000014 RID: 20
		private Action<AsyncOperationHandle<TObject>> m_CachedOnWrappedCompleted;

		// Token: 0x04000015 RID: 21
		private bool m_ReleaseDependenciesOnFailure = true;
	}
}
