using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System
{
	/// <summary>Represents any time zone in the world.</summary>
	// Token: 0x0200009B RID: 155
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class TimeZoneInfo : IEquatable<TimeZoneInfo>, ISerializable, IDeserializationCallback
	{
		/// <summary>Retrieves an array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects that apply to the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>An array of objects for this time zone.</returns>
		/// <exception cref="T:System.OutOfMemoryException">The system does not have enough memory to make an in-memory copy of the adjustment rules.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060003D0 RID: 976 RVA: 0x00014A9B File Offset: 0x00012C9B
		public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
		{
			if (this._adjustmentRules == null)
			{
				return Array.Empty<TimeZoneInfo.AdjustmentRule>();
			}
			return (TimeZoneInfo.AdjustmentRule[])this._adjustmentRules.Clone();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00014ABB File Offset: 0x00012CBB
		private static void PopulateAllSystemTimeZones(TimeZoneInfo.CachedData cachedData)
		{
			if (TimeZoneInfo.HaveRegistry)
			{
				TimeZoneInfo.PopulateAllSystemTimeZonesFromRegistry(cachedData);
				return;
			}
			TimeZoneInfo.GetSystemTimeZonesWinRTFallback(cachedData);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00014AD4 File Offset: 0x00012CD4
		private static void PopulateAllSystemTimeZonesFromRegistry(TimeZoneInfo.CachedData cachedData)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", false))
			{
				if (registryKey != null)
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						TimeZoneInfo timeZoneInfo;
						Exception ex;
						TimeZoneInfo.TryGetTimeZone(subKeyNames[i], false, out timeZoneInfo, out ex, cachedData, false);
					}
				}
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00014B38 File Offset: 0x00012D38
		private TimeZoneInfo(in Interop.Kernel32.TIME_ZONE_INFORMATION zone, bool dstDisabled)
		{
			Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION = zone;
			string standardName = time_ZONE_INFORMATION.GetStandardName();
			if (standardName.Length == 0)
			{
				this._id = "Local";
			}
			else
			{
				this._id = standardName;
			}
			this._baseUtcOffset = new TimeSpan(0, -zone.Bias, 0);
			if (!dstDisabled)
			{
				Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT = new Interop.Kernel32.REG_TZI_FORMAT(in zone);
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(in reg_TZI_FORMAT, DateTime.MinValue.Date, DateTime.MaxValue.Date, zone.Bias);
				if (adjustmentRule != null)
				{
					this._adjustmentRules = new TimeZoneInfo.AdjustmentRule[] { adjustmentRule };
				}
			}
			TimeZoneInfo.ValidateTimeZoneInfo(this._id, this._baseUtcOffset, this._adjustmentRules, out this._supportsDaylightSavingTime);
			this._displayName = standardName;
			this._standardDisplayName = standardName;
			time_ZONE_INFORMATION = zone;
			this._daylightDisplayName = time_ZONE_INFORMATION.GetDaylightName();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00014C08 File Offset: 0x00012E08
		private static bool CheckDaylightSavingTimeNotSupported(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone)
		{
			Interop.Kernel32.SYSTEMTIME daylightDate = timeZone.DaylightDate;
			return daylightDate.Equals(in timeZone.StandardDate);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00014C2C File Offset: 0x00012E2C
		private static TimeZoneInfo.AdjustmentRule CreateAdjustmentRuleFromTimeZoneInformation(in Interop.Kernel32.REG_TZI_FORMAT timeZoneInformation, DateTime startDate, DateTime endDate, int defaultBaseUtcOffset)
		{
			if (timeZoneInformation.StandardDate.Month <= 0)
			{
				if (timeZoneInformation.Bias == defaultBaseUtcOffset)
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, TimeSpan.Zero, TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue, 1, 1), TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(1.0), 1, 1), new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0), false);
			}
			else
			{
				TimeZoneInfo.TransitionTime transitionTime;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(in timeZoneInformation, out transitionTime, true))
				{
					return null;
				}
				TimeZoneInfo.TransitionTime transitionTime2;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(in timeZoneInformation, out transitionTime2, false))
				{
					return null;
				}
				if (transitionTime.Equals(transitionTime2))
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, new TimeSpan(0, -timeZoneInformation.DaylightBias, 0), transitionTime, transitionTime2, new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0), false);
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00014CE4 File Offset: 0x00012EE4
		private static string FindIdFromTimeZoneInformation(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone, out bool dstDisabled)
		{
			dstDisabled = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", false))
			{
				if (registryKey == null)
				{
					return null;
				}
				foreach (string text in registryKey.GetSubKeyNames())
				{
					if (TimeZoneInfo.TryCompareTimeZoneInformationToRegistry(in timeZone, text, out dstDisabled))
					{
						return text;
					}
				}
			}
			return null;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00014D58 File Offset: 0x00012F58
		private static TimeZoneInfo GetLocalTimeZone(TimeZoneInfo.CachedData cachedData)
		{
			if (!TimeZoneInfo.HaveRegistry)
			{
				return TimeZoneInfo.GetLocalTimeZoneInfoWinRTFallback();
			}
			Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION time_DYNAMIC_ZONE_INFORMATION = default(Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION);
			if (Interop.Kernel32.GetDynamicTimeZoneInformation(out time_DYNAMIC_ZONE_INFORMATION) == 4294967295U)
			{
				return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
			}
			string timeZoneKeyName = time_DYNAMIC_ZONE_INFORMATION.GetTimeZoneKeyName();
			TimeZoneInfo timeZoneInfo;
			Exception ex;
			if (timeZoneKeyName.Length != 0 && TimeZoneInfo.TryGetTimeZone(timeZoneKeyName, time_DYNAMIC_ZONE_INFORMATION.DynamicDaylightTimeDisabled > 0, out timeZoneInfo, out ex, cachedData, false) == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return timeZoneInfo;
			}
			Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION = new Interop.Kernel32.TIME_ZONE_INFORMATION(in time_DYNAMIC_ZONE_INFORMATION);
			bool flag;
			string text = TimeZoneInfo.FindIdFromTimeZoneInformation(in time_ZONE_INFORMATION, out flag);
			TimeZoneInfo timeZoneInfo2;
			Exception ex2;
			if (text != null && TimeZoneInfo.TryGetTimeZone(text, flag, out timeZoneInfo2, out ex2, cachedData, false) == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return timeZoneInfo2;
			}
			return TimeZoneInfo.GetLocalTimeZoneFromWin32Data(in time_ZONE_INFORMATION, flag);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00014DFC File Offset: 0x00012FFC
		private static TimeZoneInfo GetLocalTimeZoneFromWin32Data(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZoneInformation, bool dstDisabled)
		{
			try
			{
				return new TimeZoneInfo(in timeZoneInformation, dstDisabled);
			}
			catch (ArgumentException)
			{
			}
			catch (InvalidTimeZoneException)
			{
			}
			if (!dstDisabled)
			{
				try
				{
					return new TimeZoneInfo(in timeZoneInformation, true);
				}
				catch (ArgumentException)
				{
				}
				catch (InvalidTimeZoneException)
				{
				}
			}
			return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00014E7C File Offset: 0x0001307C
		internal static TimeSpan GetDateTimeNowUtcOffsetFromUtc(DateTime time, out bool isAmbiguousLocalDst)
		{
			isAmbiguousLocalDst = false;
			int year = time.Year;
			TimeZoneInfo.OffsetAndRule oneYearLocalFromUtc = TimeZoneInfo.s_cachedData.GetOneYearLocalFromUtc(year);
			TimeSpan timeSpan = oneYearLocalFromUtc.Offset;
			if (oneYearLocalFromUtc.Rule != null)
			{
				timeSpan += oneYearLocalFromUtc.Rule.BaseUtcOffsetDelta;
				if (oneYearLocalFromUtc.Rule.HasDaylightSaving)
				{
					bool isDaylightSavingsFromUtc = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, oneYearLocalFromUtc.Offset, oneYearLocalFromUtc.Rule, null, out isAmbiguousLocalDst, TimeZoneInfo.Local);
					timeSpan += (isDaylightSavingsFromUtc ? oneYearLocalFromUtc.Rule.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00014F14 File Offset: 0x00013114
		private static bool TransitionTimeFromTimeZoneInformation(in Interop.Kernel32.REG_TZI_FORMAT timeZoneInformation, out TimeZoneInfo.TransitionTime transitionTime, bool readStartDate)
		{
			if (timeZoneInformation.StandardDate.Month <= 0)
			{
				transitionTime = default(TimeZoneInfo.TransitionTime);
				return false;
			}
			if (readStartDate)
			{
				if (timeZoneInformation.DaylightDate.Year == 0)
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.DaylightDate.Hour, (int)timeZoneInformation.DaylightDate.Minute, (int)timeZoneInformation.DaylightDate.Second, (int)timeZoneInformation.DaylightDate.Milliseconds), (int)timeZoneInformation.DaylightDate.Month, (int)timeZoneInformation.DaylightDate.Day, (DayOfWeek)timeZoneInformation.DaylightDate.DayOfWeek);
				}
				else
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.DaylightDate.Hour, (int)timeZoneInformation.DaylightDate.Minute, (int)timeZoneInformation.DaylightDate.Second, (int)timeZoneInformation.DaylightDate.Milliseconds), (int)timeZoneInformation.DaylightDate.Month, (int)timeZoneInformation.DaylightDate.Day);
				}
			}
			else if (timeZoneInformation.StandardDate.Year == 0)
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.StandardDate.Hour, (int)timeZoneInformation.StandardDate.Minute, (int)timeZoneInformation.StandardDate.Second, (int)timeZoneInformation.StandardDate.Milliseconds), (int)timeZoneInformation.StandardDate.Month, (int)timeZoneInformation.StandardDate.Day, (DayOfWeek)timeZoneInformation.StandardDate.DayOfWeek);
			}
			else
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.StandardDate.Hour, (int)timeZoneInformation.StandardDate.Minute, (int)timeZoneInformation.StandardDate.Second, (int)timeZoneInformation.StandardDate.Milliseconds), (int)timeZoneInformation.StandardDate.Month, (int)timeZoneInformation.StandardDate.Day);
			}
			return true;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000150D4 File Offset: 0x000132D4
		private static bool TryCreateAdjustmentRules(string id, in Interop.Kernel32.REG_TZI_FORMAT defaultTimeZoneInformation, out TimeZoneInfo.AdjustmentRule[] rules, out Exception e, int defaultBaseUtcOffset)
		{
			rules = null;
			e = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\" + id + "\\Dynamic DST", false))
				{
					if (registryKey == null)
					{
						TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(in defaultTimeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, defaultBaseUtcOffset);
						if (adjustmentRule != null)
						{
							rules = new TimeZoneInfo.AdjustmentRule[] { adjustmentRule };
						}
						return true;
					}
					int num = (int)registryKey.GetValue("FirstEntry", -1, RegistryValueOptions.None);
					int num2 = (int)registryKey.GetValue("LastEntry", -1, RegistryValueOptions.None);
					if (num == -1 || num2 == -1 || num > num2)
					{
						return false;
					}
					Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT;
					if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, num.ToString(CultureInfo.InvariantCulture), out reg_TZI_FORMAT))
					{
						return false;
					}
					if (num == num2)
					{
						TimeZoneInfo.AdjustmentRule adjustmentRule2 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(in reg_TZI_FORMAT, DateTime.MinValue.Date, DateTime.MaxValue.Date, defaultBaseUtcOffset);
						if (adjustmentRule2 != null)
						{
							rules = new TimeZoneInfo.AdjustmentRule[] { adjustmentRule2 };
						}
						return true;
					}
					List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
					TimeZoneInfo.AdjustmentRule adjustmentRule3 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(in reg_TZI_FORMAT, DateTime.MinValue.Date, new DateTime(num, 12, 31), defaultBaseUtcOffset);
					if (adjustmentRule3 != null)
					{
						list.Add(adjustmentRule3);
					}
					for (int i = num + 1; i < num2; i++)
					{
						if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, i.ToString(CultureInfo.InvariantCulture), out reg_TZI_FORMAT))
						{
							return false;
						}
						TimeZoneInfo.AdjustmentRule adjustmentRule4 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(in reg_TZI_FORMAT, new DateTime(i, 1, 1), new DateTime(i, 12, 31), defaultBaseUtcOffset);
						if (adjustmentRule4 != null)
						{
							list.Add(adjustmentRule4);
						}
					}
					if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, num2.ToString(CultureInfo.InvariantCulture), out reg_TZI_FORMAT))
					{
						return false;
					}
					TimeZoneInfo.AdjustmentRule adjustmentRule5 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(in reg_TZI_FORMAT, new DateTime(num2, 1, 1), DateTime.MaxValue.Date, defaultBaseUtcOffset);
					if (adjustmentRule5 != null)
					{
						list.Add(adjustmentRule5);
					}
					if (list.Count != 0)
					{
						rules = list.ToArray();
					}
				}
			}
			catch (InvalidCastException ex)
			{
				e = ex;
				return false;
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				e = ex2;
				return false;
			}
			catch (ArgumentException ex3)
			{
				e = ex3;
				return false;
			}
			return true;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001535C File Offset: 0x0001355C
		private unsafe static bool TryGetTimeZoneEntryFromRegistry(RegistryKey key, string name, out Interop.Kernel32.REG_TZI_FORMAT dtzi)
		{
			byte[] array = key.GetValue(name, null, RegistryValueOptions.None) as byte[];
			if (array == null || array.Length != sizeof(Interop.Kernel32.REG_TZI_FORMAT))
			{
				dtzi = default(Interop.Kernel32.REG_TZI_FORMAT);
				return false;
			}
			fixed (byte* ptr = &array[0])
			{
				byte* ptr2 = ptr;
				dtzi = *(Interop.Kernel32.REG_TZI_FORMAT*)ptr2;
			}
			return true;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000153AC File Offset: 0x000135AC
		private static bool TryCompareStandardDate(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone, in Interop.Kernel32.REG_TZI_FORMAT registryTimeZoneInfo)
		{
			if (timeZone.Bias == registryTimeZoneInfo.Bias && timeZone.StandardBias == registryTimeZoneInfo.StandardBias)
			{
				Interop.Kernel32.SYSTEMTIME standardDate = timeZone.StandardDate;
				return standardDate.Equals(in registryTimeZoneInfo.StandardDate);
			}
			return false;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000153EC File Offset: 0x000135EC
		private static bool TryCompareTimeZoneInformationToRegistry(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone, string id, out bool dstDisabled)
		{
			dstDisabled = false;
			bool flag;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\" + id, false))
			{
				Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT;
				if (registryKey == null)
				{
					flag = false;
				}
				else if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, "TZI", out reg_TZI_FORMAT))
				{
					flag = false;
				}
				else if (!TimeZoneInfo.TryCompareStandardDate(in timeZone, in reg_TZI_FORMAT))
				{
					flag = false;
				}
				else
				{
					bool flag2;
					if (!dstDisabled && !TimeZoneInfo.CheckDaylightSavingTimeNotSupported(in timeZone))
					{
						if (timeZone.DaylightBias == reg_TZI_FORMAT.DaylightBias)
						{
							Interop.Kernel32.SYSTEMTIME daylightDate = timeZone.DaylightDate;
							flag2 = daylightDate.Equals(in reg_TZI_FORMAT.DaylightDate);
						}
						else
						{
							flag2 = false;
						}
					}
					else
					{
						flag2 = true;
					}
					bool flag3 = flag2;
					if (flag3)
					{
						string text = registryKey.GetValue("Std", string.Empty, RegistryValueOptions.None) as string;
						Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION = timeZone;
						flag3 = string.Equals(text, time_ZONE_INFORMATION.GetStandardName(), StringComparison.Ordinal);
					}
					flag = flag3;
				}
			}
			return flag;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000154C8 File Offset: 0x000136C8
		private static string TryGetLocalizedNameByMuiNativeResource(string resource)
		{
			if (string.IsNullOrEmpty(resource))
			{
				return string.Empty;
			}
			string[] array = resource.Split(',', StringSplitOptions.None);
			if (array.Length != 2)
			{
				return string.Empty;
			}
			string systemDirectory = Environment.SystemDirectory;
			string text = array[0].TrimStart('@');
			string text2;
			try
			{
				text2 = Path.Combine(systemDirectory, text);
			}
			catch (ArgumentException)
			{
				return string.Empty;
			}
			int num;
			if (!int.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				return string.Empty;
			}
			num = -num;
			string text3;
			try
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(260);
				stringBuilder.Length = 260;
				int num2 = 260;
				int num3 = 0;
				long num4 = 0L;
				if (!Interop.Kernel32.GetFileMUIPath(16U, text2, null, ref num3, stringBuilder, ref num2, ref num4))
				{
					StringBuilderCache.Release(stringBuilder);
					text3 = string.Empty;
				}
				else
				{
					text3 = TimeZoneInfo.TryGetLocalizedNameByNativeResource(StringBuilderCache.GetStringAndRelease(stringBuilder), num);
				}
			}
			catch (EntryPointNotFoundException)
			{
				text3 = string.Empty;
			}
			return text3;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000155C0 File Offset: 0x000137C0
		private static string TryGetLocalizedNameByNativeResource(string filePath, int resource)
		{
			using (SafeLibraryHandle safeLibraryHandle = Interop.Kernel32.LoadLibraryEx(filePath, IntPtr.Zero, 2))
			{
				if (!safeLibraryHandle.IsInvalid)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(500);
					if (Interop.User32.LoadString(safeLibraryHandle, resource, stringBuilder, 500) != 0)
					{
						return StringBuilderCache.GetStringAndRelease(stringBuilder);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00015628 File Offset: 0x00013828
		private static void GetLocalizedNamesByRegistryKey(RegistryKey key, out string displayName, out string standardName, out string daylightName)
		{
			displayName = string.Empty;
			standardName = string.Empty;
			daylightName = string.Empty;
			string text = key.GetValue("MUI_Display", string.Empty, RegistryValueOptions.None) as string;
			string text2 = key.GetValue("MUI_Std", string.Empty, RegistryValueOptions.None) as string;
			string text3 = key.GetValue("MUI_Dlt", string.Empty, RegistryValueOptions.None) as string;
			if (!string.IsNullOrEmpty(text))
			{
				displayName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				standardName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text2);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				daylightName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text3);
			}
			if (string.IsNullOrEmpty(displayName))
			{
				displayName = key.GetValue("Display", string.Empty, RegistryValueOptions.None) as string;
			}
			if (string.IsNullOrEmpty(standardName))
			{
				standardName = key.GetValue("Std", string.Empty, RegistryValueOptions.None) as string;
			}
			if (string.IsNullOrEmpty(daylightName))
			{
				daylightName = key.GetValue("Dlt", string.Empty, RegistryValueOptions.None) as string;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00015722 File Offset: 0x00013922
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneFromLocalMachine(string id, out TimeZoneInfo value, out Exception e)
		{
			if (TimeZoneInfo.HaveRegistry)
			{
				return TimeZoneInfo.TryGetTimeZoneFromLocalRegistry(id, out value, out e);
			}
			e = null;
			value = TimeZoneInfo.FindSystemTimeZoneByIdWinRTFallback(id);
			return TimeZoneInfo.TimeZoneInfoResult.Success;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00015740 File Offset: 0x00013940
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneFromLocalRegistry(string id, out TimeZoneInfo value, out Exception e)
		{
			e = null;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\" + id, false))
			{
				Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT;
				TimeZoneInfo.AdjustmentRule[] array;
				if (registryKey == null)
				{
					value = null;
					timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
				}
				else if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, "TZI", out reg_TZI_FORMAT))
				{
					value = null;
					timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
				}
				else if (!TimeZoneInfo.TryCreateAdjustmentRules(id, in reg_TZI_FORMAT, out array, out e, reg_TZI_FORMAT.Bias))
				{
					value = null;
					timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
				}
				else
				{
					string text;
					string text2;
					string text3;
					TimeZoneInfo.GetLocalizedNamesByRegistryKey(registryKey, out text, out text2, out text3);
					try
					{
						value = new TimeZoneInfo(id, new TimeSpan(0, -reg_TZI_FORMAT.Bias, 0), text, text2, text3, array, false);
						timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.Success;
					}
					catch (ArgumentException ex)
					{
						value = null;
						e = ex;
						timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
					}
					catch (InvalidTimeZoneException ex2)
					{
						value = null;
						e = ex2;
						timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
					}
				}
			}
			return timeZoneInfoResult;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00015828 File Offset: 0x00013A28
		private static bool HaveRegistry
		{
			get
			{
				return TimeZoneInfo.lazyHaveRegistry.Value;
			}
		}

		// Token: 0x060003E5 RID: 997
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern uint EnumDynamicTimeZoneInformation(uint dwIndex, out TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION lpTimeZoneInformation);

		// Token: 0x060003E6 RID: 998
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern uint GetDynamicTimeZoneInformation(out TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION pTimeZoneInformation);

		// Token: 0x060003E7 RID: 999
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern uint GetDynamicTimeZoneInformationEffectiveYears(ref TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION lpTimeZoneInformation, out uint FirstYear, out uint LastYear);

		// Token: 0x060003E8 RID: 1000
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern bool GetTimeZoneInformationForYear(ushort wYear, ref TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION pdtzi, out Interop.Kernel32.TIME_ZONE_INFORMATION ptzi);

		// Token: 0x060003E9 RID: 1001 RVA: 0x00015834 File Offset: 0x00013A34
		internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRuleFromTimeZoneInformation(ref TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation, DateTime startDate, DateTime endDate, int defaultBaseUtcOffset)
		{
			if (timeZoneInformation.TZI.StandardDate.Month <= 0)
			{
				if (timeZoneInformation.TZI.Bias == defaultBaseUtcOffset)
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, TimeSpan.Zero, TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue, 1, 1), TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(1.0), 1, 1), new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.TZI.Bias, 0), false);
			}
			else
			{
				TimeZoneInfo.TransitionTime transitionTime;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime, true))
				{
					return null;
				}
				TimeZoneInfo.TransitionTime transitionTime2;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime2, false))
				{
					return null;
				}
				if (transitionTime.Equals(transitionTime2))
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, new TimeSpan(0, -timeZoneInformation.TZI.DaylightBias, 0), transitionTime, transitionTime2, new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.TZI.Bias, 0), false);
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00015910 File Offset: 0x00013B10
		private static bool TransitionTimeFromTimeZoneInformation(TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation, out TimeZoneInfo.TransitionTime transitionTime, bool readStartDate)
		{
			if (timeZoneInformation.TZI.StandardDate.Month <= 0)
			{
				transitionTime = default(TimeZoneInfo.TransitionTime);
				return false;
			}
			if (readStartDate)
			{
				if (timeZoneInformation.TZI.DaylightDate.Year == 0)
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.DaylightDate.Hour, (int)timeZoneInformation.TZI.DaylightDate.Minute, (int)timeZoneInformation.TZI.DaylightDate.Second, (int)timeZoneInformation.TZI.DaylightDate.Milliseconds), (int)timeZoneInformation.TZI.DaylightDate.Month, (int)timeZoneInformation.TZI.DaylightDate.Day, (DayOfWeek)timeZoneInformation.TZI.DaylightDate.DayOfWeek);
				}
				else
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.DaylightDate.Hour, (int)timeZoneInformation.TZI.DaylightDate.Minute, (int)timeZoneInformation.TZI.DaylightDate.Second, (int)timeZoneInformation.TZI.DaylightDate.Milliseconds), (int)timeZoneInformation.TZI.DaylightDate.Month, (int)timeZoneInformation.TZI.DaylightDate.Day);
				}
			}
			else if (timeZoneInformation.TZI.StandardDate.Year == 0)
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.StandardDate.Hour, (int)timeZoneInformation.TZI.StandardDate.Minute, (int)timeZoneInformation.TZI.StandardDate.Second, (int)timeZoneInformation.TZI.StandardDate.Milliseconds), (int)timeZoneInformation.TZI.StandardDate.Month, (int)timeZoneInformation.TZI.StandardDate.Day, (DayOfWeek)timeZoneInformation.TZI.StandardDate.DayOfWeek);
			}
			else
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.StandardDate.Hour, (int)timeZoneInformation.TZI.StandardDate.Minute, (int)timeZoneInformation.TZI.StandardDate.Second, (int)timeZoneInformation.TZI.StandardDate.Milliseconds), (int)timeZoneInformation.TZI.StandardDate.Month, (int)timeZoneInformation.TZI.StandardDate.Day);
			}
			return true;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00015B64 File Offset: 0x00013D64
		internal static TimeZoneInfo TryCreateTimeZone(TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation)
		{
			uint num = 0U;
			uint num2 = 0U;
			TimeZoneInfo.AdjustmentRule[] array = null;
			int bias = timeZoneInformation.TZI.Bias;
			if (string.IsNullOrEmpty(timeZoneInformation.TimeZoneKeyName))
			{
				return null;
			}
			try
			{
				if (TimeZoneInfo.GetDynamicTimeZoneInformationEffectiveYears(ref timeZoneInformation, out num, out num2) != 0U)
				{
					num2 = (num = 0U);
				}
			}
			catch
			{
				num2 = (num = 0U);
			}
			if (num == num2)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref timeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, bias);
				if (adjustmentRule != null)
				{
					array = new TimeZoneInfo.AdjustmentRule[] { adjustmentRule };
				}
			}
			else
			{
				TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION dynamic_TIME_ZONE_INFORMATION = default(TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION);
				List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>();
				if (!TimeZoneInfo.GetTimeZoneInformationForYear((ushort)num, ref timeZoneInformation, out dynamic_TIME_ZONE_INFORMATION.TZI))
				{
					return null;
				}
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref dynamic_TIME_ZONE_INFORMATION, DateTime.MinValue.Date, new DateTime((int)num, 12, 31), bias);
				if (adjustmentRule != null)
				{
					list.Add(adjustmentRule);
				}
				for (uint num3 = num + 1U; num3 < num2; num3 += 1U)
				{
					if (!TimeZoneInfo.GetTimeZoneInformationForYear((ushort)num3, ref timeZoneInformation, out dynamic_TIME_ZONE_INFORMATION.TZI))
					{
						return null;
					}
					adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref dynamic_TIME_ZONE_INFORMATION, new DateTime((int)num3, 1, 1), new DateTime((int)num3, 12, 31), bias);
					if (adjustmentRule != null)
					{
						list.Add(adjustmentRule);
					}
				}
				if (!TimeZoneInfo.GetTimeZoneInformationForYear((ushort)num2, ref timeZoneInformation, out dynamic_TIME_ZONE_INFORMATION.TZI))
				{
					return null;
				}
				adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref dynamic_TIME_ZONE_INFORMATION, new DateTime((int)num2, 1, 1), DateTime.MaxValue.Date, bias);
				if (adjustmentRule != null)
				{
					list.Add(adjustmentRule);
				}
				if (list.Count > 0)
				{
					array = list.ToArray();
				}
			}
			return new TimeZoneInfo(timeZoneInformation.TimeZoneKeyName, new TimeSpan(0, -timeZoneInformation.TZI.Bias, 0), timeZoneInformation.TZI.GetStandardName(), timeZoneInformation.TZI.GetStandardName(), timeZoneInformation.TZI.GetDaylightName(), array, false);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00015D24 File Offset: 0x00013F24
		internal static TimeZoneInfo GetLocalTimeZoneInfoWinRTFallback()
		{
			TimeZoneInfo timeZoneInfo;
			try
			{
				TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION dynamic_TIME_ZONE_INFORMATION;
				if (TimeZoneInfo.GetDynamicTimeZoneInformation(out dynamic_TIME_ZONE_INFORMATION) == 4294967295U)
				{
					timeZoneInfo = TimeZoneInfo.Utc;
				}
				else
				{
					TimeZoneInfo timeZoneInfo2 = TimeZoneInfo.TryCreateTimeZone(dynamic_TIME_ZONE_INFORMATION);
					timeZoneInfo = ((timeZoneInfo2 != null) ? timeZoneInfo2 : TimeZoneInfo.Utc);
				}
			}
			catch
			{
				timeZoneInfo = TimeZoneInfo.Utc;
			}
			return timeZoneInfo;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00015D74 File Offset: 0x00013F74
		internal static TimeZoneInfo FindSystemTimeZoneByIdWinRTFallback(string id)
		{
			foreach (TimeZoneInfo timeZoneInfo in TimeZoneInfo.GetSystemTimeZones())
			{
				if (string.Compare(id, timeZoneInfo.Id, StringComparison.Ordinal) == 0)
				{
					return timeZoneInfo;
				}
			}
			throw new TimeZoneNotFoundException();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00015DD4 File Offset: 0x00013FD4
		private static void GetSystemTimeZonesWinRTFallback(TimeZoneInfo.CachedData cachedData)
		{
			List<TimeZoneInfo> list = new List<TimeZoneInfo>();
			try
			{
				uint num = 0U;
				TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION dynamic_TIME_ZONE_INFORMATION;
				while (TimeZoneInfo.EnumDynamicTimeZoneInformation(num++, out dynamic_TIME_ZONE_INFORMATION) != 259U)
				{
					TimeZoneInfo timeZoneInfo = TimeZoneInfo.TryCreateTimeZone(dynamic_TIME_ZONE_INFORMATION);
					if (timeZoneInfo != null)
					{
						list.Add(timeZoneInfo);
					}
				}
			}
			catch
			{
			}
			if (list.Count == 0)
			{
				list.Add(TimeZoneInfo.Local);
				list.Add(TimeZoneInfo.Utc);
			}
			list.Sort(delegate(TimeZoneInfo x, TimeZoneInfo y)
			{
				int num2 = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
				if (num2 != 0)
				{
					return num2;
				}
				return string.CompareOrdinal(x.DisplayName, y.DisplayName);
			});
			foreach (TimeZoneInfo timeZoneInfo2 in list)
			{
				if (cachedData._systemTimeZones == null)
				{
					cachedData._systemTimeZones = new Dictionary<string, TimeZoneInfo>(StringComparer.OrdinalIgnoreCase);
				}
				cachedData._systemTimeZones.Add(timeZoneInfo2.Id, timeZoneInfo2);
			}
		}

		/// <summary>Gets the time zone identifier.</summary>
		/// <returns>The time zone identifier.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00015ECC File Offset: 0x000140CC
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		/// <summary>Gets the general display name that represents the time zone.</summary>
		/// <returns>The time zone's general display name.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00015ED4 File Offset: 0x000140D4
		public string DisplayName
		{
			get
			{
				return this._displayName ?? string.Empty;
			}
		}

		/// <summary>Gets the display name for the time zone's standard time.</summary>
		/// <returns>The display name of the time zone's standard time.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00015EE5 File Offset: 0x000140E5
		public string StandardName
		{
			get
			{
				return this._standardDisplayName ?? string.Empty;
			}
		}

		/// <summary>Gets the display name for the current time zone's daylight saving time.</summary>
		/// <returns>The display name for the time zone's daylight saving time.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00015EF6 File Offset: 0x000140F6
		public string DaylightName
		{
			get
			{
				return this._daylightDisplayName ?? string.Empty;
			}
		}

		/// <summary>Gets the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).</summary>
		/// <returns>An object that indicates the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00015F07 File Offset: 0x00014107
		public TimeSpan BaseUtcOffset
		{
			get
			{
				return this._baseUtcOffset;
			}
		}

		/// <summary>Gets a value indicating whether the time zone has any daylight saving time rules.</summary>
		/// <returns>true if the time zone supports daylight saving time; otherwise, false.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00015F0F File Offset: 0x0001410F
		public bool SupportsDaylightSavingTime
		{
			get
			{
				return this._supportsDaylightSavingTime;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00015F18 File Offset: 0x00014118
		private TimeZoneInfo.AdjustmentRule GetPreviousAdjustmentRule(TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			if (ruleIndex != null && 0 < ruleIndex.Value && ruleIndex.Value < this._adjustmentRules.Length)
			{
				return this._adjustmentRules[ruleIndex.Value - 1];
			}
			TimeZoneInfo.AdjustmentRule adjustmentRule = rule;
			for (int i = 1; i < this._adjustmentRules.Length; i++)
			{
				if (rule == this._adjustmentRules[i])
				{
					adjustmentRule = this._adjustmentRules[i - 1];
					break;
				}
			}
			return adjustmentRule;
		}

		/// <summary>Calculates the offset or difference between the time in this time zone and Coordinated Universal Time (UTC) for a particular date and time.</summary>
		/// <returns>An object that indicates the time difference between the two time zones.</returns>
		/// <param name="dateTime">The date and time to determine the offset for.   </param>
		// Token: 0x060003F6 RID: 1014 RVA: 0x00015F88 File Offset: 0x00014188
		public TimeSpan GetUtcOffset(DateTime dateTime)
		{
			return this.GetUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00015F98 File Offset: 0x00014198
		internal static TimeSpan GetLocalUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return cachedData.Local.GetUtcOffset(dateTime, flags, cachedData);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00015FB9 File Offset: 0x000141B9
		internal TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.GetUtcOffset(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00015FC8 File Offset: 0x000141C8
		private TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (cachedData.GetCorrespondingKind(this) != DateTimeKind.Local)
				{
					return TimeZoneInfo.GetUtcOffsetFromUtc(TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, flags), this);
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return this._baseUtcOffset;
				}
				return TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this);
			}
			return TimeZoneInfo.GetUtcOffset(dateTime, this, flags);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001602D File Offset: 0x0001422D
		internal bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.IsDaylightSavingTime(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001603C File Offset: 0x0001423C
		private bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (!this._supportsDaylightSavingTime || this._adjustmentRules == null)
			{
				return false;
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return false;
				}
				bool flag;
				TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this, out flag);
				return flag;
			}
			else
			{
				dateTime2 = dateTime;
			}
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2, out num);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime, num);
				return TimeZoneInfo.GetIsDaylightSavings(dateTime2, adjustmentRuleForTime, daylightTime, flags);
			}
			return false;
		}

		/// <summary>Clears cached time zone data.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060003FC RID: 1020 RVA: 0x000160CD File Offset: 0x000142CD
		public static void ClearCachedData()
		{
			TimeZoneInfo.s_cachedData = new TimeZoneInfo.CachedData();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000160D9 File Offset: 0x000142D9
		internal static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000160EC File Offset: 0x000142EC
		private static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTimeKind correspondingKind = cachedData.GetCorrespondingKind(sourceTimeZone);
			if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && dateTime.Kind != DateTimeKind.Unspecified && dateTime.Kind != correspondingKind)
			{
				throw new ArgumentException("The conversion could not be completed because the supplied DateTime did not have the Kind property set correctly.  For example, when the Kind property is DateTimeKind.Local, the source time zone must be TimeZoneInfo.Local.", "sourceTimeZone");
			}
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = sourceTimeZone.GetAdjustmentRuleForTime(dateTime, out num);
			TimeSpan timeSpan = sourceTimeZone.BaseUtcOffset;
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = sourceTimeZone.GetDaylightTime(dateTime.Year, adjustmentRuleForTime, num);
					if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime))
					{
						throw new ArgumentException("The supplied DateTime represents an invalid time.  For example, when the clock is adjusted forward, any time in the period that is skipped is invalid.", "dateTime");
					}
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(dateTime, adjustmentRuleForTime, daylightTime, flags);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			DateTimeKind correspondingKind2 = cachedData.GetCorrespondingKind(destinationTimeZone);
			if (dateTime.Kind != DateTimeKind.Unspecified && correspondingKind != DateTimeKind.Unspecified && correspondingKind == correspondingKind2)
			{
				return dateTime;
			}
			bool flag;
			DateTime dateTime2 = TimeZoneInfo.ConvertUtcToTimeZone(dateTime.Ticks - timeSpan.Ticks, destinationTimeZone, out flag);
			if (correspondingKind2 == DateTimeKind.Local)
			{
				return new DateTime(dateTime2.Ticks, DateTimeKind.Local, flag);
			}
			return new DateTime(dateTime2.Ticks, correspondingKind2);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00016220 File Offset: 0x00014420
		internal static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, flags, cachedData);
		}

		/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo" /> object and another <see cref="T:System.TimeZoneInfo" /> object are equal.</summary>
		/// <returns>true if the two <see cref="T:System.TimeZoneInfo" /> objects are equal; otherwise, false.</returns>
		/// <param name="other">A second object to compare with the current object.  </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000400 RID: 1024 RVA: 0x00016252 File Offset: 0x00014452
		public bool Equals(TimeZoneInfo other)
		{
			return other != null && string.Equals(this._id, other._id, StringComparison.OrdinalIgnoreCase) && this.HasSameRules(other);
		}

		/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo" /> object and another object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.TimeZoneInfo" /> object that is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">A second object to compare with the current object.  </param>
		// Token: 0x06000401 RID: 1025 RVA: 0x00016274 File Offset: 0x00014474
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimeZoneInfo);
		}

		/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
		/// <returns>A 32-bit signed integer that serves as the hash code for this <see cref="T:System.TimeZoneInfo" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000402 RID: 1026 RVA: 0x00016282 File Offset: 0x00014482
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this._id);
		}

		/// <summary>Returns a sorted collection of all the time zones about which information is available on the local system.</summary>
		/// <returns>A read-only collection of <see cref="T:System.TimeZoneInfo" /> objects.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to store all time zone information.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to read from the registry keys that contain time zone information.</exception>
		// Token: 0x06000403 RID: 1027 RVA: 0x00016294 File Offset: 0x00014494
		public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData cachedData2 = cachedData;
			lock (cachedData2)
			{
				if (cachedData._readOnlySystemTimeZones == null)
				{
					TimeZoneInfo.PopulateAllSystemTimeZones(cachedData);
					cachedData._allSystemTimeZonesRead = true;
					List<TimeZoneInfo> list;
					if (cachedData._systemTimeZones != null)
					{
						list = new List<TimeZoneInfo>(cachedData._systemTimeZones.Values);
					}
					else
					{
						list = new List<TimeZoneInfo>();
					}
					list.Sort(delegate(TimeZoneInfo x, TimeZoneInfo y)
					{
						int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
						if (num != 0)
						{
							return num;
						}
						return string.CompareOrdinal(x.DisplayName, y.DisplayName);
					});
					cachedData._readOnlySystemTimeZones = new ReadOnlyCollection<TimeZoneInfo>(list);
				}
			}
			return cachedData._readOnlySystemTimeZones;
		}

		/// <summary>Indicates whether the current object and another <see cref="T:System.TimeZoneInfo" /> object have the same adjustment rules.</summary>
		/// <returns>true if the two time zones have identical adjustment rules and an identical base offset; otherwise, false.</returns>
		/// <param name="other">A second object to compare with the current <see cref="T:System.TimeZoneInfo" /> object.   </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is null.</exception>
		// Token: 0x06000404 RID: 1028 RVA: 0x0001633C File Offset: 0x0001453C
		public bool HasSameRules(TimeZoneInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._baseUtcOffset != other._baseUtcOffset || this._supportsDaylightSavingTime != other._supportsDaylightSavingTime)
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule[] adjustmentRules = this._adjustmentRules;
			TimeZoneInfo.AdjustmentRule[] adjustmentRules2 = other._adjustmentRules;
			bool flag = (adjustmentRules == null && adjustmentRules2 == null) || (adjustmentRules != null && adjustmentRules2 != null);
			if (!flag)
			{
				return false;
			}
			if (adjustmentRules != null)
			{
				if (adjustmentRules.Length != adjustmentRules2.Length)
				{
					return false;
				}
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					if (!adjustmentRules[i].Equals(adjustmentRules2[i]))
					{
						return false;
					}
				}
			}
			return flag;
		}

		/// <summary>Gets a <see cref="T:System.TimeZoneInfo" /> object that represents the local time zone.</summary>
		/// <returns>An object that represents the local time zone.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x000163CC File Offset: 0x000145CC
		public static TimeZoneInfo Local
		{
			get
			{
				return TimeZoneInfo.s_cachedData.Local;
			}
		}

		/// <summary>Returns the current <see cref="T:System.TimeZoneInfo" /> object's display name.</summary>
		/// <returns>The value of the <see cref="P:System.TimeZoneInfo.DisplayName" /> property of the current <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x06000406 RID: 1030 RVA: 0x000163D8 File Offset: 0x000145D8
		public override string ToString()
		{
			return this.DisplayName;
		}

		/// <summary>Gets a <see cref="T:System.TimeZoneInfo" /> object that represents the Coordinated Universal Time (UTC) zone.</summary>
		/// <returns>An object that represents the Coordinated Universal Time (UTC) zone.</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000163E0 File Offset: 0x000145E0
		public static TimeZoneInfo Utc
		{
			get
			{
				return TimeZoneInfo.s_utcTimeZone;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000163E8 File Offset: 0x000145E8
		private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			bool flag;
			TimeZoneInfo.ValidateTimeZoneInfo(id, baseUtcOffset, adjustmentRules, out flag);
			this._id = id;
			this._baseUtcOffset = baseUtcOffset;
			this._displayName = displayName;
			this._standardDisplayName = standardDisplayName;
			this._daylightDisplayName = (disableDaylightSavingTime ? null : daylightDisplayName);
			this._supportsDaylightSavingTime = flag && !disableDaylightSavingTime;
			this._adjustmentRules = adjustmentRules;
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, and a standard time display name.</summary>
		/// <returns>The new time zone.</returns>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">An object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.   </param>
		/// <param name="standardDisplayName">The name of the new time zone's standard time.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").-or-The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000409 RID: 1033 RVA: 0x0001644B File Offset: 0x0001464B
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, standardDisplayName, null, false);
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.TimeZoneInfo" /> object contains invalid or corrupted data.</exception>
		// Token: 0x0600040A RID: 1034 RVA: 0x0001645C File Offset: 0x0001465C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				bool flag;
				TimeZoneInfo.ValidateTimeZoneInfo(this._id, this._baseUtcOffset, this._adjustmentRules, out flag);
				if (flag != this._supportsDaylightSavingTime)
				{
					throw new SerializationException(SR.Format("The value of the field '{0}' is invalid.  The serialized data is corrupt.", "SupportsDaylightSavingTime"));
				}
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
			}
			catch (InvalidTimeZoneException ex2)
			{
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex2);
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x0600040B RID: 1035 RVA: 0x000164D8 File Offset: 0x000146D8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Id", this._id);
			info.AddValue("DisplayName", this._displayName);
			info.AddValue("StandardName", this._standardDisplayName);
			info.AddValue("DaylightName", this._daylightDisplayName);
			info.AddValue("BaseUtcOffset", this._baseUtcOffset);
			info.AddValue("AdjustmentRules", this._adjustmentRules);
			info.AddValue("SupportsDaylightSavingTime", this._supportsDaylightSavingTime);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00016570 File Offset: 0x00014770
		private TimeZoneInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._id = (string)info.GetValue("Id", typeof(string));
			this._displayName = (string)info.GetValue("DisplayName", typeof(string));
			this._standardDisplayName = (string)info.GetValue("StandardName", typeof(string));
			this._daylightDisplayName = (string)info.GetValue("DaylightName", typeof(string));
			this._baseUtcOffset = (TimeSpan)info.GetValue("BaseUtcOffset", typeof(TimeSpan));
			this._adjustmentRules = (TimeZoneInfo.AdjustmentRule[])info.GetValue("AdjustmentRules", typeof(TimeZoneInfo.AdjustmentRule[]));
			this._supportsDaylightSavingTime = (bool)info.GetValue("SupportsDaylightSavingTime", typeof(bool));
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00016671 File Offset: 0x00014871
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime, out int? ruleIndex)
		{
			return this.GetAdjustmentRuleForTime(dateTime, false, out ruleIndex);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001667C File Offset: 0x0001487C
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime, bool dateTimeisUtc, out int? ruleIndex)
		{
			if (this._adjustmentRules == null || this._adjustmentRules.Length == 0)
			{
				ruleIndex = null;
				return null;
			}
			DateTime dateTime2 = (dateTimeisUtc ? (dateTime + this.BaseUtcOffset).Date : dateTime.Date);
			int i = 0;
			int num = this._adjustmentRules.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				TimeZoneInfo.AdjustmentRule adjustmentRule = this._adjustmentRules[num2];
				TimeZoneInfo.AdjustmentRule adjustmentRule2 = ((num2 > 0) ? this._adjustmentRules[num2 - 1] : adjustmentRule);
				int num3 = this.CompareAdjustmentRuleToDateTime(adjustmentRule, adjustmentRule2, dateTime, dateTime2, dateTimeisUtc);
				if (num3 == 0)
				{
					ruleIndex = new int?(num2);
					return adjustmentRule;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			ruleIndex = null;
			return null;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00016740 File Offset: 0x00014940
		private int CompareAdjustmentRuleToDateTime(TimeZoneInfo.AdjustmentRule rule, TimeZoneInfo.AdjustmentRule previousRule, DateTime dateTime, DateTime dateOnly, bool dateTimeisUtc)
		{
			bool flag;
			if (rule.DateStart.Kind == DateTimeKind.Utc)
			{
				flag = (dateTimeisUtc ? dateTime : this.ConvertToUtc(dateTime, previousRule.DaylightDelta, previousRule.BaseUtcOffsetDelta)) >= rule.DateStart;
			}
			else
			{
				flag = dateOnly >= rule.DateStart;
			}
			if (!flag)
			{
				return 1;
			}
			bool flag2;
			if (rule.DateEnd.Kind == DateTimeKind.Utc)
			{
				flag2 = (dateTimeisUtc ? dateTime : this.ConvertToUtc(dateTime, rule.DaylightDelta, rule.BaseUtcOffsetDelta)) <= rule.DateEnd;
			}
			else
			{
				flag2 = dateOnly <= rule.DateEnd;
			}
			if (!flag2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000167E6 File Offset: 0x000149E6
		private DateTime ConvertToUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta)
		{
			return this.ConvertToFromUtc(dateTime, daylightDelta, baseUtcOffsetDelta, true);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000167F2 File Offset: 0x000149F2
		private DateTime ConvertFromUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta)
		{
			return this.ConvertToFromUtc(dateTime, daylightDelta, baseUtcOffsetDelta, false);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00016800 File Offset: 0x00014A00
		private DateTime ConvertToFromUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta, bool convertToUtc)
		{
			TimeSpan timeSpan = this.BaseUtcOffset + daylightDelta + baseUtcOffsetDelta;
			if (convertToUtc)
			{
				timeSpan = timeSpan.Negate();
			}
			long num = dateTime.Ticks + timeSpan.Ticks;
			if (num > DateTime.MaxValue.Ticks)
			{
				return DateTime.MaxValue;
			}
			if (num >= DateTime.MinValue.Ticks)
			{
				return new DateTime(num);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00016868 File Offset: 0x00014A68
		private static DateTime ConvertUtcToTimeZone(long ticks, TimeZoneInfo destinationTimeZone, out bool isAmbiguousLocalDst)
		{
			ticks += TimeZoneInfo.GetUtcOffsetFromUtc((ticks > DateTime.MaxValue.Ticks) ? DateTime.MaxValue : ((ticks < DateTime.MinValue.Ticks) ? DateTime.MinValue : new DateTime(ticks)), destinationTimeZone, out isAmbiguousLocalDst).Ticks;
			if (ticks > DateTime.MaxValue.Ticks)
			{
				return DateTime.MaxValue;
			}
			if (ticks >= DateTime.MinValue.Ticks)
			{
				return new DateTime(ticks);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000168E4 File Offset: 0x00014AE4
		private DaylightTimeStruct GetDaylightTime(int year, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			TimeSpan daylightDelta = rule.DaylightDelta;
			DateTime dateTime;
			DateTime dateTime2;
			if (rule.NoDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule previousAdjustmentRule = this.GetPreviousAdjustmentRule(rule, ruleIndex);
				dateTime = this.ConvertFromUtc(rule.DateStart, previousAdjustmentRule.DaylightDelta, previousAdjustmentRule.BaseUtcOffsetDelta);
				dateTime2 = this.ConvertFromUtc(rule.DateEnd, rule.DaylightDelta, rule.BaseUtcOffsetDelta);
			}
			else
			{
				dateTime = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionStart);
				dateTime2 = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionEnd);
			}
			return new DaylightTimeStruct(dateTime, dateTime2, daylightDelta);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00016960 File Offset: 0x00014B60
		private static bool GetIsDaylightSavings(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime, TimeZoneInfoOptions flags)
		{
			if (rule == null)
			{
				return false;
			}
			DateTime dateTime;
			DateTime dateTime2;
			if (time.Kind == DateTimeKind.Local)
			{
				dateTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + daylightTime.Delta));
				dateTime2 = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : daylightTime.End);
			}
			else
			{
				bool flag = rule.DaylightDelta > TimeSpan.Zero;
				dateTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + (flag ? rule.DaylightDelta : TimeSpan.Zero)));
				dateTime2 = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : (daylightTime.End + (flag ? (-rule.DaylightDelta) : TimeSpan.Zero)));
			}
			bool flag2 = TimeZoneInfo.CheckIsDst(dateTime, time, dateTime2, false, rule);
			if (flag2 && time.Kind == DateTimeKind.Local && TimeZoneInfo.GetIsAmbiguousTime(time, rule, daylightTime))
			{
				flag2 = time.IsAmbiguousDaylightSavingTime();
			}
			return flag2;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00016AB0 File Offset: 0x00014CB0
		private TimeSpan GetDaylightSavingsStartOffsetFromUtc(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			if (rule.NoDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule previousAdjustmentRule = this.GetPreviousAdjustmentRule(rule, ruleIndex);
				return baseUtcOffset + previousAdjustmentRule.BaseUtcOffsetDelta + previousAdjustmentRule.DaylightDelta;
			}
			return baseUtcOffset + rule.BaseUtcOffsetDelta;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00016AF2 File Offset: 0x00014CF2
		private TimeSpan GetDaylightSavingsEndOffsetFromUtc(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule rule)
		{
			return baseUtcOffset + rule.BaseUtcOffsetDelta + rule.DaylightDelta;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00016B0C File Offset: 0x00014D0C
		private static bool GetIsDaylightSavingsFromUtc(DateTime time, int year, TimeSpan utc, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex, out bool isAmbiguousLocalDst, TimeZoneInfo zone)
		{
			isAmbiguousLocalDst = false;
			if (rule == null)
			{
				return false;
			}
			DaylightTimeStruct daylightTime = zone.GetDaylightTime(year, rule, ruleIndex);
			bool flag = false;
			TimeSpan daylightSavingsStartOffsetFromUtc = zone.GetDaylightSavingsStartOffsetFromUtc(utc, rule, ruleIndex);
			DateTime dateTime;
			if (rule.IsStartDateMarkerForBeginningOfYear() && daylightTime.Start.Year > DateTime.MinValue.Year)
			{
				int? num;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.Start.Year - 1, 12, 31), out num);
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.IsEndDateMarkerForEndOfYear())
				{
					dateTime = zone.GetDaylightTime(daylightTime.Start.Year - 1, adjustmentRuleForTime, num).Start - utc - adjustmentRuleForTime.BaseUtcOffsetDelta;
					flag = true;
				}
				else
				{
					dateTime = new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) - daylightSavingsStartOffsetFromUtc;
				}
			}
			else
			{
				dateTime = daylightTime.Start - daylightSavingsStartOffsetFromUtc;
			}
			TimeSpan daylightSavingsEndOffsetFromUtc = zone.GetDaylightSavingsEndOffsetFromUtc(utc, rule);
			DateTime dateTime2;
			if (rule.IsEndDateMarkerForEndOfYear() && daylightTime.End.Year < DateTime.MaxValue.Year)
			{
				int? num2;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime2 = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.End.Year + 1, 1, 1), out num2);
				if (adjustmentRuleForTime2 != null && adjustmentRuleForTime2.IsStartDateMarkerForBeginningOfYear())
				{
					if (adjustmentRuleForTime2.IsEndDateMarkerForEndOfYear())
					{
						dateTime2 = new DateTime(daylightTime.End.Year + 1, 12, 31) - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					else
					{
						dateTime2 = zone.GetDaylightTime(daylightTime.End.Year + 1, adjustmentRuleForTime2, num2).End - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					flag = true;
				}
				else
				{
					dateTime2 = new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) - daylightSavingsEndOffsetFromUtc;
				}
			}
			else
			{
				dateTime2 = daylightTime.End - daylightSavingsEndOffsetFromUtc;
			}
			DateTime dateTime3;
			DateTime dateTime4;
			if (daylightTime.Delta.Ticks > 0L)
			{
				dateTime3 = dateTime2 - daylightTime.Delta;
				dateTime4 = dateTime2;
			}
			else
			{
				dateTime3 = dateTime;
				dateTime4 = dateTime - daylightTime.Delta;
			}
			bool flag2 = TimeZoneInfo.CheckIsDst(dateTime, time, dateTime2, flag, rule);
			if (flag2)
			{
				isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
				if (!isAmbiguousLocalDst && dateTime3.Year != dateTime4.Year)
				{
					try
					{
						dateTime3.AddYears(1);
						dateTime4.AddYears(1);
						isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
					if (!isAmbiguousLocalDst)
					{
						try
						{
							dateTime3.AddYears(-1);
							dateTime4.AddYears(-1);
							isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
						}
						catch (ArgumentOutOfRangeException)
						{
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00016E10 File Offset: 0x00015010
		private static bool CheckIsDst(DateTime startTime, DateTime time, DateTime endTime, bool ignoreYearAdjustment, TimeZoneInfo.AdjustmentRule rule)
		{
			if (!ignoreYearAdjustment && !rule.NoDaylightTransitions)
			{
				int year = startTime.Year;
				int year2 = endTime.Year;
				if (year != year2)
				{
					endTime = endTime.AddYears(year - year2);
				}
				int year3 = time.Year;
				if (year != year3)
				{
					time = time.AddYears(year - year3);
				}
			}
			if (startTime > endTime)
			{
				return time < endTime || time >= startTime;
			}
			if (rule.NoDaylightTransitions)
			{
				return time >= startTime && time <= endTime;
			}
			return time >= startTime && time < endTime;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00016EAC File Offset: 0x000150AC
		private static bool GetIsAmbiguousTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime dateTime;
			DateTime dateTime2;
			if (rule.DaylightDelta > TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				dateTime = daylightTime.End;
				dateTime2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				dateTime = daylightTime.Start;
				dateTime2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = time >= dateTime2 && time < dateTime;
			if (!flag && dateTime.Year != dateTime2.Year)
			{
				try
				{
					DateTime dateTime3 = dateTime.AddYears(1);
					DateTime dateTime4 = dateTime2.AddYears(1);
					flag = time >= dateTime4 && time < dateTime3;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime dateTime3 = dateTime.AddYears(-1);
						DateTime dateTime4 = dateTime2.AddYears(-1);
						flag = time >= dateTime4 && time < dateTime3;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00016FD0 File Offset: 0x000151D0
		private static bool GetIsInvalidTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime dateTime;
			DateTime dateTime2;
			if (rule.DaylightDelta < TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				dateTime = daylightTime.End;
				dateTime2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				dateTime = daylightTime.Start;
				dateTime2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = time >= dateTime && time < dateTime2;
			if (!flag && dateTime.Year != dateTime2.Year)
			{
				try
				{
					DateTime dateTime3 = dateTime.AddYears(1);
					DateTime dateTime4 = dateTime2.AddYears(1);
					flag = time >= dateTime3 && time < dateTime4;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime dateTime3 = dateTime.AddYears(-1);
						DateTime dateTime4 = dateTime2.AddYears(-1);
						flag = time >= dateTime3 && time < dateTime4;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000170F4 File Offset: 0x000152F4
		private static TimeSpan GetUtcOffset(DateTime time, TimeZoneInfo zone, TimeZoneInfoOptions flags)
		{
			TimeSpan timeSpan = zone.BaseUtcOffset;
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(time, out num);
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = zone.GetDaylightTime(time.Year, adjustmentRuleForTime, num);
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(time, adjustmentRuleForTime, daylightTime, flags);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00017160 File Offset: 0x00015360
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out flag);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00017178 File Offset: 0x00015378
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out isDaylightSavings, out flag);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00017190 File Offset: 0x00015390
		internal static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings, out bool isAmbiguousLocalDst)
		{
			isDaylightSavings = false;
			isAmbiguousLocalDst = false;
			TimeSpan timeSpan = zone.BaseUtcOffset;
			int? num;
			TimeZoneInfo.AdjustmentRule adjustmentRule;
			int num2;
			if (time > TimeZoneInfo.s_maxDateOnly)
			{
				adjustmentRule = zone.GetAdjustmentRuleForTime(DateTime.MaxValue, out num);
				num2 = 9999;
			}
			else if (time < TimeZoneInfo.s_minDateOnly)
			{
				adjustmentRule = zone.GetAdjustmentRuleForTime(DateTime.MinValue, out num);
				num2 = 1;
			}
			else
			{
				adjustmentRule = zone.GetAdjustmentRuleForTime(time, true, out num);
				num2 = (time + timeSpan).Year;
			}
			if (adjustmentRule != null)
			{
				timeSpan += adjustmentRule.BaseUtcOffsetDelta;
				if (adjustmentRule.HasDaylightSaving)
				{
					isDaylightSavings = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, num2, zone._baseUtcOffset, adjustmentRule, num, out isAmbiguousLocalDst, zone);
					timeSpan += (isDaylightSavings ? adjustmentRule.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001724C File Offset: 0x0001544C
		internal static DateTime TransitionTimeToDateTime(int year, TimeZoneInfo.TransitionTime transitionTime)
		{
			DateTime timeOfDay = transitionTime.TimeOfDay;
			DateTime dateTime;
			if (transitionTime.IsFixedDateRule)
			{
				int num = DateTime.DaysInMonth(year, transitionTime.Month);
				dateTime = new DateTime(year, transitionTime.Month, (num < transitionTime.Day) ? num : transitionTime.Day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
			}
			else if (transitionTime.Week <= 4)
			{
				dateTime = new DateTime(year, transitionTime.Month, 1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
				int dayOfWeek = (int)dateTime.DayOfWeek;
				int num2 = transitionTime.DayOfWeek - (DayOfWeek)dayOfWeek;
				if (num2 < 0)
				{
					num2 += 7;
				}
				num2 += 7 * (transitionTime.Week - 1);
				if (num2 > 0)
				{
					dateTime = dateTime.AddDays((double)num2);
				}
			}
			else
			{
				int num3 = DateTime.DaysInMonth(year, transitionTime.Month);
				dateTime = new DateTime(year, transitionTime.Month, num3, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
				int num4 = dateTime.DayOfWeek - transitionTime.DayOfWeek;
				if (num4 < 0)
				{
					num4 += 7;
				}
				if (num4 > 0)
				{
					dateTime = dateTime.AddDays((double)(-(double)num4));
				}
			}
			return dateTime;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001739C File Offset: 0x0001559C
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZone(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData, bool alwaysFallbackToLocalMachine = false)
		{
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.Success;
			e = null;
			TimeZoneInfo timeZoneInfo = null;
			if (cachedData._systemTimeZones != null && cachedData._systemTimeZones.TryGetValue(id, out timeZoneInfo))
			{
				if (dstDisabled && timeZoneInfo._supportsDaylightSavingTime)
				{
					value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName);
				}
				else
				{
					value = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
				}
				return timeZoneInfoResult;
			}
			if (!cachedData._allSystemTimeZonesRead || alwaysFallbackToLocalMachine)
			{
				timeZoneInfoResult = TimeZoneInfo.TryGetTimeZoneFromLocalMachine(id, dstDisabled, out value, out e, cachedData);
			}
			else
			{
				timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
				value = null;
			}
			return timeZoneInfoResult;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00017448 File Offset: 0x00015648
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneFromLocalMachine(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData)
		{
			TimeZoneInfo timeZoneInfo;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult = TimeZoneInfo.TryGetTimeZoneFromLocalMachine(id, out timeZoneInfo, out e);
			if (timeZoneInfoResult != TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				value = null;
				return timeZoneInfoResult;
			}
			if (cachedData._systemTimeZones == null)
			{
				cachedData._systemTimeZones = new Dictionary<string, TimeZoneInfo>(StringComparer.OrdinalIgnoreCase);
			}
			if (!cachedData._systemTimeZones.ContainsKey(id))
			{
				cachedData._systemTimeZones.Add(id, timeZoneInfo);
			}
			if (dstDisabled && timeZoneInfo._supportsDaylightSavingTime)
			{
				value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName);
				return timeZoneInfoResult;
			}
			value = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
			return timeZoneInfoResult;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000174F8 File Offset: 0x000156F8
		private static void ValidateTimeZoneInfo(string id, TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule[] adjustmentRules, out bool adjustmentRulesSupportDst)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0)
			{
				throw new ArgumentException(SR.Format("The specified ID parameter '{0}' is not supported.", id), "id");
			}
			if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset", "The TimeSpan parameter must be within plus or minus 14.0 hours.");
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException("The TimeSpan parameter cannot be specified more precisely than whole minutes.", "baseUtcOffset");
			}
			adjustmentRulesSupportDst = false;
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				adjustmentRulesSupportDst = true;
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					TimeZoneInfo.AdjustmentRule adjustmentRule2 = adjustmentRule;
					adjustmentRule = adjustmentRules[i];
					if (adjustmentRule == null)
					{
						throw new InvalidTimeZoneException("The AdjustmentRule array cannot contain null elements.");
					}
					if (!TimeZoneInfo.IsValidAdjustmentRuleOffest(baseUtcOffset, adjustmentRule))
					{
						throw new InvalidTimeZoneException("The sum of the BaseUtcOffset and DaylightDelta properties must within plus or minus 14.0 hours.");
					}
					if (adjustmentRule2 != null && adjustmentRule.DateStart <= adjustmentRule2.DateEnd)
					{
						throw new InvalidTimeZoneException("The elements of the AdjustmentRule array must be in chronological order and must not overlap.");
					}
				}
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000175D1 File Offset: 0x000157D1
		internal static bool UtcOffsetOutOfRange(TimeSpan offset)
		{
			return offset < TimeZoneInfo.MinOffset || offset > TimeZoneInfo.MaxOffset;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000175ED File Offset: 0x000157ED
		private static TimeSpan GetUtcOffset(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule adjustmentRule)
		{
			return baseUtcOffset + adjustmentRule.BaseUtcOffsetDelta + (adjustmentRule.HasDaylightSaving ? adjustmentRule.DaylightDelta : TimeSpan.Zero);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00017615 File Offset: 0x00015815
		private static bool IsValidAdjustmentRuleOffest(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule adjustmentRule)
		{
			return !TimeZoneInfo.UtcOffsetOutOfRange(TimeZoneInfo.GetUtcOffset(baseUtcOffset, adjustmentRule));
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000176B9 File Offset: 0x000158B9
		internal TimeZoneInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000285 RID: 645
		private static Lazy<bool> lazyHaveRegistry = new Lazy<bool>(delegate
		{
			bool flag;
			try
			{
				using (Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\TimeZoneInformation", false))
				{
					flag = true;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		});

		// Token: 0x04000286 RID: 646
		private readonly string _id;

		// Token: 0x04000287 RID: 647
		private readonly string _displayName;

		// Token: 0x04000288 RID: 648
		private readonly string _standardDisplayName;

		// Token: 0x04000289 RID: 649
		private readonly string _daylightDisplayName;

		// Token: 0x0400028A RID: 650
		private readonly TimeSpan _baseUtcOffset;

		// Token: 0x0400028B RID: 651
		private readonly bool _supportsDaylightSavingTime;

		// Token: 0x0400028C RID: 652
		private readonly TimeZoneInfo.AdjustmentRule[] _adjustmentRules;

		// Token: 0x0400028D RID: 653
		private static readonly TimeZoneInfo s_utcTimeZone = TimeZoneInfo.CreateCustomTimeZone("UTC", TimeSpan.Zero, "UTC", "UTC");

		// Token: 0x0400028E RID: 654
		private static TimeZoneInfo.CachedData s_cachedData = new TimeZoneInfo.CachedData();

		// Token: 0x0400028F RID: 655
		private static readonly DateTime s_maxDateOnly = new DateTime(9999, 12, 31);

		// Token: 0x04000290 RID: 656
		private static readonly DateTime s_minDateOnly = new DateTime(1, 1, 2);

		// Token: 0x04000291 RID: 657
		private static readonly TimeSpan MaxOffset = TimeSpan.FromHours(14.0);

		// Token: 0x04000292 RID: 658
		private static readonly TimeSpan MinOffset = -TimeZoneInfo.MaxOffset;

		// Token: 0x0200009C RID: 156
		private sealed class CachedData
		{
			// Token: 0x06000429 RID: 1065 RVA: 0x000176C0 File Offset: 0x000158C0
			private static TimeZoneInfo GetCurrentOneYearLocal()
			{
				Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION;
				if (Interop.Kernel32.GetTimeZoneInformation(out time_ZONE_INFORMATION) != 4294967295U)
				{
					return TimeZoneInfo.GetLocalTimeZoneFromWin32Data(in time_ZONE_INFORMATION, false);
				}
				return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x000176FC File Offset: 0x000158FC
			public TimeZoneInfo.OffsetAndRule GetOneYearLocalFromUtc(int year)
			{
				TimeZoneInfo.OffsetAndRule offsetAndRule = this._oneYearLocalFromUtc;
				if (offsetAndRule == null || offsetAndRule.Year != year)
				{
					TimeZoneInfo currentOneYearLocal = TimeZoneInfo.CachedData.GetCurrentOneYearLocal();
					TimeZoneInfo.AdjustmentRule adjustmentRule = ((currentOneYearLocal._adjustmentRules == null) ? null : currentOneYearLocal._adjustmentRules[0]);
					offsetAndRule = new TimeZoneInfo.OffsetAndRule(year, currentOneYearLocal.BaseUtcOffset, adjustmentRule);
					this._oneYearLocalFromUtc = offsetAndRule;
				}
				return offsetAndRule;
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x00017750 File Offset: 0x00015950
			private TimeZoneInfo CreateLocal()
			{
				TimeZoneInfo timeZoneInfo2;
				lock (this)
				{
					TimeZoneInfo timeZoneInfo = this._localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = TimeZoneInfo.GetLocalTimeZone(this);
						timeZoneInfo = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
						this._localTimeZone = timeZoneInfo;
					}
					timeZoneInfo2 = timeZoneInfo;
				}
				return timeZoneInfo2;
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600042C RID: 1068 RVA: 0x000177D0 File Offset: 0x000159D0
			public TimeZoneInfo Local
			{
				get
				{
					TimeZoneInfo timeZoneInfo = this._localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = this.CreateLocal();
					}
					return timeZoneInfo;
				}
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x000177F1 File Offset: 0x000159F1
			public DateTimeKind GetCorrespondingKind(TimeZoneInfo timeZone)
			{
				if (timeZone == TimeZoneInfo.s_utcTimeZone)
				{
					return DateTimeKind.Utc;
				}
				if (timeZone != this._localTimeZone)
				{
					return DateTimeKind.Unspecified;
				}
				return DateTimeKind.Local;
			}

			// Token: 0x04000293 RID: 659
			private volatile TimeZoneInfo.OffsetAndRule _oneYearLocalFromUtc;

			// Token: 0x04000294 RID: 660
			private volatile TimeZoneInfo _localTimeZone;

			// Token: 0x04000295 RID: 661
			public Dictionary<string, TimeZoneInfo> _systemTimeZones;

			// Token: 0x04000296 RID: 662
			public ReadOnlyCollection<TimeZoneInfo> _readOnlySystemTimeZones;

			// Token: 0x04000297 RID: 663
			public bool _allSystemTimeZonesRead;
		}

		// Token: 0x0200009D RID: 157
		private sealed class OffsetAndRule
		{
			// Token: 0x0600042F RID: 1071 RVA: 0x0001780B File Offset: 0x00015A0B
			public OffsetAndRule(int year, TimeSpan offset, TimeZoneInfo.AdjustmentRule rule)
			{
				this.Year = year;
				this.Offset = offset;
				this.Rule = rule;
			}

			// Token: 0x04000298 RID: 664
			public readonly int Year;

			// Token: 0x04000299 RID: 665
			public readonly TimeSpan Offset;

			// Token: 0x0400029A RID: 666
			public readonly TimeZoneInfo.AdjustmentRule Rule;
		}

		// Token: 0x0200009E RID: 158
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DYNAMIC_TIME_ZONE_INFORMATION
		{
			// Token: 0x0400029B RID: 667
			internal Interop.Kernel32.TIME_ZONE_INFORMATION TZI;

			// Token: 0x0400029C RID: 668
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string TimeZoneKeyName;

			// Token: 0x0400029D RID: 669
			internal byte DynamicDaylightTimeDisabled;
		}

		// Token: 0x0200009F RID: 159
		private enum TimeZoneInfoResult
		{
			// Token: 0x0400029F RID: 671
			Success,
			// Token: 0x040002A0 RID: 672
			TimeZoneNotFoundException,
			// Token: 0x040002A1 RID: 673
			InvalidTimeZoneException,
			// Token: 0x040002A2 RID: 674
			SecurityException
		}

		/// <summary>Provides information about a time zone adjustment, such as the transition to and from daylight saving time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x020000A0 RID: 160
		[Serializable]
		public sealed class AdjustmentRule : IEquatable<TimeZoneInfo.AdjustmentRule>, ISerializable, IDeserializationCallback
		{
			/// <summary>Gets the date when the adjustment rule takes effect.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates when the adjustment rule takes effect.</returns>
			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000430 RID: 1072 RVA: 0x00017828 File Offset: 0x00015A28
			public DateTime DateStart
			{
				get
				{
					return this._dateStart;
				}
			}

			/// <summary>Gets the date when the adjustment rule ceases to be in effect.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates the end date of the adjustment rule.</returns>
			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000431 RID: 1073 RVA: 0x00017830 File Offset: 0x00015A30
			public DateTime DateEnd
			{
				get
				{
					return this._dateEnd;
				}
			}

			/// <summary>Gets the amount of time that is required to form the time zone's daylight saving time. This amount of time is added to the time zone's offset from Coordinated Universal Time (UTC).</summary>
			/// <returns>A <see cref="T:System.TimeSpan" /> object that indicates the amount of time to add to the standard time changes as a result of the adjustment rule.</returns>
			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x00017838 File Offset: 0x00015A38
			public TimeSpan DaylightDelta
			{
				get
				{
					return this._daylightDelta;
				}
			}

			/// <summary>Gets information about the annual transition from standard time to daylight saving time.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the annual transition from a time zone's standard time to daylight saving time.</returns>
			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x00017840 File Offset: 0x00015A40
			public TimeZoneInfo.TransitionTime DaylightTransitionStart
			{
				get
				{
					return this._daylightTransitionStart;
				}
			}

			/// <summary>Gets information about the annual transition from daylight saving time back to standard time.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the annual transition from daylight saving time back to the time zone's standard time.</returns>
			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x00017848 File Offset: 0x00015A48
			public TimeZoneInfo.TransitionTime DaylightTransitionEnd
			{
				get
				{
					return this._daylightTransitionEnd;
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000435 RID: 1077 RVA: 0x00017850 File Offset: 0x00015A50
			internal TimeSpan BaseUtcOffsetDelta
			{
				get
				{
					return this._baseUtcOffsetDelta;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x00017858 File Offset: 0x00015A58
			internal bool NoDaylightTransitions
			{
				get
				{
					return this._noDaylightTransitions;
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x06000437 RID: 1079 RVA: 0x00017860 File Offset: 0x00015A60
			internal bool HasDaylightSaving
			{
				get
				{
					return this.DaylightDelta != TimeSpan.Zero || (this.DaylightTransitionStart != default(TimeZoneInfo.TransitionTime) && this.DaylightTransitionStart.TimeOfDay != DateTime.MinValue) || (this.DaylightTransitionEnd != default(TimeZoneInfo.TransitionTime) && this.DaylightTransitionEnd.TimeOfDay != DateTime.MinValue.AddMilliseconds(1.0));
				}
			}

			/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object is equal to a second <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</summary>
			/// <returns>true if both <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects have equal values; otherwise, false.</returns>
			/// <param name="other">The object to compare with the current object.</param>
			/// <filterpriority>2</filterpriority>
			// Token: 0x06000438 RID: 1080 RVA: 0x000178F0 File Offset: 0x00015AF0
			public bool Equals(TimeZoneInfo.AdjustmentRule other)
			{
				return other != null && this._dateStart == other._dateStart && this._dateEnd == other._dateEnd && this._daylightDelta == other._daylightDelta && this._baseUtcOffsetDelta == other._baseUtcOffsetDelta && this._daylightTransitionEnd.Equals(other._daylightTransitionEnd) && this._daylightTransitionStart.Equals(other._daylightTransitionStart);
			}

			/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
			/// <returns>A 32-bit signed integer that serves as the hash code for the current <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</returns>
			/// <filterpriority>2</filterpriority>
			// Token: 0x06000439 RID: 1081 RVA: 0x00017972 File Offset: 0x00015B72
			public override int GetHashCode()
			{
				return this._dateStart.GetHashCode();
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x00017980 File Offset: 0x00015B80
			private AdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta, bool noDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, noDaylightTransitions);
				this._dateStart = dateStart;
				this._dateEnd = dateEnd;
				this._daylightDelta = daylightDelta;
				this._daylightTransitionStart = daylightTransitionStart;
				this._daylightTransitionEnd = daylightTransitionEnd;
				this._baseUtcOffsetDelta = baseUtcOffsetDelta;
				this._noDaylightTransitions = noDaylightTransitions;
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x000179D6 File Offset: 0x00015BD6
			internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta, bool noDaylightTransitions)
			{
				return new TimeZoneInfo.AdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, baseUtcOffsetDelta, noDaylightTransitions);
			}

			// Token: 0x0600043C RID: 1084 RVA: 0x000179E8 File Offset: 0x00015BE8
			internal bool IsStartDateMarkerForBeginningOfYear()
			{
				return !this.NoDaylightTransitions && this.DaylightTransitionStart.Month == 1 && this.DaylightTransitionStart.Day == 1 && this.DaylightTransitionStart.TimeOfDay.Hour == 0 && this.DaylightTransitionStart.TimeOfDay.Minute == 0 && this.DaylightTransitionStart.TimeOfDay.Second == 0 && this._dateStart.Year == this._dateEnd.Year;
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x00017A84 File Offset: 0x00015C84
			internal bool IsEndDateMarkerForEndOfYear()
			{
				return !this.NoDaylightTransitions && this.DaylightTransitionEnd.Month == 1 && this.DaylightTransitionEnd.Day == 1 && this.DaylightTransitionEnd.TimeOfDay.Hour == 0 && this.DaylightTransitionEnd.TimeOfDay.Minute == 0 && this.DaylightTransitionEnd.TimeOfDay.Second == 0 && this._dateStart.Year == this._dateEnd.Year;
			}

			// Token: 0x0600043E RID: 1086 RVA: 0x00017B20 File Offset: 0x00015D20
			private static void ValidateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, bool noDaylightTransitions)
			{
				if (dateStart.Kind != DateTimeKind.Unspecified && dateStart.Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified or DateTimeKind.Utc.", "dateStart");
				}
				if (dateEnd.Kind != DateTimeKind.Unspecified && dateEnd.Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified or DateTimeKind.Utc.", "dateEnd");
				}
				if (daylightTransitionStart.Equals(daylightTransitionEnd) && !noDaylightTransitions)
				{
					throw new ArgumentException("The DaylightTransitionStart property must not equal the DaylightTransitionEnd property.", "daylightTransitionEnd");
				}
				if (dateStart > dateEnd)
				{
					throw new ArgumentException("The DateStart property must come before the DateEnd property.", "dateStart");
				}
				if (daylightDelta.TotalHours < -23.0 || daylightDelta.TotalHours > 14.0)
				{
					throw new ArgumentOutOfRangeException("daylightDelta", daylightDelta, "The TimeSpan parameter must be within plus or minus 14.0 hours.");
				}
				if (daylightDelta.Ticks % 600000000L != 0L)
				{
					throw new ArgumentException("The TimeSpan parameter cannot be specified more precisely than whole minutes.", "daylightDelta");
				}
				if (dateStart != DateTime.MinValue && dateStart.Kind == DateTimeKind.Unspecified && dateStart.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException("The supplied DateTime includes a TimeOfDay setting.   This is not supported.", "dateStart");
				}
				if (dateEnd != DateTime.MaxValue && dateEnd.Kind == DateTimeKind.Unspecified && dateEnd.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException("The supplied DateTime includes a TimeOfDay setting.   This is not supported.", "dateEnd");
				}
			}

			/// <summary>Runs when the deserialization of a <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object is completed.</summary>
			/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.   </param>
			// Token: 0x0600043F RID: 1087 RVA: 0x00017C78 File Offset: 0x00015E78
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(this._dateStart, this._dateEnd, this._daylightDelta, this._daylightTransitionStart, this._daylightTransitionEnd, this._noDaylightTransitions);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
				}
			}

			/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data that is required to serialize this object.</summary>
			/// <param name="info">The object to populate with data.</param>
			/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
			// Token: 0x06000440 RID: 1088 RVA: 0x00017CD0 File Offset: 0x00015ED0
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("DateStart", this._dateStart);
				info.AddValue("DateEnd", this._dateEnd);
				info.AddValue("DaylightDelta", this._daylightDelta);
				info.AddValue("DaylightTransitionStart", this._daylightTransitionStart);
				info.AddValue("DaylightTransitionEnd", this._daylightTransitionEnd);
				info.AddValue("BaseUtcOffsetDelta", this._baseUtcOffsetDelta);
				info.AddValue("NoDaylightTransitions", this._noDaylightTransitions);
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x00017D78 File Offset: 0x00015F78
			private AdjustmentRule(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this._dateStart = (DateTime)info.GetValue("DateStart", typeof(DateTime));
				this._dateEnd = (DateTime)info.GetValue("DateEnd", typeof(DateTime));
				this._daylightDelta = (TimeSpan)info.GetValue("DaylightDelta", typeof(TimeSpan));
				this._daylightTransitionStart = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionStart", typeof(TimeZoneInfo.TransitionTime));
				this._daylightTransitionEnd = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionEnd", typeof(TimeZoneInfo.TransitionTime));
				object obj = info.GetValueNoThrow("BaseUtcOffsetDelta", typeof(TimeSpan));
				if (obj != null)
				{
					this._baseUtcOffsetDelta = (TimeSpan)obj;
				}
				obj = info.GetValueNoThrow("NoDaylightTransitions", typeof(bool));
				if (obj != null)
				{
					this._noDaylightTransitions = (bool)obj;
				}
			}

			// Token: 0x06000442 RID: 1090 RVA: 0x000176B9 File Offset: 0x000158B9
			internal AdjustmentRule()
			{
				ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x040002A3 RID: 675
			private readonly DateTime _dateStart;

			// Token: 0x040002A4 RID: 676
			private readonly DateTime _dateEnd;

			// Token: 0x040002A5 RID: 677
			private readonly TimeSpan _daylightDelta;

			// Token: 0x040002A6 RID: 678
			private readonly TimeZoneInfo.TransitionTime _daylightTransitionStart;

			// Token: 0x040002A7 RID: 679
			private readonly TimeZoneInfo.TransitionTime _daylightTransitionEnd;

			// Token: 0x040002A8 RID: 680
			private readonly TimeSpan _baseUtcOffsetDelta;

			// Token: 0x040002A9 RID: 681
			private readonly bool _noDaylightTransitions;
		}

		/// <summary>Provides information about a specific time change, such as the change from daylight saving time to standard time or vice versa, in a particular time zone.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x020000A1 RID: 161
		[Serializable]
		public readonly struct TransitionTime : IEquatable<TimeZoneInfo.TransitionTime>, ISerializable, IDeserializationCallback
		{
			/// <summary>Gets the hour, minute, and second at which the time change occurs.</summary>
			/// <returns>The time of day at which the time change occurs.</returns>
			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000443 RID: 1091 RVA: 0x00017E83 File Offset: 0x00016083
			public DateTime TimeOfDay
			{
				get
				{
					return this._timeOfDay;
				}
			}

			/// <summary>Gets the month in which the time change occurs.</summary>
			/// <returns>The month in which the time change occurs.</returns>
			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000444 RID: 1092 RVA: 0x00017E8B File Offset: 0x0001608B
			public int Month
			{
				get
				{
					return (int)this._month;
				}
			}

			/// <summary>Gets the week of the month in which a time change occurs.</summary>
			/// <returns>The week of the month in which the time change occurs.</returns>
			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000445 RID: 1093 RVA: 0x00017E93 File Offset: 0x00016093
			public int Week
			{
				get
				{
					return (int)this._week;
				}
			}

			/// <summary>Gets the day on which the time change occurs.</summary>
			/// <returns>The day on which the time change occurs.</returns>
			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000446 RID: 1094 RVA: 0x00017E9B File Offset: 0x0001609B
			public int Day
			{
				get
				{
					return (int)this._day;
				}
			}

			/// <summary>Gets the day of the week on which the time change occurs.</summary>
			/// <returns>The day of the week on which the time change occurs.</returns>
			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000447 RID: 1095 RVA: 0x00017EA3 File Offset: 0x000160A3
			public DayOfWeek DayOfWeek
			{
				get
				{
					return this._dayOfWeek;
				}
			}

			/// <summary>Gets a value indicating whether the time change occurs at a fixed date and time (such as November 1) or a floating date and time (such as the last Sunday of October).</summary>
			/// <returns>true if the time change rule is fixed-date; false if the time change rule is floating-date.</returns>
			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000448 RID: 1096 RVA: 0x00017EAB File Offset: 0x000160AB
			public bool IsFixedDateRule
			{
				get
				{
					return this._isFixedDateRule;
				}
			}

			/// <summary>Determines whether an object has identical values to the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</summary>
			/// <returns>true if the two objects are equal; otherwise, false.</returns>
			/// <param name="obj">An object to compare with the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.   </param>
			/// <filterpriority>2</filterpriority>
			// Token: 0x06000449 RID: 1097 RVA: 0x00017EB3 File Offset: 0x000160B3
			public override bool Equals(object obj)
			{
				return obj is TimeZoneInfo.TransitionTime && this.Equals((TimeZoneInfo.TransitionTime)obj);
			}

			/// <summary>Determines whether two specified <see cref="T:System.TimeZoneInfo.TransitionTime" /> objects are not equal.</summary>
			/// <returns>true if <paramref name="t1" /> and <paramref name="t2" /> have any different member values; otherwise, false.</returns>
			/// <param name="t1">The first object to compare.</param>
			/// <param name="t2">The second object to compare.</param>
			// Token: 0x0600044A RID: 1098 RVA: 0x00017ECB File Offset: 0x000160CB
			public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return !t1.Equals(t2);
			}

			/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object has identical values to a second <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</summary>
			/// <returns>true if the two objects have identical property values; otherwise, false.</returns>
			/// <param name="other">An object to compare to the current instance. </param>
			/// <filterpriority>2</filterpriority>
			// Token: 0x0600044B RID: 1099 RVA: 0x00017ED8 File Offset: 0x000160D8
			public bool Equals(TimeZoneInfo.TransitionTime other)
			{
				if (this._isFixedDateRule != other._isFixedDateRule || !(this._timeOfDay == other._timeOfDay) || this._month != other._month)
				{
					return false;
				}
				if (!other._isFixedDateRule)
				{
					return this._week == other._week && this._dayOfWeek == other._dayOfWeek;
				}
				return this._day == other._day;
			}

			/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
			/// <returns>A 32-bit signed integer that serves as the hash code for this <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</returns>
			/// <filterpriority>2</filterpriority>
			// Token: 0x0600044C RID: 1100 RVA: 0x00017F4B File Offset: 0x0001614B
			public override int GetHashCode()
			{
				return (int)this._month ^ ((int)this._week << 8);
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x00017F5C File Offset: 0x0001615C
			private TransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek, bool isFixedDateRule)
			{
				TimeZoneInfo.TransitionTime.ValidateTransitionTime(timeOfDay, month, week, day, dayOfWeek);
				this._timeOfDay = timeOfDay;
				this._month = (byte)month;
				this._week = (byte)week;
				this._day = (byte)day;
				this._dayOfWeek = dayOfWeek;
				this._isFixedDateRule = isFixedDateRule;
			}

			/// <summary>Defines a time change that uses a fixed-date rule.</summary>
			/// <returns>Data about the time change.</returns>
			/// <param name="timeOfDay">The time at which the time change occurs.</param>
			/// <param name="month">The month in which the time change occurs.</param>
			/// <param name="day">The day of the month on which the time change occurs.</param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="timeOfDay" /> parameter has a non-default date component.-or-The <paramref name="timeOfDay" /> parameter's <see cref="P:System.DateTime.Kind" /> property is not <see cref="F:System.DateTimeKind.Unspecified" />.-or-The <paramref name="timeOfDay" /> parameter does not represent a whole number of milliseconds.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="month" /> parameter is less than 1 or greater than 12.-or-The <paramref name="day" /> parameter is less than 1 or greater than 31.</exception>
			// Token: 0x0600044E RID: 1102 RVA: 0x00017F9A File Offset: 0x0001619A
			public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, 1, day, DayOfWeek.Sunday, true);
			}

			/// <summary>Defines a time change that uses a floating-date rule.</summary>
			/// <returns>Data about the time change.</returns>
			/// <param name="timeOfDay">The time at which the time change occurs.</param>
			/// <param name="month">The month in which the time change occurs.</param>
			/// <param name="week">The week of the month in which the time change occurs.</param>
			/// <param name="dayOfWeek">The day of the week on which the time change occurs.</param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="timeOfDay" /> parameter has a non-default date component.-or-The <paramref name="timeOfDay" /> parameter does not represent a whole number of milliseconds.-or-The <paramref name="timeOfDay" /> parameter's <see cref="P:System.DateTime.Kind" /> property is not <see cref="F:System.DateTimeKind.Unspecified" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="month" /> is less than 1 or greater than 12.-or-<paramref name="week" /> is less than 1 or greater than 5.-or-The <paramref name="dayOfWeek" /> parameter is not a member of the <see cref="T:System.DayOfWeek" /> enumeration.</exception>
			// Token: 0x0600044F RID: 1103 RVA: 0x00017FA7 File Offset: 0x000161A7
			public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, week, 1, dayOfWeek, false);
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x00017FB4 File Offset: 0x000161B4
			private static void ValidateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek)
			{
				if (timeOfDay.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified.", "timeOfDay");
				}
				if (month < 1 || month > 12)
				{
					throw new ArgumentOutOfRangeException("month", "The Month parameter must be in the range 1 through 12.");
				}
				if (day < 1 || day > 31)
				{
					throw new ArgumentOutOfRangeException("day", "The Day parameter must be in the range 1 through 31.");
				}
				if (week < 1 || week > 5)
				{
					throw new ArgumentOutOfRangeException("week", "The Week parameter must be in the range 1 through 5.");
				}
				if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException("dayOfWeek", "The DayOfWeek enumeration must be in the range 0 through 6.");
				}
				int num;
				int num2;
				int num3;
				timeOfDay.GetDatePart(out num, out num2, out num3);
				if (num != 1 || num2 != 1 || num3 != 1 || timeOfDay.Ticks % 10000L != 0L)
				{
					throw new ArgumentException("The supplied DateTime must have the Year, Month, and Day properties set to 1.  The time cannot be specified more precisely than whole milliseconds.", "timeOfDay");
				}
			}

			/// <summary>Runs when the deserialization of an object has been completed.</summary>
			/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.   </param>
			// Token: 0x06000451 RID: 1105 RVA: 0x00018078 File Offset: 0x00016278
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.TransitionTime.ValidateTransitionTime(this._timeOfDay, (int)this._month, (int)this._week, (int)this._day, this._dayOfWeek);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", ex);
				}
			}

			/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data that is required to serialize this object.</summary>
			/// <param name="info">The object to populate with data.</param>
			/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
			// Token: 0x06000452 RID: 1106 RVA: 0x000180C8 File Offset: 0x000162C8
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("TimeOfDay", this._timeOfDay);
				info.AddValue("Month", this._month);
				info.AddValue("Week", this._week);
				info.AddValue("Day", this._day);
				info.AddValue("DayOfWeek", this._dayOfWeek);
				info.AddValue("IsFixedDateRule", this._isFixedDateRule);
			}

			// Token: 0x06000453 RID: 1107 RVA: 0x00018150 File Offset: 0x00016350
			private TransitionTime(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this._timeOfDay = (DateTime)info.GetValue("TimeOfDay", typeof(DateTime));
				this._month = (byte)info.GetValue("Month", typeof(byte));
				this._week = (byte)info.GetValue("Week", typeof(byte));
				this._day = (byte)info.GetValue("Day", typeof(byte));
				this._dayOfWeek = (DayOfWeek)info.GetValue("DayOfWeek", typeof(DayOfWeek));
				this._isFixedDateRule = (bool)info.GetValue("IsFixedDateRule", typeof(bool));
			}

			// Token: 0x040002AA RID: 682
			private readonly DateTime _timeOfDay;

			// Token: 0x040002AB RID: 683
			private readonly byte _month;

			// Token: 0x040002AC RID: 684
			private readonly byte _week;

			// Token: 0x040002AD RID: 685
			private readonly byte _day;

			// Token: 0x040002AE RID: 686
			private readonly DayOfWeek _dayOfWeek;

			// Token: 0x040002AF RID: 687
			private readonly bool _isFixedDateRule;
		}
	}
}
