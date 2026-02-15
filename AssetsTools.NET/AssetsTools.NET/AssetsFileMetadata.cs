using System;
using System.Collections.Generic;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x02000043 RID: 67
	public class AssetsFileMetadata
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00010008 File Offset: 0x0000E208
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00010010 File Offset: 0x0000E210
		public string UnityVersion { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00010019 File Offset: 0x0000E219
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00010021 File Offset: 0x0000E221
		public uint TargetPlatform { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0001002A File Offset: 0x0000E22A
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00010032 File Offset: 0x0000E232
		public bool TypeTreeEnabled { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0001003B File Offset: 0x0000E23B
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00010043 File Offset: 0x0000E243
		public List<TypeTreeType> TypeTreeTypes { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0001004C File Offset: 0x0000E24C
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00010054 File Offset: 0x0000E254
		public List<AssetFileInfo> AssetInfos { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0001005D File Offset: 0x0000E25D
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00010065 File Offset: 0x0000E265
		public List<AssetPPtr> ScriptTypes { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0001006E File Offset: 0x0000E26E
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00010076 File Offset: 0x0000E276
		public List<AssetsFileExternal> Externals { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0001007F File Offset: 0x0000E27F
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00010087 File Offset: 0x0000E287
		public List<TypeTreeType> RefTypes { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00010090 File Offset: 0x0000E290
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00010098 File Offset: 0x0000E298
		public string UserInformation { get; set; }

		// Token: 0x060001DC RID: 476 RVA: 0x000100A1 File Offset: 0x0000E2A1
		public void Read(AssetsFileReader reader, AssetsFileHeader header)
		{
			this.Read(reader, header.Version, header.DataOffset);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000100B8 File Offset: 0x0000E2B8
		public void Read(AssetsFileReader reader, uint version)
		{
			this.Read(reader, version, -1L);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000100C8 File Offset: 0x0000E2C8
		public void Read(AssetsFileReader reader, uint version, long dataOffset)
		{
			this._quickLookup = null;
			this.UnityVersion = reader.ReadNullTerminated();
			this.TargetPlatform = reader.ReadUInt32();
			bool flag = version >= 13U;
			if (flag)
			{
				this.TypeTreeEnabled = reader.ReadBoolean();
			}
			int num = reader.ReadInt32();
			this.TypeTreeTypes = new List<TypeTreeType>(num);
			for (int i = 0; i < num; i++)
			{
				TypeTreeType typeTreeType = new TypeTreeType();
				typeTreeType.Read(reader, version, this.TypeTreeEnabled, false);
				this.TypeTreeTypes.Add(typeTreeType);
			}
			int num2 = reader.ReadInt32();
			reader.Align();
			this.AssetInfos = new List<AssetFileInfo>(num2);
			for (int j = 0; j < num2; j++)
			{
				AssetFileInfo assetFileInfo = new AssetFileInfo();
				assetFileInfo.Read(reader, version);
				assetFileInfo.TypeId = assetFileInfo.GetTypeId(this, version);
				bool flag2 = dataOffset != -1L;
				if (flag2)
				{
					assetFileInfo.AbsoluteByteStart = assetFileInfo.GetAbsoluteByteStart(dataOffset);
				}
				this.AssetInfos.Add(assetFileInfo);
			}
			int num3 = reader.ReadInt32();
			this.ScriptTypes = new List<AssetPPtr>(num3);
			for (int k = 0; k < num3; k++)
			{
				int num4 = reader.ReadInt32();
				reader.Align();
				long num5 = reader.ReadInt64();
				AssetPPtr assetPPtr = new AssetPPtr(num4, num5);
				this.ScriptTypes.Add(assetPPtr);
			}
			int num6 = reader.ReadInt32();
			this.Externals = new List<AssetsFileExternal>(num6);
			for (int l = 0; l < num6; l++)
			{
				AssetsFileExternal assetsFileExternal = new AssetsFileExternal();
				assetsFileExternal.Read(reader);
				this.Externals.Add(assetsFileExternal);
			}
			bool flag3 = version >= 20U;
			if (flag3)
			{
				int num7 = reader.ReadInt32();
				this.RefTypes = new List<TypeTreeType>(num7);
				for (int m = 0; m < num7; m++)
				{
					TypeTreeType typeTreeType2 = new TypeTreeType();
					typeTreeType2.Read(reader, version, this.TypeTreeEnabled, true);
					this.RefTypes.Add(typeTreeType2);
				}
			}
			bool flag4 = version >= 5U;
			if (flag4)
			{
				this.UserInformation = reader.ReadNullTerminated();
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00010310 File Offset: 0x0000E510
		public void Write(AssetsFileWriter writer, uint version)
		{
			writer.WriteNullTerminated(this.UnityVersion);
			writer.Write(this.TargetPlatform);
			bool flag = version >= 13U;
			if (flag)
			{
				writer.Write(this.TypeTreeEnabled);
			}
			writer.Write(this.TypeTreeTypes.Count);
			for (int i = 0; i < this.TypeTreeTypes.Count; i++)
			{
				this.TypeTreeTypes[i].Write(writer, version, this.TypeTreeEnabled);
			}
			writer.Write(this.AssetInfos.Count);
			writer.Align();
			for (int j = 0; j < this.AssetInfos.Count; j++)
			{
				this.AssetInfos[j].Write(writer, version);
			}
			writer.Write(this.ScriptTypes.Count);
			for (int k = 0; k < this.ScriptTypes.Count; k++)
			{
				writer.Write(this.ScriptTypes[k].FileId);
				writer.Align();
				writer.Write(this.ScriptTypes[k].PathId);
			}
			writer.Write(this.Externals.Count);
			for (int l = 0; l < this.Externals.Count; l++)
			{
				this.Externals[l].Write(writer);
			}
			bool flag2 = version >= 20U;
			if (flag2)
			{
				writer.Write(this.RefTypes.Count);
				for (int m = 0; m < this.RefTypes.Count; m++)
				{
					this.RefTypes[m].Write(writer, version, this.TypeTreeEnabled);
				}
			}
			bool flag3 = version >= 5U;
			if (flag3)
			{
				writer.WriteNullTerminated(this.UserInformation);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00010518 File Offset: 0x0000E718
		public AssetFileInfo GetAssetInfo(long pathId)
		{
			bool flag = this._quickLookup != null;
			if (flag)
			{
				bool flag2 = this._quickLookup.ContainsKey(pathId);
				if (flag2)
				{
					return this.AssetInfos[this._quickLookup[pathId]];
				}
			}
			else
			{
				for (int i = 0; i < this.AssetInfos.Count; i++)
				{
					AssetFileInfo assetFileInfo = this.AssetInfos[i];
					bool flag3 = assetFileInfo.PathId == pathId;
					if (flag3)
					{
						return assetFileInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000105AC File Offset: 0x0000E7AC
		public void GenerateQuickLookup()
		{
			this._quickLookup = new Dictionary<long, int>();
			for (int i = 0; i < this.AssetInfos.Count; i++)
			{
				AssetFileInfo assetFileInfo = this.AssetInfos[i];
				this._quickLookup[assetFileInfo.PathId] = i;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00010604 File Offset: 0x0000E804
		public List<AssetFileInfo> GetAssetsOfType(int typeId)
		{
			List<AssetFileInfo> list = new List<AssetFileInfo>();
			foreach (AssetFileInfo assetFileInfo in this.AssetInfos)
			{
				bool flag = assetFileInfo.TypeId == typeId;
				if (flag)
				{
					list.Add(assetFileInfo);
				}
			}
			return list;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0001067C File Offset: 0x0000E87C
		public List<AssetFileInfo> GetAssetsOfType(int typeId, ushort scriptIndex)
		{
			List<AssetFileInfo> list = new List<AssetFileInfo>();
			foreach (AssetFileInfo assetFileInfo in this.AssetInfos)
			{
				bool flag = scriptIndex != ushort.MaxValue;
				if (flag)
				{
					bool flag2 = assetFileInfo.TypeId < 0;
					if (flag2)
					{
						bool flag3 = assetFileInfo.ScriptTypeIndex != scriptIndex;
						if (flag3)
						{
							continue;
						}
						bool flag4 = typeId != 114 && (typeId >= 0 || assetFileInfo.TypeId != typeId);
						if (flag4)
						{
							continue;
						}
					}
					else
					{
						bool flag5 = assetFileInfo.TypeId == 114;
						if (!flag5)
						{
							continue;
						}
						bool flag6 = this.FindTypeTreeTypeByID(assetFileInfo.TypeId, assetFileInfo.ScriptTypeIndex).ScriptTypeIndex != scriptIndex;
						if (flag6)
						{
							continue;
						}
						bool flag7 = typeId != 114;
						if (flag7)
						{
							continue;
						}
					}
				}
				else
				{
					bool flag8 = assetFileInfo.TypeId != typeId;
					if (flag8)
					{
						continue;
					}
				}
				list.Add(assetFileInfo);
			}
			return list;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000107B0 File Offset: 0x0000E9B0
		public List<AssetFileInfo> GetAssetsOfType(AssetClassID typeId)
		{
			return this.GetAssetsOfType((int)typeId);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000107CC File Offset: 0x0000E9CC
		public List<AssetFileInfo> GetAssetsOfType(AssetClassID typeId, ushort scriptIndex)
		{
			return this.GetAssetsOfType((int)typeId, scriptIndex);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000107E8 File Offset: 0x0000E9E8
		public TypeTreeType FindTypeTreeTypeByID(int id)
		{
			foreach (TypeTreeType typeTreeType in this.TypeTreeTypes)
			{
				bool flag = typeTreeType.TypeId == id;
				if (flag)
				{
					return typeTreeType;
				}
			}
			return null;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00010850 File Offset: 0x0000EA50
		public TypeTreeType FindTypeTreeTypeByID(int id, ushort scriptIndex)
		{
			foreach (TypeTreeType typeTreeType in this.TypeTreeTypes)
			{
				bool flag = typeTreeType.TypeId == id;
				if (flag)
				{
					bool flag2 = typeTreeType.ScriptTypeIndex == scriptIndex;
					if (flag2)
					{
						return typeTreeType;
					}
					bool flag3 = id < 0 && typeTreeType.ScriptTypeIndex == ushort.MaxValue;
					if (flag3)
					{
						return typeTreeType;
					}
				}
			}
			return null;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000108E8 File Offset: 0x0000EAE8
		public TypeTreeType FindTypeTreeTypeByScriptIndex(ushort scriptIndex)
		{
			foreach (TypeTreeType typeTreeType in this.TypeTreeTypes)
			{
				bool flag = typeTreeType.ScriptTypeIndex == scriptIndex;
				if (flag)
				{
					return typeTreeType;
				}
			}
			return null;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00010950 File Offset: 0x0000EB50
		public TypeTreeType FindTypeTreeTypeByName(string name)
		{
			foreach (TypeTreeType typeTreeType in this.TypeTreeTypes)
			{
				bool flag = typeTreeType.Nodes[0].GetTypeString(typeTreeType.StringBuffer, null) == name;
				if (flag)
				{
					return typeTreeType;
				}
			}
			return null;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000109CC File Offset: 0x0000EBCC
		public TypeTreeType FindRefTypeByIndex(ushort scriptIndex)
		{
			foreach (TypeTreeType typeTreeType in this.RefTypes)
			{
				bool flag = typeTreeType.ScriptTypeIndex == scriptIndex;
				if (flag)
				{
					return typeTreeType;
				}
			}
			return null;
		}

		// Token: 0x04000190 RID: 400
		private Dictionary<long, int> _quickLookup = null;
	}
}
