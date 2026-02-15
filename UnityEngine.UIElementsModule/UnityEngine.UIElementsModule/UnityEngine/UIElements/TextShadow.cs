using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000455 RID: 1109
	public struct TextShadow : IEquatable<TextShadow>
	{
		// Token: 0x060020C7 RID: 8391 RVA: 0x0007885C File Offset: 0x00076A5C
		public override bool Equals(object obj)
		{
			return obj is TextShadow && this.Equals((TextShadow)obj);
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00078888 File Offset: 0x00076A88
		public bool Equals(TextShadow other)
		{
			return other.offset == this.offset && other.blurRadius == this.blurRadius && other.color == this.color;
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000788D0 File Offset: 0x00076AD0
		public override int GetHashCode()
		{
			int hashCode = 1500536833;
			hashCode = hashCode * -1521134295 + this.offset.GetHashCode();
			hashCode = hashCode * -1521134295 + this.blurRadius.GetHashCode();
			return hashCode * -1521134295 + this.color.GetHashCode();
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x00078934 File Offset: 0x00076B34
		public static bool operator ==(TextShadow style1, TextShadow style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00078950 File Offset: 0x00076B50
		public static bool operator !=(TextShadow style1, TextShadow style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0007896C File Offset: 0x00076B6C
		public override string ToString()
		{
			return string.Format("offset={0}, blurRadius={1}, color={2}", this.offset, this.blurRadius, this.color);
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000789AC File Offset: 0x00076BAC
		internal static TextShadow LerpUnclamped(TextShadow a, TextShadow b, float t)
		{
			return new TextShadow
			{
				offset = Vector2.LerpUnclamped(a.offset, b.offset, t),
				blurRadius = Mathf.LerpUnclamped(a.blurRadius, b.blurRadius, t),
				color = Color.LerpUnclamped(a.color, b.color, t)
			};
		}

		// Token: 0x04000E89 RID: 3721
		public Vector2 offset;

		// Token: 0x04000E8A RID: 3722
		public float blurRadius;

		// Token: 0x04000E8B RID: 3723
		public Color color;

		// Token: 0x02000456 RID: 1110
		internal class PropertyBag : ContainerPropertyBag<TextShadow>
		{
			// Token: 0x060020CE RID: 8398 RVA: 0x00078A12 File Offset: 0x00076C12
			public PropertyBag()
			{
				base.AddProperty<Vector2>(new TextShadow.PropertyBag.OffsetProperty());
				base.AddProperty<float>(new TextShadow.PropertyBag.BlurRadiusProperty());
				base.AddProperty<Color>(new TextShadow.PropertyBag.ColorProperty());
			}

			// Token: 0x02000457 RID: 1111
			private class OffsetProperty : Property<TextShadow, Vector2>
			{
				// Token: 0x170008E4 RID: 2276
				// (get) Token: 0x060020CF RID: 8399 RVA: 0x00078A40 File Offset: 0x00076C40
				public override string Name { get; } = "offset";

				// Token: 0x170008E5 RID: 2277
				// (get) Token: 0x060020D0 RID: 8400 RVA: 0x00078A48 File Offset: 0x00076C48
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060020D1 RID: 8401 RVA: 0x00078A50 File Offset: 0x00076C50
				public override Vector2 GetValue(ref TextShadow container)
				{
					return container.offset;
				}

				// Token: 0x060020D2 RID: 8402 RVA: 0x00078A58 File Offset: 0x00076C58
				public override void SetValue(ref TextShadow container, Vector2 value)
				{
					container.offset = value;
				}
			}

			// Token: 0x02000458 RID: 1112
			private class BlurRadiusProperty : Property<TextShadow, float>
			{
				// Token: 0x170008E6 RID: 2278
				// (get) Token: 0x060020D4 RID: 8404 RVA: 0x00078A7C File Offset: 0x00076C7C
				public override string Name { get; } = "blurRadius";

				// Token: 0x170008E7 RID: 2279
				// (get) Token: 0x060020D5 RID: 8405 RVA: 0x00078A84 File Offset: 0x00076C84
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060020D6 RID: 8406 RVA: 0x00078A8C File Offset: 0x00076C8C
				public override float GetValue(ref TextShadow container)
				{
					return container.blurRadius;
				}

				// Token: 0x060020D7 RID: 8407 RVA: 0x00078A94 File Offset: 0x00076C94
				public override void SetValue(ref TextShadow container, float value)
				{
					container.blurRadius = value;
				}
			}

			// Token: 0x02000459 RID: 1113
			private class ColorProperty : Property<TextShadow, Color>
			{
				// Token: 0x170008E8 RID: 2280
				// (get) Token: 0x060020D9 RID: 8409 RVA: 0x00078AB8 File Offset: 0x00076CB8
				public override string Name { get; } = "color";

				// Token: 0x170008E9 RID: 2281
				// (get) Token: 0x060020DA RID: 8410 RVA: 0x00078AC0 File Offset: 0x00076CC0
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060020DB RID: 8411 RVA: 0x00078AC8 File Offset: 0x00076CC8
				public override Color GetValue(ref TextShadow container)
				{
					return container.color;
				}

				// Token: 0x060020DC RID: 8412 RVA: 0x00078AD0 File Offset: 0x00076CD0
				public override void SetValue(ref TextShadow container, Color value)
				{
					container.color = value;
				}
			}
		}
	}
}
