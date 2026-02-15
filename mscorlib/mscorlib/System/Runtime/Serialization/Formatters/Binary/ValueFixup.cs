using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200050A RID: 1290
	internal sealed class ValueFixup
	{
		// Token: 0x060028C9 RID: 10441 RVA: 0x000A74A1 File Offset: 0x000A56A1
		internal ValueFixup(Array arrayObj, int[] indexMap)
		{
			this.valueFixupEnum = ValueFixupEnum.Array;
			this.arrayObj = arrayObj;
			this.indexMap = indexMap;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000A74BE File Offset: 0x000A56BE
		internal ValueFixup(object memberObject, string memberName, ReadObjectInfo objectInfo)
		{
			this.valueFixupEnum = ValueFixupEnum.Member;
			this.memberObject = memberObject;
			this.memberName = memberName;
			this.objectInfo = objectInfo;
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000A74E4 File Offset: 0x000A56E4
		internal void Fixup(ParseRecord record, ParseRecord parent)
		{
			object prnewObj = record.PRnewObj;
			switch (this.valueFixupEnum)
			{
			case ValueFixupEnum.Array:
				this.arrayObj.SetValue(prnewObj, this.indexMap);
				return;
			case ValueFixupEnum.Header:
			{
				Type typeFromHandle = typeof(Header);
				if (ValueFixup.valueInfo == null)
				{
					MemberInfo[] member = typeFromHandle.GetMember("Value");
					if (member.Length != 1)
					{
						throw new SerializationException(Environment.GetResourceString("Header reflection error: number of value members: {0}.", new object[] { member.Length }));
					}
					ValueFixup.valueInfo = member[0];
				}
				FormatterServices.SerializationSetValue(ValueFixup.valueInfo, this.header, prnewObj);
				return;
			}
			case ValueFixupEnum.Member:
			{
				if (this.objectInfo.isSi)
				{
					this.objectInfo.objectManager.RecordDelayedFixup(parent.PRobjectId, this.memberName, record.PRobjectId);
					return;
				}
				MemberInfo memberInfo = this.objectInfo.GetMemberInfo(this.memberName);
				if (memberInfo != null)
				{
					this.objectInfo.objectManager.RecordFixup(parent.PRobjectId, memberInfo, record.PRobjectId);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0400149D RID: 5277
		internal ValueFixupEnum valueFixupEnum;

		// Token: 0x0400149E RID: 5278
		internal Array arrayObj;

		// Token: 0x0400149F RID: 5279
		internal int[] indexMap;

		// Token: 0x040014A0 RID: 5280
		internal object header;

		// Token: 0x040014A1 RID: 5281
		internal object memberObject;

		// Token: 0x040014A2 RID: 5282
		internal static volatile MemberInfo valueInfo;

		// Token: 0x040014A3 RID: 5283
		internal ReadObjectInfo objectInfo;

		// Token: 0x040014A4 RID: 5284
		internal string memberName;
	}
}
