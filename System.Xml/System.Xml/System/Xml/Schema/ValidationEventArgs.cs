using System;

namespace System.Xml.Schema
{
	/// <summary>Returns detailed information related to the ValidationEventHandler.</summary>
	// Token: 0x020002A1 RID: 673
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x06001F04 RID: 7940 RVA: 0x000B9B57 File Offset: 0x000B7D57
		internal ValidationEventArgs(XmlSchemaException ex)
		{
			this.ex = ex;
			this.severity = XmlSeverityType.Error;
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000B9B6D File Offset: 0x000B7D6D
		internal ValidationEventArgs(XmlSchemaException ex, XmlSeverityType severity)
		{
			this.ex = ex;
			this.severity = severity;
		}

		/// <summary>Gets the severity of the validation event.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSeverityType" /> value representing the severity of the validation event.</returns>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x000B9B83 File Offset: 0x000B7D83
		public XmlSeverityType Severity
		{
			get
			{
				return this.severity;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaException" /> associated with the validation event.</summary>
		/// <returns>The XmlSchemaException associated with the validation event.</returns>
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x000B9B8B File Offset: 0x000B7D8B
		public XmlSchemaException Exception
		{
			get
			{
				return this.ex;
			}
		}

		/// <summary>Gets the text description corresponding to the validation event.</summary>
		/// <returns>The text description.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x000B9B93 File Offset: 0x000B7D93
		public string Message
		{
			get
			{
				return this.ex.Message;
			}
		}

		// Token: 0x04000E4E RID: 3662
		private XmlSchemaException ex;

		// Token: 0x04000E4F RID: 3663
		private XmlSeverityType severity;
	}
}
