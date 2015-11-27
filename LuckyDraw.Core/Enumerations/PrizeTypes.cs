// ----------------------------------------------------------------------
// <copyright file="PrizeTypes.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Core.Enumerations
{
    using System.ComponentModel;

    public enum PrizeTypes : int
    {
        None = 0,

        [Description("Super Grand")]
        SuperGrand = 1,

        [Description("Grand")]
        Grand = 2,

        [Description("Major")]
        Major = 3,

        [Description("Cash")]
        Cash = 4
    }
}