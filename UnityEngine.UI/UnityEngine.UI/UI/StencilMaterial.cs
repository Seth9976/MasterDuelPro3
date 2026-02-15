using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x02000072 RID: 114
	public static class StencilMaterial
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0001513D File Offset: 0x0001333D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Material.Add instead.", true)]
		public static Material Add(Material baseMat, int stencilID)
		{
			return null;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00015140 File Offset: 0x00013340
		public static Material Add(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask)
		{
			return StencilMaterial.Add(baseMat, stencilID, operation, compareFunction, colorWriteMask, 255, 255);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00015157 File Offset: 0x00013357
		private static void LogWarningWhenNotInBatchmode(string warning, Object context)
		{
			if (!Application.isBatchMode)
			{
				Debug.LogWarning(warning, context);
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00015168 File Offset: 0x00013368
		public static Material Add(Material baseMat, int stencilID, StencilOp operation, CompareFunction compareFunction, ColorWriteMask colorWriteMask, int readMask, int writeMask)
		{
			if ((stencilID <= 0 && colorWriteMask == ColorWriteMask.All) || baseMat == null)
			{
				return baseMat;
			}
			if (!baseMat.HasProperty("_Stencil"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _Stencil property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilOp"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilOp property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilComp"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilComp property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilReadMask"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilReadMask property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilWriteMask"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilWriteMask property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_ColorMask"))
			{
				StencilMaterial.LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _ColorMask property", baseMat);
				return baseMat;
			}
			int listCount = StencilMaterial.m_List.Count;
			for (int i = 0; i < listCount; i++)
			{
				StencilMaterial.MatEntry ent = StencilMaterial.m_List[i];
				if (ent.baseMat == baseMat && ent.stencilId == stencilID && ent.operation == operation && ent.compareFunction == compareFunction && ent.readMask == readMask && ent.writeMask == writeMask && ent.colorMask == colorWriteMask)
				{
					ent.count++;
					return ent.customMat;
				}
			}
			StencilMaterial.MatEntry newEnt = new StencilMaterial.MatEntry();
			newEnt.count = 1;
			newEnt.baseMat = baseMat;
			newEnt.customMat = new Material(baseMat);
			newEnt.customMat.hideFlags = HideFlags.HideAndDontSave;
			newEnt.stencilId = stencilID;
			newEnt.operation = operation;
			newEnt.compareFunction = compareFunction;
			newEnt.readMask = readMask;
			newEnt.writeMask = writeMask;
			newEnt.colorMask = colorWriteMask;
			newEnt.useAlphaClip = operation != StencilOp.Keep && writeMask > 0;
			newEnt.customMat.name = string.Format("Stencil Id:{0}, Op:{1}, Comp:{2}, WriteMask:{3}, ReadMask:{4}, ColorMask:{5} AlphaClip:{6} ({7})", new object[] { stencilID, operation, compareFunction, writeMask, readMask, colorWriteMask, newEnt.useAlphaClip, baseMat.name });
			newEnt.customMat.SetFloat("_Stencil", (float)stencilID);
			newEnt.customMat.SetFloat("_StencilOp", (float)operation);
			newEnt.customMat.SetFloat("_StencilComp", (float)compareFunction);
			newEnt.customMat.SetFloat("_StencilReadMask", (float)readMask);
			newEnt.customMat.SetFloat("_StencilWriteMask", (float)writeMask);
			newEnt.customMat.SetFloat("_ColorMask", (float)colorWriteMask);
			newEnt.customMat.SetFloat("_UseUIAlphaClip", newEnt.useAlphaClip ? 1f : 0f);
			if (newEnt.useAlphaClip)
			{
				newEnt.customMat.EnableKeyword("UNITY_UI_ALPHACLIP");
			}
			else
			{
				newEnt.customMat.DisableKeyword("UNITY_UI_ALPHACLIP");
			}
			StencilMaterial.m_List.Add(newEnt);
			return newEnt.customMat;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000154AC File Offset: 0x000136AC
		public static void Remove(Material customMat)
		{
			if (customMat == null)
			{
				return;
			}
			int listCount = StencilMaterial.m_List.Count;
			for (int i = 0; i < listCount; i++)
			{
				StencilMaterial.MatEntry ent = StencilMaterial.m_List[i];
				if (!(ent.customMat != customMat))
				{
					StencilMaterial.MatEntry matEntry = ent;
					int num = matEntry.count - 1;
					matEntry.count = num;
					if (num == 0)
					{
						Misc.DestroyImmediate(ent.customMat);
						ent.baseMat = null;
						StencilMaterial.m_List.RemoveAt(i);
					}
					return;
				}
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00015528 File Offset: 0x00013728
		public static void ClearAll()
		{
			int listCount = StencilMaterial.m_List.Count;
			for (int i = 0; i < listCount; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				Misc.DestroyImmediate(matEntry.customMat);
				matEntry.baseMat = null;
			}
			StencilMaterial.m_List.Clear();
		}

		// Token: 0x0400023C RID: 572
		private static List<StencilMaterial.MatEntry> m_List = new List<StencilMaterial.MatEntry>();

		// Token: 0x02000073 RID: 115
		private class MatEntry
		{
			// Token: 0x0400023D RID: 573
			public Material baseMat;

			// Token: 0x0400023E RID: 574
			public Material customMat;

			// Token: 0x0400023F RID: 575
			public int count;

			// Token: 0x04000240 RID: 576
			public int stencilId;

			// Token: 0x04000241 RID: 577
			public StencilOp operation;

			// Token: 0x04000242 RID: 578
			public CompareFunction compareFunction = CompareFunction.Always;

			// Token: 0x04000243 RID: 579
			public int readMask;

			// Token: 0x04000244 RID: 580
			public int writeMask;

			// Token: 0x04000245 RID: 581
			public bool useAlphaClip;

			// Token: 0x04000246 RID: 582
			public ColorWriteMask colorMask;
		}
	}
}
