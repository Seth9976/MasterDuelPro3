using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x020005DC RID: 1500
	internal abstract class Win32Resource
	{
		// Token: 0x06002C62 RID: 11362 RVA: 0x000B0C12 File Offset: 0x000AEE12
		internal Win32Resource(NameOrId type, NameOrId name, int language)
		{
			this.type = type;
			this.name = name;
			this.language = language;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000B0C2F File Offset: 0x000AEE2F
		internal Win32Resource(Win32ResourceType type, int name, int language)
		{
			this.type = new NameOrId((int)type);
			this.name = new NameOrId(name);
			this.language = language;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06002C64 RID: 11364 RVA: 0x000B0C56 File Offset: 0x000AEE56
		public Win32ResourceType ResourceType
		{
			get
			{
				if (this.type.IsName)
				{
					return (Win32ResourceType)(-1);
				}
				return (Win32ResourceType)this.type.Id;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06002C65 RID: 11365 RVA: 0x000B0C72 File Offset: 0x000AEE72
		public NameOrId Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002C66 RID: 11366 RVA: 0x000B0C7A File Offset: 0x000AEE7A
		public NameOrId Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000B0C82 File Offset: 0x000AEE82
		public int Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x06002C68 RID: 11368
		public abstract void WriteTo(Stream s);

		// Token: 0x06002C69 RID: 11369 RVA: 0x000B0C8C File Offset: 0x000AEE8C
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = "Win32Resource (Kind=";
			array[1] = this.ResourceType.ToString();
			array[2] = ", Name=";
			int num = 3;
			NameOrId nameOrId = this.name;
			array[num] = ((nameOrId != null) ? nameOrId.ToString() : null);
			array[4] = ")";
			return string.Concat(array);
		}

		// Token: 0x040016A0 RID: 5792
		private NameOrId type;

		// Token: 0x040016A1 RID: 5793
		private NameOrId name;

		// Token: 0x040016A2 RID: 5794
		private int language;
	}
}
