using System;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x0200029C RID: 668
	internal class ResourcesData
	{
		// Token: 0x060011D4 RID: 4564 RVA: 0x0004405C File Offset: 0x0004225C
		public ResourcesData()
		{
			this.unversionedData = new NativeList<ResourceUnversionedData>[3];
			this.versionedData = new NativeList<ResourceVersionedData>[3];
			this.readerData = new NativeList<ResourceReaderData>[3];
			this.resourceNames = new DynamicArray<Name>[3];
			for (int t = 0; t < 3; t++)
			{
				this.versionedData[t] = new NativeList<ResourceVersionedData>(0, AllocatorManager.Persistent);
				this.unversionedData[t] = new NativeList<ResourceUnversionedData>(0, AllocatorManager.Persistent);
				this.readerData[t] = new NativeList<ResourceReaderData>(0, AllocatorManager.Persistent);
				this.resourceNames[t] = new DynamicArray<Name>(0);
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00044100 File Offset: 0x00042300
		public void Clear()
		{
			for (int t = 0; t < 3; t++)
			{
				this.unversionedData[t].Clear();
				this.versionedData[t].Clear();
				this.readerData[t].Clear();
				this.resourceNames[t].Clear();
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004415C File Offset: 0x0004235C
		public unsafe void Initialize(RenderGraphResourceRegistry resources)
		{
			for (int t = 0; t < 3; t++)
			{
				RenderGraphResourceType resourceType = (RenderGraphResourceType)t;
				int numResources = resources.GetResourceCount(resourceType);
				this.unversionedData[t].Resize(numResources, NativeArrayOptions.UninitializedMemory);
				this.resourceNames[t].Resize(numResources, true);
				if (numResources > 0)
				{
					ResourceUnversionedData nullResource = default(ResourceUnversionedData);
					nullResource.InitializeNullResource();
					this.unversionedData[t][0] = nullResource;
					*this.resourceNames[t][0] = new Name("", false);
				}
				for (int r = 1; r < numResources; r++)
				{
					ResourceHandle h = new ResourceHandle(r, resourceType, false);
					IRenderGraphResource rll = resources.GetResourceLowLevel(in h);
					*this.resourceNames[t][r] = new Name(rll.GetName(), false);
					switch (t)
					{
					case 0:
					{
						RenderTargetInfo info;
						resources.GetRenderTargetInfo(in h, out info);
						ref TextureDesc desc = ref (rll as TextureResource).desc;
						bool isResourceShared = resources.IsRenderGraphResourceShared(in h);
						this.unversionedData[t][r] = new ResourceUnversionedData(rll, ref info, ref desc, isResourceShared);
						break;
					}
					case 1:
					{
						ref BufferDesc desc2 = ref (rll as BufferResource).desc;
						bool isResourceShared2 = resources.IsRenderGraphResourceShared(in h);
						this.unversionedData[t][r] = new ResourceUnversionedData(rll, ref desc2, isResourceShared2);
						break;
					}
					case 2:
					{
						ref RayTracingAccelerationStructureDesc desc3 = ref (rll as RayTracingAccelerationStructureResource).desc;
						bool isResourceShared3 = resources.IsRenderGraphResourceShared(in h);
						this.unversionedData[t][r] = new ResourceUnversionedData(rll, ref desc3, isResourceShared3);
						break;
					}
					default:
						throw new Exception("Unsupported resource type: " + t.ToString());
					}
				}
				this.versionedData[t].Resize(20 * numResources, NativeArrayOptions.ClearMemory);
				this.readerData[t].Resize(2000 * numResources, NativeArrayOptions.ClearMemory);
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004434B File Offset: 0x0004254B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Index(ResourceHandle h)
		{
			return h.index * 20 + h.version;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004435F File Offset: 0x0004255F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexReader(ResourceHandle h, int readerID)
		{
			return (h.index * 20 + h.version) * 100 + readerID;
		}

		// Token: 0x17000224 RID: 548
		public ref ResourceVersionedData this[ResourceHandle h]
		{
			get
			{
				return this.versionedData[h.iType].ElementAt(ResourcesData.Index(h));
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00044398 File Offset: 0x00042598
		public void Dispose()
		{
			for (int t = 0; t < 3; t++)
			{
				this.versionedData[t].Dispose();
				this.unversionedData[t].Dispose();
				this.readerData[t].Dispose();
			}
		}

		// Token: 0x04000BF5 RID: 3061
		public NativeList<ResourceUnversionedData>[] unversionedData;

		// Token: 0x04000BF6 RID: 3062
		public NativeList<ResourceVersionedData>[] versionedData;

		// Token: 0x04000BF7 RID: 3063
		public NativeList<ResourceReaderData>[] readerData;

		// Token: 0x04000BF8 RID: 3064
		public const int MaxVersions = 20;

		// Token: 0x04000BF9 RID: 3065
		public const int MaxReaders = 100;

		// Token: 0x04000BFA RID: 3066
		public DynamicArray<Name>[] resourceNames;
	}
}
