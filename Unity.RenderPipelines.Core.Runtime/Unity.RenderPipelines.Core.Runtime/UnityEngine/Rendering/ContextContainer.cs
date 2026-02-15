using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002F RID: 47
	public class ContextContainer : IDisposable
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00006AE0 File Offset: 0x00004CE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Get<T>() where T : ContextItem, new()
		{
			uint typeId = ContextContainer.TypeId<T>.value;
			if (!this.Contains(typeId))
			{
				throw new InvalidOperationException("Type " + typeof(T).FullName + " has not been created yet.");
			}
			return (T)((object)this.m_Items[(int)typeId].storage);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00006B38 File Offset: 0x00004D38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Create<T>() where T : ContextItem, new()
		{
			uint typeId = ContextContainer.TypeId<T>.value;
			if (this.Contains(typeId))
			{
				throw new InvalidOperationException("Type " + typeof(T).FullName + " has already been created.");
			}
			return this.CreateAndGetData<T>(typeId);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00006B80 File Offset: 0x00004D80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T GetOrCreate<T>() where T : ContextItem, new()
		{
			uint typeId = ContextContainer.TypeId<T>.value;
			if (this.Contains(typeId))
			{
				return (T)((object)this.m_Items[(int)typeId].storage);
			}
			return this.CreateAndGetData<T>(typeId);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00006BBC File Offset: 0x00004DBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains<T>() where T : ContextItem, new()
		{
			uint typeId = ContextContainer.TypeId<T>.value;
			return this.Contains(typeId);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00006BD6 File Offset: 0x00004DD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool Contains(uint typeId)
		{
			return (ulong)typeId < (ulong)((long)this.m_Items.Length) && this.m_Items[(int)typeId].isSet;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00006BF8 File Offset: 0x00004DF8
		private T CreateAndGetData<T>(uint typeId) where T : ContextItem, new()
		{
			if ((long)this.m_Items.Length <= (long)((ulong)typeId))
			{
				ContextContainer.Item[] items = new ContextContainer.Item[math.max((long)((ulong)math.ceilpow2(ContextContainer.s_TypeCount)), (long)(this.m_Items.Length * 2))];
				for (int i = 0; i < this.m_Items.Length; i++)
				{
					items[i] = this.m_Items[i];
				}
				this.m_Items = items;
			}
			this.m_ActiveItemIndices.Add(typeId);
			ContextContainer.Item[] items2 = this.m_Items;
			ref ContextItem ptr = ref items2[(int)typeId].storage;
			if (ptr == null)
			{
				ptr = new T();
			}
			items2[(int)typeId].isSet = true;
			return (T)((object)items2[(int)typeId].storage);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public void Dispose()
		{
			foreach (uint index in this.m_ActiveItemIndices)
			{
				ContextContainer.Item[] items = this.m_Items;
				uint num = index;
				items[(int)num].storage.Reset();
				items[(int)num].isSet = false;
			}
			this.m_ActiveItemIndices.Clear();
		}

		// Token: 0x040000AE RID: 174
		private ContextContainer.Item[] m_Items = new ContextContainer.Item[64];

		// Token: 0x040000AF RID: 175
		private List<uint> m_ActiveItemIndices = new List<uint>();

		// Token: 0x040000B0 RID: 176
		private static uint s_TypeCount;

		// Token: 0x02000030 RID: 48
		private static class TypeId<T>
		{
			// Token: 0x040000B1 RID: 177
			public static uint value = ContextContainer.s_TypeCount++;
		}

		// Token: 0x02000031 RID: 49
		private struct Item
		{
			// Token: 0x040000B2 RID: 178
			public ContextItem storage;

			// Token: 0x040000B3 RID: 179
			public bool isSet;
		}
	}
}
