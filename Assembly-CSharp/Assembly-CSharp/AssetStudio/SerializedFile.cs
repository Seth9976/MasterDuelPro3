using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AssetStudio
{
	// Token: 0x02000178 RID: 376
	public class SerializedFile
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x00018750 File Offset: 0x00016950
		public SerializedFile(FileReader reader, AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
			this.reader = reader;
			this.fullName = reader.FullPath;
			this.fileName = reader.FileName;
			this.header = new SerializedFileHeader();
			this.header.m_MetadataSize = reader.ReadUInt32();
			this.header.m_FileSize = (long)((ulong)reader.ReadUInt32());
			this.header.m_Version = (SerializedFileFormatVersion)reader.ReadUInt32();
			this.header.m_DataOffset = (long)((ulong)reader.ReadUInt32());
			if (this.header.m_Version >= SerializedFileFormatVersion.Unknown_9)
			{
				this.header.m_Endianess = reader.ReadByte();
				this.header.m_Reserved = reader.ReadBytes(3);
				this.m_FileEndianess = this.header.m_Endianess;
			}
			else
			{
				reader.Position = this.header.m_FileSize - (long)((ulong)this.header.m_MetadataSize);
				this.m_FileEndianess = reader.ReadByte();
			}
			if (this.header.m_Version >= SerializedFileFormatVersion.LargeFilesSupport)
			{
				this.header.m_MetadataSize = reader.ReadUInt32();
				this.header.m_FileSize = reader.ReadInt64();
				this.header.m_DataOffset = reader.ReadInt64();
				reader.ReadInt64();
			}
			if (this.m_FileEndianess == 0)
			{
				reader.Endian = EndianType.LittleEndian;
			}
			if (this.header.m_Version >= SerializedFileFormatVersion.Unknown_7)
			{
				this.unityVersion = reader.ReadStringToNull(32767);
				this.SetVersion(this.unityVersion);
			}
			if (this.header.m_Version >= SerializedFileFormatVersion.Unknown_8)
			{
				this.m_TargetPlatform = (BuildTarget)reader.ReadInt32();
				if (!Enum.IsDefined(typeof(BuildTarget), this.m_TargetPlatform))
				{
					this.m_TargetPlatform = BuildTarget.UnknownPlatform;
				}
			}
			if (this.header.m_Version >= SerializedFileFormatVersion.HasTypeTreeHashes)
			{
				this.m_EnableTypeTree = reader.ReadBoolean();
			}
			int typeCount = reader.ReadInt32();
			this.m_Types = new List<SerializedType>(typeCount);
			for (int i = 0; i < typeCount; i++)
			{
				this.m_Types.Add(this.ReadSerializedType(false));
			}
			if (this.header.m_Version >= SerializedFileFormatVersion.Unknown_7 && this.header.m_Version < SerializedFileFormatVersion.Unknown_14)
			{
				this.bigIDEnabled = reader.ReadInt32();
			}
			int objectCount = reader.ReadInt32();
			this.m_Objects = new List<ObjectInfo>(objectCount);
			this.Objects = new List<Object>(objectCount);
			this.ObjectsDic = new Dictionary<long, Object>(objectCount);
			for (int j = 0; j < objectCount; j++)
			{
				ObjectInfo objectInfo = new ObjectInfo();
				if (this.bigIDEnabled != 0)
				{
					objectInfo.m_PathID = reader.ReadInt64();
				}
				else if (this.header.m_Version < SerializedFileFormatVersion.Unknown_14)
				{
					objectInfo.m_PathID = (long)reader.ReadInt32();
				}
				else
				{
					reader.AlignStream();
					objectInfo.m_PathID = reader.ReadInt64();
				}
				if (this.header.m_Version >= SerializedFileFormatVersion.LargeFilesSupport)
				{
					objectInfo.byteStart = reader.ReadInt64();
				}
				else
				{
					objectInfo.byteStart = (long)((ulong)reader.ReadUInt32());
				}
				objectInfo.byteStart += this.header.m_DataOffset;
				objectInfo.byteSize = reader.ReadUInt32();
				objectInfo.typeID = reader.ReadInt32();
				if (this.header.m_Version < SerializedFileFormatVersion.RefactoredClassId)
				{
					objectInfo.classID = (int)reader.ReadUInt16();
					objectInfo.serializedType = this.m_Types.Find((SerializedType x) => x.classID == objectInfo.typeID);
				}
				else
				{
					SerializedType type = this.m_Types[objectInfo.typeID];
					objectInfo.serializedType = type;
					objectInfo.classID = type.classID;
				}
				if (this.header.m_Version < SerializedFileFormatVersion.HasScriptTypeIndex)
				{
					objectInfo.isDestroyed = reader.ReadUInt16();
				}
				if (this.header.m_Version >= SerializedFileFormatVersion.HasScriptTypeIndex && this.header.m_Version < SerializedFileFormatVersion.RefactorTypeData)
				{
					short m_ScriptTypeIndex = reader.ReadInt16();
					if (objectInfo.serializedType != null)
					{
						objectInfo.serializedType.m_ScriptTypeIndex = m_ScriptTypeIndex;
					}
				}
				if (this.header.m_Version == SerializedFileFormatVersion.SupportsStrippedObject || this.header.m_Version == SerializedFileFormatVersion.RefactoredClassId)
				{
					objectInfo.stripped = reader.ReadByte();
				}
				this.m_Objects.Add(objectInfo);
			}
			if (this.header.m_Version >= SerializedFileFormatVersion.HasScriptTypeIndex)
			{
				int scriptCount = reader.ReadInt32();
				this.m_ScriptTypes = new List<LocalSerializedObjectIdentifier>(scriptCount);
				for (int k = 0; k < scriptCount; k++)
				{
					LocalSerializedObjectIdentifier m_ScriptType = new LocalSerializedObjectIdentifier();
					m_ScriptType.localSerializedFileIndex = reader.ReadInt32();
					if (this.header.m_Version < SerializedFileFormatVersion.Unknown_14)
					{
						m_ScriptType.localIdentifierInFile = (long)reader.ReadInt32();
					}
					else
					{
						reader.AlignStream();
						m_ScriptType.localIdentifierInFile = reader.ReadInt64();
					}
					this.m_ScriptTypes.Add(m_ScriptType);
				}
			}
			int externalsCount = reader.ReadInt32();
			this.m_Externals = new List<FileIdentifier>(externalsCount);
			for (int l = 0; l < externalsCount; l++)
			{
				FileIdentifier m_External = new FileIdentifier();
				if (this.header.m_Version >= SerializedFileFormatVersion.Unknown_6)
				{
					reader.ReadStringToNull(32767);
				}
				if (this.header.m_Version >= SerializedFileFormatVersion.Unknown_5)
				{
					m_External.guid = new Guid(reader.ReadBytes(16));
					m_External.type = reader.ReadInt32();
				}
				m_External.pathName = reader.ReadStringToNull(32767);
				m_External.fileName = Path.GetFileName(m_External.pathName);
				this.m_Externals.Add(m_External);
			}
			if (this.header.m_Version >= SerializedFileFormatVersion.SupportsRefObject)
			{
				int refTypesCount = reader.ReadInt32();
				this.m_RefTypes = new List<SerializedType>(refTypesCount);
				for (int m = 0; m < refTypesCount; m++)
				{
					this.m_RefTypes.Add(this.ReadSerializedType(true));
				}
			}
			if (this.header.m_Version >= SerializedFileFormatVersion.Unknown_5)
			{
				this.userInformation = reader.ReadStringToNull(32767);
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00018D94 File Offset: 0x00016F94
		public void SetVersion(string stringVersion)
		{
			if (stringVersion != "0.0.0")
			{
				this.unityVersion = stringVersion;
				string[] buildSplit = Regex.Replace(stringVersion, "\\d", "").Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
				this.buildType = new BuildType(buildSplit[0]);
				string[] versionSplit = Regex.Replace(stringVersion, "\\D", ".").Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
				this.version = versionSplit.Select(new Func<string, int>(int.Parse)).ToArray<int>();
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00018E2C File Offset: 0x0001702C
		private SerializedType ReadSerializedType(bool isRefType)
		{
			SerializedType type = new SerializedType();
			type.classID = this.reader.ReadInt32();
			if (this.header.m_Version >= SerializedFileFormatVersion.RefactoredClassId)
			{
				type.m_IsStrippedType = this.reader.ReadBoolean();
			}
			if (this.header.m_Version >= SerializedFileFormatVersion.RefactorTypeData)
			{
				type.m_ScriptTypeIndex = this.reader.ReadInt16();
			}
			if (this.header.m_Version >= SerializedFileFormatVersion.HasTypeTreeHashes)
			{
				if (isRefType && type.m_ScriptTypeIndex >= 0)
				{
					type.m_ScriptID = this.reader.ReadBytes(16);
				}
				else if ((this.header.m_Version < SerializedFileFormatVersion.RefactoredClassId && type.classID < 0) || (this.header.m_Version >= SerializedFileFormatVersion.RefactoredClassId && type.classID == 114))
				{
					type.m_ScriptID = this.reader.ReadBytes(16);
				}
				type.m_OldTypeHash = this.reader.ReadBytes(16);
			}
			if (this.m_EnableTypeTree)
			{
				type.m_Type = new TypeTree();
				type.m_Type.m_Nodes = new List<TypeTreeNode>();
				if (this.header.m_Version >= SerializedFileFormatVersion.Unknown_12 || this.header.m_Version == SerializedFileFormatVersion.Unknown_10)
				{
					this.TypeTreeBlobRead(type.m_Type);
				}
				else
				{
					this.ReadTypeTree(type.m_Type, 0);
				}
				if (this.header.m_Version >= SerializedFileFormatVersion.StoresTypeDependencies)
				{
					if (isRefType)
					{
						type.m_KlassName = this.reader.ReadStringToNull(32767);
						type.m_NameSpace = this.reader.ReadStringToNull(32767);
						type.m_AsmName = this.reader.ReadStringToNull(32767);
					}
					else
					{
						type.m_TypeDependencies = this.reader.ReadInt32Array();
					}
				}
			}
			return type;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00018FE0 File Offset: 0x000171E0
		private void ReadTypeTree(TypeTree m_Type, int level = 0)
		{
			TypeTreeNode typeTreeNode = new TypeTreeNode();
			m_Type.m_Nodes.Add(typeTreeNode);
			typeTreeNode.m_Level = level;
			typeTreeNode.m_Type = this.reader.ReadStringToNull(32767);
			typeTreeNode.m_Name = this.reader.ReadStringToNull(32767);
			typeTreeNode.m_ByteSize = this.reader.ReadInt32();
			if (this.header.m_Version == SerializedFileFormatVersion.Unknown_2)
			{
				this.reader.ReadInt32();
			}
			if (this.header.m_Version != SerializedFileFormatVersion.Unknown_3)
			{
				typeTreeNode.m_Index = this.reader.ReadInt32();
			}
			typeTreeNode.m_TypeFlags = this.reader.ReadInt32();
			typeTreeNode.m_Version = this.reader.ReadInt32();
			if (this.header.m_Version != SerializedFileFormatVersion.Unknown_3)
			{
				typeTreeNode.m_MetaFlag = this.reader.ReadInt32();
			}
			int childrenCount = this.reader.ReadInt32();
			for (int i = 0; i < childrenCount; i++)
			{
				this.ReadTypeTree(m_Type, level + 1);
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000190E0 File Offset: 0x000172E0
		private void TypeTreeBlobRead(TypeTree m_Type)
		{
			int numberOfNodes = this.reader.ReadInt32();
			int stringBufferSize = this.reader.ReadInt32();
			for (int i = 0; i < numberOfNodes; i++)
			{
				TypeTreeNode typeTreeNode = new TypeTreeNode();
				m_Type.m_Nodes.Add(typeTreeNode);
				typeTreeNode.m_Version = (int)this.reader.ReadUInt16();
				typeTreeNode.m_Level = (int)this.reader.ReadByte();
				typeTreeNode.m_TypeFlags = (int)this.reader.ReadByte();
				typeTreeNode.m_TypeStrOffset = this.reader.ReadUInt32();
				typeTreeNode.m_NameStrOffset = this.reader.ReadUInt32();
				typeTreeNode.m_ByteSize = this.reader.ReadInt32();
				typeTreeNode.m_Index = this.reader.ReadInt32();
				typeTreeNode.m_MetaFlag = this.reader.ReadInt32();
				if (this.header.m_Version >= SerializedFileFormatVersion.TypeTreeNodeWithTypeFlags)
				{
					typeTreeNode.m_RefTypeHash = this.reader.ReadUInt64();
				}
			}
			m_Type.m_StringBuffer = this.reader.ReadBytes(stringBufferSize);
			using (BinaryReader stringBufferReader = new BinaryReader(new MemoryStream(m_Type.m_StringBuffer)))
			{
				for (int j = 0; j < numberOfNodes; j++)
				{
					TypeTreeNode m_Node = m_Type.m_Nodes[j];
					m_Node.m_Type = SerializedFile.<TypeTreeBlobRead>g__ReadString|25_0(stringBufferReader, m_Node.m_TypeStrOffset);
					m_Node.m_Name = SerializedFile.<TypeTreeBlobRead>g__ReadString|25_0(stringBufferReader, m_Node.m_NameStrOffset);
				}
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001925C File Offset: 0x0001745C
		public void AddObject(Object obj)
		{
			this.Objects.Add(obj);
			this.ObjectsDic.Add(obj.m_PathID, obj);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0001927C File Offset: 0x0001747C
		public bool IsVersionStripped
		{
			get
			{
				return this.unityVersion == "0.0.0";
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00019290 File Offset: 0x00017490
		[CompilerGenerated]
		internal static string <TypeTreeBlobRead>g__ReadString|25_0(BinaryReader stringBufferReader, uint value)
		{
			if ((value & 2147483648U) == 0U)
			{
				stringBufferReader.BaseStream.Position = (long)((ulong)value);
				return stringBufferReader.ReadStringToNull(32767);
			}
			uint offset = value & 2147483647U;
			string str;
			if (CommonString.StringBuffer.TryGetValue(offset, out str))
			{
				return str;
			}
			return offset.ToString();
		}

		// Token: 0x040009D4 RID: 2516
		public AssetsManager assetsManager;

		// Token: 0x040009D5 RID: 2517
		public FileReader reader;

		// Token: 0x040009D6 RID: 2518
		public string fullName;

		// Token: 0x040009D7 RID: 2519
		public string originalPath;

		// Token: 0x040009D8 RID: 2520
		public string fileName;

		// Token: 0x040009D9 RID: 2521
		public int[] version = new int[4];

		// Token: 0x040009DA RID: 2522
		public BuildType buildType;

		// Token: 0x040009DB RID: 2523
		public List<Object> Objects;

		// Token: 0x040009DC RID: 2524
		public Dictionary<long, Object> ObjectsDic;

		// Token: 0x040009DD RID: 2525
		public SerializedFileHeader header;

		// Token: 0x040009DE RID: 2526
		private byte m_FileEndianess;

		// Token: 0x040009DF RID: 2527
		public string unityVersion = "2.5.0f5";

		// Token: 0x040009E0 RID: 2528
		public BuildTarget m_TargetPlatform = BuildTarget.UnknownPlatform;

		// Token: 0x040009E1 RID: 2529
		private bool m_EnableTypeTree = true;

		// Token: 0x040009E2 RID: 2530
		public List<SerializedType> m_Types;

		// Token: 0x040009E3 RID: 2531
		public int bigIDEnabled;

		// Token: 0x040009E4 RID: 2532
		public List<ObjectInfo> m_Objects;

		// Token: 0x040009E5 RID: 2533
		private List<LocalSerializedObjectIdentifier> m_ScriptTypes;

		// Token: 0x040009E6 RID: 2534
		public List<FileIdentifier> m_Externals;

		// Token: 0x040009E7 RID: 2535
		public List<SerializedType> m_RefTypes;

		// Token: 0x040009E8 RID: 2536
		public string userInformation;

		// Token: 0x040009E9 RID: 2537
		private const string strippedVersion = "0.0.0";
	}
}
