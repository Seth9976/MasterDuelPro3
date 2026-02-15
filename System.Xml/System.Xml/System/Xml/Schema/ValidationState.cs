using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x020002A4 RID: 676
	internal sealed class ValidationState
	{
		// Token: 0x04000E54 RID: 3668
		public bool IsNill;

		// Token: 0x04000E55 RID: 3669
		public bool IsDefault;

		// Token: 0x04000E56 RID: 3670
		public bool NeedValidateChildren;

		// Token: 0x04000E57 RID: 3671
		public bool CheckRequiredAttribute;

		// Token: 0x04000E58 RID: 3672
		public bool ValidationSkipped;

		// Token: 0x04000E59 RID: 3673
		public XmlSchemaContentProcessing ProcessContents;

		// Token: 0x04000E5A RID: 3674
		public XmlSchemaValidity Validity;

		// Token: 0x04000E5B RID: 3675
		public SchemaElementDecl ElementDecl;

		// Token: 0x04000E5C RID: 3676
		public SchemaElementDecl ElementDeclBeforeXsi;

		// Token: 0x04000E5D RID: 3677
		public string LocalName;

		// Token: 0x04000E5E RID: 3678
		public string Namespace;

		// Token: 0x04000E5F RID: 3679
		public ConstraintStruct[] Constr;

		// Token: 0x04000E60 RID: 3680
		public StateUnion CurrentState;

		// Token: 0x04000E61 RID: 3681
		public bool HasMatched;

		// Token: 0x04000E62 RID: 3682
		public BitSet[] CurPos = new BitSet[2];

		// Token: 0x04000E63 RID: 3683
		public BitSet AllElementsSet;

		// Token: 0x04000E64 RID: 3684
		public List<RangePositionInfo> RunningPositions;

		// Token: 0x04000E65 RID: 3685
		public bool TooComplex;
	}
}
