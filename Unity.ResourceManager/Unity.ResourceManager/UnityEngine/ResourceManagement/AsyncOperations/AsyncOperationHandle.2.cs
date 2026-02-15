using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x0200007D RID: 125
	public struct AsyncOperationHandle : IEnumerator
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000B999 File Offset: 0x00009B99
		internal int Version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000B9A1 File Offset: 0x00009BA1
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000B9A9 File Offset: 0x00009BA9
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

		// Token: 0x06000331 RID: 817 RVA: 0x0000B9B2 File Offset: 0x00009BB2
		internal AsyncOperationHandle(IAsyncOperation op)
		{
			this.m_InternalOp = op;
			this.m_Version = ((op != null) ? op.Version : 0);
			this.m_LocationName = null;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B9D4 File Offset: 0x00009BD4
		internal AsyncOperationHandle(IAsyncOperation op, int version)
		{
			this.m_InternalOp = op;
			this.m_Version = version;
			this.m_LocationName = null;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000B9EB File Offset: 0x00009BEB
		internal AsyncOperationHandle(IAsyncOperation op, string locationName)
		{
			this.m_InternalOp = op;
			this.m_Version = ((op != null) ? op.Version : 0);
			this.m_LocationName = locationName;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000BA0D File Offset: 0x00009C0D
		internal AsyncOperationHandle(IAsyncOperation op, int version, string locationName)
		{
			this.m_InternalOp = op;
			this.m_Version = version;
			this.m_LocationName = locationName;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000BA24 File Offset: 0x00009C24
		internal AsyncOperationHandle Acquire()
		{
			this.InternalOp.IncrementReferenceCount();
			return this;
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000336 RID: 822 RVA: 0x0000BA37 File Offset: 0x00009C37
		// (remove) Token: 0x06000337 RID: 823 RVA: 0x0000BA45 File Offset: 0x00009C45
		public event Action<AsyncOperationHandle> Completed
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

		// Token: 0x06000338 RID: 824 RVA: 0x0000BA53 File Offset: 0x00009C53
		public void ReleaseHandleOnCompletion()
		{
			this.Completed += delegate(AsyncOperationHandle op)
			{
				op.Release();
			};
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000BA7A File Offset: 0x00009C7A
		public AsyncOperationHandle<T> Convert<T>()
		{
			return new AsyncOperationHandle<T>(this.InternalOp, this.m_Version, this.m_LocationName);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000BA93 File Offset: 0x00009C93
		public bool Equals(AsyncOperationHandle other)
		{
			return this.m_Version == other.m_Version && this.m_InternalOp == other.m_InternalOp;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000BAB3 File Offset: 0x00009CB3
		public string DebugName
		{
			get
			{
				if (!this.IsValid())
				{
					return "InvalidHandle";
				}
				return this.InternalOp.DebugName;
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600033C RID: 828 RVA: 0x0000BACE File Offset: 0x00009CCE
		// (remove) Token: 0x0600033D RID: 829 RVA: 0x0000BADC File Offset: 0x00009CDC
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

		// Token: 0x0600033E RID: 830 RVA: 0x0000BAEA File Offset: 0x00009CEA
		public void GetDependencies(List<AsyncOperationHandle> deps)
		{
			this.InternalOp.GetDependencies(deps);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		public override int GetHashCode()
		{
			if (this.m_InternalOp != null)
			{
				return this.m_InternalOp.GetHashCode() * 17 + this.m_Version;
			}
			return 0;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000BB19 File Offset: 0x00009D19
		private IAsyncOperation InternalOp
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000BB47 File Offset: 0x00009D47
		public bool IsDone
		{
			get
			{
				return !this.IsValid() || this.InternalOp.IsDone;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000BB5E File Offset: 0x00009D5E
		public bool IsValid()
		{
			return this.m_InternalOp != null && this.m_InternalOp.Version == this.m_Version;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000BB7D File Offset: 0x00009D7D
		public Exception OperationException
		{
			get
			{
				return this.InternalOp.OperationException;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000BB8A File Offset: 0x00009D8A
		public float PercentComplete
		{
			get
			{
				return this.InternalOp.PercentComplete;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000BB97 File Offset: 0x00009D97
		public DownloadStatus GetDownloadStatus()
		{
			return this.InternalGetDownloadStatus(new HashSet<object>());
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000BBA4 File Offset: 0x00009DA4
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000BBEC File Offset: 0x00009DEC
		internal int ReferenceCount
		{
			get
			{
				return this.InternalOp.ReferenceCount;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000BBF9 File Offset: 0x00009DF9
		public void Release()
		{
			this.InternalOp.DecrementReferenceCount();
			this.m_InternalOp = null;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000BC0D File Offset: 0x00009E0D
		public object Result
		{
			get
			{
				return this.InternalOp.GetResultAsObject();
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000BC1A File Offset: 0x00009E1A
		public AsyncOperationStatus Status
		{
			get
			{
				return this.InternalOp.Status;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000BC27 File Offset: 0x00009E27
		public Task<object> Task
		{
			get
			{
				return this.InternalOp.Task;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000BC34 File Offset: 0x00009E34
		object IEnumerator.Current
		{
			get
			{
				return this.Result;
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BC3C File Offset: 0x00009E3C
		bool IEnumerator.MoveNext()
		{
			return !this.IsDone;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00006444 File Offset: 0x00004644
		void IEnumerator.Reset()
		{
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BC47 File Offset: 0x00009E47
		public object WaitForCompletion()
		{
			if (this.IsValid() && !this.InternalOp.IsDone)
			{
				this.InternalOp.WaitForCompletion();
			}
			if (this.IsValid())
			{
				return this.Result;
			}
			return null;
		}

		// Token: 0x04000161 RID: 353
		internal IAsyncOperation m_InternalOp;

		// Token: 0x04000162 RID: 354
		private int m_Version;

		// Token: 0x04000163 RID: 355
		private string m_LocationName;
	}
}
