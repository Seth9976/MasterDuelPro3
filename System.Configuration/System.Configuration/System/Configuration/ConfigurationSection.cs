using System;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a section within a configuration file.</summary>
	// Token: 0x0200001F RID: 31
	public abstract class ConfigurationSection : ConfigurationElement
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005415 File Offset: 0x00003615
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000541D File Offset: 0x0000361D
		internal IConfigurationSectionHandler SectionHandler
		{
			get
			{
				return this.section_handler;
			}
			set
			{
				this.section_handler = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SectionInformation" /> object that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationSection" /> object. </summary>
		/// <returns>A <see cref="T:System.Configuration.SectionInformation" /> that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005426 File Offset: 0x00003626
		[MonoTODO]
		public SectionInformation SectionInformation
		{
			get
			{
				if (this.sectionInformation == null)
				{
					this.sectionInformation = new SectionInformation();
				}
				return this.sectionInformation;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005441 File Offset: 0x00003641
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005449 File Offset: 0x00003649
		internal object ConfigContext
		{
			get
			{
				return this._configContext;
			}
			set
			{
				this._configContext = value;
			}
		}

		/// <summary>Returns a custom object when overridden in a derived class.</summary>
		/// <returns>The object representing the section.</returns>
		// Token: 0x060000FA RID: 250 RVA: 0x00005454 File Offset: 0x00003654
		[MonoTODO("Provide ConfigContext. Likely the culprit of bug #322493")]
		protected internal virtual object GetRuntimeObject()
		{
			if (this.SectionHandler == null)
			{
				return this;
			}
			ConfigurationSection configurationSection = ((this.sectionInformation != null) ? this.sectionInformation.GetParentSection() : null);
			object obj = ((configurationSection != null) ? configurationSection.GetRuntimeObject() : null);
			if (base.RawXml == null)
			{
				return obj;
			}
			try
			{
				XmlReader xmlReader = new ConfigXmlTextReader(new StringReader(base.RawXml), base.Configuration.FilePath);
				this.DoDeserializeSection(xmlReader);
				if (!string.IsNullOrEmpty(this.SectionInformation.ConfigSource))
				{
					string text = this.SectionInformation.ConfigFilePath;
					if (!string.IsNullOrEmpty(text))
					{
						text = Path.GetDirectoryName(text);
					}
					else
					{
						text = string.Empty;
					}
					string text2 = Path.Combine(text, this.SectionInformation.ConfigSource);
					if (File.Exists(text2))
					{
						base.RawXml = File.ReadAllText(text2);
						this.SectionInformation.SetRawXml(base.RawXml);
					}
				}
			}
			catch
			{
			}
			XmlDocument xmlDocument = new ConfigurationXmlDocument();
			xmlDocument.LoadXml(base.RawXml);
			return this.SectionHandler.Create(obj, this.ConfigContext, xmlDocument.DocumentElement);
		}

		/// <summary>Indicates whether this configuration element has been modified since it was last saved or loaded when implemented in a derived class.</summary>
		/// <returns>true if the element has been modified; otherwise, false. </returns>
		// Token: 0x060000FB RID: 251 RVA: 0x00005574 File Offset: 0x00003774
		[MonoTODO]
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		/// <summary>Resets the value of the <see cref="M:System.Configuration.ConfigurationElement.IsModified" /> method to false when implemented in a derived class.</summary>
		// Token: 0x060000FC RID: 252 RVA: 0x0000557C File Offset: 0x0000377C
		[MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005584 File Offset: 0x00003784
		private ConfigurationElement CreateElement(Type t)
		{
			ConfigurationElement configurationElement = (ConfigurationElement)Activator.CreateInstance(t);
			configurationElement.Init();
			configurationElement.Configuration = base.Configuration;
			if (this.IsReadOnly())
			{
				configurationElement.SetReadOnly();
			}
			return configurationElement;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000055C0 File Offset: 0x000037C0
		private void DoDeserializeSection(XmlReader reader)
		{
			reader.MoveToContent();
			string text = null;
			string text2 = null;
			while (reader.MoveToNextAttribute())
			{
				string localName = reader.LocalName;
				if (localName == "configProtectionProvider")
				{
					text = reader.Value;
				}
				else if (localName == "configSource")
				{
					text2 = reader.Value;
				}
			}
			if (text != null)
			{
				ProtectedConfigurationProvider provider = ProtectedConfiguration.GetProvider(text, true);
				XmlDocument xmlDocument = new ConfigurationXmlDocument();
				reader.MoveToElement();
				xmlDocument.Load(new StringReader(reader.ReadInnerXml()));
				reader = new XmlNodeReader(provider.Decrypt(xmlDocument));
				this.SectionInformation.ProtectSection(text);
				reader.MoveToContent();
			}
			if (text2 != null)
			{
				this.SectionInformation.ConfigSource = text2;
			}
			this.SectionInformation.SetRawXml(base.RawXml);
			if (this.SectionHandler == null)
			{
				this.DeserializeElement(reader, false);
			}
		}

		/// <summary>Reads XML from the configuration file.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object, which reads from the configuration file. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="reader" /> found no elements in the configuration file.</exception>
		// Token: 0x060000FF RID: 255 RVA: 0x0000568C File Offset: 0x0000388C
		[MonoInternalNote("find the proper location for the decryption stuff")]
		protected internal virtual void DeserializeSection(XmlReader reader)
		{
			try
			{
				this.DoDeserializeSection(reader);
			}
			catch (ConfigurationErrorsException ex)
			{
				throw new ConfigurationErrorsException(string.Format("Error deserializing configuration section {0}: {1}", this.SectionInformation.Name, ex.Message));
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000056D4 File Offset: 0x000038D4
		internal void DeserializeConfigSource(string basePath)
		{
			string configSource = this.SectionInformation.ConfigSource;
			if (string.IsNullOrEmpty(configSource))
			{
				return;
			}
			if (Path.IsPathRooted(configSource))
			{
				throw new ConfigurationErrorsException("The configSource attribute must be a relative physical path.");
			}
			if (this.HasLocalModifications())
			{
				throw new ConfigurationErrorsException("A section using 'configSource' may contain no other attributes or elements.");
			}
			string text = Path.Combine(basePath, configSource);
			if (!File.Exists(text))
			{
				base.RawXml = null;
				this.SectionInformation.SetRawXml(null);
				throw new ConfigurationErrorsException(string.Format("Unable to open configSource file '{0}'.", text));
			}
			base.RawXml = File.ReadAllText(text);
			this.SectionInformation.SetRawXml(base.RawXml);
			this.DeserializeElement(new ConfigXmlTextReader(new StringReader(base.RawXml), text), false);
		}

		/// <summary>Creates an XML string containing an unmerged view of the <see cref="T:System.Configuration.ConfigurationSection" /> object as a single section to write to a file.</summary>
		/// <returns>An XML string containing an unmerged view of the <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		/// <param name="parentElement">The <see cref="T:System.Configuration.ConfigurationElement" /> instance to use as the parent when performing the un-merge.</param>
		/// <param name="name">The name of the section to create.</param>
		/// <param name="saveMode">The <see cref="T:System.Configuration.ConfigurationSaveMode" /> instance to use when writing to a string.</param>
		// Token: 0x06000101 RID: 257 RVA: 0x00005788 File Offset: 0x00003988
		protected internal virtual string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
		{
			this.externalDataXml = null;
			ConfigurationElement configurationElement;
			if (parentElement != null)
			{
				configurationElement = this.CreateElement(base.GetType());
				configurationElement.Unmerge(this, parentElement, saveMode);
			}
			else
			{
				configurationElement = this;
			}
			configurationElement.PrepareSave(parentElement, saveMode);
			bool flag = configurationElement.HasValues(parentElement, saveMode);
			string text;
			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					if (flag)
					{
						configurationElement.SerializeToXmlElement(xmlTextWriter, name);
					}
					else if (saveMode == ConfigurationSaveMode.Modified && configurationElement.IsModified())
					{
						xmlTextWriter.WriteStartElement(name);
						xmlTextWriter.WriteEndElement();
					}
					xmlTextWriter.Close();
				}
				text = stringWriter.ToString();
			}
			string configSource = this.SectionInformation.ConfigSource;
			if (string.IsNullOrEmpty(configSource))
			{
				return text;
			}
			this.externalDataXml = text;
			string text2;
			using (StringWriter stringWriter2 = new StringWriter())
			{
				bool flag2 = !string.IsNullOrEmpty(name);
				using (XmlTextWriter xmlTextWriter2 = new XmlTextWriter(stringWriter2))
				{
					if (flag2)
					{
						xmlTextWriter2.WriteStartElement(name);
					}
					xmlTextWriter2.WriteAttributeString("configSource", configSource);
					if (flag2)
					{
						xmlTextWriter2.WriteEndElement();
					}
				}
				text2 = stringWriter2.ToString();
			}
			return text2;
		}

		// Token: 0x0400007E RID: 126
		private SectionInformation sectionInformation;

		// Token: 0x0400007F RID: 127
		private IConfigurationSectionHandler section_handler;

		// Token: 0x04000080 RID: 128
		private string externalDataXml;

		// Token: 0x04000081 RID: 129
		private object _configContext;
	}
}
