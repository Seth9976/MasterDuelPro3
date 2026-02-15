using System;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a location element within a configuration file.</summary>
	// Token: 0x02000015 RID: 21
	public class ConfigurationLocation
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00004924 File Offset: 0x00002B24
		internal ConfigurationLocation(string path, string xmlContent, Configuration parent, bool allowOverride)
		{
			if (!string.IsNullOrEmpty(path))
			{
				char c = path[0];
				if (c <= '.')
				{
					if (c != ' ' && c != '.')
					{
						goto IL_003C;
					}
				}
				else if (c != '/' && c != '\\')
				{
					goto IL_003C;
				}
				throw new ConfigurationErrorsException("<location> path attribute must be a relative virtual path.  It cannot start with any of ' ' '.' '/' or '\\'.");
				IL_003C:
				path = path.TrimEnd(ConfigurationLocation.pathTrimChars);
			}
			this.path = path;
			this.xmlContent = xmlContent;
			this.parent = parent;
			this.allowOverride = allowOverride;
		}

		/// <summary>Gets the relative path to the resource whose configuration settings are represented by this <see cref="T:System.Configuration.ConfigurationLocation" /> object.</summary>
		/// <returns>The relative path to the resource whose configuration settings are represented by this <see cref="T:System.Configuration.ConfigurationLocation" />.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004997 File Offset: 0x00002B97
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000499F File Offset: 0x00002B9F
		internal Configuration OpenedConfiguration
		{
			get
			{
				return this.configuration;
			}
		}

		/// <summary>Creates an instance of a Configuration object.</summary>
		/// <returns>A Configuration object.</returns>
		// Token: 0x060000B7 RID: 183 RVA: 0x000049A8 File Offset: 0x00002BA8
		public Configuration OpenConfiguration()
		{
			if (this.configuration == null)
			{
				if (!this.parentResolved)
				{
					Configuration parentWithFile = this.parent.GetParentWithFile();
					if (parentWithFile != null)
					{
						string configPathFromLocationSubPath = this.parent.ConfigHost.GetConfigPathFromLocationSubPath(this.parent.LocationConfigPath, this.path);
						this.parent = parentWithFile.FindLocationConfiguration(configPathFromLocationSubPath, this.parent);
					}
				}
				this.configuration = new Configuration(this.parent, this.path);
				using (XmlTextReader xmlTextReader = new ConfigXmlTextReader(new StringReader(this.xmlContent), this.path))
				{
					this.configuration.ReadData(xmlTextReader, this.allowOverride);
				}
				this.xmlContent = null;
			}
			return this.configuration;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004A78 File Offset: 0x00002C78
		internal void SetParentConfiguration(Configuration parent)
		{
			if (this.parentResolved)
			{
				return;
			}
			this.parentResolved = true;
			this.parent = parent;
			if (this.configuration != null)
			{
				this.configuration.Parent = parent;
			}
		}

		// Token: 0x04000051 RID: 81
		private static readonly char[] pathTrimChars = new char[] { '/' };

		// Token: 0x04000052 RID: 82
		private string path;

		// Token: 0x04000053 RID: 83
		private Configuration configuration;

		// Token: 0x04000054 RID: 84
		private Configuration parent;

		// Token: 0x04000055 RID: 85
		private string xmlContent;

		// Token: 0x04000056 RID: 86
		private bool parentResolved;

		// Token: 0x04000057 RID: 87
		private bool allowOverride;
	}
}
