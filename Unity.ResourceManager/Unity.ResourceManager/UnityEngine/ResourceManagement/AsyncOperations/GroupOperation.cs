using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.Exceptions;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x02000081 RID: 129
	internal class GroupOperation : AsyncOperationBase<IList<AsyncOperationHandle>>, ICachable
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		public GroupOperation()
		{
			this.m_InternalOnComplete = new Action<AsyncOperationHandle>(this.OnOperationCompleted);
			base.Result = new List<AsyncOperationHandle>();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		protected override bool InvokeWaitForCompletion()
		{
			if (base.IsDone || base.Result == null)
			{
				return true;
			}
			foreach (AsyncOperationHandle r in base.Result)
			{
				r.WaitForCompletion();
				if (base.Result == null)
				{
					return true;
				}
			}
			ResourceManager rm = this.m_RM;
			if (rm != null)
			{
				rm.Update(Time.unscaledDeltaTime);
			}
			if (!base.IsDone && base.Result != null)
			{
				this.Execute();
			}
			ResourceManager rm2 = this.m_RM;
			if (rm2 != null)
			{
				rm2.Update(Time.unscaledDeltaTime);
			}
			return base.IsDone;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000BD9C File Offset: 0x00009F9C
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		IOperationCacheKey ICachable.Key { get; set; }

		// Token: 0x06000358 RID: 856 RVA: 0x0000BDAD File Offset: 0x00009FAD
		internal IList<AsyncOperationHandle> GetDependentOps()
		{
			return base.Result;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000BDB5 File Offset: 0x00009FB5
		public override void GetDependencies(List<AsyncOperationHandle> deps)
		{
			deps.AddRange(base.Result);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		internal override void ReleaseDependencies()
		{
			for (int i = 0; i < base.Result.Count; i++)
			{
				if (base.Result[i].IsValid())
				{
					base.Result[i].Release();
				}
			}
			base.Result.Clear();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000BE1C File Offset: 0x0000A01C
		internal override DownloadStatus GetDownloadStatus(HashSet<object> visited)
		{
			DownloadStatus status = new DownloadStatus
			{
				IsDone = base.IsDone
			};
			for (int i = 0; i < base.Result.Count; i++)
			{
				if (base.Result[i].IsValid())
				{
					DownloadStatus depStatus = base.Result[i].InternalGetDownloadStatus(visited);
					status.DownloadedBytes += depStatus.DownloadedBytes;
					status.TotalBytes += depStatus.TotalBytes;
				}
			}
			return status;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000BEA8 File Offset: 0x0000A0A8
		private bool DependenciesAreUnchanged(List<AsyncOperationHandle> deps)
		{
			if (this.m_CachedDependencyLocations.Count != deps.Count)
			{
				return false;
			}
			foreach (AsyncOperationHandle d in deps)
			{
				if (!this.m_CachedDependencyLocations.Contains(d.LocationName))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000BF20 File Offset: 0x0000A120
		protected override string DebugName
		{
			get
			{
				List<AsyncOperationHandle> deps = new List<AsyncOperationHandle>();
				this.GetDependencies(deps);
				if (deps.Count == 0)
				{
					return "Dependencies";
				}
				if (this.debugName != null && this.DependenciesAreUnchanged(deps))
				{
					return this.debugName;
				}
				this.m_CachedDependencyLocations.Clear();
				string toBeDisplayed = "Dependencies [";
				for (int i = 0; i < deps.Count; i++)
				{
					string locationString = deps[i].LocationName;
					this.m_CachedDependencyLocations.Add(locationString);
					if (locationString != null)
					{
						if (locationString.Length > 45)
						{
							locationString = AsyncOperationBase<object>.ShortenPath(locationString, true);
							locationString = locationString.Substring(0, Math.Min(45, locationString.Length)) + "...";
						}
						if (i == deps.Count - 1)
						{
							toBeDisplayed += locationString;
						}
						else
						{
							toBeDisplayed = toBeDisplayed + locationString + ", ";
						}
					}
				}
				toBeDisplayed += "]";
				if (toBeDisplayed.Length > 2000)
				{
					toBeDisplayed = toBeDisplayed.Substring(0, 2000) + "...";
				}
				this.debugName = toBeDisplayed;
				return this.debugName;
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000C044 File Offset: 0x0000A244
		protected override void Execute()
		{
			this.m_LoadedCount = 0;
			for (int i = 0; i < base.Result.Count; i++)
			{
				if (base.Result[i].IsDone)
				{
					this.m_LoadedCount++;
				}
				else
				{
					base.Result[i].Completed += this.m_InternalOnComplete;
				}
			}
			this.CompleteIfDependenciesComplete();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
		private void CompleteIfDependenciesComplete()
		{
			if (this.m_LoadedCount == base.Result.Count)
			{
				bool success = true;
				OperationException ex = null;
				if (!this.m_Settings.HasFlag(GroupOperation.GroupOperationSettings.AllowFailedDependencies))
				{
					for (int i = 0; i < base.Result.Count; i++)
					{
						if (base.Result[i].Status != AsyncOperationStatus.Succeeded)
						{
							success = false;
							ex = new OperationException("GroupOperation failed because one of its dependencies failed", base.Result[i].OperationException);
							break;
						}
					}
				}
				base.Complete(base.Result, success, ex, this.m_Settings.HasFlag(GroupOperation.GroupOperationSettings.ReleaseDependenciesOnFailure));
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000C167 File Offset: 0x0000A367
		protected override void Destroy()
		{
			this.ReleaseDependencies();
			this.debugName = null;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000C178 File Offset: 0x0000A378
		protected override float Progress
		{
			get
			{
				float total = 0f;
				for (int i = 0; i < base.Result.Count; i++)
				{
					AsyncOperationHandle handle = base.Result[i];
					if (!handle.IsDone)
					{
						total += handle.PercentComplete;
					}
					else
					{
						total += 1f;
					}
				}
				return total / (float)base.Result.Count;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C1D9 File Offset: 0x0000A3D9
		public void Init(List<AsyncOperationHandle> operations, bool releaseDependenciesOnFailure = true, bool allowFailedDependencies = false)
		{
			base.Result = new List<AsyncOperationHandle>(operations);
			this.m_Settings = (releaseDependenciesOnFailure ? GroupOperation.GroupOperationSettings.ReleaseDependenciesOnFailure : GroupOperation.GroupOperationSettings.None);
			if (allowFailedDependencies)
			{
				this.m_Settings |= GroupOperation.GroupOperationSettings.AllowFailedDependencies;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000C205 File Offset: 0x0000A405
		public void Init(List<AsyncOperationHandle> operations, GroupOperation.GroupOperationSettings settings)
		{
			base.Result = new List<AsyncOperationHandle>(operations);
			this.m_Settings = settings;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000C21A File Offset: 0x0000A41A
		private void OnOperationCompleted(AsyncOperationHandle op)
		{
			this.m_LoadedCount++;
			this.CompleteIfDependenciesComplete();
		}

		// Token: 0x0400016D RID: 365
		private Action<AsyncOperationHandle> m_InternalOnComplete;

		// Token: 0x0400016E RID: 366
		private int m_LoadedCount;

		// Token: 0x0400016F RID: 367
		private GroupOperation.GroupOperationSettings m_Settings;

		// Token: 0x04000170 RID: 368
		private string debugName;

		// Token: 0x04000171 RID: 369
		private const int k_MaxDisplayedLocationLength = 45;

		// Token: 0x04000172 RID: 370
		private const int k_MaxDebugNameLength = 2000;

		// Token: 0x04000174 RID: 372
		private HashSet<string> m_CachedDependencyLocations = new HashSet<string>();

		// Token: 0x02000082 RID: 130
		[Flags]
		public enum GroupOperationSettings
		{
			// Token: 0x04000176 RID: 374
			None = 0,
			// Token: 0x04000177 RID: 375
			ReleaseDependenciesOnFailure = 1,
			// Token: 0x04000178 RID: 376
			AllowFailedDependencies = 2
		}
	}
}
