using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Modules/IMGUI/GUIContent.h")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class GUIContent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600008E RID: 142 RVA: 0x00003F60 File Offset: 0x00002160
		// (remove) Token: 0x0600008F RID: 143 RVA: 0x00003F98 File Offset: 0x00002198
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action OnTextChanged;

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003FD0 File Offset: 0x000021D0
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003FE8 File Offset: 0x000021E8
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				bool flag = this.m_Text == value;
				if (!flag)
				{
					this.m_Text = value;
					this.textWithWhitespace = value;
					Action onTextChanged = this.OnTextChanged;
					if (onTextChanged != null)
					{
						onTextChanged();
					}
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000402C File Offset: 0x0000222C
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004058 File Offset: 0x00002258
		internal string textWithWhitespace
		{
			get
			{
				return string.IsNullOrEmpty(this.m_TextWithWhitespace) ? GUIContent.k_ZeroWidthSpace : this.m_TextWithWhitespace;
			}
			set
			{
				this.m_TextWithWhitespace = value + GUIContent.k_ZeroWidthSpace;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000406B File Offset: 0x0000226B
		internal void SetTextWithoutNotify(string value)
		{
			this.m_Text = value;
			this.textWithWhitespace = value;
		}

		// Token: 0x1700002D RID: 45
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000407D File Offset: 0x0000227D
		public Texture image
		{
			set
			{
				this.m_Image = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004088 File Offset: 0x00002288
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000040A0 File Offset: 0x000022A0
		public string tooltip
		{
			get
			{
				return this.m_Tooltip;
			}
			set
			{
				this.m_Tooltip = value;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000040AA File Offset: 0x000022AA
		public GUIContent()
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000040D5 File Offset: 0x000022D5
		public GUIContent(string text)
			: this(text, null, string.Empty)
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000040E6 File Offset: 0x000022E6
		public GUIContent(string text, string tooltip)
			: this(text, null, tooltip)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000040F4 File Offset: 0x000022F4
		public GUIContent(string text, Texture image, string tooltip)
		{
			this.text = text;
			this.image = image;
			this.tooltip = tooltip;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004144 File Offset: 0x00002344
		internal static GUIContent Temp(string t)
		{
			GUIContent.s_Text.m_Text = t;
			GUIContent.s_Text.textWithWhitespace = t;
			GUIContent.s_Text.m_Tooltip = string.Empty;
			return GUIContent.s_Text;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004184 File Offset: 0x00002384
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void ClearStaticCache()
		{
			GUIContent.s_Text.m_Text = null;
			GUIContent.s_Text.m_TextWithWhitespace = null;
			GUIContent.s_Text.m_Tooltip = string.Empty;
			GUIContent.s_Image.m_Image = null;
			GUIContent.s_Image.m_Tooltip = string.Empty;
			GUIContent.s_Image.m_TextWithWhitespace = null;
			GUIContent.s_TextImage.m_Text = null;
			GUIContent.s_TextImage.m_Image = null;
			GUIContent.s_TextImage.m_TextWithWhitespace = null;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004200 File Offset: 0x00002400
		public override string ToString()
		{
			string text;
			if ((text = this.text) == null)
			{
				text = this.tooltip ?? base.ToString();
			}
			return text;
		}

		// Token: 0x0400004B RID: 75
		[SerializeField]
		private string m_Text = string.Empty;

		// Token: 0x0400004C RID: 76
		[SerializeField]
		private Texture m_Image;

		// Token: 0x0400004D RID: 77
		[SerializeField]
		private string m_Tooltip = string.Empty;

		// Token: 0x0400004E RID: 78
		[SerializeField]
		private string m_TextWithWhitespace = string.Empty;

		// Token: 0x04000050 RID: 80
		private static readonly GUIContent s_Text = new GUIContent();

		// Token: 0x04000051 RID: 81
		private static readonly GUIContent s_Image = new GUIContent();

		// Token: 0x04000052 RID: 82
		private static readonly GUIContent s_TextImage = new GUIContent();

		// Token: 0x04000053 RID: 83
		internal static string k_ZeroWidthSpace = "\u200b";

		// Token: 0x04000054 RID: 84
		public static GUIContent none = new GUIContent("");
	}
}
