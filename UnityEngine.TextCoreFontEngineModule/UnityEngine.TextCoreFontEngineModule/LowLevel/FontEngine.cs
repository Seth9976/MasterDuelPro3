using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/TextCoreFontEngine/Native/FontEngine.h")]
	public sealed class FontEngine
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002604 File Offset: 0x00000804
		public static FontEngineError LoadFontFace(string filePath)
		{
			return (FontEngineError)FontEngine.LoadFontFace_Internal(filePath);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000261C File Offset: 0x0000081C
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_Internal(string filePath)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = FontEngine.LoadFontFace_Internal_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002674 File Offset: 0x00000874
		public static FontEngineError LoadFontFace(string filePath, float pointSize, int faceIndex)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_And_FaceIndex_Internal(filePath, pointSize, faceIndex);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002690 File Offset: 0x00000890
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_With_Size_And_FaceIndex_Internal(string filePath, float pointSize, int faceIndex)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = FontEngine.LoadFontFace_With_Size_And_FaceIndex_Internal_Injected(ref managedSpanWrapper, pointSize, faceIndex);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026E8 File Offset: 0x000008E8
		public static FontEngineError LoadFontFace(Font font, float pointSize, int faceIndex)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal(font, pointSize, faceIndex);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002704 File Offset: 0x00000904
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private static int LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal(Font font, float pointSize, int faceIndex)
		{
			return FontEngine.LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal_Injected(Object.MarshalledUnityObject.Marshal<Font>(font), pointSize, faceIndex);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002720 File Offset: 0x00000920
		public static FontEngineError LoadFontFace(string familyName, string styleName, float pointSize)
		{
			return (FontEngineError)FontEngine.LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal(familyName, styleName, pointSize);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000273C File Offset: 0x0000093C
		[NativeMethod(Name = "TextCore::FontEngine::LoadFontFace", IsFreeFunction = true)]
		private unsafe static int LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal(string familyName, string styleName, float pointSize)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(familyName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = familyName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(styleName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = styleName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				num = FontEngine.LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, pointSize);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return num;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027C8 File Offset: 0x000009C8
		public static FontEngineError UnloadFontFace()
		{
			return (FontEngineError)FontEngine.UnloadFontFace_Internal();
		}

		// Token: 0x06000043 RID: 67
		[NativeMethod(Name = "TextCore::FontEngine::UnloadFontFace", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int UnloadFontFace_Internal();

		// Token: 0x06000044 RID: 68 RVA: 0x000027E0 File Offset: 0x000009E0
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static bool TryGetSystemFontReference(string familyName, string styleName, out FontReference fontRef)
		{
			return FontEngine.TryGetSystemFontReference_Internal(familyName, styleName, out fontRef);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027FC File Offset: 0x000009FC
		[NativeMethod(Name = "TextCore::FontEngine::TryGetSystemFontReference", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static bool TryGetSystemFontReference_Internal(string familyName, string styleName, out FontReference fontRef)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(familyName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = familyName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(styleName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = styleName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				flag = FontEngine.TryGetSystemFontReference_Internal_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, out fontRef);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return flag;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002888 File Offset: 0x00000A88
		public static FaceInfo GetFaceInfo()
		{
			FaceInfo faceInfo = default(FaceInfo);
			FontEngine.GetFaceInfo_Internal(ref faceInfo);
			return faceInfo;
		}

		// Token: 0x06000047 RID: 71
		[NativeMethod(Name = "TextCore::FontEngine::GetFaceInfo", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFaceInfo_Internal(ref FaceInfo faceInfo);

		// Token: 0x06000048 RID: 72 RVA: 0x000028AC File Offset: 0x00000AAC
		public static string[] GetFontFaces()
		{
			string[] faces = FontEngine.GetFontFaces_Internal();
			bool flag = faces != null && faces.Length == 0;
			string[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				array = faces;
			}
			return array;
		}

		// Token: 0x06000049 RID: 73
		[NativeMethod(Name = "TextCore::FontEngine::GetFontFaces", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetFontFaces_Internal();

		// Token: 0x0600004A RID: 74
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[NativeMethod(Name = "TextCore::FontEngine::GetVariantGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetVariantGlyphIndex(uint unicode, uint variantSelectorUnicode);

		// Token: 0x0600004B RID: 75
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphIndex", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetGlyphIndex(uint unicode);

		// Token: 0x0600004C RID: 76 RVA: 0x000028D8 File Offset: 0x00000AD8
		public static bool TryGetGlyphWithUnicodeValue(uint unicode, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphStruct = default(GlyphMarshallingStruct);
			bool flag = FontEngine.TryGetGlyphWithUnicodeValue_Internal(unicode, flags, ref glyphStruct);
			bool flag2;
			if (flag)
			{
				glyph = new Glyph(glyphStruct);
				flag2 = true;
			}
			else
			{
				glyph = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600004D RID: 77
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithUnicodeValue", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetGlyphWithUnicodeValue_Internal(uint unicode, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		// Token: 0x0600004E RID: 78 RVA: 0x00002910 File Offset: 0x00000B10
		public static bool TryGetGlyphWithIndexValue(uint glyphIndex, GlyphLoadFlags flags, out Glyph glyph)
		{
			GlyphMarshallingStruct glyphStruct = default(GlyphMarshallingStruct);
			bool flag = FontEngine.TryGetGlyphWithIndexValue_Internal(glyphIndex, flags, ref glyphStruct);
			bool flag2;
			if (flag)
			{
				glyph = new Glyph(glyphStruct);
				flag2 = true;
			}
			else
			{
				glyph = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600004F RID: 79
		[NativeMethod(Name = "TextCore::FontEngine::TryGetGlyphWithIndexValue", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetGlyphWithIndexValue_Internal(uint glyphIndex, GlyphLoadFlags loadFlags, ref GlyphMarshallingStruct glyphStruct);

		// Token: 0x06000050 RID: 80
		[NativeMethod(Name = "TextCore::FontEngine::SetTextureUploadMode", IsThreadSafe = true, IsFreeFunction = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTextureUploadMode(bool shouldUploadImmediately);

		// Token: 0x06000051 RID: 81 RVA: 0x00002948 File Offset: 0x00000B48
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static bool TryAddGlyphToTexture(uint glyphIndex, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph glyph)
		{
			int freeGlyphRectCount = freeGlyphRects.Count;
			int usedGlyphRectCount = usedGlyphRects.Count;
			int totalGlyphRects = freeGlyphRectCount + usedGlyphRectCount;
			bool flag = FontEngine.s_FreeGlyphRects.Length < totalGlyphRects || FontEngine.s_UsedGlyphRects.Length < totalGlyphRects;
			if (flag)
			{
				int newSize = Mathf.NextPowerOfTwo(totalGlyphRects + 1);
				FontEngine.s_FreeGlyphRects = new GlyphRect[newSize];
				FontEngine.s_UsedGlyphRects = new GlyphRect[newSize];
			}
			int glyphRectCount = Mathf.Max(freeGlyphRectCount, usedGlyphRectCount);
			for (int i = 0; i < glyphRectCount; i++)
			{
				bool flag2 = i < freeGlyphRectCount;
				if (flag2)
				{
					FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
				}
				bool flag3 = i < usedGlyphRectCount;
				if (flag3)
				{
					FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
				}
			}
			GlyphMarshallingStruct glyphStruct;
			bool flag4 = FontEngine.TryAddGlyphToTexture_Internal(glyphIndex, padding, packingMode, FontEngine.s_FreeGlyphRects, ref freeGlyphRectCount, FontEngine.s_UsedGlyphRects, ref usedGlyphRectCount, renderMode, texture, out glyphStruct);
			bool flag7;
			if (flag4)
			{
				glyph = new Glyph(glyphStruct);
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				glyphRectCount = Mathf.Max(freeGlyphRectCount, usedGlyphRectCount);
				for (int j = 0; j < glyphRectCount; j++)
				{
					bool flag5 = j < freeGlyphRectCount;
					if (flag5)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag6 = j < usedGlyphRectCount;
					if (flag6)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				flag7 = true;
			}
			else
			{
				glyph = null;
				flag7 = false;
			}
			return flag7;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002AB4 File Offset: 0x00000CB4
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static bool TryAddGlyphToTexture_Internal(uint glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, out GlyphMarshallingStruct glyph)
		{
			bool flag;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (freeGlyphRects != null)
				{
					fixed (GlyphRect[] array = freeGlyphRects)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				BlittableArrayWrapper blittableArrayWrapper2;
				if (usedGlyphRects != null)
				{
					fixed (GlyphRect[] array2 = usedGlyphRects)
					{
						if (array2.Length != 0)
						{
							blittableArrayWrapper2 = new BlittableArrayWrapper((void*)(&array2[0]), array2.Length);
						}
					}
				}
				flag = FontEngine.TryAddGlyphToTexture_Internal_Injected(glyphIndex, padding, packingMode, out blittableArrayWrapper, ref freeGlyphRectCount, out blittableArrayWrapper2, ref usedGlyphRectCount, renderMode, Object.MarshalledUnityObject.Marshal<Texture2D>(texture), out glyph);
			}
			finally
			{
				GlyphRect[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<GlyphRect>(ref array);
				GlyphRect[] array2;
				BlittableArrayWrapper blittableArrayWrapper2;
				blittableArrayWrapper2.Unmarshal<GlyphRect>(ref array2);
			}
			return flag;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B44 File Offset: 0x00000D44
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static bool TryAddGlyphsToTexture(List<uint> glyphIndexes, int padding, GlyphPackingMode packingMode, List<GlyphRect> freeGlyphRects, List<GlyphRect> usedGlyphRects, GlyphRenderMode renderMode, Texture2D texture, out Glyph[] glyphs)
		{
			glyphs = null;
			bool flag = glyphIndexes == null || glyphIndexes.Count == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int glyphCount = glyphIndexes.Count;
				bool flag3 = FontEngine.s_GlyphIndexes_MarshallingArray_A == null || FontEngine.s_GlyphIndexes_MarshallingArray_A.Length < glyphCount;
				if (flag3)
				{
					FontEngine.s_GlyphIndexes_MarshallingArray_A = new uint[Mathf.NextPowerOfTwo(glyphCount + 1)];
				}
				int freeGlyphRectCount = freeGlyphRects.Count;
				int usedGlyphRectCount = usedGlyphRects.Count;
				int totalGlyphRects = freeGlyphRectCount + usedGlyphRectCount + glyphCount;
				bool flag4 = FontEngine.s_FreeGlyphRects.Length < totalGlyphRects || FontEngine.s_UsedGlyphRects.Length < totalGlyphRects;
				if (flag4)
				{
					int newSize = Mathf.NextPowerOfTwo(totalGlyphRects + 1);
					FontEngine.s_FreeGlyphRects = new GlyphRect[newSize];
					FontEngine.s_UsedGlyphRects = new GlyphRect[newSize];
				}
				bool flag5 = FontEngine.s_GlyphMarshallingStruct_OUT.Length < glyphCount;
				if (flag5)
				{
					int newSize2 = Mathf.NextPowerOfTwo(glyphCount + 1);
					FontEngine.s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[newSize2];
				}
				int glyphRectCount = FontEngineUtilities.MaxValue(freeGlyphRectCount, usedGlyphRectCount, glyphCount);
				for (int i = 0; i < glyphRectCount; i++)
				{
					bool flag6 = i < glyphCount;
					if (flag6)
					{
						FontEngine.s_GlyphIndexes_MarshallingArray_A[i] = glyphIndexes[i];
					}
					bool flag7 = i < freeGlyphRectCount;
					if (flag7)
					{
						FontEngine.s_FreeGlyphRects[i] = freeGlyphRects[i];
					}
					bool flag8 = i < usedGlyphRectCount;
					if (flag8)
					{
						FontEngine.s_UsedGlyphRects[i] = usedGlyphRects[i];
					}
				}
				bool allGlyphsAdded = FontEngine.TryAddGlyphsToTexture_Internal(FontEngine.s_GlyphIndexes_MarshallingArray_A, padding, packingMode, FontEngine.s_FreeGlyphRects, ref freeGlyphRectCount, FontEngine.s_UsedGlyphRects, ref usedGlyphRectCount, renderMode, texture, FontEngine.s_GlyphMarshallingStruct_OUT, ref glyphCount);
				bool flag9 = FontEngine.s_Glyphs == null || FontEngine.s_Glyphs.Length <= glyphCount;
				if (flag9)
				{
					FontEngine.s_Glyphs = new Glyph[Mathf.NextPowerOfTwo(glyphCount + 1)];
				}
				FontEngine.s_Glyphs[glyphCount] = null;
				freeGlyphRects.Clear();
				usedGlyphRects.Clear();
				glyphRectCount = FontEngineUtilities.MaxValue(freeGlyphRectCount, usedGlyphRectCount, glyphCount);
				for (int j = 0; j < glyphRectCount; j++)
				{
					bool flag10 = j < glyphCount;
					if (flag10)
					{
						FontEngine.s_Glyphs[j] = new Glyph(FontEngine.s_GlyphMarshallingStruct_OUT[j]);
					}
					bool flag11 = j < freeGlyphRectCount;
					if (flag11)
					{
						freeGlyphRects.Add(FontEngine.s_FreeGlyphRects[j]);
					}
					bool flag12 = j < usedGlyphRectCount;
					if (flag12)
					{
						usedGlyphRects.Add(FontEngine.s_UsedGlyphRects[j]);
					}
				}
				glyphs = FontEngine.s_Glyphs;
				flag2 = allGlyphsAdded;
			}
			return flag2;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002DA4 File Offset: 0x00000FA4
		[NativeMethod(Name = "TextCore::FontEngine::TryAddGlyphsToTexture", IsThreadSafe = true, IsFreeFunction = true)]
		private unsafe static bool TryAddGlyphsToTexture_Internal(uint[] glyphIndex, int padding, GlyphPackingMode packingMode, [Out] GlyphRect[] freeGlyphRects, ref int freeGlyphRectCount, [Out] GlyphRect[] usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, Texture2D texture, [Out] GlyphMarshallingStruct[] glyphs, ref int glyphCount)
		{
			bool flag;
			try
			{
				Span<uint> span = new Span<uint>(glyphIndex);
				fixed (uint* ptr = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
					BlittableArrayWrapper blittableArrayWrapper;
					if (freeGlyphRects != null)
					{
						fixed (GlyphRect[] array = freeGlyphRects)
						{
							if (array.Length != 0)
							{
								blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
							}
						}
					}
					BlittableArrayWrapper blittableArrayWrapper2;
					if (usedGlyphRects != null)
					{
						fixed (GlyphRect[] array2 = usedGlyphRects)
						{
							if (array2.Length != 0)
							{
								blittableArrayWrapper2 = new BlittableArrayWrapper((void*)(&array2[0]), array2.Length);
							}
						}
					}
					IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Texture2D>(texture);
					BlittableArrayWrapper blittableArrayWrapper3;
					if (glyphs != null)
					{
						fixed (GlyphMarshallingStruct[] array3 = glyphs)
						{
							if (array3.Length != 0)
							{
								blittableArrayWrapper3 = new BlittableArrayWrapper((void*)(&array3[0]), array3.Length);
							}
						}
					}
					flag = FontEngine.TryAddGlyphsToTexture_Internal_Injected(ref managedSpanWrapper, padding, packingMode, out blittableArrayWrapper, ref freeGlyphRectCount, out blittableArrayWrapper2, ref usedGlyphRectCount, renderMode, intPtr, out blittableArrayWrapper3, ref glyphCount);
				}
			}
			finally
			{
				uint* ptr = null;
				GlyphRect[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<GlyphRect>(ref array);
				GlyphRect[] array2;
				BlittableArrayWrapper blittableArrayWrapper2;
				blittableArrayWrapper2.Unmarshal<GlyphRect>(ref array2);
				GlyphMarshallingStruct[] array3;
				BlittableArrayWrapper blittableArrayWrapper3;
				blittableArrayWrapper3.Unmarshal<GlyphMarshallingStruct>(ref array3);
			}
			return flag;
		}

		// Token: 0x06000055 RID: 85
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllLigatureSubstitutionRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern LigatureSubstitutionRecord[] GetAllLigatureSubstitutionRecords();

		// Token: 0x06000056 RID: 86 RVA: 0x00002E94 File Offset: 0x00001094
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(uint glyphIndex)
		{
			FontEngine.GlyphIndexToMarshallingArray(glyphIndex, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetLigatureSubstitutionRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EBC File Offset: 0x000010BC
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetLigatureSubstitutionRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002EE8 File Offset: 0x000010E8
		private static LigatureSubstitutionRecord[] GetLigatureSubstitutionRecords(uint[] glyphIndexes)
		{
			int recordCount;
			FontEngine.PopulateLigatureSubstitutionRecordMarshallingArray(glyphIndexes, out recordCount);
			bool flag = recordCount == 0;
			LigatureSubstitutionRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<LigatureSubstitutionRecord>(ref FontEngine.s_LigatureSubstitutionRecords_MarshallingArray, recordCount);
				FontEngine.GetLigatureSubstitutionRecordsFromMarshallingArray(FontEngine.s_LigatureSubstitutionRecords_MarshallingArray);
				FontEngine.s_LigatureSubstitutionRecords_MarshallingArray[recordCount] = default(LigatureSubstitutionRecord);
				array = FontEngine.s_LigatureSubstitutionRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F3C File Offset: 0x0000113C
		[NativeMethod(Name = "TextCore::FontEngine::PopulateLigatureSubstitutionRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateLigatureSubstitutionRecordMarshallingArray(uint[] glyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateLigatureSubstitutionRecordMarshallingArray_Injected(ref managedSpanWrapper, out recordCount);
			}
			return num;
		}

		// Token: 0x0600005A RID: 90
		[NativeMethod(Name = "TextCore::FontEngine::GetLigatureSubstitutionRecordsFromMarshallingArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLigatureSubstitutionRecordsFromMarshallingArray([Out] LigatureSubstitutionRecord[] ligatureSubstitutionRecords);

		// Token: 0x0600005B RID: 91 RVA: 0x00002F78 File Offset: 0x00001178
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentTable(uint[] glyphIndexes)
		{
			int recordCount;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(glyphIndexes, out recordCount);
			bool flag = recordCount == 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, recordCount);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002FD4 File Offset: 0x000011D4
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArrayFromKernTable", IsFreeFunction = true)]
		private unsafe static int PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(uint[] glyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulatePairAdjustmentRecordMarshallingArray_from_KernTable_Injected(ref managedSpanWrapper, out recordCount);
			}
			return num;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003010 File Offset: 0x00001210
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllPairAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static GlyphPairAdjustmentRecord[] GetAllPairAdjustmentRecords()
		{
			GlyphPairAdjustmentRecord[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetAllPairAdjustmentRecords_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				GlyphPairAdjustmentRecord[] array;
				blittableArrayWrapper.Unmarshal<GlyphPairAdjustmentRecord>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003044 File Offset: 0x00001244
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetPairAdjustmentRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003070 File Offset: 0x00001270
		private static GlyphPairAdjustmentRecord[] GetPairAdjustmentRecords(uint[] glyphIndexes)
		{
			int recordCount;
			FontEngine.PopulatePairAdjustmentRecordMarshallingArray(glyphIndexes, out recordCount);
			bool flag = recordCount == 0;
			GlyphPairAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<GlyphPairAdjustmentRecord>(ref FontEngine.s_PairAdjustmentRecords_MarshallingArray, recordCount);
				FontEngine.GetPairAdjustmentRecordsFromMarshallingArray(FontEngine.s_PairAdjustmentRecords_MarshallingArray);
				FontEngine.s_PairAdjustmentRecords_MarshallingArray[recordCount] = default(GlyphPairAdjustmentRecord);
				array = FontEngine.s_PairAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000030CC File Offset: 0x000012CC
		[NativeMethod(Name = "TextCore::FontEngine::PopulatePairAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulatePairAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulatePairAdjustmentRecordMarshallingArray_Injected(ref managedSpanWrapper, out recordCount);
			}
			return num;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003108 File Offset: 0x00001308
		[NativeMethod(Name = "TextCore::FontEngine::GetGlyphPairAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private unsafe static int GetPairAdjustmentRecordsFromMarshallingArray(Span<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords)
		{
			Span<GlyphPairAdjustmentRecord> span = glyphPairAdjustmentRecords;
			int pairAdjustmentRecordsFromMarshallingArray_Injected;
			fixed (GlyphPairAdjustmentRecord* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				pairAdjustmentRecordsFromMarshallingArray_Injected = FontEngine.GetPairAdjustmentRecordsFromMarshallingArray_Injected(ref managedSpanWrapper);
			}
			return pairAdjustmentRecordsFromMarshallingArray_Injected;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000313C File Offset: 0x0000133C
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMarkToBaseAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static MarkToBaseAdjustmentRecord[] GetAllMarkToBaseAdjustmentRecords()
		{
			MarkToBaseAdjustmentRecord[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetAllMarkToBaseAdjustmentRecords_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				MarkToBaseAdjustmentRecord[] array;
				blittableArrayWrapper.Unmarshal<MarkToBaseAdjustmentRecord>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003170 File Offset: 0x00001370
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMarkToBaseAdjustmentRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000319C File Offset: 0x0000139C
		private static MarkToBaseAdjustmentRecord[] GetMarkToBaseAdjustmentRecords(uint[] glyphIndexes)
		{
			int recordCount;
			FontEngine.PopulateMarkToBaseAdjustmentRecordMarshallingArray(glyphIndexes, out recordCount);
			bool flag = recordCount == 0;
			MarkToBaseAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MarkToBaseAdjustmentRecord>(ref FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray, recordCount);
				FontEngine.GetMarkToBaseAdjustmentRecordsFromMarshallingArray(FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray);
				FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray[recordCount] = default(MarkToBaseAdjustmentRecord);
				array = FontEngine.s_MarkToBaseAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000031F8 File Offset: 0x000013F8
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToBaseAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateMarkToBaseAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateMarkToBaseAdjustmentRecordMarshallingArray_Injected(ref managedSpanWrapper, out recordCount);
			}
			return num;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003234 File Offset: 0x00001434
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToBaseAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private unsafe static int GetMarkToBaseAdjustmentRecordsFromMarshallingArray(Span<MarkToBaseAdjustmentRecord> adjustmentRecords)
		{
			Span<MarkToBaseAdjustmentRecord> span = adjustmentRecords;
			int markToBaseAdjustmentRecordsFromMarshallingArray_Injected;
			fixed (MarkToBaseAdjustmentRecord* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				markToBaseAdjustmentRecordsFromMarshallingArray_Injected = FontEngine.GetMarkToBaseAdjustmentRecordsFromMarshallingArray_Injected(ref managedSpanWrapper);
			}
			return markToBaseAdjustmentRecordsFromMarshallingArray_Injected;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003268 File Offset: 0x00001468
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[NativeMethod(Name = "TextCore::FontEngine::GetAllMarkToMarkAdjustmentRecords", IsThreadSafe = true, IsFreeFunction = true)]
		internal static MarkToMarkAdjustmentRecord[] GetAllMarkToMarkAdjustmentRecords()
		{
			MarkToMarkAdjustmentRecord[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				FontEngine.GetAllMarkToMarkAdjustmentRecords_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				MarkToMarkAdjustmentRecord[] array;
				blittableArrayWrapper.Unmarshal<MarkToMarkAdjustmentRecord>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000329C File Offset: 0x0000149C
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(List<uint> glyphIndexes)
		{
			FontEngine.GenericListToMarshallingArray<uint>(ref glyphIndexes, ref FontEngine.s_GlyphIndexes_MarshallingArray_A);
			return FontEngine.GetMarkToMarkAdjustmentRecords(FontEngine.s_GlyphIndexes_MarshallingArray_A);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000032C8 File Offset: 0x000014C8
		private static MarkToMarkAdjustmentRecord[] GetMarkToMarkAdjustmentRecords(uint[] glyphIndexes)
		{
			int recordCount;
			FontEngine.PopulateMarkToMarkAdjustmentRecordMarshallingArray(FontEngine.s_GlyphIndexes_MarshallingArray_A, out recordCount);
			bool flag = recordCount == 0;
			MarkToMarkAdjustmentRecord[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				FontEngine.SetMarshallingArraySize<MarkToMarkAdjustmentRecord>(ref FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray, recordCount);
				FontEngine.GetMarkToMarkAdjustmentRecordsFromMarshallingArray(FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray);
				FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray[recordCount] = default(MarkToMarkAdjustmentRecord);
				array = FontEngine.s_MarkToMarkAdjustmentRecords_MarshallingArray;
			}
			return array;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003328 File Offset: 0x00001528
		[NativeMethod(Name = "TextCore::FontEngine::PopulateMarkToMarkAdjustmentRecordMarshallingArray", IsFreeFunction = true)]
		private unsafe static int PopulateMarkToMarkAdjustmentRecordMarshallingArray(uint[] glyphIndexes, out int recordCount)
		{
			Span<uint> span = new Span<uint>(glyphIndexes);
			int num;
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = FontEngine.PopulateMarkToMarkAdjustmentRecordMarshallingArray_Injected(ref managedSpanWrapper, out recordCount);
			}
			return num;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003364 File Offset: 0x00001564
		[NativeMethod(Name = "TextCore::FontEngine::GetMarkToMarkAdjustmentRecordsFromMarshallingArray", IsFreeFunction = true)]
		private unsafe static int GetMarkToMarkAdjustmentRecordsFromMarshallingArray(Span<MarkToMarkAdjustmentRecord> adjustmentRecords)
		{
			Span<MarkToMarkAdjustmentRecord> span = adjustmentRecords;
			int markToMarkAdjustmentRecordsFromMarshallingArray_Injected;
			fixed (MarkToMarkAdjustmentRecord* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				markToMarkAdjustmentRecordsFromMarshallingArray_Injected = FontEngine.GetMarkToMarkAdjustmentRecordsFromMarshallingArray_Injected(ref managedSpanWrapper);
			}
			return markToMarkAdjustmentRecordsFromMarshallingArray_Injected;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003398 File Offset: 0x00001598
		private static void GlyphIndexToMarshallingArray(uint glyphIndex, ref uint[] dstArray)
		{
			bool flag = dstArray == null || dstArray.Length == 1;
			if (flag)
			{
				dstArray = new uint[8];
			}
			dstArray[0] = glyphIndex;
			dstArray[1] = 0U;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000033CC File Offset: 0x000015CC
		private static void GenericListToMarshallingArray<T>(ref List<T> srcList, ref T[] dstArray)
		{
			int count = srcList.Count;
			bool flag = dstArray == null || dstArray.Length <= count;
			if (flag)
			{
				int size = Mathf.NextPowerOfTwo(count + 1);
				bool flag2 = dstArray == null;
				if (flag2)
				{
					dstArray = new T[size];
				}
				else
				{
					Array.Resize<T>(ref dstArray, size);
				}
			}
			for (int i = 0; i < count; i++)
			{
				dstArray[i] = srcList[i];
			}
			dstArray[count] = default(T);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003458 File Offset: 0x00001658
		private static void SetMarshallingArraySize<T>(ref T[] marshallingArray, int recordCount)
		{
			bool flag = marshallingArray == null || marshallingArray.Length <= recordCount;
			if (flag)
			{
				int size = Mathf.NextPowerOfTwo(recordCount + 1);
				bool flag2 = marshallingArray == null;
				if (flag2)
				{
					marshallingArray = new T[size];
				}
				else
				{
					Array.Resize<T>(ref marshallingArray, size);
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000034A4 File Offset: 0x000016A4
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		[NativeMethod(Name = "TextCore::FontEngine::ResetAtlasTexture", IsFreeFunction = true)]
		internal static void ResetAtlasTexture(Texture2D texture)
		{
			FontEngine.ResetAtlasTexture_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(texture));
		}

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_Internal_Injected(ref ManagedSpanWrapper filePath);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_And_FaceIndex_Internal_Injected(ref ManagedSpanWrapper filePath, float pointSize, int faceIndex);

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal_Injected(IntPtr font, float pointSize, int faceIndex);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal_Injected(ref ManagedSpanWrapper familyName, ref ManagedSpanWrapper styleName, float pointSize);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetSystemFontReference_Internal_Injected(ref ManagedSpanWrapper familyName, ref ManagedSpanWrapper styleName, out FontReference fontRef);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphToTexture_Internal_Injected(uint glyphIndex, int padding, GlyphPackingMode packingMode, out BlittableArrayWrapper freeGlyphRects, ref int freeGlyphRectCount, out BlittableArrayWrapper usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, IntPtr texture, out GlyphMarshallingStruct glyph);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryAddGlyphsToTexture_Internal_Injected(ref ManagedSpanWrapper glyphIndex, int padding, GlyphPackingMode packingMode, out BlittableArrayWrapper freeGlyphRects, ref int freeGlyphRectCount, out BlittableArrayWrapper usedGlyphRects, ref int usedGlyphRectCount, GlyphRenderMode renderMode, IntPtr texture, out BlittableArrayWrapper glyphs, ref int glyphCount);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateLigatureSubstitutionRecordMarshallingArray_Injected(ref ManagedSpanWrapper glyphIndexes, out int recordCount);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_from_KernTable_Injected(ref ManagedSpanWrapper glyphIndexes, out int recordCount);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAllPairAdjustmentRecords_Injected(out BlittableArrayWrapper ret);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulatePairAdjustmentRecordMarshallingArray_Injected(ref ManagedSpanWrapper glyphIndexes, out int recordCount);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPairAdjustmentRecordsFromMarshallingArray_Injected(ref ManagedSpanWrapper glyphPairAdjustmentRecords);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAllMarkToBaseAdjustmentRecords_Injected(out BlittableArrayWrapper ret);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMarkToBaseAdjustmentRecordMarshallingArray_Injected(ref ManagedSpanWrapper glyphIndexes, out int recordCount);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMarkToBaseAdjustmentRecordsFromMarshallingArray_Injected(ref ManagedSpanWrapper adjustmentRecords);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAllMarkToMarkAdjustmentRecords_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopulateMarkToMarkAdjustmentRecordMarshallingArray_Injected(ref ManagedSpanWrapper glyphIndexes, out int recordCount);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMarkToMarkAdjustmentRecordsFromMarshallingArray_Injected(ref ManagedSpanWrapper adjustmentRecords);

		// Token: 0x06000083 RID: 131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetAtlasTexture_Injected(IntPtr texture);

		// Token: 0x0400005F RID: 95
		private static Glyph[] s_Glyphs = new Glyph[16];

		// Token: 0x04000060 RID: 96
		private static uint[] s_GlyphIndexes_MarshallingArray_A;

		// Token: 0x04000061 RID: 97
		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_IN = new GlyphMarshallingStruct[16];

		// Token: 0x04000062 RID: 98
		private static GlyphMarshallingStruct[] s_GlyphMarshallingStruct_OUT = new GlyphMarshallingStruct[16];

		// Token: 0x04000063 RID: 99
		private static GlyphRect[] s_FreeGlyphRects = new GlyphRect[16];

		// Token: 0x04000064 RID: 100
		private static GlyphRect[] s_UsedGlyphRects = new GlyphRect[16];

		// Token: 0x04000065 RID: 101
		private static LigatureSubstitutionRecord[] s_LigatureSubstitutionRecords_MarshallingArray;

		// Token: 0x04000066 RID: 102
		private static GlyphPairAdjustmentRecord[] s_PairAdjustmentRecords_MarshallingArray;

		// Token: 0x04000067 RID: 103
		private static MarkToBaseAdjustmentRecord[] s_MarkToBaseAdjustmentRecords_MarshallingArray;

		// Token: 0x04000068 RID: 104
		private static MarkToMarkAdjustmentRecord[] s_MarkToMarkAdjustmentRecords_MarshallingArray;

		// Token: 0x04000069 RID: 105
		private static Dictionary<uint, Glyph> s_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
	}
}
