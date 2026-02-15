using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000195 RID: 405
	internal struct JoystickState : IInputStateTypeInfo
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0004DDA1 File Offset: 0x0004BFA1
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('J', 'O', 'Y', ' ');
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0004DDB0 File Offset: 0x0004BFB0
		public FourCC format
		{
			get
			{
				return JoystickState.kFormat;
			}
		}

		// Token: 0x04000989 RID: 2441
		[InputControl(name = "trigger", displayName = "Trigger", layout = "Button", usages = new string[] { "PrimaryTrigger", "PrimaryAction", "Submit" }, bit = 4U)]
		public int buttons;

		// Token: 0x0400098A RID: 2442
		[InputControl(displayName = "Stick", layout = "Stick", usage = "Primary2DMotion", processors = "stickDeadzone")]
		public Vector2 stick;

		// Token: 0x02000196 RID: 406
		public enum Button
		{
			// Token: 0x0400098C RID: 2444
			HatSwitchUp,
			// Token: 0x0400098D RID: 2445
			HatSwitchDown,
			// Token: 0x0400098E RID: 2446
			HatSwitchLeft,
			// Token: 0x0400098F RID: 2447
			HatSwitchRight,
			// Token: 0x04000990 RID: 2448
			Trigger
		}
	}
}
