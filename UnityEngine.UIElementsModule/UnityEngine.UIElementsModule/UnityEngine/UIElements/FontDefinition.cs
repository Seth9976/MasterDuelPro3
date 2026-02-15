using System;
using Unity.Properties;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CF RID: 719
	public struct FontDefinition : IEquatable<FontDefinition>
	{
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0005A3A4 File Offset: 0x000585A4
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x0005A3BC File Offset: 0x000585BC
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				bool flag = value != null && this.fontAsset != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both Font and FontAsset on FontDefinition");
				}
				this.m_Font = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0005A3F8 File Offset: 0x000585F8
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x0005A410 File Offset: 0x00058610
		public FontAsset fontAsset
		{
			get
			{
				return this.m_FontAsset;
			}
			set
			{
				bool flag = value != null && this.font != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both Font and FontAsset on FontDefinition");
				}
				this.m_FontAsset = value;
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0005A44C File Offset: 0x0005864C
		public static FontDefinition FromFont(Font f)
		{
			return new FontDefinition
			{
				m_Font = f
			};
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0005A470 File Offset: 0x00058670
		public static FontDefinition FromSDFFont(FontAsset f)
		{
			return new FontDefinition
			{
				m_FontAsset = f
			};
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0005A494 File Offset: 0x00058694
		internal static FontDefinition FromObject(object obj)
		{
			Font font = obj as Font;
			bool flag = font != null;
			FontDefinition fontDefinition;
			if (flag)
			{
				fontDefinition = FontDefinition.FromFont(font);
			}
			else
			{
				FontAsset fontAsset = obj as FontAsset;
				bool flag2 = fontAsset != null;
				if (flag2)
				{
					fontDefinition = FontDefinition.FromSDFFont(fontAsset);
				}
				else
				{
					fontDefinition = default(FontDefinition);
				}
			}
			return fontDefinition;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0005A4E8 File Offset: 0x000586E8
		internal bool IsEmpty()
		{
			return this.m_Font == null && this.m_FontAsset == null;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0005A518 File Offset: 0x00058718
		public override string ToString()
		{
			bool flag = this.font != null;
			string text;
			if (flag)
			{
				text = string.Format("{0}", this.font);
			}
			else
			{
				text = string.Format("{0}", this.fontAsset);
			}
			return text;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0005A560 File Offset: 0x00058760
		public bool Equals(FontDefinition other)
		{
			return object.Equals(this.m_Font, other.m_Font) && object.Equals(this.m_FontAsset, other.m_FontAsset);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0005A59C File Offset: 0x0005879C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is FontDefinition)
			{
				FontDefinition other = (FontDefinition)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0005A5C8 File Offset: 0x000587C8
		public override int GetHashCode()
		{
			return (((this.m_Font != null) ? this.m_Font.GetHashCode() : 0) * 397) ^ ((this.m_FontAsset != null) ? this.m_FontAsset.GetHashCode() : 0);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0005A61C File Offset: 0x0005881C
		public static bool operator ==(FontDefinition left, FontDefinition right)
		{
			return left.Equals(right);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0005A638 File Offset: 0x00058838
		public static bool operator !=(FontDefinition left, FontDefinition right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000B46 RID: 2886
		private Font m_Font;

		// Token: 0x04000B47 RID: 2887
		private FontAsset m_FontAsset;

		// Token: 0x020002D0 RID: 720
		internal class PropertyBag : ContainerPropertyBag<FontDefinition>
		{
			// Token: 0x060013F4 RID: 5108 RVA: 0x0005A655 File Offset: 0x00058855
			public PropertyBag()
			{
				base.AddProperty<Font>(new FontDefinition.PropertyBag.FontProperty());
				base.AddProperty<FontAsset>(new FontDefinition.PropertyBag.FontAssetProperty());
			}

			// Token: 0x020002D1 RID: 721
			private class FontProperty : Property<FontDefinition, Font>
			{
				// Token: 0x1700040C RID: 1036
				// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0005A677 File Offset: 0x00058877
				public override string Name { get; } = "font";

				// Token: 0x1700040D RID: 1037
				// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0005A67F File Offset: 0x0005887F
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060013F7 RID: 5111 RVA: 0x0005A687 File Offset: 0x00058887
				public override Font GetValue(ref FontDefinition container)
				{
					return container.font;
				}

				// Token: 0x060013F8 RID: 5112 RVA: 0x0005A68F File Offset: 0x0005888F
				public override void SetValue(ref FontDefinition container, Font value)
				{
					container.font = value;
				}
			}

			// Token: 0x020002D2 RID: 722
			private class FontAssetProperty : Property<FontDefinition, FontAsset>
			{
				// Token: 0x1700040E RID: 1038
				// (get) Token: 0x060013FA RID: 5114 RVA: 0x0005A6B4 File Offset: 0x000588B4
				public override string Name { get; } = "fontAsset";

				// Token: 0x1700040F RID: 1039
				// (get) Token: 0x060013FB RID: 5115 RVA: 0x0005A6BC File Offset: 0x000588BC
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060013FC RID: 5116 RVA: 0x0005A6C4 File Offset: 0x000588C4
				public override FontAsset GetValue(ref FontDefinition container)
				{
					return container.fontAsset;
				}

				// Token: 0x060013FD RID: 5117 RVA: 0x0005A6CC File Offset: 0x000588CC
				public override void SetValue(ref FontDefinition container, FontAsset value)
				{
					container.fontAsset = value;
				}
			}
		}
	}
}
