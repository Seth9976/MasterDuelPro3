using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	/// <summary>Provides the collections for contained elements in the <see cref="T:System.Xml.Schema.XmlSchema" /> class (for example, Attributes, AttributeGroups, Elements, and so on).</summary>
	// Token: 0x020002F0 RID: 752
	public class XmlSchemaObjectTable
	{
		// Token: 0x060021C1 RID: 8641 RVA: 0x000C0835 File Offset: 0x000BEA35
		internal XmlSchemaObjectTable()
		{
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000C0853 File Offset: 0x000BEA53
		internal void Add(XmlQualifiedName name, XmlSchemaObject value)
		{
			this.table.Add(name, value);
			this.entries.Add(new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value));
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000C0874 File Offset: 0x000BEA74
		internal void Insert(XmlQualifiedName name, XmlSchemaObject value)
		{
			XmlSchemaObject xmlSchemaObject = null;
			if (this.table.TryGetValue(name, out xmlSchemaObject))
			{
				this.table[name] = value;
				int num = this.FindIndexByValue(xmlSchemaObject);
				this.entries[num] = new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value);
				return;
			}
			this.Add(name, value);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000C08C4 File Offset: 0x000BEAC4
		internal void Replace(XmlQualifiedName name, XmlSchemaObject value)
		{
			XmlSchemaObject xmlSchemaObject;
			if (this.table.TryGetValue(name, out xmlSchemaObject))
			{
				this.table[name] = value;
				int num = this.FindIndexByValue(xmlSchemaObject);
				this.entries[num] = new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value);
			}
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000C0909 File Offset: 0x000BEB09
		internal void Clear()
		{
			this.table.Clear();
			this.entries.Clear();
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000C0924 File Offset: 0x000BEB24
		internal void Remove(XmlQualifiedName name)
		{
			XmlSchemaObject xmlSchemaObject;
			if (this.table.TryGetValue(name, out xmlSchemaObject))
			{
				this.table.Remove(name);
				int num = this.FindIndexByValue(xmlSchemaObject);
				this.entries.RemoveAt(num);
			}
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000C0964 File Offset: 0x000BEB64
		private int FindIndexByValue(XmlSchemaObject xso)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].xso == xso)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the number of items contained in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>The number of items contained in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x000C099E File Offset: 0x000BEB9E
		public int Count
		{
			get
			{
				return this.table.Count;
			}
		}

		/// <summary>Determines if the qualified name specified exists in the collection.</summary>
		/// <returns>true if the qualified name specified exists in the collection; otherwise, false.</returns>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" />.</param>
		// Token: 0x060021C9 RID: 8649 RVA: 0x000C09AB File Offset: 0x000BEBAB
		public bool Contains(XmlQualifiedName name)
		{
			return this.table.ContainsKey(name);
		}

		/// <summary>Returns the element in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> specified by qualified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> of the element in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> specified by qualified name.</returns>
		/// <param name="name">The <see cref="T:System.Xml.XmlQualifiedName" /> of the element to return.</param>
		// Token: 0x1700083E RID: 2110
		public XmlSchemaObject this[XmlQualifiedName name]
		{
			get
			{
				XmlSchemaObject xmlSchemaObject;
				if (this.table.TryGetValue(name, out xmlSchemaObject))
				{
					return xmlSchemaObject;
				}
				return null;
			}
		}

		/// <summary>Returns a collection of all the values for all the elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>A collection of all the values for all the elements in the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x000C09DC File Offset: 0x000BEBDC
		public ICollection Values
		{
			get
			{
				return new XmlSchemaObjectTable.ValuesCollection(this.entries, this.table.Count);
			}
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> that can iterate through <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" />.</returns>
		// Token: 0x060021CC RID: 8652 RVA: 0x000C09F4 File Offset: 0x000BEBF4
		public IDictionaryEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectTable.XSODictionaryEnumerator(this.entries, this.table.Count, XmlSchemaObjectTable.EnumeratorType.DictionaryEntry);
		}

		// Token: 0x04000FB2 RID: 4018
		private Dictionary<XmlQualifiedName, XmlSchemaObject> table = new Dictionary<XmlQualifiedName, XmlSchemaObject>();

		// Token: 0x04000FB3 RID: 4019
		private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries = new List<XmlSchemaObjectTable.XmlSchemaObjectEntry>();

		// Token: 0x020002F1 RID: 753
		internal enum EnumeratorType
		{
			// Token: 0x04000FB5 RID: 4021
			Keys,
			// Token: 0x04000FB6 RID: 4022
			Values,
			// Token: 0x04000FB7 RID: 4023
			DictionaryEntry
		}

		// Token: 0x020002F2 RID: 754
		internal struct XmlSchemaObjectEntry
		{
			// Token: 0x060021CD RID: 8653 RVA: 0x000C0A0D File Offset: 0x000BEC0D
			public XmlSchemaObjectEntry(XmlQualifiedName name, XmlSchemaObject value)
			{
				this.qname = name;
				this.xso = value;
			}

			// Token: 0x04000FB8 RID: 4024
			internal XmlQualifiedName qname;

			// Token: 0x04000FB9 RID: 4025
			internal XmlSchemaObject xso;
		}

		// Token: 0x020002F3 RID: 755
		internal class ValuesCollection : ICollection, IEnumerable
		{
			// Token: 0x060021CE RID: 8654 RVA: 0x000C0A1D File Offset: 0x000BEC1D
			internal ValuesCollection(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size)
			{
				this.entries = entries;
				this.size = size;
			}

			// Token: 0x17000840 RID: 2112
			// (get) Token: 0x060021CF RID: 8655 RVA: 0x000C0A33 File Offset: 0x000BEC33
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000C0A3B File Offset: 0x000BEC3B
			public object SyncRoot
			{
				get
				{
					return ((ICollection)this.entries).SyncRoot;
				}
			}

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x060021D1 RID: 8657 RVA: 0x000C0A48 File Offset: 0x000BEC48
			public bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.entries).IsSynchronized;
				}
			}

			// Token: 0x060021D2 RID: 8658 RVA: 0x000C0A58 File Offset: 0x000BEC58
			public void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				for (int i = 0; i < this.size; i++)
				{
					array.SetValue(this.entries[i].xso, arrayIndex++);
				}
			}

			// Token: 0x060021D3 RID: 8659 RVA: 0x000C0AB0 File Offset: 0x000BECB0
			public IEnumerator GetEnumerator()
			{
				return new XmlSchemaObjectTable.XSOEnumerator(this.entries, this.size, XmlSchemaObjectTable.EnumeratorType.Values);
			}

			// Token: 0x04000FBA RID: 4026
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04000FBB RID: 4027
			private int size;
		}

		// Token: 0x020002F4 RID: 756
		internal class XSOEnumerator : IEnumerator
		{
			// Token: 0x060021D4 RID: 8660 RVA: 0x000C0AC4 File Offset: 0x000BECC4
			internal XSOEnumerator(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size, XmlSchemaObjectTable.EnumeratorType enumType)
			{
				this.entries = entries;
				this.size = size;
				this.enumType = enumType;
				this.currentIndex = -1;
			}

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x060021D5 RID: 8661 RVA: 0x000C0AE8 File Offset: 0x000BECE8
			public object Current
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
					}
					switch (this.enumType)
					{
					case XmlSchemaObjectTable.EnumeratorType.Keys:
						return this.currentKey;
					case XmlSchemaObjectTable.EnumeratorType.Values:
						return this.currentValue;
					case XmlSchemaObjectTable.EnumeratorType.DictionaryEntry:
						return new DictionaryEntry(this.currentKey, this.currentValue);
					default:
						return null;
					}
				}
			}

			// Token: 0x060021D6 RID: 8662 RVA: 0x000C0B8C File Offset: 0x000BED8C
			public bool MoveNext()
			{
				if (this.currentIndex >= this.size - 1)
				{
					this.currentValue = null;
					this.currentKey = null;
					return false;
				}
				this.currentIndex++;
				this.currentValue = this.entries[this.currentIndex].xso;
				this.currentKey = this.entries[this.currentIndex].qname;
				return true;
			}

			// Token: 0x060021D7 RID: 8663 RVA: 0x000C0C00 File Offset: 0x000BEE00
			public void Reset()
			{
				this.currentIndex = -1;
				this.currentValue = null;
				this.currentKey = null;
			}

			// Token: 0x04000FBC RID: 4028
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04000FBD RID: 4029
			private XmlSchemaObjectTable.EnumeratorType enumType;

			// Token: 0x04000FBE RID: 4030
			protected int currentIndex;

			// Token: 0x04000FBF RID: 4031
			protected int size;

			// Token: 0x04000FC0 RID: 4032
			protected XmlQualifiedName currentKey;

			// Token: 0x04000FC1 RID: 4033
			protected XmlSchemaObject currentValue;
		}

		// Token: 0x020002F5 RID: 757
		internal class XSODictionaryEnumerator : XmlSchemaObjectTable.XSOEnumerator, IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060021D8 RID: 8664 RVA: 0x000C0C17 File Offset: 0x000BEE17
			internal XSODictionaryEnumerator(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size, XmlSchemaObjectTable.EnumeratorType enumType)
				: base(entries, size, enumType)
			{
			}

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x060021D9 RID: 8665 RVA: 0x000C0C24 File Offset: 0x000BEE24
			public DictionaryEntry Entry
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x17000845 RID: 2117
			// (get) Token: 0x060021DA RID: 8666 RVA: 0x000C0C98 File Offset: 0x000BEE98
			public object Key
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
					}
					return this.currentKey;
				}
			}

			// Token: 0x17000846 RID: 2118
			// (get) Token: 0x060021DB RID: 8667 RVA: 0x000C0D00 File Offset: 0x000BEF00
			public object Value
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
					}
					return this.currentValue;
				}
			}
		}
	}
}
