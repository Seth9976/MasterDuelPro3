using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x02000159 RID: 345
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
	[MovedFrom("UnityEditor")]
	internal class NumericFieldDraggerUtility
	{
		// Token: 0x06000F04 RID: 3844 RVA: 0x0001FC68 File Offset: 0x0001DE68
		public static float Acceleration(bool shiftPressed, bool altPressed)
		{
			return (float)(shiftPressed ? 4 : 1) * (altPressed ? 0.25f : 1f);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0001FC94 File Offset: 0x0001DE94
		public static float NiceDelta(Vector2 deviceDelta, float acceleration)
		{
			deviceDelta.y = -deviceDelta.y;
			bool flag = Mathf.Abs(Mathf.Abs(deviceDelta.x) - Mathf.Abs(deviceDelta.y)) / Mathf.Max(Mathf.Abs(deviceDelta.x), Mathf.Abs(deviceDelta.y)) > 0.1f;
			if (flag)
			{
				bool flag2 = Mathf.Abs(deviceDelta.x) > Mathf.Abs(deviceDelta.y);
				if (flag2)
				{
					NumericFieldDraggerUtility.s_UseYSign = false;
				}
				else
				{
					NumericFieldDraggerUtility.s_UseYSign = true;
				}
			}
			bool flag3 = NumericFieldDraggerUtility.s_UseYSign;
			float num;
			if (flag3)
			{
				num = Mathf.Sign(deviceDelta.y) * deviceDelta.magnitude * acceleration;
			}
			else
			{
				num = Mathf.Sign(deviceDelta.x) * deviceDelta.magnitude * acceleration;
			}
			return num;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0001FD5C File Offset: 0x0001DF5C
		public static double CalculateFloatDragSensitivity(double value)
		{
			bool flag = double.IsInfinity(value) || double.IsNaN(value);
			double num;
			if (flag)
			{
				num = 0.0;
			}
			else
			{
				num = Math.Max(1.0, Math.Pow(Math.Abs(value), 0.5)) * 0.029999999329447746;
			}
			return num;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		public static double CalculateFloatDragSensitivity(double value, double minValue, double maxValue)
		{
			bool flag = double.IsInfinity(value) || double.IsNaN(value);
			double num;
			if (flag)
			{
				num = 0.0;
			}
			else
			{
				double range = Math.Abs(maxValue - minValue);
				num = range / 100.0 * 0.029999999329447746;
			}
			return num;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0001FE10 File Offset: 0x0001E010
		public static long CalculateIntDragSensitivity(long value)
		{
			return (long)NumericFieldDraggerUtility.CalculateIntDragSensitivity((double)value);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0001FE2C File Offset: 0x0001E02C
		public static ulong CalculateIntDragSensitivity(ulong value)
		{
			return (ulong)NumericFieldDraggerUtility.CalculateIntDragSensitivity(value);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0001FE48 File Offset: 0x0001E048
		public static double CalculateIntDragSensitivity(double value)
		{
			return Math.Max(1.0, Math.Pow(Math.Abs(value), 0.5) * 0.029999999329447746);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0001FE88 File Offset: 0x0001E088
		public static long CalculateIntDragSensitivity(long value, long minValue, long maxValue)
		{
			long range = Math.Abs(maxValue - minValue);
			return Math.Max(1L, (long)(0.03f * (float)range / 100f));
		}

		// Token: 0x040005F4 RID: 1524
		private static bool s_UseYSign;
	}
}
