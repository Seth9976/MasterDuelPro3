using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the different patterns available for <see cref="T:System.Drawing.Drawing2D.HatchBrush" /> objects.</summary>
	// Token: 0x0200009A RID: 154
	public enum HatchStyle
	{
		/// <summary>A pattern of horizontal lines.</summary>
		// Token: 0x040002E2 RID: 738
		Horizontal,
		/// <summary>A pattern of vertical lines.</summary>
		// Token: 0x040002E3 RID: 739
		Vertical,
		/// <summary>A pattern of lines on a diagonal from upper left to lower right.</summary>
		// Token: 0x040002E4 RID: 740
		ForwardDiagonal,
		/// <summary>A pattern of lines on a diagonal from upper right to lower left.</summary>
		// Token: 0x040002E5 RID: 741
		BackwardDiagonal,
		/// <summary>Specifies horizontal and vertical lines that cross.</summary>
		// Token: 0x040002E6 RID: 742
		Cross,
		/// <summary>A pattern of crisscross diagonal lines.</summary>
		// Token: 0x040002E7 RID: 743
		DiagonalCross,
		/// <summary>Specifies a 5-percent hatch. The ratio of foreground color to background color is 5:95.</summary>
		// Token: 0x040002E8 RID: 744
		Percent05,
		/// <summary>Specifies a 10-percent hatch. The ratio of foreground color to background color is 10:90.</summary>
		// Token: 0x040002E9 RID: 745
		Percent10,
		/// <summary>Specifies a 20-percent hatch. The ratio of foreground color to background color is 20:80.</summary>
		// Token: 0x040002EA RID: 746
		Percent20,
		/// <summary>Specifies a 25-percent hatch. The ratio of foreground color to background color is 25:75.</summary>
		// Token: 0x040002EB RID: 747
		Percent25,
		/// <summary>Specifies a 30-percent hatch. The ratio of foreground color to background color is 30:70.</summary>
		// Token: 0x040002EC RID: 748
		Percent30,
		/// <summary>Specifies a 40-percent hatch. The ratio of foreground color to background color is 40:60.</summary>
		// Token: 0x040002ED RID: 749
		Percent40,
		/// <summary>Specifies a 50-percent hatch. The ratio of foreground color to background color is 50:50.</summary>
		// Token: 0x040002EE RID: 750
		Percent50,
		/// <summary>Specifies a 60-percent hatch. The ratio of foreground color to background color is 60:40.</summary>
		// Token: 0x040002EF RID: 751
		Percent60,
		/// <summary>Specifies a 70-percent hatch. The ratio of foreground color to background color is 70:30.</summary>
		// Token: 0x040002F0 RID: 752
		Percent70,
		/// <summary>Specifies a 75-percent hatch. The ratio of foreground color to background color is 75:25.</summary>
		// Token: 0x040002F1 RID: 753
		Percent75,
		/// <summary>Specifies a 80-percent hatch. The ratio of foreground color to background color is 80:100.</summary>
		// Token: 0x040002F2 RID: 754
		Percent80,
		/// <summary>Specifies a 90-percent hatch. The ratio of foreground color to background color is 90:10.</summary>
		// Token: 0x040002F3 RID: 755
		Percent90,
		/// <summary>Specifies diagonal lines that slant to the right from top points to bottom points and are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />, but are not antialiased.</summary>
		// Token: 0x040002F4 RID: 756
		LightDownwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the left from top points to bottom points and are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, but they are not antialiased.</summary>
		// Token: 0x040002F5 RID: 757
		LightUpwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the right from top points to bottom points, are spaced 50 percent closer together than, and are twice the width of <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />. This hatch pattern is not antialiased.</summary>
		// Token: 0x040002F6 RID: 758
		DarkDownwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the left from top points to bottom points, are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, and are twice its width, but the lines are not antialiased.</summary>
		// Token: 0x040002F7 RID: 759
		DarkUpwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the right from top points to bottom points, have the same spacing as hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal" />, and are triple its width, but are not antialiased.</summary>
		// Token: 0x040002F8 RID: 760
		WideDownwardDiagonal,
		/// <summary>Specifies diagonal lines that slant to the left from top points to bottom points, have the same spacing as hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal" />, and are triple its width, but are not antialiased.</summary>
		// Token: 0x040002F9 RID: 761
		WideUpwardDiagonal,
		/// <summary>Specifies vertical lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" />.</summary>
		// Token: 0x040002FA RID: 762
		LightVertical,
		/// <summary>Specifies horizontal lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
		// Token: 0x040002FB RID: 763
		LightHorizontal,
		/// <summary>Specifies vertical lines that are spaced 75 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" /> (or 25 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.LightVertical" />).</summary>
		// Token: 0x040002FC RID: 764
		NarrowVertical,
		/// <summary>Specifies horizontal lines that are spaced 75 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" /> (or 25 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.LightHorizontal" />).</summary>
		// Token: 0x040002FD RID: 765
		NarrowHorizontal,
		/// <summary>Specifies vertical lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Vertical" /> and are twice its width.</summary>
		// Token: 0x040002FE RID: 766
		DarkVertical,
		/// <summary>Specifies horizontal lines that are spaced 50 percent closer together than <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" /> and are twice the width of <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
		// Token: 0x040002FF RID: 767
		DarkHorizontal,
		/// <summary>Specifies dashed diagonal lines, that slant to the right from top points to bottom points.</summary>
		// Token: 0x04000300 RID: 768
		DashedDownwardDiagonal,
		/// <summary>Specifies dashed diagonal lines, that slant to the left from top points to bottom points.</summary>
		// Token: 0x04000301 RID: 769
		DashedUpwardDiagonal,
		/// <summary>Specifies dashed horizontal lines.</summary>
		// Token: 0x04000302 RID: 770
		DashedHorizontal,
		/// <summary>Specifies dashed vertical lines.</summary>
		// Token: 0x04000303 RID: 771
		DashedVertical,
		/// <summary>Specifies a hatch that has the appearance of confetti.</summary>
		// Token: 0x04000304 RID: 772
		SmallConfetti,
		/// <summary>Specifies a hatch that has the appearance of confetti, and is composed of larger pieces than <see cref="F:System.Drawing.Drawing2D.HatchStyle.SmallConfetti" />.</summary>
		// Token: 0x04000305 RID: 773
		LargeConfetti,
		/// <summary>Specifies horizontal lines that are composed of zigzags.</summary>
		// Token: 0x04000306 RID: 774
		ZigZag,
		/// <summary>Specifies horizontal lines that are composed of tildes.</summary>
		// Token: 0x04000307 RID: 775
		Wave,
		/// <summary>Specifies a hatch that has the appearance of layered bricks that slant to the left from top points to bottom points.</summary>
		// Token: 0x04000308 RID: 776
		DiagonalBrick,
		/// <summary>Specifies a hatch that has the appearance of horizontally layered bricks.</summary>
		// Token: 0x04000309 RID: 777
		HorizontalBrick,
		/// <summary>Specifies a hatch that has the appearance of a woven material.</summary>
		// Token: 0x0400030A RID: 778
		Weave,
		/// <summary>Specifies a hatch that has the appearance of a plaid material.</summary>
		// Token: 0x0400030B RID: 779
		Plaid,
		/// <summary>Specifies a hatch that has the appearance of divots.</summary>
		// Token: 0x0400030C RID: 780
		Divot,
		/// <summary>Specifies horizontal and vertical lines, each of which is composed of dots, that cross.</summary>
		// Token: 0x0400030D RID: 781
		DottedGrid,
		/// <summary>Specifies forward diagonal and backward diagonal lines, each of which is composed of dots, that cross.</summary>
		// Token: 0x0400030E RID: 782
		DottedDiamond,
		/// <summary>Specifies a hatch that has the appearance of diagonally layered shingles that slant to the right from top points to bottom points.</summary>
		// Token: 0x0400030F RID: 783
		Shingle,
		/// <summary>Specifies a hatch that has the appearance of a trellis.</summary>
		// Token: 0x04000310 RID: 784
		Trellis,
		/// <summary>Specifies a hatch that has the appearance of spheres laid adjacent to one another.</summary>
		// Token: 0x04000311 RID: 785
		Sphere,
		/// <summary>Specifies horizontal and vertical lines that cross and are spaced 50 percent closer together than hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Cross" />.</summary>
		// Token: 0x04000312 RID: 786
		SmallGrid,
		/// <summary>Specifies a hatch that has the appearance of a checkerboard.</summary>
		// Token: 0x04000313 RID: 787
		SmallCheckerBoard,
		/// <summary>Specifies a hatch that has the appearance of a checkerboard with squares that are twice the size of <see cref="F:System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard" />.</summary>
		// Token: 0x04000314 RID: 788
		LargeCheckerBoard,
		/// <summary>Specifies forward diagonal and backward diagonal lines that cross but are not antialiased.</summary>
		// Token: 0x04000315 RID: 789
		OutlinedDiamond,
		/// <summary>Specifies a hatch that has the appearance of a checkerboard placed diagonally.</summary>
		// Token: 0x04000316 RID: 790
		SolidDiamond,
		/// <summary>Specifies the hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Cross" />.</summary>
		// Token: 0x04000317 RID: 791
		LargeGrid = 4,
		/// <summary>Specifies hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.Horizontal" />.</summary>
		// Token: 0x04000318 RID: 792
		Min = 0,
		/// <summary>Specifies hatch style <see cref="F:System.Drawing.Drawing2D.HatchStyle.SolidDiamond" />.</summary>
		// Token: 0x04000319 RID: 793
		Max = 4
	}
}
