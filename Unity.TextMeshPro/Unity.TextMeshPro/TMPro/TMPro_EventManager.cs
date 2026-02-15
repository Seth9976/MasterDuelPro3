using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x020000A6 RID: 166
	public static class TMPro_EventManager
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x0002E38B File Offset: 0x0002C58B
		public static void ON_MATERIAL_PROPERTY_CHANGED(bool isChanged, Material mat)
		{
			TMPro_EventManager.MATERIAL_PROPERTY_EVENT.Call(isChanged, mat);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002E399 File Offset: 0x0002C599
		public static void ON_FONT_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TMPro_EventManager.FONT_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002E3A7 File Offset: 0x0002C5A7
		public static void ON_SPRITE_ASSET_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TMPro_EventManager.SPRITE_ASSET_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0002E3B5 File Offset: 0x0002C5B5
		public static void ON_TEXTMESHPRO_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TMPro_EventManager.TEXTMESHPRO_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0002E3C3 File Offset: 0x0002C5C3
		public static void ON_DRAG_AND_DROP_MATERIAL_CHANGED(GameObject sender, Material currentMaterial, Material newMaterial)
		{
			TMPro_EventManager.DRAG_AND_DROP_MATERIAL_EVENT.Call(sender, currentMaterial, newMaterial);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0002E3D2 File Offset: 0x0002C5D2
		public static void ON_TEXT_STYLE_PROPERTY_CHANGED(bool isChanged)
		{
			TMPro_EventManager.TEXT_STYLE_PROPERTY_EVENT.Call(isChanged);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002E3DF File Offset: 0x0002C5DF
		public static void ON_COLOR_GRADIENT_PROPERTY_CHANGED(global::UnityEngine.Object obj)
		{
			TMPro_EventManager.COLOR_GRADIENT_PROPERTY_EVENT.Call(obj);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002E3EC File Offset: 0x0002C5EC
		public static void ON_TEXT_CHANGED(global::UnityEngine.Object obj)
		{
			TMPro_EventManager.TEXT_CHANGED_EVENT.Call(obj);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0002E3F9 File Offset: 0x0002C5F9
		public static void ON_TMP_SETTINGS_CHANGED()
		{
			TMPro_EventManager.TMP_SETTINGS_PROPERTY_EVENT.Call();
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0002E405 File Offset: 0x0002C605
		public static void ON_RESOURCES_LOADED()
		{
			TMPro_EventManager.RESOURCE_LOAD_EVENT.Call();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0002E411 File Offset: 0x0002C611
		public static void ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(bool isChanged, global::UnityEngine.Object obj)
		{
			TMPro_EventManager.TEXTMESHPRO_UGUI_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0002E41F File Offset: 0x0002C61F
		public static void ON_COMPUTE_DT_EVENT(object Sender, Compute_DT_EventArgs e)
		{
			TMPro_EventManager.COMPUTE_DT_EVENT.Call(Sender, e);
		}

		// Token: 0x0400059F RID: 1439
		public static readonly FastAction<object, Compute_DT_EventArgs> COMPUTE_DT_EVENT = new FastAction<object, Compute_DT_EventArgs>();

		// Token: 0x040005A0 RID: 1440
		public static readonly FastAction<bool, Material> MATERIAL_PROPERTY_EVENT = new FastAction<bool, Material>();

		// Token: 0x040005A1 RID: 1441
		public static readonly FastAction<bool, global::UnityEngine.Object> FONT_PROPERTY_EVENT = new FastAction<bool, global::UnityEngine.Object>();

		// Token: 0x040005A2 RID: 1442
		public static readonly FastAction<bool, global::UnityEngine.Object> SPRITE_ASSET_PROPERTY_EVENT = new FastAction<bool, global::UnityEngine.Object>();

		// Token: 0x040005A3 RID: 1443
		public static readonly FastAction<bool, global::UnityEngine.Object> TEXTMESHPRO_PROPERTY_EVENT = new FastAction<bool, global::UnityEngine.Object>();

		// Token: 0x040005A4 RID: 1444
		public static readonly FastAction<GameObject, Material, Material> DRAG_AND_DROP_MATERIAL_EVENT = new FastAction<GameObject, Material, Material>();

		// Token: 0x040005A5 RID: 1445
		public static readonly FastAction<bool> TEXT_STYLE_PROPERTY_EVENT = new FastAction<bool>();

		// Token: 0x040005A6 RID: 1446
		public static readonly FastAction<global::UnityEngine.Object> COLOR_GRADIENT_PROPERTY_EVENT = new FastAction<global::UnityEngine.Object>();

		// Token: 0x040005A7 RID: 1447
		public static readonly FastAction TMP_SETTINGS_PROPERTY_EVENT = new FastAction();

		// Token: 0x040005A8 RID: 1448
		public static readonly FastAction RESOURCE_LOAD_EVENT = new FastAction();

		// Token: 0x040005A9 RID: 1449
		public static readonly FastAction<bool, global::UnityEngine.Object> TEXTMESHPRO_UGUI_PROPERTY_EVENT = new FastAction<bool, global::UnityEngine.Object>();

		// Token: 0x040005AA RID: 1450
		public static readonly FastAction<global::UnityEngine.Object> TEXT_CHANGED_EVENT = new FastAction<global::UnityEngine.Object>();
	}
}
