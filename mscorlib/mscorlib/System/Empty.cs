using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200019E RID: 414
	[Serializable]
	internal sealed class Empty : ISerializable
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x00003CE1 File Offset: 0x00001EE1
		private Empty()
		{
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0001C227 File Offset: 0x0001A427
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003FDBB File Offset: 0x0003DFBB
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 1, null, null);
		}

		// Token: 0x040005E9 RID: 1513
		public static readonly Empty Value = new Empty();
	}
}
