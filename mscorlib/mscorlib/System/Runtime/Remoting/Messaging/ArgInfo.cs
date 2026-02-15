using System;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000478 RID: 1144
	internal class ArgInfo
	{
		// Token: 0x060024F9 RID: 9465 RVA: 0x00096F04 File Offset: 0x00095104
		public ArgInfo(MethodBase method, ArgInfoType type)
		{
			this._method = method;
			ParameterInfo[] parameters = this._method.GetParameters();
			this._paramMap = new int[parameters.Length];
			this._inoutArgCount = 0;
			if (type == ArgInfoType.In)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (!parameters[i].ParameterType.IsByRef)
					{
						int[] paramMap = this._paramMap;
						int num = this._inoutArgCount;
						this._inoutArgCount = num + 1;
						paramMap[num] = i;
					}
				}
				return;
			}
			for (int j = 0; j < parameters.Length; j++)
			{
				if (parameters[j].ParameterType.IsByRef || parameters[j].IsOut)
				{
					int[] paramMap2 = this._paramMap;
					int num = this._inoutArgCount;
					this._inoutArgCount = num + 1;
					paramMap2[num] = j;
				}
			}
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00096FBC File Offset: 0x000951BC
		public object[] GetInOutArgs(object[] args)
		{
			object[] array = new object[this._inoutArgCount];
			for (int i = 0; i < this._inoutArgCount; i++)
			{
				array[i] = args[this._paramMap[i]];
			}
			return array;
		}

		// Token: 0x040011C3 RID: 4547
		private int[] _paramMap;

		// Token: 0x040011C4 RID: 4548
		private int _inoutArgCount;

		// Token: 0x040011C5 RID: 4549
		private MethodBase _method;
	}
}
