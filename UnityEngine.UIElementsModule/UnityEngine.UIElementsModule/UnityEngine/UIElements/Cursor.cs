using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000176 RID: 374
	public struct Cursor : IEquatable<Cursor>
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00035BB7 File Offset: 0x00033DB7
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x00035BBF File Offset: 0x00033DBF
		public Texture2D texture { readonly get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00035BC8 File Offset: 0x00033DC8
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x00035BD0 File Offset: 0x00033DD0
		public Vector2 hotspot { readonly get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00035BD9 File Offset: 0x00033DD9
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x00035BE1 File Offset: 0x00033DE1
		internal int defaultCursorId { readonly get; set; }

		// Token: 0x06000B08 RID: 2824 RVA: 0x00035BEC File Offset: 0x00033DEC
		public override bool Equals(object obj)
		{
			return obj is Cursor && this.Equals((Cursor)obj);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00035C18 File Offset: 0x00033E18
		public bool Equals(Cursor other)
		{
			return EqualityComparer<Texture2D>.Default.Equals(this.texture, other.texture) && this.hotspot.Equals(other.hotspot) && this.defaultCursorId == other.defaultCursorId;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00035C6C File Offset: 0x00033E6C
		public override int GetHashCode()
		{
			int hashCode = 1500536833;
			hashCode = hashCode * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(this.texture);
			hashCode = hashCode * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.hotspot);
			return hashCode * -1521134295 + this.defaultCursorId.GetHashCode();
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00035CD0 File Offset: 0x00033ED0
		public static bool operator ==(Cursor style1, Cursor style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00035CEC File Offset: 0x00033EEC
		public override string ToString()
		{
			return string.Format("texture={0}, hotspot={1}", this.texture, this.hotspot);
		}

		// Token: 0x02000177 RID: 375
		internal class PropertyBag : ContainerPropertyBag<Cursor>
		{
			// Token: 0x06000B0D RID: 2829 RVA: 0x00035D19 File Offset: 0x00033F19
			public PropertyBag()
			{
				base.AddProperty<Texture2D>(new Cursor.PropertyBag.TextureProperty());
				base.AddProperty<Vector2>(new Cursor.PropertyBag.HotspotProperty());
				base.AddProperty<int>(new Cursor.PropertyBag.DefaultCursorIdProperty());
			}

			// Token: 0x02000178 RID: 376
			private class TextureProperty : Property<Cursor, Texture2D>
			{
				// Token: 0x170001EF RID: 495
				// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00035D47 File Offset: 0x00033F47
				public override string Name { get; } = "texture";

				// Token: 0x170001F0 RID: 496
				// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00035D4F File Offset: 0x00033F4F
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000B10 RID: 2832 RVA: 0x00035D57 File Offset: 0x00033F57
				public override Texture2D GetValue(ref Cursor container)
				{
					return container.texture;
				}

				// Token: 0x06000B11 RID: 2833 RVA: 0x00035D5F File Offset: 0x00033F5F
				public override void SetValue(ref Cursor container, Texture2D value)
				{
					container.texture = value;
				}
			}

			// Token: 0x02000179 RID: 377
			private class HotspotProperty : Property<Cursor, Vector2>
			{
				// Token: 0x170001F1 RID: 497
				// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00035D84 File Offset: 0x00033F84
				public override string Name { get; } = "hotspot";

				// Token: 0x170001F2 RID: 498
				// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00035D8C File Offset: 0x00033F8C
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000B15 RID: 2837 RVA: 0x00035D94 File Offset: 0x00033F94
				public override Vector2 GetValue(ref Cursor container)
				{
					return container.hotspot;
				}

				// Token: 0x06000B16 RID: 2838 RVA: 0x00035D9C File Offset: 0x00033F9C
				public override void SetValue(ref Cursor container, Vector2 value)
				{
					container.hotspot = value;
				}
			}

			// Token: 0x0200017A RID: 378
			private class DefaultCursorIdProperty : Property<Cursor, int>
			{
				// Token: 0x170001F3 RID: 499
				// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00035DC1 File Offset: 0x00033FC1
				public override string Name { get; } = "defaultCursorId";

				// Token: 0x170001F4 RID: 500
				// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00035DC9 File Offset: 0x00033FC9
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000B1A RID: 2842 RVA: 0x00035DD1 File Offset: 0x00033FD1
				public override int GetValue(ref Cursor container)
				{
					return container.defaultCursorId;
				}

				// Token: 0x06000B1B RID: 2843 RVA: 0x00035DD9 File Offset: 0x00033FD9
				public override void SetValue(ref Cursor container, int value)
				{
					container.defaultCursorId = value;
				}
			}
		}
	}
}
