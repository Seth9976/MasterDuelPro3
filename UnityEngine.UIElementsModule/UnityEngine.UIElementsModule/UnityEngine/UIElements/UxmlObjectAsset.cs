using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004AA RID: 1194
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class UxmlObjectAsset : UxmlAsset
	{
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x0007CC94 File Offset: 0x0007AE94
		public bool isField
		{
			get
			{
				return this.m_IsField;
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0007CC9C File Offset: 0x0007AE9C
		public UxmlObjectAsset(string fullTypeNameOrFieldName, bool isField, UxmlNamespaceDefinition xmlNamespace = default(UxmlNamespaceDefinition))
			: base(fullTypeNameOrFieldName, xmlNamespace)
		{
			this.m_IsField = isField;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x0007CCAF File Offset: 0x0007AEAF
		public override string ToString()
		{
			return this.isField ? string.Format("Reference: {0} (id:{1} parent:{2})", base.fullTypeName, base.id, base.parentId) : base.ToString();
		}

		// Token: 0x04000F16 RID: 3862
		[SerializeField]
		private bool m_IsField;
	}
}
