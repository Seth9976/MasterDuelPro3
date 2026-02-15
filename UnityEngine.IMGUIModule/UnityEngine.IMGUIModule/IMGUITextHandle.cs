using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	internal class IMGUITextHandle : TextHandle
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00006877 File Offset: 0x00004A77
		internal static void EmptyManagedCache()
		{
			IMGUITextHandle.textHandles.Clear();
			IMGUITextHandle.textHandlesTuple.Clear();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006890 File Offset: 0x00004A90
		internal static IMGUITextHandle GetTextHandle(GUIStyle style, Rect position, string content, Color32 textColor)
		{
			bool isCached = false;
			IMGUITextHandle.ConvertGUIStyleToGenerationSettings(TextHandle.settings, style, textColor, content, position);
			return IMGUITextHandle.GetTextHandle(TextHandle.settings, false, ref isCached);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000068C8 File Offset: 0x00004AC8
		internal static IMGUITextHandle GetTextHandle(GUIStyle style, Rect position, string content, Color32 textColor, ref bool isCached)
		{
			IMGUITextHandle.ConvertGUIStyleToGenerationSettings(TextHandle.settings, style, textColor, content, position);
			return IMGUITextHandle.GetTextHandle(TextHandle.settings, true, ref isCached);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000068FC File Offset: 0x00004AFC
		private static bool ShouldCleanup(float currentTime, float lastTime, float cleanupThreshold)
		{
			float timeSinceLastCleanup = currentTime - lastTime;
			return timeSinceLastCleanup > cleanupThreshold || timeSinceLastCleanup < 0f;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006924 File Offset: 0x00004B24
		private static void ClearUnusedTextHandles()
		{
			float currentTime = Time.realtimeSinceStartup;
			while (IMGUITextHandle.textHandlesTuple.Count > 0)
			{
				IMGUITextHandle.TextHandleTuple tuple = IMGUITextHandle.textHandlesTuple.First<IMGUITextHandle.TextHandleTuple>();
				bool flag = IMGUITextHandle.ShouldCleanup(currentTime, tuple.lastTimeUsed, 5f);
				if (!flag)
				{
					break;
				}
				GUIStyle.Internal_DestroyTextGenerator(tuple.hashCode);
				IMGUITextHandle textHandleCached;
				bool flag2 = IMGUITextHandle.textHandles.TryGetValue(tuple.hashCode, out textHandleCached);
				if (flag2)
				{
					textHandleCached.RemoveTextInfoFromPermanentCache();
				}
				IMGUITextHandle.textHandles.Remove(tuple.hashCode);
				IMGUITextHandle.textHandlesTuple.RemoveFirst();
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000069C4 File Offset: 0x00004BC4
		private static IMGUITextHandle GetTextHandle(TextGenerationSettings settings, bool isCalledFromNative, ref bool isCached)
		{
			isCached = false;
			float currentTime = Time.realtimeSinceStartup;
			bool flag = IMGUITextHandle.ShouldCleanup(currentTime, IMGUITextHandle.lastCleanupTime, 30f) || IMGUITextHandle.newHandlesSinceCleanup > 500;
			if (flag)
			{
				IMGUITextHandle.ClearUnusedTextHandles();
				IMGUITextHandle.lastCleanupTime = currentTime;
				IMGUITextHandle.newHandlesSinceCleanup = 0;
			}
			int hash = settings.GetHashCode();
			IMGUITextHandle textHandleCached;
			bool flag2 = IMGUITextHandle.textHandles.TryGetValue(hash, out textHandleCached);
			IMGUITextHandle imguitextHandle;
			if (flag2)
			{
				IMGUITextHandle.textHandlesTuple.Remove(textHandleCached.tuple);
				IMGUITextHandle.textHandlesTuple.AddLast(textHandleCached.tuple);
				isCached = !isCalledFromNative || textHandleCached.isCachedOnNative;
				bool flag3 = !textHandleCached.isCachedOnNative && isCalledFromNative;
				if (flag3)
				{
					textHandleCached.UpdateWithHash(hash);
					textHandleCached.UpdatePreferredSize();
					textHandleCached.isCachedOnNative = true;
				}
				imguitextHandle = textHandleCached;
			}
			else
			{
				IMGUITextHandle handle = new IMGUITextHandle();
				IMGUITextHandle.TextHandleTuple tuple = new IMGUITextHandle.TextHandleTuple(currentTime, hash);
				LinkedListNode<IMGUITextHandle.TextHandleTuple> listNode = new LinkedListNode<IMGUITextHandle.TextHandleTuple>(tuple);
				handle.tuple = listNode;
				IMGUITextHandle.textHandles[hash] = handle;
				handle.UpdateWithHash(hash);
				handle.UpdatePreferredSize();
				IMGUITextHandle.textHandlesTuple.AddLast(listNode);
				handle.isCachedOnNative = isCalledFromNative;
				IMGUITextHandle.newHandlesSinceCleanup++;
				imguitextHandle = handle;
			}
			return imguitextHandle;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006AF4 File Offset: 0x00004CF4
		internal static float GetLineHeight(GUIStyle style)
		{
			IMGUITextHandle.ConvertGUIStyleToGenerationSettings(TextHandle.settings, style, Color.white, "", Rect.zero);
			return TextHandle.GetLineHeightDefault(TextHandle.settings);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006B2C File Offset: 0x00004D2C
		internal Vector2 GetPreferredSize()
		{
			return base.preferredSize;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006B44 File Offset: 0x00004D44
		private static void ConvertGUIStyleToGenerationSettings(TextGenerationSettings settings, GUIStyle style, Color textColor, string text, Rect rect)
		{
			settings.textSettings = RuntimeTextSettings.defaultTextSettings;
			bool flag = settings.textSettings == null;
			if (!flag)
			{
				Font font = style.font;
				bool flag2 = !font;
				if (flag2)
				{
					font = GUIStyle.GetDefaultFont();
				}
				bool flag3 = style.fontSize > 0;
				if (flag3)
				{
					settings.fontSize = (float)style.fontSize;
				}
				else
				{
					bool flag4 = font;
					if (flag4)
					{
						settings.fontSize = (float)font.fontSize;
					}
					else
					{
						settings.fontSize = 13f;
					}
				}
				settings.fontStyle = TextGeneratorUtilities.LegacyStyleToNewStyle(style.fontStyle);
				settings.fontAsset = settings.textSettings.GetCachedFontAsset(font, TextShaderUtilities.ShaderRef_MobileSDF_IMGUI);
				bool flag5 = settings.fontAsset == null;
				if (!flag5)
				{
					settings.material = settings.fontAsset.material;
					settings.fontAsset.material.SetFloat("_Sharpness", 0.5f);
					settings.screenRect = new Rect(0f, 0f, rect.width, rect.height);
					settings.text = text;
					TextAnchor tempAlignment = style.alignment;
					bool flag6 = style.imagePosition == ImagePosition.ImageAbove;
					if (flag6)
					{
						switch (style.alignment)
						{
						case TextAnchor.MiddleLeft:
						case TextAnchor.LowerLeft:
							tempAlignment = TextAnchor.UpperLeft;
							break;
						case TextAnchor.MiddleCenter:
						case TextAnchor.LowerCenter:
							tempAlignment = TextAnchor.UpperCenter;
							break;
						case TextAnchor.MiddleRight:
						case TextAnchor.LowerRight:
							tempAlignment = TextAnchor.UpperRight;
							break;
						}
					}
					settings.textAlignment = TextGeneratorUtilities.LegacyAlignmentToNewAlignment(tempAlignment);
					settings.overflowMode = IMGUITextHandle.LegacyClippingToNewOverflow(style.clipping);
					settings.wordWrappingRatio = 0.4f;
					bool flag7 = rect.width > 0f && style.wordWrap;
					if (flag7)
					{
						settings.textWrappingMode = TextWrappingMode.PreserveWhitespace;
					}
					else
					{
						settings.textWrappingMode = TextWrappingMode.PreserveWhitespaceNoWrap;
					}
					settings.richText = style.richText;
					settings.parseControlCharacters = false;
					settings.isPlaceholder = false;
					settings.isRightToLeft = false;
					settings.characterSpacing = 0f;
					settings.wordSpacing = 0f;
					settings.paragraphSpacing = 0f;
					settings.color = textColor;
					settings.inverseYAxis = true;
					settings.isIMGUI = true;
					settings.shouldConvertToLinearSpace = QualitySettings.activeColorSpace == ColorSpace.Linear;
					settings.fontFeatures = TextHandle.m_ActiveFontFeatures;
				}
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006D84 File Offset: 0x00004F84
		private static TextOverflowMode LegacyClippingToNewOverflow(TextClipping clipping)
		{
			switch (clipping)
			{
			case TextClipping.Clip:
				return TextOverflowMode.Masking;
			case TextClipping.Ellipsis:
				return TextOverflowMode.Ellipsis;
			}
			return TextOverflowMode.Overflow;
		}

		// Token: 0x040000C2 RID: 194
		internal LinkedListNode<IMGUITextHandle.TextHandleTuple> tuple;

		// Token: 0x040000C3 RID: 195
		private static Dictionary<int, IMGUITextHandle> textHandles = new Dictionary<int, IMGUITextHandle>();

		// Token: 0x040000C4 RID: 196
		private static LinkedList<IMGUITextHandle.TextHandleTuple> textHandlesTuple = new LinkedList<IMGUITextHandle.TextHandleTuple>();

		// Token: 0x040000C5 RID: 197
		private static float lastCleanupTime;

		// Token: 0x040000C6 RID: 198
		private static int newHandlesSinceCleanup = 0;

		// Token: 0x040000C7 RID: 199
		internal bool isCachedOnNative = false;

		// Token: 0x02000021 RID: 33
		internal class TextHandleTuple
		{
			// Token: 0x0600019C RID: 412 RVA: 0x00006DE3 File Offset: 0x00004FE3
			public TextHandleTuple(float lastTimeUsed, int hashCode)
			{
				this.hashCode = hashCode;
				this.lastTimeUsed = lastTimeUsed;
			}

			// Token: 0x040000C8 RID: 200
			public float lastTimeUsed;

			// Token: 0x040000C9 RID: 201
			public int hashCode;
		}
	}
}
