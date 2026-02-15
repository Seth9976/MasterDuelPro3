using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200013A RID: 314
	internal abstract class RegexCompiler
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x0002110A File Offset: 0x0001F30A
		private static FieldInfo RegexRunnerField(string fieldname)
		{
			return typeof(RegexRunner).GetField(fieldname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0002111E File Offset: 0x0001F31E
		private static MethodInfo RegexRunnerMethod(string methname)
		{
			return typeof(RegexRunner).GetMethod(methname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00021132 File Offset: 0x0001F332
		internal static RegexRunnerFactory Compile(RegexCode code, RegexOptions options)
		{
			return new RegexLWCGCompiler().FactoryInstanceFromCode(code, options);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00021140 File Offset: 0x0001F340
		private int AddBacktrackNote(int flags, Label l, int codepos)
		{
			if (this._notes == null || this._notecount >= this._notes.Length)
			{
				RegexCompiler.BacktrackNote[] array = new RegexCompiler.BacktrackNote[(this._notes == null) ? 16 : (this._notes.Length * 2)];
				if (this._notes != null)
				{
					Array.Copy(this._notes, 0, array, 0, this._notecount);
				}
				this._notes = array;
			}
			this._notes[this._notecount] = new RegexCompiler.BacktrackNote(flags, l, codepos);
			int notecount = this._notecount;
			this._notecount = notecount + 1;
			return notecount;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000211CA File Offset: 0x0001F3CA
		private int AddTrack()
		{
			return this.AddTrack(128);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000211D7 File Offset: 0x0001F3D7
		private int AddTrack(int flags)
		{
			return this.AddBacktrackNote(flags, this.DefineLabel(), this._codepos);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000211EC File Offset: 0x0001F3EC
		private int AddGoto(int destpos)
		{
			if (this._goto[destpos] == -1)
			{
				this._goto[destpos] = this.AddBacktrackNote(0, this._labels[destpos], destpos);
			}
			return this._goto[destpos];
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0002121D File Offset: 0x0001F41D
		private int AddUniqueTrack(int i)
		{
			return this.AddUniqueTrack(i, 128);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0002122B File Offset: 0x0001F42B
		private int AddUniqueTrack(int i, int flags)
		{
			if (this._uniquenote[i] == -1)
			{
				this._uniquenote[i] = this.AddTrack(flags);
			}
			return this._uniquenote[i];
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0002124F File Offset: 0x0001F44F
		private Label DefineLabel()
		{
			return this._ilg.DefineLabel();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0002125C File Offset: 0x0001F45C
		private void MarkLabel(Label l)
		{
			this._ilg.MarkLabel(l);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0002126A File Offset: 0x0001F46A
		private int Operand(int i)
		{
			return this._codes[this._codepos + i + 1];
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0002127D File Offset: 0x0001F47D
		private bool IsRtl()
		{
			return (this._regexopcode & 64) != 0;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0002128B File Offset: 0x0001F48B
		private bool IsCi()
		{
			return (this._regexopcode & 512) != 0;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0002129C File Offset: 0x0001F49C
		private int Code()
		{
			return this._regexopcode & 63;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000212A7 File Offset: 0x0001F4A7
		private void Ldstr(string str)
		{
			this._ilg.Emit(OpCodes.Ldstr, str);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000212BA File Offset: 0x0001F4BA
		private void Ldc(int i)
		{
			if (i <= 127 && i >= -128)
			{
				this._ilg.Emit(OpCodes.Ldc_I4_S, (byte)i);
				return;
			}
			this._ilg.Emit(OpCodes.Ldc_I4, i);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000212EA File Offset: 0x0001F4EA
		private void Dup()
		{
			this._ilg.Emit(OpCodes.Dup);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000212FC File Offset: 0x0001F4FC
		private void Ret()
		{
			this._ilg.Emit(OpCodes.Ret);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0002130E File Offset: 0x0001F50E
		private void Pop()
		{
			this._ilg.Emit(OpCodes.Pop);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00021320 File Offset: 0x0001F520
		private void Add()
		{
			this._ilg.Emit(OpCodes.Add);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00021332 File Offset: 0x0001F532
		private void Add(bool negate)
		{
			if (negate)
			{
				this._ilg.Emit(OpCodes.Sub);
				return;
			}
			this._ilg.Emit(OpCodes.Add);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00021358 File Offset: 0x0001F558
		private void Sub()
		{
			this._ilg.Emit(OpCodes.Sub);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0002136A File Offset: 0x0001F56A
		private void Sub(bool negate)
		{
			if (negate)
			{
				this._ilg.Emit(OpCodes.Add);
				return;
			}
			this._ilg.Emit(OpCodes.Sub);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00021390 File Offset: 0x0001F590
		private void Ldloc(LocalBuilder lt)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, lt);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000213A3 File Offset: 0x0001F5A3
		private void Stloc(LocalBuilder lt)
		{
			this._ilg.Emit(OpCodes.Stloc_S, lt);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000213B6 File Offset: 0x0001F5B6
		private void Ldthis()
		{
			this._ilg.Emit(OpCodes.Ldarg_0);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000213C8 File Offset: 0x0001F5C8
		private void Ldthisfld(FieldInfo ft)
		{
			this.Ldthis();
			this._ilg.Emit(OpCodes.Ldfld, ft);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000213E1 File Offset: 0x0001F5E1
		private void Mvfldloc(FieldInfo ft, LocalBuilder lt)
		{
			this.Ldthisfld(ft);
			this.Stloc(lt);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000213F1 File Offset: 0x0001F5F1
		private void Mvlocfld(LocalBuilder lt, FieldInfo ft)
		{
			this.Ldthis();
			this.Ldloc(lt);
			this.Stfld(ft);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00021407 File Offset: 0x0001F607
		private void Stfld(FieldInfo ft)
		{
			this._ilg.Emit(OpCodes.Stfld, ft);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0002141A File Offset: 0x0001F61A
		private void Callvirt(MethodInfo mt)
		{
			this._ilg.Emit(OpCodes.Callvirt, mt);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0002142D File Offset: 0x0001F62D
		private void Call(MethodInfo mt)
		{
			this._ilg.Emit(OpCodes.Call, mt);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00021440 File Offset: 0x0001F640
		private void BrfalseFar(Label l)
		{
			this._ilg.Emit(OpCodes.Brfalse, l);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00021453 File Offset: 0x0001F653
		private void BrtrueFar(Label l)
		{
			this._ilg.Emit(OpCodes.Brtrue, l);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00021466 File Offset: 0x0001F666
		private void BrFar(Label l)
		{
			this._ilg.Emit(OpCodes.Br, l);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00021479 File Offset: 0x0001F679
		private void BleFar(Label l)
		{
			this._ilg.Emit(OpCodes.Ble, l);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0002148C File Offset: 0x0001F68C
		private void BltFar(Label l)
		{
			this._ilg.Emit(OpCodes.Blt, l);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0002149F File Offset: 0x0001F69F
		private void BgeFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bge, l);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000214B2 File Offset: 0x0001F6B2
		private void BgtFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt, l);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000214C5 File Offset: 0x0001F6C5
		private void BneFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bne_Un, l);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000214D8 File Offset: 0x0001F6D8
		private void BeqFar(Label l)
		{
			this._ilg.Emit(OpCodes.Beq, l);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000214EB File Offset: 0x0001F6EB
		private void Brfalse(Label l)
		{
			this._ilg.Emit(OpCodes.Brfalse_S, l);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000214FE File Offset: 0x0001F6FE
		private void Br(Label l)
		{
			this._ilg.Emit(OpCodes.Br_S, l);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00021511 File Offset: 0x0001F711
		private void Ble(Label l)
		{
			this._ilg.Emit(OpCodes.Ble_S, l);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00021524 File Offset: 0x0001F724
		private void Blt(Label l)
		{
			this._ilg.Emit(OpCodes.Blt_S, l);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00021537 File Offset: 0x0001F737
		private void Bge(Label l)
		{
			this._ilg.Emit(OpCodes.Bge_S, l);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0002154A File Offset: 0x0001F74A
		private void Bgt(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt_S, l);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0002155D File Offset: 0x0001F75D
		private void Bgtun(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt_Un_S, l);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00021570 File Offset: 0x0001F770
		private void Bne(Label l)
		{
			this._ilg.Emit(OpCodes.Bne_Un_S, l);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00021583 File Offset: 0x0001F783
		private void Beq(Label l)
		{
			this._ilg.Emit(OpCodes.Beq_S, l);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00021596 File Offset: 0x0001F796
		private void Ldlen()
		{
			this._ilg.Emit(OpCodes.Ldlen);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000215A8 File Offset: 0x0001F7A8
		private void Rightchar()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Callvirt(RegexCompiler.s_getcharM);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000215D0 File Offset: 0x0001F7D0
		private void Rightcharnext()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Dup();
			this.Ldc(1);
			this.Add();
			this.Stloc(this._textposV);
			this.Callvirt(RegexCompiler.s_getcharM);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0002161F File Offset: 0x0001F81F
		private void Leftchar()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub();
			this.Callvirt(RegexCompiler.s_getcharM);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00021654 File Offset: 0x0001F854
		private void Leftcharnext()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub();
			this.Dup();
			this.Stloc(this._textposV);
			this.Callvirt(RegexCompiler.s_getcharM);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000216A3 File Offset: 0x0001F8A3
		private void Track()
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddTrack());
			this.DoPush();
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000216BD File Offset: 0x0001F8BD
		private void Trackagain()
		{
			this.ReadyPushTrack();
			this.Ldc(this._backpos);
			this.DoPush();
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000216D7 File Offset: 0x0001F8D7
		private void PushTrack(LocalBuilder lt)
		{
			this.ReadyPushTrack();
			this.Ldloc(lt);
			this.DoPush();
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000216EC File Offset: 0x0001F8EC
		private void TrackUnique(int i)
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddUniqueTrack(i));
			this.DoPush();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00021707 File Offset: 0x0001F907
		private void TrackUnique2(int i)
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddUniqueTrack(i, 256));
			this.DoPush();
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00021728 File Offset: 0x0001F928
		private void ReadyPushTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Sub);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc_S, this._trackposV);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000217A8 File Offset: 0x0001F9A8
		private void PopTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00021837 File Offset: 0x0001FA37
		private void TopTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00021875 File Offset: 0x0001FA75
		private void PushStack(LocalBuilder lt)
		{
			this.ReadyPushStack();
			this._ilg.Emit(OpCodes.Ldloc_S, lt);
			this.DoPush();
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00021894 File Offset: 0x0001FA94
		internal void ReadyReplaceStack(int i)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			if (i != 0)
			{
				this.Ldc(i);
				this._ilg.Emit(OpCodes.Add);
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000218E8 File Offset: 0x0001FAE8
		private void ReadyPushStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Sub);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00021967 File Offset: 0x0001FB67
		private void TopStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000219A8 File Offset: 0x0001FBA8
		private void PopStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00021A37 File Offset: 0x0001FC37
		private void PopDiscardStack()
		{
			this.PopDiscardStack(1);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00021A40 File Offset: 0x0001FC40
		private void PopDiscardStack(int i)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this.Ldc(i);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00021A90 File Offset: 0x0001FC90
		private void DoReplace()
		{
			this._ilg.Emit(OpCodes.Stelem_I4);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00021A90 File Offset: 0x0001FC90
		private void DoPush()
		{
			this._ilg.Emit(OpCodes.Stelem_I4);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00021AA2 File Offset: 0x0001FCA2
		private void Back()
		{
			this._ilg.Emit(OpCodes.Br, this._backtrack);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00021ABC File Offset: 0x0001FCBC
		private void Goto(int i)
		{
			if (i < this._codepos)
			{
				Label label = this.DefineLabel();
				this.Ldloc(this._trackposV);
				this.Ldc(this._trackcount * 4);
				this.Ble(label);
				this.Ldloc(this._stackposV);
				this.Ldc(this._trackcount * 3);
				this.BgtFar(this._labels[i]);
				this.MarkLabel(label);
				this.ReadyPushTrack();
				this.Ldc(this.AddGoto(i));
				this.DoPush();
				this.BrFar(this._backtrack);
				return;
			}
			this.BrFar(this._labels[i]);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00021B68 File Offset: 0x0001FD68
		private int NextCodepos()
		{
			return this._codepos + RegexCode.OpcodeSize(this._codes[this._codepos]);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00021B83 File Offset: 0x0001FD83
		private Label AdvanceLabel()
		{
			return this._labels[this.NextCodepos()];
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00021B96 File Offset: 0x0001FD96
		private void Advance()
		{
			this._ilg.Emit(OpCodes.Br, this.AdvanceLabel());
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00021BAE File Offset: 0x0001FDAE
		private void CallToLower()
		{
			if ((this._options & RegexOptions.CultureInvariant) != RegexOptions.None)
			{
				this.Call(RegexCompiler.s_getInvariantCulture);
			}
			else
			{
				this.Call(RegexCompiler.s_getCurrentCulture);
			}
			this.Call(RegexCompiler.s_chartolowerM);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		private void GenerateForwardSection()
		{
			this._labels = new Label[this._codes.Length];
			this._goto = new int[this._codes.Length];
			for (int i = 0; i < this._codes.Length; i += RegexCode.OpcodeSize(this._codes[i]))
			{
				this._goto[i] = -1;
				this._labels[i] = this._ilg.DefineLabel();
			}
			this._uniquenote = new int[10];
			for (int j = 0; j < 10; j++)
			{
				this._uniquenote[j] = -1;
			}
			this.Mvfldloc(RegexCompiler.s_textF, this._textV);
			this.Mvfldloc(RegexCompiler.s_textstartF, this._textstartV);
			this.Mvfldloc(RegexCompiler.s_textbegF, this._textbegV);
			this.Mvfldloc(RegexCompiler.s_textendF, this._textendV);
			this.Mvfldloc(RegexCompiler.s_textposF, this._textposV);
			this.Mvfldloc(RegexCompiler.s_trackF, this._trackV);
			this.Mvfldloc(RegexCompiler.s_trackposF, this._trackposV);
			this.Mvfldloc(RegexCompiler.s_stackF, this._stackV);
			this.Mvfldloc(RegexCompiler.s_stackposF, this._stackposV);
			this._backpos = -1;
			for (int i = 0; i < this._codes.Length; i += RegexCode.OpcodeSize(this._codes[i]))
			{
				this.MarkLabel(this._labels[i]);
				this._codepos = i;
				this._regexopcode = this._codes[i];
				this.GenerateOneCode();
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00021D68 File Offset: 0x0001FF68
		private void GenerateMiddleSection()
		{
			this.DefineLabel();
			this.MarkLabel(this._backtrack);
			this.Mvlocfld(this._trackposV, RegexCompiler.s_trackposF);
			this.Mvlocfld(this._stackposV, RegexCompiler.s_stackposF);
			this.Ldthis();
			this.Callvirt(RegexCompiler.s_ensurestorageM);
			this.Mvfldloc(RegexCompiler.s_trackposF, this._trackposV);
			this.Mvfldloc(RegexCompiler.s_stackposF, this._stackposV);
			this.Mvfldloc(RegexCompiler.s_trackF, this._trackV);
			this.Mvfldloc(RegexCompiler.s_stackF, this._stackV);
			this.PopTrack();
			Label[] array = new Label[this._notecount];
			for (int i = 0; i < this._notecount; i++)
			{
				array[i] = this._notes[i]._label;
			}
			this._ilg.Emit(OpCodes.Switch, array);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00021E48 File Offset: 0x00020048
		private void GenerateBacktrackSection()
		{
			for (int i = 0; i < this._notecount; i++)
			{
				RegexCompiler.BacktrackNote backtrackNote = this._notes[i];
				if (backtrackNote._flags != 0)
				{
					this._ilg.MarkLabel(backtrackNote._label);
					this._codepos = backtrackNote._codepos;
					this._backpos = i;
					this._regexopcode = this._codes[backtrackNote._codepos] | backtrackNote._flags;
					this.GenerateOneCode();
				}
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00021EBC File Offset: 0x000200BC
		protected void GenerateFindFirstChar()
		{
			this._textposV = this.DeclareInt();
			this._textV = this.DeclareString();
			this._tempV = this.DeclareInt();
			this._temp2V = this.DeclareInt();
			if ((this._anchors & 53) != 0)
			{
				if (!this._code.RightToLeft)
				{
					if ((this._anchors & 1) != 0)
					{
						Label label = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Ble(label);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Stfld(RegexCompiler.s_textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label);
					}
					if ((this._anchors & 4) != 0)
					{
						Label label2 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textstartF);
						this.Ble(label2);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Stfld(RegexCompiler.s_textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label2);
					}
					if ((this._anchors & 16) != 0)
					{
						Label label3 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Ldc(1);
						this.Sub();
						this.Bge(label3);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Ldc(1);
						this.Sub();
						this.Stfld(RegexCompiler.s_textposF);
						this.MarkLabel(label3);
					}
					if ((this._anchors & 32) != 0)
					{
						Label label4 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Bge(label4);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Stfld(RegexCompiler.s_textposF);
						this.MarkLabel(label4);
					}
				}
				else
				{
					if ((this._anchors & 32) != 0)
					{
						Label label5 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Bge(label5);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Stfld(RegexCompiler.s_textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label5);
					}
					if ((this._anchors & 16) != 0)
					{
						Label label6 = this.DefineLabel();
						Label label7 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Ldc(1);
						this.Sub();
						this.Blt(label6);
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textendF);
						this.Beq(label7);
						this.Ldthisfld(RegexCompiler.s_textF);
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Callvirt(RegexCompiler.s_getcharM);
						this.Ldc(10);
						this.Beq(label7);
						this.MarkLabel(label6);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Stfld(RegexCompiler.s_textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label7);
					}
					if ((this._anchors & 4) != 0)
					{
						Label label8 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textstartF);
						this.Bge(label8);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Stfld(RegexCompiler.s_textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label8);
					}
					if ((this._anchors & 1) != 0)
					{
						Label label9 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler.s_textposF);
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Ble(label9);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler.s_textbegF);
						this.Stfld(RegexCompiler.s_textposF);
						this.MarkLabel(label9);
					}
				}
				this.Ldc(1);
				this.Ret();
				return;
			}
			if (this._bmPrefix != null && this._bmPrefix.NegativeUnicode == null)
			{
				LocalBuilder tempV = this._tempV;
				LocalBuilder tempV2 = this._tempV;
				LocalBuilder temp2V = this._temp2V;
				Label label10 = this.DefineLabel();
				Label label11 = this.DefineLabel();
				Label label12 = this.DefineLabel();
				Label label13 = this.DefineLabel();
				this.DefineLabel();
				Label label14 = this.DefineLabel();
				int num;
				int num2;
				if (!this._code.RightToLeft)
				{
					num = -1;
					num2 = this._bmPrefix.Pattern.Length - 1;
				}
				else
				{
					num = this._bmPrefix.Pattern.Length;
					num2 = 0;
				}
				int num3 = (int)this._bmPrefix.Pattern[num2];
				this.Mvfldloc(RegexCompiler.s_textF, this._textV);
				if (!this._code.RightToLeft)
				{
					this.Ldthisfld(RegexCompiler.s_textendF);
				}
				else
				{
					this.Ldthisfld(RegexCompiler.s_textbegF);
				}
				this.Stloc(temp2V);
				this.Ldthisfld(RegexCompiler.s_textposF);
				if (!this._code.RightToLeft)
				{
					this.Ldc(this._bmPrefix.Pattern.Length - 1);
					this.Add();
				}
				else
				{
					this.Ldc(this._bmPrefix.Pattern.Length);
					this.Sub();
				}
				this.Stloc(this._textposV);
				this.Br(label13);
				this.MarkLabel(label10);
				if (!this._code.RightToLeft)
				{
					this.Ldc(this._bmPrefix.Pattern.Length);
				}
				else
				{
					this.Ldc(-this._bmPrefix.Pattern.Length);
				}
				this.MarkLabel(label11);
				this.Ldloc(this._textposV);
				this.Add();
				this.Stloc(this._textposV);
				this.MarkLabel(label13);
				this.Ldloc(this._textposV);
				this.Ldloc(temp2V);
				if (!this._code.RightToLeft)
				{
					this.BgeFar(label12);
				}
				else
				{
					this.BltFar(label12);
				}
				this.Rightchar();
				if (this._bmPrefix.CaseInsensitive)
				{
					this.CallToLower();
				}
				this.Dup();
				this.Stloc(tempV);
				this.Ldc(num3);
				this.BeqFar(label14);
				this.Ldloc(tempV);
				this.Ldc(this._bmPrefix.LowASCII);
				this.Sub();
				this.Dup();
				this.Stloc(tempV);
				this.Ldc(this._bmPrefix.HighASCII - this._bmPrefix.LowASCII);
				this.Bgtun(label10);
				Label[] array = new Label[this._bmPrefix.HighASCII - this._bmPrefix.LowASCII + 1];
				for (int i = this._bmPrefix.LowASCII; i <= this._bmPrefix.HighASCII; i++)
				{
					if (this._bmPrefix.NegativeASCII[i] == num)
					{
						array[i - this._bmPrefix.LowASCII] = label10;
					}
					else
					{
						array[i - this._bmPrefix.LowASCII] = this.DefineLabel();
					}
				}
				this.Ldloc(tempV);
				this._ilg.Emit(OpCodes.Switch, array);
				for (int i = this._bmPrefix.LowASCII; i <= this._bmPrefix.HighASCII; i++)
				{
					if (this._bmPrefix.NegativeASCII[i] != num)
					{
						this.MarkLabel(array[i - this._bmPrefix.LowASCII]);
						this.Ldc(this._bmPrefix.NegativeASCII[i]);
						this.BrFar(label11);
					}
				}
				this.MarkLabel(label14);
				this.Ldloc(this._textposV);
				this.Stloc(tempV2);
				for (int i = this._bmPrefix.Pattern.Length - 2; i >= 0; i--)
				{
					Label label15 = this.DefineLabel();
					int num4;
					if (!this._code.RightToLeft)
					{
						num4 = i;
					}
					else
					{
						num4 = this._bmPrefix.Pattern.Length - 1 - i;
					}
					this.Ldloc(this._textV);
					this.Ldloc(tempV2);
					this.Ldc(1);
					this.Sub(this._code.RightToLeft);
					this.Dup();
					this.Stloc(tempV2);
					this.Callvirt(RegexCompiler.s_getcharM);
					if (this._bmPrefix.CaseInsensitive)
					{
						this.CallToLower();
					}
					this.Ldc((int)this._bmPrefix.Pattern[num4]);
					this.Beq(label15);
					this.Ldc(this._bmPrefix.Positive[num4]);
					this.BrFar(label11);
					this.MarkLabel(label15);
				}
				this.Ldthis();
				this.Ldloc(tempV2);
				if (this._code.RightToLeft)
				{
					this.Ldc(1);
					this.Add();
				}
				this.Stfld(RegexCompiler.s_textposF);
				this.Ldc(1);
				this.Ret();
				this.MarkLabel(label12);
				this.Ldthis();
				if (!this._code.RightToLeft)
				{
					this.Ldthisfld(RegexCompiler.s_textendF);
				}
				else
				{
					this.Ldthisfld(RegexCompiler.s_textbegF);
				}
				this.Stfld(RegexCompiler.s_textposF);
				this.Ldc(0);
				this.Ret();
				return;
			}
			if (this._fcPrefix == null)
			{
				this.Ldc(1);
				this.Ret();
				return;
			}
			LocalBuilder temp2V2 = this._temp2V;
			LocalBuilder tempV3 = this._tempV;
			Label label16 = this.DefineLabel();
			Label label17 = this.DefineLabel();
			Label label18 = this.DefineLabel();
			Label label19 = this.DefineLabel();
			Label label20 = this.DefineLabel();
			this.Mvfldloc(RegexCompiler.s_textposF, this._textposV);
			this.Mvfldloc(RegexCompiler.s_textF, this._textV);
			if (!this._code.RightToLeft)
			{
				this.Ldthisfld(RegexCompiler.s_textendF);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldthisfld(RegexCompiler.s_textbegF);
			}
			this.Sub();
			this.Stloc(temp2V2);
			this.Ldloc(temp2V2);
			this.Ldc(0);
			this.BleFar(label19);
			this.MarkLabel(label16);
			this.Ldloc(temp2V2);
			this.Ldc(1);
			this.Sub();
			this.Stloc(temp2V2);
			if (this._code.RightToLeft)
			{
				this.Leftcharnext();
			}
			else
			{
				this.Rightcharnext();
			}
			if (this._fcPrefix.GetValueOrDefault().CaseInsensitive)
			{
				this.CallToLower();
			}
			if (!RegexCharClass.IsSingleton(this._fcPrefix.GetValueOrDefault().Prefix))
			{
				this.Ldstr(this._fcPrefix.GetValueOrDefault().Prefix);
				this.Call(RegexCompiler.s_charInSetM);
				this.BrtrueFar(label17);
			}
			else
			{
				this.Ldc((int)RegexCharClass.SingletonChar(this._fcPrefix.GetValueOrDefault().Prefix));
				this.Beq(label17);
			}
			this.MarkLabel(label20);
			this.Ldloc(temp2V2);
			this.Ldc(0);
			if (!RegexCharClass.IsSingleton(this._fcPrefix.GetValueOrDefault().Prefix))
			{
				this.BgtFar(label16);
			}
			else
			{
				this.Bgt(label16);
			}
			this.Ldc(0);
			this.BrFar(label18);
			this.MarkLabel(label17);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub(this._code.RightToLeft);
			this.Stloc(this._textposV);
			this.Ldc(1);
			this.MarkLabel(label18);
			this.Mvlocfld(this._textposV, RegexCompiler.s_textposF);
			this.Ret();
			this.MarkLabel(label19);
			this.Ldc(0);
			this.Ret();
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00022A2B File Offset: 0x00020C2B
		protected void GenerateInitTrackCount()
		{
			this.Ldthis();
			this.Ldc(this._trackcount);
			this.Stfld(RegexCompiler.s_trackcountF);
			this.Ret();
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00022A50 File Offset: 0x00020C50
		private LocalBuilder DeclareInt()
		{
			return this._ilg.DeclareLocal(typeof(int));
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00022A67 File Offset: 0x00020C67
		private LocalBuilder DeclareIntArray()
		{
			return this._ilg.DeclareLocal(typeof(int[]));
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00022A7E File Offset: 0x00020C7E
		private LocalBuilder DeclareString()
		{
			return this._ilg.DeclareLocal(typeof(string));
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00022A98 File Offset: 0x00020C98
		protected void GenerateGo()
		{
			this._textposV = this.DeclareInt();
			this._textV = this.DeclareString();
			this._trackposV = this.DeclareInt();
			this._trackV = this.DeclareIntArray();
			this._stackposV = this.DeclareInt();
			this._stackV = this.DeclareIntArray();
			this._tempV = this.DeclareInt();
			this._temp2V = this.DeclareInt();
			this._temp3V = this.DeclareInt();
			this._textbegV = this.DeclareInt();
			this._textendV = this.DeclareInt();
			this._textstartV = this.DeclareInt();
			this._labels = null;
			this._notes = null;
			this._notecount = 0;
			this._backtrack = this.DefineLabel();
			this.GenerateForwardSection();
			this.GenerateMiddleSection();
			this.GenerateBacktrackSection();
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00022B68 File Offset: 0x00020D68
		private void GenerateOneCode()
		{
			this.Ldthis();
			this.Callvirt(RegexCompiler.s_checkTimeoutM);
			int regexopcode = this._regexopcode;
			if (regexopcode <= 285)
			{
				if (regexopcode <= 164)
				{
					switch (regexopcode)
					{
					case 0:
					case 1:
					case 2:
					case 64:
					case 65:
					case 66:
						goto IL_1438;
					case 3:
					case 4:
					case 5:
					case 67:
					case 68:
					case 69:
						goto IL_1604;
					case 6:
					case 7:
					case 8:
					case 70:
					case 71:
					case 72:
						goto IL_18EF;
					case 9:
					case 10:
					case 11:
					case 73:
					case 74:
					case 75:
						break;
					case 12:
						goto IL_1024;
					case 13:
					case 77:
						goto IL_11F6;
					case 14:
					{
						Label label = this._labels[this.NextCodepos()];
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ble(label);
						this.Leftchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					}
					case 15:
					{
						Label label2 = this._labels[this.NextCodepos()];
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Bge(label2);
						this.Rightchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					}
					case 16:
					case 17:
						this.Ldthis();
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ldloc(this._textendV);
						this.Callvirt(RegexCompiler.s_isboundaryM);
						if (this.Code() == 16)
						{
							this.BrfalseFar(this._backtrack);
							return;
						}
						this.BrtrueFar(this._backtrack);
						return;
					case 18:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.BgtFar(this._backtrack);
						return;
					case 19:
						this.Ldloc(this._textposV);
						this.Ldthisfld(RegexCompiler.s_textstartF);
						this.BneFar(this._backtrack);
						return;
					case 20:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Ldc(1);
						this.Sub();
						this.BltFar(this._backtrack);
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Bge(this._labels[this.NextCodepos()]);
						this.Rightchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					case 21:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.BltFar(this._backtrack);
						return;
					case 22:
						this.Back();
						return;
					case 23:
						this.PushTrack(this._textposV);
						this.Track();
						return;
					case 24:
					{
						LocalBuilder tempV = this._tempV;
						Label label3 = this.DefineLabel();
						this.PopStack();
						this.Dup();
						this.Stloc(tempV);
						this.PushTrack(tempV);
						this.Ldloc(this._textposV);
						this.Beq(label3);
						this.PushTrack(this._textposV);
						this.PushStack(this._textposV);
						this.Track();
						this.Goto(this.Operand(0));
						this.MarkLabel(label3);
						this.TrackUnique2(5);
						return;
					}
					case 25:
					{
						LocalBuilder tempV2 = this._tempV;
						Label label4 = this.DefineLabel();
						Label label5 = this.DefineLabel();
						Label label6 = this.DefineLabel();
						this.PopStack();
						this.Dup();
						this.Stloc(tempV2);
						this.Ldloc(tempV2);
						this.Ldc(-1);
						this.Beq(label5);
						this.PushTrack(tempV2);
						this.Br(label6);
						this.MarkLabel(label5);
						this.PushTrack(this._textposV);
						this.MarkLabel(label6);
						this.Ldloc(this._textposV);
						this.Beq(label4);
						this.PushTrack(this._textposV);
						this.Track();
						this.Br(this.AdvanceLabel());
						this.MarkLabel(label4);
						this.ReadyPushStack();
						this.Ldloc(tempV2);
						this.DoPush();
						this.TrackUnique2(6);
						return;
					}
					case 26:
						this.ReadyPushStack();
						this.Ldc(-1);
						this.DoPush();
						this.ReadyPushStack();
						this.Ldc(this.Operand(0));
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 27:
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldc(this.Operand(0));
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 28:
					{
						LocalBuilder tempV3 = this._tempV;
						LocalBuilder temp2V = this._temp2V;
						Label label7 = this.DefineLabel();
						Label label8 = this.DefineLabel();
						this.PopStack();
						this.Stloc(tempV3);
						this.PopStack();
						this.Dup();
						this.Stloc(temp2V);
						this.PushTrack(temp2V);
						this.Ldloc(this._textposV);
						this.Bne(label7);
						this.Ldloc(tempV3);
						this.Ldc(0);
						this.Bge(label8);
						this.MarkLabel(label7);
						this.Ldloc(tempV3);
						this.Ldc(this.Operand(1));
						this.Bge(label8);
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldloc(tempV3);
						this.Ldc(1);
						this.Add();
						this.DoPush();
						this.Track();
						this.Goto(this.Operand(0));
						this.MarkLabel(label8);
						this.PushTrack(tempV3);
						this.TrackUnique2(7);
						return;
					}
					case 29:
					{
						LocalBuilder tempV4 = this._tempV;
						LocalBuilder temp2V2 = this._temp2V;
						Label label9 = this.DefineLabel();
						this.DefineLabel();
						Label[] labels = this._labels;
						this.NextCodepos();
						this.PopStack();
						this.Stloc(tempV4);
						this.PopStack();
						this.Stloc(temp2V2);
						this.Ldloc(tempV4);
						this.Ldc(0);
						this.Bge(label9);
						this.PushTrack(temp2V2);
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldloc(tempV4);
						this.Ldc(1);
						this.Add();
						this.DoPush();
						this.TrackUnique2(8);
						this.Goto(this.Operand(0));
						this.MarkLabel(label9);
						this.PushTrack(temp2V2);
						this.PushTrack(tempV4);
						this.PushTrack(this._textposV);
						this.Track();
						return;
					}
					case 30:
						this.ReadyPushStack();
						this.Ldc(-1);
						this.DoPush();
						this.TrackUnique(0);
						return;
					case 31:
						this.PushStack(this._textposV);
						this.TrackUnique(0);
						return;
					case 32:
						if (this.Operand(1) != -1)
						{
							this.Ldthis();
							this.Ldc(this.Operand(1));
							this.Callvirt(RegexCompiler.s_ismatchedM);
							this.BrfalseFar(this._backtrack);
						}
						this.PopStack();
						this.Stloc(this._tempV);
						if (this.Operand(1) != -1)
						{
							this.Ldthis();
							this.Ldc(this.Operand(0));
							this.Ldc(this.Operand(1));
							this.Ldloc(this._tempV);
							this.Ldloc(this._textposV);
							this.Callvirt(RegexCompiler.s_transferM);
						}
						else
						{
							this.Ldthis();
							this.Ldc(this.Operand(0));
							this.Ldloc(this._tempV);
							this.Ldloc(this._textposV);
							this.Callvirt(RegexCompiler.s_captureM);
						}
						this.PushTrack(this._tempV);
						if (this.Operand(0) != -1 && this.Operand(1) != -1)
						{
							this.TrackUnique(4);
							return;
						}
						this.TrackUnique(3);
						return;
					case 33:
						this.ReadyPushTrack();
						this.PopStack();
						this.Dup();
						this.Stloc(this._textposV);
						this.DoPush();
						this.Track();
						return;
					case 34:
						this.ReadyPushStack();
						this.Ldthisfld(RegexCompiler.s_trackF);
						this.Ldlen();
						this.Ldloc(this._trackposV);
						this.Sub();
						this.DoPush();
						this.ReadyPushStack();
						this.Ldthis();
						this.Callvirt(RegexCompiler.s_crawlposM);
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 35:
					{
						Label label10 = this.DefineLabel();
						Label label11 = this.DefineLabel();
						this.PopStack();
						this.Ldthisfld(RegexCompiler.s_trackF);
						this.Ldlen();
						this.PopStack();
						this.Sub();
						this.Stloc(this._trackposV);
						this.Dup();
						this.Ldthis();
						this.Callvirt(RegexCompiler.s_crawlposM);
						this.Beq(label11);
						this.MarkLabel(label10);
						this.Ldthis();
						this.Callvirt(RegexCompiler.s_uncaptureM);
						this.Dup();
						this.Ldthis();
						this.Callvirt(RegexCompiler.s_crawlposM);
						this.Bne(label10);
						this.MarkLabel(label11);
						this.Pop();
						this.Back();
						return;
					}
					case 36:
						this.PopStack();
						this.Stloc(this._tempV);
						this.Ldthisfld(RegexCompiler.s_trackF);
						this.Ldlen();
						this.PopStack();
						this.Sub();
						this.Stloc(this._trackposV);
						this.PushTrack(this._tempV);
						this.TrackUnique(9);
						return;
					case 37:
						this.Ldthis();
						this.Ldc(this.Operand(0));
						this.Callvirt(RegexCompiler.s_ismatchedM);
						this.BrfalseFar(this._backtrack);
						return;
					case 38:
						this.Goto(this.Operand(0));
						return;
					case 39:
					case 43:
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 57:
					case 58:
					case 59:
					case 60:
					case 61:
					case 62:
					case 63:
						goto IL_1AE4;
					case 40:
						this.Mvlocfld(this._textposV, RegexCompiler.s_textposF);
						this.Ret();
						return;
					case 41:
					case 42:
						this.Ldthis();
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ldloc(this._textendV);
						this.Callvirt(RegexCompiler.s_isECMABoundaryM);
						if (this.Code() == 41)
						{
							this.BrfalseFar(this._backtrack);
							return;
						}
						this.BrtrueFar(this._backtrack);
						return;
					case 76:
						goto IL_110B;
					default:
						switch (regexopcode)
						{
						case 131:
						case 132:
						case 133:
							goto IL_184F;
						case 134:
						case 135:
						case 136:
							goto IL_19D9;
						case 137:
						case 138:
						case 139:
						case 140:
						case 141:
						case 142:
						case 143:
						case 144:
						case 145:
						case 146:
						case 147:
						case 148:
						case 149:
						case 150:
						case 163:
							goto IL_1AE4;
						case 151:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.Goto(this.Operand(0));
							return;
						case 152:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PopStack();
							this.Pop();
							this.TrackUnique2(5);
							this.Advance();
							return;
						case 153:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PushStack(this._textposV);
							this.TrackUnique2(6);
							this.Goto(this.Operand(0));
							return;
						case 154:
						case 155:
							this.PopDiscardStack(2);
							this.Back();
							return;
						case 156:
						{
							LocalBuilder tempV5 = this._tempV;
							Label label12 = this.DefineLabel();
							this.PopStack();
							this.Ldc(1);
							this.Sub();
							this.Dup();
							this.Stloc(tempV5);
							this.Ldc(0);
							this.Blt(label12);
							this.PopStack();
							this.Stloc(this._textposV);
							this.PushTrack(tempV5);
							this.TrackUnique2(7);
							this.Advance();
							this.MarkLabel(label12);
							this.ReadyReplaceStack(0);
							this.PopTrack();
							this.DoReplace();
							this.PushStack(tempV5);
							this.Back();
							return;
						}
						case 157:
						{
							Label label13 = this.DefineLabel();
							LocalBuilder tempV6 = this._tempV;
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PopTrack();
							this.Dup();
							this.Stloc(tempV6);
							this.Ldc(this.Operand(1));
							this.Bge(label13);
							this.Ldloc(this._textposV);
							this.TopTrack();
							this.Beq(label13);
							this.PushStack(this._textposV);
							this.ReadyPushStack();
							this.Ldloc(tempV6);
							this.Ldc(1);
							this.Add();
							this.DoPush();
							this.TrackUnique2(8);
							this.Goto(this.Operand(0));
							this.MarkLabel(label13);
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.PushStack(tempV6);
							this.Back();
							return;
						}
						case 158:
						case 159:
							this.PopDiscardStack();
							this.Back();
							return;
						case 160:
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.Ldthis();
							this.Callvirt(RegexCompiler.s_uncaptureM);
							if (this.Operand(0) != -1 && this.Operand(1) != -1)
							{
								this.Ldthis();
								this.Callvirt(RegexCompiler.s_uncaptureM);
							}
							this.Back();
							return;
						case 161:
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.Back();
							return;
						case 162:
							this.PopDiscardStack(2);
							this.Back();
							return;
						case 164:
						{
							Label label14 = this.DefineLabel();
							Label label15 = this.DefineLabel();
							this.PopTrack();
							this.Dup();
							this.Ldthis();
							this.Callvirt(RegexCompiler.s_crawlposM);
							this.Beq(label15);
							this.MarkLabel(label14);
							this.Ldthis();
							this.Callvirt(RegexCompiler.s_uncaptureM);
							this.Dup();
							this.Ldthis();
							this.Callvirt(RegexCompiler.s_crawlposM);
							this.Bne(label14);
							this.MarkLabel(label15);
							this.Pop();
							this.Back();
							return;
						}
						default:
							goto IL_1AE4;
						}
						break;
					}
				}
				else
				{
					if (regexopcode - 195 <= 2)
					{
						goto IL_184F;
					}
					if (regexopcode - 198 <= 2)
					{
						goto IL_19D9;
					}
					switch (regexopcode)
					{
					case 280:
						this.ReadyPushStack();
						this.PopTrack();
						this.DoPush();
						this.Back();
						return;
					case 281:
						this.ReadyReplaceStack(0);
						this.PopTrack();
						this.DoReplace();
						this.Back();
						return;
					case 282:
					case 283:
						goto IL_1AE4;
					case 284:
						this.PopTrack();
						this.Stloc(this._tempV);
						this.ReadyPushStack();
						this.PopTrack();
						this.DoPush();
						this.PushStack(this._tempV);
						this.Back();
						return;
					case 285:
						this.ReadyReplaceStack(1);
						this.PopTrack();
						this.DoReplace();
						this.ReadyReplaceStack(0);
						this.TopStack();
						this.Ldc(1);
						this.Sub();
						this.DoReplace();
						this.Back();
						return;
					default:
						goto IL_1AE4;
					}
				}
			}
			else if (regexopcode <= 645)
			{
				switch (regexopcode)
				{
				case 512:
				case 513:
				case 514:
					goto IL_1438;
				case 515:
				case 516:
				case 517:
					goto IL_1604;
				case 518:
				case 519:
				case 520:
					goto IL_18EF;
				case 521:
				case 522:
				case 523:
					break;
				case 524:
					goto IL_1024;
				case 525:
					goto IL_11F6;
				default:
					switch (regexopcode)
					{
					case 576:
					case 577:
					case 578:
						goto IL_1438;
					case 579:
					case 580:
					case 581:
						goto IL_1604;
					case 582:
					case 583:
					case 584:
						goto IL_18EF;
					case 585:
					case 586:
					case 587:
						break;
					case 588:
						goto IL_110B;
					case 589:
						goto IL_11F6;
					default:
						if (regexopcode - 643 > 2)
						{
							goto IL_1AE4;
						}
						goto IL_184F;
					}
					break;
				}
			}
			else
			{
				if (regexopcode - 646 <= 2)
				{
					goto IL_19D9;
				}
				if (regexopcode - 707 <= 2)
				{
					goto IL_184F;
				}
				if (regexopcode - 710 > 2)
				{
					goto IL_1AE4;
				}
				goto IL_19D9;
			}
			this.Ldloc(this._textposV);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.BgeFar(this._backtrack);
				this.Rightcharnext();
			}
			else
			{
				this.Ldloc(this._textbegV);
				this.BleFar(this._backtrack);
				this.Leftcharnext();
			}
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 11)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler.s_charInSetM);
				this.BrfalseFar(this._backtrack);
				return;
			}
			this.Ldc(this.Operand(0));
			if (this.Code() == 9)
			{
				this.BneFar(this._backtrack);
				return;
			}
			this.BeqFar(this._backtrack);
			return;
			IL_1024:
			string text = this._strings[this.Operand(0)];
			this.Ldc(text.Length);
			this.Ldloc(this._textendV);
			this.Ldloc(this._textposV);
			this.Sub();
			this.BgtFar(this._backtrack);
			for (int i = 0; i < text.Length; i++)
			{
				this.Ldloc(this._textV);
				this.Ldloc(this._textposV);
				if (i != 0)
				{
					this.Ldc(i);
					this.Add();
				}
				this.Callvirt(RegexCompiler.s_getcharM);
				if (this.IsCi())
				{
					this.CallToLower();
				}
				this.Ldc((int)text[i]);
				this.BneFar(this._backtrack);
			}
			this.Ldloc(this._textposV);
			this.Ldc(text.Length);
			this.Add();
			this.Stloc(this._textposV);
			return;
			IL_110B:
			string text2 = this._strings[this.Operand(0)];
			this.Ldc(text2.Length);
			this.Ldloc(this._textposV);
			this.Ldloc(this._textbegV);
			this.Sub();
			this.BgtFar(this._backtrack);
			int j = text2.Length;
			while (j > 0)
			{
				j--;
				this.Ldloc(this._textV);
				this.Ldloc(this._textposV);
				this.Ldc(text2.Length - j);
				this.Sub();
				this.Callvirt(RegexCompiler.s_getcharM);
				if (this.IsCi())
				{
					this.CallToLower();
				}
				this.Ldc((int)text2[j]);
				this.BneFar(this._backtrack);
			}
			this.Ldloc(this._textposV);
			this.Ldc(text2.Length);
			this.Sub();
			this.Stloc(this._textposV);
			return;
			IL_11F6:
			LocalBuilder tempV7 = this._tempV;
			LocalBuilder temp2V3 = this._temp2V;
			Label label16 = this.DefineLabel();
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler.s_ismatchedM);
			if ((this._options & RegexOptions.ECMAScript) != RegexOptions.None)
			{
				this.Brfalse(this.AdvanceLabel());
			}
			else
			{
				this.BrfalseFar(this._backtrack);
			}
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler.s_matchlengthM);
			this.Dup();
			this.Stloc(tempV7);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldloc(this._textbegV);
			}
			this.Sub();
			this.BgtFar(this._backtrack);
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler.s_matchindexM);
			if (!this.IsRtl())
			{
				this.Ldloc(tempV7);
				this.Add(this.IsRtl());
			}
			this.Stloc(temp2V3);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV7);
			this.Add(this.IsRtl());
			this.Stloc(this._textposV);
			this.MarkLabel(label16);
			this.Ldloc(tempV7);
			this.Ldc(0);
			this.Ble(this.AdvanceLabel());
			this.Ldloc(this._textV);
			this.Ldloc(temp2V3);
			this.Ldloc(tempV7);
			if (this.IsRtl())
			{
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV7);
			}
			this.Sub(this.IsRtl());
			this.Callvirt(RegexCompiler.s_getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV7);
			if (!this.IsRtl())
			{
				this.Dup();
				this.Ldc(1);
				this.Sub();
				this.Stloc(tempV7);
			}
			this.Sub(this.IsRtl());
			this.Callvirt(RegexCompiler.s_getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			this.Beq(label16);
			this.Back();
			return;
			IL_1438:
			LocalBuilder tempV8 = this._tempV;
			Label label17 = this.DefineLabel();
			int num = this.Operand(1);
			if (num == 0)
			{
				return;
			}
			this.Ldc(num);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldloc(this._textbegV);
			}
			this.Sub();
			this.BgtFar(this._backtrack);
			this.Ldloc(this._textposV);
			this.Ldc(num);
			this.Add(this.IsRtl());
			this.Stloc(this._textposV);
			this.Ldc(num);
			this.Stloc(tempV8);
			this.MarkLabel(label17);
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV8);
			if (this.IsRtl())
			{
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV8);
				this.Add();
			}
			else
			{
				this.Dup();
				this.Ldc(1);
				this.Sub();
				this.Stloc(tempV8);
				this.Sub();
			}
			this.Callvirt(RegexCompiler.s_getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 2)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler.s_charInSetM);
				this.BrfalseFar(this._backtrack);
			}
			else
			{
				this.Ldc(this.Operand(0));
				if (this.Code() == 0)
				{
					this.BneFar(this._backtrack);
				}
				else
				{
					this.BeqFar(this._backtrack);
				}
			}
			this.Ldloc(tempV8);
			this.Ldc(0);
			if (this.Code() == 2)
			{
				this.BgtFar(label17);
				return;
			}
			this.Bgt(label17);
			return;
			IL_1604:
			LocalBuilder tempV9 = this._tempV;
			LocalBuilder temp2V4 = this._temp2V;
			Label label18 = this.DefineLabel();
			Label label19 = this.DefineLabel();
			int num2 = this.Operand(1);
			if (num2 != 0)
			{
				if (!this.IsRtl())
				{
					this.Ldloc(this._textendV);
					this.Ldloc(this._textposV);
				}
				else
				{
					this.Ldloc(this._textposV);
					this.Ldloc(this._textbegV);
				}
				this.Sub();
				if (num2 != 2147483647)
				{
					Label label20 = this.DefineLabel();
					this.Dup();
					this.Ldc(num2);
					this.Blt(label20);
					this.Pop();
					this.Ldc(num2);
					this.MarkLabel(label20);
				}
				this.Dup();
				this.Stloc(temp2V4);
				this.Ldc(1);
				this.Add();
				this.Stloc(tempV9);
				this.MarkLabel(label18);
				this.Ldloc(tempV9);
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV9);
				this.Ldc(0);
				if (this.Code() == 5)
				{
					this.BleFar(label19);
				}
				else
				{
					this.Ble(label19);
				}
				if (this.IsRtl())
				{
					this.Leftcharnext();
				}
				else
				{
					this.Rightcharnext();
				}
				if (this.IsCi())
				{
					this.CallToLower();
				}
				if (this.Code() == 5)
				{
					this.Ldstr(this._strings[this.Operand(0)]);
					this.Call(RegexCompiler.s_charInSetM);
					this.BrtrueFar(label18);
				}
				else
				{
					this.Ldc(this.Operand(0));
					if (this.Code() == 3)
					{
						this.Beq(label18);
					}
					else
					{
						this.Bne(label18);
					}
				}
				this.Ldloc(this._textposV);
				this.Ldc(1);
				this.Sub(this.IsRtl());
				this.Stloc(this._textposV);
				this.MarkLabel(label19);
				this.Ldloc(temp2V4);
				this.Ldloc(tempV9);
				this.Ble(this.AdvanceLabel());
				this.ReadyPushTrack();
				this.Ldloc(temp2V4);
				this.Ldloc(tempV9);
				this.Sub();
				this.Ldc(1);
				this.Sub();
				this.DoPush();
				this.ReadyPushTrack();
				this.Ldloc(this._textposV);
				this.Ldc(1);
				this.Sub(this.IsRtl());
				this.DoPush();
				this.Track();
				return;
			}
			return;
			IL_184F:
			this.PopTrack();
			this.Stloc(this._textposV);
			this.PopTrack();
			this.Stloc(this._tempV);
			this.Ldloc(this._tempV);
			this.Ldc(0);
			this.BleFar(this.AdvanceLabel());
			this.ReadyPushTrack();
			this.Ldloc(this._tempV);
			this.Ldc(1);
			this.Sub();
			this.DoPush();
			this.ReadyPushTrack();
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub(this.IsRtl());
			this.DoPush();
			this.Trackagain();
			this.Advance();
			return;
			IL_18EF:
			LocalBuilder tempV10 = this._tempV;
			int num3 = this.Operand(1);
			if (num3 != 0)
			{
				if (!this.IsRtl())
				{
					this.Ldloc(this._textendV);
					this.Ldloc(this._textposV);
				}
				else
				{
					this.Ldloc(this._textposV);
					this.Ldloc(this._textbegV);
				}
				this.Sub();
				if (num3 != 2147483647)
				{
					Label label21 = this.DefineLabel();
					this.Dup();
					this.Ldc(num3);
					this.Blt(label21);
					this.Pop();
					this.Ldc(num3);
					this.MarkLabel(label21);
				}
				this.Dup();
				this.Stloc(tempV10);
				this.Ldc(0);
				this.Ble(this.AdvanceLabel());
				this.ReadyPushTrack();
				this.Ldloc(tempV10);
				this.Ldc(1);
				this.Sub();
				this.DoPush();
				this.PushTrack(this._textposV);
				this.Track();
				return;
			}
			return;
			IL_19D9:
			this.PopTrack();
			this.Stloc(this._textposV);
			this.PopTrack();
			this.Stloc(this._temp2V);
			if (!this.IsRtl())
			{
				this.Rightcharnext();
			}
			else
			{
				this.Leftcharnext();
			}
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 8)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler.s_charInSetM);
				this.BrfalseFar(this._backtrack);
			}
			else
			{
				this.Ldc(this.Operand(0));
				if (this.Code() == 6)
				{
					this.BneFar(this._backtrack);
				}
				else
				{
					this.BeqFar(this._backtrack);
				}
			}
			this.Ldloc(this._temp2V);
			this.Ldc(0);
			this.BleFar(this.AdvanceLabel());
			this.ReadyPushTrack();
			this.Ldloc(this._temp2V);
			this.Ldc(1);
			this.Sub();
			this.DoPush();
			this.PushTrack(this._textposV);
			this.Trackagain();
			this.Advance();
			return;
			IL_1AE4:
			throw new NotImplementedException("Unimplemented state.");
		}

		// Token: 0x04000567 RID: 1383
		private static FieldInfo s_textbegF = RegexCompiler.RegexRunnerField("runtextbeg");

		// Token: 0x04000568 RID: 1384
		private static FieldInfo s_textendF = RegexCompiler.RegexRunnerField("runtextend");

		// Token: 0x04000569 RID: 1385
		private static FieldInfo s_textstartF = RegexCompiler.RegexRunnerField("runtextstart");

		// Token: 0x0400056A RID: 1386
		private static FieldInfo s_textposF = RegexCompiler.RegexRunnerField("runtextpos");

		// Token: 0x0400056B RID: 1387
		private static FieldInfo s_textF = RegexCompiler.RegexRunnerField("runtext");

		// Token: 0x0400056C RID: 1388
		private static FieldInfo s_trackposF = RegexCompiler.RegexRunnerField("runtrackpos");

		// Token: 0x0400056D RID: 1389
		private static FieldInfo s_trackF = RegexCompiler.RegexRunnerField("runtrack");

		// Token: 0x0400056E RID: 1390
		private static FieldInfo s_stackposF = RegexCompiler.RegexRunnerField("runstackpos");

		// Token: 0x0400056F RID: 1391
		private static FieldInfo s_stackF = RegexCompiler.RegexRunnerField("runstack");

		// Token: 0x04000570 RID: 1392
		private static FieldInfo s_trackcountF = RegexCompiler.RegexRunnerField("runtrackcount");

		// Token: 0x04000571 RID: 1393
		private static MethodInfo s_ensurestorageM = RegexCompiler.RegexRunnerMethod("EnsureStorage");

		// Token: 0x04000572 RID: 1394
		private static MethodInfo s_captureM = RegexCompiler.RegexRunnerMethod("Capture");

		// Token: 0x04000573 RID: 1395
		private static MethodInfo s_transferM = RegexCompiler.RegexRunnerMethod("TransferCapture");

		// Token: 0x04000574 RID: 1396
		private static MethodInfo s_uncaptureM = RegexCompiler.RegexRunnerMethod("Uncapture");

		// Token: 0x04000575 RID: 1397
		private static MethodInfo s_ismatchedM = RegexCompiler.RegexRunnerMethod("IsMatched");

		// Token: 0x04000576 RID: 1398
		private static MethodInfo s_matchlengthM = RegexCompiler.RegexRunnerMethod("MatchLength");

		// Token: 0x04000577 RID: 1399
		private static MethodInfo s_matchindexM = RegexCompiler.RegexRunnerMethod("MatchIndex");

		// Token: 0x04000578 RID: 1400
		private static MethodInfo s_isboundaryM = RegexCompiler.RegexRunnerMethod("IsBoundary");

		// Token: 0x04000579 RID: 1401
		private static MethodInfo s_isECMABoundaryM = RegexCompiler.RegexRunnerMethod("IsECMABoundary");

		// Token: 0x0400057A RID: 1402
		private static MethodInfo s_chartolowerM = typeof(char).GetMethod("ToLower", new Type[]
		{
			typeof(char),
			typeof(CultureInfo)
		});

		// Token: 0x0400057B RID: 1403
		private static MethodInfo s_getcharM = typeof(string).GetMethod("get_Chars", new Type[] { typeof(int) });

		// Token: 0x0400057C RID: 1404
		private static MethodInfo s_crawlposM = RegexCompiler.RegexRunnerMethod("Crawlpos");

		// Token: 0x0400057D RID: 1405
		private static MethodInfo s_charInSetM = RegexCompiler.RegexRunnerMethod("CharInClass");

		// Token: 0x0400057E RID: 1406
		private static MethodInfo s_getCurrentCulture = typeof(CultureInfo).GetMethod("get_CurrentCulture");

		// Token: 0x0400057F RID: 1407
		private static MethodInfo s_getInvariantCulture = typeof(CultureInfo).GetMethod("get_InvariantCulture");

		// Token: 0x04000580 RID: 1408
		private static MethodInfo s_checkTimeoutM = RegexCompiler.RegexRunnerMethod("CheckTimeout");

		// Token: 0x04000581 RID: 1409
		protected ILGenerator _ilg;

		// Token: 0x04000582 RID: 1410
		private LocalBuilder _textstartV;

		// Token: 0x04000583 RID: 1411
		private LocalBuilder _textbegV;

		// Token: 0x04000584 RID: 1412
		private LocalBuilder _textendV;

		// Token: 0x04000585 RID: 1413
		private LocalBuilder _textposV;

		// Token: 0x04000586 RID: 1414
		private LocalBuilder _textV;

		// Token: 0x04000587 RID: 1415
		private LocalBuilder _trackposV;

		// Token: 0x04000588 RID: 1416
		private LocalBuilder _trackV;

		// Token: 0x04000589 RID: 1417
		private LocalBuilder _stackposV;

		// Token: 0x0400058A RID: 1418
		private LocalBuilder _stackV;

		// Token: 0x0400058B RID: 1419
		private LocalBuilder _tempV;

		// Token: 0x0400058C RID: 1420
		private LocalBuilder _temp2V;

		// Token: 0x0400058D RID: 1421
		private LocalBuilder _temp3V;

		// Token: 0x0400058E RID: 1422
		protected RegexCode _code;

		// Token: 0x0400058F RID: 1423
		protected int[] _codes;

		// Token: 0x04000590 RID: 1424
		protected string[] _strings;

		// Token: 0x04000591 RID: 1425
		protected RegexPrefix? _fcPrefix;

		// Token: 0x04000592 RID: 1426
		protected RegexBoyerMoore _bmPrefix;

		// Token: 0x04000593 RID: 1427
		protected int _anchors;

		// Token: 0x04000594 RID: 1428
		private Label[] _labels;

		// Token: 0x04000595 RID: 1429
		private RegexCompiler.BacktrackNote[] _notes;

		// Token: 0x04000596 RID: 1430
		private int _notecount;

		// Token: 0x04000597 RID: 1431
		protected int _trackcount;

		// Token: 0x04000598 RID: 1432
		private Label _backtrack;

		// Token: 0x04000599 RID: 1433
		private int _regexopcode;

		// Token: 0x0400059A RID: 1434
		private int _codepos;

		// Token: 0x0400059B RID: 1435
		private int _backpos;

		// Token: 0x0400059C RID: 1436
		protected RegexOptions _options;

		// Token: 0x0400059D RID: 1437
		private int[] _uniquenote;

		// Token: 0x0400059E RID: 1438
		private int[] _goto;

		// Token: 0x0200013B RID: 315
		private sealed class BacktrackNote
		{
			// Token: 0x060006D0 RID: 1744 RVA: 0x00024664 File Offset: 0x00022864
			public BacktrackNote(int flags, Label label, int codepos)
			{
				this._codepos = codepos;
				this._flags = flags;
				this._label = label;
			}

			// Token: 0x0400059F RID: 1439
			internal int _codepos;

			// Token: 0x040005A0 RID: 1440
			internal int _flags;

			// Token: 0x040005A1 RID: 1441
			internal Label _label;
		}
	}
}
