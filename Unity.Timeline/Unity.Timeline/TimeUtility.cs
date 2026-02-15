using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000075 RID: 117
	internal static class TimeUtility
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0000AF3E File Offset: 0x0000913E
		private static void ValidateFrameRate(double frameRate)
		{
			if (frameRate <= TimeUtility.kTimeEpsilon)
			{
				throw new ArgumentException("frame rate cannot be 0 or negative");
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000AF54 File Offset: 0x00009154
		public static int ToFrames(double time, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			time = Math.Min(Math.Max(time, -TimeUtility.k_MaxTimelineDurationInSeconds), TimeUtility.k_MaxTimelineDurationInSeconds);
			double tolerance = TimeUtility.GetEpsilon(time, frameRate);
			if (time < 0.0)
			{
				return (int)Math.Ceiling(time * frameRate - tolerance);
			}
			return (int)Math.Floor(time * frameRate + tolerance);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000AFAA File Offset: 0x000091AA
		public static double ToExactFrames(double time, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			return time * frameRate;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000AFB5 File Offset: 0x000091B5
		public static double FromFrames(int frames, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			return (double)frames / frameRate;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000AFC1 File Offset: 0x000091C1
		public static double FromFrames(double frames, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			return frames / frameRate;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000AFCC File Offset: 0x000091CC
		public static bool OnFrameBoundary(double time, double frameRate)
		{
			return TimeUtility.OnFrameBoundary(time, frameRate, TimeUtility.GetEpsilon(time, frameRate));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000AFDC File Offset: 0x000091DC
		public static double GetEpsilon(double time, double frameRate)
		{
			return Math.Max(Math.Abs(time), 1.0) * frameRate * TimeUtility.kTimeEpsilon;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000AFFA File Offset: 0x000091FA
		public static int PreviousFrame(double time, double frameRate)
		{
			return Math.Max(0, TimeUtility.ToFrames(time, frameRate) - 1);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000B00B File Offset: 0x0000920B
		public static int NextFrame(double time, double frameRate)
		{
			return TimeUtility.ToFrames(time, frameRate) + 1;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000B016 File Offset: 0x00009216
		public static double PreviousFrameTime(double time, double frameRate)
		{
			return TimeUtility.FromFrames(TimeUtility.PreviousFrame(time, frameRate), frameRate);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000B025 File Offset: 0x00009225
		public static double NextFrameTime(double time, double frameRate)
		{
			return TimeUtility.FromFrames(TimeUtility.NextFrame(time, frameRate), frameRate);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000B034 File Offset: 0x00009234
		public static bool OnFrameBoundary(double time, double frameRate, double epsilon)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			double num = TimeUtility.ToExactFrames(time, frameRate);
			double rounded = Math.Round(num);
			return Math.Abs(num - rounded) < epsilon;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000B060 File Offset: 0x00009260
		public static double RoundToFrame(double time, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			double frameBefore = (double)((int)Math.Floor(time * frameRate)) / frameRate;
			double frameAfter = (double)((int)Math.Ceiling(time * frameRate)) / frameRate;
			if (Math.Abs(time - frameBefore) >= Math.Abs(time - frameAfter))
			{
				return frameAfter;
			}
			return frameBefore;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000B0A4 File Offset: 0x000092A4
		public static string TimeAsFrames(double timeValue, double frameRate, string format = "F2")
		{
			if (TimeUtility.OnFrameBoundary(timeValue, frameRate))
			{
				return TimeUtility.ToFrames(timeValue, frameRate).ToString();
			}
			return TimeUtility.ToExactFrames(timeValue, frameRate).ToString(format);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000B0DC File Offset: 0x000092DC
		public static string TimeAsTimeCode(double timeValue, double frameRate, string format = "F2")
		{
			TimeUtility.ValidateFrameRate(frameRate);
			int intTime = (int)Math.Abs(timeValue);
			int hours = intTime / 3600;
			int minutes = intTime % 3600 / 60;
			int seconds = intTime % 60;
			string sign = ((timeValue < 0.0) ? "-" : string.Empty);
			string result;
			if (hours > 0)
			{
				result = string.Concat(new string[]
				{
					hours.ToString(),
					":",
					minutes.ToString("D2"),
					":",
					seconds.ToString("D2")
				});
			}
			else if (minutes > 0)
			{
				result = minutes.ToString() + ":" + seconds.ToString("D2");
			}
			else
			{
				result = seconds.ToString();
			}
			int frameDigits = (int)Math.Floor(Math.Log10(frameRate) + 1.0);
			string frames = (TimeUtility.ToFrames(timeValue, frameRate) - TimeUtility.ToFrames((double)intTime, frameRate)).ToString().PadLeft(frameDigits, '0');
			if (!TimeUtility.OnFrameBoundary(timeValue, frameRate))
			{
				string decimals = TimeUtility.ToExactFrames(timeValue, frameRate).ToString(format);
				int decPlace = decimals.IndexOf('.');
				if (decPlace >= 0)
				{
					frames = frames + " [" + decimals.Substring(decPlace) + "]";
				}
			}
			return sign + result + ":" + frames;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000B238 File Offset: 0x00009438
		public static double ParseTimeCode(string timeCode, double frameRate, double defaultValue)
		{
			timeCode = TimeUtility.RemoveChar(timeCode, (char c) => char.IsWhiteSpace(c));
			string[] sections = timeCode.Split(':', StringSplitOptions.None);
			if (sections.Length == 0 || sections.Length > 4)
			{
				return defaultValue;
			}
			int hours = 0;
			int minutes = 0;
			double seconds = 0.0;
			double frames = 0.0;
			try
			{
				string lastSection = sections[sections.Length - 1];
				if (Regex.Match(lastSection, "^\\d+\\.\\d+$").Success)
				{
					seconds = double.Parse(lastSection);
					if (sections.Length > 3)
					{
						return defaultValue;
					}
					if (sections.Length > 1)
					{
						minutes = int.Parse(sections[sections.Length - 2]);
					}
					if (sections.Length > 2)
					{
						hours = int.Parse(sections[sections.Length - 3]);
					}
				}
				else
				{
					if (Regex.Match(lastSection, "^\\d+\\[\\.\\d+\\]$").Success)
					{
						frames = double.Parse(TimeUtility.RemoveChar(lastSection, (char c) => c == '[' || c == ']'));
					}
					else
					{
						if (!Regex.Match(lastSection, "^\\d*$").Success)
						{
							return defaultValue;
						}
						frames = (double)int.Parse(lastSection);
					}
					if (sections.Length > 1)
					{
						seconds = (double)int.Parse(sections[sections.Length - 2]);
					}
					if (sections.Length > 2)
					{
						minutes = int.Parse(sections[sections.Length - 3]);
					}
					if (sections.Length > 3)
					{
						hours = int.Parse(sections[sections.Length - 4]);
					}
				}
			}
			catch (FormatException)
			{
				return defaultValue;
			}
			return frames / frameRate + seconds + (double)(minutes * 60) + (double)(hours * 3600);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static double ParseTimeSeconds(string timeCode, double frameRate, double defaultValue)
		{
			timeCode = TimeUtility.RemoveChar(timeCode, (char c) => char.IsWhiteSpace(c));
			string[] sections = timeCode.Split(':', StringSplitOptions.None);
			if (sections.Length == 0 || sections.Length > 4)
			{
				return defaultValue;
			}
			int hours = 0;
			int minutes = 0;
			double seconds = 0.0;
			try
			{
				string lastSection = sections[sections.Length - 1];
				if (!double.TryParse(lastSection, NumberStyles.Integer, CultureInfo.InvariantCulture, out seconds))
				{
					if (!Regex.Match(lastSection, "^\\d+\\.\\d+$").Success)
					{
						return defaultValue;
					}
					seconds = double.Parse(lastSection);
				}
				if (!double.TryParse(lastSection, NumberStyles.Float, CultureInfo.InvariantCulture, out seconds))
				{
					return defaultValue;
				}
				if (sections.Length > 3)
				{
					return defaultValue;
				}
				if (sections.Length > 1)
				{
					minutes = int.Parse(sections[sections.Length - 2]);
				}
				if (sections.Length > 2)
				{
					hours = int.Parse(sections[sections.Length - 3]);
				}
			}
			catch (FormatException)
			{
				return defaultValue;
			}
			return seconds + (double)(minutes * 60) + (double)(hours * 3600);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000B4E4 File Offset: 0x000096E4
		public static double GetAnimationClipLength(AnimationClip clip)
		{
			if (clip == null || clip.empty)
			{
				return 0.0;
			}
			double length = (double)clip.length;
			if (clip.frameRate > 0f)
			{
				length = (double)Mathf.Round(clip.length * clip.frameRate) / (double)clip.frameRate;
			}
			return length;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000B540 File Offset: 0x00009740
		private static string RemoveChar(string str, Func<char, bool> charToRemoveFunc)
		{
			int len = str.Length;
			char[] src = str.ToCharArray();
			int dstIdx = 0;
			for (int i = 0; i < len; i++)
			{
				if (!charToRemoveFunc(src[i]))
				{
					src[dstIdx++] = src[i];
				}
			}
			return new string(src, 0, dstIdx);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000B588 File Offset: 0x00009788
		public static FrameRate GetClosestFrameRate(double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			FrameRate actualFrameRate = FrameRate.DoubleToFrameRate(frameRate);
			if (Math.Abs(frameRate - actualFrameRate.rate) >= TimeUtility.kFrameRateRounding)
			{
				return default(FrameRate);
			}
			return actualFrameRate;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000B5C4 File Offset: 0x000097C4
		public static FrameRate ToFrameRate(StandardFrameRates enumValue)
		{
			switch (enumValue)
			{
			case StandardFrameRates.Fps24:
				return FrameRate.k_24Fps;
			case StandardFrameRates.Fps23_97:
				return FrameRate.k_23_976Fps;
			case StandardFrameRates.Fps25:
				return FrameRate.k_25Fps;
			case StandardFrameRates.Fps30:
				return FrameRate.k_30Fps;
			case StandardFrameRates.Fps29_97:
				return FrameRate.k_29_97Fps;
			case StandardFrameRates.Fps50:
				return FrameRate.k_50Fps;
			case StandardFrameRates.Fps60:
				return FrameRate.k_60Fps;
			case StandardFrameRates.Fps59_94:
				return FrameRate.k_59_94Fps;
			default:
				return default(FrameRate);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000B634 File Offset: 0x00009834
		internal static bool ToStandardFrameRate(FrameRate rate, out StandardFrameRates standard)
		{
			if (rate == FrameRate.k_23_976Fps)
			{
				standard = StandardFrameRates.Fps23_97;
			}
			else if (rate == FrameRate.k_24Fps)
			{
				standard = StandardFrameRates.Fps24;
			}
			else if (rate == FrameRate.k_25Fps)
			{
				standard = StandardFrameRates.Fps25;
			}
			else if (rate == FrameRate.k_30Fps)
			{
				standard = StandardFrameRates.Fps30;
			}
			else if (rate == FrameRate.k_29_97Fps)
			{
				standard = StandardFrameRates.Fps29_97;
			}
			else if (rate == FrameRate.k_50Fps)
			{
				standard = StandardFrameRates.Fps50;
			}
			else if (rate == FrameRate.k_59_94Fps)
			{
				standard = StandardFrameRates.Fps59_94;
			}
			else
			{
				if (!(rate == FrameRate.k_60Fps))
				{
					standard = (StandardFrameRates)Enum.GetValues(typeof(StandardFrameRates)).Length;
					return false;
				}
				standard = StandardFrameRates.Fps60;
			}
			return true;
		}

		// Token: 0x04000180 RID: 384
		public static readonly double kTimeEpsilon = 1E-14;

		// Token: 0x04000181 RID: 385
		public static readonly double kFrameRateEpsilon = 1E-06;

		// Token: 0x04000182 RID: 386
		public static readonly double k_MaxTimelineDurationInSeconds = 9000000.0;

		// Token: 0x04000183 RID: 387
		public static readonly double kFrameRateRounding = 0.01;
	}
}
