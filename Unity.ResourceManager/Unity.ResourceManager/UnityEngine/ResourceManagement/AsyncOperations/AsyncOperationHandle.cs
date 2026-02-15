using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x0200007B RID: 123
	public struct AsyncOperationHandle<TObject> : IEnumerator, IEquatable<AsyncOperationHandle<TObject>>
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000B615 File Offset: 0x00009815
		internal int Version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000B61D File Offset: 0x0000981D
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000B625 File Offset: 0x00009825
		internal string LocationName
		{
			get
			{
				return this.m_LocationName;
			}
			set
			{
				this.m_LocationName = value;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000B62E File Offset: 0x0000982E
		public static implicit operator AsyncOperationHandle(AsyncOperationHandle<TObject> obj)
		{
			return new AsyncOperationHandle(obj.m_InternalOp, obj.m_Version, obj.m_LocationName);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000B647 File Offset: 0x00009847
		internal AsyncOperationHandle(AsyncOperationBase<TObject> op)
		{
			this.m_InternalOp = op;
			this.m_Version = ((op != null) ? op.Version : 0);
			this.m_LocationName = null;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B669 File Offset: 0x00009869
		public DownloadStatus GetDownloadStatus()
		{
			return this.InternalGetDownloadStatus(new HashSet<object>());
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B678 File Offset: 0x00009878
		internal DownloadStatus InternalGetDownloadStatus(HashSet<object> visited)
		{
			if (visited == null)
			{
				visited = new HashSet<object>();
			}
			if (!visited.Add(this.InternalOp))
			{
				return new DownloadStatus
				{
					IsDone = this.IsDone
				};
			}
			return this.InternalOp.GetDownloadStatus(visited);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B6C0 File Offset: 0x000098C0
		internal AsyncOperationHandle(IAsyncOperation op)
		{
			this.m_InternalOp = (AsyncOperationBase<TObject>)op;
			this.m_Version = ((op != null) ? op.Version : 0);
			this.m_LocationName = null;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B6E7 File Offset: 0x000098E7
		internal AsyncOperationHandle(IAsyncOperation op, int version)
		{
			this.m_InternalOp = (AsyncOperationBase<TObject>)op;
			this.m_Version = version;
			this.m_LocationName = null;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B703 File Offset: 0x00009903
		internal AsyncOperationHandle(IAsyncOperation op, string locationName)
		{
			this.m_InternalOp = (AsyncOperationBase<TObject>)op;
			this.m_Version = ((op != null) ? op.Version : 0);
			this.m_LocationName = locationName;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B72A File Offset: 0x0000992A
		internal AsyncOperationHandle(IAsyncOperation op, int version, string locationName)
		{
			this.m_InternalOp = (AsyncOperationBase<TObject>)op;
			this.m_Version = version;
			this.m_LocationName = locationName;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B746 File Offset: 0x00009946
		internal AsyncOperationHandle<TObject> Acquire()
		{
			this.InternalOp.IncrementReferenceCount();
			return this;
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000312 RID: 786 RVA: 0x0000B759 File Offset: 0x00009959
		// (remove) Token: 0x06000313 RID: 787 RVA: 0x0000B767 File Offset: 0x00009967
		public event Action<AsyncOperationHandle<TObject>> Completed
		{
			add
			{
				this.InternalOp.Completed += value;
			}
			remove
			{
				this.InternalOp.Completed -= value;
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B775 File Offset: 0x00009975
		public void ReleaseHandleOnCompletion()
		{
			this.Completed += delegate(AsyncOperationHandle<TObject> op)
			{
				op.Release();
			};
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000315 RID: 789 RVA: 0x0000B79C File Offset: 0x0000999C
		// (remove) Token: 0x06000316 RID: 790 RVA: 0x0000B7AA File Offset: 0x000099AA
		public event Action<AsyncOperationHandle> CompletedTypeless
		{
			add
			{
				this.InternalOp.CompletedTypeless += value;
			}
			remove
			{
				this.InternalOp.CompletedTypeless -= value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000B7B8 File Offset: 0x000099B8
		public string DebugName
		{
			get
			{
				if (!this.IsValid())
				{
					return "InvalidHandle";
				}
				return ((IAsyncOperation)this.InternalOp).DebugName;
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000B7D3 File Offset: 0x000099D3
		public void GetDependencies(List<AsyncOperationHandle> deps)
		{
			this.InternalOp.GetDependencies(deps);
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000319 RID: 793 RVA: 0x0000B7E1 File Offset: 0x000099E1
		// (remove) Token: 0x0600031A RID: 794 RVA: 0x0000B7EF File Offset: 0x000099EF
		public event Action<AsyncOperationHandle> Destroyed
		{
			add
			{
				this.InternalOp.Destroyed += value;
			}
			remove
			{
				this.InternalOp.Destroyed -= value;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000B7FD File Offset: 0x000099FD
		public bool Equals(AsyncOperationHandle<TObject> other)
		{
			return this.m_Version == other.m_Version && this.m_InternalOp == other.m_InternalOp;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000B81D File Offset: 0x00009A1D
		public override int GetHashCode()
		{
			if (this.m_InternalOp != null)
			{
				return this.m_InternalOp.GetHashCode() * 17 + this.m_Version;
			}
			return 0;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000B840 File Offset: 0x00009A40
		public TObject WaitForCompletion()
		{
			if (this.IsValid() && !this.InternalOp.IsDone)
			{
				this.InternalOp.WaitForCompletion();
			}
			AsyncOperationBase<TObject> internalOp = this.m_InternalOp;
			if (internalOp != null)
			{
				ResourceManager rm = internalOp.m_RM;
				if (rm != null)
				{
					rm.Update(Time.unscaledDeltaTime);
				}
			}
			if (this.IsValid())
			{
				return this.Result;
			}
			return default(TObject);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000B8A6 File Offset: 0x00009AA6
		internal AsyncOperationBase<TObject> InternalOp
		{
			get
			{
				if (this.m_InternalOp == null || this.m_InternalOp.Version != this.m_Version)
				{
					throw new Exception("Attempting to use an invalid operation handle");
				}
				return this.m_InternalOp;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000B8D4 File Offset: 0x00009AD4
		public bool IsDone
		{
			get
			{
				return !this.IsValid() || this.InternalOp.IsDone;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B8EB File Offset: 0x00009AEB
		public bool IsValid()
		{
			return this.m_InternalOp != null && this.m_InternalOp.Version == this.m_Version;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000B90A File Offset: 0x00009B0A
		public Exception OperationException
		{
			get
			{
				return this.InternalOp.OperationException;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B917 File Offset: 0x00009B17
		public float PercentComplete
		{
			get
			{
				return this.InternalOp.PercentComplete;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000B924 File Offset: 0x00009B24
		internal int ReferenceCount
		{
			get
			{
				return this.InternalOp.ReferenceCount;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000B931 File Offset: 0x00009B31
		public void Release()
		{
			this.InternalOp.DecrementReferenceCount();
			this.m_InternalOp = null;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000B945 File Offset: 0x00009B45
		public TObject Result
		{
			get
			{
				return this.InternalOp.Result;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000B952 File Offset: 0x00009B52
		public AsyncOperationStatus Status
		{
			get
			{
				return this.InternalOp.Status;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000B95F File Offset: 0x00009B5F
		public Task<TObject> Task
		{
			get
			{
				return this.InternalOp.Task;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B96C File Offset: 0x00009B6C
		object IEnumerator.Current
		{
			get
			{
				return this.Result;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B979 File Offset: 0x00009B79
		bool IEnumerator.MoveNext()
		{
			return !this.IsDone;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00006444 File Offset: 0x00004644
		void IEnumerator.Reset()
		{
		}

		// Token: 0x0400015C RID: 348
		internal AsyncOperationBase<TObject> m_InternalOp;

		// Token: 0x0400015D RID: 349
		private int m_Version;

		// Token: 0x0400015E RID: 350
		private string m_LocationName;
	}
}
