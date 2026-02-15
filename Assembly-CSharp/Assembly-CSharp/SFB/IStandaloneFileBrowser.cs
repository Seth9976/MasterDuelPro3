using System;

namespace SFB
{
	// Token: 0x0200006F RID: 111
	public interface IStandaloneFileBrowser
	{
		// Token: 0x0600020B RID: 523
		string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect);

		// Token: 0x0600020C RID: 524
		string[] OpenFolderPanel(string title, string directory, bool multiselect);

		// Token: 0x0600020D RID: 525
		string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions);

		// Token: 0x0600020E RID: 526
		void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb);

		// Token: 0x0600020F RID: 527
		void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb);

		// Token: 0x06000210 RID: 528
		void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb);
	}
}
