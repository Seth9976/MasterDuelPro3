using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class SqliteFunctionAttribute : Attribute
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000AC08 File Offset: 0x00008E08
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000AC10 File Offset: 0x00008E10
		public int Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000AC18 File Offset: 0x00008E18
		public FunctionType FuncType
		{
			get
			{
				return this._functionType;
			}
		}

		// Token: 0x04000084 RID: 132
		private string _name;

		// Token: 0x04000085 RID: 133
		private int _arguments;

		// Token: 0x04000086 RID: 134
		private FunctionType _functionType;

		// Token: 0x04000087 RID: 135
		internal Type _instanceType;
	}
}
