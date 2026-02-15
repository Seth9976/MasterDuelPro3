using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AssetsTools.NET.Extra;

namespace AssetsTools.NET
{
	// Token: 0x0200003C RID: 60
	public class AssetsFile
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000F14B File Offset: 0x0000D34B
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000F153 File Offset: 0x0000D353
		public AssetsFileHeader Header { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000F15C File Offset: 0x0000D35C
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000F164 File Offset: 0x0000D364
		public AssetsFileMetadata Metadata { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000F16D File Offset: 0x0000D36D
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000F175 File Offset: 0x0000D375
		public AssetsFileReader Reader { get; set; }

		// Token: 0x06000195 RID: 405 RVA: 0x0000F17E File Offset: 0x0000D37E
		public void Close()
		{
			this.Reader.Close();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000F190 File Offset: 0x0000D390
		public void Read(AssetsFileReader reader)
		{
			this.Reader = reader;
			this.Header = new AssetsFileHeader();
			this.Header.Read(reader);
			this.Metadata = new AssetsFileMetadata();
			this.Metadata.Read(reader, this.Header);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000F1DE File Offset: 0x0000D3DE
		public void Read(Stream stream)
		{
			this.Read(new AssetsFileReader(stream));
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		public void Write(AssetsFileWriter writer, long filePos, List<AssetsReplacer> replacers, ClassDatabaseFile typeMeta = null)
		{
			long num = filePos;
			bool flag = filePos == -1L;
			if (flag)
			{
				num = writer.Position;
			}
			else
			{
				writer.Position = filePos;
			}
			this.Header.Write(writer);
			List<TypeTreeType> typeTreeTypes = this.Metadata.TypeTreeTypes;
			foreach (AssetsReplacer assetsReplacer in replacers.Where((AssetsReplacer r) => r.GetReplacementType() == AssetsReplacementType.AddOrModify))
			{
				int replacerClassId = assetsReplacer.GetClassID();
				ushort replacerScriptIndex = assetsReplacer.GetMonoScriptID();
				bool flag2 = this.Header.Version >= 16U;
				bool flag3;
				if (flag2)
				{
					flag3 = typeTreeTypes.Any((TypeTreeType t) => t.TypeId == replacerClassId && t.ScriptTypeIndex == replacerScriptIndex);
				}
				else
				{
					flag3 = typeTreeTypes.Any((TypeTreeType t) => t.TypeId == replacerClassId);
				}
				bool flag4 = !flag3;
				if (flag4)
				{
					TypeTreeType typeTreeType = null;
					bool flag5 = typeMeta != null;
					if (flag5)
					{
						ClassDatabaseType classDatabaseType = typeMeta.FindAssetClassByID(replacerClassId);
						bool flag6 = classDatabaseType != null;
						if (flag6)
						{
							typeTreeType = ClassDatabaseToTypeTree.Convert(typeMeta, classDatabaseType, false);
							typeTreeType.ScriptTypeIndex = replacerScriptIndex;
						}
					}
					bool flag7 = typeTreeType == null;
					if (flag7)
					{
						typeTreeType = new TypeTreeType
						{
							TypeId = replacerClassId,
							IsStrippedType = false,
							ScriptTypeIndex = replacerScriptIndex,
							ScriptIdHash = Hash128.NewBlankHash(),
							TypeHash = Hash128.NewBlankHash(),
							Nodes = new List<TypeTreeNode>(),
							StringBufferBytes = new byte[0],
							TypeDependencies = new int[0]
						};
					}
					typeTreeTypes.Add(typeTreeType);
				}
			}
			Dictionary<long, AssetFileInfo> dictionary = new Dictionary<long, AssetFileInfo>();
			Dictionary<long, AssetsReplacer> dictionary2 = replacers.ToDictionary((AssetsReplacer r) => r.GetPathID());
			List<AssetFileInfo> list = new List<AssetFileInfo>();
			foreach (AssetFileInfo assetFileInfo in this.Metadata.AssetInfos)
			{
				dictionary.Add(assetFileInfo.PathId, assetFileInfo);
				bool flag8 = dictionary2.ContainsKey(assetFileInfo.PathId);
				if (!flag8)
				{
					AssetFileInfo assetFileInfo2 = new AssetFileInfo
					{
						PathId = assetFileInfo.PathId,
						TypeIdOrIndex = assetFileInfo.TypeIdOrIndex,
						ClassId = assetFileInfo.ClassId,
						ScriptTypeIndex = assetFileInfo.ScriptTypeIndex,
						Stripped = assetFileInfo.Stripped
					};
					list.Add(assetFileInfo2);
				}
			}
			using (IEnumerator<AssetsReplacer> enumerator3 = replacers.Where((AssetsReplacer r) => r.GetReplacementType() == AssetsReplacementType.AddOrModify).GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					AssetsReplacer replacer = enumerator3.Current;
					AssetFileInfo assetFileInfo3 = new AssetFileInfo
					{
						PathId = replacer.GetPathID(),
						ClassId = (ushort)replacer.GetClassID(),
						ScriptTypeIndex = replacer.GetMonoScriptID(),
						Stripped = 0
					};
					bool flag9 = this.Header.Version < 16U;
					if (flag9)
					{
						bool flag10 = replacer.GetClassID() < 0;
						if (flag10)
						{
							assetFileInfo3.ClassId = 114;
						}
						assetFileInfo3.TypeIdOrIndex = replacer.GetClassID();
					}
					else
					{
						bool flag11 = replacer.GetMonoScriptID() == ushort.MaxValue;
						if (flag11)
						{
							assetFileInfo3.TypeIdOrIndex = typeTreeTypes.FindIndex((TypeTreeType t) => t.TypeId == replacer.GetClassID());
						}
						else
						{
							assetFileInfo3.TypeIdOrIndex = typeTreeTypes.FindIndex((TypeTreeType t) => t.TypeId == replacer.GetClassID() && t.ScriptTypeIndex == replacer.GetMonoScriptID());
						}
					}
					list.Add(assetFileInfo3);
				}
			}
			list.Sort((AssetFileInfo i1, AssetFileInfo i2) => i1.PathId.CompareTo(i2.PathId));
			AssetsFileMetadata assetsFileMetadata = new AssetsFileMetadata
			{
				UnityVersion = this.Metadata.UnityVersion,
				TargetPlatform = this.Metadata.TargetPlatform,
				TypeTreeEnabled = this.Metadata.TypeTreeEnabled,
				TypeTreeTypes = typeTreeTypes,
				AssetInfos = list,
				ScriptTypes = this.Metadata.ScriptTypes,
				Externals = this.Metadata.Externals,
				RefTypes = this.Metadata.RefTypes,
				UserInformation = this.Metadata.UserInformation
			};
			long position = writer.Position;
			assetsFileMetadata.Write(writer, this.Header.Version);
			int num2 = (int)(writer.Position - position);
			bool flag12 = writer.Position < 4096L;
			if (flag12)
			{
				while (writer.Position < 4096L)
				{
					writer.Write(0);
				}
			}
			else
			{
				bool flag13 = writer.Position % 16L == 0L;
				if (flag13)
				{
					writer.Position += 16L;
				}
				else
				{
					writer.Align16();
				}
			}
			long position2 = writer.Position;
			for (int i = 0; i < list.Count; i++)
			{
				AssetFileInfo assetFileInfo4 = list[i];
				assetFileInfo4.ByteStart = writer.Position - position2;
				AssetsReplacer assetsReplacer2;
				bool flag14 = dictionary2.TryGetValue(assetFileInfo4.PathId, out assetsReplacer2);
				if (flag14)
				{
					assetsReplacer2.Write(writer);
				}
				else
				{
					AssetFileInfo assetFileInfo5 = dictionary[assetFileInfo4.PathId];
					this.Reader.Position = this.Header.DataOffset + assetFileInfo5.ByteStart;
					this.Reader.BaseStream.CopyToCompat(writer.BaseStream, (long)((ulong)assetFileInfo5.ByteSize), 81920);
				}
				assetFileInfo4.ByteSize = (uint)(writer.Position - (position2 + assetFileInfo4.ByteStart));
				bool flag15 = i != list.Count - 1;
				if (flag15)
				{
					writer.Align8();
				}
			}
			long num3 = writer.Position - num;
			AssetsFileHeader assetsFileHeader = new AssetsFileHeader
			{
				MetadataSize = (long)num2,
				FileSize = num3,
				Version = this.Header.Version,
				DataOffset = position2,
				Endianness = this.Header.Endianness
			};
			writer.Position = num;
			assetsFileHeader.Write(writer);
			writer.Position = position;
			assetsFileMetadata.Write(writer, this.Header.Version);
			writer.Position = num + num3;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000F934 File Offset: 0x0000DB34
		public ushort GetScriptIndex(AssetFileInfo info)
		{
			bool flag = this.Header.Version < 16U;
			ushort num;
			if (flag)
			{
				num = info.ScriptTypeIndex;
			}
			else
			{
				num = this.Metadata.TypeTreeTypes[info.TypeIdOrIndex].ScriptTypeIndex;
			}
			return num;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000F980 File Offset: 0x0000DB80
		public static bool IsAssetsFile(string filePath)
		{
			bool flag;
			using (AssetsFileReader assetsFileReader = new AssetsFileReader(filePath))
			{
				flag = AssetsFile.IsAssetsFile(assetsFileReader, 0L, assetsFileReader.BaseStream.Length);
			}
			return flag;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
		public static bool IsAssetsFile(AssetsFileReader reader, long offset, long length)
		{
			reader.BigEndian = true;
			bool flag = length < 48L;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				reader.Position = offset;
				string text = reader.ReadStringLength(5);
				bool flag3 = text == "Unity";
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					reader.Position = offset + 8L;
					int num = reader.ReadInt32();
					bool flag4 = num > 99;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						reader.Position = offset + 20L;
						bool flag5 = num >= 22;
						if (flag5)
						{
							reader.Position += 28L;
						}
						string text2 = "";
						char c;
						while (reader.Position < reader.BaseStream.Length && (c = (char)reader.ReadByte()) > '\0')
						{
							text2 += c.ToString();
							bool flag6 = text2.Length > 255;
							if (flag6)
							{
								return false;
							}
						}
						string text3 = Regex.Replace(text2, "[a-zA-Z0-9\\.\\n]", "");
						string text4 = Regex.Replace(text2, "[^a-zA-Z0-9\\.\\n]", "");
						flag2 = text3 == "" && text4.Length > 0;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000FB0A File Offset: 0x0000DD0A
		public AssetFileInfo GetAssetInfo(long pathId)
		{
			return this.Metadata.GetAssetInfo(pathId);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000FB18 File Offset: 0x0000DD18
		public void GenerateQuickLookup()
		{
			this.Metadata.GenerateQuickLookup();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000FB26 File Offset: 0x0000DD26
		public List<AssetFileInfo> GetAssetsOfType(int typeId)
		{
			return this.Metadata.GetAssetsOfType(typeId);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000FB34 File Offset: 0x0000DD34
		public List<AssetFileInfo> GetAssetsOfType(AssetClassID typeId)
		{
			return this.Metadata.GetAssetsOfType(typeId);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000FB42 File Offset: 0x0000DD42
		public List<AssetFileInfo> GetAssetsOfType(int typeId, ushort scriptIndex)
		{
			return this.Metadata.GetAssetsOfType(typeId, scriptIndex);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000FB51 File Offset: 0x0000DD51
		public List<AssetFileInfo> GetAssetsOfType(AssetClassID typeId, ushort scriptIndex)
		{
			return this.Metadata.GetAssetsOfType(typeId, scriptIndex);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000FB60 File Offset: 0x0000DD60
		public List<AssetFileInfo> AssetInfos
		{
			get
			{
				return this.Metadata.AssetInfos;
			}
		}
	}
}
