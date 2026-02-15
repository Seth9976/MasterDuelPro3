using System;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	// Token: 0x0200004D RID: 77
	internal class VistaFileDialogEvents : IFileDialogEvents, IFileDialogControlEvents
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00009228 File Offset: 0x00007428
		public VistaFileDialogEvents(VistaFileDialog dialog)
		{
			bool flag = dialog == null;
			if (flag)
			{
				throw new ArgumentNullException("dialog");
			}
			this._dialog = dialog;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009258 File Offset: 0x00007458
		public HRESULT OnFileOk(IFileDialog pfd)
		{
			bool flag = this._dialog.DoFileOk(pfd);
			HRESULT hresult;
			if (flag)
			{
				hresult = HRESULT.S_OK;
			}
			else
			{
				hresult = HRESULT.S_FALSE;
			}
			return hresult;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009284 File Offset: 0x00007484
		public HRESULT OnFolderChanging(IFileDialog pfd, IShellItem psiFolder)
		{
			GC.SuppressFinalize(psiFolder);
			return HRESULT.S_OK;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000929F File Offset: 0x0000749F
		public void OnFolderChange(IFileDialog pfd)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000929F File Offset: 0x0000749F
		public void OnSelectionChange(IFileDialog pfd)
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000929F File Offset: 0x0000749F
		public void OnShareViolation(IFileDialog pfd, IShellItem psi)
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000929F File Offset: 0x0000749F
		public void OnTypeChange(IFileDialog pfd)
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000929F File Offset: 0x0000749F
		public void OnOverwrite(IFileDialog pfd, IShellItem psi)
		{
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000929F File Offset: 0x0000749F
		public void OnItemSelected(IFileDialogCustomize pfdc, int dwIDCtl, int dwIDItem)
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000092A4 File Offset: 0x000074A4
		public void OnButtonClicked(IFileDialogCustomize pfdc, int dwIDCtl)
		{
			bool flag = dwIDCtl == 16385;
			if (flag)
			{
				this._dialog.DoHelpRequest();
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000929F File Offset: 0x0000749F
		public void OnCheckButtonToggled(IFileDialogCustomize pfdc, int dwIDCtl, bool bChecked)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000929F File Offset: 0x0000749F
		public void OnControlActivating(IFileDialogCustomize pfdc, int dwIDCtl)
		{
		}

		// Token: 0x04000205 RID: 517
		private const uint S_OK = 0U;

		// Token: 0x04000206 RID: 518
		private const uint S_FALSE = 1U;

		// Token: 0x04000207 RID: 519
		private const uint E_NOTIMPL = 2147500033U;

		// Token: 0x04000208 RID: 520
		private VistaFileDialog _dialog;
	}
}
