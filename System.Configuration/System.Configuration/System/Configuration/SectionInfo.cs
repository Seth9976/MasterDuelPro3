using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000040 RID: 64
	internal class SectionInfo : ConfigInfo
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000072D4 File Offset: 0x000054D4
		public override object CreateInstance()
		{
			object obj = base.CreateInstance();
			ConfigurationSection configurationSection = obj as ConfigurationSection;
			if (configurationSection != null)
			{
				configurationSection.SectionInformation.AllowLocation = this.allowLocation;
				configurationSection.SectionInformation.AllowDefinition = this.allowDefinition;
				configurationSection.SectionInformation.AllowExeDefinition = this.allowExeDefinition;
				configurationSection.SectionInformation.RequirePermission = this.requirePermission;
				configurationSection.SectionInformation.RestartOnExternalChanges = this.restartOnExternalChanges;
				configurationSection.SectionInformation.SetName(this.Name);
			}
			return obj;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007358 File Offset: 0x00005558
		public override void ReadConfig(Configuration cfg, string streamName, XmlReader reader)
		{
			base.StreamName = streamName;
			this.ConfigHost = cfg.ConfigHost;
			while (reader.MoveToNextAttribute())
			{
				string name = reader.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 1766272347U)
				{
					if (num != 1066839313U)
					{
						if (num != 1361572173U)
						{
							if (num != 1766272347U)
							{
								goto IL_029E;
							}
							if (!(name == "requirePermission"))
							{
								goto IL_029E;
							}
							string value = reader.Value;
							bool flag = value == "true";
							if (!flag && value != "false")
							{
								base.ThrowException("Invalid attribute value", reader);
							}
							this.requirePermission = flag;
							continue;
						}
						else if (!(name == "type"))
						{
							goto IL_029E;
						}
					}
					else
					{
						if (!(name == "allowLocation"))
						{
							goto IL_029E;
						}
						string value2 = reader.Value;
						this.allowLocation = value2 == "true";
						if (!this.allowLocation && value2 != "false")
						{
							base.ThrowException("Invalid attribute value", reader);
							continue;
						}
						continue;
					}
				}
				else
				{
					if (num <= 1931054735U)
					{
						if (num != 1841158919U)
						{
							if (num != 1931054735U)
							{
								goto IL_029E;
							}
							if (!(name == "allowExeDefinition"))
							{
								goto IL_029E;
							}
						}
						else
						{
							if (!(name == "restartOnExternalChanges"))
							{
								goto IL_029E;
							}
							string value3 = reader.Value;
							bool flag2 = value3 == "true";
							if (!flag2 && value3 != "false")
							{
								base.ThrowException("Invalid attribute value", reader);
							}
							this.restartOnExternalChanges = flag2;
							continue;
						}
					}
					else if (num != 2369371622U)
					{
						if (num != 3263379011U)
						{
							goto IL_029E;
						}
						if (!(name == "allowDefinition"))
						{
							goto IL_029E;
						}
						string value4 = reader.Value;
						try
						{
							this.allowDefinition = (ConfigurationAllowDefinition)Enum.Parse(typeof(ConfigurationAllowDefinition), value4);
							continue;
						}
						catch
						{
							base.ThrowException("Invalid attribute value", reader);
							continue;
						}
					}
					else
					{
						if (!(name == "name"))
						{
							goto IL_029E;
						}
						this.Name = reader.Value;
						if (this.Name == "location")
						{
							base.ThrowException("location is a reserved section name", reader);
							continue;
						}
						continue;
					}
					string value5 = reader.Value;
					try
					{
						this.allowExeDefinition = (ConfigurationAllowExeDefinition)Enum.Parse(typeof(ConfigurationAllowExeDefinition), value5);
						continue;
					}
					catch
					{
						base.ThrowException("Invalid attribute value", reader);
						continue;
					}
				}
				this.TypeName = reader.Value;
				continue;
				IL_029E:
				base.ThrowException(string.Format("Unrecognized attribute: {0}", reader.Name), reader);
			}
			if (this.Name == null || this.TypeName == null)
			{
				base.ThrowException("Required attribute missing", reader);
			}
			reader.MoveToElement();
			reader.Skip();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000766C File Offset: 0x0000586C
		public override void ReadData(Configuration config, XmlReader reader, bool overrideAllowed)
		{
			if (!config.HasFile && !this.allowLocation)
			{
				throw new ConfigurationErrorsException("The configuration section <" + this.Name + "> cannot be defined inside a <location> element.", reader);
			}
			if (!config.ConfigHost.IsDefinitionAllowed(config.ConfigPath, this.allowDefinition, this.allowExeDefinition))
			{
				object obj = ((this.allowExeDefinition != ConfigurationAllowExeDefinition.MachineToApplication) ? this.allowExeDefinition : this.allowDefinition);
				throw new ConfigurationErrorsException(string.Concat(new string[]
				{
					"The section <",
					this.Name,
					"> can't be defined in this configuration file (the allowed definition context is '",
					(obj != null) ? obj.ToString() : null,
					"')."
				}), reader);
			}
			if (config.GetSectionXml(this) != null)
			{
				base.ThrowException("The section <" + this.Name + "> is defined more than once in the same configuration file.", reader);
			}
			config.SetSectionXml(this, reader.ReadOuterXml());
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000029FD File Offset: 0x00000BFD
		internal override void Merge(ConfigInfo data)
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000775C File Offset: 0x0000595C
		internal override void ResetModified(Configuration config)
		{
			ConfigurationSection sectionInstance = config.GetSectionInstance(this, false);
			if (sectionInstance != null)
			{
				sectionInstance.ResetModified();
			}
		}

		// Token: 0x040000CA RID: 202
		private bool allowLocation = true;

		// Token: 0x040000CB RID: 203
		private bool requirePermission = true;

		// Token: 0x040000CC RID: 204
		private bool restartOnExternalChanges;

		// Token: 0x040000CD RID: 205
		private ConfigurationAllowDefinition allowDefinition = ConfigurationAllowDefinition.Everywhere;

		// Token: 0x040000CE RID: 206
		private ConfigurationAllowExeDefinition allowExeDefinition = ConfigurationAllowExeDefinition.MachineToApplication;
	}
}
