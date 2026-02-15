using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000149 RID: 329
	[Serializable]
	internal sealed class OrdinalIgnoreCaseComparer : OrdinalComparer, ISerializable
	{
		// Token: 0x06000B2F RID: 2863 RVA: 0x00032216 File Offset: 0x00030416
		public OrdinalIgnoreCaseComparer()
			: base(true)
		{
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0003221F File Offset: 0x0003041F
		public override int Compare(string x, string y)
		{
			return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00032229 File Offset: 0x00030429
		public override bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00032233 File Offset: 0x00030433
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			return CompareInfo.GetIgnoreCaseHash(obj);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00032244 File Offset: 0x00030444
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(OrdinalComparer));
			info.AddValue("_ignoreCase", true);
		}
	}
}
