using System;

namespace System.Runtime.Remoting
{
	// Token: 0x0200042B RID: 1067
	[Serializable]
	internal class TypeInfo : IRemotingTypeInfo
	{
		// Token: 0x060023A0 RID: 9120 RVA: 0x00092E20 File Offset: 0x00091020
		public TypeInfo(Type type)
		{
			if (type.IsInterface)
			{
				this.serverType = typeof(MarshalByRefObject).AssemblyQualifiedName;
				this.serverHierarchy = new string[0];
				Type[] interfaces = type.GetInterfaces();
				this.interfacesImplemented = new string[interfaces.Length + 1];
				for (int i = 0; i < interfaces.Length; i++)
				{
					this.interfacesImplemented[i] = interfaces[i].AssemblyQualifiedName;
				}
				this.interfacesImplemented[interfaces.Length] = type.AssemblyQualifiedName;
				return;
			}
			this.serverType = type.AssemblyQualifiedName;
			int num = 0;
			Type type2 = type.BaseType;
			while (type2 != typeof(MarshalByRefObject) && type2 != null)
			{
				type2 = type2.BaseType;
				num++;
			}
			this.serverHierarchy = new string[num];
			type2 = type.BaseType;
			for (int j = 0; j < num; j++)
			{
				this.serverHierarchy[j] = type2.AssemblyQualifiedName;
				type2 = type2.BaseType;
			}
			Type[] interfaces2 = type.GetInterfaces();
			this.interfacesImplemented = new string[interfaces2.Length];
			for (int k = 0; k < interfaces2.Length; k++)
			{
				this.interfacesImplemented[k] = interfaces2[k].AssemblyQualifiedName;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x00092F55 File Offset: 0x00091155
		// (set) Token: 0x060023A2 RID: 9122 RVA: 0x00092F5D File Offset: 0x0009115D
		public string TypeName
		{
			get
			{
				return this.serverType;
			}
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x00092F68 File Offset: 0x00091168
		public bool CanCastTo(Type fromType, object o)
		{
			if (fromType == typeof(object))
			{
				return true;
			}
			if (fromType == typeof(MarshalByRefObject))
			{
				return true;
			}
			string text = fromType.AssemblyQualifiedName;
			int num = text.IndexOf(',');
			if (num != -1)
			{
				num = text.IndexOf(',', num + 1);
			}
			if (num != -1)
			{
				text = text.Substring(0, num + 1);
			}
			else
			{
				text += ",";
			}
			if ((this.serverType + ",").StartsWith(text))
			{
				return true;
			}
			if (this.serverHierarchy != null)
			{
				string[] array = this.serverHierarchy;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			if (this.interfacesImplemented != null)
			{
				string[] array = this.interfacesImplemented;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04001134 RID: 4404
		private string serverType;

		// Token: 0x04001135 RID: 4405
		private string[] serverHierarchy;

		// Token: 0x04001136 RID: 4406
		private string[] interfacesImplemented;
	}
}
