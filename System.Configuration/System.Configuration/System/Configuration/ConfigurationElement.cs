using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a configuration element within a configuration file.</summary>
	// Token: 0x0200000D RID: 13
	public abstract class ConfigurationElement
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000299E File Offset: 0x00000B9E
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000029A6 File Offset: 0x00000BA6
		internal Configuration Configuration
		{
			get
			{
				return this._configuration;
			}
			set
			{
				this._configuration = value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000029AF File Offset: 0x00000BAF
		internal virtual void InitFromProperty(PropertyInformation propertyInfo)
		{
			this.elementInfo = new ElementInformation(this, propertyInfo);
			this.Init();
		}

		/// <summary>Gets an <see cref="T:System.Configuration.ElementInformation" /> object that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationElement" /> object. </summary>
		/// <returns>An <see cref="T:System.Configuration.ElementInformation" /> that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000029C4 File Offset: 0x00000BC4
		public ElementInformation ElementInformation
		{
			get
			{
				if (this.elementInfo == null)
				{
					this.elementInfo = new ElementInformation(this, null);
				}
				return this.elementInfo;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000029E1 File Offset: 0x00000BE1
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000029E9 File Offset: 0x00000BE9
		internal string RawXml
		{
			get
			{
				return this.rawXml;
			}
			set
			{
				if (this.rawXml == null || value != null)
				{
					this.rawXml = value;
				}
			}
		}

		/// <summary>Sets the <see cref="T:System.Configuration.ConfigurationElement" /> object to its initial state.</summary>
		// Token: 0x06000042 RID: 66 RVA: 0x000029FD File Offset: 0x00000BFD
		protected internal virtual void Init()
		{
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ContextInformation" /> for the <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The current element is not associated with a context.</exception>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000029FF File Offset: 0x00000BFF
		protected ContextInformation EvaluationContext
		{
			get
			{
				if (this.Configuration != null)
				{
					return this.Configuration.EvaluationContext;
				}
				throw new ConfigurationErrorsException("This element is not currently associated with any context.");
			}
		}

		/// <summary>Gets the collection of locked attributes.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked attributes (properties) for the element.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002A1F File Offset: 0x00000C1F
		public ConfigurationLockCollection LockAllAttributesExcept
		{
			get
			{
				if (this.lockAllAttributesExcept == null)
				{
					this.lockAllAttributesExcept = new ConfigurationLockCollection(this, ConfigurationLockType.Attribute | ConfigurationLockType.Exclude);
				}
				return this.lockAllAttributesExcept;
			}
		}

		/// <summary>Gets the collection of locked elements.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked elements.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002A3D File Offset: 0x00000C3D
		public ConfigurationLockCollection LockAllElementsExcept
		{
			get
			{
				if (this.lockAllElementsExcept == null)
				{
					this.lockAllElementsExcept = new ConfigurationLockCollection(this, ConfigurationLockType.Element | ConfigurationLockType.Exclude);
				}
				return this.lockAllElementsExcept;
			}
		}

		/// <summary>Gets the collection of locked attributes </summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked attributes (properties) for the element.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A5B File Offset: 0x00000C5B
		public ConfigurationLockCollection LockAttributes
		{
			get
			{
				if (this.lockAttributes == null)
				{
					this.lockAttributes = new ConfigurationLockCollection(this, ConfigurationLockType.Attribute);
				}
				return this.lockAttributes;
			}
		}

		/// <summary>Gets the collection of locked elements.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLockCollection" /> of locked elements.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002A78 File Offset: 0x00000C78
		public ConfigurationLockCollection LockElements
		{
			get
			{
				if (this.lockElements == null)
				{
					this.lockElements = new ConfigurationLockCollection(this, ConfigurationLockType.Element);
				}
				return this.lockElements;
			}
		}

		/// <summary>Gets or sets a value indicating whether the element is locked.</summary>
		/// <returns>true if the element is locked; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element has already been locked at a higher configuration level.</exception>
		// Token: 0x1700001C RID: 28
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002A95 File Offset: 0x00000C95
		public bool LockItem
		{
			set
			{
				this.lockItem = value;
			}
		}

		/// <summary>Sets a property to the specified value.</summary>
		/// <param name="prop">The element property to set. </param>
		/// <param name="value">The value to assign to the property.</param>
		/// <param name="ignoreLocks">true if the locks on the property should be ignored; otherwise, false.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs if the element is read-only or <paramref name="ignoreLocks" /> is true but the locks cannot be ignored.</exception>
		// Token: 0x06000049 RID: 73 RVA: 0x00002AA0 File Offset: 0x00000CA0
		[MonoTODO]
		protected void SetPropertyValue(ConfigurationProperty prop, object value, bool ignoreLocks)
		{
			try
			{
				if (value != null)
				{
					prop.Validate(value);
				}
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(string.Format("The value for the property '{0}' on type {1} is not valid.", prop.Name, this.ElementInformation.Type), ex);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002AEC File Offset: 0x00000CEC
		internal ConfigurationPropertyCollection GetKeyProperties()
		{
			if (this.keyProps != null)
			{
				return this.keyProps;
			}
			ConfigurationPropertyCollection configurationPropertyCollection = new ConfigurationPropertyCollection();
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				if (configurationProperty.IsKey)
				{
					configurationPropertyCollection.Add(configurationProperty);
				}
			}
			return this.keyProps = configurationPropertyCollection;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B70 File Offset: 0x00000D70
		internal ConfigurationElementCollection GetDefaultCollection()
		{
			if (this.defaultCollection != null)
			{
				return this.defaultCollection;
			}
			ConfigurationProperty configurationProperty = null;
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty2 = (ConfigurationProperty)obj;
				if (configurationProperty2.IsDefaultCollection)
				{
					configurationProperty = configurationProperty2;
					break;
				}
			}
			if (configurationProperty != null)
			{
				this.defaultCollection = this[configurationProperty] as ConfigurationElementCollection;
			}
			return this.defaultCollection;
		}

		/// <summary>Gets or sets a property or attribute of this configuration element.</summary>
		/// <returns>The specified property, attribute, or child element.</returns>
		/// <param name="prop">The property to access. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">
		///   <paramref name="prop" /> is null or does not exist within the element.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="prop" /> is read only or locked.</exception>
		// Token: 0x1700001D RID: 29
		protected internal object this[ConfigurationProperty prop]
		{
			get
			{
				return this[prop.Name];
			}
			set
			{
				this[prop.Name] = value;
			}
		}

		/// <summary>Gets or sets a property, attribute, or child element of this configuration element.</summary>
		/// <returns>The specified property, attribute, or child element</returns>
		/// <param name="propertyName">The name of the <see cref="T:System.Configuration.ConfigurationProperty" /> to access.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="prop" /> is read-only or locked.</exception>
		// Token: 0x1700001E RID: 30
		protected internal object this[string propertyName]
		{
			get
			{
				PropertyInformation propertyInformation = this.ElementInformation.Properties[propertyName];
				if (propertyInformation == null)
				{
					throw new InvalidOperationException("Property '" + propertyName + "' not found in configuration element");
				}
				return propertyInformation.Value;
			}
			set
			{
				PropertyInformation propertyInformation = this.ElementInformation.Properties[propertyName];
				if (propertyInformation == null)
				{
					throw new InvalidOperationException("Property '" + propertyName + "' not found in configuration element");
				}
				this.SetPropertyValue(propertyInformation.Property, value, false);
				propertyInformation.Value = value;
				this.modified = true;
			}
		}

		/// <summary>Gets the collection of properties.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> of properties for the element.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002CA0 File Offset: 0x00000EA0
		protected internal virtual ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.map == null)
				{
					this.map = ElementMap.GetMap(base.GetType());
				}
				return this.map.Properties;
			}
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.ConfigurationElement" /> instance to the specified object.</summary>
		/// <returns>true if the object to compare with is equal to the current <see cref="T:System.Configuration.ConfigurationElement" /> instance; otherwise, false. The default is false. </returns>
		/// <param name="compareTo">The object to compare with.</param>
		// Token: 0x06000051 RID: 81 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public override bool Equals(object compareTo)
		{
			ConfigurationElement configurationElement = compareTo as ConfigurationElement;
			if (configurationElement == null)
			{
				return false;
			}
			if (base.GetType() != configurationElement.GetType())
			{
				return false;
			}
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				if (!object.Equals(this[configurationProperty], configurationElement[configurationProperty]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a unique value representing the current <see cref="T:System.Configuration.ConfigurationElement" /> instance.</summary>
		/// <returns>A unique value representing the current <see cref="T:System.Configuration.ConfigurationElement" /> instance.</returns>
		// Token: 0x06000052 RID: 82 RVA: 0x00002D5C File Offset: 0x00000F5C
		public override int GetHashCode()
		{
			int num = 0;
			foreach (object obj in this.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				object obj2 = this[configurationProperty];
				if (obj2 != null)
				{
					num += obj2.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DC8 File Offset: 0x00000FC8
		internal virtual bool HasLocalModifications()
		{
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere && propertyInformation.IsModified)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Reads XML from the configuration file.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> that reads from the configuration file.</param>
		/// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element to read is locked.- or -An attribute of the current node is not recognized.- or -The lock status of the current node cannot be determined.  </exception>
		// Token: 0x06000054 RID: 84 RVA: 0x00002E38 File Offset: 0x00001038
		protected internal virtual void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			Hashtable hashtable = new Hashtable();
			reader.MoveToContent();
			this.elementPresent = true;
			while (reader.MoveToNextAttribute())
			{
				PropertyInformation propertyInformation = this.ElementInformation.Properties[reader.LocalName];
				if (propertyInformation == null || (serializeCollectionKey && !propertyInformation.IsKey))
				{
					if (reader.LocalName == "lockAllAttributesExcept")
					{
						this.LockAllAttributesExcept.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockAllElementsExcept")
					{
						this.LockAllElementsExcept.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockAttributes")
					{
						this.LockAttributes.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockElements")
					{
						this.LockElements.SetFromList(reader.Value);
					}
					else if (reader.LocalName == "lockItem")
					{
						this.LockItem = reader.Value.ToLowerInvariant() == "true";
					}
					else if (!(reader.LocalName == "xmlns") && (!(this is ConfigurationSection) || !(reader.LocalName == "configSource")) && !this.OnDeserializeUnrecognizedAttribute(reader.LocalName, reader.Value))
					{
						throw new ConfigurationErrorsException("Unrecognized attribute '" + reader.LocalName + "'.", reader);
					}
				}
				else
				{
					if (hashtable.ContainsKey(propertyInformation))
					{
						throw new ConfigurationErrorsException("The attribute '" + propertyInformation.Name + "' may only appear once in this element.", reader);
					}
					try
					{
						string value = reader.Value;
						this.ValidateValue(propertyInformation.Property, value);
						propertyInformation.SetStringValue(value);
					}
					catch (ConfigurationErrorsException)
					{
						throw;
					}
					catch (ConfigurationException)
					{
						throw;
					}
					catch (Exception ex)
					{
						throw new ConfigurationErrorsException(string.Format("The value for the property '{0}' is not valid. The error is: {1}", propertyInformation.Name, ex.Message), reader);
					}
					hashtable[propertyInformation] = propertyInformation.Name;
					ConfigXmlTextReader configXmlTextReader = reader as ConfigXmlTextReader;
					if (configXmlTextReader != null)
					{
						propertyInformation.Source = configXmlTextReader.Filename;
						propertyInformation.LineNumber = configXmlTextReader.LineNumber;
					}
				}
			}
			reader.MoveToElement();
			if (!reader.IsEmptyElement)
			{
				int depth = reader.Depth;
				reader.ReadStartElement();
				reader.MoveToContent();
				PropertyInformation propertyInformation2;
				for (;;)
				{
					if (reader.NodeType != XmlNodeType.Element)
					{
						reader.Skip();
					}
					else
					{
						propertyInformation2 = this.ElementInformation.Properties[reader.LocalName];
						if (propertyInformation2 == null || (serializeCollectionKey && !propertyInformation2.IsKey))
						{
							if (!this.OnDeserializeUnrecognizedElement(reader.LocalName, reader))
							{
								if (propertyInformation2 != null)
								{
									break;
								}
								ConfigurationElementCollection configurationElementCollection = this.GetDefaultCollection();
								if (configurationElementCollection == null || !configurationElementCollection.OnDeserializeUnrecognizedElement(reader.LocalName, reader))
								{
									break;
								}
							}
						}
						else
						{
							if (!propertyInformation2.IsElement)
							{
								goto Block_22;
							}
							if (hashtable.Contains(propertyInformation2))
							{
								goto Block_23;
							}
							((ConfigurationElement)propertyInformation2.Value).DeserializeElement(reader, serializeCollectionKey);
							hashtable[propertyInformation2] = propertyInformation2.Name;
							if (depth == reader.Depth)
							{
								reader.Read();
							}
						}
					}
					if (depth >= reader.Depth)
					{
						goto IL_0367;
					}
				}
				throw new ConfigurationErrorsException("Unrecognized element '" + reader.LocalName + "'.", reader);
				Block_22:
				throw new ConfigurationErrorsException("Property '" + propertyInformation2.Name + "' is not a ConfigurationElement.");
				Block_23:
				throw new ConfigurationErrorsException("The element <" + propertyInformation2.Name + "> may only appear once in this section.", reader);
			}
			reader.Skip();
			IL_0367:
			this.modified = false;
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation3 = (PropertyInformation)obj;
				if (!string.IsNullOrEmpty(propertyInformation3.Name) && propertyInformation3.IsRequired && !hashtable.ContainsKey(propertyInformation3) && this.ElementInformation.Properties[propertyInformation3.Name] == null)
				{
					object obj2 = this.OnRequiredPropertyNotFound(propertyInformation3.Name);
					if (!object.Equals(obj2, propertyInformation3.DefaultValue))
					{
						propertyInformation3.Value = obj2;
						propertyInformation3.IsModified = false;
					}
				}
			}
			this.PostDeserialize();
		}

		/// <summary>Gets a value indicating whether an unknown attribute is encountered during deserialization.</summary>
		/// <returns>true when an unknown attribute is encountered while deserializing; otherwise, false.</returns>
		/// <param name="name">The name of the unrecognized attribute.</param>
		/// <param name="value">The value of the unrecognized attribute.</param>
		// Token: 0x06000055 RID: 85 RVA: 0x0000329C File Offset: 0x0000149C
		protected virtual bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether an unknown element is encountered during deserialization.</summary>
		/// <returns>true when an unknown element is encountered while deserializing; otherwise, false.</returns>
		/// <param name="elementName">The name of the unknown subelement.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> being used for deserialization.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element identified by <paramref name="elementName" /> is locked.- or -One or more of the element's attributes is locked.- or -<paramref name="elementName" /> is unrecognized, or the element has an unrecognized attribute.- or -The element has a Boolean attribute with an invalid value.- or -An attempt was made to deserialize a property more than once.- or -An attempt was made to deserialize a property that is not a valid member of the element.- or -The element cannot contain a CDATA or text element.</exception>
		// Token: 0x06000056 RID: 86 RVA: 0x0000329C File Offset: 0x0000149C
		protected virtual bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return false;
		}

		/// <summary>Throws an exception when a required property is not found.</summary>
		/// <returns>None.</returns>
		/// <param name="name">The name of the required attribute that was not found.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">In all cases.</exception>
		// Token: 0x06000057 RID: 87 RVA: 0x0000329F File Offset: 0x0000149F
		protected virtual object OnRequiredPropertyNotFound(string name)
		{
			throw new ConfigurationErrorsException("Required attribute '" + name + "' not found.");
		}

		/// <summary>Called before serialization.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that will be used to serialize the <see cref="T:System.Configuration.ConfigurationElement" />.</param>
		// Token: 0x06000058 RID: 88 RVA: 0x000029FD File Offset: 0x00000BFD
		protected virtual void PreSerialize(XmlWriter writer)
		{
		}

		/// <summary>Called after deserialization.</summary>
		// Token: 0x06000059 RID: 89 RVA: 0x000029FD File Offset: 0x00000BFD
		protected virtual void PostDeserialize()
		{
		}

		/// <summary>Used to initialize a default set of values for the <see cref="T:System.Configuration.ConfigurationElement" /> object.</summary>
		// Token: 0x0600005A RID: 90 RVA: 0x000029FD File Offset: 0x00000BFD
		protected internal virtual void InitializeDefault()
		{
		}

		/// <summary>Indicates whether this configuration element has been modified since it was last saved or loaded, when implemented in a derived class.</summary>
		/// <returns>true if the element has been modified; otherwise, false. </returns>
		// Token: 0x0600005B RID: 91 RVA: 0x000032B8 File Offset: 0x000014B8
		protected internal virtual bool IsModified()
		{
			if (this.modified)
			{
				return true;
			}
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.IsElement)
				{
					ConfigurationElement configurationElement = propertyInformation.Value as ConfigurationElement;
					if (configurationElement != null && configurationElement.IsModified())
					{
						this.modified = true;
						break;
					}
				}
			}
			return this.modified;
		}

		/// <summary>Sets the <see cref="M:System.Configuration.ConfigurationElement.IsReadOnly" /> property for the <see cref="T:System.Configuration.ConfigurationElement" /> object and all subelements.</summary>
		// Token: 0x0600005C RID: 92 RVA: 0x00003348 File Offset: 0x00001548
		protected internal virtual void SetReadOnly()
		{
			this.readOnly = true;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only; otherwise, false.</returns>
		// Token: 0x0600005D RID: 93 RVA: 0x00003351 File Offset: 0x00001551
		public virtual bool IsReadOnly()
		{
			return this.readOnly;
		}

		/// <summary>Resets the internal state of the <see cref="T:System.Configuration.ConfigurationElement" /> object, including the locks and the properties collections.</summary>
		/// <param name="parentElement">The parent node of the configuration element.</param>
		// Token: 0x0600005E RID: 94 RVA: 0x00003359 File Offset: 0x00001559
		protected internal virtual void Reset(ConfigurationElement parentElement)
		{
			this.elementPresent = false;
			if (parentElement != null)
			{
				this.ElementInformation.Reset(parentElement.ElementInformation);
				return;
			}
			this.InitializeDefault();
		}

		/// <summary>Resets the value of the <see cref="M:System.Configuration.ConfigurationElement.IsModified" /> method to false when implemented in a derived class.</summary>
		// Token: 0x0600005F RID: 95 RVA: 0x00003380 File Offset: 0x00001580
		protected internal virtual void ResetModified()
		{
			this.modified = false;
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				propertyInformation.IsModified = false;
				ConfigurationElement configurationElement = propertyInformation.Value as ConfigurationElement;
				if (configurationElement != null)
				{
					configurationElement.ResetModified();
				}
			}
		}

		/// <summary>Writes the contents of this configuration element to the configuration file when implemented in a derived class.</summary>
		/// <returns>true if any data was actually serialized; otherwise, false.</returns>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file. </param>
		/// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The current attribute is locked at a higher configuration level.</exception>
		// Token: 0x06000060 RID: 96 RVA: 0x000033F8 File Offset: 0x000015F8
		protected internal virtual bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			this.PreSerialize(writer);
			if (serializeCollectionKey)
			{
				ConfigurationPropertyCollection keyProperties = this.GetKeyProperties();
				foreach (object obj in keyProperties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					writer.WriteAttributeString(configurationProperty.Name, configurationProperty.ConvertToString(this[configurationProperty.Name]));
				}
				return keyProperties.Count > 0;
			}
			bool flag = false;
			foreach (object obj2 in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj2;
				if (!propertyInformation.IsElement)
				{
					if (this.saveContext == null)
					{
						throw new InvalidOperationException();
					}
					if (this.saveContext.HasValue(propertyInformation))
					{
						writer.WriteAttributeString(propertyInformation.Name, propertyInformation.GetStringValue());
						flag = true;
					}
				}
			}
			foreach (object obj3 in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation2 = (PropertyInformation)obj3;
				if (propertyInformation2.IsElement)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)propertyInformation2.Value;
					if (configurationElement != null)
					{
						flag = configurationElement.SerializeToXmlElement(writer, propertyInformation2.Name) || flag;
					}
				}
			}
			return flag;
		}

		/// <summary>Writes the outer tags of this configuration element to the configuration file when implemented in a derived class.</summary>
		/// <returns>true if writing was successful; otherwise, false.</returns>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file. </param>
		/// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> to be written. </param>
		/// <exception cref="T:System.Exception">The element has multiple child elements. </exception>
		// Token: 0x06000061 RID: 97 RVA: 0x00003584 File Offset: 0x00001784
		protected internal virtual bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (this.saveContext == null)
			{
				throw new InvalidOperationException();
			}
			if (!this.saveContext.HasValues())
			{
				return false;
			}
			if (elementName != null && elementName != "")
			{
				writer.WriteStartElement(elementName);
			}
			bool flag = this.SerializeElement(writer, false);
			if (elementName != null && elementName != "")
			{
				writer.WriteEndElement();
			}
			return flag;
		}

		/// <summary>Modifies the <see cref="T:System.Configuration.ConfigurationElement" /> object to remove all values that should not be saved. </summary>
		/// <param name="sourceElement">A <see cref="T:System.Configuration.ConfigurationElement" /> at the current level containing a merged view of the properties.</param>
		/// <param name="parentElement">The parent <see cref="T:System.Configuration.ConfigurationElement" />, or null if this is the top level.</param>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> that determines which property values to include.</param>
		// Token: 0x06000062 RID: 98 RVA: 0x000035E4 File Offset: 0x000017E4
		protected internal virtual void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (parentElement != null && sourceElement.GetType() != parentElement.GetType())
			{
				throw new ConfigurationErrorsException("Can't unmerge two elements of different type");
			}
			bool flag = saveMode == ConfigurationSaveMode.Minimal || saveMode == ConfigurationSaveMode.Modified;
			foreach (object obj in sourceElement.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.ValueOrigin != PropertyValueOrigin.Default)
				{
					PropertyInformation propertyInformation2 = this.ElementInformation.Properties[propertyInformation.Name];
					object value = propertyInformation.Value;
					if (parentElement == null || !parentElement.HasValue(propertyInformation.Name))
					{
						propertyInformation2.Value = value;
					}
					else if (value != null)
					{
						object obj2 = parentElement[propertyInformation.Name];
						if (!propertyInformation.IsElement)
						{
							if (!object.Equals(value, obj2) || saveMode == ConfigurationSaveMode.Full || (saveMode == ConfigurationSaveMode.Modified && propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere))
							{
								propertyInformation2.Value = value;
							}
						}
						else
						{
							ConfigurationElement configurationElement = (ConfigurationElement)value;
							if (!flag || configurationElement.IsModified())
							{
								if (obj2 == null)
								{
									propertyInformation2.Value = value;
								}
								else
								{
									ConfigurationElement configurationElement2 = (ConfigurationElement)obj2;
									((ConfigurationElement)propertyInformation2.Value).Unmerge(configurationElement, configurationElement2, saveMode);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000373C File Offset: 0x0000193C
		internal bool HasValue(string propName)
		{
			PropertyInformation propertyInformation = this.ElementInformation.Properties[propName];
			return propertyInformation != null && propertyInformation.ValueOrigin > PropertyValueOrigin.Default;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000376C File Offset: 0x0000196C
		private void ValidateValue(ConfigurationProperty p, string value)
		{
			ConfigurationValidatorBase validator;
			if (p == null || (validator = p.Validator) == null)
			{
				return;
			}
			if (!validator.CanValidate(p.Type))
			{
				throw new ConfigurationErrorsException(string.Format("Validator does not support type {0}", p.Type));
			}
			validator.Validate(p.ConvertFromString(value));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000037B8 File Offset: 0x000019B8
		internal bool HasValue(ConfigurationElement parent, PropertyInformation prop, ConfigurationSaveMode mode)
		{
			if (prop.ValueOrigin == PropertyValueOrigin.Default)
			{
				return false;
			}
			if (mode == ConfigurationSaveMode.Modified && prop.ValueOrigin == PropertyValueOrigin.SetHere && prop.IsModified)
			{
				return true;
			}
			object obj = ((parent != null && parent.HasValue(prop.Name)) ? parent[prop.Name] : prop.DefaultValue);
			if (!prop.IsElement)
			{
				return !object.Equals(prop.Value, obj);
			}
			ConfigurationElement configurationElement = (ConfigurationElement)prop.Value;
			ConfigurationElement configurationElement2 = (ConfigurationElement)obj;
			return configurationElement.HasValues(configurationElement2, mode);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003840 File Offset: 0x00001A40
		internal virtual bool HasValues(ConfigurationElement parent, ConfigurationSaveMode mode)
		{
			if (mode == ConfigurationSaveMode.Full)
			{
				return true;
			}
			if (this.modified && mode == ConfigurationSaveMode.Modified)
			{
				return true;
			}
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (this.HasValue(parent, propertyInformation, mode))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000038BC File Offset: 0x00001ABC
		internal virtual void PrepareSave(ConfigurationElement parent, ConfigurationSaveMode mode)
		{
			this.saveContext = new ConfigurationElement.SaveContext(this, parent, mode);
			foreach (object obj in this.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.IsElement)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)propertyInformation.Value;
					if (parent == null || !parent.HasValue(propertyInformation.Name))
					{
						configurationElement.PrepareSave(null, mode);
					}
					else
					{
						ConfigurationElement configurationElement2 = (ConfigurationElement)parent[propertyInformation.Name];
						configurationElement.PrepareSave(configurationElement2, mode);
					}
				}
			}
		}

		// Token: 0x04000027 RID: 39
		private string rawXml;

		// Token: 0x04000028 RID: 40
		private bool modified;

		// Token: 0x04000029 RID: 41
		private ElementMap map;

		// Token: 0x0400002A RID: 42
		private ConfigurationPropertyCollection keyProps;

		// Token: 0x0400002B RID: 43
		private ConfigurationElementCollection defaultCollection;

		// Token: 0x0400002C RID: 44
		private bool readOnly;

		// Token: 0x0400002D RID: 45
		private ElementInformation elementInfo;

		// Token: 0x0400002E RID: 46
		private Configuration _configuration;

		// Token: 0x0400002F RID: 47
		private bool elementPresent;

		// Token: 0x04000030 RID: 48
		private ConfigurationLockCollection lockAllAttributesExcept;

		// Token: 0x04000031 RID: 49
		private ConfigurationLockCollection lockAllElementsExcept;

		// Token: 0x04000032 RID: 50
		private ConfigurationLockCollection lockAttributes;

		// Token: 0x04000033 RID: 51
		private ConfigurationLockCollection lockElements;

		// Token: 0x04000034 RID: 52
		private bool lockItem;

		// Token: 0x04000035 RID: 53
		private ConfigurationElement.SaveContext saveContext;

		// Token: 0x0200000E RID: 14
		private class SaveContext
		{
			// Token: 0x06000068 RID: 104 RVA: 0x00003970 File Offset: 0x00001B70
			public SaveContext(ConfigurationElement element, ConfigurationElement parent, ConfigurationSaveMode mode)
			{
				this.Element = element;
				this.Parent = parent;
				this.Mode = mode;
			}

			// Token: 0x06000069 RID: 105 RVA: 0x0000398D File Offset: 0x00001B8D
			public bool HasValues()
			{
				return this.Mode == ConfigurationSaveMode.Full || this.Element.HasValues(this.Parent, this.Mode);
			}

			// Token: 0x0600006A RID: 106 RVA: 0x000039B1 File Offset: 0x00001BB1
			public bool HasValue(PropertyInformation prop)
			{
				return this.Mode == ConfigurationSaveMode.Full || this.Element.HasValue(this.Parent, prop, this.Mode);
			}

			// Token: 0x04000036 RID: 54
			public readonly ConfigurationElement Element;

			// Token: 0x04000037 RID: 55
			public readonly ConfigurationElement Parent;

			// Token: 0x04000038 RID: 56
			public readonly ConfigurationSaveMode Mode;
		}
	}
}
