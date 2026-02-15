using System;
using System.IO;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public struct SerializedType
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006724 File Offset: 0x00004924
		public string AssemblyName
		{
			get
			{
				return this.m_AssemblyName;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000672C File Offset: 0x0000492C
		public string ClassName
		{
			get
			{
				return this.m_ClassName;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006734 File Offset: 0x00004934
		public override string ToString()
		{
			if (!(this.Value == null))
			{
				return this.Value.Name;
			}
			return "<none>";
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006758 File Offset: 0x00004958
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000067F4 File Offset: 0x000049F4
		public Type Value
		{
			get
			{
				Type type;
				try
				{
					if (string.IsNullOrEmpty(this.m_AssemblyName) || string.IsNullOrEmpty(this.m_ClassName))
					{
						type = null;
					}
					else
					{
						if (this.m_CachedType == null)
						{
							Assembly assembly = Assembly.Load(this.m_AssemblyName);
							if (assembly != null)
							{
								this.m_CachedType = assembly.GetType(this.m_ClassName);
							}
						}
						type = this.m_CachedType;
					}
				}
				catch (Exception ex)
				{
					if (ex.GetType() != typeof(FileNotFoundException))
					{
						Debug.LogException(ex);
					}
					type = null;
				}
				return type;
			}
			set
			{
				if (value != null)
				{
					this.m_AssemblyName = value.Assembly.FullName;
					this.m_ClassName = value.FullName;
					return;
				}
				this.m_AssemblyName = (this.m_ClassName = null);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006838 File Offset: 0x00004A38
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00006840 File Offset: 0x00004A40
		public bool ValueChanged { readonly get; set; }

		// Token: 0x0400008E RID: 142
		[FormerlySerializedAs("m_assemblyName")]
		[SerializeField]
		private string m_AssemblyName;

		// Token: 0x0400008F RID: 143
		[FormerlySerializedAs("m_className")]
		[SerializeField]
		private string m_ClassName;

		// Token: 0x04000090 RID: 144
		private Type m_CachedType;
	}
}
