using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BF RID: 703
	public struct Background : IEquatable<Background>
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x0004E6E0 File Offset: 0x0004C8E0
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x0004E6F8 File Offset: 0x0004C8F8
		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				bool flag = this.m_Texture == value;
				if (!flag)
				{
					this.m_Texture = value;
					this.m_Sprite = null;
					this.m_RenderTexture = null;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0004E734 File Offset: 0x0004C934
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x0004E74C File Offset: 0x0004C94C
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				bool flag = this.m_Sprite == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = value;
					this.m_RenderTexture = null;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0004E788 File Offset: 0x0004C988
		// (set) Token: 0x060012F6 RID: 4854 RVA: 0x0004E7A0 File Offset: 0x0004C9A0
		public RenderTexture renderTexture
		{
			get
			{
				return this.m_RenderTexture;
			}
			set
			{
				bool flag = this.m_RenderTexture == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = null;
					this.m_RenderTexture = value;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0004E7DC File Offset: 0x0004C9DC
		// (set) Token: 0x060012F8 RID: 4856 RVA: 0x0004E7F4 File Offset: 0x0004C9F4
		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = this.vectorImage == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = null;
					this.m_RenderTexture = null;
					this.m_VectorImage = value;
				}
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0004E830 File Offset: 0x0004CA30
		public static Background FromTexture2D(Texture2D t)
		{
			return new Background
			{
				texture = t
			};
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0004E854 File Offset: 0x0004CA54
		public static Background FromRenderTexture(RenderTexture rt)
		{
			return new Background
			{
				renderTexture = rt
			};
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004E878 File Offset: 0x0004CA78
		public static Background FromSprite(Sprite s)
		{
			return new Background
			{
				sprite = s
			};
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0004E89C File Offset: 0x0004CA9C
		public static Background FromVectorImage(VectorImage vi)
		{
			return new Background
			{
				vectorImage = vi
			};
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004E8C0 File Offset: 0x0004CAC0
		internal static Background FromObject(object obj)
		{
			Texture2D texture = obj as Texture2D;
			bool flag = texture != null;
			Background background;
			if (flag)
			{
				background = Background.FromTexture2D(texture);
			}
			else
			{
				RenderTexture renderTexture = obj as RenderTexture;
				bool flag2 = renderTexture != null;
				if (flag2)
				{
					background = Background.FromRenderTexture(renderTexture);
				}
				else
				{
					Sprite sprite = obj as Sprite;
					bool flag3 = sprite != null;
					if (flag3)
					{
						background = Background.FromSprite(sprite);
					}
					else
					{
						VectorImage vectorImage = obj as VectorImage;
						bool flag4 = vectorImage != null;
						if (flag4)
						{
							background = Background.FromVectorImage(vectorImage);
						}
						else
						{
							background = default(Background);
						}
					}
				}
			}
			return background;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0004E958 File Offset: 0x0004CB58
		public bool IsEmpty()
		{
			return this.texture == null && this.sprite == null && this.vectorImage == null && this.renderTexture == null;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004E9A4 File Offset: 0x0004CBA4
		public static bool operator ==(Background lhs, Background rhs)
		{
			return lhs.texture == rhs.texture && lhs.sprite == rhs.sprite && lhs.renderTexture == rhs.renderTexture && lhs.vectorImage == rhs.vectorImage;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0004EA0C File Offset: 0x0004CC0C
		public static bool operator !=(Background lhs, Background rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004EA28 File Offset: 0x0004CC28
		public static implicit operator Background(Texture2D v)
		{
			return Background.FromTexture2D(v);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0004EA40 File Offset: 0x0004CC40
		public bool Equals(Background other)
		{
			return other == this;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0004EA60 File Offset: 0x0004CC60
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Background);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Background v = (Background)obj;
				flag2 = v == this;
			}
			return flag2;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0004EA9C File Offset: 0x0004CC9C
		public override int GetHashCode()
		{
			int hashCode = 851985039;
			bool flag = this.texture != null;
			if (flag)
			{
				hashCode = hashCode * -1521134295 + this.texture.GetHashCode();
			}
			bool flag2 = this.sprite != null;
			if (flag2)
			{
				hashCode = hashCode * -1521134295 + this.sprite.GetHashCode();
			}
			bool flag3 = this.renderTexture != null;
			if (flag3)
			{
				hashCode = hashCode * -1521134295 + this.renderTexture.GetHashCode();
			}
			bool flag4 = this.vectorImage != null;
			if (flag4)
			{
				hashCode = hashCode * -1521134295 + this.vectorImage.GetHashCode();
			}
			return hashCode;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0004EB40 File Offset: 0x0004CD40
		public override string ToString()
		{
			bool flag = this.texture != null;
			string text;
			if (flag)
			{
				text = this.texture.ToString();
			}
			else
			{
				bool flag2 = this.sprite != null;
				if (flag2)
				{
					text = this.sprite.ToString();
				}
				else
				{
					bool flag3 = this.renderTexture != null;
					if (flag3)
					{
						text = this.renderTexture.ToString();
					}
					else
					{
						bool flag4 = this.vectorImage != null;
						if (flag4)
						{
							text = this.vectorImage.ToString();
						}
						else
						{
							text = "";
						}
					}
				}
			}
			return text;
		}

		// Token: 0x04000AF7 RID: 2807
		private Texture2D m_Texture;

		// Token: 0x04000AF8 RID: 2808
		private Sprite m_Sprite;

		// Token: 0x04000AF9 RID: 2809
		private RenderTexture m_RenderTexture;

		// Token: 0x04000AFA RID: 2810
		private VectorImage m_VectorImage;

		// Token: 0x020002C0 RID: 704
		internal class PropertyBag : ContainerPropertyBag<Background>
		{
			// Token: 0x06001306 RID: 4870 RVA: 0x0004EBD1 File Offset: 0x0004CDD1
			public PropertyBag()
			{
				base.AddProperty<Texture2D>(new Background.PropertyBag.TextureProperty());
				base.AddProperty<Sprite>(new Background.PropertyBag.SpriteProperty());
				base.AddProperty<RenderTexture>(new Background.PropertyBag.RenderTextureProperty());
				base.AddProperty<VectorImage>(new Background.PropertyBag.VectorImageProperty());
			}

			// Token: 0x020002C1 RID: 705
			private class TextureProperty : Property<Background, Texture2D>
			{
				// Token: 0x170003A9 RID: 937
				// (get) Token: 0x06001307 RID: 4871 RVA: 0x0004EC0B File Offset: 0x0004CE0B
				public override string Name { get; } = "texture";

				// Token: 0x170003AA RID: 938
				// (get) Token: 0x06001308 RID: 4872 RVA: 0x0004EC13 File Offset: 0x0004CE13
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001309 RID: 4873 RVA: 0x0004EC1B File Offset: 0x0004CE1B
				public override Texture2D GetValue(ref Background container)
				{
					return container.texture;
				}

				// Token: 0x0600130A RID: 4874 RVA: 0x0004EC23 File Offset: 0x0004CE23
				public override void SetValue(ref Background container, Texture2D value)
				{
					container.texture = value;
				}
			}

			// Token: 0x020002C2 RID: 706
			private class SpriteProperty : Property<Background, Sprite>
			{
				// Token: 0x170003AB RID: 939
				// (get) Token: 0x0600130C RID: 4876 RVA: 0x0004EC48 File Offset: 0x0004CE48
				public override string Name { get; } = "sprite";

				// Token: 0x170003AC RID: 940
				// (get) Token: 0x0600130D RID: 4877 RVA: 0x0004EC50 File Offset: 0x0004CE50
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600130E RID: 4878 RVA: 0x0004EC58 File Offset: 0x0004CE58
				public override Sprite GetValue(ref Background container)
				{
					return container.sprite;
				}

				// Token: 0x0600130F RID: 4879 RVA: 0x0004EC60 File Offset: 0x0004CE60
				public override void SetValue(ref Background container, Sprite value)
				{
					container.sprite = value;
				}
			}

			// Token: 0x020002C3 RID: 707
			private class RenderTextureProperty : Property<Background, RenderTexture>
			{
				// Token: 0x170003AD RID: 941
				// (get) Token: 0x06001311 RID: 4881 RVA: 0x0004EC85 File Offset: 0x0004CE85
				public override string Name { get; } = "renderTexture";

				// Token: 0x170003AE RID: 942
				// (get) Token: 0x06001312 RID: 4882 RVA: 0x0004EC8D File Offset: 0x0004CE8D
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001313 RID: 4883 RVA: 0x0004EC95 File Offset: 0x0004CE95
				public override RenderTexture GetValue(ref Background container)
				{
					return container.renderTexture;
				}

				// Token: 0x06001314 RID: 4884 RVA: 0x0004EC9D File Offset: 0x0004CE9D
				public override void SetValue(ref Background container, RenderTexture value)
				{
					container.renderTexture = value;
				}
			}

			// Token: 0x020002C4 RID: 708
			private class VectorImageProperty : Property<Background, VectorImage>
			{
				// Token: 0x170003AF RID: 943
				// (get) Token: 0x06001316 RID: 4886 RVA: 0x0004ECC2 File Offset: 0x0004CEC2
				public override string Name { get; } = "vectorImage";

				// Token: 0x170003B0 RID: 944
				// (get) Token: 0x06001317 RID: 4887 RVA: 0x0004ECCA File Offset: 0x0004CECA
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001318 RID: 4888 RVA: 0x0004ECD2 File Offset: 0x0004CED2
				public override VectorImage GetValue(ref Background container)
				{
					return container.vectorImage;
				}

				// Token: 0x06001319 RID: 4889 RVA: 0x0004ECDA File Offset: 0x0004CEDA
				public override void SetValue(ref Background container, VectorImage value)
				{
					container.vectorImage = value;
				}
			}
		}
	}
}
