using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000285 RID: 645
	internal class CompilerContextData : IDisposable, RenderGraph.ICompiledGraph
	{
		// Token: 0x06001168 RID: 4456 RVA: 0x0003F13C File Offset: 0x0003D33C
		public CompilerContextData(int estimatedNumPasses)
		{
			this.passData = new NativeList<PassData>(estimatedNumPasses, AllocatorManager.Persistent);
			this.fences = new Dictionary<int, GraphicsFence>();
			this.passNames = new DynamicArray<Name>(estimatedNumPasses, false);
			this.inputData = new NativeList<PassInputData>(estimatedNumPasses * 2, AllocatorManager.Persistent);
			this.outputData = new NativeList<PassOutputData>(estimatedNumPasses * 2, AllocatorManager.Persistent);
			this.fragmentData = new NativeList<PassFragmentData>(estimatedNumPasses * 4, AllocatorManager.Persistent);
			this.randomAccessResourceData = new NativeList<PassRandomWriteData>(4, AllocatorManager.Persistent);
			this.resources = new ResourcesData();
			this.nativePassData = new NativeList<NativePassData>(estimatedNumPasses, AllocatorManager.Persistent);
			this.nativeSubPassData = new NativeList<SubPassDescriptor>(estimatedNumPasses, AllocatorManager.Persistent);
			this.createData = new NativeList<ResourceHandle>(estimatedNumPasses * 2, AllocatorManager.Persistent);
			this.destroyData = new NativeList<ResourceHandle>(estimatedNumPasses * 2, AllocatorManager.Persistent);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0003F215 File Offset: 0x0003D415
		public void Initialize(RenderGraphResourceRegistry resourceRegistry)
		{
			this.resources.Initialize(resourceRegistry);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0003F224 File Offset: 0x0003D424
		public void Clear()
		{
			this.passData.Clear();
			this.fences.Clear();
			this.passNames.Clear();
			this.inputData.Clear();
			this.outputData.Clear();
			this.fragmentData.Clear();
			this.randomAccessResourceData.Clear();
			this.resources.Clear();
			this.nativePassData.Clear();
			this.nativeSubPassData.Clear();
			this.createData.Clear();
			this.destroyData.Clear();
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0003F2B5 File Offset: 0x0003D4B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref ResourceUnversionedData UnversionedResourceData(ResourceHandle h)
		{
			return this.resources.unversionedData[h.iType].ElementAt(h.index);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003F2DA File Offset: 0x0003D4DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref ResourceVersionedData VersionedResourceData(ResourceHandle h)
		{
			return this.resources[h];
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0003F2E8 File Offset: 0x0003D4E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<ResourceReaderData> Readers(ResourceHandle h)
		{
			int firstReader = ResourcesData.IndexReader(h, 0);
			int numReaders = this.resources[h].numReaders;
			return (ref this.resources.readerData[h.iType]).MakeReadOnlySpan(firstReader, numReaders);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0003F32D File Offset: 0x0003D52D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref ResourceReaderData ResourceReader(ResourceHandle h, int i)
		{
			ref ResourceVersionedData ptr = ref this.resources[h];
			return this.resources.readerData[h.iType].ElementAt(ResourcesData.IndexReader(h, 0) + i);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0003F364 File Offset: 0x0003D564
		public bool AddToFragmentList(TextureAccess access, int listFirstIndex, int numItems)
		{
			for (int i = listFirstIndex; i < listFirstIndex + numItems; i++)
			{
				if (this.fragmentData.ElementAt(i).resource.index == access.textureHandle.handle.index)
				{
					return false;
				}
			}
			PassFragmentData passFragmentData = default(PassFragmentData);
			passFragmentData.resource = access.textureHandle.handle;
			passFragmentData.accessFlags = access.flags;
			passFragmentData.mipLevel = access.mipLevel;
			passFragmentData.depthSlice = access.depthSlice;
			this.fragmentData.Add(in passFragmentData);
			return true;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0003F3F9 File Offset: 0x0003D5F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe Name GetFullPassName(int passId)
		{
			return *this.passNames[passId];
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003F40C File Offset: 0x0003D60C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string GetPassName(int passId)
		{
			return this.passNames[passId].name;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0003F41F File Offset: 0x0003D61F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string GetResourceName(ResourceHandle h)
		{
			return this.resources.resourceNames[h.iType][h.index].name;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0003F448 File Offset: 0x0003D648
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string GetResourceVersionedName(ResourceHandle h)
		{
			return this.GetResourceName(h) + " V" + h.version.ToString();
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0003F478 File Offset: 0x0003D678
		public bool AddToRandomAccessResourceList(ResourceHandle h, int randomWriteSlotIndex, bool preserveCounterValue, int listFirstIndex, int numItems)
		{
			PassRandomWriteData passRandomWriteData;
			for (int i = listFirstIndex; i < listFirstIndex + numItems; i++)
			{
				passRandomWriteData = this.randomAccessResourceData[i];
				if (passRandomWriteData.resource.index == h.index)
				{
					passRandomWriteData = this.randomAccessResourceData[i];
					if (passRandomWriteData.resource.type == h.type)
					{
						passRandomWriteData = this.randomAccessResourceData[i];
						if (passRandomWriteData.resource.version != h.version)
						{
							throw new Exception("Trying to UseTextureRandomWrite two versions of the same resource");
						}
						return false;
					}
				}
			}
			passRandomWriteData = default(PassRandomWriteData);
			passRandomWriteData.resource = h;
			passRandomWriteData.index = randomWriteSlotIndex;
			passRandomWriteData.preserveCounterValue = preserveCounterValue;
			this.randomAccessResourceData.Add(in passRandomWriteData);
			return true;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0003F538 File Offset: 0x0003D738
		public void TagAllPasses(int value)
		{
			for (int passId = 0; passId < this.passData.Length; passId++)
			{
				this.passData.ElementAt(passId).tag = value;
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0003F570 File Offset: 0x0003D770
		public void CullAllPasses(bool isCulled)
		{
			for (int passId = 0; passId < this.passData.Length; passId++)
			{
				this.passData.ElementAt(passId).culled = isCulled;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0003F5A5 File Offset: 0x0003D7A5
		public CompilerContextData.NativePassIterator NativePasses
		{
			get
			{
				return new CompilerContextData.NativePassIterator(this);
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0003F5B0 File Offset: 0x0003D7B0
		internal List<NativePassData> GetNativePasses()
		{
			List<NativePassData> result = new List<NativePassData>();
			foreach (ref NativePassData pass in this.NativePasses)
			{
				result.Add(pass);
			}
			return result;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0003F5F4 File Offset: 0x0003D7F4
		~CompilerContextData()
		{
			this.Cleanup();
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0003F620 File Offset: 0x0003D820
		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0003F630 File Offset: 0x0003D830
		private void Cleanup()
		{
			if (!this.m_Disposed)
			{
				this.resources.Dispose();
				this.passData.Dispose();
				this.inputData.Dispose();
				this.outputData.Dispose();
				this.fragmentData.Dispose();
				this.createData.Dispose();
				this.destroyData.Dispose();
				this.randomAccessResourceData.Dispose();
				this.nativePassData.Dispose();
				this.nativeSubPassData.Dispose();
				this.m_Disposed = true;
			}
		}

		// Token: 0x04000B3F RID: 2879
		public ResourcesData resources;

		// Token: 0x04000B40 RID: 2880
		public NativeList<PassData> passData;

		// Token: 0x04000B41 RID: 2881
		public Dictionary<int, GraphicsFence> fences;

		// Token: 0x04000B42 RID: 2882
		public DynamicArray<Name> passNames;

		// Token: 0x04000B43 RID: 2883
		public NativeList<PassInputData> inputData;

		// Token: 0x04000B44 RID: 2884
		public NativeList<PassOutputData> outputData;

		// Token: 0x04000B45 RID: 2885
		public NativeList<PassFragmentData> fragmentData;

		// Token: 0x04000B46 RID: 2886
		public NativeList<ResourceHandle> createData;

		// Token: 0x04000B47 RID: 2887
		public NativeList<ResourceHandle> destroyData;

		// Token: 0x04000B48 RID: 2888
		public NativeList<PassRandomWriteData> randomAccessResourceData;

		// Token: 0x04000B49 RID: 2889
		public NativeList<NativePassData> nativePassData;

		// Token: 0x04000B4A RID: 2890
		public NativeList<SubPassDescriptor> nativeSubPassData;

		// Token: 0x04000B4B RID: 2891
		private bool m_Disposed;

		// Token: 0x02000286 RID: 646
		public struct NativePassIterator
		{
			// Token: 0x0600117C RID: 4476 RVA: 0x0003F6BA File Offset: 0x0003D8BA
			public NativePassIterator(CompilerContextData ctx)
			{
				this.m_Ctx = ctx;
				this.m_Index = -1;
			}

			// Token: 0x17000221 RID: 545
			// (get) Token: 0x0600117D RID: 4477 RVA: 0x0003F6CA File Offset: 0x0003D8CA
			public readonly ref NativePassData Current
			{
				get
				{
					return this.m_Ctx.nativePassData.ElementAt(this.m_Index);
				}
			}

			// Token: 0x0600117E RID: 4478 RVA: 0x0003F6E4 File Offset: 0x0003D8E4
			public bool MoveNext()
			{
				bool inRange;
				do
				{
					this.m_Index++;
					inRange = this.m_Index < this.m_Ctx.nativePassData.Length;
				}
				while (inRange && !this.m_Ctx.nativePassData.ElementAt(this.m_Index).IsValid());
				return inRange;
			}

			// Token: 0x0600117F RID: 4479 RVA: 0x0003F739 File Offset: 0x0003D939
			public CompilerContextData.NativePassIterator GetEnumerator()
			{
				return this;
			}

			// Token: 0x04000B4C RID: 2892
			private readonly CompilerContextData m_Ctx;

			// Token: 0x04000B4D RID: 2893
			private int m_Index;
		}
	}
}
