/*
*	TickLuck
*	All rights reserved
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseZone : MonoBehaviour
{
    #region Fields
    [SerializeField] public GameObject DoneUI;
    [SerializeField] public Text points_txt;
    [SerializeField] private Material wood_material;
    [SerializeField] public List<WoodPart> wood_parts;
    [SerializeField] public List<GameObject> tree_points;

    private int points_counter;

    [Header("For Development")]
    [Space]
    public int out_value;
    #endregion

    #region Unity Methods
    void Start()
    {
        DoneUI.SetActive(false);
        points_counter = tree_points.Count;
        out_value = 0;
        DefaultPointText();

        foreach (var item in wood_parts)
        {
            item.GetComponent<Renderer>().material = wood_material;
        }
    }
    #endregion

    public void DefaultPointText()
    {
        points_txt.text = "0/" + points_counter.ToString();
    }

    public void ModifyPointsText(int new_number)
    {
        out_value += new_number;

        //if (out_value < 0 || out_value > tree_points.Count)
        //    return;

        points_txt.text = out_value.ToString() + "/" + points_counter.ToString();
    }
}
