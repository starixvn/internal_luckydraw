// ----------------------------------------------------------------------
// <copyright file="PrizeModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Models.Prize
{
    using System;

    public class PrizeModel
    {
        public int PrizeId { get; set; }

        public string Name { get; set; }

        public PrizeTypeModel PrizeType { get; set; }

        public int PrizeIndex { get; set; }

        public Byte[] Image { get; set; }

        public string PriceProgram { get; set; }
    }
}