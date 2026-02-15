using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000515 RID: 1301
	internal class Page : IDisposable
	{
		// Token: 0x06002432 RID: 9266 RVA: 0x00088466 File Offset: 0x00086666
		public Page(uint vertexMaxCount, uint indexMaxCount, uint maxQueuedFrameCount, bool mockPage)
		{
			vertexMaxCount = Math.Min(vertexMaxCount, 65536U);
			this.vertices = new Page.DataSet<Vertex>(Utility.GPUBufferType.Vertex, vertexMaxCount, maxQueuedFrameCount, 32U, mockPage);
			this.indices = new Page.DataSet<ushort>(Utility.GPUBufferType.Index, indexMaxCount, maxQueuedFrameCount, 32U, mockPage);
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002433 RID: 9267 RVA: 0x000884A1 File Offset: 0x000866A1
		// (set) Token: 0x06002434 RID: 9268 RVA: 0x000884A9 File Offset: 0x000866A9
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002435 RID: 9269 RVA: 0x000884B2 File Offset: 0x000866B2
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000884C4 File Offset: 0x000866C4
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.indices.Dispose();
					this.vertices.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002437 RID: 9271 RVA: 0x00088508 File Offset: 0x00086708
		public bool isEmpty
		{
			get
			{
				return this.vertices.allocator.isEmpty && this.indices.allocator.isEmpty;
			}
		}

		// Token: 0x040010D2 RID: 4306
		public Page.DataSet<Vertex> vertices;

		// Token: 0x040010D3 RID: 4307
		public Page.DataSet<ushort> indices;

		// Token: 0x040010D4 RID: 4308
		public Page next;

		// Token: 0x040010D5 RID: 4309
		public int framesEmpty;

		// Token: 0x02000516 RID: 1302
		public class DataSet<T> : IDisposable where T : struct
		{
			// Token: 0x06002438 RID: 9272 RVA: 0x00088540 File Offset: 0x00086740
			public DataSet(Utility.GPUBufferType bufferType, uint totalCount, uint maxQueuedFrameCount, uint updateRangePoolSize, bool mockBuffer)
			{
				bool flag = !mockBuffer;
				if (flag)
				{
					this.gpuData = new Utility.GPUBuffer<T>((int)totalCount, bufferType);
				}
				this.cpuData = new NativeArray<T>((int)totalCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.allocator = new GPUBufferAllocator(totalCount);
				bool flag2 = !mockBuffer;
				if (flag2)
				{
					this.m_ElemStride = (uint)this.gpuData.ElementStride;
				}
				this.m_UpdateRangePoolSize = updateRangePoolSize;
				uint multipliedUpdateRangePoolSize = this.m_UpdateRangePoolSize * maxQueuedFrameCount;
				this.updateRanges = new NativeArray<GfxUpdateBufferRange>((int)multipliedUpdateRangePoolSize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_UpdateRangeMin = uint.MaxValue;
				this.m_UpdateRangeMax = 0U;
				this.m_UpdateRangesEnqueued = 0U;
				this.m_UpdateRangesBatchStart = 0U;
			}

			// Token: 0x17000968 RID: 2408
			// (get) Token: 0x06002439 RID: 9273 RVA: 0x000885DA File Offset: 0x000867DA
			// (set) Token: 0x0600243A RID: 9274 RVA: 0x000885E2 File Offset: 0x000867E2
			private protected bool disposed { protected get; private set; }

			// Token: 0x0600243B RID: 9275 RVA: 0x000885EB File Offset: 0x000867EB
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x0600243C RID: 9276 RVA: 0x00088600 File Offset: 0x00086800
			public void Dispose(bool disposing)
			{
				bool disposed = this.disposed;
				if (!disposed)
				{
					if (disposing)
					{
						Utility.GPUBuffer<T> gpubuffer = this.gpuData;
						if (gpubuffer != null)
						{
							gpubuffer.Dispose();
						}
						this.cpuData.Dispose();
						this.updateRanges.Dispose();
					}
					this.disposed = true;
				}
			}

			// Token: 0x0600243D RID: 9277 RVA: 0x00088658 File Offset: 0x00086858
			public void RegisterUpdate(uint start, uint size)
			{
				Debug.Assert((ulong)(start + size) <= (ulong)((long)this.cpuData.Length));
				int rangeIndex = (int)(this.m_UpdateRangesBatchStart + this.m_UpdateRangesEnqueued);
				bool flag = this.m_UpdateRangesEnqueued > 0U;
				if (flag)
				{
					int lastIndex = rangeIndex - 1;
					GfxUpdateBufferRange lastRange = this.updateRanges[lastIndex];
					uint startBytes = start * this.m_ElemStride;
					bool flag2 = lastRange.offsetFromWriteStart + lastRange.size == startBytes;
					if (flag2)
					{
						this.updateRanges[lastIndex] = new GfxUpdateBufferRange
						{
							source = lastRange.source,
							offsetFromWriteStart = lastRange.offsetFromWriteStart,
							size = lastRange.size + size * this.m_ElemStride
						};
						this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
						return;
					}
				}
				this.m_UpdateRangeMin = Math.Min(this.m_UpdateRangeMin, start);
				this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
				bool flag3 = this.m_UpdateRangesEnqueued == this.m_UpdateRangePoolSize;
				if (flag3)
				{
					this.m_UpdateRangesSaturated = true;
				}
				else
				{
					UIntPtr cpuDataSlice = new UIntPtr(this.cpuData.Slice((int)start, (int)size).GetUnsafeReadOnlyPtr<T>());
					this.updateRanges[rangeIndex] = new GfxUpdateBufferRange
					{
						source = cpuDataSlice,
						offsetFromWriteStart = start * this.m_ElemStride,
						size = size * this.m_ElemStride
					};
					this.m_UpdateRangesEnqueued += 1U;
				}
			}

			// Token: 0x0600243E RID: 9278 RVA: 0x000887E4 File Offset: 0x000869E4
			private bool HasMappedBufferRange()
			{
				return Utility.HasMappedBufferRange();
			}

			// Token: 0x0600243F RID: 9279 RVA: 0x000887FC File Offset: 0x000869FC
			public void SendUpdates()
			{
				bool flag = this.HasMappedBufferRange();
				if (flag)
				{
					this.SendPartialRanges();
				}
				else
				{
					this.SendFullRange();
				}
			}

			// Token: 0x06002440 RID: 9280 RVA: 0x00088824 File Offset: 0x00086A24
			public void SendFullRange()
			{
				uint fullRangeBytes = (uint)((long)this.cpuData.Length * (long)((ulong)this.m_ElemStride));
				this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
				{
					source = new UIntPtr(this.cpuData.GetUnsafeReadOnlyPtr<T>()),
					offsetFromWriteStart = 0U,
					size = fullRangeBytes
				};
				Utility.GPUBuffer<T> gpubuffer = this.gpuData;
				if (gpubuffer != null)
				{
					gpubuffer.UpdateRanges(this.updateRanges.Slice((int)this.m_UpdateRangesBatchStart, 1), 0, (int)fullRangeBytes);
				}
				this.ResetUpdateState();
			}

			// Token: 0x06002441 RID: 9281 RVA: 0x000888B8 File Offset: 0x00086AB8
			public void SendPartialRanges()
			{
				bool flag = this.m_UpdateRangesEnqueued == 0U;
				if (!flag)
				{
					bool updateRangesSaturated = this.m_UpdateRangesSaturated;
					if (updateRangesSaturated)
					{
						uint updateSize = this.m_UpdateRangeMax - this.m_UpdateRangeMin;
						this.m_UpdateRangesEnqueued = 1U;
						this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
						{
							source = new UIntPtr(this.cpuData.Slice((int)this.m_UpdateRangeMin, (int)updateSize).GetUnsafeReadOnlyPtr<T>()),
							offsetFromWriteStart = this.m_UpdateRangeMin * this.m_ElemStride,
							size = updateSize * this.m_ElemStride
						};
					}
					uint minByte = this.m_UpdateRangeMin * this.m_ElemStride;
					uint maxByte = this.m_UpdateRangeMax * this.m_ElemStride;
					bool flag2 = minByte > 0U;
					if (flag2)
					{
						for (uint i = 0U; i < this.m_UpdateRangesEnqueued; i += 1U)
						{
							int index = (int)(i + this.m_UpdateRangesBatchStart);
							this.updateRanges[index] = new GfxUpdateBufferRange
							{
								source = this.updateRanges[index].source,
								offsetFromWriteStart = this.updateRanges[index].offsetFromWriteStart - minByte,
								size = this.updateRanges[index].size
							};
						}
					}
					Utility.GPUBuffer<T> gpubuffer = this.gpuData;
					if (gpubuffer != null)
					{
						gpubuffer.UpdateRanges(this.updateRanges.Slice((int)this.m_UpdateRangesBatchStart, (int)this.m_UpdateRangesEnqueued), (int)minByte, (int)maxByte);
					}
					this.ResetUpdateState();
				}
			}

			// Token: 0x06002442 RID: 9282 RVA: 0x00088A4C File Offset: 0x00086C4C
			private void ResetUpdateState()
			{
				this.m_UpdateRangeMin = uint.MaxValue;
				this.m_UpdateRangeMax = 0U;
				this.m_UpdateRangesEnqueued = 0U;
				this.m_UpdateRangesBatchStart += this.m_UpdateRangePoolSize;
				bool flag = (ulong)this.m_UpdateRangesBatchStart >= (ulong)((long)this.updateRanges.Length);
				if (flag)
				{
					this.m_UpdateRangesBatchStart = 0U;
				}
				this.m_UpdateRangesSaturated = false;
			}

			// Token: 0x040010D7 RID: 4311
			public Utility.GPUBuffer<T> gpuData;

			// Token: 0x040010D8 RID: 4312
			public NativeArray<T> cpuData;

			// Token: 0x040010D9 RID: 4313
			public NativeArray<GfxUpdateBufferRange> updateRanges;

			// Token: 0x040010DA RID: 4314
			public GPUBufferAllocator allocator;

			// Token: 0x040010DB RID: 4315
			private readonly uint m_UpdateRangePoolSize;

			// Token: 0x040010DC RID: 4316
			private uint m_ElemStride;

			// Token: 0x040010DD RID: 4317
			private uint m_UpdateRangeMin;

			// Token: 0x040010DE RID: 4318
			private uint m_UpdateRangeMax;

			// Token: 0x040010DF RID: 4319
			private uint m_UpdateRangesEnqueued;

			// Token: 0x040010E0 RID: 4320
			private uint m_UpdateRangesBatchStart;

			// Token: 0x040010E1 RID: 4321
			private bool m_UpdateRangesSaturated;
		}
	}
}
