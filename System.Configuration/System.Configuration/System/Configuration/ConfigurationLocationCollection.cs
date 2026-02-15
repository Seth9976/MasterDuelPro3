using System;
using System.Collections;
using System.Reflection;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.ConfigurationLocationCollection" /> objects.</summary>
	// Token: 0x02000016 RID: 22
	[DefaultMember("Item")]
	public class ConfigurationLocationCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004AB7 File Offset: 0x00002CB7
		internal ConfigurationLocationCollection()
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004ABF File Offset: 0x00002CBF
		internal void Add(ConfigurationLocation loc)
		{
			base.InnerList.Add(loc);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004AD0 File Offset: 0x00002CD0
		internal ConfigurationLocation Find(string location)
		{
			foreach (object obj in base.InnerList)
			{
				ConfigurationLocation configurationLocation = (ConfigurationLocation)obj;
				if (string.Compare(configurationLocation.Path, location, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return configurationLocation;
				}
			}
			return null;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004B38 File Offset: 0x00002D38
		internal ConfigurationLocation FindBest(string location)
		{
			if (string.IsNullOrEmpty(location))
			{
				return null;
			}
			ConfigurationLocation configurationLocation = null;
			int length = location.Length;
			int num = 0;
			foreach (object obj in base.InnerList)
			{
				ConfigurationLocation configurationLocation2 = (ConfigurationLocation)obj;
				string path = configurationLocation2.Path;
				if (!string.IsNullOrEmpty(path))
				{
					int length2 = path.Length;
					if (location.StartsWith(path, StringComparison.OrdinalIgnoreCase))
					{
						if (length == length2)
						{
							return configurationLocation2;
						}
						if (length <= length2 || location[length2] == '/')
						{
							if (configurationLocation == null)
							{
								configurationLocation = configurationLocation2;
							}
							else if (num < length2)
							{
								configurationLocation = configurationLocation2;
								num = length2;
							}
						}
					}
				}
			}
			return configurationLocation;
		}
	}
}
