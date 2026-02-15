using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200056B RID: 1387
	internal class UIRVEShaderInfoAllocator
	{
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x00097828 File Offset: 0x00095A28
		private static int pageWidth
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x0009783C File Offset: 0x00095A3C
		private static int pageHeight
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x00097850 File Offset: 0x00095A50
		private static void AcquireDefaultShaderInfoTexture()
		{
			bool flag = ++UIRVEShaderInfoAllocator.s_DefaultShaderInfoTextureRefCount == 1;
			if (flag)
			{
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture = new Texture2D(64, 64, TextureFormat.RGBAFloat, false);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.name = "DefaultShaderInfoTexFloat";
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.hideFlags = HideFlags.HideAndDontSave;
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.filterMode = FilterMode.Point;
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y, UIRVEShaderInfoAllocator.identityTransformRow0Value);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 1, UIRVEShaderInfoAllocator.identityTransformRow1Value);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 2, UIRVEShaderInfoAllocator.identityTransformRow2Value);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, UIRVEShaderInfoAllocator.infiniteClipRectTexel.y, UIRVEShaderInfoAllocator.infiniteClipRectValue);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.fullOpacityTexel.x, UIRVEShaderInfoAllocator.fullOpacityTexel.y, UIRVEShaderInfoAllocator.fullOpacityValue);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y, Color.white);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 1, Color.clear);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 2, Color.clear);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 3, Color.clear);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture.Apply(false, true);
			}
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x00097A5C File Offset: 0x00095C5C
		private static void ReleaseDefaultShaderInfoTexture()
		{
			bool flag = --UIRVEShaderInfoAllocator.s_DefaultShaderInfoTextureRefCount == 0;
			if (flag)
			{
				UIRUtility.Destroy(UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture);
				UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture = null;
			}
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x00097A94 File Offset: 0x00095C94
		private static Vector2Int AllocToTexelCoord(ref BitmapAllocator32 allocator, BMPAlloc alloc)
		{
			ushort x;
			ushort y;
			allocator.GetAllocPageAtlasLocation(alloc.page, out x, out y);
			return new Vector2Int((int)alloc.bitIndex * allocator.entryWidth + (int)x, (int)alloc.pageLine * allocator.entryHeight + (int)y);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x00097ADC File Offset: 0x00095CDC
		private static bool AtlasRectMatchesPage(ref BitmapAllocator32 allocator, BMPAlloc defAlloc, RectInt atlasRect)
		{
			ushort x;
			ushort y;
			allocator.GetAllocPageAtlasLocation(defAlloc.page, out x, out y);
			return (int)x == atlasRect.xMin && (int)y == atlasRect.yMin && allocator.entryWidth * UIRVEShaderInfoAllocator.pageWidth == atlasRect.width && allocator.entryHeight * UIRVEShaderInfoAllocator.pageHeight == atlasRect.height;
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x00097B40 File Offset: 0x00095D40
		public Texture atlas
		{
			get
			{
				bool storageReallyCreated = this.m_StorageReallyCreated;
				Texture texture;
				if (storageReallyCreated)
				{
					texture = this.m_Storage.texture;
				}
				else
				{
					texture = UIRVEShaderInfoAllocator.s_DefaultShaderInfoTexture;
				}
				return texture;
			}
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x00097B70 File Offset: 0x00095D70
		public UIRVEShaderInfoAllocator(ColorSpace colorSpace)
		{
			this.m_ColorSpace = colorSpace;
			this.m_OpacityAllocator = (this.m_ColorAllocator = (this.m_ClipRectAllocator = (this.m_TransformAllocator = (this.m_TextSettingsAllocator = default(BitmapAllocator32)))));
			this.m_TransformAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 3);
			this.m_TransformAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.identityTransformTexel.x, (ushort)UIRVEShaderInfoAllocator.identityTransformTexel.y);
			this.m_ClipRectAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_ClipRectAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, (ushort)UIRVEShaderInfoAllocator.infiniteClipRectTexel.y);
			this.m_OpacityAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_OpacityAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.fullOpacityTexel.x, (ushort)UIRVEShaderInfoAllocator.fullOpacityTexel.y);
			this.m_ColorAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_ColorAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.clearColorTexel.x, (ushort)UIRVEShaderInfoAllocator.clearColorTexel.y);
			this.m_TextSettingsAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 4);
			this.m_TextSettingsAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, (ushort)UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y);
			UIRVEShaderInfoAllocator.AcquireDefaultShaderInfoTexture();
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x00097CEC File Offset: 0x00095EEC
		private void ReallyCreateStorage()
		{
			this.m_Storage = new ShaderInfoStorageRGBAFloat(64, 4096);
			RectInt rcTransform;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_TransformAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_TransformAllocator.entryHeight, out rcTransform);
			RectInt rcClipRect;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_ClipRectAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_ClipRectAllocator.entryHeight, out rcClipRect);
			RectInt rcOpacity;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_OpacityAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_OpacityAllocator.entryHeight, out rcOpacity);
			RectInt rcColor;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_ColorAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_ColorAllocator.entryHeight, out rcColor);
			RectInt rcTextCoreSettings;
			this.m_Storage.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_TextSettingsAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_TextSettingsAllocator.entryHeight, out rcTextCoreSettings);
			bool flag = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_TransformAllocator, UIRVEShaderInfoAllocator.identityTransform, rcTransform);
			if (flag)
			{
				throw new Exception("Atlas identity transform allocation failed unexpectedly");
			}
			bool flag2 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_ClipRectAllocator, UIRVEShaderInfoAllocator.infiniteClipRect, rcClipRect);
			if (flag2)
			{
				throw new Exception("Atlas infinite clip rect allocation failed unexpectedly");
			}
			bool flag3 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_OpacityAllocator, UIRVEShaderInfoAllocator.fullOpacity, rcOpacity);
			if (flag3)
			{
				throw new Exception("Atlas full opacity allocation failed unexpectedly");
			}
			bool flag4 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_ColorAllocator, UIRVEShaderInfoAllocator.clearColor, rcColor);
			if (flag4)
			{
				throw new Exception("Atlas clear color allocation failed unexpectedly");
			}
			bool flag5 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_TextSettingsAllocator, UIRVEShaderInfoAllocator.defaultTextCoreSettings, rcTextCoreSettings);
			if (flag5)
			{
				throw new Exception("Atlas text setting allocation failed unexpectedly");
			}
			this.SetTransformValue(UIRVEShaderInfoAllocator.identityTransform, UIRVEShaderInfoAllocator.identityTransformValue);
			this.SetClipRectValue(UIRVEShaderInfoAllocator.infiniteClipRect, UIRVEShaderInfoAllocator.infiniteClipRectValue);
			this.SetOpacityValue(UIRVEShaderInfoAllocator.fullOpacity, UIRVEShaderInfoAllocator.fullOpacityValue.w);
			this.SetColorValue(UIRVEShaderInfoAllocator.clearColor, UIRVEShaderInfoAllocator.clearColorValue);
			this.SetTextCoreSettingValue(UIRVEShaderInfoAllocator.defaultTextCoreSettings, UIRVEShaderInfoAllocator.defaultTextCoreSettingsValue);
			this.m_StorageReallyCreated = true;
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x00097F1C File Offset: 0x0009611C
		public void Dispose()
		{
			bool flag = this.m_Storage != null;
			if (flag)
			{
				this.m_Storage.Dispose();
			}
			this.m_Storage = null;
			this.m_StorageReallyCreated = false;
			UIRVEShaderInfoAllocator.ReleaseDefaultShaderInfoTexture();
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x00097F57 File Offset: 0x00096157
		public void IssuePendingStorageChanges()
		{
			BaseShaderInfoStorage storage = this.m_Storage;
			if (storage != null)
			{
				storage.UpdateTexture();
			}
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x00097F6C File Offset: 0x0009616C
		public BMPAlloc AllocTransform()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_TransformAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x00097FA4 File Offset: 0x000961A4
		public BMPAlloc AllocClipRect()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_ClipRectAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x00097FDC File Offset: 0x000961DC
		public BMPAlloc AllocOpacity()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_OpacityAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x00098014 File Offset: 0x00096214
		public BMPAlloc AllocColor()
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_ColorAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x0009804C File Offset: 0x0009624C
		public BMPAlloc AllocTextCoreSettings(TextCoreSettings settings)
		{
			bool flag = !this.m_StorageReallyCreated;
			if (flag)
			{
				this.ReallyCreateStorage();
			}
			return this.m_TextSettingsAllocator.Allocate(this.m_Storage);
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x00098084 File Offset: 0x00096284
		public void SetTransformValue(BMPAlloc alloc, Matrix4x4 xform)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int allocXY = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_TransformAllocator, alloc);
			this.m_Storage.SetTexel(allocXY.x, allocXY.y, xform.GetRow(0));
			this.m_Storage.SetTexel(allocXY.x, allocXY.y + 1, xform.GetRow(1));
			this.m_Storage.SetTexel(allocXY.x, allocXY.y + 2, xform.GetRow(2));
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x00098128 File Offset: 0x00096328
		public void SetClipRectValue(BMPAlloc alloc, Vector4 clipRect)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int allocXY = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_ClipRectAllocator, alloc);
			this.m_Storage.SetTexel(allocXY.x, allocXY.y, clipRect);
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00098170 File Offset: 0x00096370
		public void SetOpacityValue(BMPAlloc alloc, float opacity)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int allocXY = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_OpacityAllocator, alloc);
			this.m_Storage.SetTexel(allocXY.x, allocXY.y, new Color(1f, 1f, 1f, opacity));
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000981C8 File Offset: 0x000963C8
		public void SetColorValue(BMPAlloc alloc, Color color)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int allocXY = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_ColorAllocator, alloc);
			bool flag = this.m_ColorSpace == ColorSpace.Linear;
			if (flag)
			{
				this.m_Storage.SetTexel(allocXY.x, allocXY.y, color.linear);
			}
			else
			{
				this.m_Storage.SetTexel(allocXY.x, allocXY.y, color);
			}
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x0009823C File Offset: 0x0009643C
		public void SetTextCoreSettingValue(BMPAlloc alloc, TextCoreSettings settings)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int allocXY = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_TextSettingsAllocator, alloc);
			Color settingValues = new Color(-settings.underlayOffset.x, settings.underlayOffset.y, settings.underlaySoftness, settings.outlineWidth);
			bool flag = this.m_ColorSpace == ColorSpace.Linear;
			if (flag)
			{
				this.m_Storage.SetTexel(allocXY.x, allocXY.y, settings.faceColor.linear);
				this.m_Storage.SetTexel(allocXY.x, allocXY.y + 1, settings.outlineColor.linear);
				this.m_Storage.SetTexel(allocXY.x, allocXY.y + 2, settings.underlayColor.linear);
			}
			else
			{
				this.m_Storage.SetTexel(allocXY.x, allocXY.y, settings.faceColor);
				this.m_Storage.SetTexel(allocXY.x, allocXY.y + 1, settings.outlineColor);
				this.m_Storage.SetTexel(allocXY.x, allocXY.y + 2, settings.underlayColor);
			}
			this.m_Storage.SetTexel(allocXY.x, allocXY.y + 3, settingValues);
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x00098398 File Offset: 0x00096598
		public void FreeTransform(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_TransformAllocator.Free(alloc);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000983B5 File Offset: 0x000965B5
		public void FreeClipRect(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_ClipRectAllocator.Free(alloc);
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000983D2 File Offset: 0x000965D2
		public void FreeOpacity(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_OpacityAllocator.Free(alloc);
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000983EF File Offset: 0x000965EF
		public void FreeColor(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_ColorAllocator.Free(alloc);
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x0009840C File Offset: 0x0009660C
		public void FreeTextCoreSettings(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_TextSettingsAllocator.Free(alloc);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x0009842C File Offset: 0x0009662C
		public Color32 TransformAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort x = 0;
			ushort y = 0;
			this.m_TransformAllocator.GetAllocPageAtlasLocation(alloc.page, out x, out y);
			return new Color32((byte)(x >> 5), (byte)(y >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00098494 File Offset: 0x00096694
		public Color32 ClipRectAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort x = 0;
			ushort y = 0;
			this.m_ClipRectAllocator.GetAllocPageAtlasLocation(alloc.page, out x, out y);
			return new Color32((byte)(x >> 5), (byte)(y >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000984FC File Offset: 0x000966FC
		public Color32 OpacityAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort x;
			ushort y;
			this.m_OpacityAllocator.GetAllocPageAtlasLocation(alloc.page, out x, out y);
			return new Color32((byte)(x >> 5), (byte)(y >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x00098560 File Offset: 0x00096760
		public Color32 ColorAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort x;
			ushort y;
			this.m_ColorAllocator.GetAllocPageAtlasLocation(alloc.page, out x, out y);
			return new Color32((byte)(x >> 5), (byte)(y >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000985C4 File Offset: 0x000967C4
		public Color32 TextCoreSettingsToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort x;
			ushort y;
			this.m_TextSettingsAllocator.GetAllocPageAtlasLocation(alloc.page, out x, out y);
			return new Color32((byte)(x >> 5), (byte)(y >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x0400133E RID: 4926
		private BaseShaderInfoStorage m_Storage;

		// Token: 0x0400133F RID: 4927
		private BitmapAllocator32 m_TransformAllocator;

		// Token: 0x04001340 RID: 4928
		private BitmapAllocator32 m_ClipRectAllocator;

		// Token: 0x04001341 RID: 4929
		private BitmapAllocator32 m_OpacityAllocator;

		// Token: 0x04001342 RID: 4930
		private BitmapAllocator32 m_ColorAllocator;

		// Token: 0x04001343 RID: 4931
		private BitmapAllocator32 m_TextSettingsAllocator;

		// Token: 0x04001344 RID: 4932
		private bool m_StorageReallyCreated;

		// Token: 0x04001345 RID: 4933
		private ColorSpace m_ColorSpace;

		// Token: 0x04001346 RID: 4934
		private static readonly Vector2Int identityTransformTexel = new Vector2Int(0, 0);

		// Token: 0x04001347 RID: 4935
		private static readonly Vector2Int infiniteClipRectTexel = new Vector2Int(0, 32);

		// Token: 0x04001348 RID: 4936
		private static readonly Vector2Int fullOpacityTexel = new Vector2Int(32, 32);

		// Token: 0x04001349 RID: 4937
		private static readonly Vector2Int clearColorTexel = new Vector2Int(0, 40);

		// Token: 0x0400134A RID: 4938
		private static readonly Vector2Int defaultTextCoreSettingsTexel = new Vector2Int(32, 0);

		// Token: 0x0400134B RID: 4939
		private static readonly Matrix4x4 identityTransformValue = Matrix4x4.identity;

		// Token: 0x0400134C RID: 4940
		private static readonly Vector4 identityTransformRow0Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(0);

		// Token: 0x0400134D RID: 4941
		private static readonly Vector4 identityTransformRow1Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(1);

		// Token: 0x0400134E RID: 4942
		private static readonly Vector4 identityTransformRow2Value = UIRVEShaderInfoAllocator.identityTransformValue.GetRow(2);

		// Token: 0x0400134F RID: 4943
		private static readonly Vector4 infiniteClipRectValue = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04001350 RID: 4944
		private static readonly Vector4 fullOpacityValue = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x04001351 RID: 4945
		private static readonly Vector4 clearColorValue = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04001352 RID: 4946
		private static readonly TextCoreSettings defaultTextCoreSettingsValue = new TextCoreSettings
		{
			faceColor = Color.white,
			outlineColor = Color.clear,
			outlineWidth = 0f,
			underlayColor = Color.clear,
			underlayOffset = Vector2.zero,
			underlaySoftness = 0f
		};

		// Token: 0x04001353 RID: 4947
		public static readonly BMPAlloc identityTransform;

		// Token: 0x04001354 RID: 4948
		public static readonly BMPAlloc infiniteClipRect;

		// Token: 0x04001355 RID: 4949
		public static readonly BMPAlloc fullOpacity;

		// Token: 0x04001356 RID: 4950
		public static readonly BMPAlloc clearColor;

		// Token: 0x04001357 RID: 4951
		public static readonly BMPAlloc defaultTextCoreSettings;

		// Token: 0x04001358 RID: 4952
		private static int s_DefaultShaderInfoTextureRefCount;

		// Token: 0x04001359 RID: 4953
		private static Texture2D s_DefaultShaderInfoTexture;
	}
}
