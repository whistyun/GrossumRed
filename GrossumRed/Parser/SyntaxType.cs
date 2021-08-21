namespace GrossumRed.Parser
{
    public enum SyntaxType
    {
        Attribute,
        AttributeList,
        BlockEnd,
        Comment,
        DefDelegate,
        DefEvent,
        DefField,
        PropGetterOrSetterWithBlock,
        PropGetterOrSetterWithLambda,
        PropertyGet,
        PropertySet,
        StartClass,
        StartFieldWithInit,
        StartInterface,
        StartMethodWithBlock,
        StartMethodWithLambda,
        StartNamespace,
        StartProperty,
        Using,
        Whitespace,
    }
}
