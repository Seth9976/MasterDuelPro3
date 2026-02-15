using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers for the standard set of commands that are available to most applications.</summary>
	// Token: 0x020002EA RID: 746
	public class StandardCommands
	{
		// Token: 0x04000AEC RID: 2796
		private static readonly Guid s_standardCommandSet = StandardCommands.ShellGuids.VSStandardCommandSet97;

		// Token: 0x04000AED RID: 2797
		private static readonly Guid s_ndpCommandSet = new Guid("{74D21313-2AEE-11d1-8BFB-00A0C90F26F7}");

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignBottom command. This field is read-only.</summary>
		// Token: 0x04000AEE RID: 2798
		public static readonly CommandID AlignBottom = new CommandID(StandardCommands.s_standardCommandSet, 1);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignHorizontalCenters command. This field is read-only.</summary>
		// Token: 0x04000AEF RID: 2799
		public static readonly CommandID AlignHorizontalCenters = new CommandID(StandardCommands.s_standardCommandSet, 2);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignLeft command. This field is read-only.</summary>
		// Token: 0x04000AF0 RID: 2800
		public static readonly CommandID AlignLeft = new CommandID(StandardCommands.s_standardCommandSet, 3);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignRight command. This field is read-only.</summary>
		// Token: 0x04000AF1 RID: 2801
		public static readonly CommandID AlignRight = new CommandID(StandardCommands.s_standardCommandSet, 4);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignToGrid command. This field is read-only.</summary>
		// Token: 0x04000AF2 RID: 2802
		public static readonly CommandID AlignToGrid = new CommandID(StandardCommands.s_standardCommandSet, 5);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignTop command. This field is read-only.</summary>
		// Token: 0x04000AF3 RID: 2803
		public static readonly CommandID AlignTop = new CommandID(StandardCommands.s_standardCommandSet, 6);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignVerticalCenters command. This field is read-only.</summary>
		// Token: 0x04000AF4 RID: 2804
		public static readonly CommandID AlignVerticalCenters = new CommandID(StandardCommands.s_standardCommandSet, 7);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeBottom command. This field is read-only.</summary>
		// Token: 0x04000AF5 RID: 2805
		public static readonly CommandID ArrangeBottom = new CommandID(StandardCommands.s_standardCommandSet, 8);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeRight command. This field is read-only.</summary>
		// Token: 0x04000AF6 RID: 2806
		public static readonly CommandID ArrangeRight = new CommandID(StandardCommands.s_standardCommandSet, 9);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringForward command. This field is read-only.</summary>
		// Token: 0x04000AF7 RID: 2807
		public static readonly CommandID BringForward = new CommandID(StandardCommands.s_standardCommandSet, 10);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringToFront command. This field is read-only.</summary>
		// Token: 0x04000AF8 RID: 2808
		public static readonly CommandID BringToFront = new CommandID(StandardCommands.s_standardCommandSet, 11);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterHorizontally command. This field is read-only.</summary>
		// Token: 0x04000AF9 RID: 2809
		public static readonly CommandID CenterHorizontally = new CommandID(StandardCommands.s_standardCommandSet, 12);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterVertically command. This field is read-only.</summary>
		// Token: 0x04000AFA RID: 2810
		public static readonly CommandID CenterVertically = new CommandID(StandardCommands.s_standardCommandSet, 13);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewCode command. This field is read-only.</summary>
		// Token: 0x04000AFB RID: 2811
		public static readonly CommandID ViewCode = new CommandID(StandardCommands.s_standardCommandSet, 333);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Document Outline command. This field is read-only.</summary>
		// Token: 0x04000AFC RID: 2812
		public static readonly CommandID DocumentOutline = new CommandID(StandardCommands.s_standardCommandSet, 239);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Copy command. This field is read-only.</summary>
		// Token: 0x04000AFD RID: 2813
		public static readonly CommandID Copy = new CommandID(StandardCommands.s_standardCommandSet, 15);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Cut command. This field is read-only.</summary>
		// Token: 0x04000AFE RID: 2814
		public static readonly CommandID Cut = new CommandID(StandardCommands.s_standardCommandSet, 16);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Delete command. This field is read-only.</summary>
		// Token: 0x04000AFF RID: 2815
		public static readonly CommandID Delete = new CommandID(StandardCommands.s_standardCommandSet, 17);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Group command. This field is read-only.</summary>
		// Token: 0x04000B00 RID: 2816
		public static readonly CommandID Group = new CommandID(StandardCommands.s_standardCommandSet, 20);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x04000B01 RID: 2817
		public static readonly CommandID HorizSpaceConcatenate = new CommandID(StandardCommands.s_standardCommandSet, 21);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x04000B02 RID: 2818
		public static readonly CommandID HorizSpaceDecrease = new CommandID(StandardCommands.s_standardCommandSet, 22);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x04000B03 RID: 2819
		public static readonly CommandID HorizSpaceIncrease = new CommandID(StandardCommands.s_standardCommandSet, 23);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x04000B04 RID: 2820
		public static readonly CommandID HorizSpaceMakeEqual = new CommandID(StandardCommands.s_standardCommandSet, 24);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Paste command. This field is read-only.</summary>
		// Token: 0x04000B05 RID: 2821
		public static readonly CommandID Paste = new CommandID(StandardCommands.s_standardCommandSet, 26);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Properties command. This field is read-only.</summary>
		// Token: 0x04000B06 RID: 2822
		public static readonly CommandID Properties = new CommandID(StandardCommands.s_standardCommandSet, 28);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Redo command. This field is read-only.</summary>
		// Token: 0x04000B07 RID: 2823
		public static readonly CommandID Redo = new CommandID(StandardCommands.s_standardCommandSet, 29);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelRedo command. This field is read-only.</summary>
		// Token: 0x04000B08 RID: 2824
		public static readonly CommandID MultiLevelRedo = new CommandID(StandardCommands.s_standardCommandSet, 30);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SelectAll command. This field is read-only.</summary>
		// Token: 0x04000B09 RID: 2825
		public static readonly CommandID SelectAll = new CommandID(StandardCommands.s_standardCommandSet, 31);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendBackward command. This field is read-only.</summary>
		// Token: 0x04000B0A RID: 2826
		public static readonly CommandID SendBackward = new CommandID(StandardCommands.s_standardCommandSet, 32);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendToBack command. This field is read-only.</summary>
		// Token: 0x04000B0B RID: 2827
		public static readonly CommandID SendToBack = new CommandID(StandardCommands.s_standardCommandSet, 33);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControl command. This field is read-only.</summary>
		// Token: 0x04000B0C RID: 2828
		public static readonly CommandID SizeToControl = new CommandID(StandardCommands.s_standardCommandSet, 35);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlHeight command. This field is read-only.</summary>
		// Token: 0x04000B0D RID: 2829
		public static readonly CommandID SizeToControlHeight = new CommandID(StandardCommands.s_standardCommandSet, 36);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlWidth command. This field is read-only.</summary>
		// Token: 0x04000B0E RID: 2830
		public static readonly CommandID SizeToControlWidth = new CommandID(StandardCommands.s_standardCommandSet, 37);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToFit command. This field is read-only.</summary>
		// Token: 0x04000B0F RID: 2831
		public static readonly CommandID SizeToFit = new CommandID(StandardCommands.s_standardCommandSet, 38);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToGrid command. This field is read-only.</summary>
		// Token: 0x04000B10 RID: 2832
		public static readonly CommandID SizeToGrid = new CommandID(StandardCommands.s_standardCommandSet, 39);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SnapToGrid command. This field is read-only.</summary>
		// Token: 0x04000B11 RID: 2833
		public static readonly CommandID SnapToGrid = new CommandID(StandardCommands.s_standardCommandSet, 40);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the TabOrder command. This field is read-only.</summary>
		// Token: 0x04000B12 RID: 2834
		public static readonly CommandID TabOrder = new CommandID(StandardCommands.s_standardCommandSet, 41);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Undo command. This field is read-only.</summary>
		// Token: 0x04000B13 RID: 2835
		public static readonly CommandID Undo = new CommandID(StandardCommands.s_standardCommandSet, 43);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelUndo command. This field is read-only.</summary>
		// Token: 0x04000B14 RID: 2836
		public static readonly CommandID MultiLevelUndo = new CommandID(StandardCommands.s_standardCommandSet, 44);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Ungroup command. This field is read-only.</summary>
		// Token: 0x04000B15 RID: 2837
		public static readonly CommandID Ungroup = new CommandID(StandardCommands.s_standardCommandSet, 45);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x04000B16 RID: 2838
		public static readonly CommandID VertSpaceConcatenate = new CommandID(StandardCommands.s_standardCommandSet, 46);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x04000B17 RID: 2839
		public static readonly CommandID VertSpaceDecrease = new CommandID(StandardCommands.s_standardCommandSet, 47);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x04000B18 RID: 2840
		public static readonly CommandID VertSpaceIncrease = new CommandID(StandardCommands.s_standardCommandSet, 48);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x04000B19 RID: 2841
		public static readonly CommandID VertSpaceMakeEqual = new CommandID(StandardCommands.s_standardCommandSet, 49);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowGrid command. This field is read-only.</summary>
		// Token: 0x04000B1A RID: 2842
		public static readonly CommandID ShowGrid = new CommandID(StandardCommands.s_standardCommandSet, 103);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewGrid command. This field is read-only.</summary>
		// Token: 0x04000B1B RID: 2843
		public static readonly CommandID ViewGrid = new CommandID(StandardCommands.s_standardCommandSet, 125);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Replace command. This field is read-only.</summary>
		// Token: 0x04000B1C RID: 2844
		public static readonly CommandID Replace = new CommandID(StandardCommands.s_standardCommandSet, 230);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the PropertiesWindow command. This field is read-only.</summary>
		// Token: 0x04000B1D RID: 2845
		public static readonly CommandID PropertiesWindow = new CommandID(StandardCommands.s_standardCommandSet, 235);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LockControls command. This field is read-only.</summary>
		// Token: 0x04000B1E RID: 2846
		public static readonly CommandID LockControls = new CommandID(StandardCommands.s_standardCommandSet, 369);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the F1Help command. This field is read-only.</summary>
		// Token: 0x04000B1F RID: 2847
		public static readonly CommandID F1Help = new CommandID(StandardCommands.s_standardCommandSet, 377);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeIcons command. This field is read-only.</summary>
		// Token: 0x04000B20 RID: 2848
		public static readonly CommandID ArrangeIcons = new CommandID(StandardCommands.s_ndpCommandSet, 12298);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LineupIcons command. This field is read-only.</summary>
		// Token: 0x04000B21 RID: 2849
		public static readonly CommandID LineupIcons = new CommandID(StandardCommands.s_ndpCommandSet, 12299);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowLargeIcons command. This field is read-only.</summary>
		// Token: 0x04000B22 RID: 2850
		public static readonly CommandID ShowLargeIcons = new CommandID(StandardCommands.s_ndpCommandSet, 12300);

		/// <summary>Gets the first of a set of verbs. This field is read-only.</summary>
		// Token: 0x04000B23 RID: 2851
		public static readonly CommandID VerbFirst = new CommandID(StandardCommands.s_ndpCommandSet, 8192);

		/// <summary>Gets the last of a set of verbs. This field is read-only.</summary>
		// Token: 0x04000B24 RID: 2852
		public static readonly CommandID VerbLast = new CommandID(StandardCommands.s_ndpCommandSet, 8448);

		// Token: 0x020002EB RID: 747
		private static class ShellGuids
		{
			// Token: 0x04000B25 RID: 2853
			internal static readonly Guid VSStandardCommandSet97 = new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}");

			// Token: 0x04000B26 RID: 2854
			internal static readonly Guid guidDsdCmdId = new Guid("{1F0FD094-8e53-11d2-8f9c-0060089fc486}");

			// Token: 0x04000B27 RID: 2855
			internal static readonly Guid SID_SOleComponentUIManager = new Guid("{5efc7974-14bc-11cf-9b2b-00aa00573819}");

			// Token: 0x04000B28 RID: 2856
			internal static readonly Guid GUID_VSTASKCATEGORY_DATADESIGNER = new Guid("{6B32EAED-13BB-11d3-A64F-00C04F683820}");

			// Token: 0x04000B29 RID: 2857
			internal static readonly Guid GUID_PropertyBrowserToolWindow = new Guid(-285584864, -7528, 4560, new byte[] { 143, 120, 0, 160, 201, 17, 0, 87 });
		}
	}
}
