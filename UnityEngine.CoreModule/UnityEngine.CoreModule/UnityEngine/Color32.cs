using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000143 RID: 323
	[DefaultMember("Item")]
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit)]
	public struct Color32 : IEquatable<Color32>, IFormattable
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x0001A413 File Offset: 0x00018613
		public Color32(byte r, byte g, byte b, byte a)
		{
			this.rgba = 0;
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0001A43C File Offset: 0x0001863C
		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)Mathf.Round(Mathf.Clamp01(c.r) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.g) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.b) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.a) * 255f));
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0001A4B0 File Offset: 0x000186B0
		public static implicit operator Color(Color32 c)
		{
			return new Color((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0001A4FC File Offset: 0x000186FC
		public override int GetHashCode()
		{
			return this.rgba.GetHashCode();
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0001A51C File Offset: 0x0001871C
		public override bool Equals(object other)
		{
			Color32 color;
			bool flag;
			if (other is Color32)
			{
				color = (Color32)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(color);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0001A550 File Offset: 0x00018750
		public bool Equals(Color32 other)
		{
			return this.rgba == other.rgba;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0001A570 File Offset: 0x00018770
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0001A58C File Offset: 0x0001878C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
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

		// Token: 0x04000571 RID: 1393
		[Ignore(DoesNotContributeToSize = true)]
		[FieldOffset(0)]
		private int rgba;

		// Token: 0x04000572 RID: 1394
		[FieldOffset(0)]
		public byte r;

		// Token: 0x04000573 RID: 1395
		[FieldOffset(1)]
		public byte g;

		// Token: 0x04000574 RID: 1396
		[FieldOffset(2)]
		public byte b;

		// Token: 0x04000575 RID: 1397
		[FieldOffset(3)]
		public byte a;
	}
}
