using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004B0 RID: 1200
	[Serializable]
	internal sealed class MemberHolder
	{
		// Token: 0x0600264C RID: 9804 RVA: 0x0009A959 File Offset: 0x00098B59
		internal MemberHolder(Type type, StreamingContext ctx)
		{
			this._memberType = type;
			this._context = ctx;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0009A96F File Offset: 0x00098B6F
		public override int GetHashCode()
		{
			return this._memberType.GetHashCode();
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x0009A97C File Offset: 0x00098B7C
		public override bool Equals(object obj)
		{
			MemberHolder memberHolder = obj as MemberHolder;
			return memberHolder != null && memberHolder._memberType == this._memberType && memberHolder._context.State == this._context.State;
		}

		// Token: 0x0400124D RID: 4685
		internal readonly Type _memberType;

		// Token: 0x0400124E RID: 4686
		internal readonly StreamingContext _context;
	}
}
