using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Haptics
{
	// Token: 0x0200016E RID: 366
	internal struct DualMotorRumble
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0004D46E File Offset: 0x0004B66E
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x0004D476 File Offset: 0x0004B676
		public float lowFrequencyMotorSpeed { readonly get; private set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0004D47F File Offset: 0x0004B67F
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x0004D487 File Offset: 0x0004B687
		public float highFrequencyMotorSpeed { readonly get; private set; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0004D490 File Offset: 0x0004B690
		public bool isRumbling
		{
			get
			{
				return !Mathf.Approximately(this.lowFrequencyMotorSpeed, 0f) || !Mathf.Approximately(this.highFrequencyMotorSpeed, 0f);
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0004D4BC File Offset: 0x0004B6BC
		public void PauseHaptics(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!this.isRumbling)
			{
				return;
			}
			DualMotorRumbleCommand command = DualMotorRumbleCommand.Create(0f, 0f);
			device.ExecuteCommand<DualMotorRumbleCommand>(ref command);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0004D4F9 File Offset: 0x0004B6F9
		public void ResumeHaptics(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!this.isRumbling)
			{
				return;
			}
			this.SetMotorSpeeds(device, this.lowFrequencyMotorSpeed, this.highFrequencyMotorSpeed);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0004D525 File Offset: 0x0004B725
		public void ResetHaptics(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!this.isRumbling)
			{
				return;
			}
			this.SetMotorSpeeds(device, 0f, 0f);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0004D550 File Offset: 0x0004B750
		public void SetMotorSpeeds(InputDevice device, float lowFrequency, float highFrequency)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.lowFrequencyMotorSpeed = Mathf.Clamp(lowFrequency, 0f, 1f);
			this.highFrequencyMotorSpeed = Mathf.Clamp(highFrequency, 0f, 1f);
			DualMotorRumbleCommand command = DualMotorRumbleCommand.Create(this.lowFrequencyMotorSpeed, this.highFrequencyMotorSpeed);
			device.ExecuteCommand<DualMotorRumbleCommand>(ref command);
		}
	}
}
