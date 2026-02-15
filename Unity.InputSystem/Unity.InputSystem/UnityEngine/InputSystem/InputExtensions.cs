using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000BE RID: 190
	public static class InputExtensions
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x00034913 File Offset: 0x00032B13
		public static bool IsInProgress(this InputActionPhase phase)
		{
			return phase == InputActionPhase.Started || phase == InputActionPhase.Performed;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0003491F File Offset: 0x00032B1F
		public static bool IsEndedOrCanceled(this TouchPhase phase)
		{
			return phase == TouchPhase.Canceled || phase == TouchPhase.Ended;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0003492B File Offset: 0x00032B2B
		public static bool IsActive(this TouchPhase phase)
		{
			return phase - TouchPhase.Began <= 1 || phase == TouchPhase.Stationary;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0003493A File Offset: 0x00032B3A
		public static bool IsModifierKey(this Key key)
		{
			return key - Key.LeftShift <= 7;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00034946 File Offset: 0x00032B46
		public static bool IsTextInputKey(this Key key)
		{
			return key > Key.Tab && key - Key.LeftShift > 26 && key - Key.F1 > 17;
		}
	}
}
