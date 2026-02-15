using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000148 RID: 328
	[Serializable]
	internal sealed class OrdinalCaseSensitiveComparer : OrdinalComparer, ISerializable
	{
		// Token: 0x06000B2A RID: 2858 RVA: 0x000321CC File Offset: 0x000303CC
		public OrdinalCaseSensitiveComparer()
			: base(false)
		{
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000321D5 File Offset: 0x000303D5
		public override int Compare(string x, string y)
		{
			return string.CompareOrdinal(x, y);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000321DE File Offset: 0x000303DE
		public override bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000321E7 File Offset: 0x000303E7
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000321F8 File Offset: 0x000303F8
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(OrdinalComparer));
			info.AddValue("_ignoreCase", false);
		}
	}
}
