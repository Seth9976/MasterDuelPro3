using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000054 RID: 84
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextLib.h")]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "Unity.UIElements.PlayModeTests" })]
	[StructLayout(LayoutKind.Sequential)]
	internal class TextLib
	{
		// Token: 0x06000224 RID: 548 RVA: 0x0002C19D File Offset: 0x0002A39D
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal TextLib()
		{
			this.m_Ptr = TextLib.GetInstance();
		}

		// Token: 0x06000225 RID: 549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetInstance();

		// Token: 0x06000226 RID: 550 RVA: 0x0002C1B4 File Offset: 0x0002A3B4
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal NativeTextInfo GenerateText(NativeTextGenerationSettings settings, IntPtr textGenerationInfo)
		{
			Debug.Assert((settings.fontStyle & FontStyles.Bold) == FontStyles.Normal);
			NativeTextInfo textInfo = this.GenerateTextInternal(settings, textGenerationInfo);
			Span<ATGMeshInfo> span = textInfo.meshInfos.AsSpan<ATGMeshInfo>();
			for (int i = 0; i < span.Length; i++)
			{
				ref ATGMeshInfo meshInfo = ref span[i];
				FontAsset fa = FontAsset.GetFontAssetByID(meshInfo.fontAssetId);
				meshInfo.fontAsset = fa;
				Span<NativeTextElementInfo> span2 = meshInfo.textElementInfos.AsSpan<NativeTextElementInfo>();
				for (int j = 0; j < span2.Length; j++)
				{
					ref NativeTextElementInfo textElementInfo = ref span2[j];
					int glyphID = textElementInfo.glyphID;
					Glyph glyph;
					bool success = fa.TryAddGlyphInternal((uint)glyphID, out glyph);
					bool flag = !success;
					if (!flag)
					{
						GlyphRect glyphRect = glyph.glyphRect;
						float padding = (float)settings.vertexPadding / 64f;
						Vector2 bottomLeftUV;
						bottomLeftUV.x = ((float)glyphRect.x - padding) / (float)fa.atlasWidth;
						bottomLeftUV.y = ((float)glyphRect.y - padding) / (float)fa.atlasHeight;
						Vector2 topLeftUV;
						topLeftUV.x = bottomLeftUV.x;
						topLeftUV.y = ((float)(glyphRect.y + glyphRect.height) + padding) / (float)fa.atlasHeight;
						Vector2 topRightUV;
						topRightUV.x = ((float)(glyphRect.x + glyphRect.width) + padding) / (float)fa.atlasWidth;
						topRightUV.y = topLeftUV.y;
						Vector2 bottomRightUV;
						bottomRightUV.x = topRightUV.x;
						bottomRightUV.y = bottomLeftUV.y;
						textElementInfo.bottomLeft.uv0 = topRightUV * textElementInfo.bottomLeft.uv0 + bottomLeftUV * (new Vector2(1f, 1f) - textElementInfo.bottomLeft.uv0);
						textElementInfo.topLeft.uv0 = topRightUV * textElementInfo.topLeft.uv0 + bottomLeftUV * (new Vector2(1f, 1f) - textElementInfo.topLeft.uv0);
						textElementInfo.topRight.uv0 = topRightUV * textElementInfo.topRight.uv0 + bottomLeftUV * (new Vector2(1f, 1f) - textElementInfo.topRight.uv0);
						textElementInfo.bottomRight.uv0 = topRightUV * textElementInfo.bottomRight.uv0 + bottomLeftUV * (new Vector2(1f, 1f) - textElementInfo.bottomRight.uv0);
					}
				}
			}
			return textInfo;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0002C484 File Offset: 0x0002A684
		[NativeMethod(Name = "TextLib::GenerateTextMesh")]
		private NativeTextInfo GenerateTextInternal(NativeTextGenerationSettings settings, IntPtr textGenerationInfo)
		{
			IntPtr intPtr = TextLib.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NativeTextInfo nativeTextInfo;
			TextLib.GenerateTextInternal_Injected(intPtr, ref settings, textGenerationInfo, out nativeTextInfo);
			return nativeTextInfo;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0002C4AC File Offset: 0x0002A6AC
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[NativeMethod(Name = "TextLib::MeasureText")]
		internal Vector2 MeasureText(NativeTextGenerationSettings settings, IntPtr textGenerationInfo)
		{
			IntPtr intPtr = TextLib.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			TextLib.MeasureText_Injected(intPtr, ref settings, textGenerationInfo, out vector);
			return vector;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0002C4D4 File Offset: 0x0002A6D4
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[NativeMethod(Name = "TextLib::FindIntersectingLink")]
		internal static int FindIntersectingLink(Vector2 point, IntPtr textGenerationInfo)
		{
			return TextLib.FindIntersectingLink_Injected(ref point, textGenerationInfo);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0002C4EC File Offset: 0x0002A6EC
		private static TextAsset GetICUAsset()
		{
			foreach (TextAsset t in Resources.FindObjectsOfTypeAll<TextAsset>())
			{
				bool flag = t.name == "icudt73l";
				if (flag)
				{
					return t;
				}
			}
			return null;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0002C538 File Offset: 0x0002A738
		[RequiredByNativeCode]
		internal static int LoadAndCountICUdata()
		{
			TextAsset icuasset = TextLib.GetICUAsset();
			TextLib.s_ICUData = ((icuasset != null) ? icuasset.bytes : null);
			byte[] array = TextLib.s_ICUData;
			return (array != null) ? array.Length : 0;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0002C570 File Offset: 0x0002A770
		[VisibleToOtherModules(new string[] { "Unity.UIElements.PlayModeTests" })]
		[RequiredByNativeCode]
		internal static bool GetICUdata(Span<byte> data, int maxSize)
		{
			bool flag = TextLib.s_ICUData == null;
			if (flag)
			{
				TextLib.LoadAndCountICUdata();
			}
			Debug.Assert(TextLib.s_ICUData != null, "s_ICUData not initialized");
			Debug.Assert(maxSize == TextLib.s_ICUData.Length);
			TextLib.s_ICUData.CopyTo(data);
			TextLib.s_ICUData = null;
			return true;
		}

		// Token: 0x0600022D RID: 557
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateTextInternal_Injected(IntPtr _unity_self, [In] ref NativeTextGenerationSettings settings, IntPtr textGenerationInfo, out NativeTextInfo ret);

		// Token: 0x0600022E RID: 558
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MeasureText_Injected(IntPtr _unity_self, [In] ref NativeTextGenerationSettings settings, IntPtr textGenerationInfo, out Vector2 ret);

		// Token: 0x0600022F RID: 559
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FindIntersectingLink_Injected([In] ref Vector2 point, IntPtr textGenerationInfo);

		// Token: 0x04000350 RID: 848
		private readonly IntPtr m_Ptr;

		// Token: 0x04000351 RID: 849
		private static byte[] s_ICUData;

		// Token: 0x02000055 RID: 85
		internal static class BindingsMarshaller
		{
			// Token: 0x06000230 RID: 560 RVA: 0x0002C5CA File Offset: 0x0002A7CA
			public static IntPtr ConvertToNative(TextLib textLib)
			{
				return textLib.m_Ptr;
			}
		}
	}
}
