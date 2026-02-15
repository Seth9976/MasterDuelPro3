using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x0200051F RID: 1311
	public static class GameObjectUtil
	{
		// Token: 0x06002A10 RID: 10768 RVA: 0x0000216A File Offset: 0x0000036A
		public static GameObject FindInChildren(GameObject parent, string name, bool includeInactive = false)
		{
			return null;
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x0000216A File Offset: 0x0000036A
		public static GameObject FindWithPathInChildren(GameObject root, string path, bool includeInactive = false)
		{
			return null;
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x0000216A File Offset: 0x0000036A
		private static GameObject FindWithPathInChildren(GameObject currentGo, string[] paths, int findIdx, bool includeInactive)
		{
			return null;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000F1B20 File Offset: 0x000EFD20
		public static T FindInChildren<T>(GameObject parent, string name, bool includeInactive = false) where T : Component
		{
			return default(T);
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000F1B38 File Offset: 0x000EFD38
		public static T FindInParent<T>(GameObject go) where T : Component
		{
			return default(T);
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsParent(GameObject go, GameObject parent)
		{
			return false;
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetX(this Transform transform, float x)
		{
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetY(this Transform transform, float y)
		{
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetZ(this Transform transform, float z)
		{
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetXY(this Transform transform, Vector2 position)
		{
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetXY(this Transform transform, float x, float y)
		{
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLocalX(this Transform transform, float x)
		{
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLocalY(this Transform transform, float y)
		{
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLocalZ(this Transform transform, float z)
		{
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLocalXY(this Transform transform, Vector2 position)
		{
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetScaleX(this Transform transform, float x)
		{
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetScaleY(this Transform transform, float y)
		{
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetScaleZ(this Transform transform, float z)
		{
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLocalXY(this Transform transform, float x, float y)
		{
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAnchorX(this RectTransform rect, float x)
		{
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAnchorY(this RectTransform rect, float y)
		{
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetAnchorZ(this RectTransform rect, float z)
		{
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetActiveEx(this GameObject go, bool active)
		{
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DestroyMaterials(GameObject go, bool includeinactive = false)
		{
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetReplacedLegacyShaderName(string name)
		{
			return null;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLayer(GameObject target, int layer, bool recursive = false)
		{
		}

		// Token: 0x0400297A RID: 10618
		private static readonly string[] legacy_names;
	}
}
