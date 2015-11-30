// ----------------------------------------------------------------------
// <copyright file="PrizeTypeModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Models.Prize
{
    using LuckyDraw.Core.Enumerations;
    using LuckyDraw.Core.Utilities;

    public class PrizeTypeModel
    {
        public int PrizeTypeId { get; set; }

        public string Name
        {
            get
            {
                return Enumerations.GetDescription((PrizeType)PrizeTypeId);
            }
        }
    }
}