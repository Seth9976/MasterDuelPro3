using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004B1 RID: 1201
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class VisualElementAsset : UxmlAsset, ISerializationCallbackReceiver
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x0007D070 File Offset: 0x0007B270
		public int ruleIndex
		{
			get
			{
				return this.m_RuleIndex;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x0007D088 File Offset: 0x0007B288
		public string[] classes
		{
			get
			{
				return this.m_Classes;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x0007D0A0 File Offset: 0x0007B2A0
		public List<string> stylesheetPaths
		{
			get
			{
				List<string> list;
				if ((list = this.m_StylesheetPaths) == null)
				{
					list = (this.m_StylesheetPaths = new List<string>());
				}
				return list;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0007D0CA File Offset: 0x0007B2CA
		public bool hasStylesheetPaths
		{
			get
			{
				return this.m_StylesheetPaths != null;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x0007D0D8 File Offset: 0x0007B2D8
		public List<StyleSheet> stylesheets
		{
			get
			{
				List<StyleSheet> list;
				if ((list = this.m_Stylesheets) == null)
				{
					list = (this.m_Stylesheets = new List<StyleSheet>());
				}
				return list;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x0007D0FD File Offset: 0x0007B2FD
		public bool hasStylesheets
		{
			get
			{
				return this.m_Stylesheets != null;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x0007D108 File Offset: 0x0007B308
		public UxmlSerializedData serializedData
		{
			get
			{
				return this.m_SerializedData;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x0007D110 File Offset: 0x0007B310
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool skipClone
		{
			get
			{
				return this.m_SkipClone;
			}
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000020EA File Offset: 0x000002EA
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0007D118 File Offset: 0x0007B318
		public void OnAfterDeserialize()
		{
			bool flag = !string.IsNullOrEmpty(this.m_Name) && !this.m_Properties.Contains("name");
			if (flag)
			{
				base.SetAttribute("name", this.m_Name);
			}
			bool flag2 = !string.IsNullOrEmpty(this.m_Text) && !this.m_Properties.Contains("text");
			if (flag2)
			{
				base.SetAttribute("text", this.m_Text);
			}
			bool flag3 = this.m_PickingMode != PickingMode.Position && !this.m_Properties.Contains("picking-mode") && !this.m_Properties.Contains("pickingMode");
			if (flag3)
			{
				base.SetAttribute("picking-mode", this.m_PickingMode.ToString());
			}
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x0007D1F0 File Offset: 0x0007B3F0
		private static bool IdsPathMatchesAttributeOverrideIdsPath(List<int> idsPath, List<int> attributeOverrideIdsPath, int templateId)
		{
			bool flag = idsPath == null || attributeOverrideIdsPath == null || idsPath.Count == 0 || attributeOverrideIdsPath.Count == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int templateIdIndex = idsPath.IndexOf(templateId);
				bool flag3 = idsPath.Count != attributeOverrideIdsPath.Count + templateIdIndex + 1;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					for (int i = idsPath.Count - 1; i > templateIdIndex; i--)
					{
						bool flag4 = idsPath[i] != attributeOverrideIdsPath[i - templateIdIndex - 1];
						if (flag4)
						{
							return false;
						}
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0007D290 File Offset: 0x0007B490
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal virtual VisualElement Instantiate(CreationContext cc)
		{
			VisualElement ve = (VisualElement)this.serializedData.CreateInstance();
			this.serializedData.Deserialize(ve);
			bool hasOverrides = cc.hasOverrides;
			if (hasOverrides)
			{
				cc.veaIdsPath.Add(base.id);
				for (int i = cc.serializedDataOverrides.Count - 1; i >= 0; i--)
				{
					foreach (TemplateAsset.UxmlSerializedDataOverride attributeOverride in cc.serializedDataOverrides[i].attributeOverrides)
					{
						bool flag = attributeOverride.m_ElementId == base.id && VisualElementAsset.IdsPathMatchesAttributeOverrideIdsPath(cc.veaIdsPath, attributeOverride.m_ElementIdsPath, cc.serializedDataOverrides[i].templateId);
						if (flag)
						{
							attributeOverride.m_SerializedData.Deserialize(ve);
						}
					}
				}
				cc.veaIdsPath.Remove(base.id);
			}
			bool hasStylesheetPaths = this.hasStylesheetPaths;
			if (hasStylesheetPaths)
			{
				for (int j = 0; j < this.stylesheetPaths.Count; j++)
				{
					ve.AddStyleSheetPath(this.stylesheetPaths[j]);
				}
			}
			bool hasStylesheets = this.hasStylesheets;
			if (hasStylesheets)
			{
				for (int k = 0; k < this.stylesheets.Count; k++)
				{
					bool flag2 = this.stylesheets[k] != null;
					if (flag2)
					{
						ve.styleSheets.Add(this.stylesheets[k]);
					}
				}
			}
			bool flag3 = this.classes != null;
			if (flag3)
			{
				for (int l = 0; l < this.classes.Length; l++)
				{
					ve.AddToClassList(this.classes[l]);
				}
			}
			return ve;
		}

		// Token: 0x04000F21 RID: 3873
		[SerializeField]
		private string m_Name;

		// Token: 0x04000F22 RID: 3874
		[SerializeField]
		private int m_RuleIndex;

		// Token: 0x04000F23 RID: 3875
		[SerializeField]
		private string m_Text;

		// Token: 0x04000F24 RID: 3876
		[SerializeField]
		private PickingMode m_PickingMode;

		// Token: 0x04000F25 RID: 3877
		[SerializeField]
		private string[] m_Classes;

		// Token: 0x04000F26 RID: 3878
		[SerializeField]
		private List<string> m_StylesheetPaths;

		// Token: 0x04000F27 RID: 3879
		[SerializeField]
		private List<StyleSheet> m_Stylesheets;

		// Token: 0x04000F28 RID: 3880
		[SerializeReference]
		internal UxmlSerializedData m_SerializedData;

		// Token: 0x04000F29 RID: 3881
		[SerializeField]
		private bool m_SkipClone;
	}
}
