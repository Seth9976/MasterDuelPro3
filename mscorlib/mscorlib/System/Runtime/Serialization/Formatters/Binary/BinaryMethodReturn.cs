using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004E7 RID: 1255
	internal sealed class BinaryMethodReturn
	{
		// Token: 0x0600275E RID: 10078 RVA: 0x0009E96A File Offset: 0x0009CB6A
		internal BinaryMethodReturn()
		{
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x0009E97C File Offset: 0x0009CB7C
		internal object[] WriteArray(object returnValue, object[] args, Exception exception, object callContext, object[] properties)
		{
			this.returnValue = returnValue;
			this.args = args;
			this.exception = exception;
			this.callContext = callContext;
			this.properties = properties;
			int num = 0;
			if (args == null || args.Length == 0)
			{
				this.messageEnum = MessageEnum.NoArgs;
			}
			else
			{
				this.argTypes = new Type[args.Length];
				this.bArgsPrimitive = true;
				for (int i = 0; i < args.Length; i++)
				{
					if (args[i] != null)
					{
						this.argTypes[i] = args[i].GetType();
						if (Converter.ToCode(this.argTypes[i]) <= InternalPrimitiveTypeE.Invalid && this.argTypes[i] != Converter.typeofString)
						{
							this.bArgsPrimitive = false;
							break;
						}
					}
				}
				if (this.bArgsPrimitive)
				{
					this.messageEnum = MessageEnum.ArgsInline;
				}
				else
				{
					num++;
					this.messageEnum = MessageEnum.ArgsInArray;
				}
			}
			if (returnValue == null)
			{
				this.messageEnum |= MessageEnum.NoReturnValue;
			}
			else if (returnValue.GetType() == typeof(void))
			{
				this.messageEnum |= MessageEnum.ReturnValueVoid;
			}
			else
			{
				this.returnType = returnValue.GetType();
				if (Converter.ToCode(this.returnType) > InternalPrimitiveTypeE.Invalid || this.returnType == Converter.typeofString)
				{
					this.messageEnum |= MessageEnum.ReturnValueInline;
				}
				else
				{
					num++;
					this.messageEnum |= MessageEnum.ReturnValueInArray;
				}
			}
			if (exception != null)
			{
				num++;
				this.messageEnum |= MessageEnum.ExceptionInArray;
			}
			if (callContext == null)
			{
				this.messageEnum |= MessageEnum.NoContext;
			}
			else if (callContext is string)
			{
				this.messageEnum |= MessageEnum.ContextInline;
			}
			else
			{
				num++;
				this.messageEnum |= MessageEnum.ContextInArray;
			}
			if (properties != null)
			{
				num++;
				this.messageEnum |= MessageEnum.PropertyInArray;
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray) && num == 1)
			{
				this.messageEnum ^= MessageEnum.ArgsInArray;
				this.messageEnum |= MessageEnum.ArgsIsArray;
				return args;
			}
			if (num > 0)
			{
				int num2 = 0;
				this.callA = new object[num];
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
				{
					this.callA[num2++] = args;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInArray))
				{
					this.callA[num2++] = returnValue;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ExceptionInArray))
				{
					this.callA[num2++] = exception;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
				{
					this.callA[num2++] = callContext;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
				{
					this.callA[num2] = properties;
				}
				return this.callA;
			}
			return null;
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x0009EC28 File Offset: 0x0009CE28
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(22);
			sout.WriteInt32((int)this.messageEnum);
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInline))
			{
				IOUtil.WriteWithCode(this.returnType, this.returnValue, sout);
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
			{
				IOUtil.WriteStringWithCode((string)this.callContext, sout);
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
			{
				sout.WriteInt32(this.args.Length);
				for (int i = 0; i < this.args.Length; i++)
				{
					IOUtil.WriteWithCode(this.argTypes[i], this.args[i], sout);
				}
			}
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x0009ECD4 File Offset: 0x0009CED4
		public void Read(__BinaryParser input)
		{
			this.messageEnum = (MessageEnum)input.ReadInt32();
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.NoReturnValue))
			{
				this.returnValue = null;
			}
			else if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueVoid))
			{
				this.returnValue = BinaryMethodReturn.instanceOfVoid;
			}
			else if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInline))
			{
				this.returnValue = IOUtil.ReadWithCode(input);
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
			{
				this.scallContext = (string)IOUtil.ReadWithCode(input);
				this.callContext = new LogicalCallContext
				{
					RemotingData = 
					{
						LogicalCallID = this.scallContext
					}
				};
			}
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
			{
				this.args = IOUtil.ReadArgs(input);
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x0009EDA0 File Offset: 0x0009CFA0
		internal IMethodReturnMessage ReadArray(object[] returnA, IMethodCallMessage methodCallMessage, object handlerObject)
		{
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsIsArray))
			{
				this.args = returnA;
			}
			else
			{
				int num = 0;
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
				{
					if (returnA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.args = (object[])returnA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInArray))
				{
					if (returnA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.returnValue = returnA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ExceptionInArray))
				{
					if (returnA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.exception = (Exception)returnA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
				{
					if (returnA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.callContext = returnA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
				{
					if (returnA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.properties = returnA[num++];
				}
			}
			return new MethodResponse(methodCallMessage, handlerObject, new BinaryMethodReturnMessage(this.returnValue, this.args, this.exception, (LogicalCallContext)this.callContext, (object[])this.properties));
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x04001342 RID: 4930
		private object returnValue;

		// Token: 0x04001343 RID: 4931
		private object[] args;

		// Token: 0x04001344 RID: 4932
		private Exception exception;

		// Token: 0x04001345 RID: 4933
		private object callContext;

		// Token: 0x04001346 RID: 4934
		private string scallContext;

		// Token: 0x04001347 RID: 4935
		private object properties;

		// Token: 0x04001348 RID: 4936
		private Type[] argTypes;

		// Token: 0x04001349 RID: 4937
		private bool bArgsPrimitive = true;

		// Token: 0x0400134A RID: 4938
		private MessageEnum messageEnum;

		// Token: 0x0400134B RID: 4939
		private object[] callA;

		// Token: 0x0400134C RID: 4940
		private Type returnType;

		// Token: 0x0400134D RID: 4941
		private static object instanceOfVoid = FormatterServices.GetUninitializedObject(Converter.typeofSystemVoid);
	}
}
