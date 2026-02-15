using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine.Pool;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200044B RID: 1099
	internal class UITKTextJobSystem
	{
		// Token: 0x06001FD6 RID: 8150 RVA: 0x00075AF8 File Offset: 0x00073CF8
		public UITKTextJobSystem()
		{
			this.m_PrepareTextJobifiedCallback = new MeshGenerationCallback(this.PrepareTextJobified);
			this.m_GenerateTextJobifiedCallback = new MeshGenerationCallback(this.GenerateTextJobified);
			this.m_AddDrawEntriesCallback = new MeshGenerationCallback(this.AddDrawEntries);
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x00075B4E File Offset: 0x00073D4E
		private static void OnGetManagedJob(UITKTextJobSystem.ManagedJobData managedJobData)
		{
			managedJobData.vertices = null;
			managedJobData.indices = null;
			managedJobData.materials = null;
			managedJobData.renderModes = null;
			managedJobData.prepareSuccess = false;
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00075B74 File Offset: 0x00073D74
		internal void GenerateText(MeshGenerationContext mgc, TextElement textElement)
		{
			MeshGenerationNode node;
			mgc.InsertMeshGenerationNode(out node);
			UITKTextJobSystem.ManagedJobData managedJobData = UITKTextJobSystem.s_JobDataPool.Get();
			managedJobData.visualElement = textElement;
			managedJobData.node = node;
			this.textJobDatas.Add(managedJobData);
			bool flag = this.hasPendingTextWork;
			if (!flag)
			{
				this.hasPendingTextWork = true;
				this.textJobDatasHandle = GCHandle.Alloc(this.textJobDatas);
				mgc.AddMeshGenerationCallback(this.m_PrepareTextJobifiedCallback, null, MeshGenerationCallbackType.WorkThenFork, false);
			}
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x00075BE8 File Offset: 0x00073DE8
		internal void PrepareTextJobified(MeshGenerationContext mgc, object _)
		{
			TextHandle.InitThreadArrays();
			PanelTextSettings.InitializeDefaultPanelTextSettingsIfNull();
			TextHandle.UpdateCurrentFrame();
			this.hasPendingTextWork = false;
			UITKTextJobSystem.PrepareTextJobData prepareJob = new UITKTextJobSystem.PrepareTextJobData
			{
				managedJobDataHandle = this.textJobDatasHandle
			};
			TextGenerator.IsExecutingJob = true;
			JobHandle jobHandle = prepareJob.Schedule(this.textJobDatas.Count, 1, default(JobHandle));
			mgc.AddMeshGenerationJob(jobHandle);
			mgc.AddMeshGenerationCallback(this.m_GenerateTextJobifiedCallback, null, MeshGenerationCallbackType.Work, true);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00075C64 File Offset: 0x00073E64
		private void GenerateTextJobified(MeshGenerationContext mgc, object _)
		{
			TextGenerator.IsExecutingJob = false;
			foreach (UITKTextJobSystem.ManagedJobData textData in this.textJobDatas)
			{
				bool prepareSuccess = textData.prepareSuccess;
				if (!prepareSuccess)
				{
					textData.visualElement.uitkTextHandle.ConvertUssToTextGenerationSettings();
					textData.visualElement.uitkTextHandle.PrepareFontAsset();
				}
			}
			FontAsset.UpdateFontAssetsInUpdateQueue();
			TempMeshAllocator allocator;
			mgc.GetTempMeshAllocator(out allocator);
			UITKTextJobSystem.GenerateTextJobData textJob = new UITKTextJobSystem.GenerateTextJobData
			{
				managedJobDataHandle = this.textJobDatasHandle,
				alloc = allocator
			};
			TextHandle.UpdateCurrentFrame();
			TextGenerator.IsExecutingJob = true;
			JobHandle jobHandle = textJob.Schedule(this.textJobDatas.Count, 1, default(JobHandle));
			mgc.AddMeshGenerationJob(jobHandle);
			mgc.AddMeshGenerationCallback(this.m_AddDrawEntriesCallback, null, MeshGenerationCallbackType.Work, true);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00075D64 File Offset: 0x00073F64
		private static void ConvertMeshInfoToUIRVertex(MeshInfo[] meshInfos, TempMeshAllocator alloc, TextElement visualElement, ref List<Material> materials, ref List<NativeSlice<Vertex>> verticesArray, ref List<NativeSlice<ushort>> indicesArray, ref List<GlyphRenderMode> renderModes)
		{
			ObjectPool<List<Material>> objectPool = UITKTextJobSystem.s_MaterialPool;
			lock (objectPool)
			{
				materials = UITKTextJobSystem.s_MaterialPool.Get();
				verticesArray = UITKTextJobSystem.s_VerticesPool.Get();
				indicesArray = UITKTextJobSystem.s_IndicesPool.Get();
				renderModes = UITKTextJobSystem.s_RenderModesPool.Get();
			}
			Vector2 pos = visualElement.contentRect.min;
			bool hasMultipleColors = visualElement.uitkTextHandle.textInfo.hasMultipleColors;
			bool flag2 = hasMultipleColors;
			if (flag2)
			{
				visualElement.renderChainData.flags = visualElement.renderChainData.flags | RenderDataFlags.IsIgnoringDynamicColorHint;
			}
			else
			{
				visualElement.renderChainData.flags = visualElement.renderChainData.flags & ~RenderDataFlags.IsIgnoringDynamicColorHint;
			}
			foreach (MeshInfo meshInfo in meshInfos)
			{
				Debug.Assert((meshInfo.vertexCount & 3) == 0);
				int verticesPerAlloc = (int)((ulong)UIRenderDevice.maxVerticesPerPage & 18446744073709551612UL);
				int remainingVertexCount = meshInfo.vertexCount;
				int vSrc = 0;
				while (remainingVertexCount > 0)
				{
					int vertexCount = Mathf.Min(remainingVertexCount, verticesPerAlloc);
					int quadCount = vertexCount >> 2;
					int indexCount = quadCount * 6;
					materials.Add(meshInfo.material);
					renderModes.Add(meshInfo.glyphRenderMode);
					bool hasGradientScale = meshInfo.glyphRenderMode != GlyphRenderMode.SMOOTH && meshInfo.glyphRenderMode != GlyphRenderMode.COLOR;
					bool isDynamicColor = meshInfo.applySDF && !hasMultipleColors && (RenderEvents.NeedsColorID(visualElement) || (hasGradientScale && RenderEvents.NeedsTextCoreSettings(visualElement)));
					NativeSlice<Vertex> vertices;
					NativeSlice<ushort> indices;
					alloc.AllocateTempMesh(vertexCount, indexCount, out vertices, out indices);
					int vDst = 0;
					int j = 0;
					while (vDst < vertexCount)
					{
						vertices[vDst] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[vSrc], pos, isDynamicColor);
						vertices[vDst + 1] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[vSrc + 1], pos, isDynamicColor);
						vertices[vDst + 2] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[vSrc + 2], pos, isDynamicColor);
						vertices[vDst + 3] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[vSrc + 3], pos, isDynamicColor);
						indices[j] = (ushort)vDst;
						indices[j + 1] = (ushort)(vDst + 1);
						indices[j + 2] = (ushort)(vDst + 2);
						indices[j + 3] = (ushort)(vDst + 2);
						indices[j + 4] = (ushort)(vDst + 3);
						indices[j + 5] = (ushort)vDst;
						vDst += 4;
						vSrc += 4;
						j += 6;
					}
					verticesArray.Add(vertices);
					indicesArray.Add(indices);
					remainingVertexCount -= vertexCount;
				}
				Debug.Assert(remainingVertexCount == 0);
			}
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x00076060 File Offset: 0x00074260
		private void AddDrawEntries(MeshGenerationContext mgc, object _)
		{
			TextGenerator.IsExecutingJob = false;
			foreach (UITKTextJobSystem.ManagedJobData managedJobData in this.textJobDatas)
			{
				mgc.Begin(managedJobData.node.GetParentEntry(), managedJobData.visualElement);
				managedJobData.visualElement.uitkTextHandle.HandleLinkAndATagCallbacks();
				mgc.meshGenerator.DrawText(managedJobData.vertices, managedJobData.indices, managedJobData.materials, managedJobData.renderModes);
				managedJobData.visualElement.OnGenerateTextOver(mgc);
				mgc.End();
				managedJobData.Release();
			}
			this.textJobDatas.Clear();
			this.textJobDatasHandle.Free();
		}

		// Token: 0x04000E21 RID: 3617
		private static readonly ProfilerMarker k_ExecuteMarker = new ProfilerMarker("TextJob.GenerateText");

		// Token: 0x04000E22 RID: 3618
		private static readonly ProfilerMarker k_UpdateMainThreadMarker = new ProfilerMarker("TextJob.UpdateMainThread");

		// Token: 0x04000E23 RID: 3619
		private static readonly ProfilerMarker k_PrepareMainThreadMarker = new ProfilerMarker("TextJob.PrepareMainThread");

		// Token: 0x04000E24 RID: 3620
		private static readonly ProfilerMarker k_PrepareJobifiedMarker = new ProfilerMarker("TextJob.PrepareJobified");

		// Token: 0x04000E25 RID: 3621
		private GCHandle textJobDatasHandle;

		// Token: 0x04000E26 RID: 3622
		private List<UITKTextJobSystem.ManagedJobData> textJobDatas = new List<UITKTextJobSystem.ManagedJobData>();

		// Token: 0x04000E27 RID: 3623
		private bool hasPendingTextWork;

		// Token: 0x04000E28 RID: 3624
		private static ObjectPool<UITKTextJobSystem.ManagedJobData> s_JobDataPool = new ObjectPool<UITKTextJobSystem.ManagedJobData>(() => new UITKTextJobSystem.ManagedJobData(), new Action<UITKTextJobSystem.ManagedJobData>(UITKTextJobSystem.OnGetManagedJob), delegate(UITKTextJobSystem.ManagedJobData inst)
		{
			inst.visualElement = null;
		}, null, false, 10, 10000);

		// Token: 0x04000E29 RID: 3625
		private static ObjectPool<List<Material>> s_MaterialPool = new ObjectPool<List<Material>>(() => new List<Material>(), null, delegate(List<Material> list)
		{
			list.Clear();
		}, null, false, 10, 10000);

		// Token: 0x04000E2A RID: 3626
		private static ObjectPool<List<GlyphRenderMode>> s_RenderModesPool = new ObjectPool<List<GlyphRenderMode>>(() => new List<GlyphRenderMode>(), null, delegate(List<GlyphRenderMode> list)
		{
			list.Clear();
		}, null, false, 10, 10000);

		// Token: 0x04000E2B RID: 3627
		private static ObjectPool<List<NativeSlice<Vertex>>> s_VerticesPool = new ObjectPool<List<NativeSlice<Vertex>>>(() => new List<NativeSlice<Vertex>>(), null, delegate(List<NativeSlice<Vertex>> list)
		{
			list.Clear();
		}, null, false, 10, 10000);

		// Token: 0x04000E2C RID: 3628
		private static ObjectPool<List<NativeSlice<ushort>>> s_IndicesPool = new ObjectPool<List<NativeSlice<ushort>>>(() => new List<NativeSlice<ushort>>(), null, delegate(List<NativeSlice<ushort>> list)
		{
			list.Clear();
		}, null, false, 10, 10000);

		// Token: 0x04000E2D RID: 3629
		internal MeshGenerationCallback m_PrepareTextJobifiedCallback;

		// Token: 0x04000E2E RID: 3630
		internal MeshGenerationCallback m_GenerateTextJobifiedCallback;

		// Token: 0x04000E2F RID: 3631
		internal MeshGenerationCallback m_AddDrawEntriesCallback;

		// Token: 0x0200044C RID: 1100
		private class ManagedJobData
		{
			// Token: 0x06001FDE RID: 8158 RVA: 0x00076290 File Offset: 0x00074490
			public void Release()
			{
				bool flag = this.materials != null;
				if (flag)
				{
					UITKTextJobSystem.s_MaterialPool.Release(this.materials);
					UITKTextJobSystem.s_VerticesPool.Release(this.vertices);
					UITKTextJobSystem.s_IndicesPool.Release(this.indices);
					UITKTextJobSystem.s_RenderModesPool.Release(this.renderModes);
				}
				UITKTextJobSystem.s_JobDataPool.Release(this);
			}

			// Token: 0x04000E30 RID: 3632
			public TextElement visualElement;

			// Token: 0x04000E31 RID: 3633
			public MeshGenerationNode node;

			// Token: 0x04000E32 RID: 3634
			public List<Material> materials;

			// Token: 0x04000E33 RID: 3635
			public List<GlyphRenderMode> renderModes;

			// Token: 0x04000E34 RID: 3636
			public List<NativeSlice<Vertex>> vertices;

			// Token: 0x04000E35 RID: 3637
			public List<NativeSlice<ushort>> indices;

			// Token: 0x04000E36 RID: 3638
			public bool prepareSuccess;
		}

		// Token: 0x0200044D RID: 1101
		private struct PrepareTextJobData : IJobParallelFor
		{
			// Token: 0x06001FE0 RID: 8160 RVA: 0x00076300 File Offset: 0x00074500
			public void Execute(int index)
			{
				List<UITKTextJobSystem.ManagedJobData> managedJobDatas = (List<UITKTextJobSystem.ManagedJobData>)this.managedJobDataHandle.Target;
				UITKTextJobSystem.ManagedJobData managedJobData = managedJobDatas[index];
				TextElement visualElement = managedJobData.visualElement;
				managedJobData.prepareSuccess = visualElement.uitkTextHandle.ConvertUssToTextGenerationSettings();
				bool prepareSuccess = managedJobData.prepareSuccess;
				if (prepareSuccess)
				{
					managedJobData.prepareSuccess = visualElement.uitkTextHandle.PrepareFontAsset();
				}
			}

			// Token: 0x04000E37 RID: 3639
			public GCHandle managedJobDataHandle;
		}

		// Token: 0x0200044E RID: 1102
		private struct GenerateTextJobData : IJobParallelFor
		{
			// Token: 0x06001FE1 RID: 8161 RVA: 0x0007635C File Offset: 0x0007455C
			public void Execute(int index)
			{
				List<UITKTextJobSystem.ManagedJobData> managedJobDatas = (List<UITKTextJobSystem.ManagedJobData>)this.managedJobDataHandle.Target;
				UITKTextJobSystem.ManagedJobData managedJobData = managedJobDatas[index];
				TextElement visualElement = managedJobData.visualElement;
				visualElement.uitkTextHandle.UpdateMesh();
				TextInfo textInfo = visualElement.uitkTextHandle.textInfo;
				MeshInfo[] meshInfos = textInfo.meshInfo;
				List<Material> materials = null;
				List<NativeSlice<Vertex>> verticesArray = null;
				List<NativeSlice<ushort>> indicesArray = null;
				List<GlyphRenderMode> renderModes = null;
				UITKTextJobSystem.ConvertMeshInfoToUIRVertex(meshInfos, this.alloc, visualElement, ref materials, ref verticesArray, ref indicesArray, ref renderModes);
				managedJobData.materials = materials;
				managedJobData.vertices = verticesArray;
				managedJobData.indices = indicesArray;
				managedJobData.renderModes = renderModes;
				visualElement.uitkTextHandle.HandleATag();
				visualElement.uitkTextHandle.HandleLinkTag();
			}

			// Token: 0x04000E38 RID: 3640
			public GCHandle managedJobDataHandle;

			// Token: 0x04000E39 RID: 3641
			[ReadOnly]
			public TempMeshAllocator alloc;
		}
	}
}
