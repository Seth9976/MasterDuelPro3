using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000191 RID: 401
	internal sealed class LocalDataStore
	{
		// Token: 0x06000E9D RID: 3741 RVA: 0x0003D0B0 File Offset: 0x0003B2B0
		public LocalDataStore(LocalDataStoreMgr mgr, int InitialCapacity)
		{
			this.m_Manager = mgr;
			this.m_DataTable = new LocalDataStoreElement[InitialCapacity];
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003D0CB File Offset: 0x0003B2CB
		internal void Dispose()
		{
			this.m_Manager.DeleteLocalDataStore(this);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003D0DC File Offset: 0x0003B2DC
		public object GetData(LocalDataStoreSlot slot)
		{
			this.m_Manager.ValidateSlot(slot);
			int slot2 = slot.Slot;
			if (slot2 >= 0)
			{
				if (slot2 >= this.m_DataTable.Length)
				{
					return null;
				}
				LocalDataStoreElement localDataStoreElement = this.m_DataTable[slot2];
				if (localDataStoreElement == null)
				{
					return null;
				}
				if (localDataStoreElement.Cookie == slot.Cookie)
				{
					return localDataStoreElement.Value;
				}
			}
			throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003D140 File Offset: 0x0003B340
		public void SetData(LocalDataStoreSlot slot, object data)
		{
			this.m_Manager.ValidateSlot(slot);
			int slot2 = slot.Slot;
			if (slot2 >= 0)
			{
				LocalDataStoreElement localDataStoreElement = ((slot2 < this.m_DataTable.Length) ? this.m_DataTable[slot2] : null);
				if (localDataStoreElement == null)
				{
					localDataStoreElement = this.PopulateElement(slot);
				}
				if (localDataStoreElement.Cookie == slot.Cookie)
				{
					localDataStoreElement.Value = data;
					return;
				}
			}
			throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003D1AC File Offset: 0x0003B3AC
		internal void FreeData(int slot, long cookie)
		{
			if (slot >= this.m_DataTable.Length)
			{
				return;
			}
			LocalDataStoreElement localDataStoreElement = this.m_DataTable[slot];
			if (localDataStoreElement != null && localDataStoreElement.Cookie == cookie)
			{
				this.m_DataTable[slot] = null;
			}
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003D1E4 File Offset: 0x0003B3E4
		private LocalDataStoreElement PopulateElement(LocalDataStoreSlot slot)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreElement localDataStoreElement;
			try
			{
				Monitor.Enter(this.m_Manager, ref flag);
				int slot2 = slot.Slot;
				if (slot2 < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("LocalDataStoreSlot storage has been freed."));
				}
				if (slot2 >= this.m_DataTable.Length)
				{
					LocalDataStoreElement[] array = new LocalDataStoreElement[this.m_Manager.GetSlotTableLength()];
					Array.Copy(this.m_DataTable, array, this.m_DataTable.Length);
					this.m_DataTable = array;
				}
				if (this.m_DataTable[slot2] == null)
				{
					this.m_DataTable[slot2] = new LocalDataStoreElement(slot.Cookie);
				}
				localDataStoreElement = this.m_DataTable[slot2];
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.m_Manager);
				}
			}
			return localDataStoreElement;
		}

		// Token: 0x040005C9 RID: 1481
		private LocalDataStoreElement[] m_DataTable;

		// Token: 0x040005CA RID: 1482
		private LocalDataStoreMgr m_Manager;
	}
}
