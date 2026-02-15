using System;

namespace System.IO
{
	// Token: 0x02000791 RID: 1937
	internal struct DisableMediaInsertionPrompt : IDisposable
	{
		// Token: 0x06003D26 RID: 15654 RVA: 0x000EB808 File Offset: 0x000E9A08
		public static DisableMediaInsertionPrompt Create()
		{
			DisableMediaInsertionPrompt disableMediaInsertionPrompt = default(DisableMediaInsertionPrompt);
			disableMediaInsertionPrompt._disableSuccess = Interop.Kernel32.SetThreadErrorMode(1U, out disableMediaInsertionPrompt._oldMode);
			return disableMediaInsertionPrompt;
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000EB834 File Offset: 0x000E9A34
		public void Dispose()
		{
			if (this._disableSuccess)
			{
				uint num;
				Interop.Kernel32.SetThreadErrorMode(this._oldMode, out num);
			}
		}

		// Token: 0x04001F75 RID: 8053
		private bool _disableSuccess;

		// Token: 0x04001F76 RID: 8054
		private uint _oldMode;
	}
}
