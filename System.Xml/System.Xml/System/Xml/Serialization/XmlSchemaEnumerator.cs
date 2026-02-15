using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Enables iteration over a collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects. </summary>
	// Token: 0x020001C4 RID: 452
	public class XmlSchemaEnumerator : IEnumerator<XmlSchema>, IDisposable, IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaEnumerator" /> class. </summary>
		/// <param name="list">The <see cref="T:System.Xml.Serialization.XmlSchemas" /> object you want to iterate over.</param>
		// Token: 0x06001612 RID: 5650 RVA: 0x0007098B File Offset: 0x0006EB8B
		public XmlSchemaEnumerator(XmlSchemas list)
		{
			this.list = list;
			this.idx = -1;
			this.end = list.Count - 1;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Xml.Serialization.XmlSchemaEnumerator" />.</summary>
		// Token: 0x06001613 RID: 5651 RVA: 0x0000A558 File Offset: 0x00008758
		public void Dispose()
		{
		}

		/// <summary>Advances the enumerator to the next item in the collection.</summary>
		/// <returns>true if the move is successful; otherwise, false.</returns>
		// Token: 0x06001614 RID: 5652 RVA: 0x000709AF File Offset: 0x0006EBAF
		public bool MoveNext()
		{
			if (this.idx >= this.end)
			{
				return false;
			}
			this.idx++;
			return true;
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchema" /> object in the collection.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x000709D0 File Offset: 0x0006EBD0
		public XmlSchema Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		/// <summary>Gets the current element in the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</summary>
		/// <returns>The current element in the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</returns>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x000709D0 File Offset: 0x0006EBD0
		object IEnumerator.Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</summary>
		// Token: 0x06001617 RID: 5655 RVA: 0x000709E3 File Offset: 0x0006EBE3
		void IEnumerator.Reset()
		{
			this.idx = -1;
		}

		// Token: 0x040009C0 RID: 2496
		private XmlSchemas list;

		// Token: 0x040009C1 RID: 2497
		private int idx;

		// Token: 0x040009C2 RID: 2498
		private int end;
	}
}
