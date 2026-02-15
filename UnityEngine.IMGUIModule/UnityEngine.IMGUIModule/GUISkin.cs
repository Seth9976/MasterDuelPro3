using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	[ExecuteInEditMode]
	[RequiredByNativeCode]
	[AssetFileNameExtension("guiskin", new string[] { })]
	[Serializable]
	public sealed class GUISkin : ScriptableObject
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004ADE File Offset: 0x00002CDE
		public GUISkin()
		{
			this.m_CustomStyles = new GUIStyle[1];
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004B06 File Offset: 0x00002D06
		internal void OnEnable()
		{
			this.Apply();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004B10 File Offset: 0x00002D10
		internal static void CleanupRoots()
		{
			GUISkin.current = null;
			GUISkin.ms_Error = null;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004B20 File Offset: 0x00002D20
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004B38 File Offset: 0x00002D38
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
				bool flag = GUISkin.current == this;
				if (flag)
				{
					GUIStyle.SetDefaultFont(this.m_Font);
				}
				this.Apply();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004B70 File Offset: 0x00002D70
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004B88 File Offset: 0x00002D88
		public GUIStyle box
		{
			get
			{
				return this.m_box;
			}
			set
			{
				this.m_box = value;
				this.Apply();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004B9C File Offset: 0x00002D9C
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public GUIStyle label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
				this.Apply();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004BC8 File Offset: 0x00002DC8
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public GUIStyle textField
		{
			get
			{
				return this.m_textField;
			}
			set
			{
				this.m_textField = value;
				this.Apply();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004BF4 File Offset: 0x00002DF4
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00004C0C File Offset: 0x00002E0C
		public GUIStyle textArea
		{
			get
			{
				return this.m_textArea;
			}
			set
			{
				this.m_textArea = value;
				this.Apply();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004C20 File Offset: 0x00002E20
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004C38 File Offset: 0x00002E38
		public GUIStyle button
		{
			get
			{
				return this.m_button;
			}
			set
			{
				this.m_button = value;
				this.Apply();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004C4C File Offset: 0x00002E4C
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00004C64 File Offset: 0x00002E64
		public GUIStyle toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
				this.Apply();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004C78 File Offset: 0x00002E78
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00004C90 File Offset: 0x00002E90
		public GUIStyle window
		{
			get
			{
				return this.m_window;
			}
			set
			{
				this.m_window = value;
				this.Apply();
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004CA4 File Offset: 0x00002EA4
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00004CBC File Offset: 0x00002EBC
		public GUIStyle horizontalSlider
		{
			get
			{
				return this.m_horizontalSlider;
			}
			set
			{
				this.m_horizontalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004CD0 File Offset: 0x00002ED0
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004CE8 File Offset: 0x00002EE8
		public GUIStyle horizontalSliderThumb
		{
			get
			{
				return this.m_horizontalSliderThumb;
			}
			set
			{
				this.m_horizontalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004CFC File Offset: 0x00002EFC
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004D14 File Offset: 0x00002F14
		internal GUIStyle horizontalSliderThumbExtent
		{
			get
			{
				return this.m_horizontalSliderThumbExtent;
			}
			set
			{
				this.m_horizontalSliderThumbExtent = value;
				this.Apply();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004D28 File Offset: 0x00002F28
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00004D40 File Offset: 0x00002F40
		internal GUIStyle sliderMixed
		{
			get
			{
				return this.m_SliderMixed;
			}
			set
			{
				this.m_SliderMixed = value;
				this.Apply();
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004D54 File Offset: 0x00002F54
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004D6C File Offset: 0x00002F6C
		public GUIStyle verticalSlider
		{
			get
			{
				return this.m_verticalSlider;
			}
			set
			{
				this.m_verticalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004D80 File Offset: 0x00002F80
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004D98 File Offset: 0x00002F98
		public GUIStyle verticalSliderThumb
		{
			get
			{
				return this.m_verticalSliderThumb;
			}
			set
			{
				this.m_verticalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004DAC File Offset: 0x00002FAC
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004DC4 File Offset: 0x00002FC4
		internal GUIStyle verticalSliderThumbExtent
		{
			get
			{
				return this.m_verticalSliderThumbExtent;
			}
			set
			{
				this.m_verticalSliderThumbExtent = value;
				this.Apply();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004DD8 File Offset: 0x00002FD8
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public GUIStyle horizontalScrollbar
		{
			get
			{
				return this.m_horizontalScrollbar;
			}
			set
			{
				this.m_horizontalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004E04 File Offset: 0x00003004
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004E1C File Offset: 0x0000301C
		public GUIStyle horizontalScrollbarThumb
		{
			get
			{
				return this.m_horizontalScrollbarThumb;
			}
			set
			{
				this.m_horizontalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004E30 File Offset: 0x00003030
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004E48 File Offset: 0x00003048
		public GUIStyle horizontalScrollbarLeftButton
		{
			get
			{
				return this.m_horizontalScrollbarLeftButton;
			}
			set
			{
				this.m_horizontalScrollbarLeftButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004E5C File Offset: 0x0000305C
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004E74 File Offset: 0x00003074
		public GUIStyle horizontalScrollbarRightButton
		{
			get
			{
				return this.m_horizontalScrollbarRightButton;
			}
			set
			{
				this.m_horizontalScrollbarRightButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004E88 File Offset: 0x00003088
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004EA0 File Offset: 0x000030A0
		public GUIStyle verticalScrollbar
		{
			get
			{
				return this.m_verticalScrollbar;
			}
			set
			{
				this.m_verticalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004EB4 File Offset: 0x000030B4
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004ECC File Offset: 0x000030CC
		public GUIStyle verticalScrollbarThumb
		{
			get
			{
				return this.m_verticalScrollbarThumb;
			}
			set
			{
				this.m_verticalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004EE0 File Offset: 0x000030E0
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004EF8 File Offset: 0x000030F8
		public GUIStyle verticalScrollbarUpButton
		{
			get
			{
				return this.m_verticalScrollbarUpButton;
			}
			set
			{
				this.m_verticalScrollbarUpButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004F0C File Offset: 0x0000310C
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004F24 File Offset: 0x00003124
		public GUIStyle verticalScrollbarDownButton
		{
			get
			{
				return this.m_verticalScrollbarDownButton;
			}
			set
			{
				this.m_verticalScrollbarDownButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004F38 File Offset: 0x00003138
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004F50 File Offset: 0x00003150
		public GUIStyle scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
			set
			{
				this.m_ScrollView = value;
				this.Apply();
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004F64 File Offset: 0x00003164
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00004F7C File Offset: 0x0000317C
		public GUIStyle[] customStyles
		{
			get
			{
				return this.m_CustomStyles;
			}
			set
			{
				this.m_CustomStyles = value;
				this.Apply();
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004F90 File Offset: 0x00003190
		public GUISettings settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004FA8 File Offset: 0x000031A8
		internal static GUIStyle error
		{
			get
			{
				bool flag = GUISkin.ms_Error == null;
				if (flag)
				{
					GUISkin.ms_Error = new GUIStyle();
					GUISkin.ms_Error.name = "StyleNotFoundError";
				}
				return GUISkin.ms_Error;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004FE8 File Offset: 0x000031E8
		internal void Apply()
		{
			bool flag = this.m_CustomStyles == null;
			if (flag)
			{
				Debug.Log("custom styles is null");
			}
			this.BuildStyleCache();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005018 File Offset: 0x00003218
		private void BuildStyleCache()
		{
			bool flag = this.m_box == null;
			if (flag)
			{
				this.m_box = new GUIStyle();
			}
			bool flag2 = this.m_button == null;
			if (flag2)
			{
				this.m_button = new GUIStyle();
			}
			bool flag3 = this.m_toggle == null;
			if (flag3)
			{
				this.m_toggle = new GUIStyle();
			}
			bool flag4 = this.m_label == null;
			if (flag4)
			{
				this.m_label = new GUIStyle();
			}
			bool flag5 = this.m_window == null;
			if (flag5)
			{
				this.m_window = new GUIStyle();
			}
			bool flag6 = this.m_textField == null;
			if (flag6)
			{
				this.m_textField = new GUIStyle();
			}
			bool flag7 = this.m_textArea == null;
			if (flag7)
			{
				this.m_textArea = new GUIStyle();
			}
			bool flag8 = this.m_horizontalSlider == null;
			if (flag8)
			{
				this.m_horizontalSlider = new GUIStyle();
			}
			bool flag9 = this.m_horizontalSliderThumb == null;
			if (flag9)
			{
				this.m_horizontalSliderThumb = new GUIStyle();
			}
			bool flag10 = this.m_verticalSlider == null;
			if (flag10)
			{
				this.m_verticalSlider = new GUIStyle();
			}
			bool flag11 = this.m_verticalSliderThumb == null;
			if (flag11)
			{
				this.m_verticalSliderThumb = new GUIStyle();
			}
			bool flag12 = this.m_horizontalScrollbar == null;
			if (flag12)
			{
				this.m_horizontalScrollbar = new GUIStyle();
			}
			bool flag13 = this.m_horizontalScrollbarThumb == null;
			if (flag13)
			{
				this.m_horizontalScrollbarThumb = new GUIStyle();
			}
			bool flag14 = this.m_horizontalScrollbarLeftButton == null;
			if (flag14)
			{
				this.m_horizontalScrollbarLeftButton = new GUIStyle();
			}
			bool flag15 = this.m_horizontalScrollbarRightButton == null;
			if (flag15)
			{
				this.m_horizontalScrollbarRightButton = new GUIStyle();
			}
			bool flag16 = this.m_verticalScrollbar == null;
			if (flag16)
			{
				this.m_verticalScrollbar = new GUIStyle();
			}
			bool flag17 = this.m_verticalScrollbarThumb == null;
			if (flag17)
			{
				this.m_verticalScrollbarThumb = new GUIStyle();
			}
			bool flag18 = this.m_verticalScrollbarUpButton == null;
			if (flag18)
			{
				this.m_verticalScrollbarUpButton = new GUIStyle();
			}
			bool flag19 = this.m_verticalScrollbarDownButton == null;
			if (flag19)
			{
				this.m_verticalScrollbarDownButton = new GUIStyle();
			}
			bool flag20 = this.m_ScrollView == null;
			if (flag20)
			{
				this.m_ScrollView = new GUIStyle();
			}
			this.m_Styles = new Dictionary<string, GUIStyle>(StringComparer.OrdinalIgnoreCase);
			this.m_Styles["box"] = this.m_box;
			this.m_box.name = "box";
			this.m_Styles["button"] = this.m_button;
			this.m_button.name = "button";
			this.m_Styles["toggle"] = this.m_toggle;
			this.m_toggle.name = "toggle";
			this.m_Styles["label"] = this.m_label;
			this.m_label.name = "label";
			this.m_Styles["window"] = this.m_window;
			this.m_window.name = "window";
			this.m_Styles["textfield"] = this.m_textField;
			this.m_textField.name = "textfield";
			this.m_Styles["textarea"] = this.m_textArea;
			this.m_textArea.name = "textarea";
			this.m_Styles["horizontalslider"] = this.m_horizontalSlider;
			this.m_horizontalSlider.name = "horizontalslider";
			this.m_Styles["horizontalsliderthumb"] = this.m_horizontalSliderThumb;
			this.m_horizontalSliderThumb.name = "horizontalsliderthumb";
			this.m_Styles["verticalslider"] = this.m_verticalSlider;
			this.m_verticalSlider.name = "verticalslider";
			this.m_Styles["verticalsliderthumb"] = this.m_verticalSliderThumb;
			this.m_verticalSliderThumb.name = "verticalsliderthumb";
			this.m_Styles["horizontalscrollbar"] = this.m_horizontalScrollbar;
			this.m_horizontalScrollbar.name = "horizontalscrollbar";
			this.m_Styles["horizontalscrollbarthumb"] = this.m_horizontalScrollbarThumb;
			this.m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
			this.m_Styles["horizontalscrollbarleftbutton"] = this.m_horizontalScrollbarLeftButton;
			this.m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
			this.m_Styles["horizontalscrollbarrightbutton"] = this.m_horizontalScrollbarRightButton;
			this.m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
			this.m_Styles["verticalscrollbar"] = this.m_verticalScrollbar;
			this.m_verticalScrollbar.name = "verticalscrollbar";
			this.m_Styles["verticalscrollbarthumb"] = this.m_verticalScrollbarThumb;
			this.m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
			this.m_Styles["verticalscrollbarupbutton"] = this.m_verticalScrollbarUpButton;
			this.m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
			this.m_Styles["verticalscrollbardownbutton"] = this.m_verticalScrollbarDownButton;
			this.m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
			this.m_Styles["scrollview"] = this.m_ScrollView;
			this.m_ScrollView.name = "scrollview";
			bool flag21 = this.m_CustomStyles != null;
			if (flag21)
			{
				for (int i = 0; i < this.m_CustomStyles.Length; i++)
				{
					bool flag22 = this.m_CustomStyles[i] == null;
					if (!flag22)
					{
						this.m_Styles[this.m_CustomStyles[i].name] = this.m_CustomStyles[i];
					}
				}
			}
			bool flag23 = !this.m_Styles.TryGetValue("HorizontalSliderThumbExtent", out this.m_horizontalSliderThumbExtent);
			if (flag23)
			{
				this.m_horizontalSliderThumbExtent = new GUIStyle();
				this.m_horizontalSliderThumbExtent.name = "horizontalsliderthumbextent";
				this.m_Styles["HorizontalSliderThumbExtent"] = this.m_horizontalSliderThumbExtent;
			}
			bool flag24 = !this.m_Styles.TryGetValue("SliderMixed", out this.m_SliderMixed);
			if (flag24)
			{
				this.m_SliderMixed = new GUIStyle();
				this.m_SliderMixed.name = "SliderMixed";
				this.m_Styles["SliderMixed"] = this.m_SliderMixed;
			}
			bool flag25 = !this.m_Styles.TryGetValue("VerticalSliderThumbExtent", out this.m_verticalSliderThumbExtent);
			if (flag25)
			{
				this.m_verticalSliderThumbExtent = new GUIStyle();
				this.m_Styles["VerticalSliderThumbExtent"] = this.m_verticalSliderThumbExtent;
				this.m_verticalSliderThumbExtent.name = "verticalsliderthumbextent";
			}
			GUISkin.error.stretchHeight = true;
			GUISkin.error.normal.textColor = Color.red;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000056DC File Offset: 0x000038DC
		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle s = this.FindStyle(styleName);
			bool flag = s != null;
			GUIStyle guistyle;
			if (flag)
			{
				guistyle = s;
			}
			else
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to find style '",
					styleName,
					"' in skin '",
					base.name,
					"' ",
					(Event.current != null) ? Event.current.type.ToString() : "<called outside OnGUI>"
				}));
				guistyle = GUISkin.error;
			}
			return guistyle;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005768 File Offset: 0x00003968
		public GUIStyle FindStyle(string styleName)
		{
			bool flag = this.m_Styles == null;
			if (flag)
			{
				this.BuildStyleCache();
			}
			GUIStyle style;
			bool flag2 = this.m_Styles.TryGetValue(styleName, out style);
			GUIStyle guistyle;
			if (flag2)
			{
				guistyle = style;
			}
			else
			{
				guistyle = null;
			}
			return guistyle;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000057A8 File Offset: 0x000039A8
		internal void MakeCurrent()
		{
			GUISkin.current = this;
			GUIStyle.SetDefaultFont(this.font);
			bool flag = GUISkin.m_SkinChanged != null;
			if (flag)
			{
				GUISkin.m_SkinChanged();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000057E0 File Offset: 0x000039E0
		public IEnumerator GetEnumerator()
		{
			bool flag = this.m_Styles == null;
			if (flag)
			{
				this.BuildStyleCache();
			}
			return this.m_Styles.Values.GetEnumerator();
		}

		// Token: 0x0400007B RID: 123
		[SerializeField]
		private Font m_Font;

		// Token: 0x0400007C RID: 124
		[SerializeField]
		private GUIStyle m_box;

		// Token: 0x0400007D RID: 125
		[SerializeField]
		private GUIStyle m_button;

		// Token: 0x0400007E RID: 126
		[SerializeField]
		private GUIStyle m_toggle;

		// Token: 0x0400007F RID: 127
		[SerializeField]
		private GUIStyle m_label;

		// Token: 0x04000080 RID: 128
		[SerializeField]
		private GUIStyle m_textField;

		// Token: 0x04000081 RID: 129
		[SerializeField]
		private GUIStyle m_textArea;

		// Token: 0x04000082 RID: 130
		[SerializeField]
		private GUIStyle m_window;

		// Token: 0x04000083 RID: 131
		[SerializeField]
		private GUIStyle m_horizontalSlider;

		// Token: 0x04000084 RID: 132
		[SerializeField]
		private GUIStyle m_horizontalSliderThumb;

		// Token: 0x04000085 RID: 133
		[NonSerialized]
		private GUIStyle m_horizontalSliderThumbExtent;

		// Token: 0x04000086 RID: 134
		[SerializeField]
		private GUIStyle m_verticalSlider;

		// Token: 0x04000087 RID: 135
		[SerializeField]
		private GUIStyle m_verticalSliderThumb;

		// Token: 0x04000088 RID: 136
		[NonSerialized]
		private GUIStyle m_verticalSliderThumbExtent;

		// Token: 0x04000089 RID: 137
		[NonSerialized]
		private GUIStyle m_SliderMixed;

		// Token: 0x0400008A RID: 138
		[SerializeField]
		private GUIStyle m_horizontalScrollbar;

		// Token: 0x0400008B RID: 139
		[SerializeField]
		private GUIStyle m_horizontalScrollbarThumb;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		private GUIStyle m_horizontalScrollbarLeftButton;

		// Token: 0x0400008D RID: 141
		[SerializeField]
		private GUIStyle m_horizontalScrollbarRightButton;

		// Token: 0x0400008E RID: 142
		[SerializeField]
		private GUIStyle m_verticalScrollbar;

		// Token: 0x0400008F RID: 143
		[SerializeField]
		private GUIStyle m_verticalScrollbarThumb;

		// Token: 0x04000090 RID: 144
		[SerializeField]
		private GUIStyle m_verticalScrollbarUpButton;

		// Token: 0x04000091 RID: 145
		[SerializeField]
		private GUIStyle m_verticalScrollbarDownButton;

		// Token: 0x04000092 RID: 146
		[SerializeField]
		private GUIStyle m_ScrollView;

		// Token: 0x04000093 RID: 147
		[SerializeField]
		internal GUIStyle[] m_CustomStyles;

		// Token: 0x04000094 RID: 148
		[SerializeField]
		private GUISettings m_Settings = new GUISettings();

		// Token: 0x04000095 RID: 149
		internal static GUIStyle ms_Error;

		// Token: 0x04000096 RID: 150
		private Dictionary<string, GUIStyle> m_Styles = null;

		// Token: 0x04000097 RID: 151
		internal static GUISkin.SkinChangedDelegate m_SkinChanged;

		// Token: 0x04000098 RID: 152
		internal static GUISkin current;

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x060000F5 RID: 245
		internal delegate void SkinChangedDelegate();
	}
}
