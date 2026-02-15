using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004E6 RID: 1254
	internal sealed class BinaryMethodCall
	{
		// Token: 0x06002757 RID: 10071 RVA: 0x0009E438 File Offset: 0x0009C638
		internal object[] WriteArray(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, object callContext, object[] properties)
		{
			this.uri = uri;
			this.methodName = methodName;
			this.typeName = typeName;
			this.instArgs = instArgs;
			this.args = args;
			this.methodSignature = methodSignature;
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
						if ((Converter.ToCode(this.argTypes[i]) <= InternalPrimitiveTypeE.Invalid && this.argTypes[i] != Converter.typeofString) || args[i] is ISerializable)
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
			if (instArgs != null)
			{
				num++;
				this.messageEnum |= MessageEnum.GenericMethod;
			}
			if (methodSignature != null)
			{
				num++;
				this.messageEnum |= MessageEnum.MethodSignatureInArray;
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
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.GenericMethod))
				{
					this.callA[num2++] = instArgs;
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.MethodSignatureInArray))
				{
					this.callA[num2++] = methodSignature;
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

		// Token: 0x06002758 RID: 10072 RVA: 0x0009E69C File Offset: 0x0009C89C
		internal void Write(__BinaryWriter sout)
		{
			sout.WriteByte(21);
			sout.WriteInt32((int)this.messageEnum);
			IOUtil.WriteStringWithCode(this.methodName, sout);
			IOUtil.WriteStringWithCode(this.typeName, sout);
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

		// Token: 0x06002759 RID: 10073 RVA: 0x0009E73C File Offset: 0x0009C93C
		internal void Read(__BinaryParser input)
		{
			this.messageEnum = (MessageEnum)input.ReadInt32();
			this.methodName = (string)IOUtil.ReadWithCode(input);
			this.typeName = (string)IOUtil.ReadWithCode(input);
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

		// Token: 0x0600275A RID: 10074 RVA: 0x0009E7D0 File Offset: 0x0009C9D0
		internal IMethodCallMessage ReadArray(object[] callA, object handlerObject)
		{
			if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsIsArray))
			{
				this.args = callA;
			}
			else
			{
				int num = 0;
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.args = (object[])callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.GenericMethod))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.instArgs = (Type[])callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.MethodSignatureInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.methodSignature = callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.callContext = callA[num++];
				}
				if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
				{
					if (callA.Length < num)
					{
						throw new SerializationException(Environment.GetResourceString("Invalid MethodCall or MethodReturn stream format."));
					}
					this.properties = callA[num++];
				}
			}
			return new MethodCall(handlerObject, new BinaryMethodCallMessage(this.uri, this.methodName, this.typeName, this.instArgs, this.args, this.methodSignature, (LogicalCallContext)this.callContext, (object[])this.properties));
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x00002C89 File Offset: 0x00000E89
		internal void Dump()
		{
		}

		// Token: 0x04001335 RID: 4917
		private string uri;

		// Token: 0x04001336 RID: 4918
		private string methodName;

		// Token: 0x04001337 RID: 4919
		private string typeName;

		// Token: 0x04001338 RID: 4920
		private Type[] instArgs;

		// Token: 0x04001339 RID: 4921
		private object[] args;

		// Token: 0x0400133A RID: 4922
		private object methodSignature;

		// Token: 0x0400133B RID: 4923
		private object callContext;

		// Token: 0x0400133C RID: 4924
		private string scallContext;

		// Token: 0x0400133D RID: 4925
		private object properties;

		// Token: 0x0400133E RID: 4926
		private Type[] argTypes;

		// Token: 0x0400133F RID: 4927
		private bool bArgsPrimitive = true;

		// Token: 0x04001340 RID: 4928
		private MessageEnum messageEnum;

		// Token: 0x04001341 RID: 4929
		private object[] callA;
	}
}
