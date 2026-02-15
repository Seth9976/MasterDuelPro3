using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000438 RID: 1080
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class StyleValueKeywordExtension
	{
		// Token: 0x06001F3B RID: 7995 RVA: 0x0007197C File Offset: 0x0006FB7C
		public static string ToUssString(this StyleValueKeyword svk)
		{
			string text;
			switch (svk)
			{
			case StyleValueKeyword.Inherit:
				text = "inherit";
				break;
			case StyleValueKeyword.Initial:
				text = "initial";
				break;
			case StyleValueKeyword.Auto:
				text = "auto";
				break;
			case StyleValueKeyword.Unset:
				text = "unset";
				break;
			case StyleValueKeyword.True:
				text = "true";
				break;
			case StyleValueKeyword.False:
				text = "false";
				break;
			case StyleValueKeyword.None:
				text = "none";
				break;
			case StyleValueKeyword.Cover:
				text = "cover";
				break;
			case StyleValueKeyword.Contain:
				text = "contain";
				break;
			default:
				throw new ArgumentOutOfRangeException("svk", svk, "Unknown StyleValueKeyword");
			}
			return text;
		}
	}
}
