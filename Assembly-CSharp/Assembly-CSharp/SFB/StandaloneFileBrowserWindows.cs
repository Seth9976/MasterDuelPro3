using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ookii.Dialogs;

namespace SFB
{
	// Token: 0x02000073 RID: 115
	public class StandaloneFileBrowserWindows : IStandaloneFileBrowser
	{
		// Token: 0x06000220 RID: 544
		[DllImport("user32.dll")]
		private static extern IntPtr GetActiveWindow();

		// Token: 0x06000221 RID: 545 RVA: 0x00006C6C File Offset: 0x00004E6C
		public string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
		{
			VistaOpenFileDialog fd = new VistaOpenFileDialog();
			fd.Title = title;
			if (extensions != null)
			{
				fd.Filter = StandaloneFileBrowserWindows.GetFilterFromFileExtensionList(extensions);
				fd.FilterIndex = 1;
			}
			else
			{
				fd.Filter = string.Empty;
			}
			fd.Multiselect = multiselect;
			if (!string.IsNullOrEmpty(directory))
			{
				fd.FileName = StandaloneFileBrowserWindows.GetDirectoryPath(directory);
			}
			string[] array = ((fd.ShowDialog(new WindowWrapper(StandaloneFileBrowserWindows.GetActiveWindow())) == DialogResult.OK) ? fd.FileNames : new string[0]);
			fd.Dispose();
			return array;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006CEC File Offset: 0x00004EEC
		public void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb)
		{
			cb(this.OpenFilePanel(title, directory, extensions, multiselect));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006D00 File Offset: 0x00004F00
		public string[] OpenFolderPanel(string title, string directory, bool multiselect)
		{
			VistaFolderBrowserDialog fd = new VistaFolderBrowserDialog();
			fd.Description = title;
			if (!string.IsNullOrEmpty(directory))
			{
				fd.SelectedPath = StandaloneFileBrowserWindows.GetDirectoryPath(directory);
			}
			string[] array;
			if (fd.ShowDialog(new WindowWrapper(StandaloneFileBrowserWindows.GetActiveWindow())) != DialogResult.OK)
			{
				array = new string[0];
			}
			else
			{
				(array = new string[1])[0] = fd.SelectedPath;
			}
			fd.Dispose();
			return array;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006D5E File Offset: 0x00004F5E
		public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
		{
			cb(this.OpenFolderPanel(title, directory, multiselect));
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006D70 File Offset: 0x00004F70
		public string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
		{
			VistaSaveFileDialog fd = new VistaSaveFileDialog();
			fd.Title = title;
			string finalFilename = "";
			if (!string.IsNullOrEmpty(directory))
			{
				finalFilename = StandaloneFileBrowserWindows.GetDirectoryPath(directory);
			}
			if (!string.IsNullOrEmpty(defaultName))
			{
				finalFilename += defaultName;
			}
			fd.FileName = finalFilename;
			if (extensions != null)
			{
				fd.Filter = StandaloneFileBrowserWindows.GetFilterFromFileExtensionList(extensions);
				fd.FilterIndex = 1;
				fd.DefaultExt = extensions[0].Extensions[0];
				fd.AddExtension = true;
			}
			else
			{
				fd.DefaultExt = string.Empty;
				fd.Filter = string.Empty;
				fd.AddExtension = false;
			}
			string filename = ((fd.ShowDialog(new WindowWrapper(StandaloneFileBrowserWindows.GetActiveWindow())) == DialogResult.OK) ? fd.FileName : "");
			fd.Dispose();
			return filename;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006E31 File Offset: 0x00005031
		public void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb)
		{
			cb(this.SaveFilePanel(title, directory, defaultName, extensions));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006E48 File Offset: 0x00005048
		private static string GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
		{
			string filterString = "";
			foreach (ExtensionFilter filter in extensions)
			{
				filterString = filterString + filter.Name + "(";
				foreach (string ext in filter.Extensions)
				{
					filterString = filterString + "*." + ext + ",";
				}
				filterString = filterString.Remove(filterString.Length - 1);
				filterString += ") |";
				foreach (string ext2 in filter.Extensions)
				{
					filterString = filterString + "*." + ext2 + "; ";
				}
				filterString += "|";
			}
			return filterString.Remove(filterString.Length - 1);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006F2C File Offset: 0x0000512C
		private static string GetDirectoryPath(string directory)
		{
			string directoryPath = Path.GetFullPath(directory);
			if (!directoryPath.EndsWith("\\"))
			{
				directoryPath += "\\";
			}
			if (Path.GetPathRoot(directoryPath) == directoryPath)
			{
				return directory;
			}
			return Path.GetDirectoryName(directoryPath) + Path.DirectorySeparatorChar.ToString();
		}
	}
}
