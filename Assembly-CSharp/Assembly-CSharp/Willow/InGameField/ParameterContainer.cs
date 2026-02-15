using System;
using System.Collections.Generic;
using UnityEngine;

namespace Willow.InGameField
{
	// Token: 0x02001572 RID: 5490
	[CreateAssetMenu]
	[Serializable]
	public class ParameterContainer : ParameterAsset
	{
		// Token: 0x06009F20 RID: 40736 RVA: 0x0000216D File Offset: 0x0000036D
		private void __internalAwake()
		{
		}

		// Token: 0x06009F21 RID: 40737 RVA: 0x0019B6F5 File Offset: 0x001998F5
		public List<ParameterAsset> GetRootParameters()
		{
			return this.m_childParameterList;
		}

		// Token: 0x06009F22 RID: 40738 RVA: 0x0019B6FD File Offset: 0x001998FD
		public Dictionary<string, ParameterAsset> GetRootParametersMap()
		{
			return this.m_cachedChildParameterMap;
		}

		// Token: 0x06009F23 RID: 40739 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateRootParameterCache()
		{
		}

		// Token: 0x06009F24 RID: 40740 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateRootParameterCacheMap()
		{
		}

		// Token: 0x06009F25 RID: 40741 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddMonsterModelParameter()
		{
		}

		// Token: 0x06009F26 RID: 40742 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddMonsterLocationParameter()
		{
		}

		// Token: 0x06009F27 RID: 40743 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddMonsterLocationOffsetParameter()
		{
		}

		// Token: 0x06009F28 RID: 40744 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddMonsterRootLocationParameter()
		{
		}

		// Token: 0x06009F29 RID: 40745 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddMonsterSkillEffectParentParameter()
		{
		}

		// Token: 0x06009F2A RID: 40746 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateDummyAsset()
		{
		}

		// Token: 0x06009F2B RID: 40747 RVA: 0x0000216A File Offset: 0x0000036A
		private ParameterAsset CreateChildParameter(Type type, string assetName = "", ParameterContainer parent = null, Type supportType = null)
		{
			return null;
		}

		// Token: 0x06009F2C RID: 40748 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetVariableAssetName(string assetName, Type type)
		{
			return null;
		}

		// Token: 0x06009F2D RID: 40749 RVA: 0x0000216A File Offset: 0x0000036A
		private ParameterAsset CreateParameterAssetInstance(string parameterAssetName, Type type, ParameterContainer parent = null, Type supportType = null)
		{
			return null;
		}

		// Token: 0x06009F2E RID: 40750 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetParentContainer(ParameterAsset parameterAsset, ParameterContainer parent)
		{
		}

		// Token: 0x06009F2F RID: 40751 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddParameterAsSubAsset(ParameterAsset parameterAsset)
		{
		}

		// Token: 0x0400DE5B RID: 56923
		[SerializeField]
		private List<ParameterAsset> m_childParameterList;

		// Token: 0x0400DE5C RID: 56924
		[HideInInspector]
		[NonSerialized]
		private List<ParameterAsset> m_cacheChildParameterList;

		// Token: 0x0400DE5D RID: 56925
		[HideInInspector]
		[NonSerialized]
		private Dictionary<string, ParameterAsset> m_cachedChildParameterMap;
	}
}
