// ----------------------------------------------------------------------
// <copyright file="Grade.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Core.Enumerations
{
    using System.ComponentModel;

    public enum Grade : int
    {
        None = 0,

        [Description("Pink")]
        Pink = 10,

        [Description("Green")]
        Green = 11,

        [Description("Blue")]
        Blue = 12
    }
}