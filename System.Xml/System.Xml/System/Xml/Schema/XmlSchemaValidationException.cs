using System;
using System.Runtime.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the exception thrown when XML Schema Definition Language (XSD) schema validation errors and warnings are encountered in an XML document being validated. </summary>
	// Token: 0x02000308 RID: 776
	[Serializable]
	public class XmlSchemaValidationException : XmlSchemaException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects specified.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06002282 RID: 8834 RVA: 0x000A684F File Offset: 0x000A4A4F
		protected XmlSchemaValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Constructs a new <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> object with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> information that contains all the properties of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" />.</summary>
		/// <param name="info">
		///   <see cref="T:System.Runtime.Serialization.SerializationInfo" />
		/// </param>
		/// <param name="context">
		///   <see cref="T:System.Runtime.Serialization.StreamingContext" />
		/// </param>
		// Token: 0x06002283 RID: 8835 RVA: 0x000A6859 File Offset: 0x000A4A59
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class.</summary>
		// Token: 0x06002284 RID: 8836 RVA: 0x000A6863 File Offset: 0x000A4A63
		public XmlSchemaValidationException()
			: base(null)
		{
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000BFF98 File Offset: 0x000BE198
		internal XmlSchemaValidationException(string res, string arg, string sourceUri, int lineNumber, int linePosition)
			: base(res, new string[] { arg }, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000BFFC2 File Offset: 0x000BE1C2
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, int lineNumber, int linePosition)
			: base(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000C338F File Offset: 0x000C158F
		internal XmlSchemaValidationException(string res, string[] args, Exception innerException, string sourceUri, int lineNumber, int linePosition)
			: base(res, args, innerException, sourceUri, lineNumber, linePosition, null)
		{
		}
	}
}
