using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace System.Windows.Forms
{
	// Token: 0x02000098 RID: 152
	internal class UnixFileSystem : FileSystem
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x000198CC File Offset: 0x00017ACC
		public UnixFileSystem()
		{
			this.personal_folder = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
			this.recently_used_path = Path.Combine(this.personal_folder, ".recently-used");
			this.full_kde_recent_document_dir = this.personal_folder + "/.kde/share/apps/RecentDocuments";
			this.desktopFSEntry = new FSEntry();
			this.desktopFSEntry.Attributes = FileAttributes.Directory;
			this.desktopFSEntry.FullName = MWFVFS.DesktopPrefix;
			this.desktopFSEntry.Name = Locale.GetText("Desktop");
			this.desktopFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesDesktop);
			this.desktopFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.desktopFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("desktop/desktop");
			this.desktopFSEntry.LastAccessTime = DateTime.Now;
			this.recentlyusedFSEntry = new FSEntry();
			this.recentlyusedFSEntry.Attributes = FileAttributes.Directory;
			this.recentlyusedFSEntry.FullName = MWFVFS.RecentlyUsedPrefix;
			this.recentlyusedFSEntry.Name = Locale.GetText("Recently Used");
			this.recentlyusedFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.recentlyusedFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("recently/recently");
			this.recentlyusedFSEntry.LastAccessTime = DateTime.Now;
			this.personalFSEntry = new FSEntry();
			this.personalFSEntry.Attributes = FileAttributes.Directory;
			this.personalFSEntry.FullName = MWFVFS.PersonalPrefix;
			this.personalFSEntry.Name = Locale.GetText("Personal");
			this.personalFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.personalFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
			this.personalFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.personalFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("directory/home");
			this.personalFSEntry.LastAccessTime = DateTime.Now;
			this.mycomputerpersonalFSEntry = new FSEntry();
			this.mycomputerpersonalFSEntry.Attributes = FileAttributes.Directory;
			this.mycomputerpersonalFSEntry.FullName = MWFVFS.MyComputerPersonalPrefix;
			this.mycomputerpersonalFSEntry.Name = Locale.GetText("Personal");
			this.mycomputerpersonalFSEntry.MainTopNode = this.GetMyComputerFSEntry();
			this.mycomputerpersonalFSEntry.RealName = ThemeEngine.Current.Places(UIIcon.PlacesPersonal);
			this.mycomputerpersonalFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mycomputerpersonalFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("directory/home");
			this.mycomputerpersonalFSEntry.LastAccessTime = DateTime.Now;
			this.mycomputerFSEntry = new FSEntry();
			this.mycomputerFSEntry.Attributes = FileAttributes.Directory;
			this.mycomputerFSEntry.FullName = MWFVFS.MyComputerPrefix;
			this.mycomputerFSEntry.Name = Locale.GetText("My Computer");
			this.mycomputerFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.mycomputerFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mycomputerFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("workplace/workplace");
			this.mycomputerFSEntry.LastAccessTime = DateTime.Now;
			this.mynetworkFSEntry = new FSEntry();
			this.mynetworkFSEntry.Attributes = FileAttributes.Directory;
			this.mynetworkFSEntry.FullName = MWFVFS.MyNetworkPrefix;
			this.mynetworkFSEntry.Name = Locale.GetText("My Network");
			this.mynetworkFSEntry.MainTopNode = this.GetDesktopFSEntry();
			this.mynetworkFSEntry.FileType = FSEntry.FSEntryType.Directory;
			this.mynetworkFSEntry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("network/network");
			this.mynetworkFSEntry.LastAccessTime = DateTime.Now;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00019C44 File Offset: 0x00017E44
		public override void WriteRecentlyUsedFiles(string fileToAdd)
		{
			if (File.Exists(this.recently_used_path) && new FileInfo(this.recently_used_path).Length > 0L)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(this.recently_used_path);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("RecentFiles");
				if (xmlNode == null)
				{
					return;
				}
				XmlElement xmlElement = xmlDocument.CreateElement("RecentItem");
				XmlElement xmlElement2 = xmlDocument.CreateElement("URI");
				UriBuilder uriBuilder = new UriBuilder();
				uriBuilder.Path = fileToAdd;
				uriBuilder.Host = null;
				uriBuilder.Scheme = "file";
				XmlText xmlText = xmlDocument.CreateTextNode(uriBuilder.ToString());
				xmlElement2.AppendChild(xmlText);
				xmlElement.AppendChild(xmlElement2);
				xmlElement2 = xmlDocument.CreateElement("Mime-Type");
				xmlText = xmlDocument.CreateTextNode(Mime.GetMimeTypeForFile(fileToAdd));
				xmlElement2.AppendChild(xmlText);
				xmlElement.AppendChild(xmlElement2);
				xmlElement2 = xmlDocument.CreateElement("Timestamp");
				xmlText = xmlDocument.CreateTextNode(((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString());
				xmlElement2.AppendChild(xmlText);
				xmlElement.AppendChild(xmlElement2);
				xmlElement2 = xmlDocument.CreateElement("Groups");
				xmlElement.AppendChild(xmlElement2);
				foreach (object obj in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode("URI");
					if (xmlNode3 != null)
					{
						XmlNode firstChild = xmlNode3.FirstChild;
						if (firstChild is XmlText && uriBuilder.ToString() == ((XmlText)firstChild).Data)
						{
							xmlNode.RemoveChild(xmlNode2);
							break;
						}
					}
				}
				xmlNode.PrependChild(xmlElement);
				if (xmlNode.ChildNodes.Count > 10)
				{
					while (xmlNode.ChildNodes.Count > 10)
					{
						xmlNode.RemoveChild(xmlNode.LastChild);
					}
				}
				try
				{
					xmlDocument.Save(this.recently_used_path);
					return;
				}
				catch (Exception)
				{
					return;
				}
			}
			XmlDocument xmlDocument2 = new XmlDocument();
			xmlDocument2.AppendChild(xmlDocument2.CreateXmlDeclaration("1.0", string.Empty, string.Empty));
			XmlElement xmlElement3 = xmlDocument2.CreateElement("RecentFiles");
			XmlElement xmlElement4 = xmlDocument2.CreateElement("RecentItem");
			XmlElement xmlElement5 = xmlDocument2.CreateElement("URI");
			XmlText xmlText2 = xmlDocument2.CreateTextNode(new UriBuilder
			{
				Path = fileToAdd,
				Host = null,
				Scheme = "file"
			}.ToString());
			xmlElement5.AppendChild(xmlText2);
			xmlElement4.AppendChild(xmlElement5);
			xmlElement5 = xmlDocument2.CreateElement("Mime-Type");
			xmlText2 = xmlDocument2.CreateTextNode(Mime.GetMimeTypeForFile(fileToAdd));
			xmlElement5.AppendChild(xmlText2);
			xmlElement4.AppendChild(xmlElement5);
			xmlElement5 = xmlDocument2.CreateElement("Timestamp");
			xmlText2 = xmlDocument2.CreateTextNode(((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString());
			xmlElement5.AppendChild(xmlText2);
			xmlElement4.AppendChild(xmlElement5);
			xmlElement5 = xmlDocument2.CreateElement("Groups");
			xmlElement4.AppendChild(xmlElement5);
			xmlElement3.AppendChild(xmlElement4);
			xmlDocument2.AppendChild(xmlElement3);
			try
			{
				xmlDocument2.Save(this.recently_used_path);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00019FD8 File Offset: 0x000181D8
		public override ArrayList GetRecentlyUsedFiles()
		{
			ArrayList arrayList = new ArrayList();
			if (File.Exists(this.recently_used_path))
			{
				try
				{
					XmlTextReader xmlTextReader = new XmlTextReader(this.recently_used_path);
					while (xmlTextReader.Read())
					{
						if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name.ToUpper() == "URI")
						{
							xmlTextReader.Read();
							Uri uri = new Uri(xmlTextReader.Value);
							if (!arrayList.Contains(uri.LocalPath) && File.Exists(uri.LocalPath))
							{
								FSEntry fileFSEntry = this.GetFileFSEntry(new FileInfo(uri.LocalPath));
								if (fileFSEntry != null)
								{
									arrayList.Add(fileFSEntry);
								}
							}
						}
					}
					xmlTextReader.Close();
				}
				catch (Exception)
				{
				}
			}
			if (Directory.Exists(this.full_kde_recent_document_dir))
			{
				string[] files = Directory.GetFiles(this.full_kde_recent_document_dir, "*.desktop");
				for (int i = 0; i < files.Length; i++)
				{
					StreamReader streamReader = new StreamReader(files[i]);
					string text = streamReader.ReadLine();
					while (text != null)
					{
						text = text.Trim();
						if (text.StartsWith("URL="))
						{
							text = text.Replace("URL=", string.Empty);
							text = text.Replace("$HOME", this.personal_folder);
							Uri uri2 = new Uri(text);
							if (arrayList.Contains(uri2.LocalPath) || !File.Exists(uri2.LocalPath))
							{
								break;
							}
							FSEntry fileFSEntry2 = this.GetFileFSEntry(new FileInfo(uri2.LocalPath));
							if (fileFSEntry2 != null)
							{
								arrayList.Add(fileFSEntry2);
								break;
							}
							break;
						}
						else
						{
							text = streamReader.ReadLine();
						}
					}
					streamReader.Close();
				}
			}
			return arrayList;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001A18C File Offset: 0x0001838C
		public override ArrayList GetMyComputerContent()
		{
			ArrayList arrayList = new ArrayList();
			if (this.masterMount.ProcMountAvailable)
			{
				this.masterMount.GetMounts();
				foreach (object obj in this.masterMount.Block_devices)
				{
					MasterMount.Mount mount = (MasterMount.Mount)obj;
					FSEntry fsentry = new FSEntry();
					fsentry.FileType = FSEntry.FSEntryType.Device;
					fsentry.FullName = mount.mount_point;
					fsentry.Name = string.Concat(new object[] { "HDD (", mount.fsType, ", ", mount.device_short, ")" });
					fsentry.FsType = mount.fsType;
					fsentry.DeviceShort = mount.device_short;
					fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("harddisk/harddisk");
					fsentry.Attributes = FileAttributes.Directory;
					fsentry.MainTopNode = this.GetMyComputerFSEntry();
					arrayList.Add(fsentry);
					if (!MWFVFS.MyComputerDevicesPrefix.Contains(fsentry.FullName + "://"))
					{
						MWFVFS.MyComputerDevicesPrefix.Add(fsentry.FullName + "://", fsentry);
					}
				}
				foreach (object obj2 in this.masterMount.Removable_devices)
				{
					MasterMount.Mount mount2 = (MasterMount.Mount)obj2;
					FSEntry fsentry2 = new FSEntry();
					fsentry2.FileType = FSEntry.FSEntryType.RemovableDevice;
					fsentry2.FullName = mount2.mount_point;
					int num = ((mount2.fsType == MasterMount.FsTypes.usbfs) ? 0 : 1);
					string text = ((num != 0) ? "DVD/CD-Rom" : "USB");
					string text2 = ((num != 0) ? "cdrom/cdrom" : "removable/removable");
					fsentry2.Name = text + " (" + mount2.device_short + ")";
					fsentry2.IconIndex = MimeIconEngine.GetIconIndexForMimeType(text2);
					fsentry2.FsType = mount2.fsType;
					fsentry2.DeviceShort = mount2.device_short;
					fsentry2.Attributes = FileAttributes.Directory;
					fsentry2.MainTopNode = this.GetMyComputerFSEntry();
					arrayList.Add(fsentry2);
					string text3 = fsentry2.FullName + "://";
					if (!MWFVFS.MyComputerDevicesPrefix.Contains(text3))
					{
						MWFVFS.MyComputerDevicesPrefix.Add(text3, fsentry2);
					}
				}
			}
			arrayList.Add(this.GetMyComputerPersonalFSEntry());
			return arrayList;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001A43C File Offset: 0x0001863C
		public override ArrayList GetMyNetworkContent()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.masterMount.Network_devices)
			{
				MasterMount.Mount mount = (MasterMount.Mount)obj;
				FSEntry fsentry = new FSEntry();
				fsentry.FileType = FSEntry.FSEntryType.Network;
				fsentry.FullName = mount.mount_point;
				fsentry.FsType = mount.fsType;
				fsentry.DeviceShort = mount.device_short;
				fsentry.Name = string.Concat(new object[] { "Network (", mount.fsType, ", ", mount.device_short, ")" });
				switch (mount.fsType)
				{
				case MasterMount.FsTypes.ncpfs:
					fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("network/network");
					break;
				case MasterMount.FsTypes.nfs:
					fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("nfs/nfs");
					break;
				case MasterMount.FsTypes.smbfs:
					fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("smb/smb");
					break;
				case MasterMount.FsTypes.cifs:
					fsentry.IconIndex = MimeIconEngine.GetIconIndexForMimeType("network/network");
					break;
				}
				fsentry.Attributes = FileAttributes.Directory;
				fsentry.MainTopNode = this.GetMyNetworkFSEntry();
				arrayList.Add(fsentry);
			}
			return arrayList;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001A5AC File Offset: 0x000187AC
		protected override FSEntry GetDesktopFSEntry()
		{
			return this.desktopFSEntry;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001A5B4 File Offset: 0x000187B4
		protected override FSEntry GetRecentlyUsedFSEntry()
		{
			return this.recentlyusedFSEntry;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001A5BC File Offset: 0x000187BC
		protected override FSEntry GetPersonalFSEntry()
		{
			return this.personalFSEntry;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001A5C4 File Offset: 0x000187C4
		protected override FSEntry GetMyComputerPersonalFSEntry()
		{
			return this.mycomputerpersonalFSEntry;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001A5CC File Offset: 0x000187CC
		protected override FSEntry GetMyComputerFSEntry()
		{
			return this.mycomputerFSEntry;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001A5D4 File Offset: 0x000187D4
		protected override FSEntry GetMyNetworkFSEntry()
		{
			return this.mynetworkFSEntry;
		}

		// Token: 0x040003DE RID: 990
		private MasterMount masterMount = new MasterMount();

		// Token: 0x040003DF RID: 991
		private FSEntry desktopFSEntry;

		// Token: 0x040003E0 RID: 992
		private FSEntry recentlyusedFSEntry;

		// Token: 0x040003E1 RID: 993
		private FSEntry personalFSEntry;

		// Token: 0x040003E2 RID: 994
		private FSEntry mycomputerpersonalFSEntry;

		// Token: 0x040003E3 RID: 995
		private FSEntry mycomputerFSEntry;

		// Token: 0x040003E4 RID: 996
		private FSEntry mynetworkFSEntry;

		// Token: 0x040003E5 RID: 997
		private string personal_folder;

		// Token: 0x040003E6 RID: 998
		private string recently_used_path;

		// Token: 0x040003E7 RID: 999
		private string full_kde_recent_document_dir;
	}
}
