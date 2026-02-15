using System;
using System.Runtime.CompilerServices;

namespace YgomGame.Enquete
{
	// Token: 0x02000C26 RID: 3110
	public abstract class SheetContentContextBase : ContextBase, ISheetContentContext, IContext
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060058B3 RID: 22707
		public abstract SheetContentType sheetContentType { get; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x0000216A File Offset: 0x0000036A
		public RootContext rootContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x060058B5 RID: 22709 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060058B6 RID: 22710 RVA: 0x0000216D File Offset: 0x0000036D
		public SheetContentGroupContext groupContext
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060058B7 RID: 22711 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool isInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x000F4D1D File Offset: 0x000F2F1D
		public SheetContentContextBase(RootContext rootContext)
		{
		}

		// Token: 0x060058B9 RID: 22713 RVA: 0x000F4D1D File Offset: 0x000F2F1D
		public SheetContentContextBase(object jsonData, RootContext rootContext)
		{
		}

		// Token: 0x060058BA RID: 22714 RVA: 0x0000216A File Offset: 0x0000036A
		public static ISheetContentContext CreateByJsonData(object jsonData, RootContext rootContext)
		{
			return null;
		}

		// Token: 0x040094DA RID: 38106
		public readonly RootContext m_RootContext;
	}
}
