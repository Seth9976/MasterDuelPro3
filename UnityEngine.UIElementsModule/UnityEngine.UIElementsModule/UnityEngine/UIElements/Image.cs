using System;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020000DA RID: 218
	public class Image : VisualElement
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001FA46 File Offset: 0x0001DC46
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0001FA50 File Offset: 0x0001DC50
		[CreateProperty]
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				bool flag = this.m_Image == value && this.m_ImageIsInline;
				if (!flag)
				{
					this.m_ImageIsInline = value != null;
					this.SetProperty<Texture, Sprite, VectorImage>(value, ref this.m_Image, ref this.m_Sprite, ref this.m_VectorImage, Image.imageProperty);
				}
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001FAA7 File Offset: 0x0001DCA7
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0001FAB0 File Offset: 0x0001DCB0
		[CreateProperty]
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				bool flag = this.m_Sprite == value && this.m_ImageIsInline;
				if (!flag)
				{
					this.m_ImageIsInline = value != null;
					this.SetProperty<Sprite, Texture, VectorImage>(value, ref this.m_Sprite, ref this.m_Image, ref this.m_VectorImage, Image.spriteProperty);
				}
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001FB07 File Offset: 0x0001DD07
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0001FB10 File Offset: 0x0001DD10
		[CreateProperty]
		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = this.m_VectorImage == value && this.m_ImageIsInline;
				if (!flag)
				{
					this.m_ImageIsInline = value != null;
					this.SetProperty<VectorImage, Texture, Sprite>(value, ref this.m_VectorImage, ref this.m_Image, ref this.m_Sprite, Image.vectorImageProperty);
				}
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001FB67 File Offset: 0x0001DD67
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001FB70 File Offset: 0x0001DD70
		[CreateProperty]
		public Rect sourceRect
		{
			get
			{
				return this.GetSourceRect();
			}
			set
			{
				bool flag = this.GetSourceRect() == value;
				if (!flag)
				{
					bool flag2 = this.sprite != null;
					if (flag2)
					{
						Debug.LogError("Cannot set sourceRect on a sprite image");
					}
					else
					{
						this.CalculateUV(value);
						base.NotifyPropertyChanged(in Image.sourceRectProperty);
					}
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001FBC2 File Offset: 0x0001DDC2
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001FBCC File Offset: 0x0001DDCC
		[CreateProperty]
		public Rect uv
		{
			get
			{
				return this.m_UV;
			}
			set
			{
				bool flag = this.m_UV == value;
				if (!flag)
				{
					this.m_UV = value;
					base.NotifyPropertyChanged(in Image.uvProperty);
				}
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001FBFF File Offset: 0x0001DDFF
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001FC08 File Offset: 0x0001DE08
		[CreateProperty]
		public ScaleMode scaleMode
		{
			get
			{
				return this.m_ScaleMode;
			}
			set
			{
				bool flag = this.m_ScaleMode == value && this.m_ScaleModeIsInline;
				if (!flag)
				{
					this.m_ScaleModeIsInline = true;
					this.SetScaleMode(value);
				}
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001FC3D File Offset: 0x0001DE3D
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001FC48 File Offset: 0x0001DE48
		[CreateProperty]
		public Color tintColor
		{
			get
			{
				return this.m_TintColor;
			}
			set
			{
				bool flag = this.m_TintColor == value && this.m_TintColorIsInline;
				if (!flag)
				{
					this.m_TintColorIsInline = true;
					this.SetTintColor(value);
				}
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001FC84 File Offset: 0x0001DE84
		public Image()
		{
			base.AddToClassList(Image.ussClassName);
			this.m_ScaleMode = ScaleMode.ScaleToFit;
			this.m_TintColor = Color.white;
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			base.requireMeasureFunction = true;
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001FD18 File Offset: 0x0001DF18
		private Vector2 GetTextureDisplaySize(Texture texture)
		{
			Vector2 result = Vector2.zero;
			bool flag = texture != null;
			if (flag)
			{
				result = new Vector2((float)texture.width, (float)texture.height);
			}
			return result;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001FD54 File Offset: 0x0001DF54
		private Vector2 GetTextureDisplaySize(Sprite sprite)
		{
			Vector2 result = Vector2.zero;
			bool flag = sprite != null;
			if (flag)
			{
				float scale = UIElementsUtility.PixelsPerUnitScaleForElement(this, sprite);
				result = sprite.bounds.size * sprite.pixelsPerUnit * scale;
			}
			return result;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001FDAC File Offset: 0x0001DFAC
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			float measuredWidth = float.NaN;
			float measuredHeight = float.NaN;
			bool flag = this.image == null && this.sprite == null && this.vectorImage == null;
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(measuredWidth, measuredHeight);
			}
			else
			{
				Vector2 sourceSize = Vector2.zero;
				bool flag2 = this.image != null;
				if (flag2)
				{
					sourceSize = this.GetTextureDisplaySize(this.image);
				}
				else
				{
					bool flag3 = this.sprite != null;
					if (flag3)
					{
						sourceSize = this.GetTextureDisplaySize(this.sprite);
					}
					else
					{
						sourceSize = this.vectorImage.size;
					}
				}
				Rect rect = this.sourceRect;
				bool hasRect = rect != Rect.zero;
				measuredWidth = (hasRect ? Mathf.Abs(rect.width) : sourceSize.x);
				measuredHeight = (hasRect ? Mathf.Abs(rect.height) : sourceSize.y);
				bool flag4 = widthMode == VisualElement.MeasureMode.AtMost;
				if (flag4)
				{
					measuredWidth = Mathf.Min(measuredWidth, desiredWidth);
				}
				bool flag5 = heightMode == VisualElement.MeasureMode.AtMost;
				if (flag5)
				{
					measuredHeight = Mathf.Min(measuredHeight, desiredHeight);
				}
				vector = new Vector2(measuredWidth, measuredHeight);
			}
			return vector;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.image == null && this.sprite == null && this.vectorImage == null;
			if (!flag)
			{
				Rect alignedRect = GUIUtility.AlignRectToDevice(base.contentRect);
				VisualElement visualElement = mgc.visualElement;
				Color playModeTintColor = ((visualElement != null) ? visualElement.playModeTintColor : Color.white);
				MeshGenerator.RectangleParams rectParams = default(MeshGenerator.RectangleParams);
				bool flag2 = this.image != null;
				if (flag2)
				{
					rectParams = MeshGenerator.RectangleParams.MakeTextured(alignedRect, this.uv, this.image, this.scaleMode, playModeTintColor);
				}
				else
				{
					bool flag3 = this.sprite != null;
					if (flag3)
					{
						Vector4 slices = Vector4.zero;
						rectParams = MeshGenerator.RectangleParams.MakeSprite(alignedRect, this.uv, this.sprite, this.scaleMode, playModeTintColor, false, ref slices, false);
					}
					else
					{
						bool flag4 = this.vectorImage != null;
						if (flag4)
						{
							rectParams = MeshGenerator.RectangleParams.MakeVectorTextured(alignedRect, this.uv, this.vectorImage, this.scaleMode, playModeTintColor);
						}
					}
				}
				rectParams.color = this.tintColor;
				mgc.meshGenerator.DrawRectangle(rectParams);
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001FFF4 File Offset: 0x0001E1F4
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this.ReadCustomProperties(e.customStyle);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00020004 File Offset: 0x0001E204
		private void ReadCustomProperties(ICustomStyle customStyleProvider)
		{
			bool flag = !this.m_ImageIsInline;
			if (flag)
			{
				Texture2D textureValue;
				bool flag2 = customStyleProvider.TryGetValue(Image.s_ImageProperty, out textureValue);
				if (flag2)
				{
					this.SetProperty<Texture, Sprite, VectorImage>(textureValue, ref this.m_Image, ref this.m_Sprite, ref this.m_VectorImage, Image.imageProperty);
				}
				else
				{
					Sprite spriteValue;
					bool flag3 = customStyleProvider.TryGetValue(Image.s_SpriteProperty, out spriteValue);
					if (flag3)
					{
						this.SetProperty<Sprite, Texture, VectorImage>(spriteValue, ref this.m_Sprite, ref this.m_Image, ref this.m_VectorImage, Image.spriteProperty);
					}
					else
					{
						VectorImage vectorImageValue;
						bool flag4 = customStyleProvider.TryGetValue(Image.s_VectorImageProperty, out vectorImageValue);
						if (flag4)
						{
							this.SetProperty<VectorImage, Texture, Sprite>(vectorImageValue, ref this.m_VectorImage, ref this.m_Image, ref this.m_Sprite, Image.vectorImageProperty);
						}
						else
						{
							this.ClearProperty();
						}
					}
				}
			}
			string scaleModeValue;
			bool flag5 = !this.m_ScaleModeIsInline && customStyleProvider.TryGetValue(Image.s_ScaleModeProperty, out scaleModeValue);
			if (flag5)
			{
				int intValue;
				StylePropertyUtil.TryGetEnumIntValue(StyleEnumType.ScaleMode, scaleModeValue, out intValue);
				this.SetScaleMode((ScaleMode)intValue);
			}
			bool flag6 = !this.m_TintColorIsInline;
			if (flag6)
			{
				Color tintValue;
				bool flag7 = customStyleProvider.TryGetValue(Image.s_TintColorProperty, out tintValue);
				if (flag7)
				{
					this.SetTintColor(tintValue);
				}
				else
				{
					this.SetTintColor(Color.white);
				}
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00020140 File Offset: 0x0001E340
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetProperty<T0, T1, T2>(T0 src, ref T0 dst, ref T1 alt0, ref T2 alt1, BindingId binding) where T0 : Object where T1 : Object where T2 : Object
		{
			bool flag = src == dst;
			if (!flag)
			{
				dst = src;
				bool flag2 = dst != null;
				if (flag2)
				{
					alt0 = default(T1);
					alt1 = default(T2);
				}
				bool flag3 = dst == null;
				if (flag3)
				{
					this.uv = new Rect(0f, 0f, 1f, 1f);
					this.ReadCustomProperties(base.customStyle);
				}
				base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				base.NotifyPropertyChanged(in binding);
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000201F4 File Offset: 0x0001E3F4
		private void ClearProperty()
		{
			bool imageIsInline = this.m_ImageIsInline;
			if (!imageIsInline)
			{
				this.image = null;
				this.sprite = null;
				this.vectorImage = null;
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00020228 File Offset: 0x0001E428
		private void SetScaleMode(ScaleMode mode)
		{
			bool flag = this.m_ScaleMode != mode;
			if (flag)
			{
				this.m_ScaleMode = mode;
				base.IncrementVersion(VersionChangeType.Repaint);
				base.NotifyPropertyChanged(in Image.scaleModeProperty);
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00020268 File Offset: 0x0001E468
		private void SetTintColor(Color color)
		{
			bool flag = this.m_TintColor != color;
			if (flag)
			{
				this.m_TintColor = color;
				base.IncrementVersion(VersionChangeType.Repaint);
				base.NotifyPropertyChanged(in Image.tintColorProperty);
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000202A8 File Offset: 0x0001E4A8
		private void CalculateUV(Rect srcRect)
		{
			this.m_UV = new Rect(0f, 0f, 1f, 1f);
			Vector2 size = Vector2.zero;
			Texture texture = this.image;
			bool flag = texture != null;
			if (flag)
			{
				size = this.GetTextureDisplaySize(texture);
			}
			VectorImage vi = this.vectorImage;
			bool flag2 = vi != null;
			if (flag2)
			{
				size = vi.size;
			}
			bool flag3 = size != Vector2.zero;
			if (flag3)
			{
				this.m_UV.x = srcRect.x / size.x;
				this.m_UV.width = srcRect.width / size.x;
				this.m_UV.height = srcRect.height / size.y;
				this.m_UV.y = 1f - this.m_UV.height - srcRect.y / size.y;
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000203A0 File Offset: 0x0001E5A0
		private Rect GetSourceRect()
		{
			Rect rect = Rect.zero;
			Vector2 size = Vector2.zero;
			Texture texture = this.image;
			bool flag = texture != null;
			if (flag)
			{
				size = this.GetTextureDisplaySize(texture);
			}
			VectorImage vi = this.vectorImage;
			bool flag2 = vi != null;
			if (flag2)
			{
				size = vi.size;
			}
			bool flag3 = size != Vector2.zero;
			if (flag3)
			{
				rect.x = this.uv.x * size.x;
				rect.width = this.uv.width * size.x;
				rect.y = (1f - this.uv.y - this.uv.height) * size.y;
				rect.height = this.uv.height * size.y;
			}
			return rect;
		}

		// Token: 0x0400041B RID: 1051
		internal static readonly BindingId imageProperty = "image";

		// Token: 0x0400041C RID: 1052
		internal static readonly BindingId spriteProperty = "sprite";

		// Token: 0x0400041D RID: 1053
		internal static readonly BindingId vectorImageProperty = "vectorImage";

		// Token: 0x0400041E RID: 1054
		internal static readonly BindingId sourceRectProperty = "sourceRect";

		// Token: 0x0400041F RID: 1055
		internal static readonly BindingId uvProperty = "uv";

		// Token: 0x04000420 RID: 1056
		internal static readonly BindingId scaleModeProperty = "scaleMode";

		// Token: 0x04000421 RID: 1057
		internal static readonly BindingId tintColorProperty = "tintColor";

		// Token: 0x04000422 RID: 1058
		private ScaleMode m_ScaleMode;

		// Token: 0x04000423 RID: 1059
		private Texture m_Image;

		// Token: 0x04000424 RID: 1060
		private Sprite m_Sprite;

		// Token: 0x04000425 RID: 1061
		private VectorImage m_VectorImage;

		// Token: 0x04000426 RID: 1062
		private Rect m_UV;

		// Token: 0x04000427 RID: 1063
		private Color m_TintColor;

		// Token: 0x04000428 RID: 1064
		internal bool m_ImageIsInline;

		// Token: 0x04000429 RID: 1065
		private bool m_ScaleModeIsInline;

		// Token: 0x0400042A RID: 1066
		private bool m_TintColorIsInline;

		// Token: 0x0400042B RID: 1067
		public static readonly string ussClassName = "unity-image";

		// Token: 0x0400042C RID: 1068
		private static CustomStyleProperty<Texture2D> s_ImageProperty = new CustomStyleProperty<Texture2D>("--unity-image");

		// Token: 0x0400042D RID: 1069
		private static CustomStyleProperty<Sprite> s_SpriteProperty = new CustomStyleProperty<Sprite>("--unity-image");

		// Token: 0x0400042E RID: 1070
		private static CustomStyleProperty<VectorImage> s_VectorImageProperty = new CustomStyleProperty<VectorImage>("--unity-image");

		// Token: 0x0400042F RID: 1071
		private static CustomStyleProperty<string> s_ScaleModeProperty = new CustomStyleProperty<string>("--unity-image-size");

		// Token: 0x04000430 RID: 1072
		private static CustomStyleProperty<Color> s_TintColorProperty = new CustomStyleProperty<Color>("--unity-image-tint-color");

		// Token: 0x020000DB RID: 219
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Image, Image.UxmlTraits>
		{
		}

		// Token: 0x020000DC RID: 220
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
		}
	}
}
