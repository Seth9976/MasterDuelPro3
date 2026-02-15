using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	/// <summary>Customizes SOAP generation and processing for target types. This class cannot be inherited.</summary>
	// Token: 0x0200046F RID: 1135
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	[ComVisible(true)]
	public sealed class SoapTypeAttribute : SoapAttribute
	{
		/// <summary>Gets or sets a value indicating whether the target of the current attribute will be serialized as an XML attribute instead of an XML field.</summary>
		/// <returns>The current implementation always returns false.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">An attempt was made to set the current property. </exception>
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x00096809 File Offset: 0x00094A09
		public override bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
		}

		/// <summary>Gets or sets the XML element name.</summary>
		/// <returns>The XML element name.</returns>
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x00096811 File Offset: 0x00094A11
		public string XmlElementName
		{
			get
			{
				return this._xmlElementName;
			}
		}

		/// <summary>Gets or sets the XML namespace that is used during serialization of the target object type.</summary>
		/// <returns>The XML namespace that is used during serialization of the target object type.</returns>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x00096819 File Offset: 0x00094A19
		public override string XmlNamespace
		{
			get
			{
				return this._xmlNamespace;
			}
		}

		/// <summary>Gets or sets the XML type name for the target object type.</summary>
		/// <returns>The XML type name for the target object type.</returns>
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x00096821 File Offset: 0x00094A21
		public string XmlTypeName
		{
			get
			{
				return this._xmlTypeName;
			}
		}

		/// <summary>Gets or sets the XML type namespace for the current object type.</summary>
		/// <returns>The XML type namespace for the current object type.</returns>
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x00096829 File Offset: 0x00094A29
		public string XmlTypeNamespace
		{
			get
			{
				return this._xmlTypeNamespace;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x00096831 File Offset: 0x00094A31
		internal bool IsInteropXmlElement
		{
			get
			{
				return this._isElement;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x00096839 File Offset: 0x00094A39
		internal bool IsInteropXmlType
		{
			get
			{
				return this._isType;
			}
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00096844 File Offset: 0x00094A44
		internal override void SetReflectionObject(object reflectionObject)
		{
			Type type = (Type)reflectionObject;
			if (this._xmlElementName == null)
			{
				this._xmlElementName = type.Name;
			}
			if (this._xmlTypeName == null)
			{
				this._xmlTypeName = type.Name;
			}
			if (this._xmlTypeNamespace == null)
			{
				string text;
				if (type.Assembly == typeof(object).Assembly)
				{
					text = string.Empty;
				}
				else
				{
					text = type.Assembly.GetName().Name;
				}
				this._xmlTypeNamespace = SoapServices.CodeXmlNamespaceForClrTypeNamespace(type.Namespace, text);
			}
			if (this._xmlNamespace == null)
			{
				this._xmlNamespace = this._xmlTypeNamespace;
			}
		}

		// Token: 0x040011AD RID: 4525
		private bool _useAttribute;

		// Token: 0x040011AE RID: 4526
		private string _xmlElementName;

		// Token: 0x040011AF RID: 4527
		private string _xmlNamespace;

		// Token: 0x040011B0 RID: 4528
		private string _xmlTypeName;

		// Token: 0x040011B1 RID: 4529
		private string _xmlTypeNamespace;

		// Token: 0x040011B2 RID: 4530
		private bool _isType;

		// Token: 0x040011B3 RID: 4531
		private bool _isElement;
	}
}
