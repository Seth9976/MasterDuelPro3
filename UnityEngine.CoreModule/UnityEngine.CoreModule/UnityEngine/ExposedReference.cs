using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000BA RID: 186
	[UsedByNativeCode(Name = "ExposedReference")]
	[Serializable]
	public struct ExposedReference<T> where T : Object
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x000094E0 File Offset: 0x000076E0
		public T Resolve(IExposedPropertyTable resolver)
		{
			bool flag = resolver != null;
			if (flag)
			{
				bool isValid;
				Object result = resolver.GetReferenceValue(this.exposedName, out isValid);
				bool flag2 = isValid;
				if (flag2)
				{
					return result as T;
				}
			}
			return this.defaultValue as T;
		}

		// Token: 0x04000241 RID: 577
		[SerializeField]
		public PropertyName exposedName;

		// Token: 0x04000242 RID: 578
		[SerializeField]
		public Object defaultValue;
	}
}
