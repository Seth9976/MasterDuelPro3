using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace System.Resources
{
	// Token: 0x020005CD RID: 1485
	internal class FileBasedResourceGroveler : IResourceGroveler
	{
		// Token: 0x06002BE2 RID: 11234 RVA: 0x000AD008 File Offset: 0x000AB208
		public FileBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
		{
			this._mediator = mediator;
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000AD018 File Offset: 0x000AB218
		public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark)
		{
			ResourceSet resourceSet = null;
			string resourceFileName = this._mediator.GetResourceFileName(culture);
			string text = this.FindResourceFile(culture, resourceFileName);
			if (text == null)
			{
				if (tryParents && culture.HasInvariantCultureName)
				{
					throw new MissingManifestResourceException(string.Concat(new string[]
					{
						Environment.GetResourceString("Could not find any resources appropriate for the specified culture (or the neutral culture) on disk."),
						Environment.NewLine,
						"baseName: ",
						this._mediator.BaseNameField,
						"  locationInfo: ",
						(this._mediator.LocationInfo == null) ? "<null>" : this._mediator.LocationInfo.FullName,
						"  fileName: ",
						this._mediator.GetResourceFileName(culture)
					}));
				}
			}
			else
			{
				resourceSet = this.CreateResourceSet(text);
			}
			return resourceSet;
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000AD0E8 File Offset: 0x000AB2E8
		private string FindResourceFile(CultureInfo culture, string fileName)
		{
			if (this._mediator.ModuleDir != null)
			{
				string text = Path.Combine(this._mediator.ModuleDir, fileName);
				if (File.Exists(text))
				{
					return text;
				}
			}
			if (File.Exists(fileName))
			{
				return fileName;
			}
			return null;
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x000AD12C File Offset: 0x000AB32C
		private ResourceSet CreateResourceSet(string file)
		{
			if (this._mediator.UserResourceSet == null)
			{
				return new RuntimeResourceSet(file);
			}
			object[] array = new object[] { file };
			ResourceSet resourceSet;
			try
			{
				resourceSet = (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, array);
			}
			catch (MissingMethodException ex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("'{0}': ResourceSet derived classes must provide a constructor that takes a String file name and a constructor that takes a Stream.", new object[] { this._mediator.UserResourceSet.AssemblyQualifiedName }), ex);
			}
			return resourceSet;
		}

		// Token: 0x0400164B RID: 5707
		private ResourceManager.ResourceManagerMediator _mediator;
	}
}
