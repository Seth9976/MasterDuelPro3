using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020001BA RID: 442
	internal class WorkItems
	{
		// Token: 0x1700050D RID: 1293
		internal ImportStructWorkItem this[int index]
		{
			get
			{
				return (ImportStructWorkItem)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x000690D4 File Offset: 0x000672D4
		internal int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x000690E1 File Offset: 0x000672E1
		internal void Add(ImportStructWorkItem item)
		{
			this.list.Add(item);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x000690F0 File Offset: 0x000672F0
		internal bool Contains(StructMapping mapping)
		{
			return this.IndexOf(mapping) >= 0;
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00069100 File Offset: 0x00067300
		internal int IndexOf(StructMapping mapping)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Mapping == mapping)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00069130 File Offset: 0x00067330
		internal void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x04000997 RID: 2455
		private ArrayList list = new ArrayList();
	}
}
