using AsmResolver.DotNet;
using AsmResolver.DotNet.Code.Cil;
using AsmResolver.PE.DotNet.Cil;
using Deoxys.Core;

namespace Deoxys.Pipeline.DevirtualizationStages
{
    public class Cleanup : IDevirtualizationStage
    {
        public string Name => nameof(Cleanup);
        public bool Execute(DeoxysContext context)
        {
            PurgeCtor(context);
            RemoveDLL(context);
            context.Cfg.DeclaringType.Fields.Remove(context.Cfg);
            return true;
        }

        public void PurgeCtor(DeoxysContext context)
        {
            //Clearing the .ctor
            var ctor = context.Module.GetOrCreateModuleConstructor();
            ctor.CilMethodBody = new CilMethodBody(ctor);
            ctor.CilMethodBody.Instructions.Add(new CilInstruction(CilOpCodes.Ret));
            context.Logger.Success("Purged NashaVM Runtime Initializer");
        }

        public void RemoveDLL(DeoxysContext context)
        {
            //Removing the dll reference
            var dll = context.Cfg.Signature.FieldType.GetUnderlyingTypeDefOrRef().Scope;
            context.Module.AssemblyReferences.Remove(new AssemblyReference(dll.GetAssembly()));
            context.Logger.Success($"Successfully Removed DLL {dll.Name}");
        }
    }
}