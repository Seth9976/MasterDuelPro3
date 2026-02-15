using System;
using System.Collections;
using System.Configuration.Internal;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a configuration file that is applicable to a particular computer, application, or resource. This class cannot be inherited.</summary>
	// Token: 0x02000009 RID: 9
	public sealed class Configuration
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000021E4 File Offset: 0x000003E4
		internal Configuration(Configuration parent, string locationSubPath)
		{
			this.parent = parent;
			this.system = parent.system;
			this.rootGroup = parent.rootGroup;
			this.locationSubPath = locationSubPath;
			this.configPath = parent.ConfigPath;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002234 File Offset: 0x00000434
		internal Configuration(InternalConfigurationSystem system, string locationSubPath)
		{
			this.hasFile = true;
			this.system = system;
			system.InitForConfiguration(ref locationSubPath, out this.configPath, out this.locationConfigPath);
			Configuration configuration = null;
			if (locationSubPath != null)
			{
				configuration = new Configuration(system, locationSubPath);
				if (this.locationConfigPath != null)
				{
					configuration = configuration.FindLocationConfiguration(this.locationConfigPath, configuration);
				}
			}
			this.Init(system, this.configPath, configuration);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022A8 File Offset: 0x000004A8
		internal Configuration FindLocationConfiguration(string relativePath, Configuration defaultConfiguration)
		{
			Configuration configuration = defaultConfiguration;
			if (!string.IsNullOrEmpty(this.LocationConfigPath))
			{
				Configuration parentWithFile = this.GetParentWithFile();
				if (parentWithFile != null)
				{
					string configPathFromLocationSubPath = this.system.Host.GetConfigPathFromLocationSubPath(this.configPath, relativePath);
					configuration = parentWithFile.FindLocationConfiguration(configPathFromLocationSubPath, defaultConfiguration);
				}
			}
			string text = this.configPath.Substring(1) + "/";
			if (relativePath.StartsWith(text, StringComparison.Ordinal))
			{
				relativePath = relativePath.Substring(text.Length);
			}
			ConfigurationLocation configurationLocation = this.Locations.FindBest(relativePath);
			if (configurationLocation == null)
			{
				return configuration;
			}
			configurationLocation.SetParentConfiguration(configuration);
			return configurationLocation.OpenConfiguration();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002340 File Offset: 0x00000540
		internal void Init(IConfigSystem system, string configPath, Configuration parent)
		{
			this.system = system;
			this.configPath = configPath;
			this.streamName = system.Host.GetStreamName(configPath);
			this.parent = parent;
			if (parent != null)
			{
				this.rootGroup = parent.rootGroup;
			}
			else
			{
				this.rootGroup = new SectionGroupInfo();
				this.rootGroup.StreamName = this.streamName;
			}
			try
			{
				if (this.streamName != null)
				{
					this.Load();
				}
			}
			catch (XmlException ex)
			{
				throw new ConfigurationErrorsException(ex.Message, ex, this.streamName, 0);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023D8 File Offset: 0x000005D8
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000023E0 File Offset: 0x000005E0
		internal Configuration Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023EC File Offset: 0x000005EC
		internal Configuration GetParentWithFile()
		{
			Configuration configuration = this.Parent;
			while (configuration != null && !configuration.HasFile)
			{
				configuration = configuration.Parent;
			}
			return configuration;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002415 File Offset: 0x00000615
		internal IInternalConfigHost ConfigHost
		{
			get
			{
				return this.system.Host;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002422 File Offset: 0x00000622
		internal string LocationConfigPath
		{
			get
			{
				return this.locationConfigPath;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000242C File Offset: 0x0000062C
		internal string GetLocationSubPath()
		{
			Configuration configuration = this.parent;
			string text = null;
			while (configuration != null)
			{
				text = configuration.locationSubPath;
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				configuration = configuration.parent;
			}
			return text;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002460 File Offset: 0x00000660
		internal string ConfigPath
		{
			get
			{
				return this.configPath;
			}
		}

		/// <summary>Gets the physical path to the configuration file represented by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The physical path to the configuration file represented by this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002468 File Offset: 0x00000668
		public string FilePath
		{
			get
			{
				if (this.streamName == null && this.parent != null)
				{
					return this.parent.FilePath;
				}
				return this.streamName;
			}
		}

		/// <summary>Gets a value that indicates whether a file exists for the resource represented by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>true if there is a configuration file; otherwise, false.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000248C File Offset: 0x0000068C
		public bool HasFile
		{
			get
			{
				return this.hasFile;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002494 File Offset: 0x00000694
		public ContextInformation EvaluationContext
		{
			get
			{
				if (this.evaluationContext == null)
				{
					object obj = this.system.Host.CreateConfigurationContext(this.configPath, this.GetLocationSubPath());
					this.evaluationContext = new ContextInformation(this, obj);
				}
				return this.evaluationContext;
			}
		}

		/// <summary>Gets the locations defined within this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationLocationCollection" /> containing the locations defined within this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000024D9 File Offset: 0x000006D9
		public ConfigurationLocationCollection Locations
		{
			get
			{
				if (this.locations == null)
				{
					this.locations = new ConfigurationLocationCollection();
				}
				return this.locations;
			}
		}

		/// <summary>Gets the root <see cref="T:System.Configuration.ConfigurationSectionGroup" /> for this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The root section group for this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000024F4 File Offset: 0x000006F4
		public ConfigurationSectionGroup RootSectionGroup
		{
			get
			{
				if (this.rootSectionGroup == null)
				{
					this.rootSectionGroup = new ConfigurationSectionGroup();
					this.rootSectionGroup.Initialize(this, this.rootGroup);
				}
				return this.rootSectionGroup;
			}
		}

		/// <summary>Gets a collection of the section groups defined by this configuration.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> collection representing the collection of section groups for this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002521 File Offset: 0x00000721
		public ConfigurationSectionGroupCollection SectionGroups
		{
			get
			{
				return this.RootSectionGroup.SectionGroups;
			}
		}

		/// <summary>Gets a collection of the sections defined by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A collection of the sections defined by this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000252E File Offset: 0x0000072E
		public ConfigurationSectionCollection Sections
		{
			get
			{
				return this.RootSectionGroup.Sections;
			}
		}

		/// <summary>Returns the specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
		/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		/// <param name="sectionName">The path to the section to be returned.</param>
		// Token: 0x0600002B RID: 43 RVA: 0x0000253C File Offset: 0x0000073C
		public ConfigurationSection GetSection(string sectionName)
		{
			string[] array = sectionName.Split('/', StringSplitOptions.None);
			if (array.Length == 1)
			{
				return this.Sections[array[0]];
			}
			ConfigurationSectionGroup configurationSectionGroup = this.SectionGroups[array[0]];
			int num = 1;
			while (configurationSectionGroup != null && num < array.Length - 1)
			{
				configurationSectionGroup = configurationSectionGroup.SectionGroups[array[num]];
				num++;
			}
			if (configurationSectionGroup != null)
			{
				return configurationSectionGroup.Sections[array[array.Length - 1]];
			}
			return null;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025B0 File Offset: 0x000007B0
		internal ConfigurationSection GetSectionInstance(SectionInfo config, bool createDefaultInstance)
		{
			object obj = this.elementData[config];
			ConfigurationSection configurationSection = obj as ConfigurationSection;
			if (configurationSection != null || !createDefaultInstance)
			{
				return configurationSection;
			}
			object obj2 = config.CreateInstance();
			configurationSection = obj2 as ConfigurationSection;
			if (configurationSection == null)
			{
				configurationSection = new DefaultSection
				{
					SectionHandler = (obj2 as IConfigurationSectionHandler)
				};
			}
			configurationSection.Configuration = this;
			ConfigurationSection configurationSection2 = null;
			if (this.parent != null)
			{
				configurationSection2 = this.parent.GetSectionInstance(config, true);
				configurationSection.SectionInformation.SetParentSection(configurationSection2);
			}
			configurationSection.SectionInformation.ConfigFilePath = this.FilePath;
			configurationSection.ConfigContext = this.system.Host.CreateDeprecatedConfigContext(this.configPath);
			string text = obj as string;
			configurationSection.RawXml = text;
			configurationSection.Reset(configurationSection2);
			if (text != null)
			{
				XmlTextReader xmlTextReader = new ConfigXmlTextReader(new StringReader(text), this.FilePath);
				configurationSection.DeserializeSection(xmlTextReader);
				xmlTextReader.Close();
				if (!string.IsNullOrEmpty(configurationSection.SectionInformation.ConfigSource) && !string.IsNullOrEmpty(this.FilePath))
				{
					configurationSection.DeserializeConfigSource(Path.GetDirectoryName(this.FilePath));
				}
			}
			this.elementData[config] = configurationSection;
			return configurationSection;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026D0 File Offset: 0x000008D0
		internal ConfigurationSectionGroup GetSectionGroupInstance(SectionGroupInfo group)
		{
			ConfigurationSectionGroup configurationSectionGroup = group.CreateInstance() as ConfigurationSectionGroup;
			if (configurationSectionGroup != null)
			{
				configurationSectionGroup.Initialize(this, group);
			}
			return configurationSectionGroup;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026F5 File Offset: 0x000008F5
		internal void SetSectionXml(SectionInfo config, string data)
		{
			this.elementData[config] = data;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002704 File Offset: 0x00000904
		internal string GetSectionXml(SectionInfo config)
		{
			return this.elementData[config] as string;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002718 File Offset: 0x00000918
		private void ResetModified()
		{
			foreach (object obj in this.Locations)
			{
				ConfigurationLocation configurationLocation = (ConfigurationLocation)obj;
				if (configurationLocation.OpenedConfiguration != null)
				{
					configurationLocation.OpenedConfiguration.ResetModified();
				}
			}
			this.rootGroup.ResetModified(this);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000278C File Offset: 0x0000098C
		private bool Load()
		{
			if (string.IsNullOrEmpty(this.streamName))
			{
				return true;
			}
			Stream stream = null;
			try
			{
				stream = this.system.Host.OpenStreamForRead(this.streamName);
				if (stream == null)
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
			using (XmlTextReader xmlTextReader = new ConfigXmlTextReader(stream, this.streamName))
			{
				this.ReadConfigFile(xmlTextReader, this.streamName);
			}
			this.ResetModified();
			return true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000281C File Offset: 0x00000A1C
		private void ReadConfigFile(XmlReader reader, string fileName)
		{
			reader.MoveToContent();
			if (reader.NodeType != XmlNodeType.Element || reader.Name != "configuration")
			{
				this.ThrowException("Configuration file does not have a valid root element", reader);
			}
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "xmlns")
					{
						this.rootNamespace = reader.Value;
					}
					else
					{
						this.ThrowException(string.Format("Unrecognized attribute '{0}' in root element", reader.LocalName), reader);
					}
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			reader.MoveToContent();
			if (reader.LocalName == "configSections")
			{
				if (reader.HasAttributes)
				{
					this.ThrowException("Unrecognized attribute in <configSections>.", reader);
				}
				this.rootGroup.ReadConfig(this, fileName, reader);
			}
			this.rootGroup.ReadRootData(reader, this, true);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002905 File Offset: 0x00000B05
		internal void ReadData(XmlReader reader, bool allowOverride)
		{
			this.rootGroup.ReadData(this, reader, allowOverride);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002918 File Offset: 0x00000B18
		private void ThrowException(string text, XmlReader reader)
		{
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			throw new ConfigurationErrorsException(text, this.streamName, (xmlLineInfo != null) ? xmlLineInfo.LineNumber : 0);
		}

		// Token: 0x0400000B RID: 11
		private Configuration parent;

		// Token: 0x0400000C RID: 12
		private Hashtable elementData = new Hashtable();

		// Token: 0x0400000D RID: 13
		private string streamName;

		// Token: 0x0400000E RID: 14
		private ConfigurationSectionGroup rootSectionGroup;

		// Token: 0x0400000F RID: 15
		private ConfigurationLocationCollection locations;

		// Token: 0x04000010 RID: 16
		private SectionGroupInfo rootGroup;

		// Token: 0x04000011 RID: 17
		private IConfigSystem system;

		// Token: 0x04000012 RID: 18
		private bool hasFile;

		// Token: 0x04000013 RID: 19
		private string rootNamespace;

		// Token: 0x04000014 RID: 20
		private string configPath;

		// Token: 0x04000015 RID: 21
		private string locationConfigPath;

		// Token: 0x04000016 RID: 22
		private string locationSubPath;

		// Token: 0x04000017 RID: 23
		private ContextInformation evaluationContext;
	}
}
