using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200047B RID: 1147
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class TemplateAsset : VisualElementAsset
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x0007B568 File Offset: 0x00079768
		public List<TemplateAsset.AttributeOverride> attributeOverrides
		{
			get
			{
				return this.m_AttributeOverrides;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x0007B570 File Offset: 0x00079770
		internal List<VisualTreeAsset.SlotUsageEntry> slotUsages
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_SlotUsages;
			}
		}

		// Token: 0x04000ED7 RID: 3799
		[SerializeField]
		private string m_TemplateAlias;

		// Token: 0x04000ED8 RID: 3800
		[SerializeField]
		private List<TemplateAsset.AttributeOverride> m_AttributeOverrides;

		// Token: 0x04000ED9 RID: 3801
		[SerializeField]
		private List<TemplateAsset.UxmlSerializedDataOverride> m_SerializedDataOverride;

		// Token: 0x04000EDA RID: 3802
		[SerializeField]
		private List<VisualTreeAsset.SlotUsageEntry> m_SlotUsages;

		// Token: 0x0200047C RID: 1148
		[Serializable]
		public struct AttributeOverride
		{
			// Token: 0x06002198 RID: 8600 RVA: 0x0007B588 File Offset: 0x00079788
			public bool NamesPathMatchesElementNamesPath(IList<string> elementNamesPath)
			{
				bool flag = elementNamesPath == null || this.m_NamesPath == null || elementNamesPath.Count == 0 || this.m_NamesPath.Length == 0;
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					bool flag3 = this.m_NamesPath.Length == 1;
					if (flag3)
					{
						flag2 = this.m_NamesPath[0] == elementNamesPath[elementNamesPath.Count - 1];
					}
					else
					{
						bool flag4 = this.m_NamesPath.Length != elementNamesPath.Count;
						if (flag4)
						{
							flag2 = false;
						}
						else
						{
							for (int i = elementNamesPath.Count - 1; i >= 0; i--)
							{
								bool flag5 = elementNamesPath[i] != this.m_NamesPath[i];
								if (flag5)
								{
									return false;
								}
							}
							flag2 = true;
						}
					}
				}
				return flag2;
			}

			// Token: 0x04000EDB RID: 3803
			public string m_ElementName;

			// Token: 0x04000EDC RID: 3804
			public string[] m_NamesPath;

			// Token: 0x04000EDD RID: 3805
			public string m_AttributeName;

			// Token: 0x04000EDE RID: 3806
			public string m_Value;
		}

		// Token: 0x0200047D RID: 1149
		[Serializable]
		public struct UxmlSerializedDataOverride
		{
			// Token: 0x04000EDF RID: 3807
			public int m_ElementId;

			// Token: 0x04000EE0 RID: 3808
			public List<int> m_ElementIdsPath;

			// Token: 0x04000EE1 RID: 3809
			[SerializeReference]
			public UxmlSerializedData m_SerializedData;
		}
	}
}
