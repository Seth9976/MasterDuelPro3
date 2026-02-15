using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class SerializableEnum
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000092F0 File Offset: 0x000074F0
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000932C File Offset: 0x0000752C
		public Enum value
		{
			get
			{
				object result;
				if (string.IsNullOrEmpty(this.m_EnumTypeAsString) || !Enum.TryParse(Type.GetType(this.m_EnumTypeAsString), this.m_EnumValueAsString, out result))
				{
					return null;
				}
				return (Enum)result;
			}
			set
			{
				this.m_EnumValueAsString = value.ToString();
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000933A File Offset: 0x0000753A
		public SerializableEnum(Type enumType)
		{
			this.m_EnumTypeAsString = enumType.AssemblyQualifiedName;
			this.m_EnumValueAsString = Enum.GetNames(enumType)[0];
		}

		// Token: 0x04000140 RID: 320
		[SerializeField]
		private string m_EnumValueAsString;

		// Token: 0x04000141 RID: 321
		[SerializeField]
		private string m_EnumTypeAsString;
	}
}
