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
                    BackgroundImage = Properties.Resources.Cash_10mil_64_65_66_unblocked;
                    break;
                case CashPrizeIndexEnum.Prize67To69:
                    BackgroundImage = Properties.Resources.Cash_10mil_67_68_69_unblocked;
                    break;
                case CashPrizeIndexEnum.Prize70To72:
                    BackgroundImage = Properties.Resources.Cash_5mil_70_71_72_unblocked;
                    break;
                case CashPrizeIndexEnum.Prize73To75:
                    BackgroundImage = Properties.Resources.Cash_5mil_73_74_75_unblocked;
                    break;
                case CashPrizeIndexEnum.Prize76To78:
                    BackgroundImage = Properties.Resources.Cash_5mil_76_77_78_unblocked;
                    break;
                case CashPrizeIndexEnum.Prize79To81:
                    BackgroundImage = Properties.Resources.Cash_3mil_79_80_81_unblocked;
                    break;
                case CashPrizeIndexEnum.Prize82To84:
                    BackgroundImage = Properties.Resources.Cash_3mil_82_83_84_unblocked;
                    break;
                case CashPrizeIndexEnum.Prize85To87:
                    BackgroundImage = Properties.Resources.Cash_3mil_85_86_87_unblocked;
                    break;
                case CashPrizeIndexEnum.Prize88To90: 
                    BackgroundImage = Properties.Resources.Cash_3mil_88_89_90_unblocked;
                    break;
                default: 
                    BackgroundImage = Properties.Resources.prizeblock;
                    break;
            }
        }

        private void SetUnblockBackgroundForCashPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case CashPrizeIndexEnum.Prize64To66:
                    BackgroundImage = Properties.Resources.Cash_10mil_64_65_66_blocked;
                    break;
                case CashPrizeIndexEnum.Prize67To69:
                    BackgroundImage = Properties.Resources.Cash_10mil_67_68_69_blocked;
                    break;
                case CashPrizeIndexEnum.Prize70To72:
                    BackgroundImage = Properties.Resources.Cash_5mil_70_71_72_blocked;
                    break;
                case CashPrizeIndexEnum.Prize73To75:
                    BackgroundImage = Properties.Resources.Cash_5mil_73_74_75_blocked;
                    break;
                case CashPrizeIndexEnum.Prize76To78:
                    BackgroundImage = Properties.Resources.Cash_5mil_76_77_78_blocked;
                    break;
                case CashPrizeIndexEnum.Prize79To81:
                    BackgroundImage = Properties.Resources.Cash_3mil_79_80_81_blocked;
                    break;
                case CashPrizeIndexEnum.Prize82To84:
                    BackgroundImage = Properties.Resources.Cash_3mil_82_83_84_blocked;
                    break;
                case CashPrizeIndexEnum.Prize85To87:
                    BackgroundImage = Properties.Resources.Cash_3mil_85_86_87_blocked;
                    break;
                case CashPrizeIndexEnum.Prize88To90:
                    BackgroundImage = Properties.Resources.Cash_3mil_88_89_90_blocked;
                    break;
                default:
                    BackgroundImage = Properties.Resources.prizeblock;
                    break;
            }
        }

        private void SetBlockBackgroundForMajorPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case MajorPrizeIndexEnum.Prize40To42:
                    BackgroundImage = Properties.Resources.Sony_32_led_tv_40_41_42_UNBLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize43To45:
                    BackgroundImage = Properties.Resources.Sony_32_led_tv_43_44_45_UNBLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize46To48:
                    BackgroundImage = Properties.Resources.Sony_32_led_tv_46_47_48_UNBLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize49To51:
                    BackgroundImage = Properties.Resources.Sony_32_led_tv_49_50_51_UNBLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize52To54:
                    BackgroundImage = Properties.Resources.Voucher_gift_pass_6mil_52_53_54_UNBLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize55To57:
                    BackgroundImage = Properties.Resources.Canon_camera_powershot_n_55_56_57_UNBLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize58To60:
                    BackgroundImage = Properties.Resources.Sanyo_fridge_sr_u205pn_58_59_60_UNBLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize61To63:
                    BackgroundImage = Properties.Resources.Panasonic_microwave_61_62_63_UNBLOCKED;
                    break;
                default:
                    BackgroundImage = Properties.Resources.prizeblock;
                    break;
            }
        }

        private void SetUnblockBackgroundForMajorPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case MajorPrizeIndexEnum.Prize40To42:
                    BackgroundImage = Properties.Resources.Sony_32_led_tv_40_41_42_BLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize43To45:
                    BackgroundImage = Properties.Resources.Sony_32_led_tv_43_44_45_BLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize46To48:
                    BackgroundImage = Properties.Resources.Sony_32_led_tv_46_47_48_BLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize49To51:
                    BackgroundImage = Properties.Resources.Sony_32_led_tv_49_50_51_BLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize52To54:
                    BackgroundImage = Properties.Resources.Voucher_gift_pass_6mil_52_53_54_BLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize55To57:
                    BackgroundImage = Properties.Resources.Canon_camera_powershot_n_55_56_57_BLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize58To60:
                    BackgroundImage = Properties.Resources.Sanyo_fridge_sr_u205pn_58_59_60_BLOCKED;
                    break;
                case MajorPrizeIndexEnum.Prize61To63:
                    BackgroundImage = Properties.Resources.Panasonic_microwave_61_62_63_BLOCKED;
                    break;
                default:
                    BackgroundImage = Properties.Resources.prizeblock;
                    break;
            }
        }

        private void SetBlockBackgroundForGrandPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case GrandPrizeIndexEnum.Prize10To12:
                    BackgroundImage = Properties.Resources.Apple_iphone_5s_16gb_10_11_12_UNBLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize13To15:
                    BackgroundImage = Properties.Resources.Samsung_galaxy_note_3_N9000_16Gb_13_14_15_UNBLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize16To18:
                    BackgroundImage = Properties.Resources.Apple_ipad_air_16g_16_17_18_unBLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize19To21:
                    BackgroundImage = Properties.Resources.Apple_ipad_air_16g_19_20_21_UNBLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize22To24:
                    BackgroundImage = Properties.Resources.Apple_ipad_air_16g_22_23_24_UNBLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize25To27:
                    BackgroundImage = Properties.Resources.Lenovo_laptop_cpu_core_i5_25_26_27_UNBLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize28To30:
                    BackgroundImage = Properties.Resources.Lenovo_laptop_cpu_core_i5_28_29_30_UNBLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize31To33:
                    BackgroundImage = Properties.Resources.Apple_ipad_mini_16g_wifi_4g_31_32_33_UNBLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize34To36:
                    BackgroundImage = Properties.Resources.Apple_ipad_mini_16g_wifi_4g_34_35_36_UNBLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize37To39:
                    BackgroundImage = Properties.Resources.Nokia_Lumia_925_37_38_39_UNBLOCKED;
                    break;
                default:
                    BackgroundImage = Properties.Resources.prizeblock;
                    break;
            }
        }

        private void SetUnblockBackgroundForGrandPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case GrandPrizeIndexEnum.Prize10To12:
                    BackgroundImage = Properties.Resources.Apple_iphone_5s_16gb_10_11_12_BLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize13To15:
                    BackgroundImage = Properties.Resources.Samsung_galaxy_note_3_N9000_16Gb_13_14_15_BLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize16To18:
                    BackgroundImage = Properties.Resources.Apple_ipad_air_16g_16_17_18_BLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize19To21:
                    BackgroundImage = Properties.Resources.Apple_ipad_air_16g_19_20_21_BLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize22To24:
                    BackgroundImage = Properties.Resources.Apple_ipad_air_16g_22_23_24_BLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize25To27:
                    BackgroundImage = Properties.Resources.Lenovo_laptop_cpu_core_i5_25_26_27_BLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize28To30:
                    BackgroundImage = Properties.Resources.Lenovo_laptop_cpu_core_i5_28_29_30_BLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize31To33:
                    BackgroundImage = Properties.Resources.Apple_ipad_mini_16g_wifi_4g_31_32_33_BLOCKED;
                    break;
                case GrandPrizeIndexEnum.Prize34To36:
                    BackgroundImage = Properties.Resources.Apple_ipad_mini_16g_wifi_4g_34_35_36_BLOCK;
                    break;
                case GrandPrizeIndexEnum.Prize37To39:
                    BackgroundImage = Properties.Resources.Nokia_Lumia_925_37_38_39_BLOCKED;
                    break;
                default:
                    BackgroundImage = Properties.Resources.prizeblock;
                    break;
            }
        }

        private void SetBlockBackgroundForSuperGrandPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case SuperGrandPrizeIndexEnum.Prize01:
                    BackgroundImage = Properties.Resources.Super_Grand_yamaha_exciter_1_UNBLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize02:
                    BackgroundImage = Properties.Resources.Super_Grand_honda_Lead_2_UNBLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize03:
                    BackgroundImage = Properties.Resources.Super_Grand_honda_vision_3_UNBLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize04:
                    BackgroundImage = Properties.Resources.Super_Grand_superstar_travel_voucher_4_UNBLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize05:
                    BackgroundImage = Properties.Resources.Super_Grand_superstar_travel_voucher_5_UNBLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize06:
                    BackgroundImage = Properties.Resources.Super_Grand_saigontourist_travel_voucher_6_UNBLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize07:
                    BackgroundImage = Properties.Resources.Super_Grand_saigontourist_travel_voucher_7_UNBLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize08:
                    BackgroundImage = Properties.Resources.Super_Grand_sony_bdv_N9100_8_UNBLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize09:
                    BackgroundImage = Properties.Resources.Super_Grand_canon_eos700D_9_UNBLOCKED;
                    break;
                default:
                    BackgroundImage = Properties.Resources.prizeblock;
                    break;
            }
        }

        private void SetUnblockBackgroundForSuperGrandPrize(int prizeIndex)
        {
            switch (prizeIndex)
            {
                case SuperGrandPrizeIndexEnum.Prize01:
                    BackgroundImage = Properties.Resources.Super_Grand_yamaha_exciter_1_BLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize02:
                    BackgroundImage = Properties.Resources.Super_Grand_honda_Lead_2_BLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize03:
                    BackgroundImage = Properties.Resources.Super_Grand_honda_vision_3_BLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize04:
                    BackgroundImage = Properties.Resources.Super_Grand_superstar_travel_voucher_4_BLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize05:
                    BackgroundImage = Properties.Resources.Super_Grand_superstar_travel_voucher_5_BLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize06:
                    BackgroundImage = Properties.Resources.Super_Grand_saigontourist_travel_voucher_6_BLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize07:
                    BackgroundImage = Properties.Resources.Super_Grand_saigontourist_travel_voucher_7_BLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize08:
                    BackgroundImage = Properties.Resources.Super_Grand_sony_bdv_N9100_8_BLOCKED;
                    break;
                case SuperGrandPrizeIndexEnum.Prize09:
                    BackgroundImage = Properties.Resources.Super_Grand_canon_eos700D_9;
                    break;
                default:
                    BackgroundImage = Properties.Resources.prizeblock;
                    break;
            }
        }
    }
}
