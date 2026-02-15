using System;
using System.Collections;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x0200009C RID: 156
	internal class MasterMount
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x0001AE8C File Offset: 0x0001908C
		public MasterMount()
		{
			if (XplatUI.RunningOnUnix && File.Exists("/proc/mounts"))
			{
				this.proc_mount_available = true;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0001AEE5 File Offset: 0x000190E5
		public ArrayList Block_devices
		{
			get
			{
				return this.block_devices;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001AEED File Offset: 0x000190ED
		public ArrayList Network_devices
		{
			get
			{
				return this.network_devices;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001AEF5 File Offset: 0x000190F5
		public ArrayList Removable_devices
		{
			get
			{
				return this.removable_devices;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001AEFD File Offset: 0x000190FD
		public bool ProcMountAvailable
		{
			get
			{
				return this.proc_mount_available;
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001AF08 File Offset: 0x00019108
		public void GetMounts()
		{
			if (!this.proc_mount_available)
			{
				return;
			}
			this.block_devices.Clear();
			this.network_devices.Clear();
			this.removable_devices.Clear();
			try
			{
				StreamReader streamReader = new StreamReader("/proc/mounts");
				string text = streamReader.ReadLine();
				ArrayList arrayList = new ArrayList();
				while (text != null)
				{
					if (arrayList.IndexOf(text) == -1)
					{
						this.ProcessProcMountLine(text);
						arrayList.Add(text);
					}
					text = streamReader.ReadLine();
				}
				streamReader.Close();
				this.block_devices.Sort(this.mountComparer);
				this.network_devices.Sort(this.mountComparer);
				this.removable_devices.Sort(this.mountComparer);
			}
			catch
			{
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001AFCC File Offset: 0x000191CC
		private void ProcessProcMountLine(string line)
		{
			string[] array = line.Split(new char[] { ' ' });
			if (array != null && array.Length != 0)
			{
				MasterMount.Mount mount = default(MasterMount.Mount);
				if (array[0].StartsWith("/dev/"))
				{
					mount.device_short = array[0].Replace("/dev/", string.Empty);
				}
				else
				{
					mount.device_short = array[0];
				}
				mount.device_or_filesystem = array[0];
				mount.mount_point = array[1];
				if (array[2] == "nfs")
				{
					mount.fsType = MasterMount.FsTypes.nfs;
					this.network_devices.Add(mount);
					return;
				}
				if (array[2] == "smbfs")
				{
					mount.fsType = MasterMount.FsTypes.smbfs;
					this.network_devices.Add(mount);
					return;
				}
				if (array[2] == "cifs")
				{
					mount.fsType = MasterMount.FsTypes.cifs;
					this.network_devices.Add(mount);
					return;
				}
				if (array[2] == "ncpfs")
				{
					mount.fsType = MasterMount.FsTypes.ncpfs;
					this.network_devices.Add(mount);
					return;
				}
				if (array[2] == "iso9660")
				{
					mount.fsType = MasterMount.FsTypes.iso9660;
					this.removable_devices.Add(mount);
					return;
				}
				if (array[2] == "usbfs")
				{
					mount.fsType = MasterMount.FsTypes.usbfs;
					this.removable_devices.Add(mount);
					return;
				}
				if (array[0].StartsWith("/"))
				{
					if (array[1].StartsWith("/dev/"))
					{
						return;
					}
					if (array[2] == "ext2")
					{
						mount.fsType = MasterMount.FsTypes.ext2;
					}
					else if (array[2] == "ext3")
					{
						mount.fsType = MasterMount.FsTypes.ext3;
					}
					else if (array[2] == "reiserfs")
					{
						mount.fsType = MasterMount.FsTypes.reiserfs;
					}
					else if (array[2] == "xfs")
					{
						mount.fsType = MasterMount.FsTypes.xfs;
					}
					else if (array[2] == "vfat")
					{
						mount.fsType = MasterMount.FsTypes.vfat;
					}
					else if (array[2] == "ntfs")
					{
						mount.fsType = MasterMount.FsTypes.ntfs;
					}
					else if (array[2] == "msdos")
					{
						mount.fsType = MasterMount.FsTypes.msdos;
					}
					else if (array[2] == "umsdos")
					{
						mount.fsType = MasterMount.FsTypes.umsdos;
					}
					else if (array[2] == "hpfs")
					{
						mount.fsType = MasterMount.FsTypes.hpfs;
					}
					else if (array[2] == "minix")
					{
						mount.fsType = MasterMount.FsTypes.minix;
					}
					else if (array[2] == "jfs")
					{
						mount.fsType = MasterMount.FsTypes.jfs;
					}
					this.block_devices.Add(mount);
				}
			}
		}

		// Token: 0x04000405 RID: 1029
		private bool proc_mount_available;

		// Token: 0x04000406 RID: 1030
		private ArrayList block_devices = new ArrayList();

		// Token: 0x04000407 RID: 1031
		private ArrayList network_devices = new ArrayList();

		// Token: 0x04000408 RID: 1032
		private ArrayList removable_devices = new ArrayList();

		// Token: 0x04000409 RID: 1033
		private MasterMount.MountComparer mountComparer = new MasterMount.MountComparer();

		// Token: 0x0200009D RID: 157
		internal enum FsTypes
		{
			// Token: 0x0400040B RID: 1035
			none,
			// Token: 0x0400040C RID: 1036
			ext2,
			// Token: 0x0400040D RID: 1037
			ext3,
			// Token: 0x0400040E RID: 1038
			hpfs,
			// Token: 0x0400040F RID: 1039
			iso9660,
			// Token: 0x04000410 RID: 1040
			jfs,
			// Token: 0x04000411 RID: 1041
			minix,
			// Token: 0x04000412 RID: 1042
			msdos,
			// Token: 0x04000413 RID: 1043
			ntfs,
			// Token: 0x04000414 RID: 1044
			reiserfs,
			// Token: 0x04000415 RID: 1045
			ufs,
			// Token: 0x04000416 RID: 1046
			umsdos,
			// Token: 0x04000417 RID: 1047
			vfat,
			// Token: 0x04000418 RID: 1048
			sysv,
			// Token: 0x04000419 RID: 1049
			xfs,
			// Token: 0x0400041A RID: 1050
			ncpfs,
			// Token: 0x0400041B RID: 1051
			nfs,
			// Token: 0x0400041C RID: 1052
			smbfs,
			// Token: 0x0400041D RID: 1053
			usbfs,
			// Token: 0x0400041E RID: 1054
			cifs
		}

		// Token: 0x0200009E RID: 158
		internal struct Mount
		{
			// Token: 0x0400041F RID: 1055
			public string device_or_filesystem;

			// Token: 0x04000420 RID: 1056
			public string device_short;

			// Token: 0x04000421 RID: 1057
			public string mount_point;

			// Token: 0x04000422 RID: 1058
			public MasterMount.FsTypes fsType;
		}

		// Token: 0x0200009F RID: 159
		public class MountComparer : IComparer
		{
			// Token: 0x06000647 RID: 1607 RVA: 0x0001B2A1 File Offset: 0x000194A1
			public int Compare(object mount1, object mount2)
			{
				return string.Compare(((MasterMount.Mount)mount1).device_short, ((MasterMount.Mount)mount2).device_short);
			}
		}
	}
}
