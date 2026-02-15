using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace UnityEngine.XR.WindowsMR.Input
{
	// Token: 0x02000014 RID: 20
	[InputControlLayout(displayName = "Windows MR Headset", hideInUI = true)]
	public class WMRHMD : XRHMD
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002FCA File Offset: 0x000011CA
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00002FD2 File Offset: 0x000011D2
		[InputControl]
		[InputControl(name = "devicePosition", layout = "Vector3", aliases = new string[] { "HeadPosition" })]
		[InputControl(name = "deviceRotation", layout = "Quaternion", aliases = new string[] { "HeadRotation" })]
		public ButtonControl userPresence { get; protected set; }

		// Token: 0x060000F2 RID: 242 RVA: 0x00002FDB File Offset: 0x000011DB
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.userPresence = base.GetChildControl<ButtonControl>("userPresence");
		}
	}
}
