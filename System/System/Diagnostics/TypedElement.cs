using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200016C RID: 364
	internal class TypedElement : ConfigurationElement
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x0002DB5E File Offset: 0x0002BD5E
		public TypedElement(Type baseType)
		{
			this._properties = new ConfigurationPropertyCollection();
			this._properties.Add(TypedElement._propTypeName);
			this._properties.Add(TypedElement._propInitData);
			this._baseType = baseType;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0002DB98 File Offset: 0x0002BD98
		[ConfigurationProperty("initializeData", DefaultValue = "")]
		public string InitData
		{
			get
			{
				return (string)base[TypedElement._propInitData];
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0002DBAA File Offset: 0x0002BDAA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0002DBB2 File Offset: 0x0002BDB2
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0002DBC4 File Offset: 0x0002BDC4
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		public virtual string TypeName
		{
			get
			{
				return (string)base[TypedElement._propTypeName];
			}
			set
			{
				base[TypedElement._propTypeName] = value;
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002DBD2 File Offset: 0x0002BDD2
		protected object BaseGetRuntimeObject()
		{
			if (this._runtimeObject == null)
			{
				this._runtimeObject = TraceUtils.GetRuntimeObject(this.TypeName, this._baseType, this.InitData);
			}
			return this._runtimeObject;
		}

		// Token: 0x04000682 RID: 1666
		protected static readonly ConfigurationProperty _propTypeName = new ConfigurationProperty("type", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04000683 RID: 1667
		protected static readonly ConfigurationProperty _propInitData = new ConfigurationProperty("initializeData", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04000684 RID: 1668
		protected ConfigurationPropertyCollection _properties;

		// Token: 0x04000685 RID: 1669
		protected object _runtimeObject;

		// Token: 0x04000686 RID: 1670
		private Type _baseType;
	}
}
