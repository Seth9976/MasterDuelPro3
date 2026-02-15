using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004F7 RID: 1271
	internal sealed class __BinaryWriter
	{
		// Token: 0x060027C3 RID: 10179 RVA: 0x000A0814 File Offset: 0x0009EA14
		internal __BinaryWriter(Stream sout, ObjectWriter objectWriter, FormatterTypeStyle formatterTypeStyle)
		{
			this.sout = sout;
			this.formatterTypeStyle = formatterTypeStyle;
			this.objectWriter = objectWriter;
			this.m_nestedObjectCount = 0;
			this.dataWriter = new BinaryWriter(sout, Encoding.UTF8);
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x00002C89 File Offset: 0x00000E89
		internal void WriteBegin()
		{
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000A0854 File Offset: 0x0009EA54
		internal void WriteEnd()
		{
			this.dataWriter.Flush();
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000A0861 File Offset: 0x0009EA61
		internal void WriteBoolean(bool value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000A086F File Offset: 0x0009EA6F
		internal void WriteByte(byte value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000A087D File Offset: 0x0009EA7D
		private void WriteBytes(byte[] value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000A088B File Offset: 0x0009EA8B
		private void WriteBytes(byte[] byteA, int offset, int size)
		{
			this.dataWriter.Write(byteA, offset, size);
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000A089B File Offset: 0x0009EA9B
		internal void WriteChar(char value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000A08A9 File Offset: 0x0009EAA9
		internal void WriteChars(char[] value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000A08B7 File Offset: 0x0009EAB7
		internal void WriteDecimal(decimal value)
		{
			this.WriteString(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000A08CB File Offset: 0x0009EACB
		internal void WriteSingle(float value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000A08D9 File Offset: 0x0009EAD9
		internal void WriteDouble(double value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000A08E7 File Offset: 0x0009EAE7
		internal void WriteInt16(short value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000A08F5 File Offset: 0x0009EAF5
		internal void WriteInt32(int value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000A0903 File Offset: 0x0009EB03
		internal void WriteInt64(long value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000A0911 File Offset: 0x0009EB11
		internal void WriteSByte(sbyte value)
		{
			this.WriteByte((byte)value);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000A091B File Offset: 0x0009EB1B
		internal void WriteString(string value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000A0929 File Offset: 0x0009EB29
		internal void WriteTimeSpan(TimeSpan value)
		{
			this.WriteInt64(value.Ticks);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000A0938 File Offset: 0x0009EB38
		internal void WriteDateTime(DateTime value)
		{
			this.WriteInt64(value.ToBinaryRaw());
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000A0947 File Offset: 0x0009EB47
		internal void WriteUInt16(ushort value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000A0955 File Offset: 0x0009EB55
		internal void WriteUInt32(uint value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000A0963 File Offset: 0x0009EB63
		internal void WriteUInt64(ulong value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x00002C89 File Offset: 0x00000E89
		internal void WriteObjectEnd(NameInfo memberNameInfo, NameInfo typeNameInfo)
		{
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000A0971 File Offset: 0x0009EB71
		internal void WriteSerializationHeaderEnd()
		{
			MessageEnd messageEnd = new MessageEnd();
			messageEnd.Dump(this.sout);
			messageEnd.Write(this);
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000A098A File Offset: 0x0009EB8A
		internal void WriteSerializationHeader(int topId, int headerId, int minorVersion, int majorVersion)
		{
			SerializationHeaderRecord serializationHeaderRecord = new SerializationHeaderRecord(BinaryHeaderEnum.SerializedStreamHeader, topId, headerId, minorVersion, majorVersion);
			serializationHeaderRecord.Dump();
			serializationHeaderRecord.Write(this);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000A09A3 File Offset: 0x0009EBA3
		internal void WriteMethodCall()
		{
			if (this.binaryMethodCall == null)
			{
				this.binaryMethodCall = new BinaryMethodCall();
			}
			this.binaryMethodCall.Dump();
			this.binaryMethodCall.Write(this);
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000A09D0 File Offset: 0x0009EBD0
		internal object[] WriteCallArray(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, object callContext, object[] properties)
		{
			if (this.binaryMethodCall == null)
			{
				this.binaryMethodCall = new BinaryMethodCall();
			}
			return this.binaryMethodCall.WriteArray(uri, methodName, typeName, instArgs, args, methodSignature, callContext, properties);
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000A0A08 File Offset: 0x0009EC08
		internal void WriteMethodReturn()
		{
			if (this.binaryMethodReturn == null)
			{
				this.binaryMethodReturn = new BinaryMethodReturn();
			}
			this.binaryMethodReturn.Dump();
			this.binaryMethodReturn.Write(this);
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000A0A34 File Offset: 0x0009EC34
		internal object[] WriteReturnArray(object returnValue, object[] args, Exception exception, object callContext, object[] properties)
		{
			if (this.binaryMethodReturn == null)
			{
				this.binaryMethodReturn = new BinaryMethodReturn();
			}
			return this.binaryMethodReturn.WriteArray(returnValue, args, exception, callContext, properties);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000A0A5C File Offset: 0x0009EC5C
		internal void WriteObject(NameInfo nameInfo, NameInfo typeNameInfo, int numMembers, string[] memberNames, Type[] memberTypes, WriteObjectInfo[] memberObjectInfos)
		{
			this.InternalWriteItemNull();
			int num = (int)nameInfo.NIobjectId;
			string text;
			if (num < 0)
			{
				text = typeNameInfo.NIname;
			}
			else
			{
				text = nameInfo.NIname;
			}
			if (this.objectMapTable == null)
			{
				this.objectMapTable = new Hashtable();
			}
			ObjectMapInfo objectMapInfo = (ObjectMapInfo)this.objectMapTable[text];
			if (objectMapInfo != null && objectMapInfo.isCompatible(numMembers, memberNames, memberTypes))
			{
				if (this.binaryObject == null)
				{
					this.binaryObject = new BinaryObject();
				}
				this.binaryObject.Set(num, objectMapInfo.objectId);
				this.binaryObject.Write(this);
				return;
			}
			if (!typeNameInfo.NItransmitTypeOnObject)
			{
				if (this.binaryObjectWithMap == null)
				{
					this.binaryObjectWithMap = new BinaryObjectWithMap();
				}
				int num2 = (int)typeNameInfo.NIassemId;
				this.binaryObjectWithMap.Set(num, text, numMembers, memberNames, num2);
				this.binaryObjectWithMap.Dump();
				this.binaryObjectWithMap.Write(this);
				if (objectMapInfo == null)
				{
					this.objectMapTable.Add(text, new ObjectMapInfo(num, numMembers, memberNames, memberTypes));
					return;
				}
			}
			else
			{
				BinaryTypeEnum[] array = new BinaryTypeEnum[numMembers];
				object[] array2 = new object[numMembers];
				int[] array3 = new int[numMembers];
				int num2;
				for (int i = 0; i < numMembers; i++)
				{
					object obj = null;
					array[i] = BinaryConverter.GetBinaryTypeInfo(memberTypes[i], memberObjectInfos[i], null, this.objectWriter, out obj, out num2);
					array2[i] = obj;
					array3[i] = num2;
				}
				if (this.binaryObjectWithMapTyped == null)
				{
					this.binaryObjectWithMapTyped = new BinaryObjectWithMapTyped();
				}
				num2 = (int)typeNameInfo.NIassemId;
				this.binaryObjectWithMapTyped.Set(num, text, numMembers, memberNames, array, array2, array3, num2);
				this.binaryObjectWithMapTyped.Write(this);
				if (objectMapInfo == null)
				{
					this.objectMapTable.Add(text, new ObjectMapInfo(num, numMembers, memberNames, memberTypes));
				}
			}
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000A0C10 File Offset: 0x0009EE10
		internal void WriteObjectString(int objectId, string value)
		{
			this.InternalWriteItemNull();
			if (this.binaryObjectString == null)
			{
				this.binaryObjectString = new BinaryObjectString();
			}
			this.binaryObjectString.Set(objectId, value);
			this.binaryObjectString.Write(this);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000A0C44 File Offset: 0x0009EE44
		internal void WriteSingleArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound, Array array)
		{
			this.InternalWriteItemNull();
			int[] array2 = new int[] { length };
			int[] array3 = null;
			object obj = null;
			BinaryArrayTypeEnum binaryArrayTypeEnum;
			if (lowerBound == 0)
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
			}
			else
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.SingleOffset;
				array3 = new int[] { lowerBound };
			}
			int num;
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out obj, out num);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, 1, array2, array3, binaryTypeInfo, obj, binaryArrayTypeEnum, num);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
			if (Converter.IsWriteAsByteArray(arrayElemTypeNameInfo.NIprimitiveTypeEnum) && lowerBound == 0)
			{
				if (arrayElemTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Byte)
				{
					this.WriteBytes((byte[])array);
					return;
				}
				if (arrayElemTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Char)
				{
					this.WriteChars((char[])array);
					return;
				}
				this.WriteArrayAsBytes(array, Converter.TypeLength(arrayElemTypeNameInfo.NIprimitiveTypeEnum));
			}
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000A0D38 File Offset: 0x0009EF38
		private void WriteArrayAsBytes(Array array, int typeLength)
		{
			this.InternalWriteItemNull();
			int i = 0;
			if (this.byteBuffer == null)
			{
				this.byteBuffer = new byte[this.chunkSize];
			}
			while (i < array.Length)
			{
				int num = Math.Min(this.chunkSize / typeLength, array.Length - i);
				int num2 = num * typeLength;
				Buffer.InternalBlockCopy(array, i * typeLength, this.byteBuffer, 0, num2);
				if (!BitConverter.IsLittleEndian)
				{
					for (int j = 0; j < num2; j += typeLength)
					{
						for (int k = 0; k < typeLength / 2; k++)
						{
							byte b = this.byteBuffer[j + k];
							this.byteBuffer[j + k] = this.byteBuffer[j + typeLength - 1 - k];
							this.byteBuffer[j + typeLength - 1 - k] = b;
						}
					}
				}
				this.WriteBytes(this.byteBuffer, 0, num2);
				i += num;
			}
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000A0E18 File Offset: 0x0009F018
		internal void WriteJaggedArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound)
		{
			this.InternalWriteItemNull();
			int[] array = new int[] { length };
			int[] array2 = null;
			object obj = null;
			int num = 0;
			BinaryArrayTypeEnum binaryArrayTypeEnum;
			if (lowerBound == 0)
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.Jagged;
			}
			else
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.JaggedOffset;
				array2 = new int[] { lowerBound };
			}
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out obj, out num);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, 1, array, array2, binaryTypeInfo, obj, binaryArrayTypeEnum, num);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000A0EB8 File Offset: 0x0009F0B8
		internal void WriteRectangleArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int rank, int[] lengthA, int[] lowerBoundA)
		{
			this.InternalWriteItemNull();
			BinaryArrayTypeEnum binaryArrayTypeEnum = BinaryArrayTypeEnum.Rectangular;
			object obj = null;
			int num = 0;
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out obj, out num);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			for (int i = 0; i < rank; i++)
			{
				if (lowerBoundA[i] != 0)
				{
					binaryArrayTypeEnum = BinaryArrayTypeEnum.RectangularOffset;
					break;
				}
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, rank, lengthA, lowerBoundA, binaryTypeInfo, obj, binaryArrayTypeEnum, num);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000A0F51 File Offset: 0x0009F151
		internal void WriteObjectByteArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound, byte[] byteA)
		{
			this.InternalWriteItemNull();
			this.WriteSingleArray(memberNameInfo, arrayNameInfo, objectInfo, arrayElemTypeNameInfo, length, lowerBound, byteA);
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000A0F6C File Offset: 0x0009F16C
		internal void WriteMember(NameInfo memberNameInfo, NameInfo typeNameInfo, object value)
		{
			this.InternalWriteItemNull();
			InternalPrimitiveTypeE niprimitiveTypeEnum = typeNameInfo.NIprimitiveTypeEnum;
			if (memberNameInfo.NItransmitTypeOnMember)
			{
				if (this.memberPrimitiveTyped == null)
				{
					this.memberPrimitiveTyped = new MemberPrimitiveTyped();
				}
				this.memberPrimitiveTyped.Set(niprimitiveTypeEnum, value);
				bool niisArrayItem = memberNameInfo.NIisArrayItem;
				this.memberPrimitiveTyped.Dump();
				this.memberPrimitiveTyped.Write(this);
				return;
			}
			if (this.memberPrimitiveUnTyped == null)
			{
				this.memberPrimitiveUnTyped = new MemberPrimitiveUnTyped();
			}
			this.memberPrimitiveUnTyped.Set(niprimitiveTypeEnum, value);
			bool niisArrayItem2 = memberNameInfo.NIisArrayItem;
			this.memberPrimitiveUnTyped.Dump();
			this.memberPrimitiveUnTyped.Write(this);
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000A100C File Offset: 0x0009F20C
		internal void WriteNullMember(NameInfo memberNameInfo, NameInfo typeNameInfo)
		{
			this.InternalWriteItemNull();
			if (this.objectNull == null)
			{
				this.objectNull = new ObjectNull();
			}
			if (!memberNameInfo.NIisArrayItem)
			{
				this.objectNull.SetNullCount(1);
				this.objectNull.Dump();
				this.objectNull.Write(this);
				this.nullCount = 0;
			}
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000A1064 File Offset: 0x0009F264
		internal void WriteMemberObjectRef(NameInfo memberNameInfo, int idRef)
		{
			this.InternalWriteItemNull();
			if (this.memberReference == null)
			{
				this.memberReference = new MemberReference();
			}
			this.memberReference.Set(idRef);
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
			this.memberReference.Dump();
			this.memberReference.Write(this);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000A10B4 File Offset: 0x0009F2B4
		internal void WriteMemberNested(NameInfo memberNameInfo)
		{
			this.InternalWriteItemNull();
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000A10C3 File Offset: 0x0009F2C3
		internal void WriteMemberString(NameInfo memberNameInfo, NameInfo typeNameInfo, string value)
		{
			this.InternalWriteItemNull();
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
			this.WriteObjectString((int)typeNameInfo.NIobjectId, value);
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000A10E0 File Offset: 0x0009F2E0
		internal void WriteItem(NameInfo itemNameInfo, NameInfo typeNameInfo, object value)
		{
			this.InternalWriteItemNull();
			this.WriteMember(itemNameInfo, typeNameInfo, value);
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000A10F1 File Offset: 0x0009F2F1
		internal void WriteNullItem(NameInfo itemNameInfo, NameInfo typeNameInfo)
		{
			this.nullCount++;
			this.InternalWriteItemNull();
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000A1107 File Offset: 0x0009F307
		internal void WriteDelayedNullItem()
		{
			this.nullCount++;
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000A1117 File Offset: 0x0009F317
		internal void WriteItemEnd()
		{
			this.InternalWriteItemNull();
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000A1120 File Offset: 0x0009F320
		private void InternalWriteItemNull()
		{
			if (this.nullCount > 0)
			{
				if (this.objectNull == null)
				{
					this.objectNull = new ObjectNull();
				}
				this.objectNull.SetNullCount(this.nullCount);
				this.objectNull.Dump();
				this.objectNull.Write(this);
				this.nullCount = 0;
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000A1178 File Offset: 0x0009F378
		internal void WriteItemObjectRef(NameInfo nameInfo, int idRef)
		{
			this.InternalWriteItemNull();
			this.WriteMemberObjectRef(nameInfo, idRef);
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000A1188 File Offset: 0x0009F388
		internal void WriteAssembly(Type type, string assemblyString, int assemId, bool isNew)
		{
			this.InternalWriteItemNull();
			if (assemblyString == null)
			{
				assemblyString = string.Empty;
			}
			if (isNew)
			{
				if (this.binaryAssembly == null)
				{
					this.binaryAssembly = new BinaryAssembly();
				}
				this.binaryAssembly.Set(assemId, assemblyString);
				this.binaryAssembly.Dump();
				this.binaryAssembly.Write(this);
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000A11E0 File Offset: 0x0009F3E0
		internal void WriteValue(InternalPrimitiveTypeE code, object value)
		{
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.WriteBoolean(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Byte:
				this.WriteByte(Convert.ToByte(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Char:
				this.WriteChar(Convert.ToChar(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Decimal:
				this.WriteDecimal(Convert.ToDecimal(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Double:
				this.WriteDouble(Convert.ToDouble(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int16:
				this.WriteInt16(Convert.ToInt16(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int32:
				this.WriteInt32(Convert.ToInt32(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int64:
				this.WriteInt64(Convert.ToInt64(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.SByte:
				this.WriteSByte(Convert.ToSByte(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Single:
				this.WriteSingle(Convert.ToSingle(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.TimeSpan:
				this.WriteTimeSpan((TimeSpan)value);
				return;
			case InternalPrimitiveTypeE.DateTime:
				this.WriteDateTime((DateTime)value);
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.WriteUInt16(Convert.ToUInt16(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.WriteUInt32(Convert.ToUInt32(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.WriteUInt64(Convert.ToUInt64(value, CultureInfo.InvariantCulture));
				return;
			}
			throw new SerializationException(Environment.GetResourceString("Invalid type code in stream '{0}'.", new object[] { code.ToString() }));
		}

		// Token: 0x040013C7 RID: 5063
		internal Stream sout;

		// Token: 0x040013C8 RID: 5064
		internal FormatterTypeStyle formatterTypeStyle;

		// Token: 0x040013C9 RID: 5065
		internal Hashtable objectMapTable;

		// Token: 0x040013CA RID: 5066
		internal ObjectWriter objectWriter;

		// Token: 0x040013CB RID: 5067
		internal BinaryWriter dataWriter;

		// Token: 0x040013CC RID: 5068
		internal int m_nestedObjectCount;

		// Token: 0x040013CD RID: 5069
		private int nullCount;

		// Token: 0x040013CE RID: 5070
		internal BinaryMethodCall binaryMethodCall;

		// Token: 0x040013CF RID: 5071
		internal BinaryMethodReturn binaryMethodReturn;

		// Token: 0x040013D0 RID: 5072
		internal BinaryObject binaryObject;

		// Token: 0x040013D1 RID: 5073
		internal BinaryObjectWithMap binaryObjectWithMap;

		// Token: 0x040013D2 RID: 5074
		internal BinaryObjectWithMapTyped binaryObjectWithMapTyped;

		// Token: 0x040013D3 RID: 5075
		internal BinaryObjectString binaryObjectString;

		// Token: 0x040013D4 RID: 5076
		internal BinaryArray binaryArray;

		// Token: 0x040013D5 RID: 5077
		private byte[] byteBuffer;

		// Token: 0x040013D6 RID: 5078
		private int chunkSize = 4096;

		// Token: 0x040013D7 RID: 5079
		internal MemberPrimitiveUnTyped memberPrimitiveUnTyped;

		// Token: 0x040013D8 RID: 5080
		internal MemberPrimitiveTyped memberPrimitiveTyped;

		// Token: 0x040013D9 RID: 5081
		internal ObjectNull objectNull;

		// Token: 0x040013DA RID: 5082
		internal MemberReference memberReference;

		// Token: 0x040013DB RID: 5083
		internal BinaryAssembly binaryAssembly;
	}
}
