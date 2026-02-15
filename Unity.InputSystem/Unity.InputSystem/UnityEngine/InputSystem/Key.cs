using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200008D RID: 141
	public enum Key
	{
		// Token: 0x0400033C RID: 828
		None,
		// Token: 0x0400033D RID: 829
		Space,
		// Token: 0x0400033E RID: 830
		Enter,
		// Token: 0x0400033F RID: 831
		Tab,
		// Token: 0x04000340 RID: 832
		Backquote,
		// Token: 0x04000341 RID: 833
		Quote,
		// Token: 0x04000342 RID: 834
		Semicolon,
		// Token: 0x04000343 RID: 835
		Comma,
		// Token: 0x04000344 RID: 836
		Period,
		// Token: 0x04000345 RID: 837
		Slash,
		// Token: 0x04000346 RID: 838
		Backslash,
		// Token: 0x04000347 RID: 839
		LeftBracket,
		// Token: 0x04000348 RID: 840
		RightBracket,
		// Token: 0x04000349 RID: 841
		Minus,
		// Token: 0x0400034A RID: 842
		Equals,
		// Token: 0x0400034B RID: 843
		A,
		// Token: 0x0400034C RID: 844
		B,
		// Token: 0x0400034D RID: 845
		C,
		// Token: 0x0400034E RID: 846
		D,
		// Token: 0x0400034F RID: 847
		E,
		// Token: 0x04000350 RID: 848
		F,
		// Token: 0x04000351 RID: 849
		G,
		// Token: 0x04000352 RID: 850
		H,
		// Token: 0x04000353 RID: 851
		I,
		// Token: 0x04000354 RID: 852
		J,
		// Token: 0x04000355 RID: 853
		K,
		// Token: 0x04000356 RID: 854
		L,
		// Token: 0x04000357 RID: 855
		M,
		// Token: 0x04000358 RID: 856
		N,
		// Token: 0x04000359 RID: 857
		O,
		// Token: 0x0400035A RID: 858
		P,
		// Token: 0x0400035B RID: 859
		Q,
		// Token: 0x0400035C RID: 860
		R,
		// Token: 0x0400035D RID: 861
		S,
		// Token: 0x0400035E RID: 862
		T,
		// Token: 0x0400035F RID: 863
		U,
		// Token: 0x04000360 RID: 864
		V,
		// Token: 0x04000361 RID: 865
		W,
		// Token: 0x04000362 RID: 866
		X,
		// Token: 0x04000363 RID: 867
		Y,
		// Token: 0x04000364 RID: 868
		Z,
		// Token: 0x04000365 RID: 869
		Digit1,
		// Token: 0x04000366 RID: 870
		Digit2,
		// Token: 0x04000367 RID: 871
		Digit3,
		// Token: 0x04000368 RID: 872
		Digit4,
		// Token: 0x04000369 RID: 873
		Digit5,
		// Token: 0x0400036A RID: 874
		Digit6,
		// Token: 0x0400036B RID: 875
		Digit7,
		// Token: 0x0400036C RID: 876
		Digit8,
		// Token: 0x0400036D RID: 877
		Digit9,
		// Token: 0x0400036E RID: 878
		Digit0,
		// Token: 0x0400036F RID: 879
		LeftShift,
		// Token: 0x04000370 RID: 880
		RightShift,
		// Token: 0x04000371 RID: 881
		LeftAlt,
		// Token: 0x04000372 RID: 882
		RightAlt,
		// Token: 0x04000373 RID: 883
		AltGr = 54,
		// Token: 0x04000374 RID: 884
		LeftCtrl,
		// Token: 0x04000375 RID: 885
		RightCtrl,
		// Token: 0x04000376 RID: 886
		LeftMeta,
		// Token: 0x04000377 RID: 887
		RightMeta,
		// Token: 0x04000378 RID: 888
		LeftWindows = 57,
		// Token: 0x04000379 RID: 889
		RightWindows,
		// Token: 0x0400037A RID: 890
		LeftApple = 57,
		// Token: 0x0400037B RID: 891
		RightApple,
		// Token: 0x0400037C RID: 892
		LeftCommand = 57,
		// Token: 0x0400037D RID: 893
		RightCommand,
		// Token: 0x0400037E RID: 894
		ContextMenu,
		// Token: 0x0400037F RID: 895
		Escape,
		// Token: 0x04000380 RID: 896
		LeftArrow,
		// Token: 0x04000381 RID: 897
		RightArrow,
		// Token: 0x04000382 RID: 898
		UpArrow,
		// Token: 0x04000383 RID: 899
		DownArrow,
		// Token: 0x04000384 RID: 900
		Backspace,
		// Token: 0x04000385 RID: 901
		PageDown,
		// Token: 0x04000386 RID: 902
		PageUp,
		// Token: 0x04000387 RID: 903
		Home,
		// Token: 0x04000388 RID: 904
		End,
		// Token: 0x04000389 RID: 905
		Insert,
		// Token: 0x0400038A RID: 906
		Delete,
		// Token: 0x0400038B RID: 907
		CapsLock,
		// Token: 0x0400038C RID: 908
		NumLock,
		// Token: 0x0400038D RID: 909
		PrintScreen,
		// Token: 0x0400038E RID: 910
		ScrollLock,
		// Token: 0x0400038F RID: 911
		Pause,
		// Token: 0x04000390 RID: 912
		NumpadEnter,
		// Token: 0x04000391 RID: 913
		NumpadDivide,
		// Token: 0x04000392 RID: 914
		NumpadMultiply,
		// Token: 0x04000393 RID: 915
		NumpadPlus,
		// Token: 0x04000394 RID: 916
		NumpadMinus,
		// Token: 0x04000395 RID: 917
		NumpadPeriod,
		// Token: 0x04000396 RID: 918
		NumpadEquals,
		// Token: 0x04000397 RID: 919
		Numpad0,
		// Token: 0x04000398 RID: 920
		Numpad1,
		// Token: 0x04000399 RID: 921
		Numpad2,
		// Token: 0x0400039A RID: 922
		Numpad3,
		// Token: 0x0400039B RID: 923
		Numpad4,
		// Token: 0x0400039C RID: 924
		Numpad5,
		// Token: 0x0400039D RID: 925
		Numpad6,
		// Token: 0x0400039E RID: 926
		Numpad7,
		// Token: 0x0400039F RID: 927
		Numpad8,
		// Token: 0x040003A0 RID: 928
		Numpad9,
		// Token: 0x040003A1 RID: 929
		F1,
		// Token: 0x040003A2 RID: 930
		F2,
		// Token: 0x040003A3 RID: 931
		F3,
		// Token: 0x040003A4 RID: 932
		F4,
		// Token: 0x040003A5 RID: 933
		F5,
		// Token: 0x040003A6 RID: 934
		F6,
		// Token: 0x040003A7 RID: 935
		F7,
		// Token: 0x040003A8 RID: 936
		F8,
		// Token: 0x040003A9 RID: 937
		F9,
		// Token: 0x040003AA RID: 938
		F10,
		// Token: 0x040003AB RID: 939
		F11,
		// Token: 0x040003AC RID: 940
		F12,
		// Token: 0x040003AD RID: 941
		OEM1,
		// Token: 0x040003AE RID: 942
		OEM2,
		// Token: 0x040003AF RID: 943
		OEM3,
		// Token: 0x040003B0 RID: 944
		OEM4,
		// Token: 0x040003B1 RID: 945
		OEM5,
		// Token: 0x040003B2 RID: 946
		IMESelected
	}
}
