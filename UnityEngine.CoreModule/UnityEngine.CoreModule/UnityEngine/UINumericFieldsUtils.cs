using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001E3 RID: 483
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
	internal static class UINumericFieldsUtils
	{
		// Token: 0x06001286 RID: 4742 RVA: 0x0002721C File Offset: 0x0002541C
		public static bool TryConvertStringToDouble(string str, out double value, out ExpressionEvaluator.Expression expr)
		{
			expr = null;
			string lowered = str.ToLower();
			string text = lowered;
			string text2 = text;
			if (!(text2 == "inf") && !(text2 == "infinity"))
			{
				if (!(text2 == "-inf") && !(text2 == "-infinity"))
				{
					if (!(text2 == "nan"))
					{
						return ExpressionEvaluator.Evaluate<double>(str, out value, out expr);
					}
					value = double.NaN;
				}
				else
				{
					value = double.NegativeInfinity;
				}
			}
			else
			{
				value = double.PositiveInfinity;
			}
			return true;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000272B4 File Offset: 0x000254B4
		public static bool TryConvertStringToDouble(string str, string initialValueAsString, out double value)
		{
			ExpressionEvaluator.Expression expression;
			bool success = UINumericFieldsUtils.TryConvertStringToDouble(str, out value, out expression);
			bool flag = !success && expression != null && !string.IsNullOrEmpty(initialValueAsString);
			if (flag)
			{
				double oldValue;
				ExpressionEvaluator.Expression expression2;
				bool flag2 = UINumericFieldsUtils.TryConvertStringToDouble(initialValueAsString, out oldValue, out expression2);
				if (flag2)
				{
					value = oldValue;
					success = expression.Evaluate<double>(ref value, 0, 1);
				}
			}
			return success;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0002730C File Offset: 0x0002550C
		public static bool TryConvertStringToFloat(string str, string initialValueAsString, out float value)
		{
			double v;
			bool success = UINumericFieldsUtils.TryConvertStringToDouble(str, initialValueAsString, out v);
			value = Mathf.ClampToFloat(v);
			return success;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00027334 File Offset: 0x00025534
		public static bool TryConvertStringToLong(string str, out long value, out ExpressionEvaluator.Expression expr)
		{
			return ExpressionEvaluator.Evaluate<long>(str, out value, out expr);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00027350 File Offset: 0x00025550
		public static bool TryConvertStringToLong(string str, string initialValueAsString, out long value)
		{
			ExpressionEvaluator.Expression expression;
			bool success = UINumericFieldsUtils.TryConvertStringToLong(str, out value, out expression);
			bool flag = !success && expression != null && !string.IsNullOrEmpty(initialValueAsString);
			if (flag)
			{
				long oldValue;
				ExpressionEvaluator.Expression expression2;
				bool flag2 = UINumericFieldsUtils.TryConvertStringToLong(initialValueAsString, out oldValue, out expression2);
				if (flag2)
				{
					value = oldValue;
					success = expression.Evaluate<long>(ref value, 0, 1);
				}
			}
			return success;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000273A8 File Offset: 0x000255A8
		public static bool TryConvertStringToULong(string str, out ulong value, out ExpressionEvaluator.Expression expr)
		{
			return ExpressionEvaluator.Evaluate<ulong>(str, out value, out expr);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000273C4 File Offset: 0x000255C4
		public static bool TryConvertStringToULong(string str, string initialValueAsString, out ulong value)
		{
			ExpressionEvaluator.Expression expression;
			bool success = UINumericFieldsUtils.TryConvertStringToULong(str, out value, out expression);
			bool flag = !success && expression != null && !string.IsNullOrEmpty(initialValueAsString);
			if (flag)
			{
				ulong newValue;
				ExpressionEvaluator.Expression expression2;
				bool flag2 = UINumericFieldsUtils.TryConvertStringToULong(initialValueAsString, out newValue, out expression2);
				if (flag2)
				{
					value = newValue;
					success = expression.Evaluate<ulong>(ref value, 0, 1);
				}
			}
			return success;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0002741C File Offset: 0x0002561C
		public static bool TryConvertStringToInt(string str, string initialValueAsString, out int value)
		{
			long v;
			bool success = UINumericFieldsUtils.TryConvertStringToLong(str, initialValueAsString, out v);
			value = Mathf.ClampToInt(v);
			return success;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00027444 File Offset: 0x00025644
		public static bool TryConvertStringToUInt(string str, string initialValueAsString, out uint value)
		{
			long v;
			bool success = UINumericFieldsUtils.TryConvertStringToLong(str, initialValueAsString, out v);
			value = Mathf.ClampToUInt(v);
			return success;
		}

		// Token: 0x040006E9 RID: 1769
		public static readonly string k_AllowedCharactersForFloat = "inftynaeINFTYNAE0123456789.,-*/+%^()cosqrludxvRL=pP#";

		// Token: 0x040006EA RID: 1770
		public static readonly string k_AllowedCharactersForInt = "0123456789-*/+%^()cosintaqrtelfundxvRL,=pPI#";

		// Token: 0x040006EB RID: 1771
		public static readonly string k_DoubleFieldFormatString = "R";

		// Token: 0x040006EC RID: 1772
		public static readonly string k_FloatFieldFormatString = "g7";

		// Token: 0x040006ED RID: 1773
		public static readonly string k_IntFieldFormatString = "#######0";
	}
}
