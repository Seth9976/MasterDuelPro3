using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000250 RID: 592
	[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal abstract class RenderGraphPass
	{
		// Token: 0x06001006 RID: 4102
		public abstract void Execute(InternalRenderGraphContext renderGraphContext);

		// Token: 0x06001007 RID: 4103
		public abstract void Release(RenderGraphObjectPool pool);

		// Token: 0x06001008 RID: 4104
		public abstract bool HasRenderFunc();

		// Token: 0x06001009 RID: 4105
		public abstract int GetRenderFuncHash();

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0003A61B File Offset: 0x0003881B
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x0003A623 File Offset: 0x00038823
		public string name { get; protected set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0003A62C File Offset: 0x0003882C
		// (set) Token: 0x0600100D RID: 4109 RVA: 0x0003A634 File Offset: 0x00038834
		public int index { get; protected set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0003A63D File Offset: 0x0003883D
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x0003A645 File Offset: 0x00038845
		public RenderGraphPassType type { get; internal set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x0003A64E File Offset: 0x0003884E
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x0003A656 File Offset: 0x00038856
		public ProfilingSampler customSampler { get; protected set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0003A65F File Offset: 0x0003885F
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x0003A667 File Offset: 0x00038867
		public bool enableAsyncCompute { get; protected set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0003A670 File Offset: 0x00038870
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x0003A678 File Offset: 0x00038878
		public bool allowPassCulling { get; protected set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0003A681 File Offset: 0x00038881
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x0003A689 File Offset: 0x00038889
		public bool allowGlobalState { get; protected set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0003A692 File Offset: 0x00038892
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0003A69A File Offset: 0x0003889A
		public bool enableFoveatedRasterization { get; protected set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003A6A3 File Offset: 0x000388A3
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x0003A6AB File Offset: 0x000388AB
		public TextureAccess depthAccess { get; protected set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0003A6B4 File Offset: 0x000388B4
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x0003A6BC File Offset: 0x000388BC
		public TextureAccess[] colorBufferAccess { get; protected set; } = new TextureAccess[RenderGraph.kMaxMRTCount];

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0003A6C5 File Offset: 0x000388C5
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x0003A6CD File Offset: 0x000388CD
		public int colorBufferMaxIndex { get; protected set; } = -1;

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0003A6D6 File Offset: 0x000388D6
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x0003A6DE File Offset: 0x000388DE
		public TextureAccess[] fragmentInputAccess { get; protected set; } = new TextureAccess[RenderGraph.kMaxMRTCount];

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0003A6E7 File Offset: 0x000388E7
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x0003A6EF File Offset: 0x000388EF
		public int fragmentInputMaxIndex { get; protected set; } = -1;

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003A6F8 File Offset: 0x000388F8
		// (set) Token: 0x06001025 RID: 4133 RVA: 0x0003A700 File Offset: 0x00038900
		public RenderGraphPass.RandomWriteResourceInfo[] randomAccessResource { get; protected set; } = new RenderGraphPass.RandomWriteResourceInfo[RenderGraph.kMaxMRTCount];

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0003A709 File Offset: 0x00038909
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0003A711 File Offset: 0x00038911
		public int randomAccessResourceMaxIndex { get; protected set; } = -1;

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0003A71A File Offset: 0x0003891A
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0003A722 File Offset: 0x00038922
		public bool generateDebugData { get; protected set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0003A72B File Offset: 0x0003892B
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x0003A733 File Offset: 0x00038933
		public bool allowRendererListCulling { get; protected set; }

		// Token: 0x0600102C RID: 4140 RVA: 0x0003A73C File Offset: 0x0003893C
		public RenderGraphPass()
		{
			for (int i = 0; i < 3; i++)
			{
				this.resourceReadLists[i] = new List<ResourceHandle>();
				this.resourceWriteLists[i] = new List<ResourceHandle>();
				this.transientResourceList[i] = new List<ResourceHandle>();
			}
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0003A80C File Offset: 0x00038A0C
		public void Clear()
		{
			this.name = "";
			this.index = -1;
			this.customSampler = null;
			for (int i = 0; i < 3; i++)
			{
				this.resourceReadLists[i].Clear();
				this.resourceWriteLists[i].Clear();
				this.transientResourceList[i].Clear();
			}
			this.usedRendererListList.Clear();
			this.setGlobalsList.Clear();
			this.useAllGlobalTextures = false;
			this.implicitReadsList.Clear();
			this.enableAsyncCompute = false;
			this.allowPassCulling = true;
			this.allowRendererListCulling = true;
			this.allowGlobalState = false;
			this.enableFoveatedRasterization = false;
			this.generateDebugData = true;
			this.colorBufferMaxIndex = -1;
			this.fragmentInputMaxIndex = -1;
			this.randomAccessResourceMaxIndex = -1;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0003A8CC File Offset: 0x00038ACC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasRenderAttachments()
		{
			return this.depthAccess.textureHandle.IsValid() || this.colorBufferAccess[0].textureHandle.IsValid() || this.colorBufferMaxIndex > 0;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003A914 File Offset: 0x00038B14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsTransient(in ResourceHandle res)
		{
			List<ResourceHandle>[] array = this.transientResourceList;
			ResourceHandle resourceHandle = res;
			return array[resourceHandle.iType].Contains(res);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0003A944 File Offset: 0x00038B44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsWritten(in ResourceHandle res)
		{
			int i = 0;
			for (;;)
			{
				int num = i;
				List<ResourceHandle>[] array = this.resourceWriteLists;
				ResourceHandle resourceHandle = res;
				if (num >= array[resourceHandle.iType].Count)
				{
					return false;
				}
				List<ResourceHandle>[] array2 = this.resourceWriteLists;
				resourceHandle = res;
				int index = array2[resourceHandle.iType][i].index;
				resourceHandle = res;
				if (index == resourceHandle.index)
				{
					break;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0003A9AC File Offset: 0x00038BAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsRead(in ResourceHandle res)
		{
			ResourceHandle resourceHandle = res;
			if (resourceHandle.IsVersioned)
			{
				List<ResourceHandle>[] array = this.resourceReadLists;
				resourceHandle = res;
				return array[resourceHandle.iType].Contains(res);
			}
			int i = 0;
			for (;;)
			{
				int num = i;
				List<ResourceHandle>[] array2 = this.resourceReadLists;
				resourceHandle = res;
				if (num >= array2[resourceHandle.iType].Count)
				{
					return false;
				}
				List<ResourceHandle>[] array3 = this.resourceReadLists;
				resourceHandle = res;
				int index = array3[resourceHandle.iType][i].index;
				resourceHandle = res;
				if (index == resourceHandle.index)
				{
					break;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0003AA48 File Offset: 0x00038C48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsAttachment(in TextureHandle res)
		{
			TextureAccess textureAccess = this.depthAccess;
			if (textureAccess.textureHandle.IsValid())
			{
				textureAccess = this.depthAccess;
				int index = textureAccess.textureHandle.handle.index;
				ResourceHandle resourceHandle = res.handle;
				if (index == resourceHandle.index)
				{
					return true;
				}
			}
			for (int i = 0; i < this.colorBufferAccess.Length; i++)
			{
				if (this.colorBufferAccess[i].textureHandle.IsValid())
				{
					int index2 = this.colorBufferAccess[i].textureHandle.handle.index;
					ResourceHandle resourceHandle = res.handle;
					if (index2 == resourceHandle.index)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0003AAF0 File Offset: 0x00038CF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddResourceWrite(in ResourceHandle res)
		{
			List<ResourceHandle>[] array = this.resourceWriteLists;
			ResourceHandle resourceHandle = res;
			array[resourceHandle.iType].Add(res);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0003AB20 File Offset: 0x00038D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddResourceRead(in ResourceHandle res)
		{
			List<ResourceHandle>[] array = this.resourceReadLists;
			ResourceHandle resourceHandle = res;
			array[resourceHandle.iType].Add(res);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0003AB50 File Offset: 0x00038D50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddTransientResource(in ResourceHandle res)
		{
			List<ResourceHandle>[] array = this.transientResourceList;
			ResourceHandle resourceHandle = res;
			array[resourceHandle.iType].Add(res);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0003AB7D File Offset: 0x00038D7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void UseRendererList(in RendererListHandle rendererList)
		{
			this.usedRendererListList.Add(rendererList);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0003AB90 File Offset: 0x00038D90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void EnableAsyncCompute(bool value)
		{
			this.enableAsyncCompute = value;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0003AB99 File Offset: 0x00038D99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AllowPassCulling(bool value)
		{
			this.allowPassCulling = value;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0003ABA2 File Offset: 0x00038DA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void EnableFoveatedRasterization(bool value)
		{
			this.enableFoveatedRasterization = value;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0003ABAB File Offset: 0x00038DAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AllowRendererListCulling(bool value)
		{
			this.allowRendererListCulling = value;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0003ABB4 File Offset: 0x00038DB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AllowGlobalState(bool value)
		{
			this.allowGlobalState = value;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0003ABBD File Offset: 0x00038DBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GenerateDebugData(bool value)
		{
			this.generateDebugData = value;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0003ABC6 File Offset: 0x00038DC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetColorBuffer(in TextureHandle resource, int index)
		{
			this.colorBufferMaxIndex = Math.Max(this.colorBufferMaxIndex, index);
			this.colorBufferAccess[index].textureHandle = resource;
			this.AddResourceWrite(in resource.handle);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0003AC00 File Offset: 0x00038E00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetColorBufferRaw(in TextureHandle resource, int index, AccessFlags accessFlags, int mipLevel, int depthSlice)
		{
			if (this.colorBufferAccess[index].textureHandle.handle.Equals(resource.handle) || !this.colorBufferAccess[index].textureHandle.IsValid())
			{
				this.colorBufferMaxIndex = Math.Max(this.colorBufferMaxIndex, index);
				this.colorBufferAccess[index].textureHandle = resource;
				this.colorBufferAccess[index].flags = accessFlags;
				this.colorBufferAccess[index].mipLevel = mipLevel;
				this.colorBufferAccess[index].depthSlice = depthSlice;
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0003ACAC File Offset: 0x00038EAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFragmentInputRaw(in TextureHandle resource, int index, AccessFlags accessFlags, int mipLevel, int depthSlice)
		{
			if (this.fragmentInputAccess[index].textureHandle.handle.Equals(resource.handle) || !this.fragmentInputAccess[index].textureHandle.IsValid())
			{
				this.fragmentInputMaxIndex = Math.Max(this.fragmentInputMaxIndex, index);
				this.fragmentInputAccess[index].textureHandle = resource;
				this.fragmentInputAccess[index].flags = accessFlags;
				this.fragmentInputAccess[index].mipLevel = mipLevel;
				this.fragmentInputAccess[index].depthSlice = depthSlice;
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0003AD58 File Offset: 0x00038F58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetRandomWriteResourceRaw(in ResourceHandle resource, int index, bool preserveCounterValue, AccessFlags accessFlags)
		{
			if (this.randomAccessResource[index].h.Equals(resource) || !this.randomAccessResource[index].h.IsValid())
			{
				this.randomAccessResourceMaxIndex = Math.Max(this.randomAccessResourceMaxIndex, index);
				RenderGraphPass.RandomWriteResourceInfo[] randomAccessResource = this.randomAccessResource;
				randomAccessResource[index].h = resource;
				randomAccessResource[index].preserveCounterValue = preserveCounterValue;
				return;
			}
			throw new InvalidOperationException("You can only bind a single texture to an random write input index. Verify your indexes are correct.");
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0003ADD6 File Offset: 0x00038FD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDepthBuffer(in TextureHandle resource, DepthAccess flags)
		{
			this.depthAccess = new TextureAccess(resource, (AccessFlags)flags, 0, 0);
			if ((flags & DepthAccess.Read) != (DepthAccess)0)
			{
				this.AddResourceRead(in resource.handle);
			}
			if ((flags & DepthAccess.Write) != (DepthAccess)0)
			{
				this.AddResourceWrite(in resource.handle);
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0003AE10 File Offset: 0x00039010
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDepthBufferRaw(in TextureHandle resource, AccessFlags accessFlags, int mipLevel, int depthSlice)
		{
			TextureAccess textureAccess = this.depthAccess;
			if (!textureAccess.textureHandle.handle.Equals(resource.handle))
			{
				textureAccess = this.depthAccess;
				if (textureAccess.textureHandle.IsValid())
				{
					return;
				}
			}
			this.depthAccess = new TextureAccess(resource, accessFlags, mipLevel, depthSlice);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0003AE68 File Offset: 0x00039068
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ComputeTextureHash(ref HashFNV1A32 generator, in ResourceHandle handle, RenderGraphResourceRegistry resources)
		{
			ResourceHandle resourceHandle = handle;
			if (resourceHandle.index == 0)
			{
				return;
			}
			int num;
			if (resources.IsRenderGraphResourceImported(in handle))
			{
				TextureResource res = resources.GetTextureResource(in handle);
				if (res.graphicsResource.externalTexture != null)
				{
					Texture externalTexture = res.graphicsResource.externalTexture;
					num = (int)externalTexture.graphicsFormat;
					generator.Append(in num);
					num = (int)externalTexture.dimension;
					generator.Append(in num);
					num = externalTexture.width;
					generator.Append(in num);
					num = externalTexture.height;
					generator.Append(in num);
					RenderTexture externalRT = externalTexture as RenderTexture;
					if (externalRT != null)
					{
						num = externalRT.antiAliasing;
						generator.Append(in num);
					}
				}
				else if (res.graphicsResource.rt != null)
				{
					RenderTexture rt = res.graphicsResource.rt;
					num = (int)rt.graphicsFormat;
					generator.Append(in num);
					num = (int)rt.dimension;
					generator.Append(in num);
					num = rt.antiAliasing;
					generator.Append(in num);
					if (res.graphicsResource.useScaling)
					{
						if (res.graphicsResource.scaleFunc != null)
						{
							generator.Append(res.graphicsResource.scaleFunc);
						}
						else
						{
							Vector2 scaleFactor = res.graphicsResource.scaleFactor;
							generator.Append(in scaleFactor);
						}
					}
					else
					{
						num = rt.width;
						generator.Append(in num);
						num = rt.height;
						generator.Append(in num);
					}
				}
				else if (res.graphicsResource.nameID != default(RenderTargetIdentifier))
				{
					ref TextureDesc desc = ref res.desc;
					num = (int)desc.format;
					generator.Append(in num);
					num = (int)desc.dimension;
					generator.Append(in num);
					num = (int)desc.msaaSamples;
					generator.Append(in num);
					generator.Append(in desc.width);
					generator.Append(in desc.height);
				}
				generator.Append(in res.desc.clearBuffer);
				generator.Append(in res.desc.discardBuffer);
				return;
			}
			TextureDesc desc2 = resources.GetTextureResourceDesc(in handle, false);
			num = (int)desc2.format;
			generator.Append(in num);
			num = (int)desc2.dimension;
			generator.Append(in num);
			num = (int)desc2.msaaSamples;
			generator.Append(in num);
			generator.Append(in desc2.clearBuffer);
			generator.Append(in desc2.discardBuffer);
			switch (desc2.sizeMode)
			{
			case TextureSizeMode.Explicit:
				generator.Append(in desc2.width);
				generator.Append(in desc2.height);
				return;
			case TextureSizeMode.Scale:
				generator.Append(in desc2.scale);
				return;
			case TextureSizeMode.Functor:
				generator.Append(desc2.func);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0003B120 File Offset: 0x00039320
		public void ComputeHash(ref HashFNV1A32 generator, RenderGraphResourceRegistry resources)
		{
			int num = (int)this.type;
			generator.Append(in num);
			bool flag = this.enableAsyncCompute;
			generator.Append(in flag);
			flag = this.allowPassCulling;
			generator.Append(in flag);
			flag = this.allowGlobalState;
			generator.Append(in flag);
			flag = this.enableFoveatedRasterization;
			generator.Append(in flag);
			ResourceHandle depthHandle = this.depthAccess.textureHandle.handle;
			if (depthHandle.IsValid())
			{
				this.ComputeTextureHash(ref generator, in depthHandle, resources);
				TextureAccess depthAccess = this.depthAccess;
				RenderGraphPass.ComputeHashForTextureAccess(ref generator, in depthHandle, in depthAccess);
			}
			for (int i = 0; i < this.colorBufferMaxIndex + 1; i++)
			{
				TextureAccess colorBufferAccessElement = this.colorBufferAccess[i];
				ResourceHandle handle = colorBufferAccessElement.textureHandle.handle;
				if (handle.IsValid())
				{
					this.ComputeTextureHash(ref generator, in handle, resources);
					RenderGraphPass.ComputeHashForTextureAccess(ref generator, in handle, in colorBufferAccessElement);
				}
			}
			num = this.colorBufferMaxIndex;
			generator.Append(in num);
			for (int j = 0; j < this.fragmentInputMaxIndex + 1; j++)
			{
				TextureAccess fragmentInputAccessElement = this.fragmentInputAccess[j];
				ResourceHandle handle2 = fragmentInputAccessElement.textureHandle.handle;
				if (handle2.IsValid())
				{
					this.ComputeTextureHash(ref generator, in handle2, resources);
					RenderGraphPass.ComputeHashForTextureAccess(ref generator, in handle2, in fragmentInputAccessElement);
				}
			}
			for (int k = 0; k < this.randomAccessResourceMaxIndex + 1; k++)
			{
				RenderGraphPass.RandomWriteResourceInfo rar = this.randomAccessResource[k];
				if (rar.h.IsValid())
				{
					num = rar.h.index;
					generator.Append(in num);
					generator.Append(in rar.preserveCounterValue);
				}
			}
			num = this.randomAccessResourceMaxIndex;
			generator.Append(in num);
			num = this.fragmentInputMaxIndex;
			generator.Append(in num);
			flag = this.generateDebugData;
			generator.Append(in flag);
			flag = this.allowRendererListCulling;
			generator.Append(in flag);
			for (int resType = 0; resType < 3; resType++)
			{
				List<ResourceHandle> resourceReads = this.resourceReadLists[resType];
				for (int l = 0; l < resourceReads.Count; l++)
				{
					num = resourceReads[l].index;
					generator.Append(in num);
				}
				List<ResourceHandle> resourceWrites = this.resourceWriteLists[resType];
				for (int m = 0; m < resourceWrites.Count; m++)
				{
					num = resourceWrites[m].index;
					generator.Append(in num);
				}
				List<ResourceHandle> resourceTransient = this.transientResourceList[resType];
				for (int n = 0; n < resourceTransient.Count; n++)
				{
					num = resourceTransient[n].index;
					generator.Append(in num);
				}
			}
			for (int i2 = 0; i2 < this.usedRendererListList.Count; i2++)
			{
				num = this.usedRendererListList[i2].handle;
				generator.Append(in num);
			}
			for (int i3 = 0; i3 < this.setGlobalsList.Count; i3++)
			{
				ValueTuple<TextureHandle, int> global = this.setGlobalsList[i3];
				num = global.Item1.handle.index;
				generator.Append(in num);
				generator.Append(in global.Item2);
			}
			generator.Append(in this.useAllGlobalTextures);
			for (int i4 = 0; i4 < this.implicitReadsList.Count; i4++)
			{
				num = this.implicitReadsList[i4].index;
				generator.Append(in num);
			}
			num = this.GetRenderFuncHash();
			generator.Append(in num);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0003B498 File Offset: 0x00039698
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void ComputeHashForTextureAccess(ref HashFNV1A32 generator, in ResourceHandle handle, in TextureAccess textureAccess)
		{
			ResourceHandle resourceHandle = handle;
			int num = resourceHandle.index;
			generator.Append(in num);
			num = (int)textureAccess.flags;
			generator.Append(in num);
			generator.Append(in textureAccess.mipLevel);
			generator.Append(in textureAccess.depthSlice);
		}

		// Token: 0x04000A5E RID: 2654
		public List<ResourceHandle>[] resourceReadLists = new List<ResourceHandle>[3];

		// Token: 0x04000A5F RID: 2655
		public List<ResourceHandle>[] resourceWriteLists = new List<ResourceHandle>[3];

		// Token: 0x04000A60 RID: 2656
		public List<ResourceHandle>[] transientResourceList = new List<ResourceHandle>[3];

		// Token: 0x04000A61 RID: 2657
		public List<RendererListHandle> usedRendererListList = new List<RendererListHandle>();

		// Token: 0x04000A62 RID: 2658
		public List<ValueTuple<TextureHandle, int>> setGlobalsList = new List<ValueTuple<TextureHandle, int>>();

		// Token: 0x04000A63 RID: 2659
		public bool useAllGlobalTextures;

		// Token: 0x04000A64 RID: 2660
		public List<ResourceHandle> implicitReadsList = new List<ResourceHandle>();

		// Token: 0x02000251 RID: 593
		public struct RandomWriteResourceInfo
		{
			// Token: 0x04000A65 RID: 2661
			public ResourceHandle h;

			// Token: 0x04000A66 RID: 2662
			public bool preserveCounterValue;
		}
	}
}
