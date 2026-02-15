using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200014C RID: 332
	[GenerateTestsForBurstCompatibility]
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	public struct UnsafeText : INativeDisposable, IDisposable, IUTF8Bytes, INativeList<byte>, IIndexable<byte>
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x0002A64C File Offset: 0x0002884C
		public unsafe UnsafeText(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_UntypedListData = default(UntypedUnsafeList);
			*(ref this).AsUnsafeListOfBytes() = new UnsafeList<byte>(capacity + 1, allocator, NativeArrayOptions.UninitializedMemory);
			this.Length = 0;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0002A678 File Offset: 0x00028878
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.AsUnsafeListOfBytesRO().IsCreated;
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0002A698 File Offset: 0x00028898
		internal unsafe static UnsafeText* Alloc(AllocatorManager.AllocatorHandle allocator)
		{
			return (UnsafeText*)Memory.Unmanaged.Allocate((long)sizeof(UnsafeText), UnsafeUtility.AlignOf<UnsafeText>(), allocator);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002A6BC File Offset: 0x000288BC
		internal unsafe static void Free(UnsafeText* data)
		{
			if (data == null)
			{
				throw new InvalidOperationException("UnsafeText has yet to be created or has been destroyed!");
			}
			AllocatorManager.AllocatorHandle allocator = data->m_UntypedListData.Allocator;
			data->Dispose();
			Memory.Unmanaged.Free<UnsafeText>(data, allocator);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002A6F2 File Offset: 0x000288F2
		public void Dispose()
		{
			(ref this).AsUnsafeListOfBytes().Dispose();
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002A6FF File Offset: 0x000288FF
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return (ref this).AsUnsafeListOfBytes().Dispose(inputDeps);
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0002A70D File Offset: 0x0002890D
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x170001A3 RID: 419
		public byte this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return UnsafeUtility.ReadArrayElement<byte>(this.m_UntypedListData.Ptr, index);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				UnsafeUtility.WriteArrayElement<byte>(this.m_UntypedListData.Ptr, index, value);
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002A749 File Offset: 0x00028949
		public ref byte ElementAt(int index)
		{
			return UnsafeUtility.ArrayElementAsRef<byte>(this.m_UntypedListData.Ptr, index);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002A75C File Offset: 0x0002895C
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002A765 File Offset: 0x00028965
		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)this.m_UntypedListData.Ptr;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0002A772 File Offset: 0x00028972
		public bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory)
		{
			(ref this).AsUnsafeListOfBytes().Resize(newLength + 1, clearOptions);
			(ref this).AsUnsafeListOfBytes()[newLength] = 0;
			return true;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0002A794 File Offset: 0x00028994
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0002A7B6 File Offset: 0x000289B6
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.AsUnsafeListOfBytesRO().Capacity - 1;
			}
			set
			{
				(ref this).AsUnsafeListOfBytes().SetCapacity(value + 1);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0002A7C8 File Offset: 0x000289C8
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x0002A7EA File Offset: 0x000289EA
		public int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.AsUnsafeListOfBytesRO().Length - 1;
			}
			set
			{
				(ref this).AsUnsafeListOfBytes().Resize(value + 1, NativeArrayOptions.UninitializedMemory);
				(ref this).AsUnsafeListOfBytes()[value] = 0;
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0002A808 File Offset: 0x00028A08
		[ExcludeFromBurstCompatTesting("Returns managed string")]
		public override string ToString()
		{
			if (!this.IsCreated)
			{
				return "";
			}
			return (ref this).ConvertToString<UnsafeText>();
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0002A820 File Offset: 0x00028A20
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} must be positive.", index));
			}
			if (index >= this.Length)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range in UnsafeText of {1} length.", index, this.Length));
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002A871 File Offset: 0x00028A71
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowCopyError(CopyError error, string source)
		{
			throw new ArgumentException(string.Format("UnsafeText: {0} while copying \"{1}\"", error, source));
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002A889 File Offset: 0x00028A89
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckCapacityInRange(int value, int length)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value {0} must be positive.", value));
			}
			if (value < length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value {0} is out of range in NativeList of '{1}' Length.", value, length));
			}
		}

		// Token: 0x0400054C RID: 1356
		internal UntypedUnsafeList m_UntypedListData;
	}
}
