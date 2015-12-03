// ----------------------------------------------------------------------
// <copyright file="ServerController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.11.27">Created.</change>
// </history>

namespace LuckyDraw.Site.Controllers.API
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using LuckyDraw.Core.Enumerations;
    using LuckyDraw.Core.Model;
    using LuckyDraw.Criteria.Prize;
    using LuckyDraw.Criteria.Staff;
    using LuckyDraw.Models.Prize;
    using LuckyDraw.Models.Staff;
    using LuckyDraw.Services.Implements;
    using LuckyDraw.Services.Interfaces;

    public class ServerController : ApiController
    {
        private const int NumRow = 3;
        private readonly IPrizeService _prizeService;
        private readonly IStaffService _staffService;
        private readonly CommonController _commonController;

        public ServerController()
            : this(null, null, null)
        {
        }

        public ServerController(
            CommonController commonController,
            IPrizeService prizeService,
            IStaffService staffService)
        {
            _commonController = commonController ?? new CommonController();
            _prizeService = prizeService ?? new PrizeService();
            _staffService = staffService ?? new StaffService();
        }

        [HttpGet]
        public List<object> RenderListPrize(int prizeType)
        {
            PrizeSettingModel prizeSetting = _commonController.GetPrizeSettingByPrizeType(prizeType);
            int total = prizeSetting.Quantity / prizeSetting.Multiply,
                currentPrize = prizeSetting.CurrentPrize,
                numColumn = (total % NumRow).Equals(0) ? (total / NumRow) : ((total / NumRow) + 1);

            return RenderPrize(prizeType, total, currentPrize, numColumn);
        }

        [HttpGet]
        public WinnerDetailModel GetWinnerDetail(int prizeType, int currentPrize)
        {
            WinnerDetailModel winnerDetail = new WinnerDetailModel();

            GetPrizeByPrizeTypeCriteria prizeCriteria = new GetPrizeByPrizeTypeCriteria()
            {
                PrizeTypeId = prizeType
            };

            winnerDetail.Prize = _prizeService.GetPrizeByPrizeType(prizeCriteria).Skip(currentPrize - 1).Take(1).First();

            if (winnerDetail.Prize.PrizeId > 0)
            {
                GetWinnerByPrizeIdCriteria winnerCriteria = new GetWinnerByPrizeIdCriteria()
                {
                    PrizeIds = winnerDetail.Prize.PrizeId.ToString()
                };

                winnerDetail.Winners = _staffService.GetWinnerByPrizeId(winnerCriteria);
            }

            return winnerDetail;
        }

        [HttpGet]
        public WinnerDetailModel GetSuperGrandWinnerDetail(int prizeType, int currentPrize)
        {
            WinnerDetailModel winnerDetail = new WinnerDetailModel();
            int beginPrize = (currentPrize - 1) / 3;

            GetPrizeByPrizeTypeCriteria criteria = new GetPrizeByPrizeTypeCriteria()
            {
                PrizeTypeId = prizeType
            };

            winnerDetail.Prizes = _prizeService.GetPrizeByPrizeType(criteria).Skip((beginPrize) * 3).Take(3).OrderBy(x => x.PrizeIndex).ToList();

            if (winnerDetail.Prizes.Any())
            {
                GetWinnerByPrizeIdCriteria winnerCriteria = new GetWinnerByPrizeIdCriteria()
                {
                    PrizeIds = string.Join("-1,", winnerDetail.Prizes.Select(x => x.PrizeId))
                };

                winnerDetail.Winners = _staffService.GetWinnerByPrizeId(winnerCriteria);
            }

            return winnerDetail;
        }

        [HttpGet]
        public string GetPrizeNumber(int prizeIndex)
        {
            string imagePath = string.Empty;

            switch (prizeIndex)
            {
                case 1:
                    imagePath = "Images/Title/1.png";
                    break;

                case 2:
                    imagePath = "Images/Title/2.png";
                    break;

                case 3:
                    imagePath = "Images/Title/3.png";
                    break;

                case 4:
                    imagePath = "Images/Title/4.png";
                    break;

                case 5:
                    imagePath = "Images/Title/5.png";
                    break;

                case 6:
                    imagePath = "Images/Title/6.png";
                    break;

                case 7:
                    imagePath = "Images/Title/7.png";
                    break;

                case 8:
                    imagePath = "Images/Title/8.png";
                    break;

                case 9:
                    imagePath = "Images/Title/9.png";
                    break;

                default:
                    imagePath = "Images/Title/1.png";
                    break;
            }

            return imagePath;
        }

        [HttpPut]
        public CommandResult UpdateCurrentPrize(UpdateCurrentPrizeCriteria criteria)
        {
            return _prizeService.UpdateCurrentPrize(criteria);
        }

        [HttpDelete]
        public CommandResult DeleteWinner(string staffIds)
        {
            DeleteWinnerCriteria criteria = new DeleteWinnerCriteria()
            {
                StaffIds = staffIds
            };

            return _staffService.DeleteWinner(criteria);
        }

        private List<object> RenderPrize(int prizeType, int total, int currentPrize, int numColumn)
        {
            var result = new List<object>();

            for (int i = 0; i < NumRow; i++)
            {
                var itemRows = new List<object>();

                for (int j = 0; j < numColumn; j++)
                {
                    int index = (i * numColumn) + j;
                    bool isDone = (index + 1 > currentPrize) ? true : false;
                    string imagePath = string.Empty;

                    if (index < total)
                    {
                        switch (prizeType)
                        {
                            case (int)PrizeType.Cash:
                                imagePath = SetBackgroundForCashPrize(index + 1, isDone);
                                break;

                            case (int)PrizeType.Major:
                                imagePath = SetBackgroundForMajorPrize(index + 1, isDone);
                                break;

                            case (int)PrizeType.Grand:
                                imagePath = SetBackgroundForGrandPrize(index + 1, isDone);
                                break;

                            case (int)PrizeType.SuperGrand:
                                imagePath = SetBackgroundForSuperGrandPrize(index + 1, isDone);
                                break;
                        }

                        var itemRow = new
                        {
                            ImagePath = imagePath,
                            IsDone = isDone
                        };

                        itemRows.Add(itemRow);
                    }
                }

                result.Add(itemRows);
            }

            return result;
        }

        private string SetBackgroundForCashPrize(int prizeIndex, bool isDone)
        {
            if (isDone)
            {
                return SetBlockBackgroundForCashPrize(prizeIndex);
            }

            return SetUnblockBackgroundForCashPrize(prizeIndex);
        }

        private string SetBackgroundForMajorPrize(int prizeIndex, bool isDone)
        {
            if (isDone)
            {
                return SetBlockBackgroundForMajorPrize(prizeIndex);
            }

            return SetUnblockBackgroundForMajorPrize(prizeIndex);
        }

        private string SetBackgroundForGrandPrize(int prizeIndex, bool isDone)
        {
            if (isDone)
            {
                return SetBlockBackgroundForGrandPrize(prizeIndex);
            }

            return SetUnblockBackgroundForGrandPrize(prizeIndex);
        }

        private string SetBackgroundForSuperGrandPrize(int prizeIndex, bool isDone)
        {
            if (isDone)
            {
                return SetBlockBackgroundForSuperGrandPrize(prizeIndex);
            }

            return SetUnblockBackgroundForSuperGrandPrize(prizeIndex);
        }

        private string SetBlockBackgroundForCashPrize(int prizeIndex)
        {
            string imagePath = string.Empty;

            switch (prizeIndex)
            {
                case (int)CashPrizeIndex.Prize64To66:
                    imagePath = "Images/Title/66-65-64-after.png";
                    break;

                case (int)CashPrizeIndex.Prize67To69:
                    imagePath = "Images/Title/69-68-67-after.png";
                    break;

                case (int)CashPrizeIndex.Prize70To72:
                    imagePath = "Images/Title/72-71-70-after.png";
                    break;

                case (int)CashPrizeIndex.Prize73To75:
                    imagePath = "Images/Title/75-74-73-after.png";
                    break;

                case (int)CashPrizeIndex.Prize76To78:
                    imagePath = "Images/Title/78-77-76-after.png";
                    break;

                case (int)CashPrizeIndex.Prize79To81:
                    imagePath = "Images/Title/81-80-79-after.png";
                    break;

                case (int)CashPrizeIndex.Prize82To84:
                    imagePath = "Images/Title/84-83-82-after.png";
                    break;

                case (int)CashPrizeIndex.Prize85To87:
                    imagePath = "Images/Title/87-86-85-after.png";
                    break;

                case (int)CashPrizeIndex.Prize88To90:
                    imagePath = "Images/Title/90-89-88-after.png";
                    break;

                default:
                    imagePath = "Images/Title/blank-ticket-b.png";
                    break;
            }

            return imagePath;
        }

        private string SetUnblockBackgroundForCashPrize(int prizeIndex)
        {
            string imagePath = string.Empty;

            switch (prizeIndex)
            {
                case (int)CashPrizeIndex.Prize64To66:
                    imagePath = "Images/Title/66-65-64-before.png";
                    break;

                case (int)CashPrizeIndex.Prize67To69:
                    imagePath = "Images/Title/69-68-67-before.png";
                    break;

                case (int)CashPrizeIndex.Prize70To72:
                    imagePath = "Images/Title/72-71-70-before.png";
                    break;

                case (int)CashPrizeIndex.Prize73To75:
                    imagePath = "Images/Title/75-74-73-before.png";
                    break;

                case (int)CashPrizeIndex.Prize76To78:
                    imagePath = "Images/Title/78-77-76-before.png";
                    break;

                case (int)CashPrizeIndex.Prize79To81:
                    imagePath = "Images/Title/81-80-79-before.png";
                    break;

                case (int)CashPrizeIndex.Prize82To84:
                    imagePath = "Images/Title/84-83-82-before.png";
                    break;

                case (int)CashPrizeIndex.Prize85To87:
                    imagePath = "Images/Title/87-86-85-before.png";
                    break;

                case (int)CashPrizeIndex.Prize88To90:
                    imagePath = "Images/Title/90-89-88-before.png";
                    break;

                default:
                    imagePath = "Images/Title/blank-ticket-a.png";
                    break;
            }

            return imagePath;
        }

        private string SetBlockBackgroundForMajorPrize(int prizeIndex)
        {
            string imagePath = string.Empty;

            switch (prizeIndex)
            {
                case (int)MajorPrizeIndex.Prize40To42:
                    imagePath = "Images/Title/42-41-40-after.png";
                    break;

                case (int)MajorPrizeIndex.Prize43To45:
                    imagePath = "Images/Title/45-44-43-after.png";
                    break;

                case (int)MajorPrizeIndex.Prize46To48:
                    imagePath = "Images/Title/48-47-46-after.png";
                    break;

                case (int)MajorPrizeIndex.Prize49To51:
                    imagePath = "Images/Title/51-50-49-after.png";
                    break;

                case (int)MajorPrizeIndex.Prize52To54:
                    imagePath = "Images/Title/54-53-52-after.png";
                    break;

                case (int)MajorPrizeIndex.Prize55To57:
                    imagePath = "Images/Title/57-56-55-after.png";
                    break;

                case (int)MajorPrizeIndex.Prize58To60:
                    imagePath = "Images/Title/60-59-58-after.png";
                    break;

                case (int)MajorPrizeIndex.Prize61To63:
                    imagePath = "Images/Title/63-62-61-after.png";
                    break;

                default:
                    imagePath = "Images/Title/blank-ticket-b.png";
                    break;
            }

            return imagePath;
        }

        private string SetUnblockBackgroundForMajorPrize(int prizeIndex)
        {
            string imagePath = string.Empty;

            switch (prizeIndex)
            {
                case (int)MajorPrizeIndex.Prize40To42:
                    imagePath = "Images/Title/42-41-40-before.png";
                    break;

                case (int)MajorPrizeIndex.Prize43To45:
                    imagePath = "Images/Title/45-44-43-before.png";
                    break;

                case (int)MajorPrizeIndex.Prize46To48:
                    imagePath = "Images/Title/48-47-46-before.png";
                    break;

                case (int)MajorPrizeIndex.Prize49To51:
                    imagePath = "Images/Title/51-50-49-before.png";
                    break;

                case (int)MajorPrizeIndex.Prize52To54:
                    imagePath = "Images/Title/54-53-52-before.png";
                    break;

                case (int)MajorPrizeIndex.Prize55To57:
                    imagePath = "Images/Title/57-56-55-before.png";
                    break;

                case (int)MajorPrizeIndex.Prize58To60:
                    imagePath = "Images/Title/60-59-58-before.png";
                    break;

                case (int)MajorPrizeIndex.Prize61To63:
                    imagePath = "Images/Title/63-62-61-before.png";
                    break;

                default:
                    imagePath = "Images/Title/blank-ticket-a.png";
                    break;
            }

            return imagePath;
        }

        private string SetBlockBackgroundForGrandPrize(int prizeIndex)
        {
            string imagePath = string.Empty;

            switch (prizeIndex)
            {
                case (int)GrandPrizeIndex.Prize10To12:
                    imagePath = "Images/Title/12-11-10-after.png";
                    break;

                case (int)GrandPrizeIndex.Prize13To15:
                    imagePath = "Images/Title/15-14-13-after.png";
                    break;

                case (int)GrandPrizeIndex.Prize16To18:
                    imagePath = "Images/Title/18-17-16-after.png";
                    break;

                case (int)GrandPrizeIndex.Prize19To21:
                    imagePath = "Images/Title/21-20-19-after.png";
                    break;

                case (int)GrandPrizeIndex.Prize22To24:
                    imagePath = "Images/Title/24-23-22-after.png";
                    break;

                case (int)GrandPrizeIndex.Prize25To27:
                    imagePath = "Images/Title/27-26-25-after.png";
                    break;

                case (int)GrandPrizeIndex.Prize28To30:
                    imagePath = "Images/Title/30-29-28-after.png";
                    break;

                case (int)GrandPrizeIndex.Prize31To33:
                    imagePath = "Images/Title/33-32-31-after.png";
                    break;

                case (int)GrandPrizeIndex.Prize34To36:
                    imagePath = "Images/Title/36-35-34-after.png";
                    break;

                case (int)GrandPrizeIndex.Prize37To39:
                    imagePath = "Images/Title/39-38-37-after.png";
                    break;

                default:
                    imagePath = "Images/Title/blank-ticket-b.png";
                    break;
            }

            return imagePath;
        }

        private string SetUnblockBackgroundForGrandPrize(int prizeIndex)
        {
            string imagePath = string.Empty;

            switch (prizeIndex)
            {
                case (int)GrandPrizeIndex.Prize10To12:
                    imagePath = "Images/Title/12-11-10-before.png";
                    break;

                case (int)GrandPrizeIndex.Prize13To15:
                    imagePath = "Images/Title/15-14-13-before.png";
                    break;

                case (int)GrandPrizeIndex.Prize16To18:
                    imagePath = "Images/Title/18-17-16-before.png";
                    break;

                case (int)GrandPrizeIndex.Prize19To21:
                    imagePath = "Images/Title/21-20-19-before.png";
                    break;

                case (int)GrandPrizeIndex.Prize22To24:
                    imagePath = "Images/Title/24-23-22-before.png";
                    break;

                case (int)GrandPrizeIndex.Prize25To27:
                    imagePath = "Images/Title/27-26-25-before.png";
                    break;

                case (int)GrandPrizeIndex.Prize28To30:
                    imagePath = "Images/Title/30-29-28-before.png";
                    break;

                case (int)GrandPrizeIndex.Prize31To33:
                    imagePath = "Images/Title/33-32-31-before.png";
                    break;

                case (int)GrandPrizeIndex.Prize34To36:
                    imagePath = "Images/Title/36-35-34-before.png";
                    break;

                case (int)GrandPrizeIndex.Prize37To39:
                    imagePath = "Images/Title/39-38-37-before.png";
                    break;

                default:
                    imagePath = "Images/Title/blank-ticket-a.png";
                    break;
            }

            return imagePath;
        }

        private string SetBlockBackgroundForSuperGrandPrize(int prizeIndex)
        {
            string imagePath = string.Empty;

            switch (prizeIndex)
            {
                case (int)SuperGrandPrizeIndex.Prize01:
                    imagePath = "Images/Title/1-after.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize02:
                    imagePath = "Images/Title/2-after.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize03:
                    imagePath = "Images/Title/3-after.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize04:
                    imagePath = "Images/Title/4-after.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize05:
                    imagePath = "Images/Title/5-after.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize06:
                    imagePath = "Images/Title/6-after.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize07:
                    imagePath = "Images/Title/7-after.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize08:
                    imagePath = "Images/Title/8-after.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize09:
                    imagePath = "Images/Title/9-after.png";
                    break;

                default:
                    imagePath = "Images/Title/blank-ticket-b.png";
                    break;
            }

            return imagePath;
        }

        private string SetUnblockBackgroundForSuperGrandPrize(int prizeIndex)
        {
            string imagePath = string.Empty;

            switch (prizeIndex)
            {
                case (int)SuperGrandPrizeIndex.Prize01:
                    imagePath = "Images/Title/1-before.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize02:
                    imagePath = "Images/Title/2-before.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize03:
                    imagePath = "Images/Title/3-before.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize04:
                    imagePath = "Images/Title/4-before.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize05:
                    imagePath = "Images/Title/5-before.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize06:
                    imagePath = "Images/Title/6-before.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize07:
                    imagePath = "Images/Title/7-before.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize08:
                    imagePath = "Images/Title/8-before.png";
                    break;

                case (int)SuperGrandPrizeIndex.Prize09:
                    imagePath = "Images/Title/9-before.png";
                    break;

                default:
                    imagePath = "Images/Title/blank-ticket-a.png";
                    break;
            }

            return imagePath;
        }
    }
}