// ----------------------------------------------------------------------
// <copyright file="WinnerDetailModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.12.02">Created.</change>
// </history>

namespace LuckyDraw.Models.Staff
{
    using System.Collections.Generic;
    using System.Linq;
    using LuckyDraw.Models.Prize;

    public class WinnerDetailModel
    {
        public WinnerDetailModel()
        {
            Prizes = new List<PrizeModel>();
            Winners = new List<StaffModel>();
        }

        public bool IsSamePrize
        {
            get
            {
                bool isSamePrize = false;

                if (Prizes.Count > 1)
                {
                    isSamePrize = Prizes.All(x => x.Name.ToLower() == Prizes.First().Name.ToLower());
                }

                return isSamePrize;
            }
        }

        public PrizeModel Prize { get; set; }

        public IList<PrizeModel> Prizes { get; set; }

        public IList<StaffModel> Winners { get; set; }
    }
}