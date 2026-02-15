using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to instantiate a configuration property. This class cannot be inherited.</summary>
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ConfigurationPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Configuration.ConfigurationPropertyAttribute" /> class.</summary>
		/// <param name="name">Name of the <see cref="T:System.Configuration.ConfigurationProperty" /> object defined.</param>
		// Token: 0x060000E2 RID: 226 RVA: 0x00005280 File Offset: 0x00003480
		public ConfigurationPropertyAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets a value indicating whether this is a key property for the decorated element property.</summary>
		/// <returns>true if the property is a key property for an element of the collection; otherwise, false. The default is false.</returns>
		// Token: 0x17000043 RID: 67
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000529A File Offset: 0x0000349A
		public bool IsKey
		{
			set
			{
				if (value)
				{
					this.flags |= ConfigurationPropertyOptions.IsKey;
					return;
				}
				this.flags &= ~ConfigurationPropertyOptions.IsKey;
			}
		}

		/// <summary>Gets or sets a value indicating whether this is the default property collection for the decorated configuration property. </summary>
		/// <returns>true if the property represents the default collection of an element; otherwise, false. The default is false.</returns>
		// Token: 0x17000044 RID: 68
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000052BD File Offset: 0x000034BD
		public bool IsDefaultCollection
		{
			set
			{
				if (value)
				{
					this.flags |= ConfigurationPropertyOptions.IsDefaultCollection;
					return;
				}
				this.flags &= ~ConfigurationPropertyOptions.IsDefaultCollection;
			}
		}

		/// <summary>Gets or sets the default value for the decorated property.</summary>
		/// <returns>The object representing the default value of the decorated configuration-element property.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000052E0 File Offset: 0x000034E0
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000052E8 File Offset: 0x000034E8
		public object DefaultValue
		{
			get
			{
				return this.default_value;
			}
			set
			{
				this.default_value = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> for the decorated configuration-element property.</summary>
		/// <returns>One of the <see cref="T:System.Configuration.ConfigurationPropertyOptions" /> enumeration values associated with the property.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000052F1 File Offset: 0x000034F1
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000052F9 File Offset: 0x000034F9
		public ConfigurationPropertyOptions Options
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		/// <summary>Gets the name of the decorated configuration-element property.</summary>
		/// <returns>The name of the decorated configuration-element property.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005302 File Offset: 0x00003502
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets a value indicating whether the decorated element property is required.</summary>
		/// <returns>true if the property is required; otherwise, false. The default is false.</returns>
		// Token: 0x17000048 RID: 72
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000530A File Offset: 0x0000350A
		public bool IsRequired
		{
			set
			{
				if (value)
				{
					this.flags |= ConfigurationPropertyOptions.IsRequired;
					return;
				}
				this.flags &= ~ConfigurationPropertyOptions.IsRequired;
			}
		}

		// Token: 0x0400006E RID: 110
		private string name;

		// Token: 0x0400006F RID: 111
		private object default_value = ConfigurationProperty.NoDefaultValue;

		// Token: 0x04000070 RID: 112
		private ConfigurationPropertyOptions flags;
	}
}
