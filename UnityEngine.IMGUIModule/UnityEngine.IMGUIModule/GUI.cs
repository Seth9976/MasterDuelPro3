using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Modules/IMGUI/GUISkin.bindings.h")]
	[NativeHeader("Modules/IMGUI/GUI.bindings.h")]
	public class GUI
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003AFC File Offset: 0x00001CFC
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003B14 File Offset: 0x00001D14
		public static Color color
		{
			get
			{
				Color color;
				GUI.get_color_Injected(out color);
				return color;
			}
			set
			{
				GUI.set_color_Injected(ref value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003B28 File Offset: 0x00001D28
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003B40 File Offset: 0x00001D40
		public static Color backgroundColor
		{
			get
			{
				Color color;
				GUI.get_backgroundColor_Injected(out color);
				return color;
			}
			set
			{
				GUI.set_backgroundColor_Injected(ref value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003B54 File Offset: 0x00001D54
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003B6C File Offset: 0x00001D6C
		public static Color contentColor
		{
			get
			{
				Color color;
				GUI.get_contentColor_Injected(out color);
				return color;
			}
			set
			{
				GUI.set_contentColor_Injected(ref value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000065 RID: 101
		// (set) Token: 0x06000066 RID: 102
		public static extern bool changed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000067 RID: 103
		// (set) Token: 0x06000068 RID: 104
		public static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000026 RID: 38
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003C21 File Offset: 0x00001E21
		internal static DateTime nextScrollStepTime
		{
			[CompilerGenerated]
			set
			{
				GUI.<nextScrollStepTime>k__BackingField = value;
			}
		} = DateTime.Now;

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003C3C File Offset: 0x00001E3C
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003C29 File Offset: 0x00001E29
		public static GUISkin skin
		{
			get
			{
				GUIUtility.CheckOnGUI();
				return GUI.s_Skin;
			}
			set
			{
				GUIUtility.CheckOnGUI();
				GUI.DoSetSkin(value);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003C5C File Offset: 0x00001E5C
		internal static void DoSetSkin(GUISkin newSkin)
		{
			bool flag = !newSkin;
			if (flag)
			{
				newSkin = GUIUtility.GetDefaultSkin();
			}
			GUI.s_Skin = newSkin;
			newSkin.MakeCurrent();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003C8C File Offset: 0x00001E8C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003CA3 File Offset: 0x00001EA3
		public static Matrix4x4 matrix
		{
			get
			{
				return GUIClip.GetMatrix();
			}
			set
			{
				GUIClip.SetMatrix(value);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003CAD File Offset: 0x00001EAD
		public static void Label(Rect position, string text)
		{
			GUI.Label(position, GUIContent.Temp(text), GUI.s_Skin.label);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003CC7 File Offset: 0x00001EC7
		public static void Label(Rect position, string text, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public static void Label(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUI.DoLabel(position, content, style);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003CEA File Offset: 0x00001EEA
		public static void Box(Rect position, string text)
		{
			GUI.Box(position, GUIContent.Temp(text), GUI.s_Skin.box);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003D04 File Offset: 0x00001F04
		public static void Box(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int id = GUIUtility.GetControlID(GUI.s_BoxHash, FocusType.Passive);
			bool flag = Event.current.type == EventType.Repaint;
			if (flag)
			{
				style.Draw(position, content, id, false, position.Contains(Event.current.mousePosition));
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003D54 File Offset: 0x00001F54
		private static void DoLabel(Rect position, GUIContent content, GUIStyle style)
		{
			Event evt = Event.current;
			bool flag = evt.type != EventType.Repaint;
			if (!flag)
			{
				bool hovered = position.Contains(evt.mousePosition);
				style.Draw(position, content, hovered, false, false, false);
				bool flag2 = !string.IsNullOrEmpty(content.tooltip) && hovered && GUIClip.visibleRect.Contains(evt.mousePosition);
				if (flag2)
				{
					bool flag3 = !GUIStyle.IsTooltipActive(content.tooltip);
					if (flag3)
					{
						GUI.s_ToolTipRect = new Rect(evt.mousePosition, Vector2.zero);
					}
					GUIStyle.SetMouseTooltip(content.tooltip, GUI.s_ToolTipRect);
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003E01 File Offset: 0x00002001
		internal static GenericStack scrollViewStates { get; } = new GenericStack();

		// Token: 0x06000077 RID: 119 RVA: 0x00003E08 File Offset: 0x00002008
		[RequiredByNativeCode]
		internal static void CallWindowDelegate(GUI.WindowFunction func, int id, int instanceID, GUISkin _skin, int forceRect, float width, float height, GUIStyle style)
		{
			GUILayoutUtility.SelectIDList(id, true);
			GUISkin temp = GUI.skin;
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				bool flag2 = forceRect != 0;
				if (flag2)
				{
					GUILayoutOption[] options = new GUILayoutOption[]
					{
						GUILayout.Width(width),
						GUILayout.Height(height)
					};
					GUILayoutUtility.BeginWindow(id, style, options);
				}
				else
				{
					GUILayoutUtility.BeginWindow(id, style, null);
				}
			}
			else
			{
				GUILayoutUtility.BeginWindow(id, GUIStyle.none, null);
			}
			GUI.skin = _skin;
			if (func != null)
			{
				func(id);
			}
			bool flag3 = Event.current.type == EventType.Layout;
			if (flag3)
			{
				GUILayoutUtility.Layout();
			}
			GUI.skin = temp;
		}

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(out Color ret);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected([In] ref Color value);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_backgroundColor_Injected(out Color ret);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_backgroundColor_Injected([In] ref Color value);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_contentColor_Injected(out Color ret);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_contentColor_Injected([In] ref Color value);

		// Token: 0x0400003D RID: 61
		private static int s_HotTextField = -1;

		// Token: 0x0400003E RID: 62
		private static readonly int s_BoxHash = "Box".GetHashCode();

		// Token: 0x0400003F RID: 63
		private static readonly int s_ButonHash = "Button".GetHashCode();

		// Token: 0x04000040 RID: 64
		private static readonly int s_RepeatButtonHash = "repeatButton".GetHashCode();

		// Token: 0x04000041 RID: 65
		private static readonly int s_ToggleHash = "Toggle".GetHashCode();

		// Token: 0x04000042 RID: 66
		private static readonly int s_ButtonGridHash = "ButtonGrid".GetHashCode();

		// Token: 0x04000043 RID: 67
		private static readonly int s_SliderHash = "Slider".GetHashCode();

		// Token: 0x04000044 RID: 68
		private static readonly int s_BeginGroupHash = "BeginGroup".GetHashCode();

		// Token: 0x04000045 RID: 69
		private static readonly int s_ScrollviewHash = "scrollView".GetHashCode();

		// Token: 0x04000047 RID: 71
		private static GUISkin s_Skin;

		// Token: 0x04000048 RID: 72
		internal static Rect s_ToolTipRect;

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x0600007F RID: 127
		public delegate void WindowFunction(int id);
	}
}
