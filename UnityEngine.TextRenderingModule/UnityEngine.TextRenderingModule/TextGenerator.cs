using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("Modules/TextRendering/TextGenerator.h")]
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TextGenerator : IDisposable
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002229 File Offset: 0x00000429
		public TextGenerator()
			: this(50)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002235 File Offset: 0x00000435
		public TextGenerator(int initialCapacity)
		{
			this.m_Ptr = TextGenerator.Internal_Create();
			this.m_Verts = new List<UIVertex>((initialCapacity + 1) * 4);
			this.m_Characters = new List<UICharInfo>(initialCapacity + 1);
			this.m_Lines = new List<UILineInfo>(20);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002278 File Offset: 0x00000478
		~TextGenerator()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022A8 File Offset: 0x000004A8
		void IDisposable.Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				TextGenerator.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000022E3 File Offset: 0x000004E3
		public int characterCountVisible
		{
			get
			{
				return this.characterCount - 1;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022F0 File Offset: 0x000004F0
		private TextGenerationSettings ValidatedSettings(TextGenerationSettings settings)
		{
			bool flag = settings.font != null && settings.font.dynamic;
			TextGenerationSettings textGenerationSettings;
			if (flag)
			{
				textGenerationSettings = settings;
			}
			else
			{
				bool flag2 = settings.fontSize != 0 || settings.fontStyle > FontStyle.Normal;
				if (flag2)
				{
					bool flag3 = settings.font != null;
					if (flag3)
					{
						Debug.LogWarningFormat(settings.font, "Font size and style overrides are only supported for dynamic fonts. Font '{0}' is not dynamic.", new object[] { settings.font.name });
					}
					settings.fontSize = 0;
					settings.fontStyle = FontStyle.Normal;
				}
				bool resizeTextForBestFit = settings.resizeTextForBestFit;
				if (resizeTextForBestFit)
				{
					bool flag4 = settings.font != null;
					if (flag4)
					{
						Debug.LogWarningFormat(settings.font, "BestFit is only supported for dynamic fonts. Font '{0}' is not dynamic.", new object[] { settings.font.name });
					}
					settings.resizeTextForBestFit = false;
				}
				textGenerationSettings = settings;
			}
			return textGenerationSettings;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023D6 File Offset: 0x000005D6
		public void Invalidate()
		{
			this.m_HasGenerated = false;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023E0 File Offset: 0x000005E0
		public void GetCharacters(List<UICharInfo> characters)
		{
			this.GetCharactersInternal(characters);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023EB File Offset: 0x000005EB
		public void GetLines(List<UILineInfo> lines)
		{
			this.GetLinesInternal(lines);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023F6 File Offset: 0x000005F6
		public void GetVertices(List<UIVertex> vertices)
		{
			this.GetVerticesInternal(vertices);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002404 File Offset: 0x00000604
		public float GetPreferredWidth(string str, TextGenerationSettings settings)
		{
			settings.horizontalOverflow = HorizontalWrapMode.Overflow;
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.width;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002448 File Offset: 0x00000648
		public float GetPreferredHeight(string str, TextGenerationSettings settings)
		{
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.height;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002484 File Offset: 0x00000684
		public bool PopulateWithErrors(string str, TextGenerationSettings settings, GameObject context)
		{
			TextGenerationError error = this.PopulateWithError(str, settings);
			bool flag = error == TextGenerationError.None;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = (error & TextGenerationError.CustomSizeOnNonDynamicFont) > TextGenerationError.None;
				if (flag3)
				{
					Debug.LogErrorFormat(context, "Font '{0}' is not dynamic, which is required to override its size", new object[] { settings.font });
				}
				bool flag4 = (error & TextGenerationError.CustomStyleOnNonDynamicFont) > TextGenerationError.None;
				if (flag4)
				{
					Debug.LogErrorFormat(context, "Font '{0}' is not dynamic, which is required to override its style", new object[] { settings.font });
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024F8 File Offset: 0x000006F8
		public bool Populate(string str, TextGenerationSettings settings)
		{
			TextGenerationError textGenerationError = this.PopulateWithError(str, settings);
			return textGenerationError == TextGenerationError.None;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002518 File Offset: 0x00000718
		private TextGenerationError PopulateWithError(string str, TextGenerationSettings settings)
		{
			bool flag = this.m_HasGenerated && str == this.m_LastString && settings.Equals(this.m_LastSettings);
			TextGenerationError textGenerationError;
			if (flag)
			{
				textGenerationError = this.m_LastValid;
			}
			else
			{
				this.m_LastValid = this.PopulateAlways(str, settings);
				textGenerationError = this.m_LastValid;
			}
			return textGenerationError;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002574 File Offset: 0x00000774
		private TextGenerationError PopulateAlways(string str, TextGenerationSettings settings)
		{
			this.m_LastString = str;
			this.m_HasGenerated = true;
			this.m_CachedVerts = false;
			this.m_CachedCharacters = false;
			this.m_CachedLines = false;
			this.m_LastSettings = settings;
			TextGenerationSettings validSettings = this.ValidatedSettings(settings);
			TextGenerationError error;
			this.Populate_Internal(str, validSettings.font, validSettings.color, validSettings.fontSize, validSettings.scaleFactor, validSettings.lineSpacing, validSettings.fontStyle, validSettings.richText, validSettings.resizeTextForBestFit, validSettings.resizeTextMinSize, validSettings.resizeTextMaxSize, validSettings.verticalOverflow, validSettings.horizontalOverflow, validSettings.updateBounds, validSettings.textAnchor, validSettings.generationExtents, validSettings.pivot, validSettings.generateOutOfBounds, validSettings.alignByGeometry, out error);
			this.m_LastValid = error;
			return error;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002638 File Offset: 0x00000838
		public IList<UIVertex> verts
		{
			get
			{
				bool flag = !this.m_CachedVerts;
				if (flag)
				{
					this.GetVertices(this.m_Verts);
					this.m_CachedVerts = true;
				}
				return this.m_Verts;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002674 File Offset: 0x00000874
		public IList<UICharInfo> characters
		{
			get
			{
				bool flag = !this.m_CachedCharacters;
				if (flag)
				{
					this.GetCharacters(this.m_Characters);
					this.m_CachedCharacters = true;
				}
				return this.m_Characters;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000026B0 File Offset: 0x000008B0
		public IList<UILineInfo> lines
		{
			get
			{
				bool flag = !this.m_CachedLines;
				if (flag)
				{
					this.GetLines(this.m_Lines);
					this.m_CachedLines = true;
				}
				return this.m_Lines;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000026EC File Offset: 0x000008EC
		public Rect rectExtents
		{
			get
			{
				IntPtr intPtr = TextGenerator.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rect rect;
				TextGenerator.get_rectExtents_Injected(intPtr, out rect);
				return rect;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002714 File Offset: 0x00000914
		public int characterCount
		{
			get
			{
				IntPtr intPtr = TextGenerator.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TextGenerator.get_characterCount_Injected(intPtr);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002738 File Offset: 0x00000938
		public int lineCount
		{
			get
			{
				IntPtr intPtr = TextGenerator.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TextGenerator.get_lineCount_Injected(intPtr);
			}
		}

		// Token: 0x0600001A RID: 26
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x0600001B RID: 27
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0600001C RID: 28 RVA: 0x0000275C File Offset: 0x0000095C
		internal unsafe bool Populate_Internal(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds, bool alignByGeometry, out uint error)
		{
			bool flag;
			try
			{
				IntPtr intPtr = TextGenerator.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(str, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = str.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = TextGenerator.Populate_Internal_Injected(intPtr, ref managedSpanWrapper, Object.MarshalledUnityObject.Marshal<Font>(font), ref color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, verticalOverFlow, horizontalOverflow, updateBounds, anchor, extentsX, extentsY, pivotX, pivotY, generateOutOfBounds, alignByGeometry, out error);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027F0 File Offset: 0x000009F0
		internal bool Populate_Internal(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, VerticalWrapMode verticalOverFlow, HorizontalWrapMode horizontalOverflow, bool updateBounds, TextAnchor anchor, Vector2 extents, Vector2 pivot, bool generateOutOfBounds, bool alignByGeometry, out TextGenerationError error)
		{
			bool flag = font == null;
			bool flag2;
			if (flag)
			{
				error = TextGenerationError.NoFont;
				flag2 = false;
			}
			else
			{
				uint uerror = 0U;
				bool res = this.Populate_Internal(str, font, color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, (int)verticalOverFlow, (int)horizontalOverflow, updateBounds, anchor, extents.x, extents.y, pivot.x, pivot.y, generateOutOfBounds, alignByGeometry, out uerror);
				error = (TextGenerationError)uerror;
				flag2 = res;
			}
			return flag2;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002864 File Offset: 0x00000A64
		[NativeThrows]
		private void GetVerticesInternal(object vertices)
		{
			IntPtr intPtr = TextGenerator.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TextGenerator.GetVerticesInternal_Injected(intPtr, vertices);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002888 File Offset: 0x00000A88
		[NativeThrows]
		private void GetCharactersInternal(object characters)
		{
			IntPtr intPtr = TextGenerator.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TextGenerator.GetCharactersInternal_Injected(intPtr, characters);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028AC File Offset: 0x00000AAC
		[NativeThrows]
		private void GetLinesInternal(object lines)
		{
			IntPtr intPtr = TextGenerator.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TextGenerator.GetLinesInternal_Injected(intPtr, lines);
		}

		// Token: 0x06000021 RID: 33
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rectExtents_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x06000022 RID: 34
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_characterCount_Injected(IntPtr _unity_self);

		// Token: 0x06000023 RID: 35
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_lineCount_Injected(IntPtr _unity_self);

		// Token: 0x06000024 RID: 36
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Populate_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper str, IntPtr font, [In] ref Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds, bool alignByGeometry, out uint error);

		// Token: 0x06000025 RID: 37
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVerticesInternal_Injected(IntPtr _unity_self, object vertices);

		// Token: 0x06000026 RID: 38
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCharactersInternal_Injected(IntPtr _unity_self, object characters);

		// Token: 0x06000027 RID: 39
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLinesInternal_Injected(IntPtr _unity_self, object lines);

		// Token: 0x0400001D RID: 29
		internal IntPtr m_Ptr;

		// Token: 0x0400001E RID: 30
		private string m_LastString;

		// Token: 0x0400001F RID: 31
		private TextGenerationSettings m_LastSettings;

		// Token: 0x04000020 RID: 32
		private bool m_HasGenerated;

		// Token: 0x04000021 RID: 33
		private TextGenerationError m_LastValid;

		// Token: 0x04000022 RID: 34
		private readonly List<UIVertex> m_Verts;

		// Token: 0x04000023 RID: 35
		private readonly List<UICharInfo> m_Characters;

		// Token: 0x04000024 RID: 36
		private readonly List<UILineInfo> m_Lines;

		// Token: 0x04000025 RID: 37
		private bool m_CachedVerts;

		// Token: 0x04000026 RID: 38
		private bool m_CachedCharacters;

		// Token: 0x04000027 RID: 39
		private bool m_CachedLines;

		// Token: 0x02000006 RID: 6
		internal static class BindingsMarshaller
		{
			// Token: 0x06000028 RID: 40 RVA: 0x000028CF File Offset: 0x00000ACF
			public static IntPtr ConvertToNative(TextGenerator textGenerator)
			{
				return textGenerator.m_Ptr;
			}
		}
	}
}
