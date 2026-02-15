using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A9 RID: 681
	public class Painter2D : IDisposable
	{
		// Token: 0x06001267 RID: 4711 RVA: 0x0004C938 File Offset: 0x0004AB38
		internal Painter2D(MeshGenerationContext ctx)
		{
			this.m_Handle = new SafeHandleAccess(UIPainter2D.Create(false));
			this.m_Ctx = ctx;
			this.m_JobSnapshots = new List<Painter2D.Painter2DJobData>(32);
			this.m_OnMeshGenerationDelegate = new MeshGenerationCallback(this.OnMeshGeneration);
			this.Reset();
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004C992 File Offset: 0x0004AB92
		internal void Reset()
		{
			UIPainter2D.Reset(this.m_Handle);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0004C9A6 File Offset: 0x0004ABA6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0004C9B8 File Offset: 0x0004ABB8
		private void Dispose(bool disposing)
		{
			bool disposed = this.m_Disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = !this.m_Handle.IsNull();
					if (flag)
					{
						UIPainter2D.Destroy(this.m_Handle);
						this.m_Handle = new SafeHandleAccess(IntPtr.Zero);
					}
					bool flag2 = this.m_DetachedAllocator != null;
					if (flag2)
					{
						this.m_DetachedAllocator.Dispose();
					}
					this.m_JobParameters.Dispose();
				}
				this.m_Disposed = true;
			}
		}

		// Token: 0x1700038A RID: 906
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x0004CA3C File Offset: 0x0004AC3C
		internal static bool isPainterActive
		{
			[CompilerGenerated]
			set
			{
				Painter2D.<isPainterActive>k__BackingField = value;
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0004CA44 File Offset: 0x0004AC44
		internal void ScheduleJobs(MeshGenerationContext mgc)
		{
			int snapshotCount = this.m_JobSnapshots.Count;
			bool flag = snapshotCount == 0;
			if (!flag)
			{
				bool flag2 = this.m_JobParameters.Length < snapshotCount;
				if (flag2)
				{
					this.m_JobParameters.Dispose();
					this.m_JobParameters = new NativeArray<Painter2D.Painter2DJobData>(snapshotCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				}
				for (int i = 0; i < snapshotCount; i++)
				{
					this.m_JobParameters[i] = this.m_JobSnapshots[i];
				}
				this.m_JobSnapshots.Clear();
				Painter2D.Painter2DJob job = new Painter2D.Painter2DJob
				{
					painterHandle = this.m_Handle,
					jobParameters = this.m_JobParameters.Slice(0, snapshotCount)
				};
				mgc.GetTempMeshAllocator(out job.allocator);
				JobHandle jobHandle = job.Schedule(snapshotCount, 1, default(JobHandle));
				mgc.AddMeshGenerationJob(jobHandle);
				mgc.AddMeshGenerationCallback(this.m_OnMeshGenerationDelegate, null, MeshGenerationCallbackType.Work, true);
			}
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0004CB42 File Offset: 0x0004AD42
		private void OnMeshGeneration(MeshGenerationContext ctx, object data)
		{
			UIPainter2D.ClearSnapshots(this.m_Handle);
		}

		// Token: 0x04000AAA RID: 2730
		private MeshGenerationContext m_Ctx;

		// Token: 0x04000AAB RID: 2731
		internal DetachedAllocator m_DetachedAllocator;

		// Token: 0x04000AAC RID: 2732
		internal SafeHandleAccess m_Handle;

		// Token: 0x04000AAD RID: 2733
		private List<Painter2D.Painter2DJobData> m_JobSnapshots = null;

		// Token: 0x04000AAE RID: 2734
		private NativeArray<Painter2D.Painter2DJobData> m_JobParameters;

		// Token: 0x04000AAF RID: 2735
		private bool m_Disposed;

		// Token: 0x04000AB1 RID: 2737
		private static readonly ProfilerMarker s_StrokeMarker = new ProfilerMarker("Painter2D.Stroke");

		// Token: 0x04000AB2 RID: 2738
		private static readonly ProfilerMarker s_FillMarker = new ProfilerMarker("Painter2D.Fill");

		// Token: 0x04000AB3 RID: 2739
		private MeshGenerationCallback m_OnMeshGenerationDelegate;

		// Token: 0x020002AA RID: 682
		private struct Painter2DJobData
		{
			// Token: 0x04000AB4 RID: 2740
			public UnsafeMeshGenerationNode node;

			// Token: 0x04000AB5 RID: 2741
			public int snapshotIndex;
		}

		// Token: 0x020002AB RID: 683
		private struct Painter2DJob : IJobParallelFor
		{
			// Token: 0x0600126F RID: 4719 RVA: 0x0004CB78 File Offset: 0x0004AD78
			public unsafe void Execute(int i)
			{
				Painter2D.Painter2DJobData data = this.jobParameters[i];
				MeshWriteDataInterface meshData = UIPainter2D.ExecuteSnapshotFromJob(this.painterHandle, data.snapshotIndex);
				NativeSlice<Vertex> nativeVertices = UIRenderDevice.PtrToSlice<Vertex>((void*)meshData.vertices, meshData.vertexCount);
				NativeSlice<ushort> nativeIndices = UIRenderDevice.PtrToSlice<ushort>((void*)meshData.indices, meshData.indexCount);
				bool flag = nativeVertices.Length == 0 || nativeIndices.Length == 0;
				if (!flag)
				{
					NativeSlice<Vertex> vertices;
					NativeSlice<ushort> indices;
					this.allocator.AllocateTempMesh(nativeVertices.Length, nativeIndices.Length, out vertices, out indices);
					Debug.Assert(vertices.Length == nativeVertices.Length);
					Debug.Assert(indices.Length == nativeIndices.Length);
					vertices.CopyFrom(nativeVertices);
					indices.CopyFrom(nativeIndices);
					data.node.DrawMesh(vertices, indices, null);
				}
			}

			// Token: 0x04000AB6 RID: 2742
			[NativeDisableUnsafePtrRestriction]
			public IntPtr painterHandle;

			// Token: 0x04000AB7 RID: 2743
			[ReadOnly]
			public TempMeshAllocator allocator;

			// Token: 0x04000AB8 RID: 2744
			[ReadOnly]
			public NativeSlice<Painter2D.Painter2DJobData> jobParameters;
		}
	}
}
