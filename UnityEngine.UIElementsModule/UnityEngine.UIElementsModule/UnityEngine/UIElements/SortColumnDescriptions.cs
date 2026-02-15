using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011E RID: 286
	[DefaultMember("Item")]
	[UxmlObject]
	public class SortColumnDescriptions : ICollection<SortColumnDescription>, IEnumerable<SortColumnDescription>, IEnumerable
	{
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060008EA RID: 2282 RVA: 0x0002ABF0 File Offset: 0x00028DF0
		// (remove) Token: 0x060008EB RID: 2283 RVA: 0x0002AC28 File Offset: 0x00028E28
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action changed;

		// Token: 0x060008EC RID: 2284 RVA: 0x0002AC60 File Offset: 0x00028E60
		public IEnumerator<SortColumnDescription> GetEnumerator()
		{
			return this.m_Descriptions.GetEnumerator();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002AC80 File Offset: 0x00028E80
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002AC98 File Offset: 0x00028E98
		public void Add(SortColumnDescription item)
		{
			this.Insert(this.m_Descriptions.Count, item);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002ACB0 File Offset: 0x00028EB0
		public void Clear()
		{
			while (this.m_Descriptions.Count > 0)
			{
				this.Remove(this.m_Descriptions[0]);
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002ACE8 File Offset: 0x00028EE8
		public bool Contains(SortColumnDescription item)
		{
			return this.m_Descriptions.Contains(item);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002AD06 File Offset: 0x00028F06
		public void CopyTo(SortColumnDescription[] array, int arrayIndex)
		{
			this.m_Descriptions.CopyTo(array, arrayIndex);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002AD18 File Offset: 0x00028F18
		public bool Remove(SortColumnDescription desc)
		{
			bool flag = desc == null;
			if (flag)
			{
				throw new ArgumentException("Cannot remove null description");
			}
			bool flag2 = this.m_Descriptions.Remove(desc);
			bool flag3;
			if (flag2)
			{
				desc.column = null;
				desc.changed -= this.OnDescriptionChanged;
				Action action = this.changed;
				if (action != null)
				{
					action();
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002AD80 File Offset: 0x00028F80
		private void OnDescriptionChanged(SortColumnDescription desc)
		{
			Action action = this.changed;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0002AD95 File Offset: 0x00028F95
		public int Count
		{
			get
			{
				return this.m_Descriptions.Count;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0002ADA2 File Offset: 0x00028FA2
		public bool IsReadOnly
		{
			get
			{
				return this.m_Descriptions.IsReadOnly;
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002ADB0 File Offset: 0x00028FB0
		public void Insert(int index, SortColumnDescription desc)
		{
			bool flag = desc == null;
			if (flag)
			{
				throw new ArgumentException("Cannot insert null description");
			}
			bool flag2 = this.Contains(desc);
			if (flag2)
			{
				throw new ArgumentException("Already contains this description");
			}
			this.m_Descriptions.Insert(index, desc);
			desc.changed += this.OnDescriptionChanged;
			Action action = this.changed;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x0400059A RID: 1434
		[SerializeField]
		private readonly IList<SortColumnDescription> m_Descriptions = new List<SortColumnDescription>();

		// Token: 0x0200011F RID: 287
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory<T> : UxmlObjectFactory<T, SortColumnDescriptions.UxmlObjectTraits<T>> where T : SortColumnDescriptions, new()
		{
		}

		// Token: 0x02000120 RID: 288
		[Obsolete("UxmlObjectFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory : SortColumnDescriptions.UxmlObjectFactory<SortColumnDescriptions>
		{
		}

		// Token: 0x02000121 RID: 289
		[Obsolete("UxmlObjectTraits<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectTraits<T> : UnityEngine.UIElements.UxmlObjectTraits<T> where T : SortColumnDescriptions
		{
			// Token: 0x060008FA RID: 2298 RVA: 0x0002AE40 File Offset: 0x00029040
			public override void Init(ref T obj, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ref obj, bag, cc);
				List<SortColumnDescription> sortColumnDescriptions = this.m_SortColumnDescriptions.GetValueFromBag(bag, cc);
				bool flag = sortColumnDescriptions != null;
				if (flag)
				{
					foreach (SortColumnDescription d in sortColumnDescriptions)
					{
						obj.Add(d);
					}
				}
			}

			// Token: 0x0400059C RID: 1436
			private readonly UxmlObjectListAttributeDescription<SortColumnDescription> m_SortColumnDescriptions = new UxmlObjectListAttributeDescription<SortColumnDescription>();
		}
	}
}
