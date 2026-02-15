using System;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000222 RID: 546
	[Serializable]
	internal class ArgumentCache : ISerializationCallbackReceiver
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0002A0E4 File Offset: 0x000282E4
		public Object unityObjectArgument
		{
			get
			{
				return this.m_ObjectArgument;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0002A0FC File Offset: 0x000282FC
		public string unityObjectArgumentAssemblyTypeName
		{
			get
			{
				return this.m_ObjectArgumentAssemblyTypeName;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0002A114 File Offset: 0x00028314
		public int intArgument
		{
			get
			{
				return this.m_IntArgument;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0002A12C File Offset: 0x0002832C
		public float floatArgument
		{
			get
			{
				return this.m_FloatArgument;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0002A144 File Offset: 0x00028344
		public string stringArgument
		{
			get
			{
				return this.m_StringArgument;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0002A15C File Offset: 0x0002835C
		public bool boolArgument
		{
			get
			{
				return this.m_BoolArgument;
			}
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0002A174 File Offset: 0x00028374
		public void OnBeforeSerialize()
		{
			this.m_ObjectArgumentAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_ObjectArgumentAssemblyTypeName);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0002A174 File Offset: 0x00028374
		public void OnAfterDeserialize()
		{
			this.m_ObjectArgumentAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_ObjectArgumentAssemblyTypeName);
		}

		// Token: 0x04000773 RID: 1907
		[FormerlySerializedAs("objectArgument")]
		[SerializeField]
		private Object m_ObjectArgument;

		// Token: 0x04000774 RID: 1908
		[FormerlySerializedAs("objectArgumentAssemblyTypeName")]
		[SerializeField]
		private string m_ObjectArgumentAssemblyTypeName;

		// Token: 0x04000775 RID: 1909
		[SerializeField]
		[FormerlySerializedAs("intArgument")]
		private int m_IntArgument;

		// Token: 0x04000776 RID: 1910
		[FormerlySerializedAs("floatArgument")]
		[SerializeField]
		private float m_FloatArgument;

		// Token: 0x04000777 RID: 1911
		[FormerlySerializedAs("stringArgument")]
		[SerializeField]
		private string m_StringArgument;

		// Token: 0x04000778 RID: 1912
		[SerializeField]
		private bool m_BoolArgument;
	}
}
