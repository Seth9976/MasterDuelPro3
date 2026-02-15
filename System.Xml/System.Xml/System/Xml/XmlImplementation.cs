using System;

namespace System.Xml
{
	/// <summary>Defines the context for a set of <see cref="T:System.Xml.XmlDocument" /> objects.</summary>
	// Token: 0x020000E9 RID: 233
	public class XmlImplementation
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlImplementation" /> class.</summary>
		// Token: 0x06000C18 RID: 3096 RVA: 0x0003DB73 File Offset: 0x0003BD73
		public XmlImplementation()
			: this(new NameTable())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlImplementation" /> class with the <see cref="T:System.Xml.XmlNameTable" /> specified.</summary>
		/// <param name="nt">An <see cref="T:System.Xml.XmlNameTable" /> object.</param>
		// Token: 0x06000C19 RID: 3097 RVA: 0x0003DB80 File Offset: 0x0003BD80
		public XmlImplementation(XmlNameTable nt)
		{
			this.nameTable = nt;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlDocument" />.</summary>
		/// <returns>The new XmlDocument object.</returns>
		// Token: 0x06000C1A RID: 3098 RVA: 0x0003DB8F File Offset: 0x0003BD8F
		public virtual XmlDocument CreateDocument()
		{
			return new XmlDocument(this);
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0003DB97 File Offset: 0x0003BD97
		internal XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x04000647 RID: 1607
		private XmlNameTable nameTable;
	}
}
