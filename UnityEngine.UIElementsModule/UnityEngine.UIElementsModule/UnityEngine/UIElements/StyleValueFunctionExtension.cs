using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000435 RID: 1077
	internal static class StyleValueFunctionExtension
	{
		// Token: 0x06001F38 RID: 7992 RVA: 0x00071900 File Offset: 0x0006FB00
		public static string ToUssString(this StyleValueFunction svf)
		{
			string text;
			switch (svf)
			{
			case StyleValueFunction.Var:
				text = "var";
				break;
			case StyleValueFunction.Env:
				text = "env";
				break;
			case StyleValueFunction.LinearGradient:
				text = "linear-gradient";
				break;
			default:
				throw new ArgumentOutOfRangeException("svf", svf, "Unknown StyleValueFunction");
			}
			return text;
		}
	}
}
