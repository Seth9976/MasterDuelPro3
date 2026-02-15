using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Rendering;

[assembly: AssemblyVersion("0.0.0.0")]
[assembly: InternalsVisibleTo("Unity.RenderPipelines.Core.Editor")]
[assembly: InternalsVisibleTo("Unity.RenderPipelines.Core.Editor.Tests")]
[assembly: RegisterGenericJobType(typeof(RegisterNewInstancesJob<BatchMeshID>))]
[assembly: RegisterGenericJobType(typeof(RegisterNewInstancesJob<BatchMaterialID>))]
[assembly: RegisterGenericJobType(typeof(FindNonRegisteredInstancesJob<BatchMeshID>))]
[assembly: RegisterGenericJobType(typeof(FindNonRegisteredInstancesJob<BatchMaterialID>))]
