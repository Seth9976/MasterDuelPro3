using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Rendering;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000509 RID: 1289
	internal class UIRenderDevice : IDisposable
	{
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x000856D7 File Offset: 0x000838D7
		internal static uint maxVerticesPerPage
		{
			get
			{
				return 65535U;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000856DE File Offset: 0x000838DE
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x000856E6 File Offset: 0x000838E6
		internal bool breakBatches { get; set; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x000856EF File Offset: 0x000838EF
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x000856F7 File Offset: 0x000838F7
		internal bool isFlat { get; set; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x00085700 File Offset: 0x00083900
		// (set) Token: 0x060023F2 RID: 9202 RVA: 0x00085708 File Offset: 0x00083908
		internal bool drawsInCameras { get; set; }

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x00085711 File Offset: 0x00083911
		internal uint frameIndex
		{
			get
			{
				return this.m_FrameIndex;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x00085719 File Offset: 0x00083919
		internal List<CommandList>[] commandLists
		{
			get
			{
				return this.m_CommandLists;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x00085721 File Offset: 0x00083921
		internal List<CommandList> currentFrameCommandLists
		{
			get
			{
				return (this.m_CommandLists == null) ? null : this.m_CommandLists[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_CommandLists.Length))];
			}
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x00085748 File Offset: 0x00083948
		static UIRenderDevice()
		{
			Utility.EngineUpdate += UIRenderDevice.OnEngineUpdateGlobal;
			Utility.FlushPendingResources += UIRenderDevice.OnFlushPendingResources;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000857F3 File Offset: 0x000839F3
		public UIRenderDevice(uint initialVertexCapacity = 0U, uint initialIndexCapacity = 0U)
			: this(initialVertexCapacity, initialIndexCapacity, false)
		{
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00085800 File Offset: 0x00083A00
		protected UIRenderDevice(uint initialVertexCapacity, uint initialIndexCapacity, bool mockDevice)
		{
			this.m_MockDevice = mockDevice;
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			Debug.Assert(true);
			bool flag = UIRenderDevice.m_ActiveDeviceCount++ == 0;
			if (flag)
			{
				bool flag2 = !UIRenderDevice.m_SubscribedToNotifications && !this.m_MockDevice;
				if (flag2)
				{
					Utility.NotifyOfUIREvents(true);
					UIRenderDevice.m_SubscribedToNotifications = true;
				}
			}
			this.m_NextPageVertexCount = Math.Max(initialVertexCapacity / 2U, 2048U);
			this.m_LargeMeshVertexCount = this.m_NextPageVertexCount;
			this.m_IndexToVertexCountRatio = initialIndexCapacity / initialVertexCapacity;
			this.m_IndexToVertexCountRatio = Mathf.Max(this.m_IndexToVertexCountRatio, 2f);
			this.m_DeferredFrees = new List<List<UIRenderDevice.AllocToFree>>(4);
			this.m_Updates = new List<List<UIRenderDevice.AllocToUpdate>>(4);
			int i = 0;
			while ((long)i < 4L)
			{
				this.m_DeferredFrees.Add(new List<UIRenderDevice.AllocToFree>());
				this.m_Updates.Add(new List<UIRenderDevice.AllocToUpdate>());
				i++;
			}
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x00085970 File Offset: 0x00083B70
		private void InitVertexDeclaration()
		{
			VertexAttributeDescriptor[] vertexDecl = new VertexAttributeDescriptor[]
			{
				new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord2, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord3, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord4, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord5, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord6, VertexAttributeFormat.Float32, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord7, VertexAttributeFormat.Float32, 1, 0)
			};
			this.m_VertexDecl = Utility.GetVertexDeclaration(vertexDecl);
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00085A38 File Offset: 0x00083C38
		private void CompleteCreation()
		{
			bool flag = this.m_MockDevice || this.fullyCreated;
			if (!flag)
			{
				this.InitVertexDeclaration();
				this.m_Fences = new uint[4];
				this.m_ConstantProps = new MaterialPropertyBlock();
				this.m_BatchProps = new MaterialPropertyBlock();
				this.m_DefaultStencilState = Utility.CreateStencilState(new StencilState
				{
					enabled = this.isFlat,
					readMask = byte.MaxValue,
					writeMask = byte.MaxValue,
					compareFunctionFront = CompareFunction.Equal,
					passOperationFront = StencilOp.Keep,
					failOperationFront = StencilOp.Keep,
					zFailOperationFront = StencilOp.IncrementSaturate,
					compareFunctionBack = CompareFunction.Less,
					passOperationBack = StencilOp.Keep,
					failOperationBack = StencilOp.Keep,
					zFailOperationBack = StencilOp.DecrementSaturate
				});
				this.m_CommandLists = new List<CommandList>[4];
				int i = 0;
				while ((long)i < 4L)
				{
					this.m_CommandLists[i] = new List<CommandList>();
					i++;
				}
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x00085B38 File Offset: 0x00083D38
		private bool fullyCreated
		{
			get
			{
				return this.m_Fences != null;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x00085B53 File Offset: 0x00083D53
		// (set) Token: 0x060023FD RID: 9213 RVA: 0x00085B5B File Offset: 0x00083D5B
		private protected bool disposed { protected get; private set; }

		// Token: 0x060023FE RID: 9214 RVA: 0x00085B64 File Offset: 0x00083D64
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x00085B78 File Offset: 0x00083D78
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				UIRenderDevice.m_ActiveDeviceCount--;
				if (disposing)
				{
					UIRenderDevice.DeviceToFree free = new UIRenderDevice.DeviceToFree
					{
						handle = (this.m_MockDevice ? 0U : Utility.InsertCPUFence()),
						page = this.m_FirstPage,
						commandLists = this.m_CommandLists
					};
					bool flag = free.handle == 0U;
					if (flag)
					{
						free.Dispose();
					}
					else
					{
						UIRenderDevice.m_DeviceFreeQueue.AddLast(free);
						bool synchronousFree = UIRenderDevice.m_SynchronousFree;
						if (synchronousFree)
						{
							UIRenderDevice.ProcessDeviceFreeQueue();
						}
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00085C28 File Offset: 0x00083E28
		public MeshHandle Allocate(uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			MeshHandle meshHandle = this.m_MeshHandles.Get();
			meshHandle.triangleCount = indexCount / 3U;
			this.Allocate(meshHandle, vertexCount, indexCount, out vertexData, out indexData, false);
			indexOffset = (ushort)meshHandle.allocVerts.start;
			return meshHandle;
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x00085C70 File Offset: 0x00083E70
		public void Update(MeshHandle mesh, uint vertexCount, out NativeSlice<Vertex> vertexData)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, (int)vertexCount);
			}
			else
			{
				uint oldIndexOffset = mesh.allocVerts.start;
				NativeSlice<ushort> oldIndexData = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)mesh.allocIndices.size);
				NativeSlice<ushort> indexData;
				ushort indexOffset;
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, mesh.allocIndices.size, out vertexData, out indexData, out indexOffset, out allocToUpdate, false);
				int indexCount = (int)mesh.allocIndices.size;
				int indexDifference = (int)((uint)indexOffset - oldIndexOffset);
				for (int i = 0; i < indexCount; i++)
				{
					indexData[i] = (ushort)((int)oldIndexData[i] + indexDifference);
				}
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00085D6C File Offset: 0x00083F6C
		public void Update(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			Debug.Assert(mesh.allocIndices.size >= indexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, (int)vertexCount);
				indexData = mesh.allocPage.indices.cpuData.Slice((int)mesh.allocIndices.start, (int)indexCount);
				indexOffset = (ushort)mesh.allocVerts.start;
				this.UpdateCopyBackIndices(mesh, true);
			}
			else
			{
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, indexCount, out vertexData, out indexData, out indexOffset, out allocToUpdate, true);
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00085E38 File Offset: 0x00084038
		private void UpdateCopyBackIndices(MeshHandle mesh, bool copyBackIndices)
		{
			bool flag = mesh.updateAllocID == 0U;
			if (!flag)
			{
				int activeUpdateIndex = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> updates = this.ActiveUpdatesForMeshHandle(mesh);
				UIRenderDevice.AllocToUpdate update = updates[activeUpdateIndex];
				update.copyBackIndices = true;
				updates[activeUpdateIndex] = update;
			}
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x00085E80 File Offset: 0x00084080
		internal List<UIRenderDevice.AllocToUpdate> ActiveUpdatesForMeshHandle(MeshHandle mesh)
		{
			return this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x00085EB0 File Offset: 0x000840B0
		private bool TryAllocFromPage(Page page, uint vertexCount, uint indexCount, ref Alloc va, ref Alloc ia, bool shortLived)
		{
			va = page.vertices.allocator.Allocate(vertexCount, shortLived);
			bool flag = va.size > 0U;
			if (flag)
			{
				ia = page.indices.allocator.Allocate(indexCount, shortLived);
				bool flag2 = ia.size > 0U;
				if (flag2)
				{
					return true;
				}
				page.vertices.allocator.Free(va);
				va.size = 0U;
			}
			return false;
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00085F3C File Offset: 0x0008413C
		private void Allocate(MeshHandle meshHandle, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, bool shortLived)
		{
			Page page = null;
			Alloc va = default(Alloc);
			Alloc ia = default(Alloc);
			bool flag = vertexCount <= this.m_LargeMeshVertexCount;
			if (flag)
			{
				bool flag2 = this.m_FirstPage != null;
				if (flag2)
				{
					page = this.m_FirstPage;
					for (;;)
					{
						bool flag3 = this.TryAllocFromPage(page, vertexCount, indexCount, ref va, ref ia, shortLived) || page.next == null;
						if (flag3)
						{
							break;
						}
						page = page.next;
					}
				}
				else
				{
					this.CompleteCreation();
				}
				bool flag4 = ia.size == 0U;
				if (flag4)
				{
					this.m_NextPageVertexCount <<= 1;
					this.m_NextPageVertexCount = Math.Max(this.m_NextPageVertexCount, vertexCount * 2U);
					this.m_NextPageVertexCount = Math.Min(this.m_NextPageVertexCount, UIRenderDevice.maxVerticesPerPage);
					uint newPageIndexCount = (uint)(this.m_NextPageVertexCount * this.m_IndexToVertexCountRatio + 0.5f);
					newPageIndexCount = Math.Max(newPageIndexCount, indexCount * 2U);
					Debug.Assert(((page != null) ? page.next : null) == null);
					page = new Page(this.m_NextPageVertexCount, newPageIndexCount, 4U, this.m_MockDevice);
					page.next = this.m_FirstPage;
					this.m_FirstPage = page;
					va = page.vertices.allocator.Allocate(vertexCount, shortLived);
					ia = page.indices.allocator.Allocate(indexCount, shortLived);
					Debug.Assert(va.size > 0U);
					Debug.Assert(ia.size > 0U);
				}
			}
			else
			{
				this.CompleteCreation();
				Page current = this.m_FirstPage;
				Page lastPage = this.m_FirstPage;
				int bestFitExtraVertices = int.MaxValue;
				while (current != null)
				{
					int extraVertices = current.vertices.cpuData.Length - (int)vertexCount;
					int extraIndices = current.indices.cpuData.Length - (int)indexCount;
					bool flag5 = current.isEmpty && extraVertices >= 0 && extraIndices >= 0 && extraVertices < bestFitExtraVertices;
					if (flag5)
					{
						page = current;
						bestFitExtraVertices = extraVertices;
					}
					lastPage = current;
					current = current.next;
				}
				bool flag6 = page == null;
				if (flag6)
				{
					uint pageVertexCount = ((vertexCount > UIRenderDevice.maxVerticesPerPage) ? 2U : vertexCount);
					Debug.Assert(vertexCount <= UIRenderDevice.maxVerticesPerPage, "Requested Vertex count is above the limit. Alloc will fail.");
					page = new Page(pageVertexCount, indexCount, 4U, this.m_MockDevice);
					bool flag7 = lastPage != null;
					if (flag7)
					{
						lastPage.next = page;
					}
					else
					{
						this.m_FirstPage = page;
					}
				}
				va = page.vertices.allocator.Allocate(vertexCount, shortLived);
				ia = page.indices.allocator.Allocate(indexCount, shortLived);
			}
			Debug.Assert(va.size == vertexCount, "Vertices allocated != Vertices requested");
			Debug.Assert(ia.size == indexCount, "Indices allocated != Indices requested");
			bool flag8 = va.size != vertexCount || ia.size != indexCount;
			if (flag8)
			{
				bool flag9 = va.handle != null;
				if (flag9)
				{
					page.vertices.allocator.Free(va);
				}
				bool flag10 = ia.handle != null;
				if (flag10)
				{
					page.vertices.allocator.Free(ia);
				}
				ia = default(Alloc);
				va = default(Alloc);
			}
			page.vertices.RegisterUpdate(va.start, va.size);
			page.indices.RegisterUpdate(ia.start, ia.size);
			vertexData = new NativeSlice<Vertex>(page.vertices.cpuData, (int)va.start, (int)va.size);
			indexData = new NativeSlice<ushort>(page.indices.cpuData, (int)ia.start, (int)ia.size);
			meshHandle.allocPage = page;
			meshHandle.allocVerts = va;
			meshHandle.allocIndices = ia;
			meshHandle.allocTime = this.m_FrameIndex;
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x00086304 File Offset: 0x00084504
		private void UpdateAfterGPUUsedData(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset, out UIRenderDevice.AllocToUpdate allocToUpdate, bool copyBackIndices)
		{
			UIRenderDevice.AllocToUpdate allocToUpdate2 = default(UIRenderDevice.AllocToUpdate);
			uint nextUpdateID = this.m_NextUpdateID;
			this.m_NextUpdateID = nextUpdateID + 1U;
			allocToUpdate2.id = nextUpdateID;
			allocToUpdate2.allocTime = this.m_FrameIndex;
			allocToUpdate2.meshHandle = mesh;
			allocToUpdate2.copyBackIndices = copyBackIndices;
			allocToUpdate = allocToUpdate2;
			Debug.Assert(this.m_NextUpdateID > 0U);
			bool flag = mesh.updateAllocID == 0U;
			if (flag)
			{
				allocToUpdate.permAllocVerts = mesh.allocVerts;
				allocToUpdate.permAllocIndices = mesh.allocIndices;
				allocToUpdate.permPage = mesh.allocPage;
			}
			else
			{
				int activeUpdateIndex = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> updates = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate oldUpdate = updates[activeUpdateIndex];
				Debug.Assert(oldUpdate.id == mesh.updateAllocID);
				allocToUpdate.copyBackIndices |= oldUpdate.copyBackIndices;
				allocToUpdate.permAllocVerts = oldUpdate.permAllocVerts;
				allocToUpdate.permAllocIndices = oldUpdate.permAllocIndices;
				allocToUpdate.permPage = oldUpdate.permPage;
				oldUpdate.allocTime = uint.MaxValue;
				updates[activeUpdateIndex] = oldUpdate;
				List<UIRenderDevice.AllocToFree> queueToFree = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				queueToFree.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				queueToFree.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
			}
			bool flag2 = this.TryAllocFromPage(mesh.allocPage, vertexCount, indexCount, ref mesh.allocVerts, ref mesh.allocIndices, true);
			if (flag2)
			{
				mesh.allocPage.vertices.RegisterUpdate(mesh.allocVerts.start, mesh.allocVerts.size);
				mesh.allocPage.indices.RegisterUpdate(mesh.allocIndices.start, mesh.allocIndices.size);
			}
			else
			{
				this.Allocate(mesh, vertexCount, indexCount, out vertexData, out indexData, true);
			}
			mesh.triangleCount = indexCount / 3U;
			mesh.updateAllocID = allocToUpdate.id;
			mesh.allocTime = allocToUpdate.allocTime;
			this.m_Updates[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Updates.Count))].Add(allocToUpdate);
			vertexData = new NativeSlice<Vertex>(mesh.allocPage.vertices.cpuData, (int)mesh.allocVerts.start, (int)vertexCount);
			indexData = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)indexCount);
			indexOffset = (ushort)mesh.allocVerts.start;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000865F4 File Offset: 0x000847F4
		public void Free(MeshHandle mesh)
		{
			bool flag = mesh.updateAllocID > 0U;
			if (flag)
			{
				int activeUpdateIndex = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> updates = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate oldUpdate = updates[activeUpdateIndex];
				Debug.Assert(oldUpdate.id == mesh.updateAllocID);
				List<UIRenderDevice.AllocToFree> queueToFree = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				queueToFree.Add(new UIRenderDevice.AllocToFree
				{
					alloc = oldUpdate.permAllocVerts,
					page = oldUpdate.permPage,
					vertices = true
				});
				queueToFree.Add(new UIRenderDevice.AllocToFree
				{
					alloc = oldUpdate.permAllocIndices,
					page = oldUpdate.permPage,
					vertices = false
				});
				queueToFree.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				queueToFree.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
				oldUpdate.allocTime = uint.MaxValue;
				updates[activeUpdateIndex] = oldUpdate;
			}
			else
			{
				bool flag2 = mesh.allocTime != this.m_FrameIndex;
				if (flag2)
				{
					int queueIndex = (int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count);
					this.m_DeferredFrees[queueIndex].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocVerts,
						page = mesh.allocPage,
						vertices = true
					});
					this.m_DeferredFrees[queueIndex].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocIndices,
						page = mesh.allocPage,
						vertices = false
					});
				}
				else
				{
					mesh.allocPage.vertices.allocator.Free(mesh.allocVerts);
					mesh.allocPage.indices.allocator.Free(mesh.allocIndices);
				}
			}
			mesh.allocVerts = default(Alloc);
			mesh.allocIndices = default(Alloc);
			mesh.allocPage = null;
			mesh.updateAllocID = 0U;
			this.m_MeshHandles.Return(mesh);
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x0008687C File Offset: 0x00084A7C
		public void OnFrameRenderingBegin()
		{
			this.AdvanceFrame();
			this.m_DrawStats = default(UIRenderDevice.DrawStatistics);
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				page.vertices.SendUpdates();
				page.indices.SendUpdates();
			}
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000868E0 File Offset: 0x00084AE0
		internal unsafe static NativeSlice<T> PtrToSlice<T>(void* p, int count) where T : struct
		{
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>(p, UnsafeUtility.SizeOf<T>(), count);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x00086900 File Offset: 0x00084B00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ApplyDrawCommandState(RenderChainCommand cmd, int textureSlot, Material newMat, bool newMatDiffers, ref UIRenderDevice.EvaluationState st)
		{
			if (newMatDiffers)
			{
				st.curState.material = newMat;
				st.mustApplyMaterial = true;
			}
			st.curPage = cmd.mesh.allocPage;
			bool flag = cmd.state.texture != TextureId.invalid;
			if (flag)
			{
				bool flag2 = textureSlot < 0;
				if (flag2)
				{
					textureSlot = this.m_TextureSlotManager.FindOldestSlot();
					this.m_TextureSlotManager.Bind(cmd.state.texture, cmd.state.sdfScale, cmd.state.sharpness, textureSlot, st.batchProps, st.activeCommandList);
					st.mustApplyBatchProps = true;
				}
				else
				{
					this.m_TextureSlotManager.MarkUsed(textureSlot);
				}
			}
			bool flag3 = cmd.state.stencilRef != st.curState.stencilRef;
			if (flag3)
			{
				st.curState.stencilRef = cmd.state.stencilRef;
				st.mustApplyStencil = true;
			}
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x00086A04 File Offset: 0x00084C04
		private void ApplyBatchState(ref UIRenderDevice.EvaluationState st)
		{
			bool flag = !this.m_MockDevice;
			if (flag)
			{
				bool mustApplyMaterial = st.mustApplyMaterial;
				if (mustApplyMaterial)
				{
					bool drawsInCameras = this.drawsInCameras;
					if (drawsInCameras)
					{
						Debug.LogError("Attempted to change material when it is not allowed to do so.");
						return;
					}
					this.m_DrawStats.materialSetCount = this.m_DrawStats.materialSetCount + 1U;
					st.curState.material.SetPass(0);
					bool flag2 = st.constantProps != null;
					if (flag2)
					{
						Utility.SetPropertyBlock(st.constantProps);
					}
					st.mustApplyBatchProps = true;
					st.mustApplyStencil = true;
				}
				bool mustApplyBatchProps = st.mustApplyBatchProps;
				if (mustApplyBatchProps)
				{
					bool flag3 = st.activeCommandList == null;
					if (flag3)
					{
						Utility.SetPropertyBlock(st.batchProps);
					}
					else
					{
						st.activeCommandList.ApplyBatchProps();
					}
				}
				bool mustApplyStencil = st.mustApplyStencil;
				if (mustApplyStencil)
				{
					this.m_DrawStats.stencilRefChanges = this.m_DrawStats.stencilRefChanges + 1U;
					bool flag4 = st.activeCommandList == null;
					if (flag4)
					{
						Utility.SetStencilState(this.m_DefaultStencilState, st.curState.stencilRef);
					}
				}
			}
			st.mustApplyMaterial = false;
			st.mustApplyBatchProps = false;
			st.mustApplyStencil = false;
			this.m_TextureSlotManager.StartNewBatch();
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x00086B30 File Offset: 0x00084D30
		public unsafe void EvaluateChain(RenderChainCommand head, Material initialMat, Material defaultMat, Texture gradientSettings, Texture shaderInfo, float pixelsPerPoint, ref Exception immediateException)
		{
			Utility.ProfileDrawChainBegin();
			bool doBreakBatches = this.breakBatches;
			int rangesCount = 1024;
			DrawBufferRange* ranges;
			checked
			{
				ranges = stackalloc DrawBufferRange[unchecked((UIntPtr)rangesCount) * (UIntPtr)sizeof(DrawBufferRange)];
			}
			int rangesCountMinus = rangesCount - 1;
			int rangesStart = 0;
			int rangesReady = 0;
			DrawBufferRange curDrawRange = default(DrawBufferRange);
			int curDrawIndex = -1;
			int disableCounter = 0;
			this.currentFrameCommandListCount = 0;
			UIRenderDevice.EvaluationState st = new UIRenderDevice.EvaluationState
			{
				constantProps = this.m_ConstantProps,
				batchProps = this.m_BatchProps,
				defaultMat = defaultMat,
				curState = new State
				{
					material = initialMat
				},
				mustApplyBatchProps = true,
				mustApplyStencil = true
			};
			DrawParams drawParams = this.m_DrawParams;
			drawParams.Reset();
			bool flag = !this.drawsInCameras;
			if (flag)
			{
				drawParams.renderTexture.Add(RenderTexture.active);
			}
			this.m_TextureSlotManager.Reset();
			bool fullyCreated = this.fullyCreated;
			if (fullyCreated)
			{
				st.batchProps.Clear();
				bool flag2 = gradientSettings != null;
				if (flag2)
				{
					st.constantProps.SetTexture(UIRenderDevice.s_GradientSettingsTexID, gradientSettings);
				}
				bool flag3 = shaderInfo != null;
				if (flag3)
				{
					st.constantProps.SetTexture(UIRenderDevice.s_ShaderInfoTexID, shaderInfo);
				}
				bool flag4 = !this.drawsInCameras;
				if (flag4)
				{
					Utility.SetPropertyBlock(st.constantProps);
				}
			}
			while (head != null)
			{
				bool flag5 = head.type == CommandType.BeginDisable;
				if (flag5)
				{
					this.m_DrawStats.commandCount = this.m_DrawStats.commandCount + 1U;
					disableCounter++;
					head = head.next;
				}
				else
				{
					bool flag6 = head.type == CommandType.EndDisable;
					if (flag6)
					{
						this.m_DrawStats.commandCount = this.m_DrawStats.commandCount + 1U;
						disableCounter--;
						head = head.next;
					}
					else
					{
						bool flag7 = disableCounter > 0;
						if (flag7)
						{
							this.m_DrawStats.skippedCommandCount = this.m_DrawStats.skippedCommandCount + 1U;
							head = head.next;
						}
						else
						{
							this.m_DrawStats.drawCommandCount = this.m_DrawStats.drawCommandCount + ((head.type == CommandType.Draw) ? 1U : 0U);
							bool isLastRange = curDrawRange.indexCount > 0 && rangesReady == rangesCount - 1;
							bool stashRange = false;
							bool kickRanges = false;
							bool mustApplyCmdState = false;
							int textureSlot = -1;
							Material newMat = null;
							bool newMatDiffers = false;
							bool flag8 = head.type == CommandType.Draw;
							if (flag8)
							{
								newMat = ((head.state.material != null) ? head.state.material : defaultMat);
								bool flag9 = newMat != st.curState.material;
								if (flag9)
								{
									mustApplyCmdState = true;
									newMatDiffers = true;
									stashRange = true;
									kickRanges = true;
								}
								bool flag10 = head.mesh.allocPage != st.curPage;
								if (flag10)
								{
									mustApplyCmdState = true;
									stashRange = true;
									kickRanges = true;
								}
								else
								{
									bool flag11 = (long)curDrawIndex != (long)((ulong)head.mesh.allocIndices.start + (ulong)((long)head.indexOffset));
									if (flag11)
									{
										stashRange = true;
									}
								}
								bool flag12 = head.state.texture != TextureId.invalid;
								if (flag12)
								{
									mustApplyCmdState = true;
									textureSlot = this.m_TextureSlotManager.IndexOf(head.state.texture);
									bool flag13 = textureSlot < 0 && this.m_TextureSlotManager.FreeSlots < 1;
									if (flag13)
									{
										stashRange = true;
										kickRanges = true;
									}
								}
								bool flag14 = head.state.stencilRef != st.curState.stencilRef;
								if (flag14)
								{
									mustApplyCmdState = true;
									stashRange = true;
									kickRanges = true;
								}
								bool flag15 = stashRange && isLastRange;
								if (flag15)
								{
									kickRanges = true;
								}
							}
							else
							{
								stashRange = true;
								kickRanges = true;
							}
							bool flag16 = doBreakBatches;
							if (flag16)
							{
								stashRange = true;
								kickRanges = true;
							}
							bool flag17 = stashRange;
							if (flag17)
							{
								bool flag18 = curDrawRange.indexCount > 0;
								if (flag18)
								{
									int wrapAroundIndex = (rangesStart + rangesReady++) & rangesCountMinus;
									ranges[wrapAroundIndex] = curDrawRange;
									Debug.Assert(rangesReady < rangesCount || kickRanges);
									curDrawRange = default(DrawBufferRange);
									this.m_DrawStats.drawRangeCount = this.m_DrawStats.drawRangeCount + 1U;
								}
								bool flag19 = head.type == CommandType.Draw;
								if (flag19)
								{
									curDrawRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset);
									curDrawRange.indexCount = head.indexCount;
									curDrawRange.vertsReferenced = (int)head.mesh.allocVerts.size;
									curDrawRange.minIndexVal = (int)head.mesh.allocVerts.start;
									curDrawIndex = curDrawRange.firstIndex + head.indexCount;
									this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
								}
								bool flag20 = kickRanges;
								if (flag20)
								{
									bool flag21 = rangesReady > 0;
									if (flag21)
									{
										this.ApplyBatchState(ref st);
										this.KickRanges(ranges, ref rangesReady, ref rangesStart, rangesCount, st.curPage, st.activeCommandList);
									}
									bool flag22 = this.fullyCreated && head.type == CommandType.CutRenderChain;
									if (flag22)
									{
										bool flag23 = this.currentFrameCommandListCount < this.currentFrameCommandLists.Count;
										CommandList cmdList;
										if (flag23)
										{
											cmdList = this.currentFrameCommandLists[this.currentFrameCommandListCount];
											cmdList.Reset(head.owner);
										}
										else
										{
											cmdList = new CommandList(head.owner, this.m_VertexDecl, this.m_DefaultStencilState);
											this.currentFrameCommandLists.Add(cmdList);
										}
										this.currentFrameCommandListCount++;
										st.activeCommandList = cmdList;
										st.constantProps = st.activeCommandList.constantProps;
										st.batchProps = st.activeCommandList.batchProps;
										st.batchProps.Clear();
										bool flag24 = gradientSettings != null;
										if (flag24)
										{
											st.constantProps.SetTexture(UIRenderDevice.s_GradientSettingsTexID, gradientSettings);
										}
										bool flag25 = shaderInfo != null;
										if (flag25)
										{
											st.constantProps.SetTexture(UIRenderDevice.s_ShaderInfoTexID, shaderInfo);
										}
										this.m_TextureSlotManager.Reset();
									}
									bool flag26 = head.type > CommandType.Draw;
									if (flag26)
									{
										bool flag27 = !this.m_MockDevice;
										if (flag27)
										{
											head.ExecuteNonDrawMesh(drawParams, pixelsPerPoint, ref immediateException);
										}
										bool flag28 = head.type == CommandType.Immediate || head.type == CommandType.ImmediateCull || head.type == CommandType.BlitToPreviousRT || head.type == CommandType.PushRenderTexture || head.type == CommandType.PopDefaultMaterial || head.type == CommandType.PushDefaultMaterial;
										if (flag28)
										{
											st.curState.material = null;
											st.mustApplyMaterial = false;
											this.m_DrawStats.immediateDraws = this.m_DrawStats.immediateDraws + 1U;
											bool flag29 = head.type == CommandType.PopDefaultMaterial;
											if (flag29)
											{
												int index = drawParams.defaultMaterial.Count - 1;
												defaultMat = drawParams.defaultMaterial[index];
												drawParams.defaultMaterial.RemoveAt(index);
											}
											bool flag30 = head.type == CommandType.PushDefaultMaterial;
											if (flag30)
											{
												drawParams.defaultMaterial.Add(defaultMat);
												defaultMat = head.state.material;
											}
										}
									}
								}
								bool flag31 = head.type == CommandType.Draw && mustApplyCmdState;
								if (flag31)
								{
									this.ApplyDrawCommandState(head, textureSlot, newMat, newMatDiffers, ref st);
								}
								head = head.next;
							}
							else
							{
								bool flag32 = curDrawRange.indexCount == 0;
								if (flag32)
								{
									curDrawIndex = (curDrawRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset));
								}
								curDrawRange.indexCount += head.indexCount;
								int prevFirstVertex = curDrawRange.minIndexVal;
								int curFirstVertex = (int)head.mesh.allocVerts.start;
								int prevLastVertex = curDrawRange.minIndexVal + curDrawRange.vertsReferenced;
								int curLastVertex = (int)(head.mesh.allocVerts.start + head.mesh.allocVerts.size);
								curDrawRange.minIndexVal = Mathf.Min(prevFirstVertex, curFirstVertex);
								curDrawRange.vertsReferenced = Mathf.Max(prevLastVertex, curLastVertex) - curDrawRange.minIndexVal;
								curDrawIndex += head.indexCount;
								this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
								bool flag33 = mustApplyCmdState;
								if (flag33)
								{
									this.ApplyDrawCommandState(head, textureSlot, newMat, newMatDiffers, ref st);
								}
								head = head.next;
							}
						}
					}
				}
			}
			bool flag34 = curDrawRange.indexCount > 0;
			if (flag34)
			{
				int wrapAroundIndex2 = (rangesStart + rangesReady++) & rangesCountMinus;
				ranges[wrapAroundIndex2] = curDrawRange;
			}
			bool flag35 = rangesReady > 0;
			if (flag35)
			{
				this.ApplyBatchState(ref st);
				this.KickRanges(ranges, ref rangesReady, ref rangesStart, rangesCount, st.curPage, st.activeCommandList);
			}
			Debug.Assert(disableCounter == 0, "Rendering disabled counter is not 0, indicating a mismatch of commands");
			this.UpdateFenceValue();
			Utility.ProfileDrawChainEnd();
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000873F0 File Offset: 0x000855F0
		private unsafe void UpdateFenceValue()
		{
			bool flag = this.m_Fences != null;
			if (flag)
			{
				uint newFenceVal = Utility.InsertCPUFence();
				fixed (uint* ptr = &this.m_Fences[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length))])
				{
					uint* fence = ptr;
					bool flag3;
					do
					{
						uint curFenceVal = *fence;
						bool flag2 = newFenceVal - curFenceVal <= 0U;
						if (flag2)
						{
							break;
						}
						int cmpOldVal = Interlocked.CompareExchange(ref *(int*)fence, (int)newFenceVal, (int)curFenceVal);
						flag3 = (long)cmpOldVal == (long)((ulong)curFenceVal);
					}
					while (!flag3);
				}
			}
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00087470 File Offset: 0x00085670
		private unsafe void KickRanges(DrawBufferRange* ranges, ref int rangesReady, ref int rangesStart, int rangesCount, Page curPage, CommandList commandList)
		{
			Debug.Assert(rangesReady > 0);
			bool flag = rangesStart + rangesReady <= rangesCount;
			if (flag)
			{
				bool flag2 = !this.m_MockDevice;
				if (flag2)
				{
					this.DrawRanges(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), rangesReady), commandList);
				}
				this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 1U;
			}
			else
			{
				int firstRangeCount = rangesCount - rangesStart;
				int secondRangeCount = rangesReady - firstRangeCount;
				bool flag3 = !this.m_MockDevice;
				if (flag3)
				{
					this.DrawRanges(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), firstRangeCount), commandList);
					this.DrawRanges(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)ranges, secondRangeCount), commandList);
				}
				this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 2U;
			}
			rangesStart = (rangesStart + rangesReady) & (rangesCount - 1);
			rangesReady = 0;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x0008758C File Offset: 0x0008578C
		private unsafe void DrawRanges(Utility.GPUBuffer<ushort> ib, Utility.GPUBuffer<Vertex> vb, NativeSlice<DrawBufferRange> ranges, CommandList commandList)
		{
			bool flag = commandList != null;
			checked
			{
				if (flag)
				{
					commandList.DrawRanges(ib, vb, ranges);
				}
				else
				{
					bool flag2 = !this.drawsInCameras;
					if (flag2)
					{
						IntPtr* vStream = stackalloc IntPtr[unchecked((UIntPtr)1) * (UIntPtr)sizeof(IntPtr)];
						*vStream = vb.BufferPointer;
						Utility.DrawRanges(ib.BufferPointer, vStream, 1, new IntPtr(ranges.GetUnsafePtr<DrawBufferRange>()), ranges.Length, this.m_VertexDecl);
					}
				}
			}
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000875FC File Offset: 0x000857FC
		private void WaitOnCpuFence(uint fence)
		{
			bool flag = fence != 0U && !Utility.CPUFencePassed(fence);
			if (flag)
			{
				Utility.WaitForCPUFencePassed(fence);
			}
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00087628 File Offset: 0x00085828
		public void AdvanceFrame()
		{
			this.m_FrameIndex += 1U;
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			bool flag = this.m_Fences != null;
			if (flag)
			{
				int fenceIndex = (int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length));
				uint fence = this.m_Fences[fenceIndex];
				this.WaitOnCpuFence(fence);
				this.m_Fences[fenceIndex] = 0U;
			}
			this.m_NextUpdateID = 1U;
			List<UIRenderDevice.AllocToFree> queueToFree = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToFree alloc in queueToFree)
			{
				bool vertices = alloc.vertices;
				if (vertices)
				{
					alloc.page.vertices.allocator.Free(alloc.alloc);
				}
				else
				{
					alloc.page.indices.allocator.Free(alloc.alloc);
				}
			}
			queueToFree.Clear();
			List<UIRenderDevice.AllocToUpdate> queueToUpdate = this.m_Updates[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToUpdate update in queueToUpdate)
			{
				bool flag2 = update.meshHandle.updateAllocID == update.id && update.meshHandle.allocTime == update.allocTime;
				if (flag2)
				{
					NativeSlice<Vertex> srcVerts = new NativeSlice<Vertex>(update.meshHandle.allocPage.vertices.cpuData, (int)update.meshHandle.allocVerts.start, (int)update.meshHandle.allocVerts.size);
					NativeSlice<Vertex> destVerts = new NativeSlice<Vertex>(update.permPage.vertices.cpuData, (int)update.permAllocVerts.start, (int)update.meshHandle.allocVerts.size);
					destVerts.CopyFrom(srcVerts);
					update.permPage.vertices.RegisterUpdate(update.permAllocVerts.start, update.meshHandle.allocVerts.size);
					bool copyBackIndices = update.copyBackIndices;
					if (copyBackIndices)
					{
						NativeSlice<ushort> srcIndices = new NativeSlice<ushort>(update.meshHandle.allocPage.indices.cpuData, (int)update.meshHandle.allocIndices.start, (int)update.meshHandle.allocIndices.size);
						NativeSlice<ushort> destIndices = new NativeSlice<ushort>(update.permPage.indices.cpuData, (int)update.permAllocIndices.start, (int)update.meshHandle.allocIndices.size);
						int indexCount = destIndices.Length;
						int indexDifference = (int)(update.permAllocVerts.start - update.meshHandle.allocVerts.start);
						for (int i = 0; i < indexCount; i++)
						{
							destIndices[i] = (ushort)((int)srcIndices[i] + indexDifference);
						}
						update.permPage.indices.RegisterUpdate(update.permAllocIndices.start, update.meshHandle.allocIndices.size);
					}
					queueToFree.Add(new UIRenderDevice.AllocToFree
					{
						alloc = update.meshHandle.allocVerts,
						page = update.meshHandle.allocPage,
						vertices = true
					});
					queueToFree.Add(new UIRenderDevice.AllocToFree
					{
						alloc = update.meshHandle.allocIndices,
						page = update.meshHandle.allocPage,
						vertices = false
					});
					update.meshHandle.allocVerts = update.permAllocVerts;
					update.meshHandle.allocIndices = update.permAllocIndices;
					update.meshHandle.allocPage = update.permPage;
					update.meshHandle.updateAllocID = 0U;
				}
			}
			queueToUpdate.Clear();
			this.PruneUnusedPages();
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00087A88 File Offset: 0x00085C88
		private void PruneUnusedPages()
		{
			Page lastToPrune;
			Page firstToPrune;
			Page firstToKeep;
			Page lastToKeep = (firstToKeep = (firstToPrune = (lastToPrune = null)));
			Page next;
			for (Page current = this.m_FirstPage; current != null; current = next)
			{
				bool flag = !current.isEmpty;
				if (flag)
				{
					current.framesEmpty = 0;
				}
				else
				{
					current.framesEmpty++;
				}
				bool flag2 = current.framesEmpty < 60;
				if (flag2)
				{
					bool flag3 = firstToKeep != null;
					if (flag3)
					{
						lastToKeep.next = current;
					}
					else
					{
						firstToKeep = current;
					}
					lastToKeep = current;
				}
				else
				{
					bool flag4 = firstToPrune != null;
					if (flag4)
					{
						lastToPrune.next = current;
					}
					else
					{
						firstToPrune = current;
					}
					lastToPrune = current;
				}
				next = current.next;
				current.next = null;
			}
			this.m_FirstPage = firstToKeep;
			Page next2;
			for (Page current = firstToPrune; current != null; current = next2)
			{
				next2 = current.next;
				current.next = null;
				current.Dispose();
			}
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x00087B68 File Offset: 0x00085D68
		internal static void PrepareForGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount++;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x00087B77 File Offset: 0x00085D77
		internal static void WrapUpGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount--;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x00087B86 File Offset: 0x00085D86
		internal static void FlushAllPendingDeviceDisposes()
		{
			Utility.SyncRenderThread();
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x00087B98 File Offset: 0x00085D98
		internal UIRenderDevice.DrawStatistics GatherDrawStatistics()
		{
			return this.m_DrawStats;
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x00087BB0 File Offset: 0x00085DB0
		private static void ProcessDeviceFreeQueue()
		{
			bool synchronousFree = UIRenderDevice.m_SynchronousFree;
			if (synchronousFree)
			{
				Utility.SyncRenderThread();
			}
			for (LinkedListNode<UIRenderDevice.DeviceToFree> freeNode = UIRenderDevice.m_DeviceFreeQueue.First; freeNode != null; freeNode = UIRenderDevice.m_DeviceFreeQueue.First)
			{
				bool flag = !Utility.CPUFencePassed(freeNode.Value.handle);
				if (flag)
				{
					break;
				}
				freeNode.Value.Dispose();
				UIRenderDevice.m_DeviceFreeQueue.RemoveFirst();
			}
			Debug.Assert(!UIRenderDevice.m_SynchronousFree || UIRenderDevice.m_DeviceFreeQueue.Count == 0);
			bool flag2 = UIRenderDevice.m_ActiveDeviceCount == 0 && UIRenderDevice.m_SubscribedToNotifications;
			if (flag2)
			{
				Utility.NotifyOfUIREvents(false);
				UIRenderDevice.m_SubscribedToNotifications = false;
			}
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x00087C65 File Offset: 0x00085E65
		private static void OnEngineUpdateGlobal()
		{
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x00087C6E File Offset: 0x00085E6E
		private static void OnFlushPendingResources()
		{
			UIRenderDevice.m_SynchronousFree = true;
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x04001078 RID: 4216
		private readonly bool m_MockDevice;

		// Token: 0x04001079 RID: 4217
		private IntPtr m_DefaultStencilState;

		// Token: 0x0400107A RID: 4218
		private IntPtr m_VertexDecl;

		// Token: 0x0400107B RID: 4219
		private Page m_FirstPage;

		// Token: 0x0400107C RID: 4220
		private uint m_NextPageVertexCount;

		// Token: 0x0400107D RID: 4221
		private uint m_LargeMeshVertexCount;

		// Token: 0x0400107E RID: 4222
		private float m_IndexToVertexCountRatio;

		// Token: 0x0400107F RID: 4223
		private List<List<UIRenderDevice.AllocToFree>> m_DeferredFrees;

		// Token: 0x04001080 RID: 4224
		private List<List<UIRenderDevice.AllocToUpdate>> m_Updates;

		// Token: 0x04001081 RID: 4225
		private List<CommandList>[] m_CommandLists;

		// Token: 0x04001082 RID: 4226
		private uint[] m_Fences;

		// Token: 0x04001083 RID: 4227
		private MaterialPropertyBlock m_ConstantProps;

		// Token: 0x04001084 RID: 4228
		private MaterialPropertyBlock m_BatchProps;

		// Token: 0x04001085 RID: 4229
		private uint m_FrameIndex;

		// Token: 0x04001086 RID: 4230
		private uint m_NextUpdateID = 1U;

		// Token: 0x04001087 RID: 4231
		private UIRenderDevice.DrawStatistics m_DrawStats;

		// Token: 0x04001088 RID: 4232
		private readonly LinkedPool<MeshHandle> m_MeshHandles = new LinkedPool<MeshHandle>(() => new MeshHandle(), delegate(MeshHandle mh)
		{
		}, 10000);

		// Token: 0x04001089 RID: 4233
		private readonly DrawParams m_DrawParams = new DrawParams();

		// Token: 0x0400108A RID: 4234
		private readonly TextureSlotManager m_TextureSlotManager = new TextureSlotManager();

		// Token: 0x0400108B RID: 4235
		private static LinkedList<UIRenderDevice.DeviceToFree> m_DeviceFreeQueue = new LinkedList<UIRenderDevice.DeviceToFree>();

		// Token: 0x0400108C RID: 4236
		private static int m_ActiveDeviceCount = 0;

		// Token: 0x0400108D RID: 4237
		private static bool m_SubscribedToNotifications;

		// Token: 0x0400108E RID: 4238
		private static bool m_SynchronousFree;

		// Token: 0x0400108F RID: 4239
		private static readonly int s_GradientSettingsTexID = Shader.PropertyToID("_GradientSettingsTex");

		// Token: 0x04001090 RID: 4240
		private static readonly int s_ShaderInfoTexID = Shader.PropertyToID("_ShaderInfoTex");

		// Token: 0x04001091 RID: 4241
		private static ProfilerMarker s_MarkerAllocate = new ProfilerMarker("UIR.Allocate");

		// Token: 0x04001092 RID: 4242
		private static ProfilerMarker s_MarkerFree = new ProfilerMarker("UIR.Free");

		// Token: 0x04001093 RID: 4243
		private static ProfilerMarker s_MarkerAdvanceFrame = new ProfilerMarker("UIR.AdvanceFrame");

		// Token: 0x04001094 RID: 4244
		private static ProfilerMarker s_MarkerFence = new ProfilerMarker("UIR.WaitOnFence");

		// Token: 0x04001095 RID: 4245
		private static ProfilerMarker s_MarkerBeforeDraw = new ProfilerMarker("UIR.BeforeDraw");

		// Token: 0x04001099 RID: 4249
		internal int currentFrameCommandListCount = 0;

		// Token: 0x0200050A RID: 1290
		internal struct AllocToUpdate
		{
			// Token: 0x0400109B RID: 4251
			public uint id;

			// Token: 0x0400109C RID: 4252
			public uint allocTime;

			// Token: 0x0400109D RID: 4253
			public MeshHandle meshHandle;

			// Token: 0x0400109E RID: 4254
			public Alloc permAllocVerts;

			// Token: 0x0400109F RID: 4255
			public Alloc permAllocIndices;

			// Token: 0x040010A0 RID: 4256
			public Page permPage;

			// Token: 0x040010A1 RID: 4257
			public bool copyBackIndices;
		}

		// Token: 0x0200050B RID: 1291
		private struct AllocToFree
		{
			// Token: 0x040010A2 RID: 4258
			public Alloc alloc;

			// Token: 0x040010A3 RID: 4259
			public Page page;

			// Token: 0x040010A4 RID: 4260
			public bool vertices;
		}

		// Token: 0x0200050C RID: 1292
		private struct DeviceToFree
		{
			// Token: 0x0600241B RID: 9243 RVA: 0x00087C80 File Offset: 0x00085E80
			public void Dispose()
			{
				while (this.page != null)
				{
					Page pageToDispose = this.page;
					this.page = this.page.next;
					pageToDispose.Dispose();
				}
				bool flag = this.commandLists != null;
				if (flag)
				{
					for (int i = 0; i < this.commandLists.Length; i++)
					{
						foreach (CommandList c in this.commandLists[i])
						{
							c.Dispose();
						}
						this.commandLists[i] = null;
					}
				}
			}

			// Token: 0x040010A5 RID: 4261
			public uint handle;

			// Token: 0x040010A6 RID: 4262
			public Page page;

			// Token: 0x040010A7 RID: 4263
			public List<CommandList>[] commandLists;
		}

		// Token: 0x0200050D RID: 1293
		private struct EvaluationState
		{
			// Token: 0x040010A8 RID: 4264
			public CommandList activeCommandList;

			// Token: 0x040010A9 RID: 4265
			public MaterialPropertyBlock constantProps;

			// Token: 0x040010AA RID: 4266
			public MaterialPropertyBlock batchProps;

			// Token: 0x040010AB RID: 4267
			public Material defaultMat;

			// Token: 0x040010AC RID: 4268
			public State curState;

			// Token: 0x040010AD RID: 4269
			public Page curPage;

			// Token: 0x040010AE RID: 4270
			public bool mustApplyMaterial;

			// Token: 0x040010AF RID: 4271
			public bool mustApplyBatchProps;

			// Token: 0x040010B0 RID: 4272
			public bool mustApplyStencil;
		}

		// Token: 0x0200050E RID: 1294
		internal struct DrawStatistics
		{
			// Token: 0x040010B1 RID: 4273
			public int currentFrameIndex;

			// Token: 0x040010B2 RID: 4274
			public uint totalIndices;

			// Token: 0x040010B3 RID: 4275
			public uint commandCount;

			// Token: 0x040010B4 RID: 4276
			public uint skippedCommandCount;

			// Token: 0x040010B5 RID: 4277
			public uint drawCommandCount;

			// Token: 0x040010B6 RID: 4278
			public uint disableCommandCount;

			// Token: 0x040010B7 RID: 4279
			public uint materialSetCount;

			// Token: 0x040010B8 RID: 4280
			public uint drawRangeCount;

			// Token: 0x040010B9 RID: 4281
			public uint drawRangeCallCount;

			// Token: 0x040010BA RID: 4282
			public uint immediateDraws;

			// Token: 0x040010BB RID: 4283
			public uint stencilRefChanges;
		}
	}
}
