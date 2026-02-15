using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000A2 RID: 162
	[NativeHeader("Runtime/Misc/Cache.h")]
	[StaticAccessor("CacheWrapper", StaticAccessorType.DoubleColon)]
	public struct Cache : IEquatable<Cache>
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00006564 File Offset: 0x00004764
		internal int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000657C File Offset: 0x0000477C
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00006594 File Offset: 0x00004794
		public override bool Equals(object other)
		{
			return other is Cache && this.Equals((Cache)other);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000065C0 File Offset: 0x000047C0
		public bool Equals(Cache other)
		{
			return this.handle == other.handle;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000065E4 File Offset: 0x000047E4
		public bool valid
		{
			get
			{
				return Cache.Cache_IsValid(this.m_Handle);
			}
		}

		// Token: 0x06000297 RID: 663
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsValid(int handle);

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00006604 File Offset: 0x00004804
		public string path
		{
			get
			{
				return Cache.Cache_GetPath(this.m_Handle);
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00006624 File Offset: 0x00004824
		[NativeThrows]
		internal static string Cache_GetPath(int handle)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Cache.Cache_GetPath_Injected(handle, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000069 RID: 105
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00006654 File Offset: 0x00004854
		public long maximumAvailableStorageSpace
		{
			set
			{
				Cache.Cache_SetMaximumDiskSpaceAvailable(this.m_Handle, value);
			}
		}

		// Token: 0x0600029B RID: 667
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Cache_SetMaximumDiskSpaceAvailable(int handle, long value);

		// Token: 0x1700006A RID: 106
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00006664 File Offset: 0x00004864
		public int expirationDelay
		{
			set
			{
				Cache.Cache_SetExpirationDelay(this.m_Handle, value);
			}
		}

		// Token: 0x0600029D RID: 669
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Cache_SetExpirationDelay(int handle, int value);

		// Token: 0x0600029E RID: 670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Cache_GetPath_Injected(int handle, out ManagedSpanWrapper ret);

		// Token: 0x040001F1 RID: 497
		private int m_Handle;
	}
}
