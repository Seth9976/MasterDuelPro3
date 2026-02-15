using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D8 RID: 216
	internal class TProfilingSampler<TEnum> : ProfilingSampler where TEnum : Enum
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x00010540 File Offset: 0x0000E740
		static TProfilingSampler()
		{
			string[] names = Enum.GetNames(typeof(TEnum));
			Array values = Enum.GetValues(typeof(TEnum));
			for (int i = 0; i < names.Length; i++)
			{
				TProfilingSampler<TEnum> sample = new TProfilingSampler<TEnum>(names[i]);
				TProfilingSampler<TEnum>.samples.Add((TEnum)((object)values.GetValue(i)), sample);
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000105A5 File Offset: 0x0000E7A5
		public TProfilingSampler(string name)
			: base(name)
		{
		}

		// Token: 0x04000293 RID: 659
		internal static Dictionary<TEnum, TProfilingSampler<TEnum>> samples = new Dictionary<TEnum, TProfilingSampler<TEnum>>();
	}
}
