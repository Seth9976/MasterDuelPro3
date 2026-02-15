using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000496 RID: 1174
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoMethodMessage : IMethodCallMessage, IMethodMessage, IMessage, IMethodReturnMessage, IInternalMessage
	{
		// Token: 0x060025CC RID: 9676 RVA: 0x00099AB4 File Offset: 0x00097CB4
		internal void InitMessage(RuntimeMethodInfo method, object[] out_args)
		{
			this.method = method;
			ParameterInfo[] parametersInternal = method.GetParametersInternal();
			int num = parametersInternal.Length;
			this.args = new object[num];
			this.arg_types = new byte[num];
			this.asyncResult = null;
			this.call_type = CallType.Sync;
			this.names = new string[num];
			for (int i = 0; i < num; i++)
			{
				this.names[i] = parametersInternal[i].Name;
			}
			bool flag = out_args != null;
			int num2 = 0;
			for (int j = 0; j < num; j++)
			{
				bool isOut = parametersInternal[j].IsOut;
				byte b;
				if (parametersInternal[j].ParameterType.IsByRef)
				{
					if (flag)
					{
						this.args[j] = out_args[num2++];
					}
					b = 2;
					if (!isOut)
					{
						b |= 1;
					}
				}
				else
				{
					b = 1;
					if (isOut)
					{
						b |= 4;
					}
				}
				this.arg_types[j] = b;
			}
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x00099B95 File Offset: 0x00097D95
		public MonoMethodMessage(MethodBase method, object[] out_args)
		{
			if (method != null)
			{
				this.InitMessage((RuntimeMethodInfo)method, out_args);
				return;
			}
			this.args = null;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x00099BBC File Offset: 0x00097DBC
		internal MonoMethodMessage(MethodInfo minfo, object[] in_args, object[] out_args)
		{
			this.InitMessage((RuntimeMethodInfo)minfo, out_args);
			int num = in_args.Length;
			for (int i = 0; i < num; i++)
			{
				this.args[i] = in_args[i];
			}
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x00099BF7 File Offset: 0x00097DF7
		private static MethodInfo GetMethodInfo(Type type, string methodName)
		{
			MethodInfo methodInfo = type.GetMethod(methodName);
			if (methodInfo == null)
			{
				throw new ArgumentException(string.Format("Could not find '{0}' in {1}", methodName, type), "methodName");
			}
			return methodInfo;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x00099C20 File Offset: 0x00097E20
		public MonoMethodMessage(Type type, string methodName, object[] in_args)
			: this(MonoMethodMessage.GetMethodInfo(type, methodName), in_args, null)
		{
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x00099C31 File Offset: 0x00097E31
		public IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new MCMDictionary(this);
				}
				return this.properties;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x00099C4D File Offset: 0x00097E4D
		public int ArgCount
		{
			get
			{
				if (this.CallType == CallType.EndInvoke)
				{
					return -1;
				}
				if (this.args == null)
				{
					return 0;
				}
				return this.args.Length;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x00099C6C File Offset: 0x00097E6C
		public object[] Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x00033991 File Offset: 0x00031B91
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x00099C74 File Offset: 0x00097E74
		// (set) Token: 0x060025D6 RID: 9686 RVA: 0x00099C7C File Offset: 0x00097E7C
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this.ctx;
			}
			set
			{
				this.ctx = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x00099C85 File Offset: 0x00097E85
		public MethodBase MethodBase
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060025D8 RID: 9688 RVA: 0x00099C8D File Offset: 0x00097E8D
		public string MethodName
		{
			get
			{
				if (null == this.method)
				{
					return string.Empty;
				}
				return this.method.Name;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00099CB0 File Offset: 0x00097EB0
		public object MethodSignature
		{
			get
			{
				if (this.methodSignature == null)
				{
					ParameterInfo[] parameters = this.method.GetParameters();
					this.methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this.methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this.methodSignature;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x00099D03 File Offset: 0x00097F03
		public string TypeName
		{
			get
			{
				if (null == this.method)
				{
					return string.Empty;
				}
				return this.method.DeclaringType.AssemblyQualifiedName;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x00099D29 File Offset: 0x00097F29
		// (set) Token: 0x060025DC RID: 9692 RVA: 0x00099D31 File Offset: 0x00097F31
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00099D3A File Offset: 0x00097F3A
		public object GetArg(int arg_num)
		{
			if (this.args == null)
			{
				return null;
			}
			return this.args[arg_num];
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x00099D4E File Offset: 0x00097F4E
		public string GetArgName(int arg_num)
		{
			if (this.args == null)
			{
				return string.Empty;
			}
			return this.names[arg_num];
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x00099D68 File Offset: 0x00097F68
		public int InArgCount
		{
			get
			{
				if (this.CallType == CallType.EndInvoke)
				{
					return -1;
				}
				if (this.args == null)
				{
					return 0;
				}
				int num = 0;
				byte[] array = this.arg_types;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] & 1) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x00099DB0 File Offset: 0x00097FB0
		public object[] InArgs
		{
			get
			{
				object[] array = new object[this.InArgCount];
				int num2;
				int num = (num2 = 0);
				byte[] array2 = this.arg_types;
				for (int i = 0; i < array2.Length; i++)
				{
					if ((array2[i] & 1) != 0)
					{
						array[num++] = this.args[num2];
					}
					num2++;
				}
				return array;
			}
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x00099E04 File Offset: 0x00098004
		public object GetInArg(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 1) != 0 && num2++ == arg_num)
				{
					return this.args[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x00099E48 File Offset: 0x00098048
		public string GetInArgName(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 1) != 0 && num2++ == arg_num)
				{
					return this.names[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x00099E8B File Offset: 0x0009808B
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x00099E94 File Offset: 0x00098094
		public int OutArgCount
		{
			get
			{
				if (this.args == null)
				{
					return 0;
				}
				int num = 0;
				byte[] array = this.arg_types;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] & 2) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x00099ED0 File Offset: 0x000980D0
		public object[] OutArgs
		{
			get
			{
				if (this.args == null)
				{
					return null;
				}
				object[] array = new object[this.OutArgCount];
				int num2;
				int num = (num2 = 0);
				byte[] array2 = this.arg_types;
				for (int i = 0; i < array2.Length; i++)
				{
					if ((array2[i] & 2) != 0)
					{
						array[num++] = this.args[num2];
					}
					num2++;
				}
				return array;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x00099F2C File Offset: 0x0009812C
		public object ReturnValue
		{
			get
			{
				return this.rval;
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x00099F34 File Offset: 0x00098134
		public object GetOutArg(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 2) != 0 && num2++ == arg_num)
				{
					return this.args[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x00099F78 File Offset: 0x00098178
		public string GetOutArgName(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			byte[] array = this.arg_types;
			for (int i = 0; i < array.Length; i++)
			{
				if ((array[i] & 2) != 0 && num2++ == arg_num)
				{
					return this.names[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x00099FBB File Offset: 0x000981BB
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x00099FC3 File Offset: 0x000981C3
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x00099FCC File Offset: 0x000981CC
		bool IInternalMessage.HasProperties()
		{
			return this.properties != null;
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x00099FD7 File Offset: 0x000981D7
		public bool IsAsync
		{
			get
			{
				return this.asyncResult != null;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x00099FE2 File Offset: 0x000981E2
		public AsyncResult AsyncResult
		{
			get
			{
				return this.asyncResult;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x00099FEA File Offset: 0x000981EA
		internal CallType CallType
		{
			get
			{
				if (this.call_type == CallType.Sync && RemotingServices.IsOneWay(this.method))
				{
					this.call_type = CallType.OneWay;
				}
				return this.call_type;
			}
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x0009A010 File Offset: 0x00098210
		public bool NeedsOutProcessing(out int outCount)
		{
			bool flag = false;
			outCount = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 2) != 0)
				{
					outCount++;
				}
				else if ((b & 4) != 0)
				{
					flag = true;
				}
			}
			return outCount > 0 || flag;
		}

		// Token: 0x04001219 RID: 4633
		private RuntimeMethodInfo method;

		// Token: 0x0400121A RID: 4634
		private object[] args;

		// Token: 0x0400121B RID: 4635
		private string[] names;

		// Token: 0x0400121C RID: 4636
		private byte[] arg_types;

		// Token: 0x0400121D RID: 4637
		public LogicalCallContext ctx;

		// Token: 0x0400121E RID: 4638
		public object rval;

		// Token: 0x0400121F RID: 4639
		public Exception exc;

		// Token: 0x04001220 RID: 4640
		private AsyncResult asyncResult;

		// Token: 0x04001221 RID: 4641
		private CallType call_type;

		// Token: 0x04001222 RID: 4642
		private string uri;

		// Token: 0x04001223 RID: 4643
		private MCMDictionary properties;

		// Token: 0x04001224 RID: 4644
		private Identity identity;

		// Token: 0x04001225 RID: 4645
		private Type[] methodSignature;
	}
}
