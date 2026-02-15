using System;

namespace SFB
{
	// Token: 0x02000071 RID: 113
	public class StandaloneFileBrowser
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00006ADC File Offset: 0x00004CDC
		public static string[] OpenFilePanel(string title, string directory, string extension, bool multiselect)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] extensions = array;
			return StandaloneFileBrowser.OpenFilePanel(title, directory, extensions, multiselect);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006B1F File Offset: 0x00004D1F
		public static string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
		{
			return StandaloneFileBrowser._platformWrapper.OpenFilePanel(title, directory, extensions, multiselect);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006B30 File Offset: 0x00004D30
		public static void OpenFilePanelAsync(string title, string directory, string extension, bool multiselect, Action<string[]> cb)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] extensions = array;
			StandaloneFileBrowser.OpenFilePanelAsync(title, directory, extensions, multiselect, cb);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006B75 File Offset: 0x00004D75
		public static void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb)
		{
			StandaloneFileBrowser._platformWrapper.OpenFilePanelAsync(title, directory, extensions, multiselect, cb);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006B87 File Offset: 0x00004D87
		public static string[] OpenFolderPanel(string title, string directory, bool multiselect)
		{
			return StandaloneFileBrowser._platformWrapper.OpenFolderPanel(title, directory, multiselect);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006B96 File Offset: 0x00004D96
		public static void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
		{
			StandaloneFileBrowser._platformWrapper.OpenFolderPanelAsync(title, directory, multiselect, cb);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006BA8 File Offset: 0x00004DA8
		public static string SaveFilePanel(string title, string directory, string defaultName, string extension)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] extensions = array;
			return StandaloneFileBrowser.SaveFilePanel(title, directory, defaultName, extensions);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00006BEB File Offset: 0x00004DEB
		public static string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
		{
			return StandaloneFileBrowser._platformWrapper.SaveFilePanel(title, directory, defaultName, extensions);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006BFC File Offset: 0x00004DFC
		public static void SaveFilePanelAsync(string title, string directory, string defaultName, string extension, Action<string> cb)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] extensions = array;
			StandaloneFileBrowser.SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006C41 File Offset: 0x00004E41
		public static void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb)
		{
			StandaloneFileBrowser._platformWrapper.SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
		}

		// Token: 0x040002BE RID: 702
		private static IStandaloneFileBrowser _platformWrapper = new StandaloneFileBrowserWindows();
	}
}
