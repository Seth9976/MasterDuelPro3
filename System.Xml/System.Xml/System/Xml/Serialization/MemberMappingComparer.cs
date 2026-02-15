using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200016D RID: 365
	internal class MemberMappingComparer : IComparer
	{
		// Token: 0x06001194 RID: 4500 RVA: 0x00054B78 File Offset: 0x00052D78
		public int Compare(object o1, object o2)
		{
			MemberMapping memberMapping = (MemberMapping)o1;
			MemberMapping memberMapping2 = (MemberMapping)o2;
			if (memberMapping.IsText)
			{
				if (memberMapping2.IsText)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (memberMapping2.IsText)
				{
					return -1;
				}
				if (memberMapping.SequenceId < 0 && memberMapping2.SequenceId < 0)
				{
					return 0;
				}
				if (memberMapping.SequenceId < 0)
				{
					return 1;
				}
				if (memberMapping2.SequenceId < 0)
				{
					return -1;
				}
				if (memberMapping.SequenceId < memberMapping2.SequenceId)
				{
					return -1;
				}
				if (memberMapping.SequenceId > memberMapping2.SequenceId)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
