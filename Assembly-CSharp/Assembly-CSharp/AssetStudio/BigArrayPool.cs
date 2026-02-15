using System;
using System.Buffers;

namespace AssetStudio
{
	// Token: 0x0200008D RID: 141
	public static class BigArrayPool<T>
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000BC06 File Offset: 0x00009E06
		public static ArrayPool<T> Shared
		{
			get
			{
				return BigArrayPool<T>.s_shared;
			}
		}

		// Token: 0x04000375 RID: 885
		private static readonly ArrayPool<T> s_shared = ArrayPool<T>.Create(67108864, 3);
	}
}
