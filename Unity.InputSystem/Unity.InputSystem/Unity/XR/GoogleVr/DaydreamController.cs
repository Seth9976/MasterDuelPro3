using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace Unity.XR.GoogleVr
{
	// Token: 0x02000013 RID: 19
	[InputControlLayout(displayName = "Daydream Controller", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class DaydreamController : XRController
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002E40 File Offset: 0x00001040
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00002E48 File Offset: 0x00001048
		[InputControl]
		public Vector2Control touchpad { get; protected set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002E51 File Offset: 0x00001051
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00002E59 File Offset: 0x00001059
		[InputControl]
		public ButtonControl volumeUp { get; protected set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002E62 File Offset: 0x00001062
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00002E6A File Offset: 0x0000106A
		[InputControl]
		public ButtonControl recentered { get; protected set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002E73 File Offset: 0x00001073
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00002E7B File Offset: 0x0000107B
		[InputControl]
		public ButtonControl volumeDown { get; protected set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002E84 File Offset: 0x00001084
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002E8C File Offset: 0x0000108C
		[InputControl]
		public ButtonControl recentering { get; protected set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002E95 File Offset: 0x00001095
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002E9D File Offset: 0x0000109D
		[InputControl]
		public ButtonControl app { get; protected set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002EA6 File Offset: 0x000010A6
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002EAE File Offset: 0x000010AE
		[InputControl]
		public ButtonControl home { get; protected set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002EB7 File Offset: 0x000010B7
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002EBF File Offset: 0x000010BF
		[InputControl]
		public ButtonControl touchpadClicked { get; protected set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002EC8 File Offset: 0x000010C8
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002ED0 File Offset: 0x000010D0
		[InputControl]
		public ButtonControl touchpadTouched { get; protected set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002ED9 File Offset: 0x000010D9
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00002EE1 File Offset: 0x000010E1
		[InputControl(noisy = true)]
		public Vector3Control deviceVelocity { get; protected set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002EEA File Offset: 0x000010EA
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00002EF2 File Offset: 0x000010F2
		[InputControl(noisy = true)]
		public Vector3Control deviceAcceleration { get; protected set; }

		// Token: 0x060000EE RID: 238 RVA: 0x00002EFC File Offset: 0x000010FC
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.touchpad = base.GetChildControl<Vector2Control>("touchpad");
			this.volumeUp = base.GetChildControl<ButtonControl>("volumeUp");
			this.recentered = base.GetChildControl<ButtonControl>("recentered");
			this.volumeDown = base.GetChildControl<ButtonControl>("volumeDown");
			this.recentering = base.GetChildControl<ButtonControl>("recentering");
			this.app = base.GetChildControl<ButtonControl>("app");
			this.home = base.GetChildControl<ButtonControl>("home");
			this.touchpadClicked = base.GetChildControl<ButtonControl>("touchpadClicked");
			this.touchpadTouched = base.GetChildControl<ButtonControl>("touchpadTouched");
			this.deviceVelocity = base.GetChildControl<Vector3Control>("deviceVelocity");
			this.deviceAcceleration = base.GetChildControl<Vector3Control>("deviceAcceleration");
		}
	}
}
