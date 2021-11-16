using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SkillTree;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public int id;

    public TMP_Text TitleText;
    public TMP_Text DescText;

    public int[] ConnectedSkills;

    public void UpdateUI()
    {   //die Textfelder im SKilltree werden manipuliert
        TitleText.text=$"{skillTree.SkillLevels[id]}/{skillTree.SkillCaps[id]}\n{skillTree.SkillNames[id]}";
        DescText.text= $"{skillTree.SkillDescr[id]}\nCost:{skillTree.SkillPoints}/1 SP";

        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.yellow   //die "blätter" des Trees werden je nach bedarf gefärbt
            : skillTree.SkillPoints >= 1 ? Color.green : Color.white;


        foreach(var connectedSkill in ConnectedSkills)      //die  erreichbaren Nodes werden eingefärbt
        {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id] > 0);
            skillTree.ConnectorList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id] > 0);

        }
            
    }

    public void Buy()       //selbsterklärend
    {
        if(skillTree.SkillPoints < 1 || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ) return;
        skillTree.SkillPoints -=1;
        skillTree.SkillLevels[id] ++;
        skillTree.UpdateAllSkillUI();

    }


}
