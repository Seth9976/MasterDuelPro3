using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A3 RID: 675
	public class MeshGenerationContext
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0004C20B File Offset: 0x0004A40B
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x0004C213 File Offset: 0x0004A413
		public VisualElement visualElement { get; private set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004C21C File Offset: 0x0004A41C
		public Painter2D painter2D
		{
			get
			{
				bool disposed = this.disposed;
				Painter2D painter2D;
				if (disposed)
				{
					Debug.LogError("Accessing painter2D on disposed MeshGenerationContext");
					painter2D = null;
				}
				else
				{
					bool flag = this.m_Painter2D == null;
					if (flag)
					{
						this.m_Painter2D = new Painter2D(this);
					}
					painter2D = this.m_Painter2D;
				}
				return painter2D;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004C267 File Offset: 0x0004A467
		internal bool hasPainter2D
		{
			get
			{
				return this.m_Painter2D != null;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0004C272 File Offset: 0x0004A472
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x0004C27A File Offset: 0x0004A47A
		internal IMeshGenerator meshGenerator { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0004C283 File Offset: 0x0004A483
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x0004C28B File Offset: 0x0004A48B
		internal EntryRecorder entryRecorder { get; private set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0004C294 File Offset: 0x0004A494
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x0004C29C File Offset: 0x0004A49C
		internal Entry parentEntry { get; private set; }

		// Token: 0x0600123B RID: 4667 RVA: 0x0004C2A5 File Offset: 0x0004A4A5
		internal MeshGenerationContext(MeshWriteDataPool meshWriteDataPool, EntryRecorder entryRecorder, TempMeshAllocatorImpl allocator, MeshGenerationDeferrer meshGenerationDeferrer, MeshGenerationNodeManager meshGenerationNodeManager)
		{
			this.m_MeshWriteDataPool = meshWriteDataPool;
			this.m_Allocator = allocator;
			this.m_MeshGenerationDeferrer = meshGenerationDeferrer;
			this.m_MeshGenerationNodeManager = meshGenerationNodeManager;
			this.entryRecorder = entryRecorder;
			this.meshGenerator = new MeshGenerator(this);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0004C2E2 File Offset: 0x0004A4E2
		public void AllocateTempMesh(int vertexCount, int indexCount, out NativeSlice<Vertex> vertices, out NativeSlice<ushort> indices)
		{
			this.m_Allocator.AllocateTempMesh(vertexCount, indexCount, out vertices, out indices);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0004C2F6 File Offset: 0x0004A4F6
		internal void DrawNativeText(NativeTextInfo textInfo, Vector2 pos)
		{
			this.meshGenerator.DrawNativeText(textInfo, pos);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0004C307 File Offset: 0x0004A507
		public void GetTempMeshAllocator(out TempMeshAllocator allocator)
		{
			this.m_Allocator.CreateNativeHandle(out allocator);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0004C318 File Offset: 0x0004A518
		public void InsertMeshGenerationNode(out MeshGenerationNode node)
		{
			Entry entry = this.entryRecorder.InsertPlaceholder(this.parentEntry);
			this.m_MeshGenerationNodeManager.CreateNode(entry, out node);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0004C348 File Offset: 0x0004A548
		internal void InsertUnsafeMeshGenerationNode(out UnsafeMeshGenerationNode node)
		{
			Entry entry = this.entryRecorder.InsertPlaceholder(this.parentEntry);
			this.m_MeshGenerationNodeManager.CreateUnsafeNode(entry, out node);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0004C376 File Offset: 0x0004A576
		public void AddMeshGenerationJob(JobHandle jobHandle)
		{
			this.m_MeshGenerationDeferrer.AddMeshGenerationJob(jobHandle);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0004C386 File Offset: 0x0004A586
		internal void AddMeshGenerationCallback(MeshGenerationCallback callback, object userData, MeshGenerationCallbackType callbackType, bool isJobDependent)
		{
			this.m_MeshGenerationDeferrer.AddMeshGenerationCallback(callback, userData, callbackType, isJobDependent);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0004C39C File Offset: 0x0004A59C
		internal void Begin(Entry parentEntry, VisualElement ve)
		{
			bool flag = this.visualElement != null;
			if (flag)
			{
				throw new InvalidOperationException("Begin can only be called when there is no target set. Did you forget to call End?");
			}
			bool flag2 = parentEntry == null;
			if (flag2)
			{
				throw new ArgumentException("The state of the provided MeshGenerationNode is invalid (entry is null).");
			}
			bool flag3 = parentEntry.firstChild != null;
			if (flag3)
			{
				throw new ArgumentException("The state of the provided MeshGenerationNode is invalid (entry isn't empty).");
			}
			bool flag4 = ve == null;
			if (flag4)
			{
				throw new ArgumentException("ve");
			}
			this.parentEntry = parentEntry;
			this.visualElement = ve;
			this.meshGenerator.currentElement = ve;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0004C420 File Offset: 0x0004A620
		internal void End()
		{
			bool flag = this.visualElement == null;
			if (flag)
			{
				throw new InvalidOperationException("End can only be called after a successful call to Begin.");
			}
			this.meshGenerator.currentElement = null;
			this.visualElement = null;
			this.parentEntry = null;
			Painter2D painter2D = this.m_Painter2D;
			if (painter2D != null)
			{
				painter2D.Reset();
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x0004C475 File Offset: 0x0004A675
		// (set) Token: 0x06001246 RID: 4678 RVA: 0x0004C47D File Offset: 0x0004A67D
		internal bool disposed { get; private set; }

		// Token: 0x06001247 RID: 4679 RVA: 0x0004C486 File Offset: 0x0004A686
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004C498 File Offset: 0x0004A698
		private void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					Painter2D painter2D = this.m_Painter2D;
					if (painter2D != null)
					{
						painter2D.Dispose();
					}
					this.m_Painter2D = null;
					this.m_MeshWriteDataPool = null;
					this.entryRecorder = null;
					MeshGenerator meshGenerator = this.meshGenerator as MeshGenerator;
					if (meshGenerator != null)
					{
						meshGenerator.Dispose();
					}
					this.meshGenerator = null;
					this.m_Allocator = null;
					this.m_MeshGenerationDeferrer = null;
					this.m_MeshGenerationNodeManager = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000A91 RID: 2705
		private Painter2D m_Painter2D;

		// Token: 0x04000A92 RID: 2706
		private MeshWriteDataPool m_MeshWriteDataPool;

		// Token: 0x04000A93 RID: 2707
		private TempMeshAllocatorImpl m_Allocator;

		// Token: 0x04000A94 RID: 2708
		private MeshGenerationDeferrer m_MeshGenerationDeferrer;

		// Token: 0x04000A95 RID: 2709
		private MeshGenerationNodeManager m_MeshGenerationNodeManager;

		// Token: 0x04000A99 RID: 2713
		private static readonly ProfilerMarker k_AllocateMarker = new ProfilerMarker("UIR.MeshGenerationContext.Allocate");

		// Token: 0x04000A9A RID: 2714
		private static readonly ProfilerMarker k_DrawVectorImageMarker = new ProfilerMarker("UIR.MeshGenerationContext.DrawVectorImage");

		// Token: 0x020002A4 RID: 676
		[Flags]
		internal enum MeshFlags
		{
			// Token: 0x04000A9D RID: 2717
			None = 0,
			// Token: 0x04000A9E RID: 2718
			SkipDynamicAtlas = 2,
			// Token: 0x04000A9F RID: 2719
			IsUsingVectorImageGradients = 4
		}
	}
}
