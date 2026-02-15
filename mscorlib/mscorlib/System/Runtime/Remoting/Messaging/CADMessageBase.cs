using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200047D RID: 1149
	internal class CADMessageBase
	{
		// Token: 0x06002514 RID: 9492 RVA: 0x00097464 File Offset: 0x00095664
		public CADMessageBase(IMethodMessage msg)
		{
			CADMethodRef cadmethodRef = new CADMethodRef(msg);
			this.serializedMethod = CADSerializer.SerializeObject(cadmethodRef).GetBuffer();
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0009748F File Offset: 0x0009568F
		internal MethodBase GetMethod()
		{
			return ((CADMethodRef)CADSerializer.DeserializeObjectSafe(this.serializedMethod)).Resolve();
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000974A8 File Offset: 0x000956A8
		protected static Type[] GetSignature(MethodBase methodBase, bool load)
		{
			ParameterInfo[] parameters = methodBase.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				if (load)
				{
					array[i] = Type.GetType(parameters[i].ParameterType.AssemblyQualifiedName, true);
				}
				else
				{
					array[i] = parameters[i].ParameterType;
				}
			}
			return array;
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000974FC File Offset: 0x000956FC
		internal static int MarshalProperties(IDictionary dict, ref ArrayList args)
		{
			int num = 0;
			MessageDictionary messageDictionary = dict as MessageDictionary;
			if (messageDictionary != null)
			{
				if (!messageDictionary.HasUserData())
				{
					return num;
				}
				IDictionary internalDictionary = messageDictionary.InternalDictionary;
				if (internalDictionary == null)
				{
					return num;
				}
				using (IDictionaryEnumerator dictionaryEnumerator = internalDictionary.GetEnumerator())
				{
					while (dictionaryEnumerator.MoveNext())
					{
						object obj = dictionaryEnumerator.Current;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (args == null)
						{
							args = new ArrayList();
						}
						args.Add(dictionaryEntry);
						num++;
					}
					return num;
				}
			}
			if (dict != null)
			{
				foreach (object obj2 in dict)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (args == null)
					{
						args = new ArrayList();
					}
					args.Add(dictionaryEntry2);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000975F4 File Offset: 0x000957F4
		internal static void UnmarshalProperties(IDictionary dict, int count, ArrayList args)
		{
			for (int i = 0; i < count; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)args[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00097630 File Offset: 0x00095830
		private static bool IsPossibleToIgnoreMarshal(object obj)
		{
			Type type = obj.GetType();
			return type.IsPrimitive || type == typeof(void) || (type.IsArray && type.GetElementType().IsPrimitive && ((Array)obj).Rank == 1) || (obj is string || obj is DateTime || obj is TimeSpan);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000976A0 File Offset: 0x000958A0
		protected object MarshalArgument(object arg, ref ArrayList args)
		{
			if (arg == null)
			{
				return null;
			}
			if (CADMessageBase.IsPossibleToIgnoreMarshal(arg))
			{
				return arg;
			}
			MarshalByRefObject marshalByRefObject = arg as MarshalByRefObject;
			if (marshalByRefObject != null && !RemotingServices.IsTransparentProxy(marshalByRefObject))
			{
				return new CADObjRef(RemotingServices.Marshal(marshalByRefObject), Thread.GetDomainID());
			}
			if (args == null)
			{
				args = new ArrayList();
			}
			args.Add(arg);
			return new CADArgHolder(args.Count - 1);
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00097704 File Offset: 0x00095904
		protected object UnmarshalArgument(object arg, ArrayList args)
		{
			if (arg == null)
			{
				return null;
			}
			CADArgHolder cadargHolder = arg as CADArgHolder;
			if (cadargHolder != null)
			{
				return args[cadargHolder.index];
			}
			CADObjRef cadobjRef = arg as CADObjRef;
			if (cadobjRef != null)
			{
				return RemotingServices.Unmarshal(cadobjRef.objref.DeserializeInTheCurrentDomain(cadobjRef.SourceDomain, cadobjRef.TypeInfo));
			}
			if (arg is Array)
			{
				Array array = (Array)arg;
				Array array2;
				switch (Type.GetTypeCode(arg.GetType().GetElementType()))
				{
				case TypeCode.Boolean:
					array2 = new bool[array.Length];
					break;
				case TypeCode.Char:
					array2 = new char[array.Length];
					break;
				case TypeCode.SByte:
					array2 = new sbyte[array.Length];
					break;
				case TypeCode.Byte:
					array2 = new byte[array.Length];
					break;
				case TypeCode.Int16:
					array2 = new short[array.Length];
					break;
				case TypeCode.UInt16:
					array2 = new ushort[array.Length];
					break;
				case TypeCode.Int32:
					array2 = new int[array.Length];
					break;
				case TypeCode.UInt32:
					array2 = new uint[array.Length];
					break;
				case TypeCode.Int64:
					array2 = new long[array.Length];
					break;
				case TypeCode.UInt64:
					array2 = new ulong[array.Length];
					break;
				case TypeCode.Single:
					array2 = new float[array.Length];
					break;
				case TypeCode.Double:
					array2 = new double[array.Length];
					break;
				case TypeCode.Decimal:
					array2 = new decimal[array.Length];
					break;
				default:
					throw new NotSupportedException();
				}
				array.CopyTo(array2, 0);
				return array2;
			}
			switch (Type.GetTypeCode(arg.GetType()))
			{
			case TypeCode.Boolean:
				return (bool)arg;
			case TypeCode.Char:
				return (char)arg;
			case TypeCode.SByte:
				return (sbyte)arg;
			case TypeCode.Byte:
				return (byte)arg;
			case TypeCode.Int16:
				return (short)arg;
			case TypeCode.UInt16:
				return (ushort)arg;
			case TypeCode.Int32:
				return (int)arg;
			case TypeCode.UInt32:
				return (uint)arg;
			case TypeCode.Int64:
				return (long)arg;
			case TypeCode.UInt64:
				return (ulong)arg;
			case TypeCode.Single:
				return (float)arg;
			case TypeCode.Double:
				return (double)arg;
			case TypeCode.Decimal:
				return (decimal)arg;
			case TypeCode.DateTime:
				return new DateTime(((DateTime)arg).Ticks);
			case TypeCode.String:
				return string.Copy((string)arg);
			}
			if (arg is TimeSpan)
			{
				return new TimeSpan(((TimeSpan)arg).Ticks);
			}
			if (arg is IntPtr)
			{
				return (IntPtr)arg;
			}
			string text = "Parameter of type ";
			Type type = arg.GetType();
			throw new NotSupportedException(text + ((type != null) ? type.ToString() : null) + " cannot be unmarshalled");
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00097A08 File Offset: 0x00095C08
		internal object[] MarshalArguments(object[] arguments, ref ArrayList args)
		{
			object[] array = new object[arguments.Length];
			int num = arguments.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.MarshalArgument(arguments[i], ref args);
			}
			return array;
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00097A3C File Offset: 0x00095C3C
		internal object[] UnmarshalArguments(object[] arguments, ArrayList args)
		{
			object[] array = new object[arguments.Length];
			int num = arguments.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.UnmarshalArgument(arguments[i], args);
			}
			return array;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00097A70 File Offset: 0x00095C70
		protected void SaveLogicalCallContext(IMethodMessage msg, ref ArrayList serializeList)
		{
			if (msg.LogicalCallContext != null && msg.LogicalCallContext.HasInfo)
			{
				if (serializeList == null)
				{
					serializeList = new ArrayList();
				}
				this._callContext = new CADArgHolder(serializeList.Count);
				serializeList.Add(msg.LogicalCallContext);
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x00097ABD File Offset: 0x00095CBD
		internal LogicalCallContext GetLogicalCallContext(ArrayList args)
		{
			if (this._callContext == null)
			{
				return null;
			}
			return (LogicalCallContext)args[this._callContext.index];
		}

		// Token: 0x040011DF RID: 4575
		protected object[] _args;

		// Token: 0x040011E0 RID: 4576
		protected byte[] _serializedArgs;

		// Token: 0x040011E1 RID: 4577
		protected int _propertyCount;

		// Token: 0x040011E2 RID: 4578
		protected CADArgHolder _callContext;

		// Token: 0x040011E3 RID: 4579
		internal byte[] serializedMethod;
	}
}
