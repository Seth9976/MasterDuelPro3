using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x020001C8 RID: 456
	public struct GPUSort
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x00030504 File Offset: 0x0002E704
		public GPUSort(GPUSort.SystemResources resources)
		{
			this.resources = resources;
			this.m_Keywords = new LocalKeyword[]
			{
				new LocalKeyword(resources.computeAsset, "STAGE_BMS"),
				new LocalKeyword(resources.computeAsset, "STAGE_LOCAL_DISPERSE"),
				new LocalKeyword(resources.computeAsset, "STAGE_BIG_FLIP"),
				new LocalKeyword(resources.computeAsset, "STAGE_BIG_DISPERSE")
			};
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00030580 File Offset: 0x0002E780
		private void DispatchStage(CommandBuffer cmd, GPUSort.Args args, uint h, GPUSort.Stage stage)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<GPUSort.Stage>(stage)))
			{
				foreach (LocalKeyword i in this.m_Keywords)
				{
					cmd.SetKeyword(this.resources.computeAsset, in i, false);
				}
				cmd.SetKeyword(this.resources.computeAsset, in this.m_Keywords[(int)stage], true);
				cmd.SetComputeIntParam(this.resources.computeAsset, "_H", (int)h);
				cmd.SetComputeIntParam(this.resources.computeAsset, "_Total", (int)args.count);
				cmd.SetComputeBufferParam(this.resources.computeAsset, 0, "_KeyBuffer", args.resources.sortBufferKeys);
				cmd.SetComputeBufferParam(this.resources.computeAsset, 0, "_ValueBuffer", args.resources.sortBufferValues);
				cmd.DispatchCompute(this.resources.computeAsset, 0, args.workGroupCount, 1, 1);
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000306A0 File Offset: 0x0002E8A0
		private void CopyBuffer(CommandBuffer cmd, GraphicsBuffer src, GraphicsBuffer dst)
		{
			foreach (LocalKeyword i in this.m_Keywords)
			{
				cmd.SetKeyword(this.resources.computeAsset, in i, false);
			}
			int entriesToCopy = src.count * src.stride / 4;
			cmd.SetComputeBufferParam(this.resources.computeAsset, 1, "_CopySrcBuffer", src);
			cmd.SetComputeBufferParam(this.resources.computeAsset, 1, "_CopyDstBuffer", dst);
			cmd.SetComputeIntParam(this.resources.computeAsset, "_CopyEntriesCount", entriesToCopy);
			cmd.DispatchCompute(this.resources.computeAsset, 1, (entriesToCopy + 63) / 64, 1, 1);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0001350A File Offset: 0x0001170A
		internal static int DivRoundUp(int x, int y)
		{
			return (x + y - 1) / y;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00030750 File Offset: 0x0002E950
		public void Dispatch(CommandBuffer cmd, GPUSort.Args args)
		{
			uint i = args.count;
			this.CopyBuffer(cmd, args.inputKeys, args.resources.sortBufferKeys);
			this.CopyBuffer(cmd, args.inputValues, args.resources.sortBufferValues);
			args.workGroupCount = Math.Max(1, GPUSort.DivRoundUp((int)i, 2048));
			uint h = Math.Min(2048U, args.maxDepth);
			this.DispatchStage(cmd, args, h, GPUSort.Stage.LocalBMS);
			for (h *= 2U; h <= Math.Min(i, args.maxDepth); h *= 2U)
			{
				this.DispatchStage(cmd, args, h, GPUSort.Stage.BigFlip);
				for (uint hh = h / 2U; hh > 1U; hh /= 2U)
				{
					if (hh <= 2048U)
					{
						this.DispatchStage(cmd, args, hh, GPUSort.Stage.LocalDisperse);
						break;
					}
					this.DispatchStage(cmd, args, hh, GPUSort.Stage.BigDisperse);
				}
			}
		}

		// Token: 0x040008D0 RID: 2256
		private const uint kWorkGroupSize = 1024U;

		// Token: 0x040008D1 RID: 2257
		private LocalKeyword[] m_Keywords;

		// Token: 0x040008D2 RID: 2258
		private GPUSort.SystemResources resources;

		// Token: 0x020001C9 RID: 457
		public struct Args
		{
			// Token: 0x040008D3 RID: 2259
			public uint count;

			// Token: 0x040008D4 RID: 2260
			public uint maxDepth;

			// Token: 0x040008D5 RID: 2261
			public GraphicsBuffer inputKeys;

			// Token: 0x040008D6 RID: 2262
			public GraphicsBuffer inputValues;

			// Token: 0x040008D7 RID: 2263
			public GPUSort.SupportResources resources;

			// Token: 0x040008D8 RID: 2264
			internal int workGroupCount;
		}

		// Token: 0x020001CA RID: 458
		public struct RenderGraphResources
		{
			// Token: 0x06000D3D RID: 3389 RVA: 0x00030818 File Offset: 0x0002EA18
			public static GPUSort.RenderGraphResources Create(int count, RenderGraph renderGraph, RenderGraphBuilder builder)
			{
				GraphicsBuffer.Target targets = GraphicsBuffer.Target.CopyDestination | GraphicsBuffer.Target.Raw;
				GPUSort.RenderGraphResources renderGraphResources = default(GPUSort.RenderGraphResources);
				BufferDesc bufferDesc = new BufferDesc(count, 4, targets);
				bufferDesc.name = "Keys";
				renderGraphResources.sortBufferKeys = builder.CreateTransientBuffer(in bufferDesc);
				BufferDesc bufferDesc2 = new BufferDesc(count, 4, targets);
				bufferDesc2.name = "Values";
				renderGraphResources.sortBufferValues = builder.CreateTransientBuffer(in bufferDesc2);
				return renderGraphResources;
			}

			// Token: 0x040008D9 RID: 2265
			public BufferHandle sortBufferKeys;

			// Token: 0x040008DA RID: 2266
			public BufferHandle sortBufferValues;
		}

		// Token: 0x020001CB RID: 459
		public struct SupportResources
		{
			// Token: 0x06000D3E RID: 3390 RVA: 0x00030880 File Offset: 0x0002EA80
			public static GPUSort.SupportResources Load(GPUSort.RenderGraphResources renderGraphResources)
			{
				return new GPUSort.SupportResources
				{
					sortBufferKeys = renderGraphResources.sortBufferKeys,
					sortBufferValues = renderGraphResources.sortBufferValues
				};
			}

			// Token: 0x06000D3F RID: 3391 RVA: 0x000308BA File Offset: 0x0002EABA
			public void Dispose()
			{
				if (this.sortBufferKeys != null)
				{
					this.sortBufferKeys.Dispose();
					this.sortBufferKeys = null;
				}
				if (this.sortBufferValues != null)
				{
					this.sortBufferValues.Dispose();
					this.sortBufferValues = null;
				}
			}

			// Token: 0x040008DB RID: 2267
			public GraphicsBuffer sortBufferKeys;

			// Token: 0x040008DC RID: 2268
			public GraphicsBuffer sortBufferValues;
		}

		// Token: 0x020001CC RID: 460
		public struct SystemResources
		{
			// Token: 0x040008DD RID: 2269
			public ComputeShader computeAsset;
		}

		// Token: 0x020001CD RID: 461
		private enum Stage
		{
			// Token: 0x040008DF RID: 2271
			LocalBMS,
			// Token: 0x040008E0 RID: 2272
			LocalDisperse,
			// Token: 0x040008E1 RID: 2273
			BigFlip,
			// Token: 0x040008E2 RID: 2274
			BigDisperse
		}
	}
}
