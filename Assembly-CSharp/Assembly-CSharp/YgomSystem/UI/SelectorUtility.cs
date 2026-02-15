using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005F7 RID: 1527
	public static class SelectorUtility
	{
		// Token: 0x060030E5 RID: 12517 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsRectContainsPoint(Vector2 rect_point0, Vector2 rect_point1, Vector2 rect_point2, Vector2 rect_point3, Vector2 check_point)
		{
			return false;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetSign(float v)
		{
			return 0;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SelectorManager.KeyType GamePadKeyTypeToKeyType(int gamepad_key_type)
		{
			return SelectorManager.KeyType.None;
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000F2288 File Offset: 0x000F0488
		public static ValueTuple<SelectionItem, float> GetClosestItem(Vector2 screenPoint, Vector2 direction, List<SelectionItem> targets, float angleDot, float minSqrDistance = -1f)
		{
			return default(ValueTuple<SelectionItem, float>);
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool InvokeAction(Action action)
		{
			return false;
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool InvokeAction<T1>(Action<T1> action, T1 arg1)
		{
			return false;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool InvokeAction<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			return false;
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool InvokeAction<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			return false;
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000F22A0 File Offset: 0x000F04A0
		public static Vector2 DirectionToVector2(PadInputDirection direction)
		{
			return default(Vector2);
		}

		// Token: 0x04002D5B RID: 11611
		private static Vector2 directionUp;

		// Token: 0x04002D5C RID: 11612
		private static Vector2 directionRight;

		// Token: 0x04002D5D RID: 11613
		private static Vector2 directionDown;

		// Token: 0x04002D5E RID: 11614
		private static Vector2 directionLeft;
	}
}
