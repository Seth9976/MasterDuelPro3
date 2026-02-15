using System;
using Unity.Collections;

namespace UnityEngine.Rendering.RendererUtils
{
	// Token: 0x020003DF RID: 991
	public struct RendererListDesc
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0003B5EB File Offset: 0x000397EB
		public readonly uint batchLayerMask { get; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x0003B5F3 File Offset: 0x000397F3
		internal readonly CullingResults cullingResult { get; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x0003B5FB File Offset: 0x000397FB
		internal readonly Camera camera { get; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x0003B603 File Offset: 0x00039803
		internal readonly ShaderTagId passName { get; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0003B60B File Offset: 0x0003980B
		internal readonly ShaderTagId[] passNames { get; }

		// Token: 0x06001B16 RID: 6934 RVA: 0x0003B614 File Offset: 0x00039814
		public bool IsValid()
		{
			bool flag = this.camera == null || (this.passName == ShaderTagId.none && (this.passNames == null || this.passNames.Length == 0));
			return !flag;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0003B66C File Offset: 0x0003986C
		public static RendererListParams ConvertToParameters(in RendererListDesc desc)
		{
			RendererListDesc rendererListDesc = desc;
			bool flag = !rendererListDesc.IsValid();
			RendererListParams rendererListParams;
			if (flag)
			{
				rendererListParams = RendererListParams.Invalid;
			}
			else
			{
				RendererListParams rlParams = default(RendererListParams);
				SortingSettings sortingSettings = new SortingSettings(desc.camera)
				{
					criteria = desc.sortingCriteria
				};
				DrawingSettings drawSettings = new DrawingSettings(RendererListDesc.s_EmptyName, sortingSettings)
				{
					perObjectData = desc.rendererConfiguration
				};
				bool flag2 = desc.passName != ShaderTagId.none;
				if (flag2)
				{
					Debug.Assert(desc.passNames == null);
					drawSettings.SetShaderPassName(0, desc.passName);
				}
				else
				{
					for (int i = 0; i < desc.passNames.Length; i++)
					{
						drawSettings.SetShaderPassName(i, desc.passNames[i]);
					}
				}
				bool flag3 = desc.overrideShader != null;
				if (flag3)
				{
					drawSettings.overrideShader = desc.overrideShader;
					drawSettings.overrideShaderPassIndex = desc.overrideShaderPassIndex;
				}
				bool flag4 = desc.overrideMaterial != null;
				if (flag4)
				{
					drawSettings.overrideMaterial = desc.overrideMaterial;
					drawSettings.overrideMaterialPassIndex = desc.overrideMaterialPassIndex;
				}
				FilteringSettings filterSettings = new FilteringSettings(new RenderQueueRange?(desc.renderQueueRange), desc.layerMask, desc.renderingLayerMask, 0)
				{
					excludeMotionVectorObjects = desc.excludeObjectMotionVectors,
					batchLayerMask = desc.batchLayerMask
				};
				rlParams.cullingResults = desc.cullingResult;
				rlParams.drawSettings = drawSettings;
				rlParams.filteringSettings = filterSettings;
				rlParams.tagName = ShaderTagId.none;
				rlParams.isPassTagName = false;
				bool flag5 = desc.stateBlock != null && desc.stateBlock != null;
				if (flag5)
				{
					NativeArray<RenderStateBlock> nativeArray = new NativeArray<RenderStateBlock>(1, Allocator.Temp, NativeArrayOptions.ClearMemory);
					nativeArray[0] = desc.stateBlock.Value;
					rlParams.stateBlocks = new NativeArray<RenderStateBlock>?(nativeArray);
					NativeArray<ShaderTagId> nativeArray2 = new NativeArray<ShaderTagId>(1, Allocator.Temp, NativeArrayOptions.ClearMemory);
					nativeArray2[0] = ShaderTagId.none;
					rlParams.tagValues = new NativeArray<ShaderTagId>?(nativeArray2);
				}
				rendererListParams = rlParams;
			}
			return rendererListParams;
		}

		// Token: 0x04000D0A RID: 3338
		public SortingCriteria sortingCriteria;

		// Token: 0x04000D0B RID: 3339
		public PerObjectData rendererConfiguration;

		// Token: 0x04000D0C RID: 3340
		public RenderQueueRange renderQueueRange;

		// Token: 0x04000D0D RID: 3341
		public RenderStateBlock? stateBlock;

		// Token: 0x04000D0E RID: 3342
		public Shader overrideShader;

		// Token: 0x04000D0F RID: 3343
		public Material overrideMaterial;

		// Token: 0x04000D10 RID: 3344
		public bool excludeObjectMotionVectors;

		// Token: 0x04000D11 RID: 3345
		public int layerMask;

		// Token: 0x04000D12 RID: 3346
		public uint renderingLayerMask;

		// Token: 0x04000D14 RID: 3348
		public int overrideMaterialPassIndex;

		// Token: 0x04000D15 RID: 3349
		public int overrideShaderPassIndex;

		// Token: 0x04000D1A RID: 3354
		private static readonly ShaderTagId s_EmptyName = new ShaderTagId("");
	}
}
