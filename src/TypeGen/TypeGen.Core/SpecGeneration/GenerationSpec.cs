using System;
using System.Collections.Generic;
using System.Reflection;
using TypeGen.Core.TypeAnnotations;

namespace TypeGen.Core.SpecGeneration
{
    public abstract class GenerationSpec
    {
        internal IDictionary<Assembly, AssemblySpec> AssemblySpecs { get; }
        internal IDictionary<Type, TypeSpec> TypeSpecs { get; }

        protected GenerationSpec()
        {
            AssemblySpecs = new Dictionary<Assembly, AssemblySpec>();
            TypeSpecs = new Dictionary<Type, TypeSpec>();
        }

        protected AssemblySpecBuilder ForAssembly(Assembly assembly)
        {
            var assemblySpec = new AssemblySpec();
            AssemblySpecs[assembly] = assemblySpec;
            
            return new AssemblySpecBuilder(assemblySpec);
        }

        protected ClassSpecBuilder AddClass(Type type, string outputDir = null)
        {
            TypeSpec typeSpec = AddTypeSpec(type, new ExportTsClassAttribute { OutputDir = outputDir });
            return new ClassSpecBuilder(typeSpec);
        }
        
        protected Generic.ClassSpecBuilder<T> AddClass<T>(string outputDir = null) where T : class
        {
            TypeSpec typeSpec = AddTypeSpec(typeof(T), new ExportTsClassAttribute { OutputDir = outputDir });
            return new Generic.ClassSpecBuilder<T>(typeSpec);
        }
        
        protected InterfaceSpecBuilder AddInterface(Type type, string outputDir = null)
        {
            TypeSpec typeSpec = AddTypeSpec(type, new ExportTsInterfaceAttribute { OutputDir = outputDir });
            return new InterfaceSpecBuilder(typeSpec);
        }
        
        protected Generic.InterfaceSpecBuilder<T> AddInterface<T>(string outputDir = null) where T : class
        {
            TypeSpec typeSpec = AddTypeSpec(typeof(T), new ExportTsInterfaceAttribute { OutputDir = outputDir });
            return new Generic.InterfaceSpecBuilder<T>(typeSpec);
        }

        protected EnumSpecBuilder AddEnum(Type type, string outputDir = null, bool isConst = false)
        {
            TypeSpec typeSpec = AddTypeSpec(type, new ExportTsEnumAttribute { OutputDir = outputDir, IsConst = isConst });
            return new EnumSpecBuilder(typeSpec);
        }

        protected Generic.EnumSpecBuilder<T> AddEnum<T>(string outputDir = null, bool isConst = false) where T : Enum
        {
            TypeSpec typeSpec = AddTypeSpec(typeof(T), new ExportTsEnumAttribute { OutputDir = outputDir, IsConst = isConst });
            return new Generic.EnumSpecBuilder<T>(typeSpec);
        }

        private TypeSpec AddTypeSpec(Type type, ExportAttribute exportAttribute)
        {
            var typeSpec = new TypeSpec(exportAttribute);
            TypeSpecs[type] = typeSpec;

            return typeSpec;
        }
    }
}