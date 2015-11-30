// ----------------------------------------------------------------------
// <copyright file="PrizeType.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Core.Enumerations
{
    using System.ComponentModel;

    public enum PrizeType : int
    {
        None = 0,

        [Description("Ad-hoc")]
        Adhoc = 1,

        [Description("Super Grand")]
        SuperGrand = 2,

        [Description("Grand")]
        Grand = 3,

        [Description("Major")]
        Major = 4,

        [Description("Cash")]
        Cash = 5
    }
}