using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200015F RID: 351
	internal class ChoiceIdentifierAccessor : Accessor
	{
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00053EEE File Offset: 0x000520EE
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x00053EF6 File Offset: 0x000520F6
		internal string MemberName
		{
			get
			{
				return this.memberName;
			}
			set
			{
				this.memberName = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00053EFF File Offset: 0x000520FF
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x00053F07 File Offset: 0x00052107
		internal string[] MemberIds
		{
			get
			{
				return this.memberIds;
			}
			set
			{
				this.memberIds = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00053F10 File Offset: 0x00052110
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x00053F18 File Offset: 0x00052118
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x0400083A RID: 2106
		private string memberName;

		// Token: 0x0400083B RID: 2107
		private string[] memberIds;

		// Token: 0x0400083C RID: 2108
		private MemberInfo memberInfo;
	}
}
