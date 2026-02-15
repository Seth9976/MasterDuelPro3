using System;

namespace System.Configuration
{
	/// <summary>Contains meta-information about an individual element within the configuration. This class cannot be inherited.</summary>
	// Token: 0x0200002A RID: 42
	public sealed class ElementInformation
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00005D30 File Offset: 0x00003F30
		internal ElementInformation(ConfigurationElement owner, PropertyInformation propertyInfo)
		{
			this.propertyInfo = propertyInfo;
			this.owner = owner;
			this.properties = new PropertyInformationCollection();
			foreach (object obj in owner.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				this.properties.Add(new PropertyInformation(owner, configurationProperty));
			}
		}

		/// <summary>Gets the source file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object originated.</summary>
		/// <returns>The source file where the associated <see cref="T:System.Configuration.ConfigurationElement" /> object originated.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005DB4 File Offset: 0x00003FB4
		public string Source
		{
			get
			{
				if (this.propertyInfo == null)
				{
					return null;
				}
				return this.propertyInfo.Source;
			}
		}

		/// <summary>Gets the type of the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The type of the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005DCB File Offset: 0x00003FCB
		public Type Type
		{
			get
			{
				if (this.propertyInfo == null)
				{
					return this.owner.GetType();
				}
				return this.propertyInfo.Type;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.PropertyInformationCollection" /> collection of the properties in the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.PropertyInformationCollection" /> collection of the properties in the associated <see cref="T:System.Configuration.ConfigurationElement" /> object.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005DEC File Offset: 0x00003FEC
		public PropertyInformationCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005DF4 File Offset: 0x00003FF4
		internal void Reset(ElementInformation parentInfo)
		{
			foreach (object obj in this.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				PropertyInformation propertyInformation2 = parentInfo.Properties[propertyInformation.Name];
				propertyInformation.Reset(propertyInformation2);
			}
		}

		// Token: 0x04000099 RID: 153
		private readonly PropertyInformation propertyInfo;

		// Token: 0x0400009A RID: 154
		private readonly ConfigurationElement owner;

		// Token: 0x0400009B RID: 155
		private readonly PropertyInformationCollection properties;
	}
}
