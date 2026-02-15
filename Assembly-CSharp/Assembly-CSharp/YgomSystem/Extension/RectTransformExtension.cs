using System;
using UnityEngine;

namespace YgomSystem.Extension
{
	// Token: 0x02000778 RID: 1912
	public static class RectTransformExtension
	{
		// Token: 0x06003B64 RID: 15204 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ToStretch(this RectTransform rectTransform)
		{
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ToNeutral(this RectTransform rectTransform)
		{
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ToRight(this RectTransform rectTransform)
		{
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ToRightStretch(this RectTransform rectTransform)
		{
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CaptureTo(this RectTransform rectTransform, RectTransform target)
		{
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x0000216A File Offset: 0x0000036A
		public static Camera GetCamera(this RectTransform rectTransform)
		{
			return null;
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x0000216A File Offset: 0x0000036A
		public static RectTransform GetParentRect(this RectTransform rectTransform)
		{
			return null;
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000F39B0 File Offset: 0x000F1BB0
		public static Vector3 GetScreenPosition(this RectTransform rectTransform)
		{
			return default(Vector3);
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000F39C8 File Offset: 0x000F1BC8
		public static Vector3 ScreenToWorldPoint(this RectTransform rectTransform, Vector3 screenPos)
		{
			return default(Vector3);
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsContainRect(this RectTransform rectTransform, RectTransform checkRect)
		{
			return false;
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsContainRect(this RectTransform rectTransform, Canvas canvas, RectTransform checkRect)
		{
			return false;
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInnerRect(this RectTransform rectTransform, RectTransform checkRect)
		{
			return false;
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInnerRect(this RectTransform rectTransform, Canvas canvas, RectTransform checkRect)
		{
			return false;
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000F39E0 File Offset: 0x000F1BE0
		public static Bounds CalculateRelativeRectTransformBounds(this RectTransform root, RectTransform child, bool nest = false)
		{
			return default(Bounds);
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000F39F8 File Offset: 0x000F1BF8
		public static Vector2 GetSizeDeltaWithPlatformOverrider(this RectTransform rectTransform)
		{
			return default(Vector2);
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetSizeDeltaWithPlatformOverrider(this RectTransform rectTransform, Vector2 sizeDelta)
		{
		}

		// Token: 0x04003483 RID: 13443
		private static readonly Vector3[] s_Corners;
	}
}
