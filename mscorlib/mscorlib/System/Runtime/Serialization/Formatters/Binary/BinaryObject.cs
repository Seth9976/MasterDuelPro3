using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004E5 RID: 1253
	internal sealed class BinaryObject
	{
		// Token: 0x06002752 RID: 10066 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal BinaryObject()
		{
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0009E3EC File Offset: 0x0009C5EC
		internal void Set(int objectId, int mapId)
		{
			this.objectId = objectId;
			this.mapId = mapId;
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x0009E3FC File Offset: 0x0009C5FC
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(1);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.mapId);
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x0009E41D File Offset: 0x0009C61D
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.mapId = input.ReadInt32();
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x04001333 RID: 4915
		internal int objectId;

		// Token: 0x04001334 RID: 4916
		internal int mapId;
	}
}
