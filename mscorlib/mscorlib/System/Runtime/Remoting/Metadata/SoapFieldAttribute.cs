using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for a field. This class cannot be inherited.</summary>
	// Token: 0x0200046C RID: 1132
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class SoapFieldAttribute : SoapAttribute
	{
		/// <summary>Gets or sets the XML element name of the field contained in the <see cref="T:System.Runtime.Remoting.Metadata.SoapFieldAttribute" /> attribute.</summary>
		/// <returns>The XML element name of the field contained in this attribute.</returns>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x0009672D File Offset: 0x0009492D
		public string XmlElementName
		{
			get
			{
				return this._elementName;
			}
		}

		/// <summary>Returns a value indicating whether the current attribute contains interop XML element values.</summary>
		/// <returns>true if the current attribute contains interop XML element values; otherwise, false.</returns>
		// Token: 0x060024BD RID: 9405 RVA: 0x00096735 File Offset: 0x00094935
		public bool IsInteropXmlElement()
		{
			return this._isElement;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x00096740 File Offset: 0x00094940
		internal override void SetReflectionObject(object reflectionObject)
		{
			FieldInfo fieldInfo = (FieldInfo)reflectionObject;
			if (this._elementName == null)
			{
				this._elementName = fieldInfo.Name;
			}
		}

		// Token: 0x040011A5 RID: 4517
		private string _elementName;

		// Token: 0x040011A6 RID: 4518
		private bool _isElement;
	}
}
