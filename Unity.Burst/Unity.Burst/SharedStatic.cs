using System;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x0200002C RID: 44
	public readonly struct SharedStatic<T> where T : struct
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00005961 File Offset: 0x00003B61
		private unsafe SharedStatic(void* buffer)
		{
			this._buffer = buffer;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000596A File Offset: 0x00003B6A
		public ref T Data
		{
			get
			{
				return Unsafe.AsRef<T>(this._buffer);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005977 File Offset: 0x00003B77
		public unsafe void* UnsafeDataPointer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000597F File Offset: 0x00003B7F
		public static SharedStatic<T> GetOrCreate<TContext>(uint alignment = 0U)
		{
			return SharedStatic<T>.GetOrCreateUnsafe(alignment, BurstRuntime.GetHashCode64<TContext>(), 0L);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000598E File Offset: 0x00003B8E
		public static SharedStatic<T> GetOrCreate<TContext, TSubContext>(uint alignment = 0U)
		{
			return SharedStatic<T>.GetOrCreateUnsafe(alignment, BurstRuntime.GetHashCode64<TContext>(), BurstRuntime.GetHashCode64<TSubContext>());
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000059A0 File Offset: 0x00003BA0
		public static SharedStatic<T> GetOrCreateUnsafe(uint alignment, long hashCode, long subHashCode)
		{
			return new SharedStatic<T>(SharedStatic.GetOrCreateSharedStaticInternal(hashCode, subHashCode, (uint)UnsafeUtility.SizeOf<T>(), (alignment == 0U) ? 16U : alignment));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000059BB File Offset: 0x00003BBB
		public static SharedStatic<T> GetOrCreatePartiallyUnsafeWithHashCode<TSubContext>(uint alignment, long hashCode)
		{
			return new SharedStatic<T>(SharedStatic.GetOrCreateSharedStaticInternal(hashCode, BurstRuntime.GetHashCode64<TSubContext>(), (uint)UnsafeUtility.SizeOf<T>(), (alignment == 0U) ? 16U : alignment));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000059DA File Offset: 0x00003BDA
		public static SharedStatic<T> GetOrCreatePartiallyUnsafeWithSubHashCode<TContext>(uint alignment, long subHashCode)
		{
			return new SharedStatic<T>(SharedStatic.GetOrCreateSharedStaticInternal(BurstRuntime.GetHashCode64<TContext>(), subHashCode, (uint)UnsafeUtility.SizeOf<T>(), (alignment == 0U) ? 16U : alignment));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000059F9 File Offset: 0x00003BF9
		public static SharedStatic<T> GetOrCreate(Type contextType, uint alignment = 0U)
		{
			return SharedStatic<T>.GetOrCreateUnsafe(alignment, BurstRuntime.GetHashCode64(contextType), 0L);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005A09 File Offset: 0x00003C09
		public static SharedStatic<T> GetOrCreate(Type contextType, Type subContextType, uint alignment = 0U)
		{
			return SharedStatic<T>.GetOrCreateUnsafe(alignment, BurstRuntime.GetHashCode64(contextType), BurstRuntime.GetHashCode64(subContextType));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005A1D File Offset: 0x00003C1D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckIf_T_IsUnmanagedOrThrow()
		{
			if (!UnsafeUtility.IsUnmanaged<T>())
			{
				throw new InvalidOperationException(string.Format("The type {0} used in SharedStatic<{1}> must be unmanaged (contain no managed types).", typeof(T), typeof(T)));
			}
		}

		// Token: 0x04000173 RID: 371
		private unsafe readonly void* _buffer;

		// Token: 0x04000174 RID: 372
		private const uint DefaultAlignment = 16U;
	}
}
