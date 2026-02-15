using System;

namespace System.Threading
{
	// Token: 0x0200022E RID: 558
	internal struct DeferredDisposableLifetime<T> where T : class, IDeferredDisposable
	{
		// Token: 0x060014BE RID: 5310 RVA: 0x000107A8 File Offset: 0x0000E9A8
		static DeferredDisposableLifetime()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00053D24 File Offset: 0x00051F24
		public bool AddRef(T obj)
		{
			for (;;)
			{
				int num = Volatile.Read(ref this._count);
				if (num < 0)
				{
					break;
				}
				int num2 = checked(num + 1);
				if (Interlocked.CompareExchange(ref this._count, num2, num) == num)
				{
					return true;
				}
			}
			throw new ObjectDisposedException(typeof(T).ToString());
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00053D6C File Offset: 0x00051F6C
		public void Release(T obj)
		{
			int num2;
			int num3;
			for (;;)
			{
				int num = Volatile.Read(ref this._count);
				if (num > 0)
				{
					num2 = num - 1;
					if (Interlocked.CompareExchange(ref this._count, num2, num) == num)
					{
						break;
					}
				}
				else
				{
					num3 = num + 1;
					if (Interlocked.CompareExchange(ref this._count, num3, num) == num)
					{
						goto Block_3;
					}
				}
			}
			if (num2 == 0)
			{
				obj.OnFinalRelease(false);
			}
			return;
			Block_3:
			if (num3 == -1)
			{
				obj.OnFinalRelease(true);
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00053DD4 File Offset: 0x00051FD4
		public void Dispose(T obj)
		{
			int num2;
			for (;;)
			{
				int num = Volatile.Read(ref this._count);
				if (num < 0)
				{
					break;
				}
				num2 = -1 - num;
				if (Interlocked.CompareExchange(ref this._count, num2, num) == num)
				{
					goto Block_1;
				}
			}
			return;
			Block_1:
			if (num2 == -1)
			{
				obj.OnFinalRelease(true);
			}
		}

		// Token: 0x04000A23 RID: 2595
		private int _count;
	}
}
