using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000092 RID: 146
	internal class MWFVFS
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x00018D6F File Offset: 0x00016F6F
		public MWFVFS()
		{
			if (XplatUI.RunningOnUnix)
			{
				this.fileSystem = new UnixFileSystem();
				return;
			}
			this.fileSystem = new WinFileSystem();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00018D95 File Offset: 0x00016F95
		public FSEntry ChangeDirectory(string folder)
		{
			return this.fileSystem.ChangeDirectory(folder);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00018DA3 File Offset: 0x00016FA3
		public void GetFolderContent()
		{
			this.GetFolderContent(null);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00018DAC File Offset: 0x00016FAC
		public void GetFolderContent(StringCollection filters)
		{
			this.the_filters = filters;
			if (this.workerThread != null)
			{
				this.workerThread.Stop();
				this.workerThread = null;
			}
			this.calling_control.CreateControl();
			this.workerThread = new MWFVFS.WorkerThread(this.fileSystem, this.the_filters, this.updateDelegate, this.calling_control);
			this.get_folder_content_thread_start = new ThreadStart(this.workerThread.GetFolderContentThread);
			this.worker = new Thread(this.get_folder_content_thread_start);
			this.worker.IsBackground = true;
			this.worker.Start();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00018E47 File Offset: 0x00017047
		public ArrayList GetFoldersOnly()
		{
			return this.fileSystem.GetFoldersOnly();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00018E54 File Offset: 0x00017054
		public void WriteRecentlyUsedFiles(string filename)
		{
			this.fileSystem.WriteRecentlyUsedFiles(filename);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00018E62 File Offset: 0x00017062
		public ArrayList GetMyComputerContent()
		{
			return this.fileSystem.GetMyComputerContent();
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00018E70 File Offset: 0x00017070
		public bool CreateFolder(string new_folder)
		{
			try
			{
				if (Directory.Exists(new_folder))
				{
					MessageBox.Show(Locale.GetText("Folder \"{0}\" already exists.", new object[] { new_folder }), Locale.GetText("Error Creating Folder"), MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				Directory.CreateDirectory(new_folder);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Locale.GetText("Error Creating Folder"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00018EE8 File Offset: 0x000170E8
		public bool MoveFolder(string sourceDirName, string destDirName)
		{
			try
			{
				if (Directory.Exists(destDirName))
				{
					MessageBox.Show(Locale.GetText("Cannot rename \"{0}\": Folder \"{1}\" already exists. Specify a different folder name.", new object[]
					{
						Path.GetFileName(sourceDirName),
						Path.GetFileName(destDirName)
					}), Locale.GetText("Error Renaming Folder"), MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				Directory.Move(sourceDirName, destDirName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Locale.GetText("Error Renaming Folder"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00018F70 File Offset: 0x00017170
		public bool MoveFile(string sourceFileName, string destFileName)
		{
			try
			{
				if (File.Exists(destFileName))
				{
					MessageBox.Show(Locale.GetText("Cannot rename \"{0}\": File \"{1}\" already exists. Specify a different file name.", new object[]
					{
						Path.GetFileName(sourceFileName),
						Path.GetFileName(destFileName)
					}), Locale.GetText("Error Renaming File"), MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				File.Move(sourceFileName, destFileName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Locale.GetText("Error Renaming File"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00018FF8 File Offset: 0x000171F8
		public string GetParent()
		{
			return this.fileSystem.GetParent();
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00019005 File Offset: 0x00017205
		public void RegisterUpdateDelegate(MWFVFS.UpdateDelegate updateDelegate, Control control)
		{
			this.updateDelegate = updateDelegate;
			this.calling_control = control;
		}

		// Token: 0x040003C5 RID: 965
		private FileSystem fileSystem;

		// Token: 0x040003C6 RID: 966
		public static readonly string DesktopPrefix = "Desktop://";

		// Token: 0x040003C7 RID: 967
		public static readonly string PersonalPrefix = "Personal://";

		// Token: 0x040003C8 RID: 968
		public static readonly string MyComputerPrefix = "MyComputer://";

		// Token: 0x040003C9 RID: 969
		public static readonly string RecentlyUsedPrefix = "RecentlyUsed://";

		// Token: 0x040003CA RID: 970
		public static readonly string MyNetworkPrefix = "MyNetwork://";

		// Token: 0x040003CB RID: 971
		public static readonly string MyComputerPersonalPrefix = "MyComputerPersonal://";

		// Token: 0x040003CC RID: 972
		public static Hashtable MyComputerDevicesPrefix = new Hashtable();

		// Token: 0x040003CD RID: 973
		private MWFVFS.UpdateDelegate updateDelegate;

		// Token: 0x040003CE RID: 974
		private Control calling_control;

		// Token: 0x040003CF RID: 975
		private ThreadStart get_folder_content_thread_start;

		// Token: 0x040003D0 RID: 976
		private Thread worker;

		// Token: 0x040003D1 RID: 977
		private MWFVFS.WorkerThread workerThread;

		// Token: 0x040003D2 RID: 978
		private StringCollection the_filters;

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x060005F2 RID: 1522
		public delegate void UpdateDelegate(ArrayList folders, ArrayList files);

		// Token: 0x02000094 RID: 148
		internal class WorkerThread
		{
			// Token: 0x060005F3 RID: 1523 RVA: 0x0001906B File Offset: 0x0001726B
			public WorkerThread(FileSystem fileSystem, StringCollection the_filters, MWFVFS.UpdateDelegate updateDelegate, Control calling_control)
			{
				this.fileSystem = fileSystem;
				this.the_filters = the_filters;
				this.updateDelegate = updateDelegate;
				this.calling_control = calling_control;
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x0001909C File Offset: 0x0001729C
			public void GetFolderContentThread()
			{
				ArrayList arrayList;
				ArrayList arrayList2;
				this.fileSystem.GetFolderContent(this.the_filters, out arrayList, out arrayList2);
				if (this.stopped)
				{
					return;
				}
				if (this.updateDelegate != null)
				{
					lock (this)
					{
						object[] array = new object[] { arrayList, arrayList2 };
						this.calling_control.BeginInvoke(this.updateDelegate, array);
					}
				}
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x00019120 File Offset: 0x00017320
			public void Stop()
			{
				object obj = this.lockobject;
				lock (obj)
				{
					this.stopped = true;
				}
			}

			// Token: 0x040003D3 RID: 979
			private FileSystem fileSystem;

			// Token: 0x040003D4 RID: 980
			private StringCollection the_filters;

			// Token: 0x040003D5 RID: 981
			private MWFVFS.UpdateDelegate updateDelegate;

			// Token: 0x040003D6 RID: 982
			private Control calling_control;

			// Token: 0x040003D7 RID: 983
			private readonly object lockobject = new object();

			// Token: 0x040003D8 RID: 984
			private bool stopped;
		}
	}
}
