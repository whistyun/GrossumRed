﻿namespace GrossumRed.Parser
{
    public enum SyntaxType
    {
        Whitespace,
        Comment,
        AttributeList,
        StartNamespace,
        StartClass,
        StartProperty,
        PropertyGet,
        PropertySet,
        Using,
        BlockEnd,
        Attribute,

        StartFieldWithInit,
        DefField,
        DefEvent,
        StartMethodWithLambda,
        StartMethodWithBlock,
        PropGetterOrSetterWithLambda,
        PropGetterOrSetterWithBlock,
    }
}
