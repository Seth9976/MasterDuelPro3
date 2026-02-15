using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000290 RID: 656
	internal struct PassData
	{
		// Token: 0x060011AA RID: 4522 RVA: 0x000427ED File Offset: 0x000409ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Name GetName(CompilerContextData ctx)
		{
			return ctx.GetFullPassName(this.passId);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000427FC File Offset: 0x000409FC
		public PassData(in RenderGraphPass pass, int passIndex)
		{
			this.passId = passIndex;
			this.type = pass.type;
			this.asyncCompute = pass.enableAsyncCompute;
			this.hasSideEffects = !pass.allowPassCulling;
			this.hasFoveatedRasterization = pass.enableFoveatedRasterization;
			this.mergeState = PassMergeState.None;
			this.nativePassIndex = -1;
			this.nativeSubPassIndex = -1;
			this.beginNativeSubpass = false;
			this.culled = false;
			this.tag = 0;
			this.firstInput = 0;
			this.numInputs = 0;
			this.firstOutput = 0;
			this.numOutputs = 0;
			this.firstFragment = 0;
			this.numFragments = 0;
			this.firstRandomAccessResource = 0;
			this.numRandomAccessResources = 0;
			this.firstFragmentInput = 0;
			this.numFragmentInputs = 0;
			this.firstCreate = 0;
			this.numCreated = 0;
			this.firstDestroy = 0;
			this.numDestroyed = 0;
			this.fragmentInfoValid = false;
			this.fragmentInfoWidth = 0;
			this.fragmentInfoHeight = 0;
			this.fragmentInfoVolumeDepth = 0;
			this.fragmentInfoSamples = 0;
			this.fragmentInfoHasDepth = false;
			this.insertGraphicsFence = false;
			this.waitOnGraphicsFencePassId = -1;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0004290C File Offset: 0x00040B0C
		public void ResetAndInitialize(in RenderGraphPass pass, int passIndex)
		{
			this.passId = passIndex;
			this.type = pass.type;
			this.asyncCompute = pass.enableAsyncCompute;
			this.hasSideEffects = !pass.allowPassCulling;
			this.hasFoveatedRasterization = pass.enableFoveatedRasterization;
			this.mergeState = PassMergeState.None;
			this.nativePassIndex = -1;
			this.nativeSubPassIndex = -1;
			this.beginNativeSubpass = false;
			this.culled = false;
			this.tag = 0;
			this.firstInput = 0;
			this.numInputs = 0;
			this.firstOutput = 0;
			this.numOutputs = 0;
			this.firstFragment = 0;
			this.numFragments = 0;
			this.firstFragmentInput = 0;
			this.numFragmentInputs = 0;
			this.firstRandomAccessResource = 0;
			this.numRandomAccessResources = 0;
			this.firstCreate = 0;
			this.numCreated = 0;
			this.firstDestroy = 0;
			this.numDestroyed = 0;
			this.fragmentInfoValid = false;
			this.fragmentInfoWidth = 0;
			this.fragmentInfoHeight = 0;
			this.fragmentInfoVolumeDepth = 0;
			this.fragmentInfoSamples = 0;
			this.fragmentInfoHasDepth = false;
			this.insertGraphicsFence = false;
			this.waitOnGraphicsFencePassId = -1;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00042A1B File Offset: 0x00040C1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<PassOutputData> Outputs(CompilerContextData ctx)
		{
			return (ref ctx.outputData).MakeReadOnlySpan(this.firstOutput, this.numOutputs);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00042A34 File Offset: 0x00040C34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<PassInputData> Inputs(CompilerContextData ctx)
		{
			return (ref ctx.inputData).MakeReadOnlySpan(this.firstInput, this.numInputs);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00042A4D File Offset: 0x00040C4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<PassFragmentData> Fragments(CompilerContextData ctx)
		{
			return (ref ctx.fragmentData).MakeReadOnlySpan(this.firstFragment, this.numFragments);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00042A66 File Offset: 0x00040C66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<PassFragmentData> FragmentInputs(CompilerContextData ctx)
		{
			return (ref ctx.fragmentData).MakeReadOnlySpan(this.firstFragmentInput, this.numFragmentInputs);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00042A7F File Offset: 0x00040C7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<ResourceHandle> FirstUsedResources(CompilerContextData ctx)
		{
			return (ref ctx.createData).MakeReadOnlySpan(this.firstCreate, this.numCreated);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00042A98 File Offset: 0x00040C98
		public ReadOnlySpan<PassRandomWriteData> RandomWriteTextures(CompilerContextData ctx)
		{
			return (ref ctx.randomAccessResourceData).MakeReadOnlySpan(this.firstRandomAccessResource, this.numRandomAccessResources);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00042AB1 File Offset: 0x00040CB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<ResourceHandle> LastUsedResources(CompilerContextData ctx)
		{
			return (ref ctx.destroyData).MakeReadOnlySpan(this.firstDestroy, this.numDestroyed);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00042ACC File Offset: 0x00040CCC
		private void SetupAndValidateFragmentInfo(ResourceHandle h, CompilerContextData ctx)
		{
			ref ResourceUnversionedData resInfo = ref ctx.UnversionedResourceData(h);
			if (!this.fragmentInfoValid)
			{
				this.fragmentInfoWidth = resInfo.width;
				this.fragmentInfoHeight = resInfo.height;
				this.fragmentInfoSamples = resInfo.msaaSamples;
				this.fragmentInfoVolumeDepth = resInfo.volumeDepth;
				this.fragmentInfoValid = true;
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00042B20 File Offset: 0x00040D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void AddFragment(ResourceHandle h, CompilerContextData ctx)
		{
			this.SetupAndValidateFragmentInfo(h, ctx);
			this.numFragments++;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00042B38 File Offset: 0x00040D38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void AddFragmentInput(ResourceHandle h, CompilerContextData ctx)
		{
			this.SetupAndValidateFragmentInfo(h, ctx);
			this.numFragmentInputs++;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00042B50 File Offset: 0x00040D50
		internal void AddRandomAccessResource()
		{
			this.numRandomAccessResources++;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00042B60 File Offset: 0x00040D60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void AddFirstUse(ResourceHandle h, CompilerContextData ctx)
		{
			ReadOnlySpan<ResourceHandle> readOnlySpan = this.FirstUsedResources(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readonly ref ResourceHandle res = ref readOnlySpan[i];
				ResourceHandle resourceHandle = res;
				if (resourceHandle.index == h.index && res.type == h.type)
				{
					return;
				}
			}
			ctx.createData.Add(in h);
			int addedIndex = (ref ctx.createData).LastIndex<ResourceHandle>();
			if (this.numCreated == 0)
			{
				this.firstCreate = addedIndex;
			}
			this.numCreated++;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00042BF0 File Offset: 0x00040DF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void AddLastUse(ResourceHandle h, CompilerContextData ctx)
		{
			ReadOnlySpan<ResourceHandle> readOnlySpan = this.LastUsedResources(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				readonly ref ResourceHandle res = ref readOnlySpan[i];
				ResourceHandle resourceHandle = res;
				if (resourceHandle.index == h.index && res.type == h.type)
				{
					return;
				}
			}
			ctx.destroyData.Add(in h);
			int addedIndex = (ref ctx.destroyData).LastIndex<ResourceHandle>();
			if (this.numDestroyed == 0)
			{
				this.firstDestroy = addedIndex;
			}
			this.numDestroyed++;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00042C80 File Offset: 0x00040E80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly bool IsUsedAsFragment(ResourceHandle h, CompilerContextData ctx)
		{
			if (h.type != RenderGraphResourceType.Texture)
			{
				return false;
			}
			if (this.type != RenderGraphPassType.Raster)
			{
				return false;
			}
			ReadOnlySpan<PassFragmentData> readOnlySpan = this.Fragments(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ResourceHandle resourceHandle = readOnlySpan[i].resource;
				if (resourceHandle.index == h.index)
				{
					return true;
				}
			}
			readOnlySpan = this.FragmentInputs(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ResourceHandle resourceHandle = readOnlySpan[i].resource;
				if (resourceHandle.index == h.index)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000B82 RID: 2946
		public int passId;

		// Token: 0x04000B83 RID: 2947
		public RenderGraphPassType type;

		// Token: 0x04000B84 RID: 2948
		public bool hasFoveatedRasterization;

		// Token: 0x04000B85 RID: 2949
		public int tag;

		// Token: 0x04000B86 RID: 2950
		public PassMergeState mergeState;

		// Token: 0x04000B87 RID: 2951
		public int nativePassIndex;

		// Token: 0x04000B88 RID: 2952
		public int nativeSubPassIndex;

		// Token: 0x04000B89 RID: 2953
		public int firstInput;

		// Token: 0x04000B8A RID: 2954
		public int numInputs;

		// Token: 0x04000B8B RID: 2955
		public int firstOutput;

		// Token: 0x04000B8C RID: 2956
		public int numOutputs;

		// Token: 0x04000B8D RID: 2957
		public int firstFragment;

		// Token: 0x04000B8E RID: 2958
		public int numFragments;

		// Token: 0x04000B8F RID: 2959
		public int firstFragmentInput;

		// Token: 0x04000B90 RID: 2960
		public int numFragmentInputs;

		// Token: 0x04000B91 RID: 2961
		public int firstRandomAccessResource;

		// Token: 0x04000B92 RID: 2962
		public int numRandomAccessResources;

		// Token: 0x04000B93 RID: 2963
		public int firstCreate;

		// Token: 0x04000B94 RID: 2964
		public int numCreated;

		// Token: 0x04000B95 RID: 2965
		public int firstDestroy;

		// Token: 0x04000B96 RID: 2966
		public int numDestroyed;

		// Token: 0x04000B97 RID: 2967
		public int fragmentInfoWidth;

		// Token: 0x04000B98 RID: 2968
		public int fragmentInfoHeight;

		// Token: 0x04000B99 RID: 2969
		public int fragmentInfoVolumeDepth;

		// Token: 0x04000B9A RID: 2970
		public int fragmentInfoSamples;

		// Token: 0x04000B9B RID: 2971
		public int waitOnGraphicsFencePassId;

		// Token: 0x04000B9C RID: 2972
		public bool asyncCompute;

		// Token: 0x04000B9D RID: 2973
		public bool hasSideEffects;

		// Token: 0x04000B9E RID: 2974
		public bool culled;

		// Token: 0x04000B9F RID: 2975
		public bool beginNativeSubpass;

		// Token: 0x04000BA0 RID: 2976
		public bool fragmentInfoValid;

		// Token: 0x04000BA1 RID: 2977
		public bool fragmentInfoHasDepth;

		// Token: 0x04000BA2 RID: 2978
		public bool insertGraphicsFence;
	}
}
