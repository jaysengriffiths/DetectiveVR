using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class M2_LostCatScript : MonoBehaviour
{
    public GameObject OldLady;
    public float speed;

    public GameObject Blacksmith;
    public GameObject GoblinChief;
    public GameObject FarmerWife;

    public GameObject OldLadyRedFace;
    public GameObject BlacksmithRedFace;
    public GameObject GoblinChiefRedFace;
    public GameObject FarmerWifeRedFace;

    public AudioClip OL_01_NewDetective_4;
    public AudioClip PC_02_HowCanIHelp_3;
    public AudioClip OL_03_CatInvisible_6;
    public AudioClip PC_04_HowDoYouKnow_2;
    public AudioClip OL_05_CantSeeHer_5;
    public AudioClip PC_06_HowToMove_2;
    public AudioClip OL_07_FocusAndMove_7;
    public AudioClip OL_08_LookAtGround_3;
    public AudioClip OL_09_CatKeepsCalling_5;
    public AudioClip PC_10_CatInChimney_3;
    public AudioClip OL_11_IllRescueHer_3;
    public AudioClip PC_12_HaveToFindWho_3;
    public AudioClip PC_13_CluePokerExamine_6;
    public AudioClip PC_14_Thumbprint_3;
    public AudioClip PC_15_RecordClue_5;
    public AudioClip PC_16_NotebookOnShirt_4;
    public AudioClip OL_17_HereIsCat_5;
    public AudioClip PC_18_WhoMight_3;
    public AudioClip OL_19_Mice_2;
    public AudioClip PC_20_RecordThis_3;
    public AudioClip OL_21_DetectivePower_6;
    public AudioClip PC_22_OnlyUseOnce_6;
    public AudioClip OL_23_PleaseUseMagic_4;
    public AudioClip BS_24_WhyDragMeHere_4;
    public AudioClip GC_25_IAmChief_6;
    public AudioClip FW_26_IWasSellingEggs_6;
    public AudioClip PC_27_YoureAllSuspects_5;
    public AudioClip BS_28_JustWeighingIron_6;
    public AudioClip GC_29_ICuffedTheCat_11;
    public AudioClip FW_30_PetTheCat_4;
    public AudioClip PC_31_ReviewNotebook_2;
    public AudioClip PC_32_ChoiceToFineOrWarn_6;
    public AudioClip PC_33_IFineYou_2;
    public AudioClip PC_34_BlacksmithAtone_5;
    public AudioClip PC_35_GoblinChiefRemind_8;
    public AudioClip PC_36_FarmerSaySorry_3;
    public AudioClip PC_37_OldLadyNot_5;
    public AudioClip BS_38_NotABigDeal_4;
    public AudioClip GC_39_DoNotNeedDetective_4;
    public AudioClip FW_40_IWasHelping_5;
    public AudioClip OL_41_OnlyWantedToMeet_6;
    public AudioClip BS_42_DefectiveDetective_4;
    public AudioClip GC_43_WrongDetective_5;
    public AudioClip FW_44_DidntThinkWasMe_4;
    public AudioClip OL_45_HowCouldYou_4;
    public AudioClip BS_46_DawnBellLate_9;

    public AudioClip catAngry1_1s;
    public AudioClip catAngry2_1s;
    public AudioClip catComplain1_3s;
    public AudioClip catComplain2_2s;
    public AudioClip catComplain3_4s;
    public AudioClip catComplain4_4s;
    public AudioClip catDemand1_1s;
    public AudioClip catDemand2_1s;
    public AudioClip catMoan_1s;
    public AudioClip catQuiet1_1s;
    public AudioClip catQuiet2_1s;
    public AudioClip catQuiet3_1s;
    public AudioClip catYowl1_3s;
    
    public AudioSource sourceCat;
    public AudioSource sourcePC;
    public AudioSource sourceOldLady;
    public AudioSource sourceBlacksmith;
    public AudioSource sourceGoblinChief;
    public AudioSource sourceFarmerWife;
    public AudioSource house1Door;
    public AudioSource house2Door;
    public AudioSource house3Door;


    void Start()
    {
        Blacksmith.SetActive(false);
        GoblinChief.SetActive(false);
        FarmerWife.SetActive(false);

        OldLadyRedFace.SetActive(false);
        BlacksmithRedFace.SetActive(false);
        GoblinChiefRedFace.SetActive(false);
        FarmerWifeRedFace.SetActive(false);
            
        StartCoroutine("M2_LostCatDialogue");
    }

    IEnumerator M2_LostCat_Dialogue()
    {
        OldLadyRedFace.SetActive(true);

        sourceOldLady.PlayOneShot(OL_01_NewDetective_4);
        yield return new WaitForSeconds(4);
        
        sourcePC.PlayOneShot(PC_02_HowCanIHelp_3);
        yield return new WaitForSeconds(3);
        
        sourceOldLady.PlayOneShot(OL_03_CatInvisible_6);
        yield return new WaitForSeconds(6);

        sourceCat.PlayOneShot(catAngry1_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catAngry2_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catComplain1_3s);
        yield return new WaitForSeconds(3);

        sourceCat.PlayOneShot(catComplain2_2s);
        yield return new WaitForSeconds(2);

        sourceCat.PlayOneShot(catComplain3_4s);
        yield return new WaitForSeconds(4);

        sourceCat.PlayOneShot(catComplain4_4s);
        yield return new WaitForSeconds(4);

        sourceCat.PlayOneShot(catDemand1_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catDemand2_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catMoan_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catQuiet1_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catQuiet2_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catQuiet3_1s);
        yield return new WaitForSeconds(1);

        sourceCat.PlayOneShot(catYowl1_3s);
        yield return new WaitForSeconds(3);


        sourcePC.PlayOneShot(PC_04_HowDoYouKnow_2);
        yield return new WaitForSeconds(2);

        sourceOldLady.PlayOneShot(OL_05_CantSeeHer_5);
        yield return new WaitForSeconds(5);

        sourcePC.PlayOneShot(PC_06_HowToMove_2);
        yield return new WaitForSeconds(2);

        sourceOldLady.PlayOneShot(OL_07_FocusAndMove_7);
        yield return new WaitForSeconds(7);

        sourceOldLady.PlayOneShot(OL_08_LookAtGround_3);
        yield return new WaitForSeconds(3);

        sourceOldLady.PlayOneShot(OL_09_CatKeepsCalling_5);
        yield return new WaitForSeconds(5);

        sourcePC.PlayOneShot(PC_10_CatInChimney_3);
        yield return new WaitForSeconds(3);

        sourceOldLady.PlayOneShot(OL_11_IllRescueHer_3);
        yield return new WaitForSeconds(3);

        sourcePC.PlayOneShot(PC_12_HaveToFindWho_3);
        yield return new WaitForSeconds(3);

        sourcePC.PlayOneShot(PC_13_CluePokerExamine_6);
        yield return new WaitForSeconds(6);

        sourcePC.PlayOneShot(PC_14_Thumbprint_3);
        yield return new WaitForSeconds(3);

        sourcePC.PlayOneShot(PC_15_RecordClue_5);
        yield return new WaitForSeconds(5);

        sourcePC.PlayOneShot(PC_16_NotebookOnShirt_4);
        yield return new WaitForSeconds(4);

        sourceOldLady.PlayOneShot(OL_17_HereIsCat_5);
        yield return new WaitForSeconds(5);

        sourcePC.PlayOneShot(PC_18_WhoMight_3);
        yield return new WaitForSeconds(3);

        sourceOldLady.PlayOneShot(OL_19_Mice_2);
        yield return new WaitForSeconds(2);

        sourcePC.PlayOneShot(PC_20_RecordThis_3);
        yield return new WaitForSeconds(3);

        sourceOldLady.PlayOneShot(OL_21_DetectivePower_6);
        yield return new WaitForSeconds(6);

        sourcePC.PlayOneShot(PC_22_OnlyUseOnce_6);
        yield return new WaitForSeconds(6);

        sourceOldLady.PlayOneShot(OL_23_PleaseUseMagic_4);
        yield return new WaitForSeconds(4);

        sourceBlacksmith.PlayOneShot(BS_24_WhyDragMeHere_4);
        yield return new WaitForSeconds(4);

        sourceGoblinChief.PlayOneShot(GC_25_IAmChief_6);
        yield return new WaitForSeconds(2);

        sourceFarmerWife.PlayOneShot(FW_26_IWasSellingEggs_6);
        yield return new WaitForSeconds(6);

        sourcePC.PlayOneShot(PC_27_YoureAllSuspects_5);
        yield return new WaitForSeconds(5);

        sourceBlacksmith.PlayOneShot(BS_28_JustWeighingIron_6);
        yield return new WaitForSeconds(6);

        sourceGoblinChief.PlayOneShot(GC_29_ICuffedTheCat_11);
        yield return new WaitForSeconds(11);

        sourceFarmerWife.PlayOneShot(FW_30_PetTheCat_4);
        yield return new WaitForSeconds(4);

        sourcePC.PlayOneShot(PC_31_ReviewNotebook_2);
        yield return new WaitForSeconds(2);

        sourcePC.PlayOneShot(PC_32_ChoiceToFineOrWarn_6);
        yield return new WaitForSeconds(6);

        sourcePC.PlayOneShot(PC_33_IFineYou_2);
        yield return new WaitForSeconds(2);

        sourcePC.PlayOneShot(PC_34_BlacksmithAtone_5);
        yield return new WaitForSeconds(5);

        sourcePC.PlayOneShot(PC_35_GoblinChiefRemind_8);
        yield return new WaitForSeconds(8);

        sourcePC.PlayOneShot(PC_36_FarmerSaySorry_3);
        yield return new WaitForSeconds(3);

        sourcePC.PlayOneShot(PC_37_OldLadyNot_5);
        yield return new WaitForSeconds(5);

        sourceBlacksmith.PlayOneShot(BS_38_NotABigDeal_4);
        yield return new WaitForSeconds(4);

        sourceFarmerWife.PlayOneShot(FW_40_IWasHelping_5);
        yield return new WaitForSeconds(5);

        sourceOldLady.PlayOneShot(OL_41_OnlyWantedToMeet_6);
        yield return new WaitForSeconds(6);

        sourceBlacksmith.PlayOneShot(BS_42_DefectiveDetective_4);
        yield return new WaitForSeconds(4);

        sourceGoblinChief.PlayOneShot(GC_43_WrongDetective_5);
        yield return new WaitForSeconds(5);

        sourceFarmerWife.PlayOneShot(FW_44_DidntThinkWasMe_4);
        yield return new WaitForSeconds(4);

        sourceOldLady.PlayOneShot(OL_45_HowCouldYou_4);
        yield return new WaitForSeconds(4);

        sourceBlacksmith.PlayOneShot(BS_46_DawnBellLate_9);
        yield return new WaitForSeconds(9);


        SceneManager.LoadScene("BellSabotage");

    }

}
