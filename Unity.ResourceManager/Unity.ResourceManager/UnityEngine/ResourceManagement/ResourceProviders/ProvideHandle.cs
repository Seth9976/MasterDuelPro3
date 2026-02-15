using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000055 RID: 85
	public struct ProvideHandle
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00008E00 File Offset: 0x00007000
		internal ProvideHandle(ResourceManager rm, IGenericProviderOperation op)
		{
			this.m_ResourceManager = rm;
			this.m_InternalOp = op;
			this.m_Version = op.ProvideHandleVersion;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00008E1C File Offset: 0x0000701C
		internal bool IsValid
		{
			get
			{
				return this.m_InternalOp != null && this.m_InternalOp.ProvideHandleVersion == this.m_Version;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00008E3B File Offset: 0x0000703B
		internal IGenericProviderOperation InternalOp
		{
			get
			{
				if (this.m_InternalOp.ProvideHandleVersion != this.m_Version)
				{
					throw new Exception("The ProvideHandle is invalid. After the handle has been completed, it can no longer be used");
				}
				return this.m_InternalOp;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008E61 File Offset: 0x00007061
		public ResourceManager ResourceManager
		{
			get
			{
				return this.m_ResourceManager;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00008E69 File Offset: 0x00007069
		public Type Type
		{
			get
			{
				return this.InternalOp.RequestedType;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008E76 File Offset: 0x00007076
		public IResourceLocation Location
		{
			get
			{
				return this.InternalOp.Location;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008E83 File Offset: 0x00007083
		public int DependencyCount
		{
			get
			{
				return this.InternalOp.DependencyCount;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008E90 File Offset: 0x00007090
		public TDepObject GetDependency<TDepObject>(int index)
		{
			return this.InternalOp.GetDependency<TDepObject>(index);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008E9E File Offset: 0x0000709E
		public void GetDependencies(IList<object> list)
		{
			this.InternalOp.GetDependencies(list);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008EAC File Offset: 0x000070AC
		public void SetProgressCallback(Func<float> callback)
		{
			this.InternalOp.SetProgressCallback(callback);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008EBA File Offset: 0x000070BA
		public void SetDownloadProgressCallbacks(Func<DownloadStatus> callback)
		{
			this.InternalOp.SetDownloadProgressCallback(callback);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008EC8 File Offset: 0x000070C8
		public void SetWaitForCompletionCallback(Func<bool> callback)
		{
			this.InternalOp.SetWaitForCompletionCallback(callback);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008ED6 File Offset: 0x000070D6
		public void Complete<T>(T result, bool status, Exception exception)
		{
			this.InternalOp.ProviderCompleted<T>(result, status, exception);
		}

		// Token: 0x040000E7 RID: 231
		private int m_Version;

		// Token: 0x040000E8 RID: 232
		private IGenericProviderOperation m_InternalOp;

		// Token: 0x040000E9 RID: 233
		private ResourceManager m_ResourceManager;
	}
}
