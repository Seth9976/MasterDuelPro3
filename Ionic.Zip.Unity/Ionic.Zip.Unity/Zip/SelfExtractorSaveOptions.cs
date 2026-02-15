using System;

namespace Ionic.Zip
{
	// Token: 0x0200003C RID: 60
	public class SelfExtractorSaveOptions
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000FCB0 File Offset: 0x0000DEB0
		public SelfExtractorFlavor Flavor { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000FCB9 File Offset: 0x0000DEB9
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000FCC1 File Offset: 0x0000DEC1
		public string PostExtractCommandLine { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000FCCA File Offset: 0x0000DECA
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000FCD2 File Offset: 0x0000DED2
		public string DefaultExtractDirectory { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000FCDB File Offset: 0x0000DEDB
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000FCE3 File Offset: 0x0000DEE3
		public string IconFile { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000FCEC File Offset: 0x0000DEEC
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000FCF4 File Offset: 0x0000DEF4
		public bool Quiet { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000FCFD File Offset: 0x0000DEFD
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000FD05 File Offset: 0x0000DF05
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000FD0E File Offset: 0x0000DF0E
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000FD16 File Offset: 0x0000DF16
		public bool RemoveUnpackedFilesAfterExecute { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000FD1F File Offset: 0x0000DF1F
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000FD27 File Offset: 0x0000DF27
		public Version FileVersion { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000FD30 File Offset: 0x0000DF30
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000FD38 File Offset: 0x0000DF38
		public string ProductVersion { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000FD41 File Offset: 0x0000DF41
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000FD49 File Offset: 0x0000DF49
		public string Copyright { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000FD52 File Offset: 0x0000DF52
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000FD5A File Offset: 0x0000DF5A
		public string Description { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000FD63 File Offset: 0x0000DF63
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000FD6B File Offset: 0x0000DF6B
		public string ProductName { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000FD74 File Offset: 0x0000DF74
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		public string SfxExeWindowTitle { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000FD85 File Offset: 0x0000DF85
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000FD8D File Offset: 0x0000DF8D
		public string AdditionalCompilerSwitches { get; set; }
	}
}
