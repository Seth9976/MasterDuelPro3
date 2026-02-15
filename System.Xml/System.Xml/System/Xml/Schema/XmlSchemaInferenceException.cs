using System;
using System.Runtime.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Returns information about errors encountered by the <see cref="T:System.Xml.Schema.XmlSchemaInference" /> class while inferring a schema from an XML document.</summary>
	// Token: 0x02000287 RID: 647
	[Serializable]
	public class XmlSchemaInferenceException : XmlSchemaException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects specified that contain all the properties of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06001D44 RID: 7492 RVA: 0x000A684F File Offset: 0x000A4A4F
		protected XmlSchemaInferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Streams all the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> object properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object specified for the <see cref="T:System.Runtime.Serialization.StreamingContext" /> object specified.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06001D45 RID: 7493 RVA: 0x000A6859 File Offset: 0x000A4A59
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInferenceException" /> class.</summary>
		// Token: 0x06001D46 RID: 7494 RVA: 0x000A6863 File Offset: 0x000A4A63
		public XmlSchemaInferenceException()
			: base(null)
		{
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000A686C File Offset: 0x000A4A6C
		internal XmlSchemaInferenceException(string res, string arg)
			: base(res, new string[] { arg }, null, null, 0, 0, null)
		{
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x000A6884 File Offset: 0x000A4A84
		internal XmlSchemaInferenceException(string res, int lineNumber, int linePosition)
			: base(res, null, null, null, lineNumber, linePosition, null)
		{
		}
	}
}
