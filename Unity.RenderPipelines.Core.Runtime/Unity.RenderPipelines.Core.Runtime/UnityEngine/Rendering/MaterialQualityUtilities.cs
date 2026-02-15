using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020001DB RID: 475
	[MovedFrom("Utilities")]
	public static class MaterialQualityUtilities
	{
		// Token: 0x06000D8A RID: 3466 RVA: 0x00031D98 File Offset: 0x0002FF98
		public static MaterialQuality GetHighestQuality(this MaterialQuality levels)
		{
			for (int i = MaterialQualityUtilities.Keywords.Length - 1; i >= 0; i--)
			{
				MaterialQuality level = (MaterialQuality)(1 << i);
				if ((levels & level) != (MaterialQuality)0)
				{
					return level;
				}
			}
			return (MaterialQuality)0;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00031DC8 File Offset: 0x0002FFC8
		public static MaterialQuality GetClosestQuality(this MaterialQuality availableLevels, MaterialQuality requestedLevel)
		{
			if (availableLevels == (MaterialQuality)0)
			{
				return MaterialQuality.Low;
			}
			int requestedLevelIndex = requestedLevel.ToFirstIndex();
			MaterialQuality chosenQuality = (MaterialQuality)0;
			for (int i = requestedLevelIndex; i >= 0; i--)
			{
				MaterialQuality level = MaterialQualityUtilities.FromIndex(i);
				if ((level & availableLevels) != (MaterialQuality)0)
				{
					chosenQuality = level;
					break;
				}
			}
			if (chosenQuality != (MaterialQuality)0)
			{
				return chosenQuality;
			}
			for (int j = requestedLevelIndex + 1; j < MaterialQualityUtilities.Keywords.Length; j++)
			{
				MaterialQuality level2 = MaterialQualityUtilities.FromIndex(j);
				Math.Abs(requestedLevel - level2);
				if ((level2 & availableLevels) != (MaterialQuality)0)
				{
					chosenQuality = level2;
					break;
				}
			}
			return chosenQuality;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00031E3C File Offset: 0x0003003C
		public static void SetGlobalShaderKeywords(this MaterialQuality level)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					Shader.EnableKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
				else
				{
					Shader.DisableKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00031E80 File Offset: 0x00030080
		public static void SetGlobalShaderKeywords(this MaterialQuality level, CommandBuffer cmd)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					cmd.EnableShaderKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
				else
				{
					cmd.DisableShaderKeyword(MaterialQualityUtilities.KeywordNames[i]);
				}
			}
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00031EC8 File Offset: 0x000300C8
		public static int ToFirstIndex(this MaterialQuality level)
		{
			for (int i = 0; i < MaterialQualityUtilities.KeywordNames.Length; i++)
			{
				if ((level & (MaterialQuality)(1 << i)) != (MaterialQuality)0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00031EF4 File Offset: 0x000300F4
		public static MaterialQuality FromIndex(int index)
		{
			return (MaterialQuality)(1 << index);
		}

		// Token: 0x04000912 RID: 2322
		public static string[] KeywordNames = new string[] { "MATERIAL_QUALITY_LOW", "MATERIAL_QUALITY_MEDIUM", "MATERIAL_QUALITY_HIGH" };

		// Token: 0x04000913 RID: 2323
		public static string[] EnumNames = Enum.GetNames(typeof(MaterialQuality));

		// Token: 0x04000914 RID: 2324
		public static ShaderKeyword[] Keywords = new ShaderKeyword[]
		{
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[0]),
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[1]),
			new ShaderKeyword(MaterialQualityUtilities.KeywordNames[2])
		};
	}
}
