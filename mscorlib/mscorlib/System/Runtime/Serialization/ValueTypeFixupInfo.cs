using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x020004B6 RID: 1206
	internal sealed class ValueTypeFixupInfo
	{
		// Token: 0x06002665 RID: 9829 RVA: 0x0009AC88 File Offset: 0x00098E88
		public ValueTypeFixupInfo(long containerID, FieldInfo member, int[] parentIndex)
		{
			if (member == null && parentIndex == null)
			{
				throw new ArgumentException("When supplying the ID of a containing object, the FieldInfo that identifies the current field within that object must also be supplied.");
			}
			if (containerID == 0L && member == null)
			{
				this._containerID = containerID;
				this._parentField = member;
				this._parentIndex = parentIndex;
			}
			if (member != null)
			{
				if (parentIndex != null)
				{
					throw new ArgumentException("Cannot supply both a MemberInfo and an Array to indicate the parent of a value type.");
				}
				if (member.FieldType.IsValueType && containerID == 0L)
				{
					throw new ArgumentException("When supplying a FieldInfo for fixing up a nested type, a valid ID for that containing object must also be supplied.");
				}
			}
			this._containerID = containerID;
			this._parentField = member;
			this._parentIndex = parentIndex;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x0009AD1A File Offset: 0x00098F1A
		public long ContainerID
		{
			get
			{
				return this._containerID;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x0009AD22 File Offset: 0x00098F22
		public FieldInfo ParentField
		{
			get
			{
				return this._parentField;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x0009AD2A File Offset: 0x00098F2A
		public int[] ParentIndex
		{
			get
			{
				return this._parentIndex;
			}
		}

		// Token: 0x04001259 RID: 4697
		private readonly long _containerID;

		// Token: 0x0400125A RID: 4698
		private readonly FieldInfo _parentField;

		// Token: 0x0400125B RID: 4699
		private readonly int[] _parentIndex;
	}
}
