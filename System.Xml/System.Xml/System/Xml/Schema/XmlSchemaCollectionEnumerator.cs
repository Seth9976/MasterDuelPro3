using System;
using System.Collections;

namespace System.Xml.Schema
{
	/// <summary>Supports a simple iteration over a collection. This class cannot be inherited. </summary>
	// Token: 0x020002C1 RID: 705
	public sealed class XmlSchemaCollectionEnumerator : IEnumerator
	{
		// Token: 0x06002061 RID: 8289 RVA: 0x000BEAC0 File Offset: 0x000BCCC0
		internal XmlSchemaCollectionEnumerator(Hashtable collection)
		{
			this.enumerator = collection.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollectionEnumerator.System.Collections.IEnumerator.Reset" />.</summary>
		// Token: 0x06002062 RID: 8290 RVA: 0x000BEAD4 File Offset: 0x000BCCD4
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollectionEnumerator.MoveNext" />.</summary>
		/// <returns>Returns the next node.</returns>
		// Token: 0x06002063 RID: 8291 RVA: 0x000BEAE1 File Offset: 0x000BCCE1
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Advances the enumerator to the next schema in the collection.</summary>
		/// <returns>true if the move was successful; false if the enumerator has passed the end of the collection.</returns>
		// Token: 0x06002064 RID: 8292 RVA: 0x000BEAE1 File Offset: 0x000BCCE1
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollectionEnumerator.Current" />.</summary>
		/// <returns>Returns the current node.</returns>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x000BEAEE File Offset: 0x000BCCEE
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Xml.Schema.XmlSchema" /> in the collection.</summary>
		/// <returns>The current XmlSchema in the collection.</returns>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x000BEAF8 File Offset: 0x000BCCF8
		public XmlSchema Current
		{
			get
			{
				XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.enumerator.Value;
				if (xmlSchemaCollectionNode != null)
				{
					return xmlSchemaCollectionNode.Schema;
				}
				return null;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x000BEB21 File Offset: 0x000BCD21
		internal XmlSchemaCollectionNode CurrentNode
		{
			get
			{
				return (XmlSchemaCollectionNode)this.enumerator.Value;
			}
		}

		// Token: 0x04000F22 RID: 3874
		private IDictionaryEnumerator enumerator;
	}
}
