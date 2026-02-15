using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A8 RID: 1192
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal struct UxmlNamespaceDefinition : IEquatable<UxmlNamespaceDefinition>
	{
		// Token: 0x06002216 RID: 8726 RVA: 0x0007C9E0 File Offset: 0x0007ABE0
		public static bool operator ==(UxmlNamespaceDefinition lhs, UxmlNamespaceDefinition rhs)
		{
			return string.Compare(lhs.prefix, rhs.prefix, StringComparison.Ordinal) == 0 && string.Compare(lhs.resolvedNamespace, rhs.resolvedNamespace, StringComparison.Ordinal) == 0;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0007CA20 File Offset: 0x0007AC20
		public bool Equals(UxmlNamespaceDefinition other)
		{
			return this == other;
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0007CA40 File Offset: 0x0007AC40
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is UxmlNamespaceDefinition)
			{
				UxmlNamespaceDefinition other = (UxmlNamespaceDefinition)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0007CA6C File Offset: 0x0007AC6C
		public override int GetHashCode()
		{
			return HashCode.Combine<string, string>(this.prefix, this.resolvedNamespace);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0007CA8F File Offset: 0x0007AC8F
		// Note: this type is marked as 'beforefieldinit'.
		static UxmlNamespaceDefinition()
		{
			UxmlNamespaceDefinition.<Empty>k__BackingField = default(UxmlNamespaceDefinition);
		}

		// Token: 0x04000F0C RID: 3852
		public string prefix;

		// Token: 0x04000F0D RID: 3853
		public string resolvedNamespace;
	}
}
