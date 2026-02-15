using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000071 RID: 113
	internal class ShadowCasterGroup2DManager
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000157CD File Offset: 0x000139CD
		public static List<ShadowCasterGroup2D> shadowCasterGroups
		{
			get
			{
				return ShadowCasterGroup2DManager.s_ShadowCasterGroups;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000157D4 File Offset: 0x000139D4
		public static void CacheValues()
		{
			if (ShadowCasterGroup2DManager.shadowCasterGroups != null)
			{
				for (int i = 0; i < ShadowCasterGroup2DManager.shadowCasterGroups.Count; i++)
				{
					if (ShadowCasterGroup2DManager.shadowCasterGroups[i] != null)
					{
						ShadowCasterGroup2DManager.shadowCasterGroups[i].CacheValues();
					}
				}
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00015820 File Offset: 0x00013A20
		public static void AddShadowCasterGroupToList(ShadowCasterGroup2D shadowCaster, List<ShadowCasterGroup2D> list)
		{
			if (list.Contains(shadowCaster))
			{
				return;
			}
			int positionToInsert = 0;
			while (positionToInsert < list.Count && shadowCaster.m_Priority >= list[positionToInsert].m_Priority)
			{
				positionToInsert++;
			}
			list.Insert(positionToInsert, shadowCaster);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00015866 File Offset: 0x00013A66
		public static void RemoveShadowCasterGroupFromList(ShadowCasterGroup2D shadowCaster, List<ShadowCasterGroup2D> list)
		{
			list.Remove(shadowCaster);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00015870 File Offset: 0x00013A70
		private static CompositeShadowCaster2D FindTopMostCompositeShadowCaster(ShadowCaster2D shadowCaster)
		{
			CompositeShadowCaster2D retGroup = null;
			Transform transformToCheck = shadowCaster.transform.parent;
			while (transformToCheck != null)
			{
				CompositeShadowCaster2D currentGroup;
				if (transformToCheck.TryGetComponent<CompositeShadowCaster2D>(out currentGroup))
				{
					retGroup = currentGroup;
				}
				transformToCheck = transformToCheck.parent;
			}
			return retGroup;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000158AC File Offset: 0x00013AAC
		public static int GetRendereringPriority(ShadowCaster2D shadowCaster)
		{
			int sortingOrder = 0;
			Renderer renderer;
			if (shadowCaster.TryGetComponent<Renderer>(out renderer))
			{
				sortingOrder = renderer.sortingOrder;
			}
			return sortingOrder;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000158D0 File Offset: 0x00013AD0
		public static bool AddToShadowCasterGroup(ShadowCaster2D shadowCaster, ref ShadowCasterGroup2D shadowCasterGroup, ref int priority)
		{
			ShadowCasterGroup2D newShadowCasterGroup = ShadowCasterGroup2DManager.FindTopMostCompositeShadowCaster(shadowCaster);
			int newPriority = 0;
			if (newShadowCasterGroup == null)
			{
				newPriority = ShadowCasterGroup2DManager.GetRendereringPriority(shadowCaster);
				shadowCaster.TryGetComponent<ShadowCasterGroup2D>(out newShadowCasterGroup);
			}
			if (newShadowCasterGroup != null && (shadowCasterGroup != newShadowCasterGroup || priority != newPriority))
			{
				newShadowCasterGroup.RegisterShadowCaster2D(shadowCaster);
				shadowCasterGroup = newShadowCasterGroup;
				priority = newPriority;
				return true;
			}
			return false;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00015927 File Offset: 0x00013B27
		public static void RemoveFromShadowCasterGroup(ShadowCaster2D shadowCaster, ShadowCasterGroup2D shadowCasterGroup)
		{
			if (shadowCasterGroup != null)
			{
				shadowCasterGroup.UnregisterShadowCaster2D(shadowCaster);
			}
			if (shadowCasterGroup == shadowCaster)
			{
				ShadowCasterGroup2DManager.RemoveGroup(shadowCasterGroup);
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00015948 File Offset: 0x00013B48
		public static void AddGroup(ShadowCasterGroup2D group)
		{
			if (group == null)
			{
				return;
			}
			if (ShadowCasterGroup2DManager.s_ShadowCasterGroups == null)
			{
				ShadowCasterGroup2DManager.s_ShadowCasterGroups = new List<ShadowCasterGroup2D>();
			}
			ShadowCasterGroup2DManager.AddShadowCasterGroupToList(group, ShadowCasterGroup2DManager.s_ShadowCasterGroups);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00015970 File Offset: 0x00013B70
		public static void RemoveGroup(ShadowCasterGroup2D group)
		{
			if (group != null && ShadowCasterGroup2DManager.s_ShadowCasterGroups != null)
			{
				ShadowCasterGroup2DManager.RemoveShadowCasterGroupFromList(group, ShadowCasterGroup2DManager.s_ShadowCasterGroups);
			}
		}

		// Token: 0x04000290 RID: 656
		private static List<ShadowCasterGroup2D> s_ShadowCasterGroups;
	}
}
