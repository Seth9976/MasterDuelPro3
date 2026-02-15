using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Encapsulates a memory slot to store local data. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000192 RID: 402
	[ComVisible(true)]
	public sealed class LocalDataStoreSlot
	{
		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003D2A0 File Offset: 0x0003B4A0
		internal LocalDataStoreSlot(LocalDataStoreMgr mgr, int slot, long cookie)
		{
			this.m_mgr = mgr;
			this.m_slot = slot;
			this.m_cookie = cookie;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0003D2BD File Offset: 0x0003B4BD
		internal LocalDataStoreMgr Manager
		{
			get
			{
				return this.m_mgr;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0003D2C5 File Offset: 0x0003B4C5
		internal int Slot
		{
			get
			{
				return this.m_slot;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0003D2CD File Offset: 0x0003B4CD
		internal long Cookie
		{
			get
			{
				return this.m_cookie;
			}
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003D2D8 File Offset: 0x0003B4D8
		protected override void Finalize()
		{
			try
			{
				LocalDataStoreMgr mgr = this.m_mgr;
				if (mgr != null)
				{
					int slot = this.m_slot;
					this.m_slot = -1;
					mgr.FreeDataSlot(slot, this.m_cookie);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x040005CB RID: 1483
		private LocalDataStoreMgr m_mgr;

		// Token: 0x040005CC RID: 1484
		private int m_slot;

		// Token: 0x040005CD RID: 1485
		private long m_cookie;
	}
}
