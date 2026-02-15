using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000261 RID: 609
	internal abstract class RenderGraphResourcePool<Type> : IRenderGraphResourcePool where Type : class
	{
		// Token: 0x0600107E RID: 4222
		protected abstract void ReleaseInternalResource(Type res);

		// Token: 0x0600107F RID: 4223
		protected abstract string GetResourceName(in Type res);

		// Token: 0x06001080 RID: 4224
		protected abstract long GetResourceSize(in Type res);

		// Token: 0x06001081 RID: 4225
		protected abstract string GetResourceTypeName();

		// Token: 0x06001082 RID: 4226
		protected abstract int GetSortIndex(Type res);

		// Token: 0x06001083 RID: 4227 RVA: 0x0003B8C4 File Offset: 0x00039AC4
		public void ReleaseResource(int hash, Type resource, int currentFrameIndex)
		{
			SortedList<int, ValueTuple<Type, int>> list;
			if (!this.m_ResourcePool.TryGetValue(hash, out list))
			{
				list = new SortedList<int, ValueTuple<Type, int>>();
				this.m_ResourcePool.Add(hash, list);
			}
			list.Add(this.GetSortIndex(resource), new ValueTuple<Type, int>(resource, currentFrameIndex));
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0003B908 File Offset: 0x00039B08
		public bool TryGetResource(int hashCode, out Type resource)
		{
			SortedList<int, ValueTuple<Type, int>> list;
			if (this.m_ResourcePool.TryGetValue(hashCode, out list) && list.Count > 0)
			{
				int index = list.Count - 1;
				resource = list.Values[index].Item1;
				list.RemoveAt(index);
				return true;
			}
			resource = default(Type);
			return false;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003B960 File Offset: 0x00039B60
		public override void Cleanup()
		{
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<Type, int>>> kvp in this.m_ResourcePool)
			{
				foreach (KeyValuePair<int, ValueTuple<Type, int>> res in kvp.Value)
				{
					this.ReleaseInternalResource(res.Value.Item1);
				}
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0003B9F4 File Offset: 0x00039BF4
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		public void RegisterFrameAllocation(int hash, Type value)
		{
			if (RenderGraph.enableValidityChecks && hash != -1)
			{
				this.m_FrameAllocatedResources.Add(new ValueTuple<int, Type>(hash, value));
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0003BA13 File Offset: 0x00039C13
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		public void UnregisterFrameAllocation(int hash, Type value)
		{
			if (RenderGraph.enableValidityChecks && hash != -1)
			{
				this.m_FrameAllocatedResources.Remove(new ValueTuple<int, Type>(hash, value));
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00005704 File Offset: 0x00003904
		public override void CheckFrameAllocation(bool onException, int frameIndex)
		{
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0003BA34 File Offset: 0x00039C34
		public override void LogResources(RenderGraphLogger logger)
		{
			List<RenderGraphResourcePool<Type>.ResourceLogInfo> allocationList = new List<RenderGraphResourcePool<Type>.ResourceLogInfo>();
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<Type, int>>> kvp in this.m_ResourcePool)
			{
				foreach (KeyValuePair<int, ValueTuple<Type, int>> res in kvp.Value)
				{
					allocationList.Add(new RenderGraphResourcePool<Type>.ResourceLogInfo
					{
						name = this.GetResourceName(in res.Value.Item1),
						size = this.GetResourceSize(in res.Value.Item1)
					});
				}
			}
			logger.LogLine("== " + this.GetResourceTypeName() + " Resources ==", Array.Empty<object>());
			allocationList.Sort(delegate(RenderGraphResourcePool<Type>.ResourceLogInfo a, RenderGraphResourcePool<Type>.ResourceLogInfo b)
			{
				if (a.size >= b.size)
				{
					return -1;
				}
				return 1;
			});
			int index = 0;
			float total = 0f;
			foreach (RenderGraphResourcePool<Type>.ResourceLogInfo element in allocationList)
			{
				float size = (float)element.size / 1048576f;
				total += size;
				logger.LogLine(string.Format("[{0:D2}]\t[{1:0.00} MB]\t{2}", index++, size, element.name), Array.Empty<object>());
			}
			logger.LogLine(string.Format("\nTotal Size [{0:0.00}]", total), Array.Empty<object>());
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0003BBF8 File Offset: 0x00039DF8
		public override void PurgeUnusedResources(int currentFrameIndex)
		{
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<Type, int>>> kvp in this.m_ResourcePool)
			{
				RenderGraphResourcePool<Type>.s_ToRemoveList.Clear();
				SortedList<int, ValueTuple<Type, int>> list = kvp.Value;
				IList<int> keys = list.Keys;
				IList<ValueTuple<Type, int>> values = list.Values;
				for (int i = 0; i < list.Count; i++)
				{
					ValueTuple<Type, int> value = values[i];
					int key = keys[i];
					if (value.Item2 + 10 < currentFrameIndex)
					{
						this.ReleaseInternalResource(value.Item1);
						RenderGraphResourcePool<Type>.s_ToRemoveList.Add(key);
					}
				}
				for (int j = 0; j < RenderGraphResourcePool<Type>.s_ToRemoveList.Count; j++)
				{
					list.Remove(RenderGraphResourcePool<Type>.s_ToRemoveList[j]);
				}
			}
		}

		// Token: 0x04000A80 RID: 2688
		[TupleElementNames(new string[] { "resource", "frameIndex" })]
		protected Dictionary<int, SortedList<int, ValueTuple<Type, int>>> m_ResourcePool = new Dictionary<int, SortedList<int, ValueTuple<Type, int>>>();

		// Token: 0x04000A81 RID: 2689
		private List<ValueTuple<int, Type>> m_FrameAllocatedResources = new List<ValueTuple<int, Type>>();

		// Token: 0x04000A82 RID: 2690
		private const int kStaleResourceLifetime = 10;

		// Token: 0x04000A83 RID: 2691
		private static List<int> s_ToRemoveList = new List<int>(32);

		// Token: 0x02000262 RID: 610
		private struct ResourceLogInfo
		{
			// Token: 0x04000A84 RID: 2692
			public string name;

			// Token: 0x04000A85 RID: 2693
			public long size;
		}
	}
}
