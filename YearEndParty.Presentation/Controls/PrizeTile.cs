using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace YearEndParty.Presentation.Controls
{
    public partial class PrizeTile : UserControl
    {
        public PrizeTypes PrizeType { get; set; }
        public int PrizeIndex { get; set; }
        public bool IsDone { get; set; }

        public PrizeTile()
        {
            InitializeComponent();

        }
        public PrizeTile(int prizeIndex, PrizeTypes prizeType, bool isDone)
        {
            InitializeComponent();
            PrizeIndex = prizeIndex;
            PrizeType = prizeType;
            IsDone = isDone;

            SetBackground(prizeIndex, isDone);
        }

        public void SetBackground(int prizeIndex, bool isDone)
        {
            switch (PrizeType)
            {
                case PrizeTypes.Cash:
                    SetBackgroundForCashPrize(prizeIndex, isDone);
                    break;
                case PrizeTypes.Major:
                    SetBackgroundForMajorPrize(prizeIndex, isDone);
                    break;
                case PrizeTypes.Grand:
                    SetBackgroundForGrandPrize(prizeIndex, isDone);
                    break;
                case PrizeTypes.SuperGrand:
                    SetBackgroundForSuperGrandPrize(prizeIndex, isDone);
                    break;
            }
        }

        private void SetBackgroundForCashPrize(int prizeIndex, bool isDone)
        {
            if (isDone)
            {
                SetBlockBackgroundForCashPrize(prizeIndex);
            }
            else
            {
                SetUnblockBackgroundForCashPrize(prizeIndex);
            }
        }

        private void SetBackgroundForMajorPrize(int prizeIndex, bool isDone)
        {
            if (isDone)
            {
                SetBlockBackgroundForMajorPrize(prizeIndex);
            }
            else
            {
                SetUnblockBackgroundForMajorPrize(prizeIndex);
            }
        }

        private void SetBackgroundForGrandPrize(int prizeIndex, bool isDone)
        {
            if (isDone)
            {
                SetBlockBackgroundForGrandPrize(prizeIndex);
            }
            else
            {
                SetUnblockBackgroundForGrandPrize(prizeIndex);
            }
        }

        private void SetBackgroundForSuperGrandPrize(int prizeIndex, bool isDone)
        {
            if (isDone)
            {
                SetBlockBackgroundForSuperGrandPrize(prizeIndex);
            }
            else
            {
                SetUnblockBackgroundForSuperGrandPrize(prizeIndex);
            }
        }

        private void SetBlockBackgroundForCashPrize(int prizeIndex) 
        {
            switch (prizeIndex)
            {
                case CashPrizeIndexEnum.Prize64To66:
                    BackgroundImage = Properties.Resources._66_65_64_after;
                    break;
                case CashPrizeIndexEnum.Prize67To69:
                    BackgroundImage = Properties.Resources._69_68_67_after;
                    break;
                case CashPrizeIndexEnum.Prize70To72:
                    BackgroundImage = Properties.Resources._72_71_70_after;
                    break;
                case CashPrizeIndexEnum.Prize73To75:
                    BackgroundImage = Properties.Resources._75_74_73_after;
                    break;
                case CashPrizeIndexEnum.Prize76To78:
                    BackgroundImage = Properties.Resources._78_77_76_after;
                    break;
                case CashPrizeIndexEnum.Prize79To81:
                    BackgroundImage = Properties.Resources._81_80_79_after;
                    break;
                case CashPrizeIndexEnum.Prize82To84:
                    BackgroundImage = Properties.Resources._84_83_82_after;
                    break;
                case CashPrizeIndexEnum.Prize85To87:
                    BackgroundImage = Properties.Resources._87_86_85_after;
                    break;
                case CashPrizeIndexEnum.Prize88To90: 
                    BackgroundImage = Properties.Resources._90_89_88_after;
                    break;
                default: 
                    BackgroundImage = Properties.Resources.blank_ticket_b;
                    break;
            }
        }

        private void SetUnblockBackgroundForCashPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case CashPrizeIndexEnum.Prize64To66:
                    BackgroundImage = Properties.Resources._66_65_64_before;
                    break;
                case CashPrizeIndexEnum.Prize67To69:
                    BackgroundImage = Properties.Resources._69_68_67_before;
                    break;
                case CashPrizeIndexEnum.Prize70To72:
                    BackgroundImage = Properties.Resources._72_71_70_before;
                    break;
                case CashPrizeIndexEnum.Prize73To75:
                    BackgroundImage = Properties.Resources._75_74_73_before;
                    break;
                case CashPrizeIndexEnum.Prize76To78:
                    BackgroundImage = Properties.Resources._78_77_76_before;
                    break;
                case CashPrizeIndexEnum.Prize79To81:
                    BackgroundImage = Properties.Resources._81_80_79_before;
                    break;
                case CashPrizeIndexEnum.Prize82To84:
                    BackgroundImage = Properties.Resources._84_83_82_before;
                    break;
                case CashPrizeIndexEnum.Prize85To87:
                    BackgroundImage = Properties.Resources._87_86_85_before;
                    break;
                case CashPrizeIndexEnum.Prize88To90:
                    BackgroundImage = Properties.Resources._90_89_88_before;
                    break;
                default:
                    BackgroundImage = Properties.Resources.blank_ticket_a;
                    break;
            }
        }

        private void SetBlockBackgroundForMajorPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case MajorPrizeIndexEnum.Prize40To42:
                    BackgroundImage = Properties.Resources._42_41_40_after;
                    break;
                case MajorPrizeIndexEnum.Prize43To45:
                    BackgroundImage = Properties.Resources._45_44_43_after;
                    break;
                case MajorPrizeIndexEnum.Prize46To48:
                    BackgroundImage = Properties.Resources._48_47_46_after;
                    break;
                case MajorPrizeIndexEnum.Prize49To51:
                    BackgroundImage = Properties.Resources._51_50_49_after;
                    break;
                case MajorPrizeIndexEnum.Prize52To54:
                    BackgroundImage = Properties.Resources._54_53_52_after;
                    break;
                case MajorPrizeIndexEnum.Prize55To57:
                    BackgroundImage = Properties.Resources._57_56_55_after;
                    break;
                case MajorPrizeIndexEnum.Prize58To60:
                    BackgroundImage = Properties.Resources._60_59_58_after;
                    break;
                case MajorPrizeIndexEnum.Prize61To63:
                    BackgroundImage = Properties.Resources._63_62_61_after;
                    break;
                default:
                    BackgroundImage = Properties.Resources.blank_ticket_b;
                    break;
            }
        }

        private void SetUnblockBackgroundForMajorPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case MajorPrizeIndexEnum.Prize40To42:
                    BackgroundImage = Properties.Resources._42_41_40_before;
                    break;
                case MajorPrizeIndexEnum.Prize43To45:
                    BackgroundImage = Properties.Resources._45_44_43_before;
                    break;
                case MajorPrizeIndexEnum.Prize46To48:
                    BackgroundImage = Properties.Resources._48_47_46_before;
                    break;
                case MajorPrizeIndexEnum.Prize49To51:
                    BackgroundImage = Properties.Resources._51_50_49_before;
                    break;
                case MajorPrizeIndexEnum.Prize52To54:
                    BackgroundImage = Properties.Resources._54_53_52_before;
                    break;
                case MajorPrizeIndexEnum.Prize55To57:
                    BackgroundImage = Properties.Resources._57_56_55_before;
                    break;
                case MajorPrizeIndexEnum.Prize58To60:
                    BackgroundImage = Properties.Resources._60_59_58_before;
                    break;
                case MajorPrizeIndexEnum.Prize61To63:
                    BackgroundImage = Properties.Resources._63_62_61_before;
                    break;
                default:
                    BackgroundImage = Properties.Resources.blank_ticket_a;
                    break;
            }
        }

        private void SetBlockBackgroundForGrandPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case GrandPrizeIndexEnum.Prize10To12:
                    BackgroundImage = Properties.Resources._12_11_10_after;
                    break;
                case GrandPrizeIndexEnum.Prize13To15:
                    BackgroundImage = Properties.Resources._15_14_13_after;
                    break;
                case GrandPrizeIndexEnum.Prize16To18:
                    BackgroundImage = Properties.Resources._18_17_16_after;
                    break;
                case GrandPrizeIndexEnum.Prize19To21:
                    BackgroundImage = Properties.Resources._21_20_19_after;
                    break;
                case GrandPrizeIndexEnum.Prize22To24:
                    BackgroundImage = Properties.Resources._24_23_22_after;
                    break;
                case GrandPrizeIndexEnum.Prize25To27:
                    BackgroundImage = Properties.Resources._27_26_25_after;
                    break;
                case GrandPrizeIndexEnum.Prize28To30:
                    BackgroundImage = Properties.Resources._30_29_28_after;
                    break;
                case GrandPrizeIndexEnum.Prize31To33:
                    BackgroundImage = Properties.Resources._33_32_31_after;
                    break;
                case GrandPrizeIndexEnum.Prize34To36:
                    BackgroundImage = Properties.Resources._36_35_34_after;
                    break;
                case GrandPrizeIndexEnum.Prize37To39:
                    BackgroundImage = Properties.Resources._39_38_37_after;
                    break;
                default:
                    BackgroundImage = Properties.Resources.blank_ticket_b;
                    break;
            }
        }

        private void SetUnblockBackgroundForGrandPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case GrandPrizeIndexEnum.Prize10To12:
                    BackgroundImage = Properties.Resources._12_11_10_before;
                    break;
                case GrandPrizeIndexEnum.Prize13To15:
                    BackgroundImage = Properties.Resources._15_14_13_before;
                    break;
                case GrandPrizeIndexEnum.Prize16To18:
                    BackgroundImage = Properties.Resources._18_17_16_before;
                    break;
                case GrandPrizeIndexEnum.Prize19To21:
                    BackgroundImage = Properties.Resources._21_20_19_before;
                    break;
                case GrandPrizeIndexEnum.Prize22To24:
                    BackgroundImage = Properties.Resources._21_20_19_before;
                    break;
                case GrandPrizeIndexEnum.Prize25To27:
                    BackgroundImage = Properties.Resources._24_23_22_before;
                    break;
                case GrandPrizeIndexEnum.Prize28To30:
                    BackgroundImage = Properties.Resources._27_26_25_before;
                    break;
                case GrandPrizeIndexEnum.Prize31To33:
                    BackgroundImage = Properties.Resources._30_2928_before;
                    break;
                case GrandPrizeIndexEnum.Prize34To36:
                    BackgroundImage = Properties.Resources._36_35_34_before;
                    break;
                case GrandPrizeIndexEnum.Prize37To39:
                    BackgroundImage = Properties.Resources._39_38_37_before;
                    break;
                default:
                    BackgroundImage = Properties.Resources.blank_ticket_a;
                    break;
            }
        }

        private void SetBlockBackgroundForSuperGrandPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case SuperGrandPrizeIndexEnum.Prize01:
                    BackgroundImage = Properties.Resources._1_after;
                    break;
                case SuperGrandPrizeIndexEnum.Prize02:
                    BackgroundImage = Properties.Resources._2_after;
                    break;
                case SuperGrandPrizeIndexEnum.Prize03:
                    BackgroundImage = Properties.Resources._3_after;
                    break;
                case SuperGrandPrizeIndexEnum.Prize04:
                    BackgroundImage = Properties.Resources._4_after;
                    break;
                case SuperGrandPrizeIndexEnum.Prize05:
                    BackgroundImage = Properties.Resources._5_after;
                    break;
                case SuperGrandPrizeIndexEnum.Prize06:
                    BackgroundImage = Properties.Resources._6_after;
                    break;
                case SuperGrandPrizeIndexEnum.Prize07:
                    BackgroundImage = Properties.Resources._7_after;
                    break;
                case SuperGrandPrizeIndexEnum.Prize08:
                    BackgroundImage = Properties.Resources._8_after;
                    break;
                case SuperGrandPrizeIndexEnum.Prize09:
                    BackgroundImage = Properties.Resources._9_after;
                    break;
                default:
                    BackgroundImage = Properties.Resources.blank_ticket_b;
                    break;
            }
        }

        private void SetUnblockBackgroundForSuperGrandPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case SuperGrandPrizeIndexEnum.Prize01:
                    BackgroundImage = Properties.Resources._1_before;
                    break;
                case SuperGrandPrizeIndexEnum.Prize02:
                    BackgroundImage = Properties.Resources._2_before;
                    break;
                case SuperGrandPrizeIndexEnum.Prize03:
                    BackgroundImage = Properties.Resources._3_before;
                    break;
                case SuperGrandPrizeIndexEnum.Prize04:
                    BackgroundImage = Properties.Resources._4_before;
                    break;
                case SuperGrandPrizeIndexEnum.Prize05:
                    BackgroundImage = Properties.Resources._5_before;
                    break;
                case SuperGrandPrizeIndexEnum.Prize06:
                    BackgroundImage = Properties.Resources._6_before;
                    break;
                case SuperGrandPrizeIndexEnum.Prize07:
                    BackgroundImage = Properties.Resources._7_before;
                    break;
                case SuperGrandPrizeIndexEnum.Prize08:
                    BackgroundImage = Properties.Resources._8_before;
                    break;
                case SuperGrandPrizeIndexEnum.Prize09:
                    BackgroundImage = Properties.Resources._9_before;
                    break;
                default:
                    BackgroundImage = Properties.Resources.blank_ticket_a;
                    break;
            }
        }
    }
}
