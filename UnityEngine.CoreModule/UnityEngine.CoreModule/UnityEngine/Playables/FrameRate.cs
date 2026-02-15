using System;
using System.Globalization;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x020002FD RID: 765
	[UsedByNativeCode("FrameRate")]
	[VisibleToOtherModules(new string[] { "UnityEngine.DirectorModule" })]
	[NativeHeader("Runtime/Director/Core/FrameRate.h")]
	internal struct FrameRate : IEquatable<FrameRate>
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0002CE17 File Offset: 0x0002B017
		public bool dropFrame
		{
			get
			{
				return this.m_Rate < 0;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0002CE22 File Offset: 0x0002B022
		public double rate
		{
			get
			{
				return this.dropFrame ? ((double)(-(double)this.m_Rate) * 0.999000999000999) : ((double)this.m_Rate);
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0002CE47 File Offset: 0x0002B047
		public FrameRate(uint frameRate = 0U, bool drop = false)
		{
			this.m_Rate = (int)((drop ? uint.MaxValue : 1U) * frameRate);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0002CE5C File Offset: 0x0002B05C
		public bool IsValid()
		{
			return this.m_Rate != 0;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0002CE78 File Offset: 0x0002B078
		public bool Equals(FrameRate other)
		{
			return this.m_Rate == other.m_Rate;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0002CE98 File Offset: 0x0002B098
		public override bool Equals(object obj)
		{
			return obj is FrameRate && this.Equals((FrameRate)obj);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0002CEC1 File Offset: 0x0002B0C1
		public static bool operator ==(FrameRate a, FrameRate b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0002CECC File Offset: 0x0002B0CC
		public override int GetHashCode()
		{
			return this.m_Rate;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0002CEE4 File Offset: 0x0002B0E4
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0002CF00 File Offset: 0x0002B100
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = (this.dropFrame ? "F2" : "F0");
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("{0} Fps", new object[] { this.rate.ToString(format, formatProvider) });
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0002CF6C File Offset: 0x0002B16C
		internal static FrameRate DoubleToFrameRate(double framerate)
		{
			uint fullFrameRate = (uint)Math.Ceiling(framerate);
			bool flag = fullFrameRate <= 0U;
			FrameRate frameRate;
			if (flag)
			{
				frameRate = new FrameRate(1U, false);
			}
			else
			{
				FrameRate dropFrameRate = new FrameRate(fullFrameRate, true);
				bool flag2 = Math.Abs(framerate - dropFrameRate.rate) < Math.Abs(framerate - fullFrameRate);
				if (flag2)
				{
					frameRate = dropFrameRate;
				}
				else
				{
					frameRate = new FrameRate(fullFrameRate, false);
				}
			}
			return frameRate;
		}

		// Token: 0x04000804 RID: 2052
		[Ignore]
		public static readonly FrameRate k_24Fps = new FrameRate(24U, false);

		// Token: 0x04000805 RID: 2053
		[Ignore]
		public static readonly FrameRate k_23_976Fps = new FrameRate(24U, true);

		// Token: 0x04000806 RID: 2054
		[Ignore]
		public static readonly FrameRate k_25Fps = new FrameRate(25U, false);

		// Token: 0x04000807 RID: 2055
		[Ignore]
		public static readonly FrameRate k_30Fps = new FrameRate(30U, false);

		// Token: 0x04000808 RID: 2056
		[Ignore]
		public static readonly FrameRate k_29_97Fps = new FrameRate(30U, true);

		// Token: 0x04000809 RID: 2057
		[Ignore]
		public static readonly FrameRate k_50Fps = new FrameRate(50U, false);

		// Token: 0x0400080A RID: 2058
		[Ignore]
		public static readonly FrameRate k_60Fps = new FrameRate(60U, false);

		// Token: 0x0400080B RID: 2059
		[Ignore]
		public static readonly FrameRate k_59_94Fps = new FrameRate(60U, true);

		// Token: 0x0400080C RID: 2060
		[SerializeField]
		private int m_Rate;
	}
}
