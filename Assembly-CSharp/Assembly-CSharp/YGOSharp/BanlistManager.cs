using System;
using System.Collections.Generic;
using System.IO;

namespace YGOSharp
{
	// Token: 0x020001B0 RID: 432
	public static class BanlistManager
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001FB6D File Offset: 0x0001DD6D
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0001FB74 File Offset: 0x0001DD74
		public static List<Banlist> Banlists { get; private set; }

		// Token: 0x06000672 RID: 1650 RVA: 0x0001FB7C File Offset: 0x0001DD7C
		public static void Init(string fileName)
		{
			BanlistManager.Banlists = new List<Banlist>();
			Banlist current = null;
			StreamReader reader = new StreamReader(fileName);
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				if (line != null && !line.StartsWith("#"))
				{
					if (line.StartsWith("!"))
					{
						current = new Banlist();
						BanlistManager.Banlists.Add(current);
					}
					else if (line.Contains(" ") && current != null)
					{
						string[] array = line.Split(' ', StringSplitOptions.None);
						int id = int.Parse(array[0]);
						int count = int.Parse(array[1]);
						current.Add(id, count);
					}
				}
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001FC14 File Offset: 0x0001DE14
		public static int GetIndex(uint hash)
		{
			for (int i = 0; i < BanlistManager.Banlists.Count; i++)
			{
				if (BanlistManager.Banlists[i].Hash == hash)
				{
					return i;
				}
			}
			return 0;
		}
	}
}
