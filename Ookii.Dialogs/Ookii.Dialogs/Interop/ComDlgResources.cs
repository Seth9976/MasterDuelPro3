using System;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000052 RID: 82
	internal static class ComDlgResources
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000A184 File Offset: 0x00008384
		public static string LoadString(ComDlgResources.ComDlgResourceId id)
		{
			return ComDlgResources._resources.LoadString((uint)id);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000A1A4 File Offset: 0x000083A4
		public static string FormatString(ComDlgResources.ComDlgResourceId id, params string[] args)
		{
			return ComDlgResources._resources.FormatString((uint)id, args);
		}

		// Token: 0x04000216 RID: 534
		private static Win32Resources _resources = new Win32Resources("comdlg32.dll");

		// Token: 0x02000053 RID: 83
		public enum ComDlgResourceId
		{
			// Token: 0x04000218 RID: 536
			OpenButton = 370,
			// Token: 0x04000219 RID: 537
			Open = 384,
			// Token: 0x0400021A RID: 538
			FileNotFound = 391,
			// Token: 0x0400021B RID: 539
			CreatePrompt = 402,
			// Token: 0x0400021C RID: 540
			ReadOnly = 427,
			// Token: 0x0400021D RID: 541
			ConfirmSaveAs = 435
		}
	}
}
