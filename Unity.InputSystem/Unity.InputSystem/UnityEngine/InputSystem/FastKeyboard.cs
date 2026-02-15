using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000093 RID: 147
	internal class FastKeyboard : Keyboard
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x0001BC1C File Offset: 0x00019E1C
		public FastKeyboard()
		{
			InputControlExtensions.DeviceBuilder builder = this.Setup(115, 15, 7).WithName("Keyboard").WithDisplayName("Keyboard")
				.WithChildren(0, 115)
				.WithLayout(new InternedString("Keyboard"))
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1262836051),
					sizeInBits = 112U
				});
			InternedString kAnyKeyLayout = new InternedString("AnyKey");
			InternedString kKeyLayout = new InternedString("Key");
			InternedString kDiscreteButtonLayout = new InternedString("DiscreteButton");
			InternedString kButtonLayout = new InternedString("Button");
			AnyKeyControl ctrlKeyboardanyKey = this.Initialize_ctrlKeyboardanyKey(kAnyKeyLayout, this);
			KeyControl ctrlKeyboardescape = this.Initialize_ctrlKeyboardescape(kKeyLayout, this);
			KeyControl ctrlKeyboardspace = this.Initialize_ctrlKeyboardspace(kKeyLayout, this);
			KeyControl ctrlKeyboardenter = this.Initialize_ctrlKeyboardenter(kKeyLayout, this);
			KeyControl ctrlKeyboardtab = this.Initialize_ctrlKeyboardtab(kKeyLayout, this);
			KeyControl ctrlKeyboardbackquote = this.Initialize_ctrlKeyboardbackquote(kKeyLayout, this);
			KeyControl ctrlKeyboardquote = this.Initialize_ctrlKeyboardquote(kKeyLayout, this);
			KeyControl ctrlKeyboardsemicolon = this.Initialize_ctrlKeyboardsemicolon(kKeyLayout, this);
			KeyControl ctrlKeyboardcomma = this.Initialize_ctrlKeyboardcomma(kKeyLayout, this);
			KeyControl ctrlKeyboardperiod = this.Initialize_ctrlKeyboardperiod(kKeyLayout, this);
			KeyControl ctrlKeyboardslash = this.Initialize_ctrlKeyboardslash(kKeyLayout, this);
			KeyControl ctrlKeyboardbackslash = this.Initialize_ctrlKeyboardbackslash(kKeyLayout, this);
			KeyControl ctrlKeyboardleftBracket = this.Initialize_ctrlKeyboardleftBracket(kKeyLayout, this);
			KeyControl ctrlKeyboardrightBracket = this.Initialize_ctrlKeyboardrightBracket(kKeyLayout, this);
			KeyControl ctrlKeyboardminus = this.Initialize_ctrlKeyboardminus(kKeyLayout, this);
			KeyControl ctrlKeyboardequals = this.Initialize_ctrlKeyboardequals(kKeyLayout, this);
			KeyControl ctrlKeyboardupArrow = this.Initialize_ctrlKeyboardupArrow(kKeyLayout, this);
			KeyControl ctrlKeyboarddownArrow = this.Initialize_ctrlKeyboarddownArrow(kKeyLayout, this);
			KeyControl ctrlKeyboardleftArrow = this.Initialize_ctrlKeyboardleftArrow(kKeyLayout, this);
			KeyControl ctrlKeyboardrightArrow = this.Initialize_ctrlKeyboardrightArrow(kKeyLayout, this);
			KeyControl ctrlKeyboarda = this.Initialize_ctrlKeyboarda(kKeyLayout, this);
			KeyControl ctrlKeyboardb = this.Initialize_ctrlKeyboardb(kKeyLayout, this);
			KeyControl ctrlKeyboardc = this.Initialize_ctrlKeyboardc(kKeyLayout, this);
			KeyControl ctrlKeyboardd = this.Initialize_ctrlKeyboardd(kKeyLayout, this);
			KeyControl ctrlKeyboarde = this.Initialize_ctrlKeyboarde(kKeyLayout, this);
			KeyControl ctrlKeyboardf = this.Initialize_ctrlKeyboardf(kKeyLayout, this);
			KeyControl ctrlKeyboardg = this.Initialize_ctrlKeyboardg(kKeyLayout, this);
			KeyControl ctrlKeyboardh = this.Initialize_ctrlKeyboardh(kKeyLayout, this);
			KeyControl ctrlKeyboardi = this.Initialize_ctrlKeyboardi(kKeyLayout, this);
			KeyControl ctrlKeyboardj = this.Initialize_ctrlKeyboardj(kKeyLayout, this);
			KeyControl ctrlKeyboardk = this.Initialize_ctrlKeyboardk(kKeyLayout, this);
			KeyControl ctrlKeyboardl = this.Initialize_ctrlKeyboardl(kKeyLayout, this);
			KeyControl ctrlKeyboardm = this.Initialize_ctrlKeyboardm(kKeyLayout, this);
			KeyControl ctrlKeyboardn = this.Initialize_ctrlKeyboardn(kKeyLayout, this);
			KeyControl ctrlKeyboardo = this.Initialize_ctrlKeyboardo(kKeyLayout, this);
			KeyControl ctrlKeyboardp = this.Initialize_ctrlKeyboardp(kKeyLayout, this);
			KeyControl ctrlKeyboardq = this.Initialize_ctrlKeyboardq(kKeyLayout, this);
			KeyControl ctrlKeyboardr = this.Initialize_ctrlKeyboardr(kKeyLayout, this);
			KeyControl ctrlKeyboards = this.Initialize_ctrlKeyboards(kKeyLayout, this);
			KeyControl ctrlKeyboardt = this.Initialize_ctrlKeyboardt(kKeyLayout, this);
			KeyControl ctrlKeyboardu = this.Initialize_ctrlKeyboardu(kKeyLayout, this);
			KeyControl ctrlKeyboardv = this.Initialize_ctrlKeyboardv(kKeyLayout, this);
			KeyControl ctrlKeyboardw = this.Initialize_ctrlKeyboardw(kKeyLayout, this);
			KeyControl ctrlKeyboardx = this.Initialize_ctrlKeyboardx(kKeyLayout, this);
			KeyControl ctrlKeyboardy = this.Initialize_ctrlKeyboardy(kKeyLayout, this);
			KeyControl ctrlKeyboardz = this.Initialize_ctrlKeyboardz(kKeyLayout, this);
			KeyControl ctrlKeyboard = this.Initialize_ctrlKeyboard1(kKeyLayout, this);
			KeyControl ctrlKeyboard2 = this.Initialize_ctrlKeyboard2(kKeyLayout, this);
			KeyControl ctrlKeyboard3 = this.Initialize_ctrlKeyboard3(kKeyLayout, this);
			KeyControl ctrlKeyboard4 = this.Initialize_ctrlKeyboard4(kKeyLayout, this);
			KeyControl ctrlKeyboard5 = this.Initialize_ctrlKeyboard5(kKeyLayout, this);
			KeyControl ctrlKeyboard6 = this.Initialize_ctrlKeyboard6(kKeyLayout, this);
			KeyControl ctrlKeyboard7 = this.Initialize_ctrlKeyboard7(kKeyLayout, this);
			KeyControl ctrlKeyboard8 = this.Initialize_ctrlKeyboard8(kKeyLayout, this);
			KeyControl ctrlKeyboard9 = this.Initialize_ctrlKeyboard9(kKeyLayout, this);
			KeyControl ctrlKeyboard10 = this.Initialize_ctrlKeyboard0(kKeyLayout, this);
			KeyControl ctrlKeyboardleftShift = this.Initialize_ctrlKeyboardleftShift(kKeyLayout, this);
			KeyControl ctrlKeyboardrightShift = this.Initialize_ctrlKeyboardrightShift(kKeyLayout, this);
			DiscreteButtonControl ctrlKeyboardshift = this.Initialize_ctrlKeyboardshift(kDiscreteButtonLayout, this);
			KeyControl ctrlKeyboardleftAlt = this.Initialize_ctrlKeyboardleftAlt(kKeyLayout, this);
			KeyControl ctrlKeyboardrightAlt = this.Initialize_ctrlKeyboardrightAlt(kKeyLayout, this);
			DiscreteButtonControl ctrlKeyboardalt = this.Initialize_ctrlKeyboardalt(kDiscreteButtonLayout, this);
			KeyControl ctrlKeyboardleftCtrl = this.Initialize_ctrlKeyboardleftCtrl(kKeyLayout, this);
			KeyControl ctrlKeyboardrightCtrl = this.Initialize_ctrlKeyboardrightCtrl(kKeyLayout, this);
			DiscreteButtonControl ctrlKeyboardctrl = this.Initialize_ctrlKeyboardctrl(kDiscreteButtonLayout, this);
			KeyControl ctrlKeyboardleftMeta = this.Initialize_ctrlKeyboardleftMeta(kKeyLayout, this);
			KeyControl ctrlKeyboardrightMeta = this.Initialize_ctrlKeyboardrightMeta(kKeyLayout, this);
			KeyControl ctrlKeyboardcontextMenu = this.Initialize_ctrlKeyboardcontextMenu(kKeyLayout, this);
			KeyControl ctrlKeyboardbackspace = this.Initialize_ctrlKeyboardbackspace(kKeyLayout, this);
			KeyControl ctrlKeyboardpageDown = this.Initialize_ctrlKeyboardpageDown(kKeyLayout, this);
			KeyControl ctrlKeyboardpageUp = this.Initialize_ctrlKeyboardpageUp(kKeyLayout, this);
			KeyControl ctrlKeyboardhome = this.Initialize_ctrlKeyboardhome(kKeyLayout, this);
			KeyControl ctrlKeyboardend = this.Initialize_ctrlKeyboardend(kKeyLayout, this);
			KeyControl ctrlKeyboardinsert = this.Initialize_ctrlKeyboardinsert(kKeyLayout, this);
			KeyControl ctrlKeyboarddelete = this.Initialize_ctrlKeyboarddelete(kKeyLayout, this);
			KeyControl ctrlKeyboardcapsLock = this.Initialize_ctrlKeyboardcapsLock(kKeyLayout, this);
			KeyControl ctrlKeyboardnumLock = this.Initialize_ctrlKeyboardnumLock(kKeyLayout, this);
			KeyControl ctrlKeyboardprintScreen = this.Initialize_ctrlKeyboardprintScreen(kKeyLayout, this);
			KeyControl ctrlKeyboardscrollLock = this.Initialize_ctrlKeyboardscrollLock(kKeyLayout, this);
			KeyControl ctrlKeyboardpause = this.Initialize_ctrlKeyboardpause(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpadEnter = this.Initialize_ctrlKeyboardnumpadEnter(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpadDivide = this.Initialize_ctrlKeyboardnumpadDivide(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpadMultiply = this.Initialize_ctrlKeyboardnumpadMultiply(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpadPlus = this.Initialize_ctrlKeyboardnumpadPlus(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpadMinus = this.Initialize_ctrlKeyboardnumpadMinus(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpadPeriod = this.Initialize_ctrlKeyboardnumpadPeriod(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpadEquals = this.Initialize_ctrlKeyboardnumpadEquals(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpad = this.Initialize_ctrlKeyboardnumpad1(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpad2 = this.Initialize_ctrlKeyboardnumpad2(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpad3 = this.Initialize_ctrlKeyboardnumpad3(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpad4 = this.Initialize_ctrlKeyboardnumpad4(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpad5 = this.Initialize_ctrlKeyboardnumpad5(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpad6 = this.Initialize_ctrlKeyboardnumpad6(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpad7 = this.Initialize_ctrlKeyboardnumpad7(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpad8 = this.Initialize_ctrlKeyboardnumpad8(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpad9 = this.Initialize_ctrlKeyboardnumpad9(kKeyLayout, this);
			KeyControl ctrlKeyboardnumpad10 = this.Initialize_ctrlKeyboardnumpad0(kKeyLayout, this);
			KeyControl ctrlKeyboardf2 = this.Initialize_ctrlKeyboardf1(kKeyLayout, this);
			KeyControl ctrlKeyboardf3 = this.Initialize_ctrlKeyboardf2(kKeyLayout, this);
			KeyControl ctrlKeyboardf4 = this.Initialize_ctrlKeyboardf3(kKeyLayout, this);
			KeyControl ctrlKeyboardf5 = this.Initialize_ctrlKeyboardf4(kKeyLayout, this);
			KeyControl ctrlKeyboardf6 = this.Initialize_ctrlKeyboardf5(kKeyLayout, this);
			KeyControl ctrlKeyboardf7 = this.Initialize_ctrlKeyboardf6(kKeyLayout, this);
			KeyControl ctrlKeyboardf8 = this.Initialize_ctrlKeyboardf7(kKeyLayout, this);
			KeyControl ctrlKeyboardf9 = this.Initialize_ctrlKeyboardf8(kKeyLayout, this);
			KeyControl ctrlKeyboardf10 = this.Initialize_ctrlKeyboardf9(kKeyLayout, this);
			KeyControl ctrlKeyboardf11 = this.Initialize_ctrlKeyboardf10(kKeyLayout, this);
			KeyControl ctrlKeyboardf12 = this.Initialize_ctrlKeyboardf11(kKeyLayout, this);
			KeyControl ctrlKeyboardf13 = this.Initialize_ctrlKeyboardf12(kKeyLayout, this);
			KeyControl ctrlKeyboardOEM = this.Initialize_ctrlKeyboardOEM1(kKeyLayout, this);
			KeyControl ctrlKeyboardOEM2 = this.Initialize_ctrlKeyboardOEM2(kKeyLayout, this);
			KeyControl ctrlKeyboardOEM3 = this.Initialize_ctrlKeyboardOEM3(kKeyLayout, this);
			KeyControl ctrlKeyboardOEM4 = this.Initialize_ctrlKeyboardOEM4(kKeyLayout, this);
			KeyControl ctrlKeyboardOEM5 = this.Initialize_ctrlKeyboardOEM5(kKeyLayout, this);
			ButtonControl ctrlKeyboardIMESelected = this.Initialize_ctrlKeyboardIMESelected(kButtonLayout, this);
			builder.WithControlUsage(0, new InternedString("Back"), ctrlKeyboardescape);
			builder.WithControlUsage(1, new InternedString("Cancel"), ctrlKeyboardescape);
			builder.WithControlUsage(2, new InternedString("Submit"), ctrlKeyboardenter);
			builder.WithControlUsage(3, new InternedString("Modifier"), ctrlKeyboardleftShift);
			builder.WithControlUsage(4, new InternedString("Modifier"), ctrlKeyboardrightShift);
			builder.WithControlUsage(5, new InternedString("Modifier"), ctrlKeyboardshift);
			builder.WithControlUsage(6, new InternedString("Modifier"), ctrlKeyboardleftAlt);
			builder.WithControlUsage(7, new InternedString("Modifier"), ctrlKeyboardrightAlt);
			builder.WithControlUsage(8, new InternedString("Modifier"), ctrlKeyboardalt);
			builder.WithControlUsage(9, new InternedString("Modifier"), ctrlKeyboardleftCtrl);
			builder.WithControlUsage(10, new InternedString("Modifier"), ctrlKeyboardrightCtrl);
			builder.WithControlUsage(11, new InternedString("Modifier"), ctrlKeyboardctrl);
			builder.WithControlUsage(12, new InternedString("Modifier"), ctrlKeyboardleftMeta);
			builder.WithControlUsage(13, new InternedString("Modifier"), ctrlKeyboardrightMeta);
			builder.WithControlUsage(14, new InternedString("Modifier"), ctrlKeyboardcontextMenu);
			builder.WithControlAlias(0, new InternedString("AltGr"));
			builder.WithControlAlias(1, new InternedString("LeftWindows"));
			builder.WithControlAlias(2, new InternedString("LeftApple"));
			builder.WithControlAlias(3, new InternedString("LeftCommand"));
			builder.WithControlAlias(4, new InternedString("RightWindows"));
			builder.WithControlAlias(5, new InternedString("RightApple"));
			builder.WithControlAlias(6, new InternedString("RightCommand"));
			base.keys = new KeyControl[110];
			base.keys[0] = ctrlKeyboardspace;
			base.keys[1] = ctrlKeyboardenter;
			base.keys[2] = ctrlKeyboardtab;
			base.keys[3] = ctrlKeyboardbackquote;
			base.keys[4] = ctrlKeyboardquote;
			base.keys[5] = ctrlKeyboardsemicolon;
			base.keys[6] = ctrlKeyboardcomma;
			base.keys[7] = ctrlKeyboardperiod;
			base.keys[8] = ctrlKeyboardslash;
			base.keys[9] = ctrlKeyboardbackslash;
			base.keys[10] = ctrlKeyboardleftBracket;
			base.keys[11] = ctrlKeyboardrightBracket;
			base.keys[12] = ctrlKeyboardminus;
			base.keys[13] = ctrlKeyboardequals;
			base.keys[14] = ctrlKeyboarda;
			base.keys[15] = ctrlKeyboardb;
			base.keys[16] = ctrlKeyboardc;
			base.keys[17] = ctrlKeyboardd;
			base.keys[18] = ctrlKeyboarde;
			base.keys[19] = ctrlKeyboardf;
			base.keys[20] = ctrlKeyboardg;
			base.keys[21] = ctrlKeyboardh;
			base.keys[22] = ctrlKeyboardi;
			base.keys[23] = ctrlKeyboardj;
			base.keys[24] = ctrlKeyboardk;
			base.keys[25] = ctrlKeyboardl;
			base.keys[26] = ctrlKeyboardm;
			base.keys[27] = ctrlKeyboardn;
			base.keys[28] = ctrlKeyboardo;
			base.keys[29] = ctrlKeyboardp;
			base.keys[30] = ctrlKeyboardq;
			base.keys[31] = ctrlKeyboardr;
			base.keys[32] = ctrlKeyboards;
			base.keys[33] = ctrlKeyboardt;
			base.keys[34] = ctrlKeyboardu;
			base.keys[35] = ctrlKeyboardv;
			base.keys[36] = ctrlKeyboardw;
			base.keys[37] = ctrlKeyboardx;
			base.keys[38] = ctrlKeyboardy;
			base.keys[39] = ctrlKeyboardz;
			base.keys[40] = ctrlKeyboard;
			base.keys[41] = ctrlKeyboard2;
			base.keys[42] = ctrlKeyboard3;
			base.keys[43] = ctrlKeyboard4;
			base.keys[44] = ctrlKeyboard5;
			base.keys[45] = ctrlKeyboard6;
			base.keys[46] = ctrlKeyboard7;
			base.keys[47] = ctrlKeyboard8;
			base.keys[48] = ctrlKeyboard9;
			base.keys[49] = ctrlKeyboard10;
			base.keys[50] = ctrlKeyboardleftShift;
			base.keys[51] = ctrlKeyboardrightShift;
			base.keys[52] = ctrlKeyboardleftAlt;
			base.keys[53] = ctrlKeyboardrightAlt;
			base.keys[54] = ctrlKeyboardleftCtrl;
			base.keys[55] = ctrlKeyboardrightCtrl;
			base.keys[56] = ctrlKeyboardleftMeta;
			base.keys[57] = ctrlKeyboardrightMeta;
			base.keys[58] = ctrlKeyboardcontextMenu;
			base.keys[59] = ctrlKeyboardescape;
			base.keys[60] = ctrlKeyboardleftArrow;
			base.keys[61] = ctrlKeyboardrightArrow;
			base.keys[62] = ctrlKeyboardupArrow;
			base.keys[63] = ctrlKeyboarddownArrow;
			base.keys[64] = ctrlKeyboardbackspace;
			base.keys[65] = ctrlKeyboardpageDown;
			base.keys[66] = ctrlKeyboardpageUp;
			base.keys[67] = ctrlKeyboardhome;
			base.keys[68] = ctrlKeyboardend;
			base.keys[69] = ctrlKeyboardinsert;
			base.keys[70] = ctrlKeyboarddelete;
			base.keys[71] = ctrlKeyboardcapsLock;
			base.keys[72] = ctrlKeyboardnumLock;
			base.keys[73] = ctrlKeyboardprintScreen;
			base.keys[74] = ctrlKeyboardscrollLock;
			base.keys[75] = ctrlKeyboardpause;
			base.keys[76] = ctrlKeyboardnumpadEnter;
			base.keys[77] = ctrlKeyboardnumpadDivide;
			base.keys[78] = ctrlKeyboardnumpadMultiply;
			base.keys[79] = ctrlKeyboardnumpadPlus;
			base.keys[80] = ctrlKeyboardnumpadMinus;
			base.keys[81] = ctrlKeyboardnumpadPeriod;
			base.keys[82] = ctrlKeyboardnumpadEquals;
			base.keys[83] = ctrlKeyboardnumpad10;
			base.keys[84] = ctrlKeyboardnumpad;
			base.keys[85] = ctrlKeyboardnumpad2;
			base.keys[86] = ctrlKeyboardnumpad3;
			base.keys[87] = ctrlKeyboardnumpad4;
			base.keys[88] = ctrlKeyboardnumpad5;
			base.keys[89] = ctrlKeyboardnumpad6;
			base.keys[90] = ctrlKeyboardnumpad7;
			base.keys[91] = ctrlKeyboardnumpad8;
			base.keys[92] = ctrlKeyboardnumpad9;
			base.keys[93] = ctrlKeyboardf2;
			base.keys[94] = ctrlKeyboardf3;
			base.keys[95] = ctrlKeyboardf4;
			base.keys[96] = ctrlKeyboardf5;
			base.keys[97] = ctrlKeyboardf6;
			base.keys[98] = ctrlKeyboardf7;
			base.keys[99] = ctrlKeyboardf8;
			base.keys[100] = ctrlKeyboardf9;
			base.keys[101] = ctrlKeyboardf10;
			base.keys[102] = ctrlKeyboardf11;
			base.keys[103] = ctrlKeyboardf12;
			base.keys[104] = ctrlKeyboardf13;
			base.keys[105] = ctrlKeyboardOEM;
			base.keys[106] = ctrlKeyboardOEM2;
			base.keys[107] = ctrlKeyboardOEM3;
			base.keys[108] = ctrlKeyboardOEM4;
			base.keys[109] = ctrlKeyboardOEM5;
			base.anyKey = ctrlKeyboardanyKey;
			base.shiftKey = ctrlKeyboardshift;
			base.ctrlKey = ctrlKeyboardctrl;
			base.altKey = ctrlKeyboardalt;
			base.imeSelected = ctrlKeyboardIMESelected;
			builder.WithStateOffsetToControlIndexMap(new uint[]
			{
				111616U, 525314U, 1049603U, 1573892U, 2098181U, 2622470U, 3146759U, 3671048U, 4195337U, 4719626U,
				5243915U, 5768204U, 6292493U, 6816782U, 7341071U, 7865364U, 8389653U, 8913942U, 9438231U, 9962520U,
				10486809U, 11011098U, 11535387U, 12059676U, 12583965U, 13108254U, 13632543U, 14156832U, 14681121U, 15205410U,
				15729699U, 16253988U, 16778277U, 17302566U, 17826855U, 18351144U, 18875433U, 19399722U, 19924011U, 20448300U,
				20972589U, 21496878U, 22021167U, 22545456U, 23069745U, 23594034U, 24118323U, 24642612U, 25166901U, 25691190U,
				26215479U, 26739768U, 26740794U, 27264057U, 27788347U, 27789373U, 28312636U, 28836926U, 28837952U, 29361215U,
				29885505U, 30409794U, 30934083U, 31458305U, 31982610U, 32506899U, 33031184U, 33555473U, 34079812U, 34604101U,
				35128390U, 35652679U, 36176968U, 36701257U, 37225546U, 37749835U, 38274124U, 38798413U, 39322702U, 39846991U,
				40371280U, 40895569U, 41419858U, 41944147U, 42468436U, 42992725U, 43517014U, 44041312U, 44565591U, 45089880U,
				45614169U, 46138458U, 46662747U, 47187036U, 47711325U, 48235614U, 48759903U, 49284193U, 49808482U, 50332771U,
				50857060U, 51381349U, 51905638U, 52429927U, 52954216U, 53478505U, 54002794U, 54527083U, 55051372U, 55575661U,
				56099950U, 56624239U, 57148528U, 57672817U, 58197106U
			});
			builder.WithControlTree(new byte[]
			{
				111, 0, 1, 0, 0, 0, 0, 56, 0, 15,
				0, 0, 0, 2, 111, 0, 3, 0, 2, 0,
				2, 84, 0, 5, 0, 0, 0, 0, 111, 0,
				169, 0, 0, 0, 0, 70, 0, 7, 0, 0,
				0, 0, 84, 0, 143, 0, 0, 0, 0, 63,
				0, 9, 0, 0, 0, 0, 70, 0, 53, 0,
				0, 0, 0, 60, 0, 131, 0, 0, 0, 0,
				63, 0, 11, 0, 0, 0, 0, 62, 0, 13,
				0, 0, 0, 0, 63, 0, byte.MaxValue, byte.MaxValue, 22, 0,
				1, 61, 0, byte.MaxValue, byte.MaxValue, 4, 0, 1, 62, 0,
				byte.MaxValue, byte.MaxValue, 21, 0, 1, 28, 0, 17, 0, 0,
				0, 0, 56, 0, 77, 0, 0, 0, 0, 14,
				0, 19, 0, 0, 0, 0, 28, 0, 45, 0,
				0, 0, 0, 7, 0, 21, 0, 0, 0, 0,
				14, 0, 33, 0, 0, 0, 0, 4, 0, 23,
				0, 0, 0, 0, 7, 0, 29, 0, 0, 0,
				0, 2, 0, 25, 0, 0, 0, 0, 4, 0,
				27, 0, 0, 0, 0, 1, 0, byte.MaxValue, byte.MaxValue, 0,
				0, 0, 2, 0, byte.MaxValue, byte.MaxValue, 5, 0, 1, 3,
				0, byte.MaxValue, byte.MaxValue, 6, 0, 1, 4, 0, byte.MaxValue, byte.MaxValue,
				7, 0, 1, 6, 0, 31, 0, 0, 0, 0,
				7, 0, byte.MaxValue, byte.MaxValue, 10, 0, 1, 5, 0, byte.MaxValue,
				byte.MaxValue, 8, 0, 1, 6, 0, byte.MaxValue, byte.MaxValue, 9, 0,
				1, 11, 0, 35, 0, 0, 0, 0, 14, 0,
				41, 0, 0, 0, 0, 9, 0, 37, 0, 0,
				0, 0, 11, 0, 39, 0, 0, 0, 0, 8,
				0, byte.MaxValue, byte.MaxValue, 11, 0, 1, 9, 0, byte.MaxValue, byte.MaxValue,
				12, 0, 1, 10, 0, byte.MaxValue, byte.MaxValue, 13, 0, 1,
				11, 0, byte.MaxValue, byte.MaxValue, 14, 0, 1, 13, 0, 43,
				0, 0, 0, 0, 14, 0, byte.MaxValue, byte.MaxValue, 17, 0,
				1, 12, 0, byte.MaxValue, byte.MaxValue, 15, 0, 1, 13, 0,
				byte.MaxValue, byte.MaxValue, 16, 0, 1, 21, 0, 47, 0, 0,
				0, 0, 28, 0, 65, 0, 0, 0, 0, 18,
				0, 49, 0, 0, 0, 0, 21, 0, 61, 0,
				0, 0, 0, 16, 0, 51, 0, 0, 0, 0,
				18, 0, 59, 0, 0, 0, 0, 15, 0, byte.MaxValue,
				byte.MaxValue, 18, 0, 1, 16, 0, byte.MaxValue, byte.MaxValue, 23, 0,
				1, 67, 0, 55, 0, 0, 0, 0, 70, 0,
				139, 0, 0, 0, 0, 65, 0, 57, 0, 0,
				0, 0, 67, 0, 137, 0, 0, 0, 0, 64,
				0, byte.MaxValue, byte.MaxValue, 19, 0, 1, 65, 0, byte.MaxValue, byte.MaxValue,
				20, 0, 1, 17, 0, byte.MaxValue, byte.MaxValue, 24, 0, 1,
				18, 0, byte.MaxValue, byte.MaxValue, 25, 0, 1, 20, 0, 63,
				0, 0, 0, 0, 21, 0, byte.MaxValue, byte.MaxValue, 28, 0,
				1, 19, 0, byte.MaxValue, byte.MaxValue, 26, 0, 1, 20, 0,
				byte.MaxValue, byte.MaxValue, 27, 0, 1, 25, 0, 67, 0, 0,
				0, 0, 28, 0, 73, 0, 0, 0, 0, 23,
				0, 69, 0, 0, 0, 0, 25, 0, 71, 0,
				0, 0, 0, 22, 0, byte.MaxValue, byte.MaxValue, 29, 0, 1,
				23, 0, byte.MaxValue, byte.MaxValue, 30, 0, 1, 24, 0, byte.MaxValue,
				byte.MaxValue, 31, 0, 1, 25, 0, byte.MaxValue, byte.MaxValue, 32, 0,
				1, 27, 0, 75, 0, 0, 0, 0, 28, 0,
				byte.MaxValue, byte.MaxValue, 35, 0, 1, 26, 0, byte.MaxValue, byte.MaxValue, 33,
				0, 1, 27, 0, byte.MaxValue, byte.MaxValue, 34, 0, 1, 42,
				0, 79, 0, 0, 0, 0, 56, 0, 105, 0,
				0, 0, 0, 35, 0, 81, 0, 0, 0, 0,
				42, 0, 93, 0, 0, 0, 0, 32, 0, 83,
				0, 0, 0, 0, 35, 0, 89, 0, 0, 0,
				0, 30, 0, 85, 0, 0, 0, 0, 32, 0,
				87, 0, 0, 0, 0, 29, 0, byte.MaxValue, byte.MaxValue, 36,
				0, 1, 30, 0, byte.MaxValue, byte.MaxValue, 37, 0, 1, 31,
				0, byte.MaxValue, byte.MaxValue, 38, 0, 1, 32, 0, byte.MaxValue, byte.MaxValue,
				39, 0, 1, 34, 0, 91, 0, 0, 0, 0,
				35, 0, byte.MaxValue, byte.MaxValue, 42, 0, 1, 33, 0, byte.MaxValue,
				byte.MaxValue, 40, 0, 1, 34, 0, byte.MaxValue, byte.MaxValue, 41, 0,
				1, 39, 0, 95, 0, 0, 0, 0, 42, 0,
				101, 0, 0, 0, 0, 37, 0, 97, 0, 0,
				0, 0, 39, 0, 99, 0, 0, 0, 0, 36,
				0, byte.MaxValue, byte.MaxValue, 43, 0, 1, 37, 0, byte.MaxValue, byte.MaxValue,
				44, 0, 1, 38, 0, byte.MaxValue, byte.MaxValue, 45, 0, 1,
				39, 0, byte.MaxValue, byte.MaxValue, 46, 0, 1, 41, 0, 103,
				0, 0, 0, 0, 42, 0, byte.MaxValue, byte.MaxValue, 49, 0,
				1, 40, 0, byte.MaxValue, byte.MaxValue, 47, 0, 1, 41, 0,
				byte.MaxValue, byte.MaxValue, 48, 0, 1, 49, 0, 107, 0, 0,
				0, 0, 56, 0, 119, 0, 0, 0, 0, 46,
				0, 109, 0, 0, 0, 0, 49, 0, 115, 0,
				0, 0, 0, 44, 0, 111, 0, 0, 0, 0,
				46, 0, 113, 0, 0, 0, 0, 43, 0, byte.MaxValue,
				byte.MaxValue, 50, 0, 1, 44, 0, byte.MaxValue, byte.MaxValue, 51, 0,
				1, 45, 0, byte.MaxValue, byte.MaxValue, 52, 0, 1, 46, 0,
				byte.MaxValue, byte.MaxValue, 53, 0, 1, 48, 0, 117, 0, 0,
				0, 0, 49, 0, byte.MaxValue, byte.MaxValue, 56, 0, 1, 47,
				0, byte.MaxValue, byte.MaxValue, 54, 0, 1, 48, 0, byte.MaxValue, byte.MaxValue,
				55, 0, 1, 53, 0, 121, 0, 0, 0, 0,
				56, 0, 127, 0, 0, 0, 0, 51, 0, 123,
				0, 0, 0, 0, 53, 0, 125, 0, 61, 0,
				1, 50, 0, byte.MaxValue, byte.MaxValue, 57, 0, 1, 51, 0,
				byte.MaxValue, byte.MaxValue, 58, 0, 1, 52, 0, byte.MaxValue, byte.MaxValue, 59,
				0, 1, 53, 0, byte.MaxValue, byte.MaxValue, 60, 0, 1, 55,
				0, 129, 0, 64, 0, 1, 56, 0, byte.MaxValue, byte.MaxValue,
				65, 0, 1, 54, 0, byte.MaxValue, byte.MaxValue, 62, 0, 1,
				55, 0, byte.MaxValue, byte.MaxValue, 63, 0, 1, 58, 0, 133,
				0, 0, 0, 0, 60, 0, 135, 0, 0, 0,
				0, 57, 0, byte.MaxValue, byte.MaxValue, 66, 0, 1, 58, 0,
				byte.MaxValue, byte.MaxValue, 67, 0, 1, 59, 0, byte.MaxValue, byte.MaxValue, 68,
				0, 1, 60, 0, byte.MaxValue, byte.MaxValue, 69, 0, 1, 66,
				0, byte.MaxValue, byte.MaxValue, 70, 0, 1, 67, 0, byte.MaxValue, byte.MaxValue,
				71, 0, 1, 69, 0, 141, 0, 0, 0, 0,
				70, 0, byte.MaxValue, byte.MaxValue, 74, 0, 1, 68, 0, byte.MaxValue,
				byte.MaxValue, 72, 0, 1, 69, 0, byte.MaxValue, byte.MaxValue, 73, 0,
				1, 77, 0, 145, 0, 0, 0, 0, 84, 0,
				157, 0, 0, 0, 0, 74, 0, 147, 0, 0,
				0, 0, 77, 0, 153, 0, 0, 0, 0, 72,
				0, 149, 0, 0, 0, 0, 74, 0, 151, 0,
				0, 0, 0, 71, 0, byte.MaxValue, byte.MaxValue, 75, 0, 1,
				72, 0, byte.MaxValue, byte.MaxValue, 76, 0, 1, 73, 0, byte.MaxValue,
				byte.MaxValue, 77, 0, 1, 74, 0, byte.MaxValue, byte.MaxValue, 78, 0,
				1, 76, 0, 155, 0, 0, 0, 0, 77, 0,
				byte.MaxValue, byte.MaxValue, 81, 0, 1, 75, 0, byte.MaxValue, byte.MaxValue, 79,
				0, 1, 76, 0, byte.MaxValue, byte.MaxValue, 80, 0, 1, 81,
				0, 159, 0, 0, 0, 0, 84, 0, 165, 0,
				0, 0, 0, 79, 0, 161, 0, 0, 0, 0,
				81, 0, 163, 0, 0, 0, 0, 78, 0, byte.MaxValue,
				byte.MaxValue, 82, 0, 1, 79, 0, byte.MaxValue, byte.MaxValue, 83, 0,
				1, 80, 0, byte.MaxValue, byte.MaxValue, 84, 0, 1, 81, 0,
				byte.MaxValue, byte.MaxValue, 85, 0, 1, 83, 0, 167, 0, 0,
				0, 0, 84, 0, byte.MaxValue, byte.MaxValue, 88, 0, 1, 82,
				0, byte.MaxValue, byte.MaxValue, 86, 0, 1, 83, 0, byte.MaxValue, byte.MaxValue,
				87, 0, 1, 98, 0, 171, 0, 0, 0, 0,
				111, 0, 197, 0, 0, 0, 0, 91, 0, 173,
				0, 0, 0, 0, 98, 0, 185, 0, 0, 0,
				0, 88, 0, 175, 0, 0, 0, 0, 91, 0,
				181, 0, 0, 0, 0, 86, 0, 177, 0, 0,
				0, 0, 88, 0, 179, 0, 0, 0, 0, 85,
				0, byte.MaxValue, byte.MaxValue, 98, 0, 1, 86, 0, byte.MaxValue, byte.MaxValue,
				89, 0, 1, 87, 0, byte.MaxValue, byte.MaxValue, 90, 0, 1,
				88, 0, byte.MaxValue, byte.MaxValue, 91, 0, 1, 90, 0, 183,
				0, 0, 0, 0, 91, 0, byte.MaxValue, byte.MaxValue, 94, 0,
				1, 89, 0, byte.MaxValue, byte.MaxValue, 92, 0, 1, 90, 0,
				byte.MaxValue, byte.MaxValue, 93, 0, 1, 95, 0, 187, 0, 0,
				0, 0, 98, 0, 193, 0, 0, 0, 0, 93,
				0, 189, 0, 0, 0, 0, 95, 0, 191, 0,
				0, 0, 0, 92, 0, byte.MaxValue, byte.MaxValue, 95, 0, 1,
				93, 0, byte.MaxValue, byte.MaxValue, 96, 0, 1, 94, 0, byte.MaxValue,
				byte.MaxValue, 97, 0, 1, 95, 0, byte.MaxValue, byte.MaxValue, 99, 0,
				1, 97, 0, 195, 0, 0, 0, 0, 98, 0,
				byte.MaxValue, byte.MaxValue, 102, 0, 1, 96, 0, byte.MaxValue, byte.MaxValue, 100,
				0, 1, 97, 0, byte.MaxValue, byte.MaxValue, 101, 0, 1, 105,
				0, 199, 0, 0, 0, 0, 111, 0, 211, 0,
				0, 0, 0, 102, 0, 201, 0, 0, 0, 0,
				105, 0, 207, 0, 0, 0, 0, 100, 0, 203,
				0, 0, 0, 0, 102, 0, 205, 0, 0, 0,
				0, 99, 0, byte.MaxValue, byte.MaxValue, 103, 0, 1, 100, 0,
				byte.MaxValue, byte.MaxValue, 104, 0, 1, 101, 0, byte.MaxValue, byte.MaxValue, 105,
				0, 1, 102, 0, byte.MaxValue, byte.MaxValue, 106, 0, 1, 104,
				0, 209, 0, 0, 0, 0, 105, 0, byte.MaxValue, byte.MaxValue,
				109, 0, 1, 103, 0, byte.MaxValue, byte.MaxValue, 107, 0, 1,
				104, 0, byte.MaxValue, byte.MaxValue, 108, 0, 1, 108, 0, 213,
				0, 0, 0, 0, 111, 0, 217, 0, 0, 0,
				0, 107, 0, 215, 0, 0, 0, 0, 108, 0,
				byte.MaxValue, byte.MaxValue, 112, 0, 1, 106, 0, byte.MaxValue, byte.MaxValue, 110,
				0, 1, 107, 0, byte.MaxValue, byte.MaxValue, 111, 0, 1, 110,
				0, 219, 0, 0, 0, 0, 111, 0, 221, 0,
				115, 0, 1, 109, 0, byte.MaxValue, byte.MaxValue, 113, 0, 1,
				110, 0, byte.MaxValue, byte.MaxValue, 114, 0, 1, 111, 0, byte.MaxValue,
				byte.MaxValue, 0, 0, 0, 111, 0, 223, 0, 0, 0,
				0, 112, 0, byte.MaxValue, byte.MaxValue, 116, 0, 1, 111, 0,
				byte.MaxValue, byte.MaxValue, 0, 0, 0
			}, new ushort[]
			{
				0, 64, 0, 64, 1, 2, 3, 4, 5, 6,
				7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
				17, 18, 19, 20, 21, 22, 23, 24, 25, 26,
				27, 28, 29, 30, 31, 32, 33, 34, 35, 36,
				37, 38, 39, 40, 41, 42, 43, 44, 45, 46,
				47, 48, 49, 50, 51, 52, 53, 54, 55, 56,
				57, 58, 59, 60, 61, 62, 63, 65, 66, 67,
				68, 69, 70, 71, 72, 73, 74, 75, 76, 77,
				78, 79, 80, 81, 82, 83, 84, 85, 86, 87,
				88, 89, 90, 91, 92, 93, 94, 95, 96, 97,
				98, 99, 100, 101, 102, 103, 104, 105, 106, 107,
				108, 109, 110, 111, 112, 113, 114
			});
			builder.Finish();
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001C850 File Offset: 0x0001AA50
		private AnyKeyControl Initialize_ctrlKeyboardanyKey(InternedString kAnyKeyLayout, InputControl parent)
		{
			AnyKeyControl anyKeyControl = new AnyKeyControl();
			anyKeyControl.Setup().At(this, 0).WithParent(parent)
				.WithName("anyKey")
				.WithDisplayName("Any Key")
				.WithLayout(kAnyKeyLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 0U,
					sizeInBits = 109U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return anyKeyControl;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001C908 File Offset: 0x0001AB08
		private KeyControl Initialize_ctrlKeyboardescape(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 1).WithParent(parent)
				.WithName("escape")
				.WithDisplayName("Escape")
				.WithLayout(kKeyLayout)
				.WithUsages(0, 2)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 60U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Escape;
			return keyControl;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001C9C8 File Offset: 0x0001ABC8
		private KeyControl Initialize_ctrlKeyboardspace(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 2).WithParent(parent)
				.WithName("space")
				.WithDisplayName("Space")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 1U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Space;
			return keyControl;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001CA7C File Offset: 0x0001AC7C
		private KeyControl Initialize_ctrlKeyboardenter(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 3).WithParent(parent)
				.WithName("enter")
				.WithDisplayName("Enter")
				.WithLayout(kKeyLayout)
				.WithUsages(2, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 2U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Enter;
			return keyControl;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		private KeyControl Initialize_ctrlKeyboardtab(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 4).WithParent(parent)
				.WithName("tab")
				.WithDisplayName("Tab")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 3U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Tab;
			return keyControl;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001CBF0 File Offset: 0x0001ADF0
		private KeyControl Initialize_ctrlKeyboardbackquote(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 5).WithParent(parent)
				.WithName("backquote")
				.WithDisplayName("`")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Backquote;
			return keyControl;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
		private KeyControl Initialize_ctrlKeyboardquote(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 6).WithParent(parent)
				.WithName("quote")
				.WithDisplayName("'")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 5U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Quote;
			return keyControl;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001CD58 File Offset: 0x0001AF58
		private KeyControl Initialize_ctrlKeyboardsemicolon(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 7).WithParent(parent)
				.WithName("semicolon")
				.WithDisplayName(";")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 6U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Semicolon;
			return keyControl;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001CE0C File Offset: 0x0001B00C
		private KeyControl Initialize_ctrlKeyboardcomma(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 8).WithParent(parent)
				.WithName("comma")
				.WithDisplayName(",")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 7U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Comma;
			return keyControl;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001CEC0 File Offset: 0x0001B0C0
		private KeyControl Initialize_ctrlKeyboardperiod(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 9).WithParent(parent)
				.WithName("period")
				.WithDisplayName(".")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 8U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Period;
			return keyControl;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001CF78 File Offset: 0x0001B178
		private KeyControl Initialize_ctrlKeyboardslash(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 10).WithParent(parent)
				.WithName("slash")
				.WithDisplayName("/")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 9U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Slash;
			return keyControl;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001D030 File Offset: 0x0001B230
		private KeyControl Initialize_ctrlKeyboardbackslash(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 11).WithParent(parent)
				.WithName("backslash")
				.WithDisplayName("\\")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 10U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Backslash;
			return keyControl;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001D0E8 File Offset: 0x0001B2E8
		private KeyControl Initialize_ctrlKeyboardleftBracket(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 12).WithParent(parent)
				.WithName("leftBracket")
				.WithDisplayName("[")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 11U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.LeftBracket;
			return keyControl;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001D1A0 File Offset: 0x0001B3A0
		private KeyControl Initialize_ctrlKeyboardrightBracket(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 13).WithParent(parent)
				.WithName("rightBracket")
				.WithDisplayName("]")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 12U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.RightBracket;
			return keyControl;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001D258 File Offset: 0x0001B458
		private KeyControl Initialize_ctrlKeyboardminus(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 14).WithParent(parent)
				.WithName("minus")
				.WithDisplayName("-")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 13U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Minus;
			return keyControl;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001D310 File Offset: 0x0001B510
		private KeyControl Initialize_ctrlKeyboardequals(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 15).WithParent(parent)
				.WithName("equals")
				.WithDisplayName("=")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 14U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Equals;
			return keyControl;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001D3C8 File Offset: 0x0001B5C8
		private KeyControl Initialize_ctrlKeyboardupArrow(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 16).WithParent(parent)
				.WithName("upArrow")
				.WithDisplayName("Up Arrow")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 63U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.UpArrow;
			return keyControl;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001D480 File Offset: 0x0001B680
		private KeyControl Initialize_ctrlKeyboarddownArrow(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 17).WithParent(parent)
				.WithName("downArrow")
				.WithDisplayName("Down Arrow")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 64U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.DownArrow;
			return keyControl;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001D538 File Offset: 0x0001B738
		private KeyControl Initialize_ctrlKeyboardleftArrow(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 18).WithParent(parent)
				.WithName("leftArrow")
				.WithDisplayName("Left Arrow")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 61U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.LeftArrow;
			return keyControl;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001D5F0 File Offset: 0x0001B7F0
		private KeyControl Initialize_ctrlKeyboardrightArrow(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 19).WithParent(parent)
				.WithName("rightArrow")
				.WithDisplayName("Right Arrow")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 62U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.RightArrow;
			return keyControl;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001D6A8 File Offset: 0x0001B8A8
		private KeyControl Initialize_ctrlKeyboarda(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 20).WithParent(parent)
				.WithName("a")
				.WithDisplayName("A")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 15U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.A;
			return keyControl;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001D760 File Offset: 0x0001B960
		private KeyControl Initialize_ctrlKeyboardb(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 21).WithParent(parent)
				.WithName("b")
				.WithDisplayName("B")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 16U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.B;
			return keyControl;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001D818 File Offset: 0x0001BA18
		private KeyControl Initialize_ctrlKeyboardc(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 22).WithParent(parent)
				.WithName("c")
				.WithDisplayName("C")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 17U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.C;
			return keyControl;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001D8D0 File Offset: 0x0001BAD0
		private KeyControl Initialize_ctrlKeyboardd(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 23).WithParent(parent)
				.WithName("d")
				.WithDisplayName("D")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 18U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.D;
			return keyControl;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001D988 File Offset: 0x0001BB88
		private KeyControl Initialize_ctrlKeyboarde(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 24).WithParent(parent)
				.WithName("e")
				.WithDisplayName("E")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 19U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.E;
			return keyControl;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001DA40 File Offset: 0x0001BC40
		private KeyControl Initialize_ctrlKeyboardf(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 25).WithParent(parent)
				.WithName("f")
				.WithDisplayName("F")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 20U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F;
			return keyControl;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		private KeyControl Initialize_ctrlKeyboardg(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 26).WithParent(parent)
				.WithName("g")
				.WithDisplayName("G")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 21U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.G;
			return keyControl;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
		private KeyControl Initialize_ctrlKeyboardh(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 27).WithParent(parent)
				.WithName("h")
				.WithDisplayName("H")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 22U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.H;
			return keyControl;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001DC68 File Offset: 0x0001BE68
		private KeyControl Initialize_ctrlKeyboardi(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 28).WithParent(parent)
				.WithName("i")
				.WithDisplayName("I")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 23U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.I;
			return keyControl;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001DD20 File Offset: 0x0001BF20
		private KeyControl Initialize_ctrlKeyboardj(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 29).WithParent(parent)
				.WithName("j")
				.WithDisplayName("J")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 24U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.J;
			return keyControl;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001DDD8 File Offset: 0x0001BFD8
		private KeyControl Initialize_ctrlKeyboardk(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 30).WithParent(parent)
				.WithName("k")
				.WithDisplayName("K")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 25U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.K;
			return keyControl;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001DE90 File Offset: 0x0001C090
		private KeyControl Initialize_ctrlKeyboardl(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 31).WithParent(parent)
				.WithName("l")
				.WithDisplayName("L")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 26U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.L;
			return keyControl;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001DF48 File Offset: 0x0001C148
		private KeyControl Initialize_ctrlKeyboardm(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 32).WithParent(parent)
				.WithName("m")
				.WithDisplayName("M")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 27U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.M;
			return keyControl;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001E000 File Offset: 0x0001C200
		private KeyControl Initialize_ctrlKeyboardn(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 33).WithParent(parent)
				.WithName("n")
				.WithDisplayName("N")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 28U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.N;
			return keyControl;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001E0B8 File Offset: 0x0001C2B8
		private KeyControl Initialize_ctrlKeyboardo(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 34).WithParent(parent)
				.WithName("o")
				.WithDisplayName("O")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 29U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.O;
			return keyControl;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001E170 File Offset: 0x0001C370
		private KeyControl Initialize_ctrlKeyboardp(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 35).WithParent(parent)
				.WithName("p")
				.WithDisplayName("P")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 30U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.P;
			return keyControl;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001E228 File Offset: 0x0001C428
		private KeyControl Initialize_ctrlKeyboardq(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 36).WithParent(parent)
				.WithName("q")
				.WithDisplayName("Q")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 31U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Q;
			return keyControl;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001E2E0 File Offset: 0x0001C4E0
		private KeyControl Initialize_ctrlKeyboardr(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 37).WithParent(parent)
				.WithName("r")
				.WithDisplayName("R")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 32U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.R;
			return keyControl;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001E398 File Offset: 0x0001C598
		private KeyControl Initialize_ctrlKeyboards(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 38).WithParent(parent)
				.WithName("s")
				.WithDisplayName("S")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 33U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.S;
			return keyControl;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001E450 File Offset: 0x0001C650
		private KeyControl Initialize_ctrlKeyboardt(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 39).WithParent(parent)
				.WithName("t")
				.WithDisplayName("T")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 34U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.T;
			return keyControl;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001E508 File Offset: 0x0001C708
		private KeyControl Initialize_ctrlKeyboardu(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 40).WithParent(parent)
				.WithName("u")
				.WithDisplayName("U")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 35U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.U;
			return keyControl;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
		private KeyControl Initialize_ctrlKeyboardv(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 41).WithParent(parent)
				.WithName("v")
				.WithDisplayName("V")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 36U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.V;
			return keyControl;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001E678 File Offset: 0x0001C878
		private KeyControl Initialize_ctrlKeyboardw(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 42).WithParent(parent)
				.WithName("w")
				.WithDisplayName("W")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 37U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.W;
			return keyControl;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001E730 File Offset: 0x0001C930
		private KeyControl Initialize_ctrlKeyboardx(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 43).WithParent(parent)
				.WithName("x")
				.WithDisplayName("X")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 38U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.X;
			return keyControl;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001E7E8 File Offset: 0x0001C9E8
		private KeyControl Initialize_ctrlKeyboardy(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 44).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Y")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 39U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Y;
			return keyControl;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001E8A0 File Offset: 0x0001CAA0
		private KeyControl Initialize_ctrlKeyboardz(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 45).WithParent(parent)
				.WithName("z")
				.WithDisplayName("Z")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 40U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Z;
			return keyControl;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001E958 File Offset: 0x0001CB58
		private KeyControl Initialize_ctrlKeyboard1(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 46).WithParent(parent)
				.WithName("1")
				.WithDisplayName("1")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 41U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Digit1;
			return keyControl;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001EA10 File Offset: 0x0001CC10
		private KeyControl Initialize_ctrlKeyboard2(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 47).WithParent(parent)
				.WithName("2")
				.WithDisplayName("2")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 42U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Digit2;
			return keyControl;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		private KeyControl Initialize_ctrlKeyboard3(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 48).WithParent(parent)
				.WithName("3")
				.WithDisplayName("3")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 43U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Digit3;
			return keyControl;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001EB80 File Offset: 0x0001CD80
		private KeyControl Initialize_ctrlKeyboard4(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 49).WithParent(parent)
				.WithName("4")
				.WithDisplayName("4")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 44U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Digit4;
			return keyControl;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001EC38 File Offset: 0x0001CE38
		private KeyControl Initialize_ctrlKeyboard5(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 50).WithParent(parent)
				.WithName("5")
				.WithDisplayName("5")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 45U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Digit5;
			return keyControl;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001ECF0 File Offset: 0x0001CEF0
		private KeyControl Initialize_ctrlKeyboard6(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 51).WithParent(parent)
				.WithName("6")
				.WithDisplayName("6")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 46U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Digit6;
			return keyControl;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001EDA8 File Offset: 0x0001CFA8
		private KeyControl Initialize_ctrlKeyboard7(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 52).WithParent(parent)
				.WithName("7")
				.WithDisplayName("7")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 47U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Digit7;
			return keyControl;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001EE60 File Offset: 0x0001D060
		private KeyControl Initialize_ctrlKeyboard8(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 53).WithParent(parent)
				.WithName("8")
				.WithDisplayName("8")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 48U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Digit8;
			return keyControl;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001EF18 File Offset: 0x0001D118
		private KeyControl Initialize_ctrlKeyboard9(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 54).WithParent(parent)
				.WithName("9")
				.WithDisplayName("9")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 49U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Digit9;
			return keyControl;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001EFD0 File Offset: 0x0001D1D0
		private KeyControl Initialize_ctrlKeyboard0(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 55).WithParent(parent)
				.WithName("0")
				.WithDisplayName("0")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 50U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Digit0;
			return keyControl;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001F088 File Offset: 0x0001D288
		private KeyControl Initialize_ctrlKeyboardleftShift(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 56).WithParent(parent)
				.WithName("leftShift")
				.WithDisplayName("Left Shift")
				.WithLayout(kKeyLayout)
				.WithUsages(3, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 51U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.LeftShift;
			return keyControl;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001F14C File Offset: 0x0001D34C
		private KeyControl Initialize_ctrlKeyboardrightShift(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 57).WithParent(parent)
				.WithName("rightShift")
				.WithDisplayName("Right Shift")
				.WithLayout(kKeyLayout)
				.WithUsages(4, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 52U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.RightShift;
			return keyControl;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001F210 File Offset: 0x0001D410
		private DiscreteButtonControl Initialize_ctrlKeyboardshift(InternedString kDiscreteButtonLayout, InputControl parent)
		{
			DiscreteButtonControl discreteButtonControl = new DiscreteButtonControl();
			discreteButtonControl.minValue = 1;
			discreteButtonControl.maxValue = 3;
			discreteButtonControl.writeMode = DiscreteButtonControl.WriteMode.WriteNullAndMaxValue;
			discreteButtonControl.Setup().At(this, 58).WithParent(parent)
				.WithName("shift")
				.WithDisplayName("Shift")
				.WithLayout(kDiscreteButtonLayout)
				.WithUsages(5, 1)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 51U,
					sizeInBits = 2U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return discreteButtonControl;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001F2E8 File Offset: 0x0001D4E8
		private KeyControl Initialize_ctrlKeyboardleftAlt(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 59).WithParent(parent)
				.WithName("leftAlt")
				.WithDisplayName("Left Alt")
				.WithLayout(kKeyLayout)
				.WithUsages(6, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 53U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.LeftAlt;
			return keyControl;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001F3AC File Offset: 0x0001D5AC
		private KeyControl Initialize_ctrlKeyboardrightAlt(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 60).WithParent(parent)
				.WithName("rightAlt")
				.WithDisplayName("Right Alt")
				.WithLayout(kKeyLayout)
				.WithUsages(7, 1)
				.WithAliases(0, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 54U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.RightAlt;
			return keyControl;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001F478 File Offset: 0x0001D678
		private DiscreteButtonControl Initialize_ctrlKeyboardalt(InternedString kDiscreteButtonLayout, InputControl parent)
		{
			DiscreteButtonControl discreteButtonControl = new DiscreteButtonControl();
			discreteButtonControl.minValue = 1;
			discreteButtonControl.maxValue = 3;
			discreteButtonControl.writeMode = DiscreteButtonControl.WriteMode.WriteNullAndMaxValue;
			discreteButtonControl.Setup().At(this, 61).WithParent(parent)
				.WithName("alt")
				.WithDisplayName("Alt")
				.WithLayout(kDiscreteButtonLayout)
				.WithUsages(8, 1)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 53U,
					sizeInBits = 2U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return discreteButtonControl;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001F550 File Offset: 0x0001D750
		private KeyControl Initialize_ctrlKeyboardleftCtrl(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 62).WithParent(parent)
				.WithName("leftCtrl")
				.WithDisplayName("Left Control")
				.WithLayout(kKeyLayout)
				.WithUsages(9, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 55U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.LeftCtrl;
			return keyControl;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001F614 File Offset: 0x0001D814
		private KeyControl Initialize_ctrlKeyboardrightCtrl(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 63).WithParent(parent)
				.WithName("rightCtrl")
				.WithDisplayName("Right Control")
				.WithLayout(kKeyLayout)
				.WithUsages(10, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 56U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.RightCtrl;
			return keyControl;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001F6D8 File Offset: 0x0001D8D8
		private DiscreteButtonControl Initialize_ctrlKeyboardctrl(InternedString kDiscreteButtonLayout, InputControl parent)
		{
			DiscreteButtonControl discreteButtonControl = new DiscreteButtonControl();
			discreteButtonControl.minValue = 1;
			discreteButtonControl.maxValue = 3;
			discreteButtonControl.writeMode = DiscreteButtonControl.WriteMode.WriteNullAndMaxValue;
			discreteButtonControl.Setup().At(this, 64).WithParent(parent)
				.WithName("ctrl")
				.WithDisplayName("Control")
				.WithLayout(kDiscreteButtonLayout)
				.WithUsages(11, 1)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 55U,
					sizeInBits = 2U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return discreteButtonControl;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001F7B0 File Offset: 0x0001D9B0
		private KeyControl Initialize_ctrlKeyboardleftMeta(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 65).WithParent(parent)
				.WithName("leftMeta")
				.WithDisplayName("Left System")
				.WithLayout(kKeyLayout)
				.WithUsages(12, 1)
				.WithAliases(1, 3)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 57U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.LeftMeta;
			return keyControl;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001F87C File Offset: 0x0001DA7C
		private KeyControl Initialize_ctrlKeyboardrightMeta(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 66).WithParent(parent)
				.WithName("rightMeta")
				.WithDisplayName("Right System")
				.WithLayout(kKeyLayout)
				.WithUsages(13, 1)
				.WithAliases(4, 3)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 58U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.RightMeta;
			return keyControl;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001F948 File Offset: 0x0001DB48
		private KeyControl Initialize_ctrlKeyboardcontextMenu(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 67).WithParent(parent)
				.WithName("contextMenu")
				.WithDisplayName("Context Menu")
				.WithLayout(kKeyLayout)
				.WithUsages(14, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 59U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.ContextMenu;
			return keyControl;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001FA0C File Offset: 0x0001DC0C
		private KeyControl Initialize_ctrlKeyboardbackspace(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 68).WithParent(parent)
				.WithName("backspace")
				.WithDisplayName("Backspace")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 65U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Backspace;
			return keyControl;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001FAC4 File Offset: 0x0001DCC4
		private KeyControl Initialize_ctrlKeyboardpageDown(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 69).WithParent(parent)
				.WithName("pageDown")
				.WithDisplayName("Page Down")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 66U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.PageDown;
			return keyControl;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001FB7C File Offset: 0x0001DD7C
		private KeyControl Initialize_ctrlKeyboardpageUp(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 70).WithParent(parent)
				.WithName("pageUp")
				.WithDisplayName("Page Up")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 67U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.PageUp;
			return keyControl;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001FC34 File Offset: 0x0001DE34
		private KeyControl Initialize_ctrlKeyboardhome(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 71).WithParent(parent)
				.WithName("home")
				.WithDisplayName("Home")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 68U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Home;
			return keyControl;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001FCEC File Offset: 0x0001DEEC
		private KeyControl Initialize_ctrlKeyboardend(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 72).WithParent(parent)
				.WithName("end")
				.WithDisplayName("End")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 69U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.End;
			return keyControl;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001FDA4 File Offset: 0x0001DFA4
		private KeyControl Initialize_ctrlKeyboardinsert(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 73).WithParent(parent)
				.WithName("insert")
				.WithDisplayName("Insert")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 70U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Insert;
			return keyControl;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001FE5C File Offset: 0x0001E05C
		private KeyControl Initialize_ctrlKeyboarddelete(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 74).WithParent(parent)
				.WithName("delete")
				.WithDisplayName("Delete")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 71U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Delete;
			return keyControl;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001FF14 File Offset: 0x0001E114
		private KeyControl Initialize_ctrlKeyboardcapsLock(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 75).WithParent(parent)
				.WithName("capsLock")
				.WithDisplayName("Caps Lock")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 72U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.CapsLock;
			return keyControl;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001FFCC File Offset: 0x0001E1CC
		private KeyControl Initialize_ctrlKeyboardnumLock(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 76).WithParent(parent)
				.WithName("numLock")
				.WithDisplayName("Num Lock")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 73U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.NumLock;
			return keyControl;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00020084 File Offset: 0x0001E284
		private KeyControl Initialize_ctrlKeyboardprintScreen(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 77).WithParent(parent)
				.WithName("printScreen")
				.WithDisplayName("Print Screen")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 74U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.PrintScreen;
			return keyControl;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0002013C File Offset: 0x0001E33C
		private KeyControl Initialize_ctrlKeyboardscrollLock(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 78).WithParent(parent)
				.WithName("scrollLock")
				.WithDisplayName("Scroll Lock")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 75U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.ScrollLock;
			return keyControl;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000201F4 File Offset: 0x0001E3F4
		private KeyControl Initialize_ctrlKeyboardpause(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 79).WithParent(parent)
				.WithName("pause")
				.WithDisplayName("Pause/Break")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 76U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Pause;
			return keyControl;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000202AC File Offset: 0x0001E4AC
		private KeyControl Initialize_ctrlKeyboardnumpadEnter(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 80).WithParent(parent)
				.WithName("numpadEnter")
				.WithDisplayName("Numpad Enter")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 77U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.NumpadEnter;
			return keyControl;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00020364 File Offset: 0x0001E564
		private KeyControl Initialize_ctrlKeyboardnumpadDivide(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 81).WithParent(parent)
				.WithName("numpadDivide")
				.WithDisplayName("Numpad /")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 78U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.NumpadDivide;
			return keyControl;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0002041C File Offset: 0x0001E61C
		private KeyControl Initialize_ctrlKeyboardnumpadMultiply(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 82).WithParent(parent)
				.WithName("numpadMultiply")
				.WithDisplayName("Numpad *")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 79U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.NumpadMultiply;
			return keyControl;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000204D4 File Offset: 0x0001E6D4
		private KeyControl Initialize_ctrlKeyboardnumpadPlus(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 83).WithParent(parent)
				.WithName("numpadPlus")
				.WithDisplayName("Numpad +")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 80U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.NumpadPlus;
			return keyControl;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002058C File Offset: 0x0001E78C
		private KeyControl Initialize_ctrlKeyboardnumpadMinus(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 84).WithParent(parent)
				.WithName("numpadMinus")
				.WithDisplayName("Numpad -")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 81U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.NumpadMinus;
			return keyControl;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00020644 File Offset: 0x0001E844
		private KeyControl Initialize_ctrlKeyboardnumpadPeriod(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 85).WithParent(parent)
				.WithName("numpadPeriod")
				.WithDisplayName("Numpad .")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 82U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.NumpadPeriod;
			return keyControl;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000206FC File Offset: 0x0001E8FC
		private KeyControl Initialize_ctrlKeyboardnumpadEquals(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 86).WithParent(parent)
				.WithName("numpadEquals")
				.WithDisplayName("Numpad =")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 83U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.NumpadEquals;
			return keyControl;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000207B4 File Offset: 0x0001E9B4
		private KeyControl Initialize_ctrlKeyboardnumpad1(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 87).WithParent(parent)
				.WithName("numpad1")
				.WithDisplayName("Numpad 1")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 85U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Numpad1;
			return keyControl;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002086C File Offset: 0x0001EA6C
		private KeyControl Initialize_ctrlKeyboardnumpad2(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 88).WithParent(parent)
				.WithName("numpad2")
				.WithDisplayName("Numpad 2")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 86U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Numpad2;
			return keyControl;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00020924 File Offset: 0x0001EB24
		private KeyControl Initialize_ctrlKeyboardnumpad3(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 89).WithParent(parent)
				.WithName("numpad3")
				.WithDisplayName("Numpad 3")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 87U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Numpad3;
			return keyControl;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000209DC File Offset: 0x0001EBDC
		private KeyControl Initialize_ctrlKeyboardnumpad4(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 90).WithParent(parent)
				.WithName("numpad4")
				.WithDisplayName("Numpad 4")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 88U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Numpad4;
			return keyControl;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00020A94 File Offset: 0x0001EC94
		private KeyControl Initialize_ctrlKeyboardnumpad5(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 91).WithParent(parent)
				.WithName("numpad5")
				.WithDisplayName("Numpad 5")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 89U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Numpad5;
			return keyControl;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00020B4C File Offset: 0x0001ED4C
		private KeyControl Initialize_ctrlKeyboardnumpad6(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 92).WithParent(parent)
				.WithName("numpad6")
				.WithDisplayName("Numpad 6")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 90U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Numpad6;
			return keyControl;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00020C04 File Offset: 0x0001EE04
		private KeyControl Initialize_ctrlKeyboardnumpad7(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 93).WithParent(parent)
				.WithName("numpad7")
				.WithDisplayName("Numpad 7")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 91U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Numpad7;
			return keyControl;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00020CBC File Offset: 0x0001EEBC
		private KeyControl Initialize_ctrlKeyboardnumpad8(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 94).WithParent(parent)
				.WithName("numpad8")
				.WithDisplayName("Numpad 8")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 92U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Numpad8;
			return keyControl;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00020D74 File Offset: 0x0001EF74
		private KeyControl Initialize_ctrlKeyboardnumpad9(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 95).WithParent(parent)
				.WithName("numpad9")
				.WithDisplayName("Numpad 9")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 93U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Numpad9;
			return keyControl;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00020E2C File Offset: 0x0001F02C
		private KeyControl Initialize_ctrlKeyboardnumpad0(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 96).WithParent(parent)
				.WithName("numpad0")
				.WithDisplayName("Numpad 0")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 84U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.Numpad0;
			return keyControl;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00020EE4 File Offset: 0x0001F0E4
		private KeyControl Initialize_ctrlKeyboardf1(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 97).WithParent(parent)
				.WithName("f1")
				.WithDisplayName("F1")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 94U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F1;
			return keyControl;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00020F9C File Offset: 0x0001F19C
		private KeyControl Initialize_ctrlKeyboardf2(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 98).WithParent(parent)
				.WithName("f2")
				.WithDisplayName("F2")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 95U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F2;
			return keyControl;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00021054 File Offset: 0x0001F254
		private KeyControl Initialize_ctrlKeyboardf3(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 99).WithParent(parent)
				.WithName("f3")
				.WithDisplayName("F3")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 96U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F3;
			return keyControl;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002110C File Offset: 0x0001F30C
		private KeyControl Initialize_ctrlKeyboardf4(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 100).WithParent(parent)
				.WithName("f4")
				.WithDisplayName("F4")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 97U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F4;
			return keyControl;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000211C4 File Offset: 0x0001F3C4
		private KeyControl Initialize_ctrlKeyboardf5(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 101).WithParent(parent)
				.WithName("f5")
				.WithDisplayName("F5")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 98U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F5;
			return keyControl;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002127C File Offset: 0x0001F47C
		private KeyControl Initialize_ctrlKeyboardf6(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 102).WithParent(parent)
				.WithName("f6")
				.WithDisplayName("F6")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 99U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F6;
			return keyControl;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00021334 File Offset: 0x0001F534
		private KeyControl Initialize_ctrlKeyboardf7(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 103).WithParent(parent)
				.WithName("f7")
				.WithDisplayName("F7")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 100U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F7;
			return keyControl;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000213EC File Offset: 0x0001F5EC
		private KeyControl Initialize_ctrlKeyboardf8(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 104).WithParent(parent)
				.WithName("f8")
				.WithDisplayName("F8")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 101U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F8;
			return keyControl;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000214A4 File Offset: 0x0001F6A4
		private KeyControl Initialize_ctrlKeyboardf9(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 105).WithParent(parent)
				.WithName("f9")
				.WithDisplayName("F9")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 102U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F9;
			return keyControl;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0002155C File Offset: 0x0001F75C
		private KeyControl Initialize_ctrlKeyboardf10(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 106).WithParent(parent)
				.WithName("f10")
				.WithDisplayName("F10")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 103U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F10;
			return keyControl;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00021614 File Offset: 0x0001F814
		private KeyControl Initialize_ctrlKeyboardf11(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 107).WithParent(parent)
				.WithName("f11")
				.WithDisplayName("F11")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 104U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F11;
			return keyControl;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000216CC File Offset: 0x0001F8CC
		private KeyControl Initialize_ctrlKeyboardf12(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 108).WithParent(parent)
				.WithName("f12")
				.WithDisplayName("F12")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 105U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.F12;
			return keyControl;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00021784 File Offset: 0x0001F984
		private KeyControl Initialize_ctrlKeyboardOEM1(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 109).WithParent(parent)
				.WithName("OEM1")
				.WithDisplayName("OEM1")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 106U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.OEM1;
			return keyControl;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002183C File Offset: 0x0001FA3C
		private KeyControl Initialize_ctrlKeyboardOEM2(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 110).WithParent(parent)
				.WithName("OEM2")
				.WithDisplayName("OEM2")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 107U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.OEM2;
			return keyControl;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000218F4 File Offset: 0x0001FAF4
		private KeyControl Initialize_ctrlKeyboardOEM3(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 111).WithParent(parent)
				.WithName("OEM3")
				.WithDisplayName("OEM3")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 108U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.OEM3;
			return keyControl;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000219AC File Offset: 0x0001FBAC
		private KeyControl Initialize_ctrlKeyboardOEM4(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 112).WithParent(parent)
				.WithName("OEM4")
				.WithDisplayName("OEM4")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 109U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.OEM4;
			return keyControl;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00021A64 File Offset: 0x0001FC64
		private KeyControl Initialize_ctrlKeyboardOEM5(InternedString kKeyLayout, InputControl parent)
		{
			KeyControl keyControl = new KeyControl();
			keyControl.Setup().At(this, 113).WithParent(parent)
				.WithName("OEM5")
				.WithDisplayName("OEM5")
				.WithLayout(kKeyLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 110U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			keyControl.keyCode = Key.OEM5;
			return keyControl;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00021B1C File Offset: 0x0001FD1C
		private ButtonControl Initialize_ctrlKeyboardIMESelected(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 114).WithParent(parent)
				.WithName("IMESelected")
				.WithDisplayName("IMESelected")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 0U,
					bitOffset = 111U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x040003E4 RID: 996
		public const string metadata = ";AnyKey;Button;Axis;Key;DiscreteButton;Keyboard";
	}
}
