using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000A RID: 10
	public struct BackgroundPosition : IEquatable<BackgroundPosition>
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000029FF File Offset: 0x00000BFF
		public BackgroundPosition(BackgroundPositionKeyword keyword)
		{
			this.keyword = keyword;
			this.offset = new Length(0f);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A19 File Offset: 0x00000C19
		public BackgroundPosition(BackgroundPositionKeyword keyword, Length offset)
		{
			this.keyword = keyword;
			this.offset = offset;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A2C File Offset: 0x00000C2C
		internal static BackgroundPosition Initial()
		{
			return BackgroundPropertyHelper.ConvertScaleModeToBackgroundPosition(ScaleMode.StretchToFill);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A44 File Offset: 0x00000C44
		public override bool Equals(object obj)
		{
			return obj is BackgroundPosition && this.Equals((BackgroundPosition)obj);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A70 File Offset: 0x00000C70
		public bool Equals(BackgroundPosition other)
		{
			return other.offset == this.offset && other.keyword == this.keyword;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public override int GetHashCode()
		{
			int hashCode = 1500536833;
			hashCode = hashCode * -1521134295 + this.keyword.GetHashCode();
			return hashCode * -1521134295 + this.offset.GetHashCode();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public static bool operator ==(BackgroundPosition style1, BackgroundPosition style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B14 File Offset: 0x00000D14
		public static bool operator !=(BackgroundPosition style1, BackgroundPosition style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B30 File Offset: 0x00000D30
		public override string ToString()
		{
			return string.Format("(type:{0} x:{1})", this.keyword, this.offset);
		}

		// Token: 0x04000018 RID: 24
		public BackgroundPositionKeyword keyword;

		// Token: 0x04000019 RID: 25
		public Length offset;

		// Token: 0x0200000B RID: 11
		internal class PropertyBag : ContainerPropertyBag<BackgroundPosition>
		{
			// Token: 0x06000034 RID: 52 RVA: 0x00002B62 File Offset: 0x00000D62
			public PropertyBag()
			{
				base.AddProperty<BackgroundPositionKeyword>(new BackgroundPosition.PropertyBag.KeywordProperty());
				base.AddProperty<Length>(new BackgroundPosition.PropertyBag.OffsetProperty());
			}

			// Token: 0x0200000C RID: 12
			private class KeywordProperty : Property<BackgroundPosition, BackgroundPositionKeyword>
			{
				// Token: 0x17000008 RID: 8
				// (get) Token: 0x06000035 RID: 53 RVA: 0x00002B84 File Offset: 0x00000D84
				public override string Name { get; } = "keyword";

				// Token: 0x17000009 RID: 9
				// (get) Token: 0x06000036 RID: 54 RVA: 0x00002B8C File Offset: 0x00000D8C
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000037 RID: 55 RVA: 0x00002B94 File Offset: 0x00000D94
				public override BackgroundPositionKeyword GetValue(ref BackgroundPosition container)
				{
					return container.keyword;
				}

				// Token: 0x06000038 RID: 56 RVA: 0x00002B9C File Offset: 0x00000D9C
				public override void SetValue(ref BackgroundPosition container, BackgroundPositionKeyword value)
				{
					container.keyword = value;
				}
			}

			// Token: 0x0200000D RID: 13
			private class OffsetProperty : Property<BackgroundPosition, Length>
			{
				// Token: 0x1700000A RID: 10
				// (get) Token: 0x0600003A RID: 58 RVA: 0x00002BC0 File Offset: 0x00000DC0
				public override string Name { get; } = "offset";

				// Token: 0x1700000B RID: 11
				// (get) Token: 0x0600003B RID: 59 RVA: 0x00002BC8 File Offset: 0x00000DC8
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600003C RID: 60 RVA: 0x00002BD0 File Offset: 0x00000DD0
				public override Length GetValue(ref BackgroundPosition container)
				{
					return container.offset;
				}

				// Token: 0x0600003D RID: 61 RVA: 0x00002BD8 File Offset: 0x00000DD8
				public override void SetValue(ref BackgroundPosition container, Length value)
				{
					container.offset = value;
				}
			}
		}
	}
}
