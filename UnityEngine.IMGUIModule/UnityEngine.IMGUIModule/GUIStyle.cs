using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("IMGUIScriptingClasses.h")]
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[RequiredByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyle
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005900 File Offset: 0x00003B00
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00005940 File Offset: 0x00003B40
		[NativeProperty("Name", false, TargetType.Function)]
		internal unsafe string rawName
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					GUIStyle.get_rawName_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					GUIStyle.set_rawName_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000059A4 File Offset: 0x00003BA4
		[NativeProperty("Font", false, TargetType.Function)]
		public Font font
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Font>(GUIStyle.get_font_Injected(intPtr));
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000059CC File Offset: 0x00003BCC
		[NativeProperty("m_ImagePosition", false, TargetType.Field)]
		public ImagePosition imagePosition
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_imagePosition_Injected(intPtr);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000059F0 File Offset: 0x00003BF0
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00005A14 File Offset: 0x00003C14
		[NativeProperty("m_Alignment", false, TargetType.Field)]
		public TextAnchor alignment
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_alignment_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_alignment_Injected(intPtr, value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005A38 File Offset: 0x00003C38
		[NativeProperty("m_WordWrap", false, TargetType.Field)]
		public bool wordWrap
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_wordWrap_Injected(intPtr);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005A5C File Offset: 0x00003C5C
		[NativeProperty("m_Clipping", false, TargetType.Field)]
		public TextClipping clipping
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_clipping_Injected(intPtr);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005A80 File Offset: 0x00003C80
		[NativeProperty("m_ContentOffset", false, TargetType.Field)]
		public Vector2 contentOffset
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				GUIStyle.get_contentOffset_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005AA8 File Offset: 0x00003CA8
		[NativeProperty("m_FixedWidth", false, TargetType.Field)]
		public float fixedWidth
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_fixedWidth_Injected(intPtr);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005ACC File Offset: 0x00003CCC
		[NativeProperty("m_FixedHeight", false, TargetType.Field)]
		public float fixedHeight
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_fixedHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005AF0 File Offset: 0x00003CF0
		[NativeProperty("m_StretchWidth", false, TargetType.Field)]
		public bool stretchWidth
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_stretchWidth_Injected(intPtr);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005B14 File Offset: 0x00003D14
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00005B38 File Offset: 0x00003D38
		[NativeProperty("m_StretchHeight", false, TargetType.Field)]
		public bool stretchHeight
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_stretchHeight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyle.set_stretchHeight_Injected(intPtr, value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005B5C File Offset: 0x00003D5C
		[NativeProperty("m_FontSize", false, TargetType.Field)]
		public int fontSize
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_fontSize_Injected(intPtr);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005B80 File Offset: 0x00003D80
		[NativeProperty("m_FontStyle", false, TargetType.Field)]
		public FontStyle fontStyle
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_fontStyle_Injected(intPtr);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005BA4 File Offset: 0x00003DA4
		[NativeProperty("m_RichText", false, TargetType.Field)]
		public bool richText
		{
			get
			{
				IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GUIStyle.get_richText_Injected(intPtr);
			}
		}

		// Token: 0x06000111 RID: 273
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create([Unmarshalled] GUIStyle self);

		// Token: 0x06000112 RID: 274
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr self);

		// Token: 0x06000113 RID: 275 RVA: 0x00005BC8 File Offset: 0x00003DC8
		[FreeFunction(Name = "GUIStyle_Bindings::GetStyleStatePtr", IsThreadSafe = true, HasExplicitThis = true)]
		private IntPtr GetStyleStatePtr(int idx)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GUIStyle.GetStyleStatePtr_Injected(intPtr, idx);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005BEC File Offset: 0x00003DEC
		[FreeFunction(Name = "GUIStyle_Bindings::GetRectOffsetPtr", HasExplicitThis = true)]
		private IntPtr GetRectOffsetPtr(int idx)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GUIStyle.GetRectOffsetPtr_Injected(intPtr, idx);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005C10 File Offset: 0x00003E10
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw", HasExplicitThis = true)]
		private void Internal_Draw(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GUIStyle.Internal_Draw_Injected(intPtr, ref screenRect, content, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005C3C File Offset: 0x00003E3C
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw2", HasExplicitThis = true)]
		private void Internal_Draw2(Rect position, GUIContent content, int controlID, bool on)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GUIStyle.Internal_Draw2_Injected(intPtr, ref position, content, controlID, on);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005C64 File Offset: 0x00003E64
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSize", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSize(GUIContent content)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			GUIStyle.Internal_CalcSize_Injected(intPtr, content, out vector);
			return vector;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005C8C File Offset: 0x00003E8C
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetTextRectOffset", HasExplicitThis = true)]
		internal Vector2 Internal_GetTextRectOffset(Rect screenRect, GUIContent content, Vector2 textSize)
		{
			IntPtr intPtr = GUIStyle.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			GUIStyle.Internal_GetTextRectOffset_Injected(intPtr, ref screenRect, content, ref textSize, out vector);
			return vector;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005CB8 File Offset: 0x00003EB8
		[FreeFunction(Name = "GUIStyle_Bindings::SetMouseTooltip")]
		internal unsafe static void SetMouseTooltip(string tooltip, Rect screenRect)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tooltip, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tooltip.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				GUIStyle.SetMouseTooltip_Injected(ref managedSpanWrapper, ref screenRect);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005D10 File Offset: 0x00003F10
		[FreeFunction(Name = "GUIStyle_Bindings::IsTooltipActive")]
		internal unsafe static bool IsTooltipActive(string tooltip)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tooltip, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tooltip.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = GUIStyle.IsTooltipActive_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005D68 File Offset: 0x00003F68
		[FreeFunction(Name = "GUIStyle::SetDefaultFont")]
		internal static void SetDefaultFont(Font font)
		{
			GUIStyle.SetDefaultFont_Injected(Object.MarshalledUnityObject.Marshal<Font>(font));
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005D80 File Offset: 0x00003F80
		[FreeFunction(Name = "GUIStyle::GetDefaultFont")]
		internal static Font GetDefaultFont()
		{
			return Unmarshal.UnmarshalUnityObject<Font>(GUIStyle.GetDefaultFont_Injected());
		}

		// Token: 0x0600011D RID: 285
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DestroyTextGenerator")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DestroyTextGenerator(int meshInfoId);

		// Token: 0x0600011E RID: 286 RVA: 0x00005D97 File Offset: 0x00003F97
		public GUIStyle()
		{
			this.m_Ptr = GUIStyle.Internal_Create(this);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005DB0 File Offset: 0x00003FB0
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					GUIStyle.Internal_Destroy(this.m_Ptr);
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005E08 File Offset: 0x00004008
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00005E33 File Offset: 0x00004033
		public string name
		{
			get
			{
				string text;
				if ((text = this.m_Name) == null)
				{
					text = (this.m_Name = this.rawName);
				}
				return text;
			}
			set
			{
				this.m_Name = value;
				this.rawName = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005E48 File Offset: 0x00004048
		public GUIStyleState normal
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Normal) == null)
				{
					guistyleState = (this.m_Normal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(0)));
				}
				return guistyleState;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005E7C File Offset: 0x0000407C
		public RectOffset margin
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Margin) == null)
				{
					rectOffset = (this.m_Margin = new RectOffset(this, this.GetRectOffsetPtr(1)));
				}
				return rectOffset;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005EB0 File Offset: 0x000040B0
		public RectOffset padding
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Padding) == null)
				{
					rectOffset = (this.m_Padding = new RectOffset(this, this.GetRectOffsetPtr(2)));
				}
				return rectOffset;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005EE2 File Offset: 0x000040E2
		public float lineHeight
		{
			get
			{
				return Mathf.Round(IMGUITextHandle.GetLineHeight(this));
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005EEF File Offset: 0x000040EF
		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, content, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005F03 File Offset: 0x00004103
		public void Draw(Rect position, GUIContent content, int controlID, bool on, bool hover)
		{
			this.Draw(position, content, controlID, hover, GUIUtility.hotControl == controlID, on, GUIUtility.HasKeyFocus(controlID));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005F24 File Offset: 0x00004124
		private void Draw(Rect position, GUIContent content, int controlId, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			bool flag = controlId == -1;
			if (flag)
			{
				this.Internal_Draw(position, content, isHover, isActive, on, hasKeyboardFocus);
			}
			else
			{
				this.Internal_Draw2(position, content, controlId, on);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005F5B File Offset: 0x0000415B
		public static GUIStyle none
		{
			get
			{
				GUIStyle guistyle;
				if ((guistyle = GUIStyle.s_None) == null)
				{
					guistyle = (GUIStyle.s_None = new GUIStyle());
				}
				return guistyle;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005F74 File Offset: 0x00004174
		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			IMGUITextHandle handle = IMGUITextHandle.GetTextHandle(this, this.padding.Remove(position), content.textWithWhitespace, Color.white);
			Vector2 cursorPos = handle.GetCursorPositionFromStringIndexUsingLineHeight(cursorStringIndex, false, true);
			cursorPos = new Vector2(Mathf.Max(0f, cursorPos.x), cursorPos.y);
			Vector2 rectOffset = this.Internal_GetTextRectOffset(position, content, new Vector2(handle.preferredSize.x, (handle.preferredSize.y > 0f) ? handle.preferredSize.y : this.lineHeight));
			return cursorPos + rectOffset - this.contentOffset - new Vector2(0f, this.lineHeight);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006038 File Offset: 0x00004238
		public Vector2 CalcSize(GUIContent content)
		{
			return this.Internal_CalcSize(content);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006054 File Offset: 0x00004254
		internal Vector2 GetPreferredSize(string content, Rect rect)
		{
			return IMGUITextHandle.GetTextHandle(this, this.padding.Remove(rect), content, Color.white).GetPreferredSize();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006088 File Offset: 0x00004288
		public override string ToString()
		{
			return UnityString.Format("GUIStyle '{0}'", new object[] { this.name });
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000060B4 File Offset: 0x000042B4
		[RequiredByNativeCode]
		internal static void GetMeshInfo(GUIStyle style, Color color, string content, Rect rect, ref MeshInfoBindings[] meshInfos, ref Vector2 dimensions, ref int generationId)
		{
			bool isCached = false;
			IMGUITextHandle textHandle = IMGUITextHandle.GetTextHandle(style, rect, content, color, ref isCached);
			generationId = TextHandle.settings.GetHashCode();
			bool flag = !isCached;
			if (flag)
			{
				TextInfo textInfo = textHandle.textInfo;
				meshInfos = new MeshInfoBindings[textInfo.materialCount];
				for (int i = 0; i < textInfo.materialCount; i++)
				{
					meshInfos[i].vertexData = new TextCoreVertex[textInfo.meshInfo[i].vertexCount];
					meshInfos[i].vertexCount = textInfo.meshInfo[i].vertexCount;
					meshInfos[i].material = textInfo.meshInfo[i].material;
					Array.Copy(textInfo.meshInfo[i].vertexData, meshInfos[i].vertexData, textInfo.meshInfo[i].vertexCount);
				}
			}
			dimensions = textHandle.preferredSize;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000061D5 File Offset: 0x000043D5
		[RequiredByNativeCode]
		internal static void GetDimensions(GUIStyle style, Color color, string content, Rect rect, ref Vector2 dimensions)
		{
			dimensions = style.GetPreferredSize(content, rect);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000061E7 File Offset: 0x000043E7
		[RequiredByNativeCode]
		internal static void GetLineHeight(GUIStyle style, ref float lineHeight)
		{
			lineHeight = style.lineHeight;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000061F2 File Offset: 0x000043F2
		[RequiredByNativeCode]
		internal static void EmptyManagedCache()
		{
			IMGUITextHandle.EmptyManagedCache();
		}

		// Token: 0x06000133 RID: 307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rawName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000134 RID: 308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rawName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06000135 RID: 309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_font_Injected(IntPtr _unity_self);

		// Token: 0x06000136 RID: 310
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ImagePosition get_imagePosition_Injected(IntPtr _unity_self);

		// Token: 0x06000137 RID: 311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextAnchor get_alignment_Injected(IntPtr _unity_self);

		// Token: 0x06000138 RID: 312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_alignment_Injected(IntPtr _unity_self, TextAnchor value);

		// Token: 0x06000139 RID: 313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_wordWrap_Injected(IntPtr _unity_self);

		// Token: 0x0600013A RID: 314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextClipping get_clipping_Injected(IntPtr _unity_self);

		// Token: 0x0600013B RID: 315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_contentOffset_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x0600013C RID: 316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_fixedWidth_Injected(IntPtr _unity_self);

		// Token: 0x0600013D RID: 317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_fixedHeight_Injected(IntPtr _unity_self);

		// Token: 0x0600013E RID: 318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_stretchWidth_Injected(IntPtr _unity_self);

		// Token: 0x0600013F RID: 319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_stretchHeight_Injected(IntPtr _unity_self);

		// Token: 0x06000140 RID: 320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stretchHeight_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000141 RID: 321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_fontSize_Injected(IntPtr _unity_self);

		// Token: 0x06000142 RID: 322
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FontStyle get_fontStyle_Injected(IntPtr _unity_self);

		// Token: 0x06000143 RID: 323
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_richText_Injected(IntPtr _unity_self);

		// Token: 0x06000144 RID: 324
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetStyleStatePtr_Injected(IntPtr _unity_self, int idx);

		// Token: 0x06000145 RID: 325
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetRectOffsetPtr_Injected(IntPtr _unity_self, int idx);

		// Token: 0x06000146 RID: 326
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Draw_Injected(IntPtr _unity_self, [In] ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus);

		// Token: 0x06000147 RID: 327
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Draw2_Injected(IntPtr _unity_self, [In] ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x06000148 RID: 328
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CalcSize_Injected(IntPtr _unity_self, GUIContent content, out Vector2 ret);

		// Token: 0x06000149 RID: 329
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTextRectOffset_Injected(IntPtr _unity_self, [In] ref Rect screenRect, GUIContent content, [In] ref Vector2 textSize, out Vector2 ret);

		// Token: 0x0600014A RID: 330
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMouseTooltip_Injected(ref ManagedSpanWrapper tooltip, [In] ref Rect screenRect);

		// Token: 0x0600014B RID: 331
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTooltipActive_Injected(ref ManagedSpanWrapper tooltip);

		// Token: 0x0600014C RID: 332
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDefaultFont_Injected(IntPtr font);

		// Token: 0x0600014D RID: 333
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultFont_Injected();

		// Token: 0x0400009B RID: 155
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x0400009C RID: 156
		[NonSerialized]
		private GUIStyleState m_Normal;

		// Token: 0x0400009D RID: 157
		[NonSerialized]
		private GUIStyleState m_Hover;

		// Token: 0x0400009E RID: 158
		[NonSerialized]
		private GUIStyleState m_Active;

		// Token: 0x0400009F RID: 159
		[NonSerialized]
		private GUIStyleState m_Focused;

		// Token: 0x040000A0 RID: 160
		[NonSerialized]
		private GUIStyleState m_OnNormal;

		// Token: 0x040000A1 RID: 161
		[NonSerialized]
		private GUIStyleState m_OnHover;

		// Token: 0x040000A2 RID: 162
		[NonSerialized]
		private GUIStyleState m_OnActive;

		// Token: 0x040000A3 RID: 163
		[NonSerialized]
		private GUIStyleState m_OnFocused;

		// Token: 0x040000A4 RID: 164
		[NonSerialized]
		private RectOffset m_Border;

		// Token: 0x040000A5 RID: 165
		[NonSerialized]
		private RectOffset m_Padding;

		// Token: 0x040000A6 RID: 166
		[NonSerialized]
		private RectOffset m_Margin;

		// Token: 0x040000A7 RID: 167
		[NonSerialized]
		private RectOffset m_Overflow;

		// Token: 0x040000A8 RID: 168
		[NonSerialized]
		private string m_Name;

		// Token: 0x040000A9 RID: 169
		internal static bool showKeyboardFocus = true;

		// Token: 0x040000AA RID: 170
		private static GUIStyle s_None;

		// Token: 0x0200001A RID: 26
		internal static class BindingsMarshaller
		{
			// Token: 0x0600014E RID: 334 RVA: 0x00006203 File Offset: 0x00004403
			public static IntPtr ConvertToNative(GUIStyle guiStyle)
			{
				return guiStyle.m_Ptr;
			}
		}
	}
}
