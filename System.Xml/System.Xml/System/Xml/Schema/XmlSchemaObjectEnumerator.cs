using System;
using System.Collections;

namespace System.Xml.Schema
{
	/// <summary>Represents the enumerator for the <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" />.</summary>
	// Token: 0x020002EF RID: 751
	public class XmlSchemaObjectEnumerator : IEnumerator
	{
		// Token: 0x060021BB RID: 8635 RVA: 0x000C07ED File Offset: 0x000BE9ED
		internal XmlSchemaObjectEnumerator(IEnumerator enumerator)
		{
			this.enumerator = enumerator;
		}

		/// <summary>Moves to the next item in the collection.</summary>
		/// <returns>false at the end of the collection.</returns>
		// Token: 0x060021BC RID: 8636 RVA: 0x000C07FC File Offset: 0x000BE9FC
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Gets the current <see cref="T:System.Xml.Schema.XmlSchemaObject" /> in the collection.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x000C0809 File Offset: 0x000BEA09
		public XmlSchemaObject Current
		{
			get
			{
				return (XmlSchemaObject)this.enumerator.Current;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaObjectEnumerator.Reset" />.</summary>
		// Token: 0x060021BE RID: 8638 RVA: 0x000C081B File Offset: 0x000BEA1B
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaObjectEnumerator.MoveNext" />.</summary>
		/// <returns>The next <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x060021BF RID: 8639 RVA: 0x000C07FC File Offset: 0x000BE9FC
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaObjectEnumerator.Current" />.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x000C0828 File Offset: 0x000BEA28
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		// Token: 0x04000FB1 RID: 4017
		private IEnumerator enumerator;
	}
}
