using System;

namespace System.IO
{
	// Token: 0x020007BA RID: 1978
	public class EnumerationOptions
	{
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x000F2B81 File Offset: 0x000F0D81
		internal static EnumerationOptions Compatible { get; } = new EnumerationOptions
		{
			MatchType = MatchType.Win32,
			AttributesToSkip = (FileAttributes)0,
			IgnoreInaccessible = false
		};

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06003F0E RID: 16142 RVA: 0x000F2B88 File Offset: 0x000F0D88
		private static EnumerationOptions CompatibleRecursive { get; } = new EnumerationOptions
		{
			RecurseSubdirectories = true,
			MatchType = MatchType.Win32,
			AttributesToSkip = (FileAttributes)0,
			IgnoreInaccessible = false
		};

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x000F2B8F File Offset: 0x000F0D8F
		internal static EnumerationOptions Default { get; } = new EnumerationOptions();

		// Token: 0x06003F10 RID: 16144 RVA: 0x000F2B96 File Offset: 0x000F0D96
		public EnumerationOptions()
		{
			this.IgnoreInaccessible = true;
			this.AttributesToSkip = FileAttributes.Hidden | FileAttributes.System;
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x000F2BAC File Offset: 0x000F0DAC
		internal static EnumerationOptions FromSearchOption(SearchOption searchOption)
		{
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", "Enum value was out of legal range.");
			}
			if (searchOption != SearchOption.AllDirectories)
			{
				return EnumerationOptions.Compatible;
			}
			return EnumerationOptions.CompatibleRecursive;
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06003F12 RID: 16146 RVA: 0x000F2BD4 File Offset: 0x000F0DD4
		// (set) Token: 0x06003F13 RID: 16147 RVA: 0x000F2BDC File Offset: 0x000F0DDC
		public bool RecurseSubdirectories { get; set; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06003F14 RID: 16148 RVA: 0x000F2BE5 File Offset: 0x000F0DE5
		// (set) Token: 0x06003F15 RID: 16149 RVA: 0x000F2BED File Offset: 0x000F0DED
		public bool IgnoreInaccessible { get; set; }

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x000F2BF6 File Offset: 0x000F0DF6
		public int BufferSize { get; }

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06003F17 RID: 16151 RVA: 0x000F2BFE File Offset: 0x000F0DFE
		// (set) Token: 0x06003F18 RID: 16152 RVA: 0x000F2C06 File Offset: 0x000F0E06
		public FileAttributes AttributesToSkip { get; set; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x000F2C0F File Offset: 0x000F0E0F
		// (set) Token: 0x06003F1A RID: 16154 RVA: 0x000F2C17 File Offset: 0x000F0E17
		public MatchType MatchType { get; set; }

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06003F1B RID: 16155 RVA: 0x000F2C20 File Offset: 0x000F0E20
		public MatchCasing MatchCasing { get; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x000F2C28 File Offset: 0x000F0E28
		public bool ReturnSpecialDirectories { get; }
	}
}
