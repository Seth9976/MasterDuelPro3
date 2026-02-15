using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace System.Configuration
{
	// Token: 0x0200000F RID: 15
	internal class ElementMap
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000039D8 File Offset: 0x00001BD8
		public static ElementMap GetMap(Type t)
		{
			ElementMap elementMap = ElementMap.elementMaps[t] as ElementMap;
			if (elementMap != null)
			{
				return elementMap;
			}
			elementMap = new ElementMap(t);
			ElementMap.elementMaps[t] = elementMap;
			return elementMap;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A10 File Offset: 0x00001C10
		public ElementMap(Type t)
		{
			this.properties = new ConfigurationPropertyCollection();
			this.collectionAttribute = Attribute.GetCustomAttribute(t, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute;
			foreach (PropertyInfo propertyInfo in t.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				ConfigurationPropertyAttribute configurationPropertyAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationPropertyAttribute)) as ConfigurationPropertyAttribute;
				if (configurationPropertyAttribute != null)
				{
					string text = ((configurationPropertyAttribute.Name != null) ? configurationPropertyAttribute.Name : propertyInfo.Name);
					ConfigurationValidatorAttribute configurationValidatorAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationValidatorAttribute)) as ConfigurationValidatorAttribute;
					ConfigurationValidatorBase configurationValidatorBase = ((configurationValidatorAttribute != null) ? configurationValidatorAttribute.ValidatorInstance : null);
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(TypeConverterAttribute));
					TypeConverter typeConverter = ((typeConverterAttribute != null) ? ((TypeConverter)Activator.CreateInstance(Type.GetType(typeConverterAttribute.ConverterTypeName), true)) : null);
					ConfigurationProperty configurationProperty = new ConfigurationProperty(text, propertyInfo.PropertyType, configurationPropertyAttribute.DefaultValue, typeConverter, configurationValidatorBase, configurationPropertyAttribute.Options);
					configurationProperty.CollectionAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute;
					this.properties.Add(configurationProperty);
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003B40 File Offset: 0x00001D40
		public ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04000039 RID: 57
		private static readonly Hashtable elementMaps = Hashtable.Synchronized(new Hashtable());

		// Token: 0x0400003A RID: 58
		private readonly ConfigurationPropertyCollection properties;

		// Token: 0x0400003B RID: 59
		private readonly ConfigurationCollectionAttribute collectionAttribute;
	}
}
