using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[NativeHeader("Modules/IMGUI/GUILayoutUtility.bindings.h")]
	public class GUILayoutUtility
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000042C0 File Offset: 0x000024C0
		private static Rect Internal_GetWindowRect(int windowID)
		{
			Rect rect;
			GUILayoutUtility.Internal_GetWindowRect_Injected(windowID, out rect);
			return rect;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000042D8 File Offset: 0x000024D8
		private static void Internal_MoveWindow(int windowID, Rect r)
		{
			GUILayoutUtility.Internal_MoveWindow_Injected(windowID, ref r);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000042F0 File Offset: 0x000024F0
		internal static GUILayoutUtility.LayoutCache GetLayoutCache(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> store = (isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts);
			GUILayoutUtility.LayoutCache cache;
			store.TryGetValue(instanceID, out cache);
			return cache;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004320 File Offset: 0x00002520
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static GUILayoutUtility.LayoutCache SelectIDList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> store = (isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts);
			GUILayoutUtility.LayoutCache cache = GUILayoutUtility.GetLayoutCache(instanceID, isWindow);
			bool flag = cache == null;
			if (flag)
			{
				cache = new GUILayoutUtility.LayoutCache(instanceID);
				store[instanceID] = cache;
			}
			GUILayoutUtility.current.topLevel = cache.topLevel;
			GUILayoutUtility.current.layoutGroups = cache.layoutGroups;
			GUILayoutUtility.current.windows = cache.windows;
			return cache;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004398 File Offset: 0x00002598
		internal static void RemoveSelectedIdList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> store = (isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts);
			bool flag = store.ContainsKey(instanceID);
			if (flag)
			{
				store.Remove(instanceID);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000043CC File Offset: 0x000025CC
		internal static void Begin(int instanceID)
		{
			GUILayoutUtility.LayoutCache cache = GUILayoutUtility.SelectIDList(instanceID, false);
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel = (cache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (cache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = cache.topLevel;
				GUILayoutUtility.current.layoutGroups = cache.layoutGroups;
				GUILayoutUtility.current.windows = cache.windows;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004484 File Offset: 0x00002684
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void BeginContainer(GUILayoutUtility.LayoutCache cache)
		{
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				cache.topLevel = new GUILayoutGroup();
				cache.layoutGroups.Clear();
				cache.layoutGroups.Push(cache.topLevel);
				cache.windows = new GUILayoutGroup();
			}
			GUILayoutUtility.current.topLevel = cache.topLevel;
			GUILayoutUtility.current.layoutGroups = cache.layoutGroups;
			GUILayoutUtility.current.windows = cache.windows;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000450C File Offset: 0x0000270C
		internal static void BeginWindow(int windowID, GUIStyle style, GUILayoutOption[] options)
		{
			GUILayoutUtility.LayoutCache cache = GUILayoutUtility.SelectIDList(windowID, true);
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel = (cache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.topLevel.style = style;
				GUILayoutUtility.current.topLevel.windowID = windowID;
				bool flag2 = options != null;
				if (flag2)
				{
					GUILayoutUtility.current.topLevel.ApplyOptions(options);
				}
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (cache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = cache.topLevel;
				GUILayoutUtility.current.layoutGroups = cache.layoutGroups;
				GUILayoutUtility.current.windows = cache.windows;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004600 File Offset: 0x00002800
		internal static void Layout()
		{
			bool flag = GUILayoutUtility.current.topLevel.windowID == -1;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, Mathf.Min((float)Screen.width / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxWidth));
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000046E8 File Offset: 0x000028E8
		internal static void LayoutFromEditorWindow()
		{
			bool flag = GUILayoutUtility.current.topLevel != null;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, (float)Screen.width / GUIUtility.pixelsPerPoint);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, (float)Screen.height / GUIUtility.pixelsPerPoint);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000478C File Offset: 0x0000298C
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void LayoutFromContainer(float w, float h)
		{
			bool flag = GUILayoutUtility.current.topLevel != null;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, w);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, h);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004818 File Offset: 0x00002A18
		internal static void LayoutFreeGroup(GUILayoutGroup toplevel)
		{
			foreach (GUILayoutEntry guilayoutEntry in toplevel.entries)
			{
				GUILayoutGroup i = (GUILayoutGroup)guilayoutEntry;
				GUILayoutUtility.LayoutSingleGroup(i);
			}
			toplevel.ResetCursor();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000487C File Offset: 0x00002A7C
		private static void LayoutSingleGroup(GUILayoutGroup i)
		{
			bool flag = !i.isWindow;
			if (flag)
			{
				float origMinWidth = i.minWidth;
				float origMaxWidth = i.maxWidth;
				i.CalcWidth();
				i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, origMinWidth, origMaxWidth));
				float origMinHeight = i.minHeight;
				float origMaxHeight = i.maxHeight;
				i.CalcHeight();
				i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, origMinHeight, origMaxHeight));
			}
			else
			{
				i.CalcWidth();
				Rect winRect = GUILayoutUtility.Internal_GetWindowRect(i.windowID);
				i.SetHorizontal(winRect.x, Mathf.Clamp(winRect.width, i.minWidth, i.maxWidth));
				i.CalcHeight();
				i.SetVertical(winRect.y, Mathf.Clamp(winRect.height, i.minHeight, i.maxHeight));
				GUILayoutUtility.Internal_MoveWindow(i.windowID, i.rect);
			}
		}

		// Token: 0x060000B1 RID: 177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetWindowRect_Injected(int windowID, out Rect ret);

		// Token: 0x060000B2 RID: 178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_MoveWindow_Injected(int windowID, [In] ref Rect r);

		// Token: 0x0400006E RID: 110
		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredLayouts = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x0400006F RID: 111
		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredWindows = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x04000070 RID: 112
		internal static GUILayoutUtility.LayoutCache current = new GUILayoutUtility.LayoutCache(-1);

		// Token: 0x04000071 RID: 113
		internal static readonly Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x02000013 RID: 19
		[DebuggerDisplay("id={id}, groups={layoutGroups.Count}")]
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal sealed class LayoutCache
		{
			// Token: 0x1700002F RID: 47
			// (set) Token: 0x060000B3 RID: 179 RVA: 0x000049BF File Offset: 0x00002BBF
			private int id
			{
				[CompilerGenerated]
				set
				{
					this.<id>k__BackingField = value;
				}
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x000049C8 File Offset: 0x00002BC8
			public LayoutCache(int instanceID = -1)
			{
				this.id = instanceID;
				this.layoutGroups.Push(this.topLevel);
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x00004A18 File Offset: 0x00002C18
			public void ResetCursor()
			{
				this.windows.ResetCursor();
				this.topLevel.ResetCursor();
				foreach (object i in this.layoutGroups)
				{
					((GUILayoutGroup)i).ResetCursor();
				}
			}

			// Token: 0x04000073 RID: 115
			public GUILayoutGroup topLevel = new GUILayoutGroup();

			// Token: 0x04000074 RID: 116
			internal GenericStack layoutGroups = new GenericStack();

			// Token: 0x04000075 RID: 117
			internal GUILayoutGroup windows = new GUILayoutGroup();
		}
	}
}
