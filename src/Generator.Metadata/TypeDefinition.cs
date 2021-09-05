namespace Generator.Metadata
{
    public abstract class TypeDefinition
    {
        public string Name { get; set; }
        public bool IsNullable { get; set; }

        public virtual T Cast<T>()
            where T : TypeDefinition
        {
            return (T)this;
        }
    }

    public class SystemTypeDefinition : TypeDefinition
    {

    }

    public class ModelTypeDefinition : TypeDefinition
    {
        public ModelDefinition Model { get; set; }
    }

    public class EnumTypeDefinition : TypeDefinition
    {
        public EnumDefinition Enum { get; set; }
    }
}
