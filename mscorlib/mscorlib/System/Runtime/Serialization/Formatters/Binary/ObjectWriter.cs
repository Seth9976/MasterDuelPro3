using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000503 RID: 1283
	internal sealed class ObjectWriter
	{
		// Token: 0x0600285C RID: 10332 RVA: 0x000A3DB0 File Offset: 0x000A1FB0
		internal ObjectWriter(ISurrogateSelector selector, StreamingContext context, InternalFE formatterEnums, SerializationBinder binder)
		{
			this.m_currentId = 1;
			this.m_surrogates = selector;
			this.m_context = context;
			this.m_binder = binder;
			this.formatterEnums = formatterEnums;
			this.m_objectManager = new SerializationObjectManager(context);
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000A3E04 File Offset: 0x000A2004
		internal void Serialize(object graph, Header[] inHeaders, __BinaryWriter serWriter, bool fCheck)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph", Environment.GetResourceString("Object Graph cannot be null."));
			}
			if (serWriter == null)
			{
				throw new ArgumentNullException("serWriter", Environment.GetResourceString("Parameter '{0}' cannot be null.", new object[] { "serWriter" }));
			}
			this.serWriter = serWriter;
			this.headers = inHeaders;
			serWriter.WriteBegin();
			long num = 0L;
			bool flag = false;
			bool flag2 = false;
			IMethodCallMessage methodCallMessage = graph as IMethodCallMessage;
			if (methodCallMessage != null)
			{
				flag = true;
				graph = this.WriteMethodCall(methodCallMessage);
			}
			else
			{
				IMethodReturnMessage methodReturnMessage = graph as IMethodReturnMessage;
				if (methodReturnMessage != null)
				{
					flag2 = true;
					graph = this.WriteMethodReturn(methodReturnMessage);
				}
			}
			if (graph == null)
			{
				this.WriteSerializedStreamHeader(this.topId, num);
				if (flag)
				{
					serWriter.WriteMethodCall();
				}
				else if (flag2)
				{
					serWriter.WriteMethodReturn();
				}
				serWriter.WriteSerializationHeaderEnd();
				serWriter.WriteEnd();
				return;
			}
			this.m_idGenerator = new ObjectIDGenerator();
			this.m_objectQueue = new Queue();
			this.m_formatterConverter = new FormatterConverter();
			this.serObjectInfoInit = new SerObjectInfoInit();
			bool flag3;
			this.topId = this.InternalGetId(graph, false, null, out flag3);
			if (this.headers != null)
			{
				num = this.InternalGetId(this.headers, false, null, out flag3);
			}
			else
			{
				num = -1L;
			}
			this.WriteSerializedStreamHeader(this.topId, num);
			if (flag)
			{
				serWriter.WriteMethodCall();
			}
			else if (flag2)
			{
				serWriter.WriteMethodReturn();
			}
			if (this.headers != null && this.headers.Length != 0)
			{
				this.m_objectQueue.Enqueue(this.headers);
			}
			if (graph != null)
			{
				this.m_objectQueue.Enqueue(graph);
			}
			long num2;
			object next;
			while ((next = this.GetNext(out num2)) != null)
			{
				WriteObjectInfo writeObjectInfo;
				if (next is WriteObjectInfo)
				{
					writeObjectInfo = (WriteObjectInfo)next;
				}
				else
				{
					writeObjectInfo = WriteObjectInfo.Serialize(next, this.m_surrogates, this.m_context, this.serObjectInfoInit, this.m_formatterConverter, this, this.m_binder);
					writeObjectInfo.assemId = this.GetAssemblyId(writeObjectInfo);
				}
				writeObjectInfo.objectId = num2;
				NameInfo nameInfo = this.TypeToNameInfo(writeObjectInfo);
				this.Write(writeObjectInfo, nameInfo, nameInfo);
				this.PutNameInfo(nameInfo);
				writeObjectInfo.ObjectEnd();
			}
			serWriter.WriteSerializationHeaderEnd();
			serWriter.WriteEnd();
			this.m_objectManager.RaiseOnSerializedEvent();
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000A4028 File Offset: 0x000A2228
		private object[] WriteMethodCall(IMethodCallMessage mcm)
		{
			string uri = mcm.Uri;
			string methodName = mcm.MethodName;
			string typeName = mcm.TypeName;
			object obj = null;
			object[] array = null;
			Type[] array2 = null;
			if (mcm.MethodBase.IsGenericMethod)
			{
				array2 = mcm.MethodBase.GetGenericArguments();
			}
			object[] args = mcm.Args;
			IInternalMessage internalMessage = mcm as IInternalMessage;
			if (internalMessage == null || internalMessage.HasProperties())
			{
				array = ObjectWriter.StoreUserPropertiesForMethodMessage(mcm);
			}
			if (mcm.MethodSignature != null && RemotingServices.IsMethodOverloaded(mcm))
			{
				obj = mcm.MethodSignature;
			}
			LogicalCallContext logicalCallContext = mcm.LogicalCallContext;
			object obj2;
			if (logicalCallContext == null)
			{
				obj2 = null;
			}
			else if (logicalCallContext.HasInfo)
			{
				obj2 = logicalCallContext;
			}
			else
			{
				obj2 = logicalCallContext.RemotingData.LogicalCallID;
			}
			return this.serWriter.WriteCallArray(uri, methodName, typeName, array2, args, obj, obj2, array);
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000A40F0 File Offset: 0x000A22F0
		private object[] WriteMethodReturn(IMethodReturnMessage mrm)
		{
			object returnValue = mrm.ReturnValue;
			object[] args = mrm.Args;
			Exception exception = mrm.Exception;
			object[] array = null;
			ReturnMessage returnMessage = mrm as ReturnMessage;
			if (returnMessage == null || returnMessage.HasProperties())
			{
				array = ObjectWriter.StoreUserPropertiesForMethodMessage(mrm);
			}
			LogicalCallContext logicalCallContext = mrm.LogicalCallContext;
			object obj;
			if (logicalCallContext == null)
			{
				obj = null;
			}
			else if (logicalCallContext.HasInfo)
			{
				obj = logicalCallContext;
			}
			else
			{
				obj = logicalCallContext.RemotingData.LogicalCallID;
			}
			return this.serWriter.WriteReturnArray(returnValue, args, exception, obj, array);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000A4170 File Offset: 0x000A2370
		private static object[] StoreUserPropertiesForMethodMessage(IMethodMessage msg)
		{
			ArrayList arrayList = null;
			IDictionary properties = msg.Properties;
			if (properties == null)
			{
				return null;
			}
			MessageDictionary messageDictionary = properties as MessageDictionary;
			if (messageDictionary != null)
			{
				if (messageDictionary.HasUserData())
				{
					int num = 0;
					foreach (object obj in messageDictionary.InternalDictionary)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(dictionaryEntry);
						num++;
					}
					return arrayList.ToArray();
				}
				return null;
			}
			else
			{
				int num2 = 0;
				foreach (object obj2 in properties)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(dictionaryEntry2);
					num2++;
				}
				if (arrayList != null)
				{
					return arrayList.ToArray();
				}
				return null;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x000A4280 File Offset: 0x000A2480
		internal SerializationObjectManager ObjectManager
		{
			get
			{
				return this.m_objectManager;
			}
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000A4288 File Offset: 0x000A2488
		private void Write(WriteObjectInfo objectInfo, NameInfo memberNameInfo, NameInfo typeNameInfo)
		{
			object obj = objectInfo.obj;
			if (obj == null)
			{
				throw new ArgumentNullException("objectInfo.obj", Environment.GetResourceString("Object cannot be null."));
			}
			Type objectType = objectInfo.objectType;
			long objectId = objectInfo.objectId;
			if (objectType == Converter.typeofString)
			{
				memberNameInfo.NIobjectId = objectId;
				this.serWriter.WriteObjectString((int)objectId, obj.ToString());
				return;
			}
			if (objectInfo.isArray)
			{
				this.WriteArray(objectInfo, memberNameInfo, null);
				return;
			}
			string[] array;
			Type[] array2;
			object[] array3;
			objectInfo.GetMemberInfo(out array, out array2, out array3);
			if (objectInfo.isSi || this.CheckTypeFormat(this.formatterEnums.FEtypeFormat, FormatterTypeStyle.TypesAlways))
			{
				memberNameInfo.NItransmitTypeOnObject = true;
				memberNameInfo.NIisParentTypeOnObject = true;
				typeNameInfo.NItransmitTypeOnObject = true;
				typeNameInfo.NIisParentTypeOnObject = true;
			}
			WriteObjectInfo[] array4 = new WriteObjectInfo[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				Type type;
				if (array2[i] != null)
				{
					type = array2[i];
				}
				else if (array3[i] != null)
				{
					type = this.GetType(array3[i]);
				}
				else
				{
					type = Converter.typeofObject;
				}
				if (this.ToCode(type) == InternalPrimitiveTypeE.Invalid && type != Converter.typeofString)
				{
					if (array3[i] != null)
					{
						array4[i] = WriteObjectInfo.Serialize(array3[i], this.m_surrogates, this.m_context, this.serObjectInfoInit, this.m_formatterConverter, this, this.m_binder);
						array4[i].assemId = this.GetAssemblyId(array4[i]);
					}
					else
					{
						array4[i] = WriteObjectInfo.Serialize(array2[i], this.m_surrogates, this.m_context, this.serObjectInfoInit, this.m_formatterConverter, this.m_binder);
						array4[i].assemId = this.GetAssemblyId(array4[i]);
					}
				}
			}
			this.Write(objectInfo, memberNameInfo, typeNameInfo, array, array2, array3, array4);
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000A443C File Offset: 0x000A263C
		private void Write(WriteObjectInfo objectInfo, NameInfo memberNameInfo, NameInfo typeNameInfo, string[] memberNames, Type[] memberTypes, object[] memberData, WriteObjectInfo[] memberObjectInfos)
		{
			int num = memberNames.Length;
			NameInfo nameInfo = null;
			if (memberNameInfo != null)
			{
				memberNameInfo.NIobjectId = objectInfo.objectId;
				this.serWriter.WriteObject(memberNameInfo, typeNameInfo, num, memberNames, memberTypes, memberObjectInfos);
			}
			else if (objectInfo.objectId == this.topId && this.topName != null)
			{
				nameInfo = this.MemberToNameInfo(this.topName);
				nameInfo.NIobjectId = objectInfo.objectId;
				this.serWriter.WriteObject(nameInfo, typeNameInfo, num, memberNames, memberTypes, memberObjectInfos);
			}
			else if (objectInfo.objectType != Converter.typeofString)
			{
				typeNameInfo.NIobjectId = objectInfo.objectId;
				this.serWriter.WriteObject(typeNameInfo, null, num, memberNames, memberTypes, memberObjectInfos);
			}
			if (memberNameInfo.NIisParentTypeOnObject)
			{
				memberNameInfo.NItransmitTypeOnObject = true;
				memberNameInfo.NIisParentTypeOnObject = false;
			}
			else
			{
				memberNameInfo.NItransmitTypeOnObject = false;
			}
			for (int i = 0; i < num; i++)
			{
				this.WriteMemberSetup(objectInfo, memberNameInfo, typeNameInfo, memberNames[i], memberTypes[i], memberData[i], memberObjectInfos[i]);
			}
			if (memberNameInfo != null)
			{
				memberNameInfo.NIobjectId = objectInfo.objectId;
				this.serWriter.WriteObjectEnd(memberNameInfo, typeNameInfo);
				return;
			}
			if (objectInfo.objectId == this.topId && this.topName != null)
			{
				this.serWriter.WriteObjectEnd(nameInfo, typeNameInfo);
				this.PutNameInfo(nameInfo);
				return;
			}
			if (objectInfo.objectType != Converter.typeofString)
			{
				this.serWriter.WriteObjectEnd(typeNameInfo, typeNameInfo);
			}
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000A4590 File Offset: 0x000A2790
		private void WriteMemberSetup(WriteObjectInfo objectInfo, NameInfo memberNameInfo, NameInfo typeNameInfo, string memberName, Type memberType, object memberData, WriteObjectInfo memberObjectInfo)
		{
			NameInfo nameInfo = this.MemberToNameInfo(memberName);
			if (memberObjectInfo != null)
			{
				nameInfo.NIassemId = memberObjectInfo.assemId;
			}
			nameInfo.NItype = memberType;
			NameInfo nameInfo2;
			if (memberObjectInfo == null)
			{
				nameInfo2 = this.TypeToNameInfo(memberType);
			}
			else
			{
				nameInfo2 = this.TypeToNameInfo(memberObjectInfo);
			}
			nameInfo.NItransmitTypeOnObject = memberNameInfo.NItransmitTypeOnObject;
			nameInfo.NIisParentTypeOnObject = memberNameInfo.NIisParentTypeOnObject;
			this.WriteMembers(nameInfo, nameInfo2, memberData, objectInfo, typeNameInfo, memberObjectInfo);
			this.PutNameInfo(nameInfo);
			this.PutNameInfo(nameInfo2);
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000A4610 File Offset: 0x000A2810
		private void WriteMembers(NameInfo memberNameInfo, NameInfo memberTypeNameInfo, object memberData, WriteObjectInfo objectInfo, NameInfo typeNameInfo, WriteObjectInfo memberObjectInfo)
		{
			Type type = memberNameInfo.NItype;
			bool flag = false;
			if (type == Converter.typeofObject || Nullable.GetUnderlyingType(type) != null)
			{
				memberTypeNameInfo.NItransmitTypeOnMember = true;
				memberNameInfo.NItransmitTypeOnMember = true;
			}
			if (this.CheckTypeFormat(this.formatterEnums.FEtypeFormat, FormatterTypeStyle.TypesAlways) || objectInfo.isSi)
			{
				memberTypeNameInfo.NItransmitTypeOnObject = true;
				memberNameInfo.NItransmitTypeOnObject = true;
				memberNameInfo.NIisParentTypeOnObject = true;
			}
			if (this.CheckForNull(objectInfo, memberNameInfo, memberTypeNameInfo, memberData))
			{
				return;
			}
			Type type2 = null;
			if (memberTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Invalid)
			{
				type2 = this.GetType(memberData);
				if (type != type2)
				{
					memberTypeNameInfo.NItransmitTypeOnMember = true;
					memberNameInfo.NItransmitTypeOnMember = true;
				}
			}
			if (type == Converter.typeofObject)
			{
				flag = true;
				type = this.GetType(memberData);
				if (memberObjectInfo == null)
				{
					this.TypeToNameInfo(type, memberTypeNameInfo);
				}
				else
				{
					this.TypeToNameInfo(memberObjectInfo, memberTypeNameInfo);
				}
			}
			if (memberObjectInfo == null || !memberObjectInfo.isArray)
			{
				if (!this.WriteKnownValueClass(memberNameInfo, memberTypeNameInfo, memberData))
				{
					if (type2 == null)
					{
						type2 = this.GetType(memberData);
					}
					long num = this.Schedule(memberData, flag, type2, memberObjectInfo);
					if (num < 0L)
					{
						memberObjectInfo.objectId = num;
						NameInfo nameInfo = this.TypeToNameInfo(memberObjectInfo);
						nameInfo.NIobjectId = num;
						this.Write(memberObjectInfo, memberNameInfo, nameInfo);
						this.PutNameInfo(nameInfo);
						memberObjectInfo.ObjectEnd();
						return;
					}
					memberNameInfo.NIobjectId = num;
					this.WriteObjectRef(memberNameInfo, num);
				}
				return;
			}
			if (type2 == null)
			{
				type2 = this.GetType(memberData);
			}
			long num2 = this.Schedule(memberData, false, null, memberObjectInfo);
			if (num2 > 0L)
			{
				memberNameInfo.NIobjectId = num2;
				this.WriteObjectRef(memberNameInfo, num2);
				return;
			}
			this.serWriter.WriteMemberNested(memberNameInfo);
			memberObjectInfo.objectId = num2;
			memberNameInfo.NIobjectId = num2;
			this.WriteArray(memberObjectInfo, memberNameInfo, memberObjectInfo);
			objectInfo.ObjectEnd();
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000A47B8 File Offset: 0x000A29B8
		private void WriteArray(WriteObjectInfo objectInfo, NameInfo memberNameInfo, WriteObjectInfo memberObjectInfo)
		{
			bool flag = false;
			if (memberNameInfo == null)
			{
				memberNameInfo = this.TypeToNameInfo(objectInfo);
				flag = true;
			}
			memberNameInfo.NIisArray = true;
			long objectId = objectInfo.objectId;
			memberNameInfo.NIobjectId = objectInfo.objectId;
			Array array = (Array)objectInfo.obj;
			Type elementType = objectInfo.objectType.GetElementType();
			WriteObjectInfo writeObjectInfo = null;
			if (!elementType.IsPrimitive)
			{
				writeObjectInfo = WriteObjectInfo.Serialize(elementType, this.m_surrogates, this.m_context, this.serObjectInfoInit, this.m_formatterConverter, this.m_binder);
				writeObjectInfo.assemId = this.GetAssemblyId(writeObjectInfo);
			}
			NameInfo nameInfo;
			if (writeObjectInfo == null)
			{
				nameInfo = this.TypeToNameInfo(elementType);
			}
			else
			{
				nameInfo = this.TypeToNameInfo(writeObjectInfo);
			}
			nameInfo.NIisArray = nameInfo.NItype.IsArray;
			NameInfo nameInfo2 = memberNameInfo;
			nameInfo2.NIobjectId = objectId;
			nameInfo2.NIisArray = true;
			nameInfo.NIobjectId = objectId;
			nameInfo.NItransmitTypeOnMember = memberNameInfo.NItransmitTypeOnMember;
			nameInfo.NItransmitTypeOnObject = memberNameInfo.NItransmitTypeOnObject;
			nameInfo.NIisParentTypeOnObject = memberNameInfo.NIisParentTypeOnObject;
			int rank = array.Rank;
			int[] array2 = new int[rank];
			int[] array3 = new int[rank];
			int[] array4 = new int[rank];
			for (int i = 0; i < rank; i++)
			{
				array2[i] = array.GetLength(i);
				array3[i] = array.GetLowerBound(i);
				array4[i] = array.GetUpperBound(i);
			}
			InternalArrayTypeE internalArrayTypeE;
			if (nameInfo.NIisArray)
			{
				if (rank == 1)
				{
					internalArrayTypeE = InternalArrayTypeE.Jagged;
				}
				else
				{
					internalArrayTypeE = InternalArrayTypeE.Rectangular;
				}
			}
			else if (rank == 1)
			{
				internalArrayTypeE = InternalArrayTypeE.Single;
			}
			else
			{
				internalArrayTypeE = InternalArrayTypeE.Rectangular;
			}
			nameInfo.NIarrayEnum = internalArrayTypeE;
			if (elementType == Converter.typeofByte && rank == 1 && array3[0] == 0)
			{
				this.serWriter.WriteObjectByteArray(memberNameInfo, nameInfo2, writeObjectInfo, nameInfo, array2[0], array3[0], (byte[])array);
				return;
			}
			if (elementType == Converter.typeofObject || Nullable.GetUnderlyingType(elementType) != null)
			{
				memberNameInfo.NItransmitTypeOnMember = true;
				nameInfo.NItransmitTypeOnMember = true;
			}
			if (this.CheckTypeFormat(this.formatterEnums.FEtypeFormat, FormatterTypeStyle.TypesAlways))
			{
				memberNameInfo.NItransmitTypeOnObject = true;
				nameInfo.NItransmitTypeOnObject = true;
			}
			if (internalArrayTypeE == InternalArrayTypeE.Single)
			{
				this.serWriter.WriteSingleArray(memberNameInfo, nameInfo2, writeObjectInfo, nameInfo, array2[0], array3[0], array);
				if (!Converter.IsWriteAsByteArray(nameInfo.NIprimitiveTypeEnum) || array3[0] != 0)
				{
					object[] array5 = null;
					if (!elementType.IsValueType)
					{
						array5 = (object[])array;
					}
					int num = array4[0] + 1;
					for (int j = array3[0]; j < num; j++)
					{
						if (array5 == null)
						{
							this.WriteArrayMember(objectInfo, nameInfo, array.GetValue(j));
						}
						else
						{
							this.WriteArrayMember(objectInfo, nameInfo, array5[j]);
						}
					}
					this.serWriter.WriteItemEnd();
				}
			}
			else if (internalArrayTypeE == InternalArrayTypeE.Jagged)
			{
				nameInfo2.NIobjectId = objectId;
				this.serWriter.WriteJaggedArray(memberNameInfo, nameInfo2, writeObjectInfo, nameInfo, array2[0], array3[0]);
				object[] array6 = (object[])array;
				for (int k = array3[0]; k < array4[0] + 1; k++)
				{
					this.WriteArrayMember(objectInfo, nameInfo, array6[k]);
				}
				this.serWriter.WriteItemEnd();
			}
			else
			{
				nameInfo2.NIobjectId = objectId;
				this.serWriter.WriteRectangleArray(memberNameInfo, nameInfo2, writeObjectInfo, nameInfo, rank, array2, array3);
				bool flag2 = false;
				for (int l = 0; l < rank; l++)
				{
					if (array2[l] == 0)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					this.WriteRectangle(objectInfo, rank, array2, array, nameInfo, array3);
				}
				this.serWriter.WriteItemEnd();
			}
			this.serWriter.WriteObjectEnd(memberNameInfo, nameInfo2);
			this.PutNameInfo(nameInfo);
			if (flag)
			{
				this.PutNameInfo(memberNameInfo);
			}
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000A4B38 File Offset: 0x000A2D38
		private void WriteArrayMember(WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, object data)
		{
			arrayElemTypeNameInfo.NIisArrayItem = true;
			if (this.CheckForNull(objectInfo, arrayElemTypeNameInfo, arrayElemTypeNameInfo, data))
			{
				return;
			}
			Type type = null;
			bool flag = false;
			if (arrayElemTypeNameInfo.NItransmitTypeOnMember)
			{
				flag = true;
			}
			if (!flag && !arrayElemTypeNameInfo.IsSealed)
			{
				type = this.GetType(data);
				if (arrayElemTypeNameInfo.NItype != type)
				{
					flag = true;
				}
			}
			NameInfo nameInfo;
			if (flag)
			{
				if (type == null)
				{
					type = this.GetType(data);
				}
				nameInfo = this.TypeToNameInfo(type);
				nameInfo.NItransmitTypeOnMember = true;
				nameInfo.NIobjectId = arrayElemTypeNameInfo.NIobjectId;
				nameInfo.NIassemId = arrayElemTypeNameInfo.NIassemId;
				nameInfo.NIisArrayItem = true;
			}
			else
			{
				nameInfo = arrayElemTypeNameInfo;
				nameInfo.NIisArrayItem = true;
			}
			if (!this.WriteKnownValueClass(arrayElemTypeNameInfo, nameInfo, data))
			{
				bool flag2 = false;
				if (arrayElemTypeNameInfo.NItype == Converter.typeofObject)
				{
					flag2 = true;
				}
				long num = this.Schedule(data, flag2, nameInfo.NItype);
				arrayElemTypeNameInfo.NIobjectId = num;
				nameInfo.NIobjectId = num;
				if (num < 1L)
				{
					WriteObjectInfo writeObjectInfo = WriteObjectInfo.Serialize(data, this.m_surrogates, this.m_context, this.serObjectInfoInit, this.m_formatterConverter, this, this.m_binder);
					writeObjectInfo.objectId = num;
					if (arrayElemTypeNameInfo.NItype != Converter.typeofObject && Nullable.GetUnderlyingType(arrayElemTypeNameInfo.NItype) == null)
					{
						writeObjectInfo.assemId = nameInfo.NIassemId;
					}
					else
					{
						writeObjectInfo.assemId = this.GetAssemblyId(writeObjectInfo);
					}
					NameInfo nameInfo2 = this.TypeToNameInfo(writeObjectInfo);
					nameInfo2.NIobjectId = num;
					writeObjectInfo.objectId = num;
					this.Write(writeObjectInfo, nameInfo, nameInfo2);
					writeObjectInfo.ObjectEnd();
				}
				else
				{
					this.serWriter.WriteItemObjectRef(arrayElemTypeNameInfo, (int)num);
				}
			}
			if (arrayElemTypeNameInfo.NItransmitTypeOnMember)
			{
				this.PutNameInfo(nameInfo);
			}
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000A4CD4 File Offset: 0x000A2ED4
		private void WriteRectangle(WriteObjectInfo objectInfo, int rank, int[] maxA, Array array, NameInfo arrayElemNameTypeInfo, int[] lowerBoundA)
		{
			int[] array2 = new int[rank];
			int[] array3 = null;
			bool flag = false;
			if (lowerBoundA != null)
			{
				for (int i = 0; i < rank; i++)
				{
					if (lowerBoundA[i] != 0)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				array3 = new int[rank];
			}
			bool flag2 = true;
			while (flag2)
			{
				flag2 = false;
				if (flag)
				{
					for (int j = 0; j < rank; j++)
					{
						array3[j] = array2[j] + lowerBoundA[j];
					}
					this.WriteArrayMember(objectInfo, arrayElemNameTypeInfo, array.GetValue(array3));
				}
				else
				{
					this.WriteArrayMember(objectInfo, arrayElemNameTypeInfo, array.GetValue(array2));
				}
				for (int k = rank - 1; k > -1; k--)
				{
					if (array2[k] < maxA[k] - 1)
					{
						array2[k]++;
						if (k < rank - 1)
						{
							for (int l = k + 1; l < rank; l++)
							{
								array2[l] = 0;
							}
						}
						flag2 = true;
						break;
					}
				}
			}
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000A4DB8 File Offset: 0x000A2FB8
		private object GetNext(out long objID)
		{
			if (this.m_objectQueue.Count == 0)
			{
				objID = 0L;
				return null;
			}
			object obj = this.m_objectQueue.Dequeue();
			object obj2;
			if (obj is WriteObjectInfo)
			{
				obj2 = ((WriteObjectInfo)obj).obj;
			}
			else
			{
				obj2 = obj;
			}
			bool flag;
			objID = this.m_idGenerator.HasId(obj2, out flag);
			if (flag)
			{
				throw new SerializationException(Environment.GetResourceString("Object {0} has never been assigned an objectID.", new object[] { obj2 }));
			}
			return obj;
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000A4E2C File Offset: 0x000A302C
		private long InternalGetId(object obj, bool assignUniqueIdToValueType, Type type, out bool isNew)
		{
			if (obj == this.previousObj)
			{
				isNew = false;
				return this.previousId;
			}
			this.m_idGenerator.m_currentCount = this.m_currentId;
			if (type != null && type.IsValueType && !assignUniqueIdToValueType)
			{
				isNew = false;
				int num = -1;
				int currentId = this.m_currentId;
				this.m_currentId = currentId + 1;
				return (long)(num * currentId);
			}
			this.m_currentId++;
			long id = this.m_idGenerator.GetId(obj, out isNew);
			this.previousObj = obj;
			this.previousId = id;
			return id;
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000A4EB1 File Offset: 0x000A30B1
		private long Schedule(object obj, bool assignUniqueIdToValueType, Type type)
		{
			return this.Schedule(obj, assignUniqueIdToValueType, type, null);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000A4EC0 File Offset: 0x000A30C0
		private long Schedule(object obj, bool assignUniqueIdToValueType, Type type, WriteObjectInfo objectInfo)
		{
			if (obj == null)
			{
				return 0L;
			}
			bool flag;
			long num = this.InternalGetId(obj, assignUniqueIdToValueType, type, out flag);
			if (flag && num > 0L)
			{
				if (objectInfo == null)
				{
					this.m_objectQueue.Enqueue(obj);
				}
				else
				{
					this.m_objectQueue.Enqueue(objectInfo);
				}
			}
			return num;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000A4F08 File Offset: 0x000A3108
		private bool WriteKnownValueClass(NameInfo memberNameInfo, NameInfo typeNameInfo, object data)
		{
			if (typeNameInfo.NItype == Converter.typeofString)
			{
				this.WriteString(memberNameInfo, typeNameInfo, data);
			}
			else
			{
				if (typeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Invalid)
				{
					return false;
				}
				if (typeNameInfo.NIisArray)
				{
					this.serWriter.WriteItem(memberNameInfo, typeNameInfo, data);
				}
				else
				{
					this.serWriter.WriteMember(memberNameInfo, typeNameInfo, data);
				}
			}
			return true;
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000A4F5E File Offset: 0x000A315E
		private void WriteObjectRef(NameInfo nameInfo, long objectId)
		{
			this.serWriter.WriteMemberObjectRef(nameInfo, (int)objectId);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000A4F70 File Offset: 0x000A3170
		private void WriteString(NameInfo memberNameInfo, NameInfo typeNameInfo, object stringObject)
		{
			bool flag = true;
			long num = -1L;
			if (!this.CheckTypeFormat(this.formatterEnums.FEtypeFormat, FormatterTypeStyle.XsdString))
			{
				num = this.InternalGetId(stringObject, false, null, out flag);
			}
			typeNameInfo.NIobjectId = num;
			if (flag || num < 0L)
			{
				this.serWriter.WriteMemberString(memberNameInfo, typeNameInfo, (string)stringObject);
				return;
			}
			this.WriteObjectRef(memberNameInfo, num);
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000A4FD0 File Offset: 0x000A31D0
		private bool CheckForNull(WriteObjectInfo objectInfo, NameInfo memberNameInfo, NameInfo typeNameInfo, object data)
		{
			bool flag = false;
			if (data == null)
			{
				flag = true;
			}
			if (flag && (this.formatterEnums.FEserializerTypeEnum == InternalSerializerTypeE.Binary || memberNameInfo.NIisArrayItem || memberNameInfo.NItransmitTypeOnObject || memberNameInfo.NItransmitTypeOnMember || objectInfo.isSi || this.CheckTypeFormat(this.formatterEnums.FEtypeFormat, FormatterTypeStyle.TypesAlways)))
			{
				if (typeNameInfo.NIisArrayItem)
				{
					if (typeNameInfo.NIarrayEnum == InternalArrayTypeE.Single)
					{
						this.serWriter.WriteDelayedNullItem();
					}
					else
					{
						this.serWriter.WriteNullItem(memberNameInfo, typeNameInfo);
					}
				}
				else
				{
					this.serWriter.WriteNullMember(memberNameInfo, typeNameInfo);
				}
			}
			return flag;
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000A5065 File Offset: 0x000A3265
		private void WriteSerializedStreamHeader(long topId, long headerId)
		{
			this.serWriter.WriteSerializationHeader((int)topId, (int)headerId, 1, 0);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000A5078 File Offset: 0x000A3278
		private NameInfo TypeToNameInfo(Type type, WriteObjectInfo objectInfo, InternalPrimitiveTypeE code, NameInfo nameInfo)
		{
			if (nameInfo == null)
			{
				nameInfo = this.GetNameInfo();
			}
			else
			{
				nameInfo.Init();
			}
			if (code == InternalPrimitiveTypeE.Invalid && objectInfo != null)
			{
				nameInfo.NIname = objectInfo.GetTypeFullName();
				nameInfo.NIassemId = objectInfo.assemId;
			}
			nameInfo.NIprimitiveTypeEnum = code;
			nameInfo.NItype = type;
			return nameInfo;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000A50CC File Offset: 0x000A32CC
		private NameInfo TypeToNameInfo(Type type)
		{
			return this.TypeToNameInfo(type, null, this.ToCode(type), null);
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000A50DE File Offset: 0x000A32DE
		private NameInfo TypeToNameInfo(WriteObjectInfo objectInfo)
		{
			return this.TypeToNameInfo(objectInfo.objectType, objectInfo, this.ToCode(objectInfo.objectType), null);
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000A50FA File Offset: 0x000A32FA
		private NameInfo TypeToNameInfo(WriteObjectInfo objectInfo, NameInfo nameInfo)
		{
			return this.TypeToNameInfo(objectInfo.objectType, objectInfo, this.ToCode(objectInfo.objectType), nameInfo);
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000A5116 File Offset: 0x000A3316
		private void TypeToNameInfo(Type type, NameInfo nameInfo)
		{
			this.TypeToNameInfo(type, null, this.ToCode(type), nameInfo);
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000A5129 File Offset: 0x000A3329
		private NameInfo MemberToNameInfo(string name)
		{
			NameInfo nameInfo = this.GetNameInfo();
			nameInfo.NIname = name;
			return nameInfo;
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000A5138 File Offset: 0x000A3338
		internal InternalPrimitiveTypeE ToCode(Type type)
		{
			if (this.previousType == type)
			{
				return this.previousCode;
			}
			InternalPrimitiveTypeE internalPrimitiveTypeE = Converter.ToCode(type);
			if (internalPrimitiveTypeE != InternalPrimitiveTypeE.Invalid)
			{
				this.previousType = type;
				this.previousCode = internalPrimitiveTypeE;
			}
			return internalPrimitiveTypeE;
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000A5170 File Offset: 0x000A3370
		private long GetAssemblyId(WriteObjectInfo objectInfo)
		{
			if (this.assemblyToIdTable == null)
			{
				this.assemblyToIdTable = new Hashtable(5);
			}
			bool flag = false;
			string assemblyString = objectInfo.GetAssemblyString();
			string text = assemblyString;
			long num;
			if (assemblyString.Length == 0)
			{
				num = 0L;
			}
			else if (assemblyString.Equals(Converter.urtAssemblyString))
			{
				num = 0L;
			}
			else
			{
				if (this.assemblyToIdTable.ContainsKey(assemblyString))
				{
					num = (long)this.assemblyToIdTable[assemblyString];
					flag = false;
				}
				else
				{
					num = this.InternalGetId("___AssemblyString___" + assemblyString, false, null, out flag);
					this.assemblyToIdTable[assemblyString] = num;
				}
				this.serWriter.WriteAssembly(objectInfo.objectType, text, (int)num, flag);
			}
			return num;
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000A5220 File Offset: 0x000A3420
		private Type GetType(object obj)
		{
			Type type;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				type = Converter.typeofMarshalByRefObject;
			}
			else
			{
				type = obj.GetType();
			}
			return type;
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000A5248 File Offset: 0x000A3448
		private NameInfo GetNameInfo()
		{
			NameInfo nameInfo;
			if (!this.niPool.IsEmpty())
			{
				nameInfo = (NameInfo)this.niPool.Pop();
				nameInfo.Init();
			}
			else
			{
				nameInfo = new NameInfo();
			}
			return nameInfo;
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000A5284 File Offset: 0x000A3484
		private bool CheckTypeFormat(FormatterTypeStyle test, FormatterTypeStyle want)
		{
			return (test & want) == want;
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000A528C File Offset: 0x000A348C
		private void PutNameInfo(NameInfo nameInfo)
		{
			this.niPool.Push(nameInfo);
		}

		// Token: 0x0400143D RID: 5181
		private Queue m_objectQueue;

		// Token: 0x0400143E RID: 5182
		private ObjectIDGenerator m_idGenerator;

		// Token: 0x0400143F RID: 5183
		private int m_currentId;

		// Token: 0x04001440 RID: 5184
		private ISurrogateSelector m_surrogates;

		// Token: 0x04001441 RID: 5185
		private StreamingContext m_context;

		// Token: 0x04001442 RID: 5186
		private __BinaryWriter serWriter;

		// Token: 0x04001443 RID: 5187
		private SerializationObjectManager m_objectManager;

		// Token: 0x04001444 RID: 5188
		private long topId;

		// Token: 0x04001445 RID: 5189
		private string topName;

		// Token: 0x04001446 RID: 5190
		private Header[] headers;

		// Token: 0x04001447 RID: 5191
		private InternalFE formatterEnums;

		// Token: 0x04001448 RID: 5192
		private SerializationBinder m_binder;

		// Token: 0x04001449 RID: 5193
		private SerObjectInfoInit serObjectInfoInit;

		// Token: 0x0400144A RID: 5194
		private IFormatterConverter m_formatterConverter;

		// Token: 0x0400144B RID: 5195
		internal object[] crossAppDomainArray;

		// Token: 0x0400144C RID: 5196
		private object previousObj;

		// Token: 0x0400144D RID: 5197
		private long previousId;

		// Token: 0x0400144E RID: 5198
		private Type previousType;

		// Token: 0x0400144F RID: 5199
		private InternalPrimitiveTypeE previousCode;

		// Token: 0x04001450 RID: 5200
		private Hashtable assemblyToIdTable;

		// Token: 0x04001451 RID: 5201
		private SerStack niPool = new SerStack("NameInfo Pool");
	}
}
