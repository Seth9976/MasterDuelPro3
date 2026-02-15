using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200016F RID: 367
	internal class MembersMapping : TypeMapping
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00054DEE File Offset: 0x00052FEE
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x00054DF6 File Offset: 0x00052FF6
		internal MemberMapping[] Members
		{
			get
			{
				return this.members;
			}
			set
			{
				this.members = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00054DFF File Offset: 0x00052FFF
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x00054E07 File Offset: 0x00053007
		internal MemberMapping XmlnsMember
		{
			get
			{
				return this.xmlnsMember;
			}
			set
			{
				this.xmlnsMember = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00054E10 File Offset: 0x00053010
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x00054E18 File Offset: 0x00053018
		internal bool HasWrapperElement
		{
			get
			{
				return this.hasWrapperElement;
			}
			set
			{
				this.hasWrapperElement = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x00054E21 File Offset: 0x00053021
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x00054E29 File Offset: 0x00053029
		internal bool ValidateRpcWrapperElement
		{
			get
			{
				return this.validateRpcWrapperElement;
			}
			set
			{
				this.validateRpcWrapperElement = value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00054E32 File Offset: 0x00053032
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x00054E3A File Offset: 0x0005303A
		internal bool WriteAccessors
		{
			get
			{
				return this.writeAccessors;
			}
			set
			{
				this.writeAccessors = value;
			}
		}

		// Token: 0x0400086E RID: 2158
		private MemberMapping[] members;

		// Token: 0x0400086F RID: 2159
		private bool hasWrapperElement = true;

		// Token: 0x04000870 RID: 2160
		private bool validateRpcWrapperElement;

		// Token: 0x04000871 RID: 2161
		private bool writeAccessors = true;

		// Token: 0x04000872 RID: 2162
		private MemberMapping xmlnsMember;
	}
}
