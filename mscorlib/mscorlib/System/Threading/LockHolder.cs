using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x02000240 RID: 576
	[ReflectionBlocked]
	public struct LockHolder : IDisposable
	{
		// Token: 0x06001534 RID: 5428 RVA: 0x000554D8 File Offset: 0x000536D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static LockHolder Hold(Lock l)
		{
			l.Acquire();
			LockHolder lockHolder;
			lockHolder._lock = l;
			return lockHolder;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000554F4 File Offset: 0x000536F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			this._lock.Release();
		}

		// Token: 0x04000A68 RID: 2664
		private Lock _lock;
	}
}
