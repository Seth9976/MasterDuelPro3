using System;

namespace System
{
	// Token: 0x0200018F RID: 399
	internal sealed class LocalDataStoreHolder
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x0003D039 File Offset: 0x0003B239
		public LocalDataStoreHolder(LocalDataStore store)
		{
			this.m_Store = store;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003D048 File Offset: 0x0003B248
		protected override void Finalize()
		{
			try
			{
				LocalDataStore store = this.m_Store;
				if (store != null)
				{
					store.Dispose();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0003D080 File Offset: 0x0003B280
		public LocalDataStore Store
		{
			get
			{
				return this.m_Store;
			}
		}

		// Token: 0x040005C6 RID: 1478
		private LocalDataStore m_Store;
	}
}
