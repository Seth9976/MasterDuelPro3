using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomSystem.Extension.UGUI.ScrollRectExtension
{
	// Token: 0x0200077C RID: 1916
	public static class ScrollRectExtension
	{
		// Token: 0x06003B7C RID: 15228 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetGamePadInputScroll(this ScrollRect scrollRect, ScrollRectExtension.InputMode inputMode = (ScrollRectExtension.InputMode)7)
		{
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ScrollMovement(this ScrollRect scrollRect, PadInputDirection direction)
		{
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ScrollMovementRight(this ScrollRect scrollRect)
		{
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ScrollMovementLeft(this ScrollRect scrollRect)
		{
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ScrollMovementUp(this ScrollRect scrollRect)
		{
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ScrollMovementDown(this ScrollRect scrollRect)
		{
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ScrollMovement(this ScrollRect scrollRect, Vector2 dir)
		{
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x000F3A28 File Offset: 0x000F1C28
		public static Vector2 GetMovementAmount(this ScrollRect scrollRect, PadInputDirection direction)
		{
			return default(Vector2);
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x000F3A40 File Offset: 0x000F1C40
		public static Vector2 GetMovementAmount(this ScrollRect scrollRect, Vector2 dir)
		{
			return default(Vector2);
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetOnSelectedItemInnerFocus(this ExtendedScrollRect scrollRect, SelectionItem selectionItem)
		{
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OnSelectedItemInnerFocus(this ExtendedScrollRect scrollRect, SelectionItem selectionItem)
		{
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetOnSelectedItemEdgeFocus(this ExtendedScrollRect scrollRect, SelectionItem selectionItem, PadInputDirection direction)
		{
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OnSelectedItemEdgeFocus(this ExtendedScrollRect scrollRect, PadInputDirection direction)
		{
		}

		// Token: 0x0200077D RID: 1917
		public enum InputMode
		{
			// Token: 0x04003485 RID: 13445
			DirectionalKey = 1,
			// Token: 0x04003486 RID: 13446
			AnalogMain,
			// Token: 0x04003487 RID: 13447
			AnalogSub = 4
		}
	}
}
