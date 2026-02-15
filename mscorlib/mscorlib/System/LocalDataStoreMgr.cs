using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000193 RID: 403
	internal sealed class LocalDataStoreMgr
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003D328 File Offset: 0x0003B528
		public LocalDataStoreHolder CreateLocalDataStore()
		{
			LocalDataStore localDataStore = new LocalDataStore(this, this.m_SlotInfoTable.Length);
			LocalDataStoreHolder localDataStoreHolder = new LocalDataStoreHolder(localDataStore);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this.m_ManagedLocalDataStores.Add(localDataStore);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return localDataStoreHolder;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003D384 File Offset: 0x0003B584
		public void DeleteLocalDataStore(LocalDataStore store)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this.m_ManagedLocalDataStores.Remove(store);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003D3CC File Offset: 0x0003B5CC
		public LocalDataStoreSlot AllocateDataSlot()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreSlot localDataStoreSlot2;
			try
			{
				Monitor.Enter(this, ref flag);
				int num = this.m_SlotInfoTable.Length;
				int num2 = this.m_FirstAvailableSlot;
				while (num2 < num && this.m_SlotInfoTable[num2])
				{
					num2++;
				}
				if (num2 >= num)
				{
					int num3;
					if (num < 512)
					{
						num3 = num * 2;
					}
					else
					{
						num3 = num + 128;
					}
					bool[] array = new bool[num3];
					Array.Copy(this.m_SlotInfoTable, array, num);
					this.m_SlotInfoTable = array;
				}
				this.m_SlotInfoTable[num2] = true;
				int num4 = num2;
				long cookieGenerator = this.m_CookieGenerator;
				this.m_CookieGenerator = checked(cookieGenerator + 1L);
				LocalDataStoreSlot localDataStoreSlot = new LocalDataStoreSlot(this, num4, cookieGenerator);
				this.m_FirstAvailableSlot = num2 + 1;
				localDataStoreSlot2 = localDataStoreSlot;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return localDataStoreSlot2;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003D494 File Offset: 0x0003B694
		public LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreSlot localDataStoreSlot2;
			try
			{
				Monitor.Enter(this, ref flag);
				LocalDataStoreSlot localDataStoreSlot = this.AllocateDataSlot();
				this.m_KeyToSlotMap.Add(name, localDataStoreSlot);
				localDataStoreSlot2 = localDataStoreSlot;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return localDataStoreSlot2;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003D4E4 File Offset: 0x0003B6E4
		public LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			LocalDataStoreSlot localDataStoreSlot;
			try
			{
				Monitor.Enter(this, ref flag);
				LocalDataStoreSlot valueOrDefault = this.m_KeyToSlotMap.GetValueOrDefault(name);
				if (valueOrDefault == null)
				{
					localDataStoreSlot = this.AllocateNamedDataSlot(name);
				}
				else
				{
					localDataStoreSlot = valueOrDefault;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
			return localDataStoreSlot;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003D53C File Offset: 0x0003B73C
		public void FreeNamedDataSlot(string name)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this.m_KeyToSlotMap.Remove(name);
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003D584 File Offset: 0x0003B784
		internal void FreeDataSlot(int slot, long cookie)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				for (int i = 0; i < this.m_ManagedLocalDataStores.Count; i++)
				{
					this.m_ManagedLocalDataStores[i].FreeData(slot, cookie);
				}
				this.m_SlotInfoTable[slot] = false;
				if (slot < this.m_FirstAvailableSlot)
				{
					this.m_FirstAvailableSlot = slot;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003D600 File Offset: 0x0003B800
		public void ValidateSlot(LocalDataStoreSlot slot)
		{
			if (slot == null || slot.Manager != this)
			{
				throw new ArgumentException(Environment.GetResourceString("Specified slot number was invalid."));
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003D61E File Offset: 0x0003B81E
		internal int GetSlotTableLength()
		{
			return this.m_SlotInfoTable.Length;
		}

		// Token: 0x040005CE RID: 1486
		private bool[] m_SlotInfoTable = new bool[64];

		// Token: 0x040005CF RID: 1487
		private int m_FirstAvailableSlot;

		// Token: 0x040005D0 RID: 1488
		private List<LocalDataStore> m_ManagedLocalDataStores = new List<LocalDataStore>();

		// Token: 0x040005D1 RID: 1489
		private Dictionary<string, LocalDataStoreSlot> m_KeyToSlotMap = new Dictionary<string, LocalDataStoreSlot>();

		// Token: 0x040005D2 RID: 1490
		private long m_CookieGenerator;
	}
}
