using System;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.CardPack.Open.Actor
{
	// Token: 0x020010D8 RID: 4312
	public abstract class ActorBase<T> : ElementWidgetBehaviourBase<T>, IActor where T : ActorBase<T>
	{
	}
}
