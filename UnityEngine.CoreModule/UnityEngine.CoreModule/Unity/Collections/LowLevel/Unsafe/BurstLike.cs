using System;
using System.Runtime.CompilerServices;
using Unity.Burst.LowLevel;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000061 RID: 97
	[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
	[StaticAccessor("BurstLike", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/BurstLike/BurstLike.bindings.h")]
	internal static class BurstLike
	{
		// Token: 0x02000062 RID: 98
		[VisibleToOtherModules(new string[] { "UnityEngine.ParticleSystemModule" })]
		internal readonly struct SharedStatic<[IsUnmanaged] T> where T : struct, ValueType
		{
			// Token: 0x0600012E RID: 302 RVA: 0x000042A9 File Offset: 0x000024A9
			private unsafe SharedStatic(void* buffer)
			{
				this._buffer = buffer;
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x0600012F RID: 303 RVA: 0x000042B2 File Offset: 0x000024B2
			public ref T Data
			{
				get
				{
					return UnsafeUtility.AsRef<T>(this._buffer);
				}
			}

			// Token: 0x06000130 RID: 304 RVA: 0x000042BF File Offset: 0x000024BF
			public static BurstLike.SharedStatic<T> GetOrCreate<TContext>(uint alignment = 0U)
			{
				return new BurstLike.SharedStatic<T>(BurstLike.SharedStatic.GetOrCreateSharedStaticInternal(BurstRuntime.GetHashCode64<TContext>(), 0L, (uint)UnsafeUtility.SizeOf<T>(), alignment));
			}

			// Token: 0x0400010F RID: 271
			private unsafe readonly void* _buffer;
		}

		// Token: 0x02000063 RID: 99
		internal static class SharedStatic
		{
			// Token: 0x06000131 RID: 305 RVA: 0x000042D8 File Offset: 0x000024D8
			public unsafe static void* GetOrCreateSharedStaticInternal(long getHashCode64, long getSubHashCode64, uint sizeOf, uint alignment)
			{
				Hash128 hash128 = new Hash128((ulong)getHashCode64, (ulong)getSubHashCode64);
				return BurstCompilerService.GetOrCreateSharedMemory(ref hash128, sizeOf, (alignment == 0U) ? 4U : alignment);
			}
		}
	}
}
