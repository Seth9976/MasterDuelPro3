using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000E2 RID: 226
	[InputControlLayout(isGenericTypeOfDevice = true, displayName = "XR HMD", canRunInBackground = true)]
	public class XRHMD : TrackedDevice
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0003DDEF File Offset: 0x0003BFEF
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x0003DDF7 File Offset: 0x0003BFF7
		[InputControl(noisy = true)]
		public Vector3Control leftEyePosition { get; protected set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0003DE00 File Offset: 0x0003C000
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0003DE08 File Offset: 0x0003C008
		[InputControl(noisy = true)]
		public QuaternionControl leftEyeRotation { get; protected set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0003DE11 File Offset: 0x0003C011
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0003DE19 File Offset: 0x0003C019
		[InputControl(noisy = true)]
		public Vector3Control rightEyePosition { get; protected set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0003DE22 File Offset: 0x0003C022
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x0003DE2A File Offset: 0x0003C02A
		[InputControl(noisy = true)]
		public QuaternionControl rightEyeRotation { get; protected set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0003DE33 File Offset: 0x0003C033
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x0003DE3B File Offset: 0x0003C03B
		[InputControl(noisy = true)]
		public Vector3Control centerEyePosition { get; protected set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0003DE44 File Offset: 0x0003C044
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x0003DE4C File Offset: 0x0003C04C
		[InputControl(noisy = true)]
		public QuaternionControl centerEyeRotation { get; protected set; }

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0003DE58 File Offset: 0x0003C058
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.centerEyePosition = base.GetChildControl<Vector3Control>("centerEyePosition");
			this.centerEyeRotation = base.GetChildControl<QuaternionControl>("centerEyeRotation");
			this.leftEyePosition = base.GetChildControl<Vector3Control>("leftEyePosition");
			this.leftEyeRotation = base.GetChildControl<QuaternionControl>("leftEyeRotation");
			this.rightEyePosition = base.GetChildControl<Vector3Control>("rightEyePosition");
			this.rightEyeRotation = base.GetChildControl<QuaternionControl>("rightEyeRotation");
		}
	}
}
