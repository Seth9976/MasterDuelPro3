using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004F3 RID: 1267
	internal sealed class ObjectMap
	{
		// Token: 0x06002799 RID: 10137 RVA: 0x0009F890 File Offset: 0x0009DA90
		internal ObjectMap(string objectName, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
		{
			this.objectName = objectName;
			this.objectType = objectType;
			this.memberNames = memberNames;
			this.objectReader = objectReader;
			this.objectId = objectId;
			this.assemblyInfo = assemblyInfo;
			this.objectInfo = objectReader.CreateReadObjectInfo(objectType);
			this.memberTypes = this.objectInfo.GetMemberTypes(memberNames, objectType);
			this.binaryTypeEnumA = new BinaryTypeEnum[this.memberTypes.Length];
			this.typeInformationA = new object[this.memberTypes.Length];
			for (int i = 0; i < this.memberTypes.Length; i++)
			{
				object obj = null;
				BinaryTypeEnum parserBinaryTypeInfo = BinaryConverter.GetParserBinaryTypeInfo(this.memberTypes[i], out obj);
				this.binaryTypeEnumA[i] = parserBinaryTypeInfo;
				this.typeInformationA[i] = obj;
			}
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x0009F958 File Offset: 0x0009DB58
		internal ObjectMap(string objectName, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
		{
			this.objectName = objectName;
			this.memberNames = memberNames;
			this.binaryTypeEnumA = binaryTypeEnumA;
			this.typeInformationA = typeInformationA;
			this.objectReader = objectReader;
			this.objectId = objectId;
			this.assemblyInfo = assemblyInfo;
			if (assemblyInfo == null)
			{
				throw new SerializationException(Environment.GetResourceString("No assembly information is available for object on the wire, '{0}'.", new object[] { objectName }));
			}
			this.objectType = objectReader.GetType(assemblyInfo, objectName);
			this.memberTypes = new Type[memberNames.Length];
			for (int i = 0; i < memberNames.Length; i++)
			{
				InternalPrimitiveTypeE internalPrimitiveTypeE;
				string text;
				Type type;
				bool flag;
				BinaryConverter.TypeFromInfo(binaryTypeEnumA[i], typeInformationA[i], objectReader, (BinaryAssemblyInfo)assemIdToAssemblyTable[memberAssemIds[i]], out internalPrimitiveTypeE, out text, out type, out flag);
				this.memberTypes[i] = type;
			}
			this.objectInfo = objectReader.CreateReadObjectInfo(this.objectType, memberNames, null);
			if (!this.objectInfo.isSi)
			{
				this.objectInfo.GetMemberTypes(memberNames, this.objectInfo.objectType);
			}
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x0009FA5C File Offset: 0x0009DC5C
		internal ReadObjectInfo CreateObjectInfo(ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isInitObjectInfo)
			{
				this.isInitObjectInfo = false;
				this.objectInfo.InitDataStore(ref si, ref memberData);
				return this.objectInfo;
			}
			this.objectInfo.PrepareForReuse();
			this.objectInfo.InitDataStore(ref si, ref memberData);
			return this.objectInfo;
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x0009FAAA File Offset: 0x0009DCAA
		internal static ObjectMap Create(string name, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
		{
			return new ObjectMap(name, objectType, memberNames, objectReader, objectId, assemblyInfo);
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x0009FABC File Offset: 0x0009DCBC
		internal static ObjectMap Create(string name, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
		{
			return new ObjectMap(name, memberNames, binaryTypeEnumA, typeInformationA, memberAssemIds, objectReader, objectId, assemblyInfo, assemIdToAssemblyTable);
		}

		// Token: 0x04001371 RID: 4977
		internal string objectName;

		// Token: 0x04001372 RID: 4978
		internal Type objectType;

		// Token: 0x04001373 RID: 4979
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x04001374 RID: 4980
		internal object[] typeInformationA;

		// Token: 0x04001375 RID: 4981
		internal Type[] memberTypes;

		// Token: 0x04001376 RID: 4982
		internal string[] memberNames;

		// Token: 0x04001377 RID: 4983
		internal ReadObjectInfo objectInfo;

		// Token: 0x04001378 RID: 4984
		internal bool isInitObjectInfo = true;

		// Token: 0x04001379 RID: 4985
		internal ObjectReader objectReader;

		// Token: 0x0400137A RID: 4986
		internal int objectId;

		// Token: 0x0400137B RID: 4987
		internal BinaryAssemblyInfo assemblyInfo;
	}
}
