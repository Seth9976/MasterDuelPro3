using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x02000089 RID: 137
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}, IsCreated = {IsCreated}")]
	[GenerateTestsForBurstCompatibility]
	public struct NativeBitArray : INativeDisposable, IDisposable
	{
		// Token: 0x060006F1 RID: 1777 RVA: 0x0001724D File Offset: 0x0001544D
		public unsafe NativeBitArray(int numBits, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			this.m_BitArray = UnsafeBitArray.Alloc(allocator);
			this.m_Allocator = allocator;
			*this.m_BitArray = new UnsafeBitArray(numBits, allocator, options);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00017275 File Offset: 0x00015475
		public unsafe readonly bool IsCreated
		{
			get
			{
				return this.m_BitArray != null && this.m_BitArray->IsCreated;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001728E File Offset: 0x0001548E
		public readonly bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000172A3 File Offset: 0x000154A3
		public unsafe void Resize(int numBits, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this.m_BitArray->Resize(numBits, options);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000172B2 File Offset: 0x000154B2
		public unsafe void SetCapacity(int capacityInBits)
		{
			this.m_BitArray->SetCapacity(capacityInBits);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000172C0 File Offset: 0x000154C0
		public unsafe void TrimExcess()
		{
			this.m_BitArray->TrimExcess();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000172CD File Offset: 0x000154CD
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			UnsafeBitArray.Free(this.m_BitArray, this.m_Allocator);
			this.m_BitArray = null;
			this.m_Allocator = AllocatorManager.Invalid;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000172FC File Offset: 0x000154FC
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new NativeBitArrayDisposeJob
			{
				Data = new NativeBitArrayDispose
				{
					m_BitArrayData = this.m_BitArray,
					m_Allocator = this.m_Allocator
				}
			}.Schedule(inputDeps);
			this.m_BitArray = null;
			this.m_Allocator = AllocatorManager.Invalid;
			return jobHandle;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001735F File Offset: 0x0001555F
		public unsafe readonly int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return CollectionHelper.AssumePositive(this.m_BitArray->Length);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00017371 File Offset: 0x00015571
		public unsafe readonly int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return CollectionHelper.AssumePositive(this.m_BitArray->Capacity);
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00017383 File Offset: 0x00015583
		public unsafe void Clear()
		{
			this.m_BitArray->Clear();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00017390 File Offset: 0x00015590
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe NativeArray<T> AsNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
		{
			int bitsPerElement = UnsafeUtility.SizeOf<T>() * 8;
			int length = this.m_BitArray->Length / bitsPerElement;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.m_BitArray->Ptr, length, Allocator.None);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000173C5 File Offset: 0x000155C5
		public unsafe void Set(int pos, bool value)
		{
			this.m_BitArray->Set(pos, value);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000173D4 File Offset: 0x000155D4
		public unsafe void SetBits(int pos, bool value, int numBits)
		{
			this.m_BitArray->SetBits(pos, value, numBits);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000173E4 File Offset: 0x000155E4
		public unsafe void SetBits(int pos, ulong value, int numBits = 1)
		{
			this.m_BitArray->SetBits(pos, value, numBits);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000173F4 File Offset: 0x000155F4
		public unsafe ulong GetBits(int pos, int numBits = 1)
		{
			return this.m_BitArray->GetBits(pos, numBits);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00017403 File Offset: 0x00015603
		public unsafe bool IsSet(int pos)
		{
			return this.m_BitArray->IsSet(pos);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00017411 File Offset: 0x00015611
		public unsafe void Copy(int dstPos, int srcPos, int numBits)
		{
			this.m_BitArray->Copy(dstPos, srcPos, numBits);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00017421 File Offset: 0x00015621
		public unsafe void Copy(int dstPos, ref NativeBitArray srcBitArray, int srcPos, int numBits)
		{
			this.m_BitArray->Copy(dstPos, ref *srcBitArray.m_BitArray, srcPos, numBits);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00017438 File Offset: 0x00015638
		public unsafe int Find(int pos, int numBits)
		{
			return this.m_BitArray->Find(pos, numBits);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00017447 File Offset: 0x00015647
		public unsafe int Find(int pos, int count, int numBits)
		{
			return this.m_BitArray->Find(pos, count, numBits);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00017457 File Offset: 0x00015657
		public unsafe bool TestNone(int pos, int numBits = 1)
		{
			return this.m_BitArray->TestNone(pos, numBits);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00017466 File Offset: 0x00015666
		public unsafe bool TestAny(int pos, int numBits = 1)
		{
			return this.m_BitArray->TestAny(pos, numBits);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00017475 File Offset: 0x00015675
		public unsafe bool TestAll(int pos, int numBits = 1)
		{
			return this.m_BitArray->TestAll(pos, numBits);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00017484 File Offset: 0x00015684
		public unsafe int CountBits(int pos, int numBits = 1)
		{
			return this.m_BitArray->CountBits(pos, numBits);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00017493 File Offset: 0x00015693
		public NativeBitArray.ReadOnly AsReadOnly()
		{
			return new NativeBitArray.ReadOnly(ref this);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private readonly void CheckRead()
		{
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001749C File Offset: 0x0001569C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private unsafe void CheckReadBounds<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
		{
			int bitsPerElement = UnsafeUtility.SizeOf<T>() * 8;
			int length = this.m_BitArray->Length / bitsPerElement;
			if (length == 0)
			{
				throw new InvalidOperationException(string.Format("Number of bits in the NativeBitArray {0} is not sufficient to cast to NativeArray<T> {1}.", this.m_BitArray->Length, UnsafeUtility.SizeOf<T>() * 8));
			}
			if (this.m_BitArray->Length != bitsPerElement * length)
			{
				throw new InvalidOperationException(string.Format("Number of bits in the NativeBitArray {0} couldn't hold multiple of T {1}. Output array would be truncated.", this.m_BitArray->Length, UnsafeUtility.SizeOf<T>()));
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x040003AC RID: 940
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeBitArray* m_BitArray;

		// Token: 0x040003AD RID: 941
		internal AllocatorManager.AllocatorHandle m_Allocator;

		// Token: 0x0200008A RID: 138
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct ReadOnly
		{
			// Token: 0x170000AE RID: 174
			// (get) Token: 0x0600070E RID: 1806 RVA: 0x00017528 File Offset: 0x00015728
			public readonly bool IsCreated
			{
				get
				{
					return this.m_BitArray.IsCreated;
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x0600070F RID: 1807 RVA: 0x00017535 File Offset: 0x00015735
			public readonly bool IsEmpty
			{
				get
				{
					return this.m_BitArray.IsEmpty;
				}
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x00017542 File Offset: 0x00015742
			internal unsafe ReadOnly(ref NativeBitArray data)
			{
				this.m_BitArray = data.m_BitArray->AsReadOnly();
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000711 RID: 1809 RVA: 0x00017555 File Offset: 0x00015755
			public readonly int Length
			{
				get
				{
					return CollectionHelper.AssumePositive(this.m_BitArray.Length);
				}
			}

			// Token: 0x06000712 RID: 1810 RVA: 0x00017567 File Offset: 0x00015767
			public readonly ulong GetBits(int pos, int numBits = 1)
			{
				return this.m_BitArray.GetBits(pos, numBits);
			}

			// Token: 0x06000713 RID: 1811 RVA: 0x00017576 File Offset: 0x00015776
			public readonly bool IsSet(int pos)
			{
				return this.m_BitArray.IsSet(pos);
			}

			// Token: 0x06000714 RID: 1812 RVA: 0x00017584 File Offset: 0x00015784
			public readonly int Find(int pos, int numBits)
			{
				return this.m_BitArray.Find(pos, numBits);
			}

			// Token: 0x06000715 RID: 1813 RVA: 0x00017593 File Offset: 0x00015793
			public readonly int Find(int pos, int count, int numBits)
			{
				return this.m_BitArray.Find(pos, count, numBits);
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x000175A3 File Offset: 0x000157A3
			public readonly bool TestNone(int pos, int numBits = 1)
			{
				return this.m_BitArray.TestNone(pos, numBits);
			}

			// Token: 0x06000717 RID: 1815 RVA: 0x000175B2 File Offset: 0x000157B2
			public readonly bool TestAny(int pos, int numBits = 1)
			{
				return this.m_BitArray.TestAny(pos, numBits);
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x000175C1 File Offset: 0x000157C1
			public readonly bool TestAll(int pos, int numBits = 1)
			{
				return this.m_BitArray.TestAll(pos, numBits);
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x000175D0 File Offset: 0x000157D0
			public readonly int CountBits(int pos, int numBits = 1)
			{
				return this.m_BitArray.CountBits(pos, numBits);
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private readonly void CheckRead()
			{
			}

			// Token: 0x040003AE RID: 942
			[NativeDisableUnsafePtrRestriction]
			internal UnsafeBitArray.ReadOnly m_BitArray;
		}
	}
}
