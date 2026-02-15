using System;
using System.Collections.Generic;
using Unity.Jobs;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000533 RID: 1331
	internal class MeshGenerationDeferrer : IDisposable
	{
		// Token: 0x060024C1 RID: 9409 RVA: 0x0008BFD2 File Offset: 0x0008A1D2
		public void AddMeshGenerationJob(JobHandle jobHandle)
		{
			this.m_Dependencies.Enqueue(jobHandle);
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x0008BFE4 File Offset: 0x0008A1E4
		public void AddMeshGenerationCallback(MeshGenerationCallback callback, object userData, MeshGenerationCallbackType callbackType, bool isJobDependent)
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentNullException("callback");
			}
			MeshGenerationDeferrer.CallbackInfo callbackInfo = new MeshGenerationDeferrer.CallbackInfo
			{
				callback = callback,
				userData = userData
			};
			bool flag2 = !isJobDependent;
			if (flag2)
			{
				switch (callbackType)
				{
				case MeshGenerationCallbackType.Fork:
					this.m_Fork.Enqueue(callbackInfo);
					break;
				case MeshGenerationCallbackType.WorkThenFork:
					this.m_WorkThenFork.Enqueue(callbackInfo);
					break;
				case MeshGenerationCallbackType.Work:
					this.m_Work.Enqueue(callbackInfo);
					break;
				default:
					throw new NotImplementedException();
				}
			}
			else
			{
				switch (callbackType)
				{
				case MeshGenerationCallbackType.Fork:
					this.m_JobDependentFork.Enqueue(callbackInfo);
					break;
				case MeshGenerationCallbackType.WorkThenFork:
					this.m_JobDependentWorkThenFork.Enqueue(callbackInfo);
					break;
				case MeshGenerationCallbackType.Work:
					this.m_JobDependentWork.Enqueue(callbackInfo);
					break;
				default:
					throw new NotImplementedException();
				}
			}
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x0008C0CC File Offset: 0x0008A2CC
		public void ProcessDeferredWork(MeshGenerationContext meshGenerationContext)
		{
			for (;;)
			{
				int forkCount = this.m_Fork.Count;
				int workThenForkCount = this.m_WorkThenFork.Count;
				int workCount = this.m_Work.Count;
				int jobDependentForkCount = this.m_JobDependentFork.Count;
				int jobDependentWorkThenForkCount = this.m_JobDependentWorkThenFork.Count;
				int jobDependentWorkCount = this.m_JobDependentWork.Count;
				int depCount = this.m_Dependencies.Count;
				bool flag = forkCount + workThenForkCount + workCount + depCount == 0;
				if (flag)
				{
					break;
				}
				for (int i = 0; i < forkCount; i++)
				{
					MeshGenerationDeferrer.CallbackInfo ci = this.m_Fork.Dequeue();
					MeshGenerationDeferrer.Invoke(ci, meshGenerationContext);
				}
				for (int j = 0; j < workThenForkCount; j++)
				{
					MeshGenerationDeferrer.CallbackInfo ci2 = this.m_WorkThenFork.Dequeue();
					MeshGenerationDeferrer.Invoke(ci2, meshGenerationContext);
				}
				for (int k = 0; k < workCount; k++)
				{
					MeshGenerationDeferrer.CallbackInfo ci3 = this.m_Work.Dequeue();
					MeshGenerationDeferrer.Invoke(ci3, meshGenerationContext);
				}
				for (int l = 0; l < depCount; l++)
				{
					this.m_DependencyMerger.Add(this.m_Dependencies.Dequeue());
				}
				this.m_DependencyMerger.MergeAndReset().Complete();
				for (int m = 0; m < jobDependentForkCount; m++)
				{
					MeshGenerationDeferrer.CallbackInfo ci4 = this.m_JobDependentFork.Dequeue();
					MeshGenerationDeferrer.Invoke(ci4, meshGenerationContext);
				}
				for (int n = 0; n < jobDependentWorkThenForkCount; n++)
				{
					MeshGenerationDeferrer.CallbackInfo ci5 = this.m_JobDependentWorkThenFork.Dequeue();
					MeshGenerationDeferrer.Invoke(ci5, meshGenerationContext);
				}
				for (int i2 = 0; i2 < jobDependentWorkCount; i2++)
				{
					MeshGenerationDeferrer.CallbackInfo ci6 = this.m_JobDependentWork.Dequeue();
					MeshGenerationDeferrer.Invoke(ci6, meshGenerationContext);
				}
			}
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x0008C2B4 File Offset: 0x0008A4B4
		private static void Invoke(MeshGenerationDeferrer.CallbackInfo ci, MeshGenerationContext mgc)
		{
			try
			{
				ci.callback(mgc, ci.userData);
				bool flag = mgc.visualElement != null;
				if (flag)
				{
					Debug.LogWarning(string.Format("MeshGenerationContext is assigned to a VisualElement after calling '{0}'. Did you forget to call '{1}'?", ci.callback, "End"));
					mgc.End();
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x0008C328 File Offset: 0x0008A528
		// (set) Token: 0x060024C6 RID: 9414 RVA: 0x0008C330 File Offset: 0x0008A530
		private protected bool disposed { protected get; private set; }

		// Token: 0x060024C7 RID: 9415 RVA: 0x0008C339 File Offset: 0x0008A539
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0008C34C File Offset: 0x0008A54C
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_DependencyMerger.Dispose();
					this.m_DependencyMerger = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x040011BA RID: 4538
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_Fork = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x040011BB RID: 4539
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_WorkThenFork = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x040011BC RID: 4540
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_Work = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x040011BD RID: 4541
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_JobDependentFork = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x040011BE RID: 4542
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_JobDependentWorkThenFork = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x040011BF RID: 4543
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_JobDependentWork = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x040011C0 RID: 4544
		private Queue<JobHandle> m_Dependencies = new Queue<JobHandle>(32);

		// Token: 0x040011C1 RID: 4545
		private JobMerger m_DependencyMerger = new JobMerger(64);

		// Token: 0x02000534 RID: 1332
		private struct CallbackInfo
		{
			// Token: 0x040011C3 RID: 4547
			public MeshGenerationCallback callback;

			// Token: 0x040011C4 RID: 4548
			public object userData;
		}
	}
}
