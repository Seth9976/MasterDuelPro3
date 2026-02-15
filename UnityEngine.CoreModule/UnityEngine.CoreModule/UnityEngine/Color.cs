using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000142 RID: 322
	[NativeHeader("Runtime/Math/Color.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("ColorRGBAf")]
	[DefaultMember("Item")]
	public struct Color : IEquatable<Color>, IFormattable
	{
		// Token: 0x06000D5A RID: 3418 RVA: 0x00019CC3 File Offset: 0x00017EC3
		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00019CE3 File Offset: 0x00017EE3
		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = 1f;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00019D08 File Offset: 0x00017F08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00019D24 File Offset: 0x00017F24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F3";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r.ToString(format, formatProvider),
				this.g.ToString(format, formatProvider),
				this.b.ToString(format, formatProvider),
				this.a.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00019DAC File Offset: 0x00017FAC
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00019DD8 File Offset: 0x00017FD8
		public override bool Equals(object other)
		{
			Color color;
			bool flag;
			if (other is Color)
			{
				color = (Color)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(color);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00019E0C File Offset: 0x0001800C
		public bool Equals(Color other)
		{
			return this.r.Equals(other.r) && this.g.Equals(other.g) && this.b.Equals(other.b) && this.a.Equals(other.a);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00019E6C File Offset: 0x0001806C
		public static Color operator +(Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00019EB8 File Offset: 0x000180B8
		public static Color operator -(Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00019F04 File Offset: 0x00018104
		public static Color operator *(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00019F50 File Offset: 0x00018150
		public static Color operator *(Color a, float b)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00019F88 File Offset: 0x00018188
		public static Color operator *(float b, Color a)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00019FC0 File Offset: 0x000181C0
		public static bool operator ==(Color lhs, Color rhs)
		{
			return lhs == rhs;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00019FE4 File Offset: 0x000181E4
		public static bool operator !=(Color lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0001A000 File Offset: 0x00018200
		public static Color Lerp(Color a, Color b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0001A078 File Offset: 0x00018278
		public static Color LerpUnclamped(Color a, Color b, float t)
		{
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0001A0E8 File Offset: 0x000182E8
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal Color RGBMultiplied(float multiplier)
		{
			return new Color(this.r * multiplier, this.g * multiplier, this.b * multiplier, this.a);
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0001A120 File Offset: 0x00018320
		public static Color red
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 0f, 0f, 1f);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0001A14C File Offset: 0x0001834C
		public static Color green
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 1f, 0f, 1f);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0001A178 File Offset: 0x00018378
		public static Color blue
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 0f, 1f, 1f);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0001A1A4 File Offset: 0x000183A4
		public static Color white
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0001A1D0 File Offset: 0x000183D0
		public static Color black
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0001A1FC File Offset: 0x000183FC
		public static Color yellow
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 0.92156863f, 0.015686275f, 1f);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0001A228 File Offset: 0x00018428
		public static Color cyan
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 1f, 1f, 1f);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0001A254 File Offset: 0x00018454
		public static Color magenta
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 0f, 1f, 1f);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0001A280 File Offset: 0x00018480
		public static Color gray
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0001A2AC File Offset: 0x000184AC
		public static Color grey
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0001A2D8 File Offset: 0x000184D8
		public static Color clear
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0001A304 File Offset: 0x00018504
		public Color linear
		{
			get
			{
				return new Color(Mathf.GammaToLinearSpace(this.r), Mathf.GammaToLinearSpace(this.g), Mathf.GammaToLinearSpace(this.b), this.a);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0001A344 File Offset: 0x00018544
		public Color gamma
		{
			get
			{
				return new Color(Mathf.LinearToGammaSpace(this.r), Mathf.LinearToGammaSpace(this.g), Mathf.LinearToGammaSpace(this.b), this.a);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0001A384 File Offset: 0x00018584
		public float maxColorComponent
		{
			get
			{
				return Mathf.Max(Mathf.Max(this.r, this.g), this.b);
			}
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0001A3B4 File Offset: 0x000185B4
		public static implicit operator Vector4(Color c)
		{
			return new Vector4(c.r, c.g, c.b, c.a);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0001A3E4 File Offset: 0x000185E4
		public static implicit operator Color(Vector4 v)
		{
			return new Color(v.x, v.y, v.z, v.w);
		}

		// Token: 0x0400056D RID: 1389
		public float r;

		// Token: 0x0400056E RID: 1390
		public float g;

		// Token: 0x0400056F RID: 1391
		public float b;

		// Token: 0x04000570 RID: 1392
		public float a;
	}
}
