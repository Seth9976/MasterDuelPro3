using System;
using UnityEngine;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F88 RID: 3976
	public interface IContentWidgetDirectionalInputListener
	{
		// Token: 0x0600749E RID: 29854
		void OnMainAnalogInput(Vector2 dir);

		// Token: 0x0600749F RID: 29855
		void OnSubAnalogInput(Vector2 dir);

		// Token: 0x060074A0 RID: 29856
		void OnLeftInput();

		// Token: 0x060074A1 RID: 29857
		void OnRightInput();

		// Token: 0x060074A2 RID: 29858
		void OnUpInput();

		// Token: 0x060074A3 RID: 29859
		void OnDownInput();
	}
}
