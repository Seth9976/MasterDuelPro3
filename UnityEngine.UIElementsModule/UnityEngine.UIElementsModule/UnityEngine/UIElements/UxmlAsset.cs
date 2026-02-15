using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A9 RID: 1193
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class UxmlAsset : IUxmlAttributes
	{
		// Token: 0x0600221B RID: 8731 RVA: 0x0007CA9C File Offset: 0x0007AC9C
		public UxmlAsset(string fullTypeName, UxmlNamespaceDefinition xmlNamespace = default(UxmlNamespaceDefinition))
		{
			this.m_FullTypeName = fullTypeName;
			this.m_XmlNamespace = xmlNamespace;
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x0007CAB4 File Offset: 0x0007ACB4
		public string fullTypeName
		{
			get
			{
				return this.m_FullTypeName;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x0007CABC File Offset: 0x0007ACBC
		// (set) Token: 0x0600221E RID: 8734 RVA: 0x0007CAC4 File Offset: 0x0007ACC4
		public int id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x0007CACD File Offset: 0x0007ACCD
		public int orderInDocument
		{
			get
			{
				return this.m_OrderInDocument;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x0007CAD5 File Offset: 0x0007ACD5
		// (set) Token: 0x06002221 RID: 8737 RVA: 0x0007CADD File Offset: 0x0007ACDD
		public int parentId
		{
			get
			{
				return this.m_ParentId;
			}
			set
			{
				this.m_ParentId = value;
			}
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0007CAE8 File Offset: 0x0007ACE8
		public bool TryGetAttributeValue(string propertyName, out string value)
		{
			bool flag = this.m_Properties == null;
			bool flag2;
			if (flag)
			{
				value = null;
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.m_Properties.Count - 1; i += 2)
				{
					bool flag3 = this.m_Properties[i] == propertyName;
					if (flag3)
					{
						value = this.m_Properties[i + 1];
						return true;
					}
				}
				value = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0007CB5E File Offset: 0x0007AD5E
		public void SetAttribute(string name, string value)
		{
			this.SetOrAddProperty(name, value);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0007CB6C File Offset: 0x0007AD6C
		public void RemoveAttribute(string attributeName)
		{
			bool flag = this.m_Properties == null || this.m_Properties.Count <= 0;
			if (!flag)
			{
				for (int i = 0; i < this.m_Properties.Count; i += 2)
				{
					string name = this.m_Properties[i];
					bool flag2 = name != attributeName;
					if (!flag2)
					{
						this.m_Properties.RemoveAt(i);
						this.m_Properties.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0007CBF0 File Offset: 0x0007ADF0
		private void SetOrAddProperty(string propertyName, string propertyValue)
		{
			bool flag = this.m_Properties == null;
			if (flag)
			{
				this.m_Properties = new List<string>();
			}
			for (int i = 0; i < this.m_Properties.Count - 1; i += 2)
			{
				bool flag2 = this.m_Properties[i] == propertyName;
				if (flag2)
				{
					this.m_Properties[i + 1] = propertyValue;
					return;
				}
			}
			this.m_Properties.Add(propertyName);
			this.m_Properties.Add(propertyValue);
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0007CC77 File Offset: 0x0007AE77
		public override string ToString()
		{
			return string.Format("{0}(id:{1})", this.fullTypeName, this.id);
		}

		// Token: 0x04000F0E RID: 3854
		public const string NullNodeType = "null";

		// Token: 0x04000F0F RID: 3855
		[SerializeField]
		private string m_FullTypeName;

		// Token: 0x04000F10 RID: 3856
		[SerializeField]
		private UxmlNamespaceDefinition m_XmlNamespace;

		// Token: 0x04000F11 RID: 3857
		[SerializeField]
		private int m_Id;

		// Token: 0x04000F12 RID: 3858
		[SerializeField]
		private int m_OrderInDocument;

		// Token: 0x04000F13 RID: 3859
		[SerializeField]
		private int m_ParentId;

		// Token: 0x04000F14 RID: 3860
		[SerializeField]
		private List<UxmlNamespaceDefinition> m_NamespaceDefinitions;

		// Token: 0x04000F15 RID: 3861
		[SerializeField]
		protected List<string> m_Properties;
	}
}
