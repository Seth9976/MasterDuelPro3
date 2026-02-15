using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000083 RID: 131
	[GenerateTestsForBurstCompatibility]
	internal struct Memory
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x00016CAC File Offset: 0x00014EAC
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckByteCountIsReasonable(long size)
		{
			if (size < 0L)
			{
				throw new InvalidOperationException(string.Format("Attempted to operate on {0} bytes of memory: negative size", size));
			}
			if (size > 1099511627776L)
			{
				throw new InvalidOperationException(string.Format("Attempted to operate on {0} bytes of memory: size too big", size));
			}
		}

		// Token: 0x040003AA RID: 938
		internal const long k_MaximumRamSizeInBytes = 1099511627776L;

		// Token: 0x02000084 RID: 132
		[GenerateTestsForBurstCompatibility]
		internal struct Unmanaged
		{
			// Token: 0x060006D4 RID: 1748 RVA: 0x00016CEB File Offset: 0x00014EEB
			internal unsafe static void* Allocate(long size, int align, AllocatorManager.AllocatorHandle allocator)
			{
				return Memory.Unmanaged.Array.Resize(null, 0L, 1L, allocator, size, align);
			}

			// Token: 0x060006D5 RID: 1749 RVA: 0x00016CFB File Offset: 0x00014EFB
			internal unsafe static void Free(void* pointer, AllocatorManager.AllocatorHandle allocator)
			{
				if (pointer == null)
				{
					return;
				}
				Memory.Unmanaged.Array.Resize(pointer, 1L, 0L, allocator, 1L, 1);
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x00016D12 File Offset: 0x00014F12
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			internal unsafe static T* Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
			{
				return Memory.Unmanaged.Array.Resize<T>(null, 0L, 1L, allocator);
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x00016D20 File Offset: 0x00014F20
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			internal unsafe static void Free<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* pointer, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
			{
				if (pointer == null)
				{
					return;
				}
				Memory.Unmanaged.Array.Resize<T>(pointer, 1L, 0L, allocator);
			}

			// Token: 0x02000085 RID: 133
			[GenerateTestsForBurstCompatibility]
			internal struct Array
			{
				// Token: 0x060006D8 RID: 1752 RVA: 0x00002FD9 File Offset: 0x000011D9
				private static bool IsCustom(AllocatorManager.AllocatorHandle allocator)
				{
					return allocator.Index >= 64;
				}

				// Token: 0x060006D9 RID: 1753 RVA: 0x00016D34 File Offset: 0x00014F34
				private unsafe static void* CustomResize(void* oldPointer, long oldCount, long newCount, AllocatorManager.AllocatorHandle allocator, long size, int align)
				{
					AllocatorManager.Block block = default(AllocatorManager.Block);
					block.Range.Allocator = allocator;
					block.Range.Items = (int)newCount;
					block.Range.Pointer = (IntPtr)oldPointer;
					block.BytesPerItem = (int)size;
					block.Alignment = align;
					block.AllocatedItems = (int)oldCount;
					AllocatorManager.Try(ref block);
					return (void*)block.Range.Pointer;
				}

				// Token: 0x060006DA RID: 1754 RVA: 0x00016DAC File Offset: 0x00014FAC
				internal unsafe static void* Resize(void* oldPointer, long oldCount, long newCount, AllocatorManager.AllocatorHandle allocator, long size, int align)
				{
					int alignment = math.max(64, align);
					if (Memory.Unmanaged.Array.IsCustom(allocator))
					{
						return Memory.Unmanaged.Array.CustomResize(oldPointer, oldCount, newCount, allocator, size, alignment);
					}
					void* newPointer = default(void*);
					if (newCount > 0L)
					{
						newPointer = UnsafeUtility.MallocTracked(newCount * size, alignment, allocator.ToAllocator, 0);
						if (oldCount > 0L)
						{
							long bytesToCopy = math.min(oldCount, newCount) * size;
							UnsafeUtility.MemCpy(newPointer, oldPointer, bytesToCopy);
						}
					}
					if (oldCount > 0L)
					{
						UnsafeUtility.FreeTracked(oldPointer, allocator.ToAllocator);
					}
					return newPointer;
				}

				// Token: 0x060006DB RID: 1755 RVA: 0x00016E23 File Offset: 0x00015023
				[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
				internal unsafe static T* Resize<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* oldPointer, long oldCount, long newCount, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
				{
					return (T*)Memory.Unmanaged.Array.Resize((void*)oldPointer, oldCount, newCount, allocator, (long)UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>());
				}

				// Token: 0x060006DC RID: 1756 RVA: 0x00016E39 File Offset: 0x00015039
				[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
				internal unsafe static T* Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(long count, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
				{
					return Memory.Unmanaged.Array.Resize<T>(null, 0L, count, allocator);
				}

				// Token: 0x060006DD RID: 1757 RVA: 0x00016E46 File Offset: 0x00015046
				[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
				internal unsafe static void Free<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* pointer, long count, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
				{
					if (pointer == null)
					{
						return;
					}
					Memory.Unmanaged.Array.Resize<T>(pointer, count, 0L, allocator);
				}
			}
		}

		// Token: 0x02000086 RID: 134
		[GenerateTestsForBurstCompatibility]
		internal struct Array
		{
			// Token: 0x060006DE RID: 1758 RVA: 0x00016E5C File Offset: 0x0001505C
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			internal unsafe static void Set<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* pointer, long count, T t = default(T)) where T : struct, ValueType
			{
				UnsafeUtility.SizeOf<T>();
				int i = 0;
				while ((long)i < count)
				{
					pointer[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = t;
					i++;
				}
			}

			// Token: 0x060006DF RID: 1759 RVA: 0x00016E90 File Offset: 0x00015090
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			internal unsafe static void Clear<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* pointer, long count) where T : struct, ValueType
			{
				long bytesToClear = count * (long)UnsafeUtility.SizeOf<T>();
				UnsafeUtility.MemClear((void*)pointer, bytesToClear);
			}

			// Token: 0x060006E0 RID: 1760 RVA: 0x00016EB0 File Offset: 0x000150B0
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			internal unsafe static void Copy<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* dest, T* src, long count) where T : struct, ValueType
			{
				long bytesToCopy = count * (long)UnsafeUtility.SizeOf<T>();
				UnsafeUtility.MemCpy((void*)dest, (void*)src, bytesToCopy);
			}
		}
	}
}
