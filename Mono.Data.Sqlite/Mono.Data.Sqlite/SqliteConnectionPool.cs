using System;
using System.Collections.Generic;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200000C RID: 12
	internal static class SqliteConnectionPool
	{
		// Token: 0x060000EA RID: 234 RVA: 0x000080A4 File Offset: 0x000062A4
		internal static SqliteConnectionHandle Remove(string fileName, int maxPoolSize, out int version)
		{
			SortedList<string, SqliteConnectionPool.Pool> connections = SqliteConnectionPool._connections;
			SqliteConnectionHandle sqliteConnectionHandle;
			lock (connections)
			{
				version = SqliteConnectionPool._poolVersion;
				SqliteConnectionPool.Pool pool;
				if (!SqliteConnectionPool._connections.TryGetValue(fileName, out pool))
				{
					pool = new SqliteConnectionPool.Pool(SqliteConnectionPool._poolVersion, maxPoolSize);
					SqliteConnectionPool._connections.Add(fileName, pool);
					sqliteConnectionHandle = null;
				}
				else
				{
					version = pool.PoolVersion;
					pool.MaxPoolSize = maxPoolSize;
					SqliteConnectionPool.ResizePool(pool, false);
					while (pool.Queue.Count > 0)
					{
						WeakReference weakReference = pool.Queue.Dequeue();
						SqliteConnectionHandle sqliteConnectionHandle2 = weakReference.Target as SqliteConnectionHandle;
						if (sqliteConnectionHandle2 != null)
						{
							return sqliteConnectionHandle2;
						}
					}
					sqliteConnectionHandle = null;
				}
			}
			return sqliteConnectionHandle;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008170 File Offset: 0x00006370
		internal static void Add(string fileName, SqliteConnectionHandle hdl, int version)
		{
			SortedList<string, SqliteConnectionPool.Pool> connections = SqliteConnectionPool._connections;
			lock (connections)
			{
				SqliteConnectionPool.Pool pool;
				if (SqliteConnectionPool._connections.TryGetValue(fileName, out pool) && version == pool.PoolVersion)
				{
					SqliteConnectionPool.ResizePool(pool, true);
					pool.Queue.Enqueue(new WeakReference(hdl, false));
					GC.KeepAlive(hdl);
				}
				else
				{
					hdl.Close();
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000081F0 File Offset: 0x000063F0
		private static void ResizePool(SqliteConnectionPool.Pool queue, bool forAdding)
		{
			int num = queue.MaxPoolSize;
			if (forAdding && num > 0)
			{
				num--;
			}
			while (queue.Queue.Count > num)
			{
				WeakReference weakReference = queue.Queue.Dequeue();
				SqliteConnectionHandle sqliteConnectionHandle = weakReference.Target as SqliteConnectionHandle;
				if (sqliteConnectionHandle != null)
				{
					sqliteConnectionHandle.Dispose();
				}
			}
		}

		// Token: 0x0400002E RID: 46
		private static SortedList<string, SqliteConnectionPool.Pool> _connections = new SortedList<string, SqliteConnectionPool.Pool>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400002F RID: 47
		private static int _poolVersion = 1;

		// Token: 0x0200000D RID: 13
		internal class Pool
		{
			// Token: 0x060000ED RID: 237 RVA: 0x0000824F File Offset: 0x0000644F
			internal Pool(int version, int maxSize)
			{
				this.PoolVersion = version;
				this.MaxPoolSize = maxSize;
			}

			// Token: 0x04000030 RID: 48
			internal readonly Queue<WeakReference> Queue = new Queue<WeakReference>();

			// Token: 0x04000031 RID: 49
			internal int PoolVersion;

			// Token: 0x04000032 RID: 50
			internal int MaxPoolSize;
		}
	}
}
