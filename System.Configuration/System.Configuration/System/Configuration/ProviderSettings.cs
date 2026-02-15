using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	/// <summary>Represents the configuration elements associated with a provider.</summary>
	// Token: 0x0200003C RID: 60
	public sealed class ProviderSettings : ConfigurationElement
	{
		// Token: 0x06000189 RID: 393 RVA: 0x000068D0 File Offset: 0x00004AD0
		static ProviderSettings()
		{
			ProviderSettings.properties.Add(ProviderSettings.nameProp);
			ProviderSettings.properties.Add(ProviderSettings.typeProp);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000693B File Offset: 0x00004B3B
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			if (this.parameters == null)
			{
				this.parameters = new ConfigNameValueCollection();
			}
			this.parameters[name] = value;
			this.parameters.ResetModified();
			return true;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006969 File Offset: 0x00004B69
		protected internal override bool IsModified()
		{
			return (this.parameters != null && this.parameters.IsModified) || base.IsModified();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006988 File Offset: 0x00004B88
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			ProviderSettings providerSettings = parentElement as ProviderSettings;
			if (providerSettings != null && providerSettings.parameters != null)
			{
				this.parameters = new ConfigNameValueCollection(providerSettings.parameters);
				return;
			}
			this.parameters = null;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000069C7 File Offset: 0x00004BC7
		[MonoTODO]
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
		}

		/// <summary>Gets or sets the name of the provider configured by this class.</summary>
		/// <returns>The name of the provider.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000069D2 File Offset: 0x00004BD2
		[ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[ProviderSettings.nameProp];
			}
		}

		/// <summary>Gets or sets the type of the provider configured by this class.</summary>
		/// <returns>The fully qualified namespace and class name for the type of provider configured by this <see cref="T:System.Configuration.ProviderSettings" /> instance.</returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000069E4 File Offset: 0x00004BE4
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[ProviderSettings.typeProp];
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000069F6 File Offset: 0x00004BF6
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProviderSettings.properties;
			}
		}

		/// <summary>Gets a collection of user-defined parameters for the provider.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of parameters for the provider.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000069FD File Offset: 0x00004BFD
		public NameValueCollection Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new ConfigNameValueCollection();
				}
				return this.parameters;
			}
		}

		// Token: 0x040000C1 RID: 193
		private ConfigNameValueCollection parameters;

		// Token: 0x040000C2 RID: 194
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040000C3 RID: 195
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040000C4 RID: 196
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
