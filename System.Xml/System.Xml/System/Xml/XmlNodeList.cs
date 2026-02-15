using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	/// <summary>Represents an ordered collection of nodes.</summary>
	// Token: 0x020000F4 RID: 244
	public abstract class XmlNodeList : IEnumerable, IDisposable
	{
		/// <summary>Retrieves a node at the given index.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> with the specified index in the collection. If <paramref name="index" /> is greater than or equal to the number of nodes in the list, this returns null.</returns>
		/// <param name="index">The zero-based index into the list of nodes.</param>
		// Token: 0x06000CB8 RID: 3256
		public abstract XmlNode Item(int index);

		/// <summary>Gets the number of nodes in the XmlNodeList.</summary>
		/// <returns>The number of nodes in the XmlNodeList.</returns>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000CB9 RID: 3257
		public abstract int Count { get; }

		/// <summary>Gets an enumerator that iterates through the collection of nodes.</summary>
		/// <returns>An enumerator used to iterate through the collection of nodes.</returns>
		// Token: 0x06000CBA RID: 3258
		public abstract IEnumerator GetEnumerator();

		/// <summary>Gets a node at the given index.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> with the specified index in the collection. If index is greater than or equal to the number of nodes in the list, this returns null.</returns>
		/// <param name="i">The zero-based index into the list of nodes.</param>
		// Token: 0x170002FA RID: 762
		[IndexerName("ItemOf")]
		public virtual XmlNode this[int i]
		{
			get
			{
				return this.Item(i);
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Xml.XmlNodeList" /> class.</summary>
		// Token: 0x06000CBC RID: 3260 RVA: 0x00040C6C File Offset: 0x0003EE6C
		void IDisposable.Dispose()
		{
			this.PrivateDisposeNodeList();
		}

		/// <summary>Disposes resources in the node list privately.</summary>
		// Token: 0x06000CBD RID: 3261 RVA: 0x0000A558 File Offset: 0x00008758
		protected virtual void PrivateDisposeNodeList()
		{
		}
	}
}
