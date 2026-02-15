using System;
using System.Globalization;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200021F RID: 543
	public class KeyControl : ButtonControl
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0005BD9C File Offset: 0x00059F9C
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x0005BDA4 File Offset: 0x00059FA4
		public Key keyCode { get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0005BDAD File Offset: 0x00059FAD
		public int scanCode
		{
			get
			{
				base.RefreshConfigurationIfNeeded();
				return this.m_ScanCode;
			}
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0005BDBC File Offset: 0x00059FBC
		protected override void RefreshConfiguration()
		{
			base.displayName = null;
			this.m_ScanCode = 0;
			QueryKeyNameCommand command = QueryKeyNameCommand.Create(this.keyCode);
			if (base.device.ExecuteCommand<QueryKeyNameCommand>(ref command) > 0L)
			{
				this.m_ScanCode = command.scanOrKeyCode;
				string rawKeyName = command.ReadKeyName();
				if (string.IsNullOrEmpty(rawKeyName))
				{
					base.displayName = rawKeyName;
					return;
				}
				TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
				string keyNameLowerCase = textInfo.ToLower(rawKeyName);
				if (string.IsNullOrEmpty(keyNameLowerCase))
				{
					base.displayName = rawKeyName;
					return;
				}
				base.displayName = textInfo.ToTitleCase(keyNameLowerCase);
			}
		}

		// Token: 0x04000BF6 RID: 3062
		private int m_ScanCode;
	}
}
