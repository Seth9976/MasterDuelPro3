using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000013 RID: 19
	public struct BackgroundSize : IEquatable<BackgroundSize>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002F4C File Offset: 0x0000114C
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002F64 File Offset: 0x00001164
		public BackgroundSizeType sizeType
		{
			get
			{
				return this.m_SizeType;
			}
			set
			{
				this.m_SizeType = value;
				this.m_X = new Length(0f);
				this.m_Y = new Length(0f);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F90 File Offset: 0x00001190
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002FA8 File Offset: 0x000011A8
		public Length x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
				this.m_SizeType = BackgroundSizeType.Length;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002FBC File Offset: 0x000011BC
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002FD4 File Offset: 0x000011D4
		public Length y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
				this.m_SizeType = BackgroundSizeType.Length;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002FE5 File Offset: 0x000011E5
		public BackgroundSize(Length sizeX, Length sizeY)
		{
			this.m_SizeType = BackgroundSizeType.Length;
			this.m_X = sizeX;
			this.m_Y = sizeY;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F64 File Offset: 0x00001164
		public BackgroundSize(BackgroundSizeType sizeType)
		{
			this.m_SizeType = sizeType;
			this.m_X = new Length(0f);
			this.m_Y = new Length(0f);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003000 File Offset: 0x00001200
		internal static BackgroundSize Initial()
		{
			return BackgroundPropertyHelper.ConvertScaleModeToBackgroundSize(ScaleMode.StretchToFill);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003018 File Offset: 0x00001218
		public override bool Equals(object obj)
		{
			return obj is BackgroundSize && this.Equals((BackgroundSize)obj);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003044 File Offset: 0x00001244
		public bool Equals(BackgroundSize other)
		{
			return other.x == this.x && other.y == this.y && other.sizeType == this.sizeType;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003090 File Offset: 0x00001290
		public override int GetHashCode()
		{
			int hashCode = 1500536833;
			hashCode = hashCode * -1521134295 + this.m_SizeType.GetHashCode();
			hashCode = hashCode * -1521134295 + this.m_X.GetHashCode();
			return hashCode * -1521134295 + this.m_Y.GetHashCode();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000030F8 File Offset: 0x000012F8
		public static bool operator ==(BackgroundSize style1, BackgroundSize style2)
		{
			return style1.Equals(style2);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003114 File Offset: 0x00001314
		public static bool operator !=(BackgroundSize style1, BackgroundSize style2)
		{
			return !(style1 == style2);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003130 File Offset: 0x00001330
		public override string ToString()
		{
			return string.Format("(sizeType:{0} x:{1}, y:{2})", this.sizeType, this.x, this.y);
		}

		// Token: 0x04000024 RID: 36
		private BackgroundSizeType m_SizeType;

		// Token: 0x04000025 RID: 37
		private Length m_X;

		// Token: 0x04000026 RID: 38
		private Length m_Y;

		// Token: 0x02000014 RID: 20
		internal class PropertyBag : ContainerPropertyBag<BackgroundSize>
		{
			// Token: 0x06000065 RID: 101 RVA: 0x0000316D File Offset: 0x0000136D
			public PropertyBag()
			{
				base.AddProperty<BackgroundSizeType>(new BackgroundSize.PropertyBag.SizeTypeProperty());
				base.AddProperty<Length>(new BackgroundSize.PropertyBag.XProperty());
				base.AddProperty<Length>(new BackgroundSize.PropertyBag.YProperty());
			}

			// Token: 0x02000015 RID: 21
			private class SizeTypeProperty : Property<BackgroundSize, BackgroundSizeType>
			{
				// Token: 0x17000013 RID: 19
				// (get) Token: 0x06000066 RID: 102 RVA: 0x0000319B File Offset: 0x0000139B
				public override string Name { get; } = "sizeType";

				// Token: 0x17000014 RID: 20
				// (get) Token: 0x06000067 RID: 103 RVA: 0x000031A3 File Offset: 0x000013A3
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000068 RID: 104 RVA: 0x000031AB File Offset: 0x000013AB
				public override BackgroundSizeType GetValue(ref BackgroundSize container)
				{
					return container.sizeType;
				}

				// Token: 0x06000069 RID: 105 RVA: 0x000031B3 File Offset: 0x000013B3
				public override void SetValue(ref BackgroundSize container, BackgroundSizeType value)
				{
					container.sizeType = value;
				}
			}

			// Token: 0x02000016 RID: 22
			private class XProperty : Property<BackgroundSize, Length>
			{
				// Token: 0x17000015 RID: 21
				// (get) Token: 0x0600006B RID: 107 RVA: 0x000031D8 File Offset: 0x000013D8
				public override string Name { get; } = "x";

				// Token: 0x17000016 RID: 22
				// (get) Token: 0x0600006C RID: 108 RVA: 0x000031E0 File Offset: 0x000013E0
				public override bool IsReadOnly { get; } = false;

				// Token: 0x0600006D RID: 109 RVA: 0x000031E8 File Offset: 0x000013E8
				public override Length GetValue(ref BackgroundSize container)
				{
					return container.x;
				}

				// Token: 0x0600006E RID: 110 RVA: 0x000031F0 File Offset: 0x000013F0
				public override void SetValue(ref BackgroundSize container, Length value)
				{
					container.x = value;
				}
			}

			// Token: 0x02000017 RID: 23
			private class YProperty : Property<BackgroundSize, Length>
			{
				// Token: 0x17000017 RID: 23
				// (get) Token: 0x06000070 RID: 112 RVA: 0x00003215 File Offset: 0x00001415
				public override string Name { get; } = "y";

				// Token: 0x17000018 RID: 24
				// (get) Token: 0x06000071 RID: 113 RVA: 0x0000321D File Offset: 0x0000141D
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06000072 RID: 114 RVA: 0x00003225 File Offset: 0x00001425
				public override Length GetValue(ref BackgroundSize container)
				{
					return container.y;
				}

				// Token: 0x06000073 RID: 115 RVA: 0x0000322D File Offset: 0x0000142D
				public override void SetValue(ref BackgroundSize container, Length value)
				{
					container.y = value;
				}
			}
		}
	}
}
