using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml.Linq
{
	/// <summary>Represents the abstract concept of a node (element, comment, document type, processing instruction, or text node) in the XML tree.  </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200001E RID: 30
	public abstract class XNode : XObject
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004DF6 File Offset: 0x00002FF6
		internal XNode()
		{
		}

		/// <summary>Removes this node from its parent.</summary>
		/// <exception cref="T:System.InvalidOperationException">The parent is null.</exception>
		// Token: 0x060000BF RID: 191 RVA: 0x00004DFE File Offset: 0x00002FFE
		public void Remove()
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this.parent.RemoveNode(this);
		}

		/// <summary>Returns the indented XML for this node.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the indented XML.</returns>
		// Token: 0x060000C0 RID: 192 RVA: 0x00004E1F File Offset: 0x0000301F
		public override string ToString()
		{
			return this.GetXmlString(base.GetSaveOptionsFromAnnotations());
		}

		/// <summary>Writes this node to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000C1 RID: 193
		public abstract void WriteTo(XmlWriter writer);

		// Token: 0x060000C2 RID: 194 RVA: 0x00002784 File Offset: 0x00000984
		internal virtual void AppendText(StringBuilder sb)
		{
		}

		// Token: 0x060000C3 RID: 195
		internal abstract XNode CloneNode();

		// Token: 0x060000C4 RID: 196 RVA: 0x00004E30 File Offset: 0x00003030
		private string GetXmlString(SaveOptions o)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.OmitXmlDeclaration = true;
				if ((o & SaveOptions.DisableFormatting) == SaveOptions.None)
				{
					xmlWriterSettings.Indent = true;
				}
				if ((o & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None)
				{
					xmlWriterSettings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
				}
				if (this is XText)
				{
					xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
				}
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
				{
					XDocument xdocument = this as XDocument;
					if (xdocument != null)
					{
						xdocument.WriteContentTo(xmlWriter);
					}
					else
					{
						this.WriteTo(xmlWriter);
					}
				}
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x0400004F RID: 79
		internal XNode next;
	}
}
