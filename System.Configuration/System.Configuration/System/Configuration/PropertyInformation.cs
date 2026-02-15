using System;

namespace System.Configuration
{
	/// <summary>Contains meta-information on an individual property within the configuration. This type cannot be inherited.</summary>
	// Token: 0x02000034 RID: 52
	public sealed class PropertyInformation
	{
		// Token: 0x06000162 RID: 354 RVA: 0x000064A0 File Offset: 0x000046A0
		internal PropertyInformation(ConfigurationElement owner, ConfigurationProperty property)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			this.owner = owner;
			this.property = property;
		}

		/// <summary>Gets an object containing the default value related to a configuration attribute.</summary>
		/// <returns>An object containing the default value of the configuration attribute.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000064D2 File Offset: 0x000046D2
		public object DefaultValue
		{
			get
			{
				return this.property.DefaultValue;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute is a key.</summary>
		/// <returns>true if the configuration attribute is a key; otherwise, false.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000064DF File Offset: 0x000046DF
		public bool IsKey
		{
			get
			{
				return this.property.IsKey;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute has been modified.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.PropertyInformation" /> object has been modified; otherwise, false.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000064EC File Offset: 0x000046EC
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000064F4 File Offset: 0x000046F4
		public bool IsModified
		{
			get
			{
				return this.isModified;
			}
			internal set
			{
				this.isModified = value;
			}
		}

		/// <summary>Gets a value specifying whether the configuration attribute is required.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.PropertyInformation" /> object is required; otherwise, false.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000064FD File Offset: 0x000046FD
		public bool IsRequired
		{
			get
			{
				return this.property.IsRequired;
			}
		}

		/// <summary>Gets the line number in the configuration file related to the configuration attribute.</summary>
		/// <returns>A line number of the configuration file.</returns>
		// Token: 0x1700006C RID: 108
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000650A File Offset: 0x0000470A
		internal int LineNumber
		{
			set
			{
				this.lineNumber = value;
			}
		}

		/// <summary>Gets the name of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006513 File Offset: 0x00004713
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		/// <summary>Gets the source file that corresponds to a configuration attribute.</summary>
		/// <returns>The source file of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006520 File Offset: 0x00004720
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00006528 File Offset: 0x00004728
		public string Source
		{
			get
			{
				return this.source;
			}
			internal set
			{
				this.source = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object that corresponds to a configuration attribute.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006531 File Offset: 0x00004731
		public Type Type
		{
			get
			{
				return this.property.Type;
			}
		}

		/// <summary>Gets or sets an object containing the value related to a configuration attribute.</summary>
		/// <returns>An object containing the value for the <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006540 File Offset: 0x00004740
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000065B3 File Offset: 0x000047B3
		public object Value
		{
			get
			{
				if (this.origin == PropertyValueOrigin.Default)
				{
					if (!this.property.IsElement)
					{
						return this.DefaultValue;
					}
					ConfigurationElement configurationElement = (ConfigurationElement)Activator.CreateInstance(this.Type, true);
					configurationElement.InitFromProperty(this);
					if (this.owner != null && this.owner.IsReadOnly())
					{
						configurationElement.SetReadOnly();
					}
					this.val = configurationElement;
					this.origin = PropertyValueOrigin.Inherited;
				}
				return this.val;
			}
			set
			{
				this.val = value;
				this.isModified = true;
				this.origin = PropertyValueOrigin.SetHere;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000065CC File Offset: 0x000047CC
		internal void Reset(PropertyInformation parentProperty)
		{
			if (parentProperty == null)
			{
				this.origin = PropertyValueOrigin.Default;
				return;
			}
			if (this.property.IsElement)
			{
				((ConfigurationElement)this.Value).Reset((ConfigurationElement)parentProperty.Value);
				return;
			}
			this.val = parentProperty.Value;
			this.origin = PropertyValueOrigin.Inherited;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006620 File Offset: 0x00004820
		internal bool IsElement
		{
			get
			{
				return this.property.IsElement;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.PropertyValueOrigin" /> object related to the configuration attribute. </summary>
		/// <returns>A <see cref="T:System.Configuration.PropertyValueOrigin" /> object.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000662D File Offset: 0x0000482D
		public PropertyValueOrigin ValueOrigin
		{
			get
			{
				return this.origin;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006635 File Offset: 0x00004835
		internal string GetStringValue()
		{
			return this.property.ConvertToString(this.Value);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006648 File Offset: 0x00004848
		internal void SetStringValue(string value)
		{
			this.val = this.property.ConvertFromString(value);
			if (!object.Equals(this.val, this.DefaultValue))
			{
				this.origin = PropertyValueOrigin.SetHere;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006676 File Offset: 0x00004876
		internal ConfigurationProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x040000B0 RID: 176
		private bool isModified;

		// Token: 0x040000B1 RID: 177
		private int lineNumber;

		// Token: 0x040000B2 RID: 178
		private string source;

		// Token: 0x040000B3 RID: 179
		private object val;

		// Token: 0x040000B4 RID: 180
		private PropertyValueOrigin origin;

		// Token: 0x040000B5 RID: 181
		private readonly ConfigurationElement owner;

		// Token: 0x040000B6 RID: 182
		private readonly ConfigurationProperty property;
	}
}
