using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of attribute objects that control how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes and deserializes SOAP methods.</summary>
	// Token: 0x0200018A RID: 394
	public class SoapAttributes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapAttributes" /> class.</summary>
		// Token: 0x06001292 RID: 4754 RVA: 0x00002127 File Offset: 0x00000327
		public SoapAttributes()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapAttributes" /> class using the specified custom type.</summary>
		/// <param name="provider">Any object that implements the <see cref="T:System.Reflection.ICustomAttributeProvider" /> interface, such as the <see cref="T:System.Type" /> class.</param>
		// Token: 0x06001293 RID: 4755 RVA: 0x00059A94 File Offset: 0x00057C94
		public SoapAttributes(ICustomAttributeProvider provider)
		{
			object[] customAttributes = provider.GetCustomAttributes(false);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] is SoapIgnoreAttribute || customAttributes[i] is ObsoleteAttribute)
				{
					this.soapIgnore = true;
					break;
				}
				if (customAttributes[i] is SoapElementAttribute)
				{
					this.soapElement = (SoapElementAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is SoapAttributeAttribute)
				{
					this.soapAttribute = (SoapAttributeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is SoapTypeAttribute)
				{
					this.soapType = (SoapTypeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is SoapEnumAttribute)
				{
					this.soapEnum = (SoapEnumAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is DefaultValueAttribute)
				{
					this.soapDefaultValue = ((DefaultValueAttribute)customAttributes[i]).Value;
				}
			}
			if (this.soapIgnore)
			{
				this.soapElement = null;
				this.soapAttribute = null;
				this.soapType = null;
				this.soapEnum = null;
				this.soapDefaultValue = null;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00059B94 File Offset: 0x00057D94
		internal SoapAttributeFlags SoapFlags
		{
			get
			{
				SoapAttributeFlags soapAttributeFlags = (SoapAttributeFlags)0;
				if (this.soapElement != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Element;
				}
				if (this.soapAttribute != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Attribute;
				}
				if (this.soapEnum != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Enum;
				}
				if (this.soapType != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Type;
				}
				return soapAttributeFlags;
			}
		}

		/// <summary>Gets or sets an object that instructs the <see cref="T:System.Xml.Serialization.XmlSerializer" /> how to serialize an object type into encoded SOAP XML.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapTypeAttribute" /> that either overrides a <see cref="T:System.Xml.Serialization.SoapTypeAttribute" /> applied to a class declaration, or is applied to a class declaration.</returns>
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00059BD4 File Offset: 0x00057DD4
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x00059BDC File Offset: 0x00057DDC
		public SoapTypeAttribute SoapType
		{
			get
			{
				return this.soapType;
			}
			set
			{
				this.soapType = value;
			}
		}

		/// <summary>Gets or sets an object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a SOAP enumeration.</summary>
		/// <returns>An object that specifies how the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes an enumeration member.</returns>
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00059BE5 File Offset: 0x00057DE5
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x00059BED File Offset: 0x00057DED
		public SoapEnumAttribute SoapEnum
		{
			get
			{
				return this.soapEnum;
			}
			set
			{
				this.soapEnum = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> serializes a public field or property as encoded SOAP XML.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must not serialize the field or property; otherwise, false.</returns>
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x00059BF6 File Offset: 0x00057DF6
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x00059BFE File Offset: 0x00057DFE
		public bool SoapIgnore
		{
			get
			{
				return this.soapIgnore;
			}
			set
			{
				this.soapIgnore = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> to override.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> to override.</returns>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x00059C07 File Offset: 0x00057E07
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x00059C0F File Offset: 0x00057E0F
		public SoapElementAttribute SoapElement
		{
			get
			{
				return this.soapElement;
			}
			set
			{
				this.soapElement = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.SoapAttributeAttribute" /> to override.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributeAttribute" /> that overrides the behavior of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> when the member is serialized.</returns>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00059C18 File Offset: 0x00057E18
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x00059C20 File Offset: 0x00057E20
		public SoapAttributeAttribute SoapAttribute
		{
			get
			{
				return this.soapAttribute;
			}
			set
			{
				this.soapAttribute = value;
			}
		}

		/// <summary>Gets or sets the default value of an XML element or attribute.</summary>
		/// <returns>An object that represents the default value of an XML element or attribute.</returns>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00059C29 File Offset: 0x00057E29
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x00059C31 File Offset: 0x00057E31
		public object SoapDefaultValue
		{
			get
			{
				return this.soapDefaultValue;
			}
			set
			{
				this.soapDefaultValue = value;
			}
		}

		// Token: 0x040008C5 RID: 2245
		private bool soapIgnore;

		// Token: 0x040008C6 RID: 2246
		private SoapTypeAttribute soapType;

		// Token: 0x040008C7 RID: 2247
		private SoapElementAttribute soapElement;

		// Token: 0x040008C8 RID: 2248
		private SoapAttributeAttribute soapAttribute;

		// Token: 0x040008C9 RID: 2249
		private SoapEnumAttribute soapEnum;

		// Token: 0x040008CA RID: 2250
		private object soapDefaultValue;
	}
}
